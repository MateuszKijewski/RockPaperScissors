using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RockPaperScissors.Application;
using RockPaperScissors.Application.Auth.Providers;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Application.Sercurity.Services;
using RockPaperScissors.Domain;
using RockPaperScissors.Persistence;
using RockPaperScissors.WebApi.Helpers;
using RockPaperScissors.WebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

JwtSecurityTokenHandler.DefaultInboundClaimFilter.Clear();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

services.AddControllers();
services.AddSignalR();
services.AddCors();
services.AddMvc();

services.AddOptions();
services.Configure<EncryptionOptions>(builder.Configuration.GetSection(nameof(EncryptionOptions)));
services.Configure<TokenValidationOptions>(builder.Configuration.GetSection(nameof(TokenValidationOptions)));
services.AddScoped<ICurrentUserService, CurrentUserService>();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RockPaperScissors", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "Bearer",
              Name = "Bearer",
              In = ParameterLocation.Header,
            },
            new List<string>()
          }
        });
});

services.AddHttpContextAccessor();

services.AddDomain(builder.Configuration);
services.AddPersistence(builder.Configuration);
services.AddApplication(builder.Configuration);

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection(nameof(TokenValidationOptions))[nameof(TokenValidationOptions.Issuer)],
            ValidAudience = builder.Configuration.GetSection(nameof(TokenValidationOptions))[nameof(TokenValidationOptions.Audience)],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection(nameof(TokenValidationOptions))[nameof(TokenValidationOptions.IssuerSigningKey)]))
        };
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My service");
    c.RoutePrefix = string.Empty;
});
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseAuthorization();
app.ConfigureSignalR();
app.MapControllers();

app.Run();