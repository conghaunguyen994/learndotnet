using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using learndotnet.Data;
using learndotnet.Endpoints;
using learndotnet.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

builder.WebHost.UseUrls($"http://*:{port}");

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);

    options.AssumeDefaultVersionWhenUnspecified = true;

    options.ReportApiVersions = true;

    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";

    options.SubstituteApiVersionInUrl = true;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LearnDotnet API",
        Version = "v1"
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "LearnDotnet API",
        Version = "v2"
    });
});

// Register layers
builder.Services.AddDataLayer(builder.Configuration);
builder.Services.AddBusinessServices();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "LearnDotnet API v1");

    options.SwaggerEndpoint("/swagger/v2/swagger.json", "LearnDotnet API v2");
});

app.UseHttpsRedirection();

app.MapGet("/", () => "API is running");

// Register endpoints
app.MapUserEndpointsV1();
app.MapUserEndpointsV2();
app.MapProductEndpoints();
app.MapCategoryEndpoints();
app.MapOrderEndpoints();
app.MapOrderItemEndpoints();

app.Run();