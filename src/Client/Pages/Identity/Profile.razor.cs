﻿using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Repres.Application.Features.Languages.Queries.GetAll;
using Repres.Application.Features.TimeZones.Queries.GetAll;
using Repres.Application.Requests.Identity;
using Repres.Client.Extensions;
using Repres.Client.Infrastructure.Managers.TimeZone;
using Repres.Shared.Constants.Localization;
using Repres.Shared.Constants.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Repres.Client.Pages.Identity
{
    public partial class Profile
    {
        [Inject] private ILanguageManager LanguageManager { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private char _firstLetterOfName;
        private readonly UpdateProfileRequest _profileModel = new();
        private KeyValuePair<string, int>[] _utcValues = UtcConstants.Values;
        private List<GetAllLanguagesResponse> _languages = new();

        public string UserId { get; set; }

        private async Task UpdateProfileAsync()
        {
            var response = await _accountManager.UpdateProfileAsync(_profileModel);
            if (response.Succeeded)
            {
                await _authenticationManager.Logout();
                _snackBar.Add(_localizer["Your Profile has been updated. Please Login to Continue."], Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var languagesResponse = await LanguageManager.GetAllAsync();
            if (languagesResponse.Succeeded)
                _languages = languagesResponse.Data.ToList();

            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            _profileModel.Email = user.GetEmail();
            _profileModel.FirstName = user.GetFirstName();
            _profileModel.LastName = user.GetLastName();
            _profileModel.PhoneNumber = user.GetPhoneNumber();
            UserId = user.GetUserId();
            var data = await _accountManager.GetProfilePictureAsync(UserId);
            if (data.Succeeded)
            {
                ImageDataUrl = data.Data;
            }
            if (_profileModel.FirstName.Length > 0)
            {
                _firstLetterOfName = _profileModel.FirstName[0];
            }

            var timeZone = await _accountManager.GetProfileUtcMinutesAsync(UserId);
            if (timeZone.Succeeded)
            {
                _profileModel.UtcMinutes = timeZone.Data;
            }
            var language = await _accountManager.GetProfileLanguageAsync(UserId);
            if (language.Succeeded)
            {
                _profileModel.Language = language.Data;
            }
        }

        private IBrowserFile _file;

        [Parameter]
        public string ImageDataUrl { get; set; }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var fileName = $"{UserId}-{Guid.NewGuid()}{extension}";
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                var request = new UpdateProfilePictureRequest { Data = buffer, FileName = fileName, Extension = extension, UploadType = Application.Enums.UploadType.ProfilePicture };
                var result = await _accountManager.UpdateProfilePictureAsync(request, UserId);
                if (result.Succeeded)
                {
                    await _localStorage.SetItemAsync(StorageConstants.Local.UserImageURL, result.Data);
                    _snackBar.Add(_localizer["Profile picture added."], Severity.Success);
                    _navigationManager.NavigateTo("/account", true);
                }
                else
                {
                    foreach (var error in result.Messages)
                    {
                        _snackBar.Add(error, Severity.Error);
                    }
                }
            }
        }

        private async Task DeleteAsync()
        {
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), $"{string.Format(_localizer["Do you want to delete the profile picture of {0}"], _profileModel.Email)}?"}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var request = new UpdateProfilePictureRequest { Data = null, FileName = string.Empty, UploadType = Application.Enums.UploadType.ProfilePicture };
                var data = await _accountManager.UpdateProfilePictureAsync(request, UserId);
                if (data.Succeeded)
                {
                    await _localStorage.RemoveItemAsync(StorageConstants.Local.UserImageURL);
                    ImageDataUrl = string.Empty;
                    _snackBar.Add(_localizer["Profile picture deleted."], Severity.Success);
                    _navigationManager.NavigateTo("/account", true);
                }
                else
                {
                    foreach (var error in data.Messages)
                    {
                        _snackBar.Add(error, Severity.Error);
                    }
                }
            }
        }
    }
}