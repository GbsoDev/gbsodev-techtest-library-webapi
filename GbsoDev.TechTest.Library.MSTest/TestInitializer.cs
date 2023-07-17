using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Wal;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace GbsoDev.TechTest.Library.MSTest
{
	[TestClass]
	public static class TestInitializer
	{
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
				.CustomAddScoped(BuildEditorialDal());
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
				return input;
			});
			autorDalMock.Setup(x => x.GetById(It.IsAny<int>()))
				.Returns<int>((id) =>
				{
					var autor = new Autor()
					{
						Id = 1,
						Nombre = "Gerson Brain",
						Apellidos = "Sánchez Ospina",
						CreatedDate = DateTime.Now,
					};
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
					return input;
				});
			editorialDalMock.Setup(x => x.GetById(It.IsAny<int>()))
				.Returns<int>((id) =>
				{
					var editorial = new Editorial()
					{
						Id = 1,
						Nombre = "Nombre de la Editorial",
						Sede = "Sede de la Editorial",
						CreatedDate = DateTime.Now,
					};
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
					input.Id = 1;
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
					return input;
				});
			libroDalMock.Setup(x => x.GetById(It.IsAny<long>()))
				.Returns<long>((id) =>
				{

					var libro = new Libro()
					{
						Id = 1,
						EditorialId = 1,
						Editorial = new Editorial()
						{
							Id = 1,
							Nombre = "Nombre de la Editorial",
							Sede = "Sede de la Editorial",
							CreatedDate = DateTime.Now,
						},
						Titulo = "Título del Libro",
						Sinopsis = "Sinopsis del Libro",
						NPaginas = "100",
						CreatedDate = DateTime.Now,
						LibroHasAutores = new List<AutorHasLibro>()
					};
					return id == libro.Id ? libro : null;
				});
			libroDalMock.Setup(x => x.List())
				.Returns(() =>
				{
					return GetRandomLibros().ToArray();
				});
			return libroDalMock.Object;
		}

		public static List<Autor> GetRandomAutores()
		{
			var createdDate = DateTime.Now;
			var random = new Random();
			var autores = new List<Autor>();
			for (int i = 1; i <= 10; i++)
			{
				var autor = new Autor
				{
					Id = random.Next(1, 1000),
					Nombre = "Nombre_" + i.ToString(),
					Apellidos = "Apellidos_" + i.ToString(),
					CreatedDate = createdDate.AddDays(random.Next(1, 30))
				};
				autores.Add(autor);
			}
			return autores;
		}
		
		public static List<Editorial> GetRandomEditoriales()
		{
			var createdDate = DateTime.Now;
			var random = new Random();
			var editoriales = new List<Editorial>();
			for (int i = 1; i <= 10; i++)
			{
				var editorial = new Editorial
				{
					Id = random.Next(1, 1000),
					Nombre = "Editorial_" + i.ToString(),
					Sede = "Sede_" + i.ToString(),
					CreatedDate = createdDate.AddDays(random.Next(1, 30))
				};
				editoriales.Add(editorial);
			}
			return editoriales;
		}

		public static List<Libro> GetRandomLibros()
		{
			var autores = GetRandomAutores();
			var createdDate = DateTime.Now;
			var random = new Random();
			var libros = new List<Libro>();
			for (int i = 1; i <= 10; i++)
			{

				var libro = new Libro
				{
					Id = random.Next(1, 1000),
					EditorialId = random.Next(1, 100),
					Editorial = new Editorial()
					{
						Id = 1,
						Nombre = "Nombre de la Editorial",
						Sede = "Sede de la Editorial",
						CreatedDate = DateTime.Now,
					},
					Titulo = "Título_" + i.ToString(),
					Sinopsis = "Sinopsis_" + i.ToString(),
					NPaginas = "100",
					CreatedDate = createdDate.AddDays(random.Next(1, 30)),
					LibroHasAutores = new List<AutorHasLibro>()
				};
				libro.LibroHasAutores = autores.Select(x => new AutorHasLibro
				{
					AutorId = x.Id,
					Autor = x,
					LibroId = libro.Id,
					Libro = libro
				}).ToList();
				libros.Add(libro);
			}
			return libros;
		}
	}
}
