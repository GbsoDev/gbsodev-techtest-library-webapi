using GbsoDev.TechTest.Library.El;
using AutoMapper;

namespace GbsoDev.TechTest.Library.Mol
{
	public class AutoMapperConfiguration : AutoMapper.Profile
	{
		public AutoMapperConfiguration()
		{
			#region Autor
			CreateMap<Autor, AutorModel>()
				.ForMember(x => x.Id, m => m.MapFrom(y => y.Id))
				.ForMember(x => x.Nombre, m => m.MapFrom(y => y.Nombre))
				.ForMember(x => x.Apellidos, m => m.MapFrom(y => y.Apellidos))
				.ReverseMap();
			#endregion
			#region Acount
			CreateMap<Libro, LibroModel>()
				.ForMember(x => x.Isbn, m => m.MapFrom(y => y.Id))
				.ForMember(x => x.Titulo, m => m.MapFrom(y => y.Titulo))
				.ForMember(x => x.EditorialId, m => m.MapFrom(y => y.EditorialId))
				.ForMember(x => x.Sinopsis, m => m.MapFrom(y => y.Sinopsis))
				.ForMember(x => x.NPaginas, m => m.MapFrom(y => y.NPaginas))
				.ForMember(x => x.Autores, m => m.MapFrom(y => y.Autores))
				.ReverseMap();
			#endregion
			#region Acount
			CreateMap<Editorial, EditorialModel>()
				.ForMember(x => x.Id, m => m.MapFrom(y => y.Id))
				.ForMember(x => x.Nombre, m => m.MapFrom(y => y.Nombre))
				.ForMember(x => x.Sede, m => m.MapFrom(y => y.Sede))
				.ReverseMap();
			#endregion
		}
	}
}
