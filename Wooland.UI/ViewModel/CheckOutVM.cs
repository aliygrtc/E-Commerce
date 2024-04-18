using Wooland.DAL.Entities;
using Wooland.UI.Models;

namespace Wooland.UI.ViewModel
{
	public class CheckOutVM
	{
		public Order Order { get; set; }

		public IEnumerable<Cart> Carts { get; set; }
	}
}
