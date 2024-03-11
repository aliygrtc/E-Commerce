using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wooland.DAL.Entities
{
    public class Brand
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(30)"), StringLength(30), Display(Name = "Marka Adı"), Required(ErrorMessage = "Marka Adı Giriniz")]
        public string Name { get; set; }
        public int DisplayIndex { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
