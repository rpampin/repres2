using Repres.Shared.Settings;
using System.Threading.Tasks;
using Repres.Shared.Wrapper;

namespace Repres.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}