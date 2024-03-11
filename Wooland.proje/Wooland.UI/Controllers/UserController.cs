using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;
using Wooland.UI.Tools;
using Wooland.UI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wooland.UI.Models;

namespace Wooland.UI.Controllers
{
	public class UserController : Controller
	{
		IRepository<User> repoUser;
		IRepository<Product> repoProduct;
		IRepository<Brand> repoBrand;
		IRepository<Category> repoCategory;

		public UserController(IRepository<User> _repoUser, IRepository<Product> _repoProduct, IRepository<Brand> _repoBrand, IRepository<Category> _repoCategory)
		{
			repoUser = _repoUser;
			repoProduct = _repoProduct;
			repoBrand = _repoBrand;
			repoCategory = _repoCategory;
		}
		public IActionResult Index()
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
			}
			else
			{
				ViewBag.Login = 0;
			}
			return View();
		}

		[Route("/user/login")]
		public IActionResult Login()
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
			}
			else
			{
				ViewBag.Login = 0;
			}
			return View();
		}

		[Route("/user/login"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(string username, string password)
		{
			string md5Password = GeneralTools.GetMD5(password);
			User user = repoUser.GetBy(x => x.UserName == username && x.Password == md5Password && x.Status);
			
			if (user != null)
			{
				List<Claim> claims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
					new Claim("adsoyad", user.NameSurname),
					new Claim(ClaimTypes.Role, "User")
				};
				ClaimsIdentity identity = new ClaimsIdentity(claims, "UserCookieScheme");
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });

				ViewBag.Login = 1;
				return Redirect("/");
			}
			else
				TempData["bilgi"] = "Kullanıcı adı veya Parola Bilgisi Hatalı";

			return RedirectToAction("login");
		}

		public async Task<IActionResult> LogOut()
		{
			if (Request.Cookies["MyCart"] != null)
			{
				Response.Cookies.Delete("MyCart");
			}
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}


		[Route("/user/profile")]
		public IActionResult Profile()
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				string id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
				UserVM userVM = new UserVM
				{
					Users = repoUser.GetBy(x => x.ID.ToString() == id),
					Products = repoProduct.GetAll(x => x.UserID.ToString() == id).Include(x => x.Category).Include(x => x.Brand)
				};
				ViewBag.Login = 1;
				return View(userVM);
			}
			else
			{
				ViewBag.Login = 0;
				return Redirect("/");
			}
		}

		[Route("/user/newproduct")]
		public IActionResult New()
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				string id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
				UserVM userVM = new UserVM
				{
					Product = new Product(),
					Brands = repoBrand.GetAll().OrderBy(x => x.Name),
					Categories = repoCategory.GetAll().OrderBy(x => x.Name),
					Users = repoUser.GetBy(x => x.ID.ToString() == id)
				};
				return View(userVM);
			}
			else
			{
				ViewBag.Login = 0;
				return Redirect("/");
			}
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Insert(UserVM userVM)
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				string id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
				userVM.Product.User = repoUser.GetBy(x => x.ID.ToString() == id);
				userVM.Product.Status = false;
				if (ModelState.IsValid)
				{
					if (Request.Form.Files.Any())
					{
						if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture")))
						{
							Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture"));
						}
						string dosyaAdi = Request.Form.Files[0].FileName;
						using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture", dosyaAdi), FileMode.Create))
						{
							await Request.Form.Files[0].CopyToAsync(stream);
						}
						if (userVM.Product.ProductPicture.Count > 0)
						{
							userVM.Product.ProductPicture.First().Picture = "/images/ProductPicture/" + dosyaAdi;
						}
						else
						{
							var Ppicture = new ProductPicture()
							{
								DisplayIndex = 1,
								Name = userVM.Product.Name + "_resim",
								ProductID = userVM.Product.ID,
								Picture = "/images/ProductPicture/" + dosyaAdi
							};
							userVM.Product.ProductPicture.Add(Ppicture);
						}
					}
					repoProduct.Add(userVM.Product);

					return Redirect("profile");
				}
				else
					return RedirectToAction("New");

			}
			else
			{
				ViewBag.Login = 0;
				return Redirect("/");
			}
		}

		[Route("/user/editproduct/{productid}")]
		public IActionResult Edit(int productid)
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				Product product = repoProduct.GetBy(x => x.ID == productid);
				string id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
				UserVM userVM = new UserVM
				{
					Product = product,
					Brands = repoBrand.GetAll().OrderBy(x => x.Name),
					Categories = repoCategory.GetAll().OrderBy(x => x.Name),
					Users = repoUser.GetBy(x => x.ID.ToString() == id),
				};
				if (product != null) return View(userVM);
				else return RedirectToAction("profile");
			}
			else
			{
				ViewBag.Login = 0;
				return Redirect("/");
			}
		}

		[HttpPost, ValidateAntiForgeryToken, Route("/user/editproduct/{productid}")]
		public async Task<IActionResult> Edit(UserVM userVM)
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				if (ModelState.IsValid)
				{
					string id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
					userVM.Product.User = repoUser.GetBy(x => x.ID.ToString() == id);
					if (Request.Form.Files.Any())
					{
						if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture")))
						{
							Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture"));
						}
						string dosyaAdi = Request.Form.Files[0].FileName;
						using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture", dosyaAdi), FileMode.Create))
						{
							await Request.Form.Files[0].CopyToAsync(stream);
						}
						if(userVM.Product.ProductPicture.Count > 0)
						{
							userVM.Product.ProductPicture.First().Picture = "/images/ProductPicture/" + dosyaAdi;
						}
						else
						{
							var Ppicture = new ProductPicture()
							{
								DisplayIndex = 1,
								Name = userVM.Product.Name + "_resim",
								ProductID = userVM.Product.ID,
								Picture = "/images/ProductPicture/" + dosyaAdi
						};
							userVM.Product.ProductPicture.Add(Ppicture);
						}
					}
					repoProduct.Update(userVM.Product);

					return RedirectToAction("profile");
				}
				else return RedirectToAction("edit");
			}
			else
			{
				ViewBag.Login = 0;
				return Redirect("/");
			}
		}

		public IActionResult Delete(int productid)
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				ViewBag.Login = 1;
				Product product = repoProduct.GetBy(x => x.ID == productid);
				if (product != null) repoProduct.Delete(product);
				return RedirectToAction("profile");
			}
			else
			{
				ViewBag.Login = 0;
				return Redirect("/");
			}
		}
	}
}
