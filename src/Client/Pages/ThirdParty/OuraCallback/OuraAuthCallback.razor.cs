using Microsoft.AspNetCore.Components;
using MudBlazor;
using Repres.Client.Extensions;
using Repres.Client.Infrastructure.Managers.Services.ThirdParty;
using System.Threading.Tasks;

namespace Repres.Client.Pages.ThirdParty.OuraCallback
{
    public partial class OuraAuthCallback
    {
        [Inject] private IApiManager ApiManager { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            _navigationManager.TryGetQueryString("code", out string _code);
            _navigationManager.TryGetQueryString("scope", out string _scope);
            _navigationManager.TryGetQueryString("state", out string _state);

            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            var currentUserId = user.GetUserId();

            var result = await ApiManager.PersistApiToken(new()
            {
                Api = "Oura", // SAME AS SERVICE IMPLEMENTING IApiService
                UserId = currentUserId,
                Code = _code,
                Scope = _scope,
                State = _state
            });
            if (!result.Succeeded)
            {
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, Severity.Error);
                }
            }
            _navigationManager.NavigateTo("/third-party/api-access");
        }
    }
}
