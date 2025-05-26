using Bouvet.Developer.Survey.Api.Extensions;
using Bouvet.Developer.Survey.Api.Swagger;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Definition;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results.Bouvet;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structure;
using Bouvet.Developer.Survey.Service.Services;
using Bouvet.Developer.Survey.Service.Services.Survey.Definition;
using Bouvet.Developer.Survey.Service.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.User;
using Bouvet.Developer.Survey.Service.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Response;
using Bouvet.Developer.Survey.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
);

var endpoint = builder.Configuration["AppConfig"];

// TODO: Add Audience and Authority
// Authentication and Authorization with Azure AD. Turned off atm.
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
        builder
            .AllowAnyOrigin()
            // builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
    )
);

//Db connection
var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<DeveloperSurveyContext>(opt =>
    opt.UseLazyLoadingProxies().UseSqlServer(connectionString)
);
builder.Services.AddDbContext<BouvetSurveyContext>(opt =>
    opt.UseSqlServer(connectionString));

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddScoped<ISurveyStructureService, SurveyStructureService>();
builder.Services.AddScoped<ISurveyResponseService, SurveyResponseService>();
builder.Services.AddScoped<ISurveyResultsService, SurveyResultsService>();
builder.Services.AddScoped<IBouvetSurveyDefinitionService, BouvetSurveyDefinitionService>();
builder.Services.AddScoped<IBouvetUserService, BouvetUserService>();


builder
    .Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
    })
    .AddApiExplorer(options =>
    {
        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
        // note: the specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
        // can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder
    .Services.AddSwaggerGen()
    .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenConfigurator>()
    .AddTransient<IConfigureOptions<SwaggerUIOptions>, SwaggerUIConfigurator>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    Console.WriteLine("Migrating database...");
    var context = services.GetRequiredService<DeveloperSurveyContext>();
    context.Database.Migrate();

    var bouvetContext = services.GetRequiredService<BouvetSurveyContext>();
    bouvetContext.Database.Migrate();

    Console.WriteLine("Database migrated.");
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred creating the DB.");
}

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

// Require auth on all controllers
app.MapControllers().RequireAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
