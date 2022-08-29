using CRM.DataLayer;
using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Repositories;
using CRM_System.API;
using CRM_System.API.Extensions;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Infrastucture;
using CRM_System.BusinessLayer.Services;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);


// to env variable
builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(@"Server=80.78.240.16;Database=CRM.Db;User Id=Student;Password=qwe!23"));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);


builder.Services.AddAuthorization();

builder.Services.AddServices();

builder.Services.AddFluentValidation();

builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();