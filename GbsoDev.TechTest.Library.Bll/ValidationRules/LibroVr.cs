using FluentValidation;
using GbsoDev.TechTest.Library.Bll.Resources;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll.ValidationRules
{
	internal class LibroVr : AbstractValidator<Libro>
	{
		public LibroVr()
		{
			RuleFor(n => n.Id)
				.NotEmpty()
				.Must(n => n.ToString().Length == 10);
			RuleFor(n => n.Titulo)
				.NotNull().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyNull, nameof(n.Titulo)));
			RuleFor(n => n.Sinopsis)
				.NotEmpty().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.Sinopsis)));
			RuleFor(n => n.Autores)
				.NotNull().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.EditorialId)))
				.Must(x => x.Count > 0).WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.EditorialId)));
			RuleFor(n => n.EditorialId)
				.NotNull().WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.EditorialId)))
				.Must(x=> x > 0).WithMessage(n => string.Format(ValidationRulesResx.VrPropertyEmpty, nameof(n.EditorialId)));
			
		}
	}
}
