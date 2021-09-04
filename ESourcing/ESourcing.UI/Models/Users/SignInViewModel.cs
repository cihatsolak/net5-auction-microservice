using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.Models.Users
{
    public class SignInViewModel
    {
        [DisplayName("E-Posta Adresi")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
