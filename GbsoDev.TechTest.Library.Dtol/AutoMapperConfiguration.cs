using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Dtol
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
			#region Libro
			CreateMap<Libro, LibroModel>()
				.ForMember(x => x.Isbn, m => m.MapFrom(y => y.Id))
				.ForMember(x => x.Titulo, m => m.MapFrom(y => y.Titulo))
				.ForMember(x => x.EditorialId, m => m.MapFrom(y => y.EditorialId))
				.ForMember(x => x.EditorialNombre, m => m.MapFrom(y => y.Editorial != null ? y.Editorial.Nombre : null))
				.ForMember(x => x.Sinopsis, m => m.MapFrom(y => y.Sinopsis))
				.ForMember(x => x.NPaginas, m => m.MapFrom(y => y.NPaginas))
				.ForMember(x => x.Autores, m => m.MapFrom(y => y.LibroHasAutores.Select(a => new AutorModel
				{
					Id = a.AutorId,
					Nombre = a.Autor != null ? a.Autor.Nombre : null,
					Apellidos = a.Autor != null ? a.Autor.Apellidos : null
				})))
				.ReverseMap()
				.ForMember(x=> x.Editorial, m=> m.Ignore())
				.ForMember(x => x.LibroHasAutores, m => m.MapFrom(y => y.Autores.Select(a => new AutorHasLibro
				{
					AutorId = a.Id,
					LibroId = y.Isbn
				})));
			#endregion
			#region Editorial
			CreateMap<Editorial, EditorialModel>()
				.ForMember(x => x.Id, m => m.MapFrom(y => y.Id))
				.ForMember(x => x.Nombre, m => m.MapFrom(y => y.Nombre))
				.ForMember(x => x.Sede, m => m.MapFrom(y => y.Sede))
				.ReverseMap();
			#endregion
			#region Auth
			CreateMap<Usuario, AuthModel>()
				.ForMember(x => x.Nombre, m => m.MapFrom(y => y.Nombre))
				.ForMember(x => x.UserName, m => m.MapFrom(y => y.UserName))
				.ForMember(x => x.Password, m => m.Ignore())
				.ReverseMap()
				.ForMember(x => x.Password, m => m.MapFrom(y=> y.Password));
			#endregion
		}
	}
}
