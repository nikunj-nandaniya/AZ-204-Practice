using Microsoft.EntityFrameworkCore;
using WebAPIwithJWT.Repositories;
using WebAPIwithJWT.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using LoggerService;
using WebAPIWithJWT.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
//builder.Services.AddDbContext<AppDbContext>(options =>
//        options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<ILoggerManager, LoggerManager>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:4200", "http://localhost:44349")
        .AllowAnyMethod()
        .AllowAnyHeader());
    //.AllowCredentials());
});

//Custom JWT Code
/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });*/

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
/*
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});*/


builder.Services.AddSwaggerGen(options => {

    options.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo { Title="Swagger Azure AD Demo", Version="1.0"});
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description="Oauth2.0 which uses AuthorizationCode Flow",
        Name = "OAuth2.0",
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri(builder.Configuration["SwaggerAzureAd:AuthorizationUrl"]),
                TokenUrl = new Uri(builder.Configuration["SwaggerAzureAd:TokenUrl"]),
                Scopes = new Dictionary<string, string>
                {
                    { builder.Configuration["SwaggerAzureAd:Scope"],"Access API as User"}
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
    {
        new OpenApiSecurityScheme 
        {
            Reference = new OpenApiReference 
            {
                Type = ReferenceType.SecurityScheme,
                Id = "oauth2"
            }            
        },
        new []{ builder.Configuration["SwaggerAzureAd:Scope"] }
    }
});   

    //options.OperationFilter<SecurityRequirementsOperationFilter>();
});




builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( options => {
        options.OAuthClientId(builder.Configuration["SwaggerAzureAd:ClientId"]);
        options.OAuthUsePkce();
        options.OAuthScopeSeparator(" ");    
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

app.Run();
