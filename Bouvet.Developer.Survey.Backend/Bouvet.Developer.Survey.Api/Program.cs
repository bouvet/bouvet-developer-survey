using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Azure.Identity;
using Bouvet.Developer.Survey.Api.Extensions;
using Bouvet.Developer.Survey.Api.Swagger;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var endpoint = builder.Configuration["AppConfig"];
if (!string.IsNullOrEmpty(endpoint))
{
    builder.Configuration.AddAzureAppConfiguration(builder =>
    {
        var creds = new DefaultAzureCredential(includeInteractiveCredentials: false);
        // connect to external configuration
        builder.Connect(new Uri(endpoint), creds)
            // enable keyvault linked items
            .ConfigureKeyVault(options => options.SetCredential(creds))
            // enable automatic settings refresh
            .ConfigureRefresh(options => options
                .Register("*", refreshAll: true)
                .SetCacheExpiration(TimeSpan.FromSeconds(5)));
    }, optional: true);
}

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    
    // options.AddPolicy("Read", policy =>
    // {
    //     policy.RequireAuthenticatedUser();
    //     policy.RequireRole("YourRoleName"); // Replace with the role name
    // });
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
    )
);

//Db connection
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<DeveloperSurveyContext>(opt =>
    opt.UseLazyLoadingProxies().UseSqlServer(connectionString));

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
//Service layer
builder.Services.AddServices();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
}).AddApiExplorer(options =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen()
    .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenConfigurator>()
    .AddTransient<IConfigureOptions<SwaggerUIOptions>, SwaggerUIConfigurator>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
// var services = scope.ServiceProvider;
// try
// {
//     Console.WriteLine("Migrating database...");
//     var context = services.GetRequiredService<DeveloperSurveyContext>();
//     context.Database.Migrate();
//     Console.WriteLine("Database migrated.");
// }
// catch (Exception ex)
// {
//     var logger = services.GetRequiredService<ILogger<Program>>();
//     logger.LogError(ex, "An error occurred creating the DB.");
// }

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {

app.UseCors();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();
app.MapHealthChecks("/health");

app.Run();