using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wooland.DAL.Entities
{
    public class Admin
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(20)"), StringLength(20), Display(Name = "Kullanıcı Adı"), Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz")]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(32)"), StringLength(32), Display(Name = "Şifre"), Required(ErrorMessage = "Şifre Boş Bırakılamaz")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Admin Adı"), Required(ErrorMessage = "Ad Soyad Boş Bırakılamaz")]
        public string NameSurname { get; set; }

        [Display(Name = "Son Giriş Tarihi")]
        public DateTime LastLoginDate { get; set; }

        [Column(TypeName = "varchar(20)"), StringLength(20), Display(Name = "Admin Son Giriş IP")]

        public string LastLoginIP { get; set; }
    }
}
