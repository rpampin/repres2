using Microsoft.AspNetCore.Components;
using MudBlazor;
using Repres.Client.Extensions;
using Repres.Client.Infrastructure.Managers.Identity.Users;

namespace Repres.Client.Pages.Identity
{
    public partial class ConfirmEmail
    {
        [Inject] private IUserManager UserManager { get; set; }

        protected override async void OnInitialized()
        {
            _navigationManager.TryGetQueryString("userId", out string _userId);
            _navigationManager.TryGetQueryString("code", out string _code);

            var result = await UserManager.ConfirmEmailAsync(_userId, _code);

            foreach (var msg in result.Messages)
            {
                _snackBar.Add(msg, result.Succeeded ? Severity.Success : Severity.Error);
            }

            _navigationManager.NavigateTo("/");
        }
    }
}
