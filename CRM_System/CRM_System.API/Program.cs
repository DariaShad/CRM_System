using CRM_System.API;
using CRM_System.API.Infrastucture;
using CRM_System.API.Models;
using CRM_System.API.Models.Responses;
using IncredibleBackendContracts.Constants;
using IncredibleBackendContracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
builder.Services.AddMassTransit(
    config => config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.ReceiveEndpoint(RabbitEndpoint.LeadsRoleUpdateCrm, c => c.Bind<LeadsRoleUpdatedEvent>());

        cfg.ReceiveEndpoint(RabbitEndpoint.LeadDelete, c => c.Bind<LeadDeletedEvent>());
        cfg.ReceiveEndpoint(RabbitEndpoint.LeadCreate, c => c.Bind<LeadCreatedEvent>());
        cfg.ReceiveEndpoint(RabbitEndpoint.AccountCreate, c => c.Bind<AccountCreatedEvent>());
        cfg.ReceiveEndpoint(RabbitEndpoint.AccountDelete, c => c.Bind<AccountDeletedEvent>());
        cfg.ReceiveEndpoint(RabbitEndpoint.AccountUpdate, c => c.Bind<AccountUpdatedEvent>());
        cfg.ReceiveEndpoint(RabbitEndpoint.LeadUpdate, c => c.Bind<LeadUpdatedEvent>());
    }));
    
builder.Services.AddConsumers();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();