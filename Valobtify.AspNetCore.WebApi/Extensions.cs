using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Valobtify.AspNetCore.WebApi;

public static class Extensions
{
    private static readonly SingleValueObjectTemplate Template = SingleValueObjectTemplate.Create("").Content!;

    internal static bool IsSingleValueObject(this Type type)
    {
        return type.BaseType is { IsGenericType: true } baseType && baseType
            .GetGenericTypeDefinition()
            .IsAssignableTo(typeof(SingleValueObject<,>));
    }

    public static IServiceCollection AddValobtifyConverters(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        if (assemblies.Length == 0) assemblies = [Assembly.GetExecutingAssembly()];

        var singleValueObjectConverters = assemblies
            .SelectMany(assembly => assembly
                .GetTypes()
                .Where(t => t.IsSingleValueObject())
                .Select(t => t.CreateJsonConverter()));

        services.ConfigureHttpJsonOptions(c =>
        {
            foreach (var converter in singleValueObjectConverters) c.SerializerOptions.Converters.Add(converter);
        });

        return services;
    }

    public static SwaggerGenOptions AddValobtifySchemaFilters(this SwaggerGenOptions options)
    {
        options.SchemaFilter<SingleValueObjectSchemaFilter>();

        return options;
    }

    private static JsonConverter CreateJsonConverter(this Type type)
    {
        var converters = new List<JsonConverter>();

        if (!type.IsSingleValueObject()) throw new InvalidOperationException("type is not SingleValueObject");

        var valueObjectValueType = type.GetProperty(nameof(Template.Value))!.PropertyType;

        var converterType = typeof(SingleValueObjectJsonConverter<,>).MakeGenericType(type, valueObjectValueType);

        var instance = Activator.CreateInstance(converterType);

        return (instance as JsonConverter)!;
    }
}