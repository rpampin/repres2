using Microsoft.AspNetCore.Components;
using MudBlazor;
using Repres.Application.Features.Apis.Queries.GetAccess;
using Repres.Client.Extensions;
using Repres.Client.Infrastructure.Managers.Services.ThirdParty;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repres.Client.Pages.ThirdParty
{
    public partial class ApiAccess
    {
        [Inject] private IApiManager ApiManager { get; set; }

        [Parameter] public string Id { get; set; }
        [Parameter] public string Description { get; set; }

        private List<GetApiAccessResponse> _apiList = new();

        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            await GetApisAsync();
            _loaded = true;
        }

        private async Task GetApisAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            var currentUserId = user.GetUserId();

            var result = await ApiManager.GetUserApiAccess(new() { UserId = currentUserId });
            if (result.Succeeded)
            {
                _apiList = result.Data.ToList();
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, Severity.Error);
                }
                _navigationManager.NavigateTo("/identity/roles");
            }
        }

        private Color GetBadgeColor(bool isAuthenticated)
            => isAuthenticated ? Color.Success : Color.Error;

        private string GetLinkText(GetApiAccessResponse api)
            => api.IsAuthenticated ? $"Re-authenticate your {api.DisplayName ?? api.Name} account" : $"Authenticate your {api.DisplayName ?? api.Name} account";

        private string GetIcon(bool isAuthenticated)
            => isAuthenticated ? Icons.Material.Filled.LockOpen : Icons.Material.Filled.Lock;
    }
}
