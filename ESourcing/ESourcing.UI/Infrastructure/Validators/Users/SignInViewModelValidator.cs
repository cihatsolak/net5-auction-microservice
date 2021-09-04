using ESourcing.UI.Models.Users;
using FluentValidation;

namespace ESourcing.UI.Infrastructure.Validators.Users
{
    public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
    {
        public SignInViewModelValidator()
        {
            RuleFor(p => p.Email).EmailAddress();

            RuleFor(p => p.Password)
                .MinimumLength(5)
                .NotNull()
                .NotEmpty();
        }
    }
}
