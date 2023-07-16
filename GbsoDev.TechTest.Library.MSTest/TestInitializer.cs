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
				.CustomAddScoped(BuildAutorDal());
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
							CreatedDate = DateTime.Now.AddDays(random.Next(1, 30))
						};
						autores.Add(autor);
					}
					return autores.ToArray();
				});
			return autorDalMock.Object;
		}

	}
}
