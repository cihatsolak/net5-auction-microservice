using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.Models.Users
{
    public class AppUserViewModel
    {
        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Firstname")]
        public string FirstName { get; set; }

        [DisplayName("Lastname")]
        public string LastName { get; set; }

        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Is Buyer")]
        public bool IsBuyer { get; set; }

        [DisplayName("Is Seller")]
        public bool IsSeller { get; set; }

        public int UserSelectedTypeId { get; set; }
    }

    public enum UserType
    {
        [Display(Name = "Buyer")] Seller = 1,
        [Display(Name = "Seller")] Buyer = 2
    }
}
