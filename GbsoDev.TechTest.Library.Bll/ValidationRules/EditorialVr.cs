using FluentValidation;
using GbsoDev.TechTest.Library.Bll.Resources;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll.ValidationRules
{
	internal class EditorialVr : AbstractValidator<Editorial>
	{
		public EditorialVr()
		{
			RuleSet(VrRuleSets.UPDATE, () =>
			{
				RuleFor(n => n.Id)
					.NotEmpty()
					.Must(n => n > 0);
			});

			RuleFor(n => n.Nombre)
				.NotEmpty().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.Nombre)));
			RuleFor(n => n.Sede)
				.NotNull().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyNull, nameof(n.Sede)));
		}
	}
}
