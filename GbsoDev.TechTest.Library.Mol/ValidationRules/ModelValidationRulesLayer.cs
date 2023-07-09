using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.Mol.ValidationRules
{
	public static class ModelValidationRulesLayer
	{
		public static IServiceCollection AddModelValidationRulesLayer(this IServiceCollection services)
		{
			//services.AddSingleton<IValidator<AutorModel>, AutorModelVr>();
			//services.AddSingleton<IValidator<LibroModel>, LibroModelVr>();
			//services.AddSingleton<IValidator<EditorialModel>, EditorialModelVr>();
			return services;
		}
	}
}
