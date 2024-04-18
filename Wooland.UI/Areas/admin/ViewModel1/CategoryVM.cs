using Wooland.DAL.Entities;

namespace Wooland.UI.Areas.admin.ViewModel1
{
    public class CategoryVM
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
