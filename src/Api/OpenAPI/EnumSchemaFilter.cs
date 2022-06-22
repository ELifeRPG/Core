using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ELifeRPG.Core.Api.OpenAPI;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum)
        {
            return;
        }

        var enumNames = new OpenApiArray();
        enumNames.AddRange(Enum.GetNames(context.Type).Select(n => new OpenApiString(n)));
        schema.Extensions.Add("x-enumNames", enumNames);
    }
}
