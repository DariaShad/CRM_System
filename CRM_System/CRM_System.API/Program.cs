using CRM.DataLayer;
using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Repositories;
//using CRM.DataLayer.Interfaces;
using CRM_System.API;
using CRM_System.API.Infrastucture;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Services;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(@"Server=DESKTOP-PMA057A;Database=CRM_System.DB;"));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization: Bearer JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            Array.Empty<string>()
        },
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = AuthOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

//builder.Services.AddScoped<ILeadRepository, LeadRepository>();
//builder.Services.AddScoped<ILeadService, LeadService>();
//
builder.Services.AddAuthorization();
builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<ILeadsRepository, LeadsRepository>();
builder.Services.AddScoped<ILeadsService, LeadsService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminsService, AdminsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
builder.Services.AddScoped<IAccountsService, AccountsService>();

builder.Services.AddAutoMapper(typeof(MapperConfigStorage));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();