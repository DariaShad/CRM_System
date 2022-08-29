using CRM_System.API.Models.Requests;
using CRM_System.API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace CRM_System.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);

            services.AddScoped<IValidator<AddAccountRequest>, AddAccountValidator>();
            services.AddScoped<IValidator<UpdateAccountRequest>, UpdateAccountValidator>();
            services.AddScoped <IValidator<LeadRegistrationRequest>, LeadRegistrationValidator>();
            services.AddScoped <IValidator<LeadUpdateRequest>, LeadUpdateValidator>();
        }
    }
}