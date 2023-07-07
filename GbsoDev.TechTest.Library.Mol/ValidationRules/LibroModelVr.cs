using FluentValidation;
using GbsoDev.TechTest.Library.Mol.Resources;

namespace GbsoDev.TechTest.Library.Mol.ValidationRules
{
	internal class LibroModelVr : AbstractValidator<LibroModel>
	{
		public LibroModelVr()
		{
			RuleFor(n => n.Titulo)
				.NotNull().WithMessage(n => string.Format(ModelResx.VrPropertyNull, nameof(n.Titulo)));
			RuleFor(n => n.Sinopsis)
				.NotEmpty().WithMessage(n => string.Format(ModelResx.VrPropertyEmpty, nameof(n.Sinopsis)));
			RuleFor(n => n.Autores)
				.NotNull().WithMessage(n => string.Format(ModelResx.VrPropertyEmpty, nameof(n.EditorialId)))
				.Must(x => x.Count > 0).WithMessage(n => string.Format(ModelResx.VrPropertyEmpty, nameof(n.EditorialId)));
			RuleFor(n => n.EditorialId)
				.NotNull().WithMessage(n => string.Format(ModelResx.VrPropertyEmpty, nameof(n.EditorialId)))
				.NotEmpty().WithMessage(n => string.Format(ModelResx.VrPropertyEmpty, nameof(n.EditorialId)));
		}
	}
}
