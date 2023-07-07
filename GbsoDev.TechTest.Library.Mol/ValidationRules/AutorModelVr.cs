using FluentValidation;
using GbsoDev.TechTest.Library.Mol;
using GbsoDev.TechTest.Library.Mol.Resources;

namespace GbsoDev.TechTest.Library.Mol.ValidationRules
{
	public class AutorModelVr : AbstractValidator<AutorModel>
	{
		public AutorModelVr()
		{
			RuleFor(c => c.Nombre)
				.NotNull().WithMessage(x => string.Format(ModelResx.VrPropertyNull, nameof(x.Nombre)))
				.NotEmpty().WithMessage(x => string.Format(ModelResx.VrPropertyNull, nameof(x.Nombre)));
			RuleFor(c => c.Apellidos)
				.NotNull().WithMessage(x => string.Format(ModelResx.VrPropertyNull, nameof(x.Apellidos)))
				.NotEmpty().WithMessage(x => string.Format(ModelResx.VrPropertyNull, nameof(x.Apellidos)));
		}
	}
}
