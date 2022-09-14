using Repres.Application.Requests.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repres.Client.Infrastructure.Managers.TimeZone;
using Repres.Application.Features.Languages.Queries.GetAll;
using Repres.Shared.Constants.Localization;
using System.Linq;
using Repres.Client.Infrastructure.Managers.Services.ThirdParty.Oura;
using Repres.Shared.Wrapper;

namespace Repres.Client.Pages.Identity
{
    public partial class UserProfile
    {
        [Inject] private IOuraManager _ouraManager { get; set; }

        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Description { get; set; }

        private bool _active;
        private char _firstLetterOfName;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private string _email;
        private int _utcMinutes;
        private string _language;
        private bool _hasSheet;

        private bool _loaded;

        private async Task ToggleUserStatus()
        {
            var request = new ToggleUserStatusRequest { ActivateUser = _active, UserId = Id };
            var result = await _userManager.ToggleUserStatusAsync(request);
            if (result.Succeeded)
            {
                _snackBar.Add(_localizer["Updated User Status."], Severity.Success);
                _navigationManager.NavigateTo("/identity/users");
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, Severity.Error);
                }
            }
        }

        private async Task ResetSheet()
        {
            var result = await _ouraManager.ResetUserData(Id);
            if (result.Succeeded)
            {
                _snackBar.Add(_localizer["User's OURA data has been purged."], Severity.Success);
                _navigationManager.NavigateTo("/identity/users");
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, Severity.Error);
                }
            }
        }

        [Parameter] public string ImageDataUrl { get; set; }
        private KeyValuePair<string, int>[] _utcValues = UtcConstants.Values;

        protected override async Task OnInitializedAsync()
        {
            var userId = Id;
            var result = await _userManager.GetAsync(userId);
            if (result.Succeeded)
            {
                var user = result.Data;
                if (user != null)
                {
                    _firstName = user.FirstName;
                    _lastName = user.LastName;
                    _email = user.Email;
                    _phoneNumber = user.PhoneNumber;
                    _active = user.IsActive;

                    var data = await _accountManager.GetProfilePictureAsync(userId);
                    if (data.Succeeded)
                    {
                        ImageDataUrl = data.Data;
                    }

                    var timeZone = await _accountManager.GetProfileUtcMinutesAsync(userId);
                    if (timeZone.Succeeded)
                    {
                        _utcMinutes = timeZone.Data;
                    }
                    var language = await _accountManager.GetProfileLanguageAsync(userId);
                    if (language.Succeeded)
                    {
                        _language = language.Data;
                    }

                    var hasSheet = await _accountManager.GetProfileHasSheet(userId);
                    if (hasSheet.Succeeded)
                    {
                        _hasSheet = hasSheet.Data;
                    }
                }
                Title = $"{_firstName} {_lastName}'s {_localizer["Profile"]}";
                Description = _email;
                if (_firstName.Length > 0)
                {
                    _firstLetterOfName = _firstName[0];
                }
            }

            _loaded = true;
        }
    }
}