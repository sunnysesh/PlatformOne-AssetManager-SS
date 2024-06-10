using FluentValidation;
using PlatformOneAsset.Core.Models.Request;

namespace PlatformOneAsset.App.Validators;

public class UpdatePriceRequestValidator : AbstractValidator<UpdatePriceRequest>
{
    public UpdatePriceRequestValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
        RuleFor(i => i.Symbol).NotEmpty();
        RuleFor(i => i.Date).NotEmpty();
    }
}