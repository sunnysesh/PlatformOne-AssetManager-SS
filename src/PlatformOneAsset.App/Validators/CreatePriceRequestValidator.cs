using FluentValidation;
using PlatformOneAsset.Core.Models.Request;

namespace PlatformOneAsset.App.Validators;

public class CreatePriceRequestValidator : AbstractValidator<CreatePriceRequest>
{
    public CreatePriceRequestValidator()
    {
        RuleFor(i => i.Symbol).NotEmpty();
        RuleFor(i => i.Date).NotEmpty();
        RuleFor(i => i.Value).NotEmpty();
        RuleFor(i => i.Source).NotEmpty();
    }
}