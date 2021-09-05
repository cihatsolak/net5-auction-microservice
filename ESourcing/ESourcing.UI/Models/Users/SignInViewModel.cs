using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.Models.Users
{
    public class SignInViewModel
    {
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
