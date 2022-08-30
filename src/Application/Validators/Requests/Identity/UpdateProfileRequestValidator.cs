using Repres.Application.Requests.Identity;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Repres.Application.Validators.Requests.Identity
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator(IStringLocalizer<UpdateProfileRequestValidator> localizer)
        {
            RuleFor(request => request.FirstName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["First Name is required"]);
            RuleFor(request => request.LastName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Last Name is required"]);
            RuleFor(request => request.UtcMinutes)
                .Must(x => x.HasValue).WithMessage(x => localizer["Must select an UTC value"]);
            RuleFor(request => request.Language)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Language is required"]);
        }
    }
}