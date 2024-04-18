using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;

namespace Wooland.UI.Controllers
{
	public class ProductController : Controller 
	{
		IRepository<Product> repoProduct;
		IRepository<Category> repoCategory;
		
		
		public ProductController(IRepository<Product> _repoProduct)
		{
			repoProduct = _repoProduct;
			
		}

        public IActionResult Index()
		{
			return View();
		}

		[Route("/product/detail/{name}/{id}")]
		public IActionResult Detail(string name, int id)
		{
            Product product = repoProduct.GetAll(x => x.ID == id)
                                  .Include(x => x.Category)
                                  .Include(x => x.ProductPicture)
                                  .Include(x => x.User)
                                  .Include(x => x.Brand)
                                  .OrderBy(x => x.ID)
                                  .FirstOrDefault();

            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
            }
            else
            {
                ViewBag.Login = 0;
            }

            return View(product);
        }		
    }
}

