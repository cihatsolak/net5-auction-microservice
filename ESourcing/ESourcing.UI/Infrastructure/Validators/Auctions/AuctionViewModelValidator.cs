using ESourcing.UI.Models.Auctions;
using FluentValidation;
using System;

namespace ESourcing.UI.Infrastructure.Validators.Auctions
{
    public class AuctionViewModelValidator : AbstractValidator<AuctionViewModel>
    {
        public AuctionViewModelValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty();
            RuleFor(p => p.Description).NotNull().NotEmpty();
            RuleFor(p => p.ProductId).NotNull().NotEmpty();
            RuleFor(p => p.Quantity).GreaterThan(0);
            RuleFor(p => p.StartedAt).Must(IsValidDateTime).WithMessage("Enter a valid date");
            RuleFor(p => p.FinishedAt).Must(IsValidDateTime).WithMessage("Enter a valid date");
            RuleFor(p => p.CreatedAt).Must(IsValidDateTime).WithMessage("Enter a valid date");
            RuleFor(p => p.Status).GreaterThan(0);
            RuleFor(p => p.SellerId).NotNull().NotEmpty();
            RuleFor(p => p.Sellers).NotNull().NotEmpty();
        }

        private bool IsValidDateTime(DateTime dateTime)
        {
            return DateTime.Now >= dateTime;
        }
    }
}
