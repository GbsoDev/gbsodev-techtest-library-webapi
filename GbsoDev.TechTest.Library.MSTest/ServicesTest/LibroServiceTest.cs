using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.El;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.UnitTest.ServicesTest
{
	[TestClass]
	public class LibroServiceTest
	{
		private CustomServiceCollection serviceCollection;

		[TestInitialize]
		public void Initialize()
		{
			serviceCollection = TestInitializer.ServiceProvider;
		}

		[TestMethod("Register Ok - Se espera un libro registrado")]
		public void Register_Ok()
		{
			// Arrange
			var id = 5648276839158;
			var input = new Libro
			{
				Id = id,
				EditorialId = 1,
				Titulo = "Título del Libro",
				Sinopsis = "Sinopsis del Libro",
				NPaginas = "100",
				LibroHasAutores = new List<AutorHasLibro>() { new AutorHasLibro()
				{
					Id = 1,
					LibroId = id,
					AutorId = 1,
				}},
				CreatedDate = default
			};

			var expected = new
			{
				input.Id,
				input.EditorialId,
				input.Titulo,
				input.Sinopsis,
				input.NPaginas,
				input.LibroHasAutores
			};

			var libroService = new LibroService(serviceCollection.BuildServiceProvider());

			// Act
			var actual = libroService.Set(input);

			// Assert
			Assert.IsNotNull(actual, "El resultado no puede ser null");
			Assert.IsNotNull(actual.CreatedDate, "La fecha de creación puede ser null");
			Utils.IsNotNulls(expected, actual);
			Utils.AreEquals(expected, actual);
		}

		[TestMethod("Delete Ok - No se espera nada")]
		public void Delete_Ok()
		{
			// Arrange
			var input = new Libro
			{
				Id = 1
			};

			var libroService = new LibroService(serviceCollection.BuildServiceProvider());

			// Act
			libroService.Delete(input);

			// Assert
		}

		[TestMethod("Update Ok - Se espera un libro actualizado")]
		public void Update_Ok()
		{
			// Arrange
			var id = 5648276839158;
			var input = new Libro
			{
				Id = 5648276839158,
				EditorialId = 1,
				Titulo = "Título del Libro",
				Sinopsis = "Sinopsis del Libro",
				NPaginas = "100",
				CreatedDate = default,
				LibroHasAutores = new List<AutorHasLibro>() { new AutorHasLibro()
				{
					Id = 1,
					LibroId = id,
					AutorId = 1,
				}}
			};

			var expected = new
			{
				input.Id,
				input.Titulo,
				input.Sinopsis,
				input.NPaginas,
				input.LibroHasAutores,
			};

			var libroService = new LibroService(serviceCollection.BuildServiceProvider());

			// Act
			var actual = libroService.Update(input);

			// Assert
			Assert.IsNotNull(actual, "El resultado no puede ser null");
			Assert.IsNotNull(actual.CreatedDate, "El resultado no puede ser null");
			Utils.IsNotNulls(expected, actual);
			Utils.AreEquals(expected, actual);
		}

		[TestMethod]
		[DataRow(1, false, DisplayName = "GetById Ok - Se espera un libro")]
		[DataRow(2, true, DisplayName = "GetById Ok - Se espera un resultado null")]
		public void GetById_Ok(int inputId, bool nullExpected)
		{
			// Arrange
			var obj = new Libro();
			var expected = new
			{
				obj.Titulo,
				obj.Sinopsis,
				obj.NPaginas,
				obj.CreatedDate,
				obj.LibroHasAutores
			};

			var libroService = new LibroService(serviceCollection.BuildServiceProvider());

			// Act
			var actual = libroService.GetById(inputId);

			// Assert
			if (nullExpected)
			{
				Assert.IsNull(actual, "Se espera un resultado null");
			}
			else
			{
				Assert.IsNotNull(actual, "No se espera un resultado null");
				Utils.IsNotNulls(expected, actual);
				Assert.AreEqual(inputId, actual.Id, "El id de entrada no corresponde con el id de salida");
			}
		}

		[TestMethod("List Ok - Se espera una lista de libros")]
		public void GetBy_Ok()
		{
			// Arrange
			var obj = new Libro();
			var expected = new
			{
				obj.Titulo,
				obj.Sinopsis,
				obj.NPaginas,
				obj.CreatedDate,
				obj.LibroHasAutores
			};

			var libroService = new LibroService(serviceCollection.BuildServiceProvider());

			// Act
			var actuals = libroService.Get();

			// Assert
			Assert.IsNotNull(actuals, "El resultado no puede ser null");
			foreach (var actual in actuals)
			{
				Assert.IsNotNull(actual, "La lista no puede devolver valores nulos");
				Utils.IsNotNulls(expected, actual);
				Assert.IsTrue(actual.Id > 0, "El Id no fue asignado");
			}
		}
	}
}
