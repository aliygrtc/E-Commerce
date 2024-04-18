using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;

namespace Wooland.UI.Areas.admin.Controllers
{
	[Area("admin"), Authorize]
	public class ProductPictureController : Controller
	{
		IRepository<ProductPicture> repoProductPicture;
		public ProductPictureController(IRepository<ProductPicture> _repoProductPicture)
		{
			repoProductPicture = _repoProductPicture;
		}
		public IActionResult Index(int productid)
		{
			ViewBag.ProductId = productid;
			return View(repoProductPicture.GetAll(x => x.ProductID == productid).OrderByDescending(x => x.ID));
		}
		public IActionResult New(int productid)
		{
			ViewBag.ProductId = productid;
			return View();
		}
		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Insert(ProductPicture model)
		{
			if (ModelState.IsValid)
			{
				if (Request.Form.Files.Any())
				{
					if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture")))
					{
						Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture"));
					}
					string dosyaAdi = Request.Form.Files["Picture"].FileName;
					using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture", dosyaAdi), FileMode.Create))
					{
						await Request.Form.Files["Picture"].CopyToAsync(stream);
					}
					model.Picture = "/images/ProductPicture/" + dosyaAdi;
				}
				repoProductPicture.Add(model);

				return RedirectToAction("Index", new {productid = model.ProductID});
			}
			else return RedirectToAction("New");
		}
		public IActionResult Edit(int id)
		{
			ProductPicture ProductPicture = repoProductPicture.GetBy(x => x.ID == id);
			if (ProductPicture != null) return View(ProductPicture);
			else return RedirectToAction("Index");
		}
		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(ProductPicture model)
		{
			if (ModelState.IsValid)
			{
				if (Request.Form.Files.Any())
				{
					if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture")))
					{
						Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture"));
					}
					string dosyaAdi = Request.Form.Files["Picture"].FileName;
					using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProductPicture", dosyaAdi), FileMode.Create))
					{
						await Request.Form.Files["Picture"].CopyToAsync(stream);
					}
					model.Picture = "/images/ProductPicture/" + dosyaAdi;
				}
				repoProductPicture.Update(model);

				return RedirectToAction("Index", new { productid = model.ProductID });
			}
			else return RedirectToAction("Edit");
		}
		public IActionResult Delete(int id)
		{
			ProductPicture ProductPicture = repoProductPicture.GetBy(x => x.ID == id);
			if (ProductPicture != null)
			{
				if (!string.IsNullOrEmpty(ProductPicture.Picture))
				{
					string _pathFile = Directory.GetCurrentDirectory() + string.Format(@"\wwwroot") + ProductPicture.Picture.Replace("/", "\\");
					FileInfo fileInfo = new FileInfo(_pathFile);
					if (fileInfo.Exists) fileInfo.Delete();
				}
				repoProductPicture.Delete(ProductPicture);
			}
			return RedirectToAction("Index");
		}
	}
}
