using Repres.Shared.Managers;
using MudBlazor;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();
    }
}