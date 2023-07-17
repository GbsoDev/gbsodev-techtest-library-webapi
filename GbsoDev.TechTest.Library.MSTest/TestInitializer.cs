using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Wal;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Runtime.CompilerServices;

namespace GbsoDev.TechTest.Library.UnitTest
{
	[TestClass]
	public static class TestInitializer
	{
		private readonly static Random random = new Random();
		/// <summary>
		/// Proveedor de servicios por inyección
		/// </summary>
		public readonly static CustomServiceCollection ServiceProvider = new CustomServiceCollection();

		/// <summary>
		/// Inicializa las inyecciónes requeridas por los servicios
		/// </summary>
		[AssemblyInitialize]
		public static void AssemblyInitializer(TestContext context)
		{
			var services = new ServiceCollection()
				.AddBllValidationRulesLayer()
				.AddMapperConfiguration();
			ServiceProvider
				.CustomAddRange(services)
				.AddDalMocks()
				.AddLoggers<BaseController>()
				.AddLoggers<BaseService>();
		}

		public static CustomServiceCollection AddDalMocks(this CustomServiceCollection services)
		{
			return services
				.CustomAddScoped(BuildAutorDal())
				.CustomAddScoped(BuildEditorialDal())
				.CustomAddScoped(BuildLibroDal());
		}

		public static IAutorDal BuildAutorDal()
		{
			var autorDalMock = new Mock<IAutorDal>();
			autorDalMock.Setup(x => x.Register(It.IsAny<Autor>()))
				.Returns<Autor>((input) =>
				{
					input.Id = 1;
					input.CreatedDate = DateTime.Now;
					return input;
				});
			autorDalMock.Setup(x => x.Delete(It.IsAny<Autor>()))
				.Callback<Autor>((input) =>
				{
				});
			autorDalMock.Setup(x => x.Update(It.IsAny<Autor>()))
			.Returns<Autor>((input) =>
			{
				input.CreatedDate = DateTime.Now;
				return input;
			});
			autorDalMock.Setup(x => x.GetById(It.IsAny<int>()))
				.Returns<int>((id) =>
				{
					var autor = GetAutor(1);
					return id == autor.Id ? autor : null;
				});
			autorDalMock.Setup(x => x.List())
				.Returns(() =>
				{
					return GetRandomAutores().ToArray();
				});
			return autorDalMock.Object;
		}

		public static IEditorialDal BuildEditorialDal()
		{
			var editorialDalMock = new Mock<IEditorialDal>();
			editorialDalMock.Setup(x => x.Register(It.IsAny<Editorial>()))
				.Returns<Editorial>((input) =>
				{
					input.Id = 1;
					input.CreatedDate = DateTime.Now;
					return input;
				});
			editorialDalMock.Setup(x => x.Delete(It.IsAny<Editorial>()))
				.Callback<Editorial>((input) =>
				{
				});
			editorialDalMock.Setup(x => x.Update(It.IsAny<Editorial>()))
				.Returns<Editorial>((input) =>
				{
					input.CreatedDate = DateTime.Now;
					return input;
				});
			editorialDalMock.Setup(x => x.GetById(It.IsAny<int>()))
				.Returns<int>((id) =>
				{
					var editorial = GetEditorial(1);
					return id == editorial.Id ? editorial : null;
				});
			editorialDalMock.Setup(x => x.List())
				.Returns(() =>
				{
					return GetRandomEditoriales().ToArray();
				});
			return editorialDalMock.Object;
		}

		public static ILibroDal BuildLibroDal()
		{
			var libroDalMock = new Mock<ILibroDal>();
			libroDalMock.Setup(x => x.Register(It.IsAny<Libro>()))
				.Returns<Libro>((input) =>
				{
					input.CreatedDate = DateTime.Now;
					return input;
				});
			libroDalMock.Setup(x => x.Delete(It.IsAny<Libro>()))
				.Callback<Libro>((input) =>
				{
				});
			libroDalMock.Setup(x => x.Update(It.IsAny<Libro>()))
				.Returns<Libro>((input) =>
				{
					input.CreatedDate = DateTime.Now;
					return input;
				});
			libroDalMock.Setup(x => x.GetById(It.IsAny<long>()))
				.Returns<long>((id) =>
				{
					var libro = GetLibro(1);
					return id == libro.Id ? libro : null;
				});
			libroDalMock.Setup(x => x.List())
				.Returns(() =>
				{
					return GetRandomLibros().ToArray();
				});
			return libroDalMock.Object;
		}

		private static Autor GetAutor(int id)
		{
			var autor = new Autor
			{
				Id = id,
				Nombre = "Nombre_" + id.ToString(),
				Apellidos = "Apellidos_" + id.ToString(),
				CreatedDate = DateTime.Now.AddYears(random.Next(-10, 10)).AddDays(random.Next(-10, 10))
			};
			return autor;
		}
		
		public static List<Autor> GetRandomAutores()
		{
			var autores = new List<Autor>();
			for (int i = 1; i <= 10; i++)
			{
				var autor = GetAutor(i);
				autores.Add(autor);
			}
			return autores;
		}

		private static Editorial GetEditorial(int id)
		{
			var editorial = new Editorial
			{
				Id = id,
				Nombre = "Editorial_" + id.ToString(),
				Sede = "Sede_" + id.ToString(),
				CreatedDate = DateTime.Now.AddYears(random.Next(-10, 10)).AddDays(random.Next(-10, 10))
			};
			return editorial;
		}

		public static List<Editorial> GetRandomEditoriales()
		{
			var editoriales = new List<Editorial>();
			for (int i = 1; i <= 3; i++)
			{
				var editorial = GetEditorial(i);
				editoriales.Add(editorial);
			}
			return editoriales;
		}

		private static Libro GetLibro(long id)
		{
			var editorial = GetEditorial(random.Next(1, 100));
			var libro = new Libro
			{
				Id = id,
				EditorialId = editorial.Id,
				Editorial = editorial,
				Titulo = "Título_" + id.ToString(),
				Sinopsis = "Sinopsis_" + id.ToString(),
				NPaginas = "100",
				CreatedDate = DateTime.Now.AddYears(random.Next(-10, 10)).AddDays(random.Next(-10, 10)),
				LibroHasAutores = new List<AutorHasLibro>()
			};
			return libro;
		}

		public static List<Libro> GetRandomLibros()
		{
			var autores = GetRandomAutores();
			var random = new Random();
			var libros = new List<Libro>();
			var firstId = random.NextInt64(1111111111111, 9999999999999);
			for (long i = firstId; i < 10; i++)
			{
				var libro = GetLibro(i);
				var nAutores = random.Next(1, autores.Count);
				for (int x = 1; x <= nAutores; x++)
				{
					libro.LibroHasAutores.Add(new AutorHasLibro
					{
						AutorId = x,
						Autor = GetAutor(x),
						LibroId = libro.Id,
						Libro = libro
					});
				}
				libros.Add(libro);
			}
			return libros;
		}
	}
}
