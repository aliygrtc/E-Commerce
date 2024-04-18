using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;
using Wooland.UI.Tools;
using Wooland.UI.ViewModel;

namespace Wooland.UI.Controllers
{
	public class AccountController : Controller
	{
		IRepository<User> repoRegister;
		public AccountController(IRepository<User> _repoRegister)
		{
			repoRegister = _repoRegister;
		}

		public IActionResult NewRegister()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult NewRegister(User model)
		{
			
			if (ModelState.IsValid)
			{
				model.Password = GeneralTools.GetMD5(model.Password);
				model.Status = true;
				model.LastLoginDate = DateTime.Now;
				repoRegister.Add(model);

				return RedirectToAction("Index", "Home");
			}
			return View();
		}
	}
}
