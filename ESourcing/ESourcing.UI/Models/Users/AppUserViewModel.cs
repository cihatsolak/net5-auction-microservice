using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.Models.Users
{
    public class AppUserViewModel
    {
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [DisplayName("Ad")]
        public string FirstName { get; set; }

        [DisplayName("Soyad")]
        public string LastName { get; set; }

        [DisplayName("Telefon No")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("E-Posta Adresi")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Alıcı mı?")]
        public bool IsBuyer { get; set; }

        [DisplayName("Satıcı mı?")]
        public bool IsSeller { get; set; }

        public int UserSelectedTypeId { get; set; }
    }

    public enum UserType
    {
        [Display(Name = "Bayi")] Seller = 1,
        [Display(Name = "Satıcı")] Buyer = 2
    }
}
