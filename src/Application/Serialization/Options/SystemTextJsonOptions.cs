using System.Text.Json;
using Repres.Application.Interfaces.Serialization.Options;

namespace Repres.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}