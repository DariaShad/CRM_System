using CRM_System.API;
using CRM_System.API.Infrastucture;
using NLog;
using NLog.Web;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment = builder.Environment;

var dbConfig = new DbConfig();
builder.Configuration.Bind(dbConfig);

LogManager.Configuration.Variables[$"{ environment: LOG_DIRECTORY}"] = "Logs";

builder.Host.UseNLog();

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

builder.Services.AddConsumersAndProducers();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();