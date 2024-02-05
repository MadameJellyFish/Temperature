using Api.Temeprature.Common;
using Api.Temeprature.IoC.Application;
using Api.Temeprature.IoC.Tests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.Configure<JwtSetting>(configuration.GetSection("JwtSettings"));

if (builder.Environment.IsEnvironment("Test"))
{
    // Configure Database connexion
    builder.Services.ConfigureDBContextTest();

    //Dependency Injection
    builder.Services.ConfigureInjectionDependencyRepositoryTest();

    builder.Services.ConfigureInjectionDependencyServiceTest();
}
else
{
    // Configure Database connexion
    builder.Services.ConfigureDBContext(configuration);

    builder.Services.ConfigureIdentity();

    //Dependency Injection
    builder.Services.ConfigureInjectionDependencyRepository();

    builder.Services.ConfigureInjectionDependencyService();
}

// Système de Validation d'un token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSetting>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Inculde xml comment on the swagger view
var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

builder.Services.AddSwaggerGen(options =>
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename))
);

// Configure HTTPS redirection with the HTTPS port
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443; // Specify your HTTPS port here
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
