
using Repres.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace Repres.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}