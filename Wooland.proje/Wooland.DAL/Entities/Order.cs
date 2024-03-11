using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wooland.DAL.Entities
{
	public class Order
	{
		public int ID { get; set; }

		[Column(TypeName = "Varchar(20)"), StringLength(20), Display(Name = "Sipariş Numarası")]
		public string OrderNumber { get; set; }

		[Display(Name = "Ödeme Seçeneği")]
		public EPaymentOption PaymentOption { get; set; }

		[Display(Name = "Sipariş Durumu")]
		public EOrderStatus OrderStatus { get; set; }


		[Display(Name = "Sipariş Tarihi")]
		public DateTime RecDate { get; set; }


		[Column(TypeName = "varchar(100)"), StringLength(100), Display(Name = "Teslimat Adresi"), Required(ErrorMessage = "Adres Giriniz")]
		public string Address { get; set; }

		[Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Teslimat Ülkesi"), Required(ErrorMessage = "Ülke Giriniz")]
		public string Country { get; set; }

		[Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Teslimat Şehri"), Required(ErrorMessage = "Şehir Giriniz")]
		public string City { get; set; }

		[Column(TypeName = "varchar(5)"), StringLength(5, MinimumLength = 5), Display(Name = "Posta Kodu"), Required(ErrorMessage = "Posta Kodu Giriniz")]
		public string ZipCode { get; set; }

		[Column(TypeName = "varchar(11)"), StringLength(11, MinimumLength = 11), Display(Name = "Telefon Numarası"), Required(ErrorMessage = "Telefon Numarası Giriniz")]
		public string PhoneNumber { get; set; }

		[Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Adı Soyadı"), Required(ErrorMessage = "Ad Soyad Giriniz")]
		public string NameSurname { get; set; }

		public ICollection<OrderDetails> OrderDetails { get; set; }


		[NotMapped, Display(Name = "Kart Numarası")]
		[StringLength(16, MinimumLength = 16, ErrorMessage = "Eksik Tuşlama")]
		public string CardNumber { get; set; }

		[NotMapped]
		public string CardMonth { get; set; }

		[NotMapped]
		[Display(Name = "Kart Yılı")]
		[Required(ErrorMessage = "Kart yılı alanı gereklidir.")]
		public string CardYear { get; set; }

		[NotMapped, Display(Name = "CV2 Kodu")]
		[StringLength(3, MinimumLength = 3, ErrorMessage = "Eksik Tuşlama")]
		public string CardCv2 { get; set; }
	}
}
