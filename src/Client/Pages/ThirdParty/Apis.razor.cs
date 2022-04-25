using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Repres.Application.Features.Apis.Commands.Edit;
using Repres.Application.Features.Apis.Queries.GetAll;
using Repres.Client.Infrastructure.Managers.Services.ThirdParty;
using Repres.Shared.Constants.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Repres.Client.Pages.ThirdParty
{
    public partial class Apis
    {
        [Inject] private IApiManager ApiManager { get; set; }

        private List<GetAllApisResponse> _apiList = new();
        private GetAllApisResponse _api = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canEditApis;
        private bool _canSearchApis;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canEditApis = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Apis.Edit)).Succeeded;
            _canSearchApis = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Apis.Search)).Succeeded;

            await GetApisAsync();
            _loaded = true;
        }

        private async Task GetApisAsync()
        {
            var response = await ApiManager.GetAllAsync();
            if (response.Succeeded)
            {
                _apiList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _api = _apiList.FirstOrDefault(c => c.Id == id);
                if (_api != null)
                {
                    parameters.Add(nameof(EditApiModal.EditApiModel), new EditApiCommand
                    {
                        Id = _api.Id,
                        Name = _api.Name,
                        DisplayName = _api.DisplayName,
                        ClientId = _api.ClientId,
                        Scopes = _api.Scopes
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<EditApiModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _api = new GetAllApisResponse();
            await GetApisAsync();
        }

        private bool Search(GetAllApisResponse api)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (api.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (api.DisplayName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}
