using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Repres.Application.Features.Apis.Commands.Edit;
using Repres.Client.Infrastructure.Managers.Services.ThirdParty;
using System.Threading.Tasks;

namespace Repres.Client.Pages.ThirdParty
{
    public partial class EditApiModal
    {
        [Inject] private IApiManager ApiManager { get; set; }

        [Parameter] public EditApiCommand EditApiModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await ApiManager.SaveAsync(EditApiModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
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
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await Task.CompletedTask;
        }
    }
}
