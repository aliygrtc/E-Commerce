using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;
using Wooland.UI.ViewModel;

namespace Wooland.UI.Controllers
{
	public class HomeController : Controller
	{
		IRepository<Slide> repoSlide;
		IRepository<Product> repoProduct;
		IRepository<User> repoUser;
		private readonly IRepository<Category> categoryRepo;
		public HomeController(IRepository<Slide> _repoSlide, IRepository<Product> _repoProduct, IRepository<Category> _categoryRepo, IRepository<User> _repoUser)
		{
			repoSlide = _repoSlide;
			repoProduct = _repoProduct;
			categoryRepo = _categoryRepo;
			repoUser = _repoUser;
		}
		public IActionResult Index()
		{
			if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
			{
				string id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
				ViewBag.Login = 1;
				IndexVM indexVM = new IndexVM()
				{
					Slides = repoSlide.GetAll().OrderBy(x => x.DisplayIndex),
					Products = repoProduct.GetAll(x => x.Status && x.User.ID.ToString() != id).Include(x => x.ProductPicture).Include(x => x.Category).OrderBy(x => Guid.NewGuid()).Take(9)
				};
				return View(indexVM);
			}
			else
			{
				ViewBag.Login = 0;
				IndexVM indexVM = new IndexVM()
				{
					Slides = repoSlide.GetAll().OrderBy(x => x.DisplayIndex),
					Products = repoProduct.GetAll(x => x.Status).Include(x => x.ProductPicture).Include(x => x.Category).OrderBy(x => Guid.NewGuid()).Take(9)
				};
				return View(indexVM);
			}
		}
		[Route("/shoplist/{categoryId}")]
		public async Task<IActionResult> ListShop(int categoryId)
		{
			var category = await categoryRepo.GetAll(x => x.ID == categoryId).Include(x => x.SubCategories).FirstAsync();
			var products = new List<Product>();
			var claimsList = User.Claims.ToList();
			if (User.Identity.IsAuthenticated)
			{
				
				products = await repoProduct.GetAll(x => x.CategoryID == categoryId && x.UserID != Convert.ToInt32(claimsList[0].Value))
					.Where(x => x.Stock > 0)
					.Include(x => x.ProductPicture)
					.Include(x => x.Category)
					.ToListAsync();
			}
			else
			{
				products = await repoProduct.GetAll(x => x.CategoryID == categoryId)
					.Where(x => x.Stock > 0)
					.Include(x => x.ProductPicture)
					.Include(x => x.Category)
					.ToListAsync();
			}
			if (category.SubCategories.Count > 0)
			{
				foreach (var subCategory in category.SubCategories)
				{
					var upperCategoryProducts = new List<Product>();
					if(User.Identity.IsAuthenticated)
					{
						upperCategoryProducts = await repoProduct.GetAll(x => x.CategoryID == subCategory.ID && x.UserID != Convert.ToInt32(claimsList[0].Value)).Where(x => x.Stock > 0)
						.Include(x => x.ProductPicture)
						.Include(x => x.Category)
						.ToListAsync();
					}
					else
					{
						upperCategoryProducts = await repoProduct.GetAll(x => x.CategoryID == subCategory.ID).Where(x => x.Stock > 0)
						.Include(x => x.ProductPicture)
						.Include(x => x.Category)
						.ToListAsync();
					}

					products.AddRange(upperCategoryProducts);
				}
			}
			return View(products);
		}
	}
}
