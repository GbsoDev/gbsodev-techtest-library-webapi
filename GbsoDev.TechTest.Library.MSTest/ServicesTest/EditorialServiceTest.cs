using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.El;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.UnitTest.ServicesTest
{
	[TestClass]
	public class EditorialServiceTest
	{
		private CustomServiceCollection serviceCollection;

		[TestInitialize]
		public void Initialize()
		{
			serviceCollection = TestInitializer.ServiceProvider;
		}

		[TestMethod("Register Ok - Se espera una editorial registrada")]
		public void Register_Ok()
		{
			// Arrange
			var input = new Editorial()
			{
				Id = default,
				Nombre = "Nombre de la Editorial",
				Sede = "Sede de la Editorial",
				CreatedDate = default
			};

			var expected = new
			{
				input.Nombre,
				input.Sede
			};

			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());

			// Act
			var actual = editorialService.Set(input);

			// Assert
			Utils.AreEquals(expected, actual);
			Assert.IsTrue(actual.Id > 0, "El Id no fue asignado");
			Assert.IsNotNull(actual.CreatedDate, "La fecha de creación puede ser null");
		}

		[TestMethod("Delete Ok - No se espera nada")]
		public void Delete_Ok()
		{
			// Arrange
			var input = new Editorial()
			{
				Id = 1
			};

			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());

			// Act
			editorialService.Delete(input);

			// Assert
			// No hay aserciones específicas ya que se espera que no ocurran errores durante la eliminación
		}

		[TestMethod("Update Ok - Se espera una editorial actualizada")]
		public void Update_Ok()
		{
			// Arrange
			var input = new Editorial()
			{
				Id = 1,
				Nombre = "Nombre Actualizado",
				Sede = "Sede Actualizada",
				CreatedDate = default
			};

			var expected = new
			{
				input.Id,
				input.Nombre,
				input.Sede
			};

			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());

			// Act
			var actual = editorialService.Update(input);

			// Assert
			Assert.IsNotNull(actual, "El resultado no puede ser null");
			Assert.IsNotNull(actual.CreatedDate, "La fecha de creación puede ser null");
			Utils.IsNotNulls(expected, actual);
			Utils.AreEquals(expected, actual);
		}

		[DataRow(1, false, DisplayName = "GetById Ok - Se espera una editorial")]
		[DataRow(2, true, DisplayName = "GetById Ok - Se espera un resultado null")]
		[TestMethod]
		public void GetById_Ok(int inputId, bool nullExpected)
		{
			// Arrange
			var obj = new Editorial();
			var expected = new
			{
				obj.Nombre,
				obj.Sede,
				obj.CreatedDate
			};

			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());

			// Act
			var actual = editorialService.GetById(inputId);

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

		[TestMethod("List Ok - Se espera una lista de editoriales")]
		public void List_Ok()
		{
			// Arrange
			var obj = new Editorial();
			var expected = new
			{
				obj.Nombre,
				obj.Sede,
				obj.CreatedDate
			};

			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());

			// Act
			var actuals = editorialService.Get();

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
