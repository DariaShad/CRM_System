using CRM_System.API.Models.Requests;
using CRM_System.API.Validators;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.RabbitMQ.Consumer;
using CRM_System.BusinessLayer.Services;
using CRM_System.DataLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using IncredibleBackend.Messaging;
using IncredibleBackend.Messaging.Extentions;
using IncredibleBackend.Messaging.Interfaces;
using IncredibleBackendContracts.Constants;
using IncredibleBackendContracts.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CRM_System.API;

public static class ServiceCollectionExtensions
{
    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
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
    }

    public static void AddAuthentications(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = TokenOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = TokenOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = TokenOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<IAccountsService, AccountsService>();
        services.AddScoped<ILeadsRepository, LeadsRepository>();
        services.AddScoped<ILeadsService, LeadsService>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IAdminsService, AdminsService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<IAccountsService, AccountsService>();
        services.AddScoped<IHttpService, TransactionStoreClient>();
        services.AddScoped<ITransactionsService, TransactionsService>();
        services.AddScoped<IMessageProducer, MessageProducer>();

    }
    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);

        services.AddScoped<IValidator<AddAccountRequest>, AddAccountValidator>();
        services.AddScoped<IValidator<UpdateAccountRequest>, UpdateAccountValidator>();
        services.AddScoped<IValidator<LeadRegistrationRequest>, LeadRegistrationValidator>();
        services.AddScoped<IValidator<LeadUpdateRequest>, LeadUpdateValidator>();
    }

    public static void AddConsumersAndProducers(this IServiceCollection services)
    {
        services.RegisterConsumersAndProducers(
            (config) =>
            {
                config.AddConsumer<RabbitMQConsumer>();
            },

            (cfg, ctx) =>
            {
                cfg.RegisterConsumer<RabbitMQConsumer>(ctx, RabbitEndpoint.LeadsRoleUpdateCrm);
            },
            (config) =>
            {
                config.RegisterProducer<LeadCreatedEvent>(RabbitEndpoint.LeadCreate);
                config.RegisterProducer<LeadDeletedEvent>(RabbitEndpoint.LeadDelete);
                config.RegisterProducer<LeadUpdatedEvent>(RabbitEndpoint.LeadUpdate);
                config.RegisterProducer<AccountCreatedEvent>(RabbitEndpoint.AccountCreate);
                config.RegisterProducer<AccountDeletedEvent>(RabbitEndpoint.AccountDelete);
                config.RegisterProducer<AccountUpdatedEvent>(RabbitEndpoint.AccountUpdate);
                config.RegisterProducer<LeadsRoleUpdatedEvent>(RabbitEndpoint.LeadsRoleUpdateReporting);
            }
            );
    }


}