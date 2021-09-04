using ESourcing.UI.Models.Users;
using FluentValidation;

namespace ESourcing.UI.Infrastructure.Validators.Users
{
    public class AppUserViewModelValidator : AbstractValidator<AppUserViewModel>
    {
        public AppUserViewModelValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().NotNull();
            RuleFor(p => p.FirstName).NotEmpty().NotNull();
            RuleFor(p => p.LastName).NotEmpty().NotNull();

            RuleFor(p => p.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .Must(p => p.StartsWith("5")).When(p => !string.IsNullOrWhiteSpace(p.PhoneNumber));

            RuleFor(p => p.Email).EmailAddress();

            RuleFor(p => p.Password)
                .MinimumLength(5)
                .NotNull()
                .NotEmpty();
        }
    }
}
