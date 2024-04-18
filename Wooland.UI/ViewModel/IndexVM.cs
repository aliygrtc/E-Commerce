using Wooland.DAL.Entities;

namespace Wooland.UI.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<Slide> Slides { get; set; }
        public IEnumerable<Product> Products { get; set; }

    }
}
