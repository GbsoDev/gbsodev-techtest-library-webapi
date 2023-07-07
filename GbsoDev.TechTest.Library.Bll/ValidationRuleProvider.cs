using FluentValidation;
using GbsoDev.TechTest.Library.Bll.ValidationRules;
using GbsoDev.TechTest.Library.El;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.Bll
{
    public static class ValidationRuleProvider
    {
        public static IServiceCollection AddBllValidationRulesLayer(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<Autor>, AutorVr>();
            services.AddSingleton<IValidator<Libro>, LibroVr>();
            services.AddSingleton<IValidator<Editorial>, EditorialVr>();
            return services;
        }
    }
}
