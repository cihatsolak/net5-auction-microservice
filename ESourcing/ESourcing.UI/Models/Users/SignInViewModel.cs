using System.ComponentModel;

namespace ESourcing.UI.Models.Users
{
    public class SignInViewModel
    {
        [DisplayName("E-Posta Adresi")]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        public string Password { get; set; }
    }
}
