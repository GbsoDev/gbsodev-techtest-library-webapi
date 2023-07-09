using FluentValidation;
using GbsoDev.TechTest.Library.Bll.Resources;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll.ValidationRules
{
	internal class LibroVr : AbstractValidator<Libro>
	{
		public LibroVr()
		{
			RuleSet(VrRuleSets.ALL, () =>
			{
				RuleFor(n => n.Id)
				.NotEmpty()
				.Must(n => n.ToString().Length == 10);
				RuleFor(n => n.Titulo)
					.NotNull().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyNull, nameof(n.Titulo)));
				RuleFor(n => n.Sinopsis)
					.NotEmpty().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.Sinopsis)));
				RuleFor(n => n.LibroHasAutores)
					.NotNull().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.LibroHasAutores)))
					.Must(x => x.Count > 0).WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.LibroHasAutores)));
				RuleFor(n => n.EditorialId)
					.NotEmpty().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.EditorialId)));
			});
			
		}
	}
}
