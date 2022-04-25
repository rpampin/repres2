using System.Linq;
using Repres.Shared.Constants.Localization;
using Repres.Shared.Settings;

namespace Repres.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}