using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;
using Wooland.UI.Tools;

namespace Wooland.UI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class UserController : Controller
    {
        IRepository<User> repoUser;
        public UserController(IRepository<User> _repoUser)
        {
            repoUser = _repoUser;
        }
        public IActionResult Index()
        {
            return View(repoUser.GetAll().OrderByDescending(x => x.ID));
        }

        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Insert(User model)
        {
            model.Password = GeneralTools.GetMD5(model.Password);
            if (ModelState.IsValid)
            {
                repoUser.Add(model);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("NewUser");
        }
        public IActionResult Edit(int id)
        {
            User User = repoUser.GetBy(x => x.ID == id);
            if (User != null) return View(User);
            else return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(User model)
        {
			model.Password = GeneralTools.GetMD5(model.Password);
			if (ModelState.IsValid)
            {
                repoUser.Update(model);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("Edit");
        }

        public IActionResult Delete(int id)
        {
            User User = repoUser.GetBy(x => x.ID == id);
            if (User != null)repoUser.Delete(User);
            return RedirectToAction("Index");
        }
    }
}
