using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;


namespace BootcampHomework2.Customizer
{
    public class AddParameterToHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "app-version",
                In = ParameterLocation.Header,
                Required = false,
                Description = "app version of request",
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    ReadOnly = true,
                    Default = new OpenApiString("2.0.0.0")
                }
            });
        }
    }
}
