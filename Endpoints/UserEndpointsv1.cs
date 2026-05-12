using Asp.Versioning;
using learndotnet.Services;

namespace learndotnet.Endpoints;

public static class UserEndpointsV1
{
    public static void MapUserEndpointsV1(this WebApplication app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("/api/v{version:apiVersion}/users")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithTags("Users V1");

        group.MapGet("/", (UserService userService) =>
        {
            return userService.GetUsers();
        });

        group.MapGet("/{id}", (int id, UserService userService) =>
        {
            var user = userService.GetUserById(id);

            return user is not null
                ? Results.Ok(user)
                : Results.NotFound();
        });
    }
}