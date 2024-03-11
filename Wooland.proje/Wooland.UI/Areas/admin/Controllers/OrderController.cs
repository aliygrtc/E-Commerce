using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;

namespace Wooland.UI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class OrderController : Controller
    {
        IRepository<Order> repoOrder;
        IRepository<Product> repoProduct;

        public OrderController(IRepository<Order> _repoOrder, IRepository<Product> _repoProduct)
        {
            repoOrder = _repoOrder;
            repoProduct = _repoProduct;
        }

        public IActionResult Index()
        {
            return View(repoOrder.GetAll().OrderByDescending(x => x.ID));
        }

        
    }
}
