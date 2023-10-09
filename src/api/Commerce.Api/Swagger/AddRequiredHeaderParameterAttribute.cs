using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Commerce.Api.Swagger;

public class AddRequiredHeaderParameterAttribute : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Connection-String",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string"
            },
            Description = "Connection String"
        });
    }
}
