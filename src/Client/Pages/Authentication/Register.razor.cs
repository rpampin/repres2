using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Repres.Application.Features.Languages.Queries.GetAll;
using Repres.Application.Features.TimeZones.Queries.GetAll;
using Repres.Application.Requests.Identity;
using Repres.Client.Infrastructure.Managers.TimeZone;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repres.Client.Pages.Authentication
{
    public partial class Register
    {
        [Inject] private ILanguageManager LanguageManager { get; set; }
        [Inject] private ITimeZoneManager TimeZoneManager { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private RegisterRequest _registerUserModel = new();

        private List<GetAllTimeZonesResponse> _timeZones = new();
        private List<GetAllLanguagesResponse> _languages = new();

        protected override async Task OnInitializedAsync()
        {
            var timeZoneResponse = await TimeZoneManager.GetAllAsync();
            if (timeZoneResponse.Succeeded)
                _timeZones = timeZoneResponse.Data.ToList();
            var languagesResponse = await LanguageManager.GetAllAsync();
            if (languagesResponse.Succeeded)
                _languages = languagesResponse.Data.ToList();
        }

        private async Task SubmitAsync()
        {
            var response = await _userManager.RegisterUserAsync(_registerUserModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                _navigationManager.NavigateTo("/login");
                _registerUserModel = new RegisterRequest();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        private void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
    }
}