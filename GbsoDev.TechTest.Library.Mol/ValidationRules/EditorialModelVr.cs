using FluentValidation;
using GbsoDev.TechTest.Library.Mol.Resources;

namespace GbsoDev.TechTest.Library.Mol.ValidationRules
{
	internal class EditorialModelVr : AbstractValidator<EditorialModel>
	{
		public EditorialModelVr()
		{
			RuleFor(c => c.Nombre)
				.NotNull().WithMessage(x => string.Format(ModelResx.VrPropertyNull, nameof(x.Nombre)))
				.NotEmpty().WithMessage(x => string.Format(ModelResx.VrPropertyNull, nameof(x.Nombre)));
			RuleFor(c => c.Sede)
				.NotNull().WithMessage(x => string.Format(ModelResx.VrPropertyNull, nameof(x.Sede)))
				.NotEmpty().WithMessage(x => string.Format(ModelResx.VrPropertyNull, nameof(x.Sede)));
		}
	}
}
