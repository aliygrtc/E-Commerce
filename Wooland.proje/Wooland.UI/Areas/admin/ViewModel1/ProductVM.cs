using Wooland.DAL.Entities;

namespace Wooland.UI.Areas.admin.ViewModel1
{
	public class ProductVM
	{
		public Product Product { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Brand> Brands { get; set; }
		public IEnumerable<User> Users { get; set; }
		public int Star { get; set; }
	}
}
