using FluentValidation;
using PlatformOneAsset.Core.Models.Request;

namespace PlatformOneAsset.App.Validators;

public class UpdateAssetRequestValidator : AbstractValidator<UpdateAssetRequest>
{
    public UpdateAssetRequestValidator()
    {
        RuleFor(i => i.Name).NotEmpty();
        RuleFor(i => i.Isin).NotEmpty();
    }
}