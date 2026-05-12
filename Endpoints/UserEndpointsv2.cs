using Asp.Versioning;
using learndotnet.Services;

namespace learndotnet.Endpoints;

public static class UserEndpointsV2
{
    public static void MapUserEndpointsV2(this WebApplication app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(2, 0))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("/api/v{version:apiVersion}/users")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(2.0)
            .WithTags("Users V2");

        group.MapGet("/", () =>
        {
            return Results.Ok(new
            {
                Message = "Users API Version 2"
            });
        }).RequireAuthorization();
    }
}