using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;

namespace Wooland.UI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        IRepository<Category> repoCategory;
        public HeaderViewComponent(IRepository<Category> _repoCategory)
        {
            repoCategory = _repoCategory;
        }

        public IViewComponentResult Invoke()
        {
            return View(repoCategory.GetAll().Include(x => x.SubCategories).OrderBy(x => x.DisplayIndex));
        }
    }
}
