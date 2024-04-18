using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wooland.DAL.Entities
{
	public class Product
	{
		public int ID { get; set; }

		[Column(TypeName = "varchar(100)"), StringLength(100), Display(Name = "Ürün Adı"), Required(ErrorMessage = "Ürün Adı Giriniz")]
		public string Name { get; set; }

		[Column(TypeName = "decimal(18,2)"), Display(Name = "Fiyat Bilgisi"), Required(ErrorMessage = "Fiyat Giriniz")]
		public decimal Price { get; set; }

		[Display(Name = "Stok Miktarı")]
		public int Stock { get; set; }

		[Column(TypeName = "varchar(250)"), StringLength(250), Display(Name = "Ürün Açıklaması")]
		public string Description { get; set; }

		[Column(TypeName = "text"), Display(Name = "Ürün Detayı")]
		public string Detail { get; set; }

		[Column(TypeName = "text"), Display(Name = "Kargo Detayı")]
		public string CargoDetail { get; set; }

		[Range(0, 5, ErrorMessage = "Yıldız puanı 0 ile 5 arasında olmalıdır.")]
		[Display(Name = "Yıldız Puanı")]
		public int Star { get; set; }

		[Display(Name = "Aktiflik Durumu")]
		public bool Status { get; set; }


		[Display(Name = "Kategorisi")]
		public int? CategoryID { get; set; }
		public Category Category { get; set; }


		[Display(Name = "Markası")]
		public int? BrandID { get; set; }
		public Brand Brand { get; set; }


		[Display(Name = "Kullanıcısı")]
		public int? UserID { get; set; }
		public User User { get; set; }

        public ICollection<ProductPicture> ProductPicture { get; set; }


    }
}
