using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

namespace ValidationPOC.Configuration;

public static class HttpJsonOptionsConfiguration
{
    private static readonly JsonSerializerOptions Default = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    public static IServiceCollection ConfigureHttpJsonOptions(this IServiceCollection services)
    {
        return services.ConfigureHttpJsonOptions(ConfigureJsonOptions);
    }

    private static void ConfigureJsonOptions(this JsonOptions options)
    {
        options.SerializerOptions.PropertyNamingPolicy = Default.PropertyNamingPolicy;

        options.SerializerOptions.Converters.Clear();

        foreach (var converter in Default.Converters)
        {
            options.SerializerOptions.Converters.Add(converter);
        }
    }
}