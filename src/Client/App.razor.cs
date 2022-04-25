using MudBlazor;
using Repres.Client.Extensions;
using Repres.Shared.Wrapper;
using System.Text.Json;

namespace Repres.Client
{
    public partial class App
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            _navigationManager.TryGetQueryString("message", out string _message);
            if (!string.IsNullOrEmpty(_message))
            {
                var result = JsonSerializer.Deserialize<IResult>(_message);
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, result.Succeeded ? Severity.Success : Severity.Error);
                }
            }
        }
    }
}
