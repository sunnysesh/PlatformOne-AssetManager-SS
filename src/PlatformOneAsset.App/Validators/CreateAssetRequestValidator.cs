using FluentValidation;
using PlatformOneAsset.Core.Models.Request;

namespace PlatformOneAsset.App.Validators;

public class CreateAssetRequestValidator : AbstractValidator<CreateAssetRequest>
{
    public CreateAssetRequestValidator()
    {
        RuleFor(i => i.Name).NotEmpty();
        RuleFor(i => i.Symbol).NotEmpty();
        RuleFor(i => i.ISIN).NotEmpty();
    }
}