using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;

namespace Wooland.UI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class BrandController : Controller
    {
        IRepository<Brand> repoBrand;
        public BrandController(IRepository<Brand> _repoBrand)
        {
            repoBrand = _repoBrand;
        }
        public IActionResult Index()
        {
            return View(repoBrand.GetAll().OrderByDescending(x => x.ID));
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Insert(Brand model)
        {
            if (ModelState.IsValid)
            {
                repoBrand.Add(model);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("New");
        }
        public IActionResult Edit(int id)
        {
            Brand Brand = repoBrand.GetBy(x => x.ID == id);
            if (Brand != null) return View(Brand);
            else return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(Brand model)
        {
            if (ModelState.IsValid)
            {
                repoBrand.Update(model);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("Edit");
        }

        public IActionResult Delete(int id)
        {
            Brand Brand = repoBrand.GetBy(x => x.ID == id);
            if (Brand != null)repoBrand.Delete(Brand);
            return RedirectToAction("Index");
        }
    }
}
