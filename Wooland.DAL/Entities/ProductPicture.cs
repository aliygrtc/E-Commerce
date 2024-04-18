using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wooland.DAL.Entities
{
	public class ProductPicture
	{
		public int ID { get; set; }
		[Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Ürün Fotoğraf Adı"), Required(ErrorMessage = "Ürünün Fotoğrafını Girin")]
		public string Name { get; set; }

		[Column(TypeName = "varchar(150)"), StringLength(150), Display(Name = "Fotoğraf Dosyası")]
		public string Picture { get; set; }

		[Display(Name = "Görüntüleme Sırası")]
		public int DisplayIndex { get; set; }

		[Display(Name = "Ürün Adı")]
		public int ProductID { get; set; }
		public Product Product { get; set; }
	}
}
