using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Newtonsoft.Json;
using System.Security.Claims;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;
using Wooland.UI.Models;
using Wooland.UI.ViewModel;

namespace Wooland.UI.Controllers
{
	public class CartController : Controller
	{
		IRepository<Product> repoProduct;
		IRepository<Order> repoOrder;
		IRepository<OrderDetails> repoOrderDetails;

		public CartController(IRepository<Product> _repoProduct, IRepository<Order> _repoOrder, IRepository<OrderDetails> _repoOrderDetails, IRepository<User> _repoUser)
		{
			repoProduct = _repoProduct;
			repoOrder = _repoOrder;
			repoOrderDetails = _repoOrderDetails;
		}


		[Route("/sepet/sepeteekle"), HttpPost]
		public string AddCart(int productid, int quantity)
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				Product product = repoProduct.GetAll(x => x.ID == productid).Include(x => x.ProductPicture).FirstOrDefault();
				bool varMi = false;
				if (product != null)
				{
					string text = string.Empty;
					if (product.ProductPicture.Count != 0)
						text = product.ProductPicture.FirstOrDefault().Picture;
					else
						text = "/img/hazirlaniyor.jpg";
					Cart cart = new Cart()
					{
						ProductId = productid,
						ProductName = product.Name,
						ProductPicture = text,
						ProductPrice = product.Price,
						Quantity = quantity
					};
					List<Cart> carts = new List<Cart>();
					if (Request.Cookies["MyCart"] != null)
					{
						carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
						foreach (Cart c in carts)
						{
							if (c.ProductId == productid)
							{
								varMi = true;
								if (c.ProductId == productid) c.Quantity += quantity;
								break;
							}
						}
					}
					if (!varMi) carts.Add(cart);
					CookieOptions options = new();
					options.Expires = DateTime.Now.AddHours(3);
					Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), options);
					return product.Name;
				}
				return "~ Ürün Bulunamadı";
			}
			else
			{
				ViewBag.Login = 0;
				return "~ Oturum Kapalı";
			}
		}

		[Route("/sepet/sepetsayisi")]
		public int CartCount()
		{
			int sayi = 0;
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				if (Request.Cookies["MyCart"] != null)
				{
					List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
					sayi = carts.Sum(x => x.Quantity);
				}
			}
			else
			{
				ViewBag.Login = 0;
			}

			return sayi;
		}

		[Route("/sepet/sepettensil"), HttpPost]
		public string RemoveCart(int productid)
		{
			string rtn = "";
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				if (Request.Cookies["MyCart"] != null)
				{
					List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
					bool varMi = false;
					foreach (Cart c in carts)
					{
						if (c.ProductId == productid)
						{
							varMi = true;
							carts.Remove(c);
							break;
						}
					}
					if (varMi == true)
					{
						CookieOptions options = new();
						options.Expires = DateTime.Now.AddHours(2);
						Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), options);
						rtn = "OK";
					}
				}
			}
			else
			{
				ViewBag.Login = 0;
			}

			return rtn;
		}

		[Route("/sepet")]
		public IActionResult Index()
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				if (Request.Cookies["MyCart"] != null)
				{
					List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
					if (carts.Count <= 0)
					{
						return Redirect("/");
					}
					else
						return View(carts);
				}
				else
					return Redirect("/");
			}
			else
			{
				ViewBag.Login = 0;
				return Redirect("/");
			}

		}

		[Route("/sepet/ödemeyap")]
		public IActionResult CheckOut()
		{
			ViewBag.ShippingFee = 500;
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				if (Request.Cookies["MyCart"] != null)
				{
					List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
					CheckOutVM checkOutVM = new CheckOutVM()
					{
						Order = new Order(),
						Carts = carts
					};
					return View(checkOutVM);
				}
				else
				{
					return Redirect("/");
				}
			}
			else
			{
				ViewBag.Login = 0;
				return Redirect("/");
			}

		}

		[Route("/sepet/temizle"), HttpPost]
		public IActionResult ClearCart()
		{
			Response.Cookies.Delete("MyCart");

			return Redirect("/sepet");
		}

		[Route("/sepet/ödemeyap"), HttpPost, ValidateAntiForgeryToken]
		public IActionResult CheckOut(CheckOutVM model)
		{
			model.Order.RecDate = DateTime.Now;
			string orderNumber = DateTime.Now.Microsecond.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Microsecond.ToString() +
								DateTime.Now.Microsecond.ToString();
			if (orderNumber.Length > 20) orderNumber = orderNumber.Substring(0, 20);
			model.Order.OrderNumber = orderNumber;
			model.Order.OrderStatus = EOrderStatus.Hazirlaniyor;
			model.Order.PaymentOption = EPaymentOption.KrediKarti;
			repoOrder.Add(model.Order);
			List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
			foreach (Cart c in carts)
			{
				OrderDetails od = new OrderDetails()
				{
					OrderID = model.Order.ID,
					ProductID = c.ProductId,
					ProductName = c.ProductName,
					ProductPicture = c.ProductPicture,
					ProductPrice = c.ProductPrice,
					Quantity = c.Quantity
				};
				repoOrderDetails.Add(od);
			}

			ClearCart();

            return RedirectToAction("Success");
        }

        [Route("/success")]
        public IActionResult Success()
        {
            return View();
        }
    }
}
