using FluentValidation;
using GbsoDev.TechTest.Library.Bll.Resources;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll.ValidationRules
{
	internal class AutorVr : AbstractValidator<Autor>
	{
		public AutorVr()
		{
			RuleSet(VrRuleSets.UPDATE, () =>
			{
				RuleFor(n => n.Id)
					.NotEmpty()
					.Must(n => n > 0);
			});
			
			RuleFor(n => n.Nombre)
				.NotEmpty().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.Nombre)));
			RuleFor(n => n.Apellidos)
				.NotEmpty().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.Apellidos)));
		}
	}
}
