using CRM_System.API;
using CRM_System.API.Infrastucture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NLog;
using NLog.Web;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment = builder.Environment;

// to env variable
var dbConfig = new DbConfig();
builder.Configuration.Bind(dbConfig);

LogManager.Configuration.Variables[$"{ environment: LOG_DIRECTORY}"] = "Logs";

builder.Host.UseNLog();

//builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(@"Server=80.78.240.16;Database=CRM.Db;User Id=Student;Password=qwe!23"));


builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(dbConfig.CRM_CONNECTION_STRING));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();



builder.Services.AddAuthorization();

builder.Services.AddAuthentications();

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