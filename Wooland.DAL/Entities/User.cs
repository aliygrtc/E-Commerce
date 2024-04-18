using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wooland.DAL.Entities
{
    public class User
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar(20)"), StringLength(20), Display(Name = "Kullanıcı Adı"), Required(ErrorMessage = "Kullanıcı Adı Giriniz")]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(32)"), StringLength(32), Display(Name = "Şifre"), Required(ErrorMessage = "Şifre Giriniz")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Adı Soyadı"), Required(ErrorMessage = "Ad Soyad Giriniz")]
        public string NameSurname { get; set; }

        [Display(Name = "Durum")]
        public bool Status { get; set; }

        [Display(Name = "Son Giriş Tarihi")]
        public DateTime? LastLoginDate { get; set; }
		[Column(TypeName = "nvarchar(100)"), StringLength(100), Display(Name = "E-Posta"), Required(ErrorMessage = "E-Posta Giriniz")]
		public string Email { get; set; }

		public ICollection<Product> Products { get; set; }
    }
}
