using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Valobtify.AspNetCore.WebApi;

public class SingleValueObjectSchemaFilter : ISchemaFilter
{
    static readonly SingleValueObjectTemplate _template = SingleValueObjectTemplate.Create("")!;

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsSingleValueObject())
        {
            var valueType = context.Type.GetProperty(nameof(_template.Value))!.PropertyType;

            var jsonSchemaType = context.SchemaGenerator.GenerateSchema(valueType, context.SchemaRepository);

            schema.Type = jsonSchemaType.Type;
            schema.Format = jsonSchemaType.Format;

            schema.Properties.Clear();
        }
    }
}