using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;
using Wooland.UI.Areas.admin.ViewModel1;

namespace Wooland.UI.Areas.admin.Controllers
{
	[Area("admin"), Authorize]
	public class ProductController : Controller
	{
		IRepository<Product> repoProduct;
        IRepository<User> repoUser;
        IRepository<Brand> repoBrand;
		IRepository<Category> repoCategory;
		
		public ProductController(IRepository<Product> _repoProduct, IRepository<User> _repoUser, IRepository<Brand> _repoBrand, IRepository<Category> _repoCategory )
		{
			repoProduct = _repoProduct;
            repoUser = _repoUser;
            repoBrand = _repoBrand;
			repoCategory = _repoCategory;
			
		}
		public IActionResult Index()
		{
			return View(repoProduct.GetAll().OrderByDescending(x => x.ID));
		}

		public IActionResult New()
		{
			ProductVM productVM = new ProductVM
			{
				Product = new Product(),
				Brands = repoBrand.GetAll().OrderBy(x => x.Name),
				Categories = repoCategory.GetAll().Include(x => x.ParentCategory).Include(x => x.SubCategories),
				Users = repoUser.GetAll().OrderBy(x => x.ID)
			};
			return View(productVM);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Insert(ProductVM model)
		{
			if (ModelState.IsValid)
			{
				repoProduct.Add(model.Product);

				return RedirectToAction("Index");
			}
			else return RedirectToAction("New");
		}
		public IActionResult Edit(int id)
		{
			Product product = repoProduct.GetBy(x => x.ID == id);
			ProductVM productVM = new ProductVM
			{
				Product = product,
				Brands = repoBrand.GetAll().OrderBy(x => x.Name),
				Categories = repoCategory.GetAll().Include(x => x.ParentCategory).Include(x => x.SubCategories),
				Users = repoUser.GetAll().OrderBy(x => x.ID)
			};
			if (product != null) return View(productVM);
			else return RedirectToAction("Index");
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Update(ProductVM model)
		{
			if (ModelState.IsValid)
			{
				repoProduct.Update(model.Product);

				return RedirectToAction("Index");
			}
			else return RedirectToAction("Edit");
		}

		public IActionResult Delete(int id)
		{
			Product Product = repoProduct.GetBy(x => x.ID == id);
			if (Product != null) repoProduct.Delete(Product);
			return RedirectToAction("Index");
		}
	}
}
