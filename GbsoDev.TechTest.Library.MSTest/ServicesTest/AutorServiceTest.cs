using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.El;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.UnitTest.ServicesTest
{
	[TestClass()]
	public class AutorServiceTest
	{
		private CustomServiceCollection serviceCollection;
		[TestInitialize]
		public void Initialize()
		{
			serviceCollection = TestInitializer.ServiceProvider;
		}

		[TestMethod("Register Ok - Se espera un autor registrado")]
		public void Register_Ok()
		{
			//Arrange
			var input = new Autor()
			{
				Id = default,
				Nombre = "Gerson Brain",
				Apellidos = "Sánchez Ospina",
				CreatedDate = default
			};

			var expected = new
			{
				input.Nombre,
				input.Apellidos,
			};
			var autorSerevice = new AutorService(serviceCollection.BuildServiceProvider());
			//Act
			var actual = autorSerevice.Set(input);
			//Assert
			Utils.AreEquals(expected, actual);
			Assert.IsNotNull(actual.CreatedDate, "La fecha de creación puede ser null");
			Assert.IsTrue(actual.Id > 0, "el Id no fue asignado");
		}

		[TestMethod("Delete Ok - No se espera nada")]
		public void Delete_Ok()
		{
			//Arrange
			var input = new Autor()
			{ 
				Id = 1,
			};
			var autorSerevice = new AutorService(serviceCollection.BuildServiceProvider());
			//Act
			autorSerevice.Delete(input);
			//Assert
		}

		[TestMethod("Update Ok - Se espera un autor actualizado")]
		public void Update_Ok()
		{
			//Arrange
			var input = new Autor()
			{
				Id = 1,
				Nombre = "Nombre Actualizado",
				Apellidos = "Apellidos Actualizados",
				CreatedDate = default
			};

			var expected = new
			{
				input.Id,
				input.Nombre,
				input.Apellidos
			};
			
			var autorService = new AutorService(serviceCollection.BuildServiceProvider());
			
			//Act
			var actual = autorService.Update(input);
			
			//Assert
			Assert.IsNotNull(actual, "El resultado no puede ser null");
			Assert.IsNotNull(actual.CreatedDate, "La fecha de creación puede ser null");
			Utils.IsNotNulls(expected, actual);
			Utils.AreEquals(expected, actual);
		}

		[DataRow(1, false, DisplayName = "GetById Ok - Se espera un autor")]
		[DataRow(2, true, DisplayName = "GetById Ok - Se espera un resultado null")]
		[TestMethod]
		public void GetById_Ok(int inputId, bool nullExpected)
		{
			//Arrange
			var obj = new Autor();
			var expected = new
			{
				obj.Nombre,
				obj.Apellidos,
				obj.AutorHasLibros,
				obj.CreatedDate
			};

			var autorService = new AutorService(serviceCollection.BuildServiceProvider());

			//Act
			var actual = autorService.GetById(inputId);

			//Assert
			if (nullExpected)
			{
				Assert.IsNull(actual, "Se espera un resultado null");
			}
			else {
				Assert.IsNotNull(actual, "No se espera un resultado null");
				Utils.IsNotNulls(expected, actual);
				Assert.AreEqual(inputId, actual.Id, "El id de entrada no con corresponde con el id de salida");
			}
		}

		[TestMethod("List Ok - Se espera una lista de autors")]
		public void GetBy_Ok()
		{
			//Arrange
			var obj = new Autor();
			var expected = new
			{
				obj.Nombre,
				obj.Apellidos,
				obj.CreatedDate,
				obj.AutorHasLibros
			};
			var autorService = new AutorService(serviceCollection.BuildServiceProvider());
			
			//Act
			var actuals = autorService.Get();

			//Assert
			Assert.IsNotNull(actuals, "El resultado no puede ser null");
			foreach (var actual in actuals)
			{
				Assert.IsNotNull(actual, "La lista no puede devolver valor nulos");
				Utils.IsNotNulls(expected, actual);
				Assert.IsTrue(actual.Id > 0, "el Id no fue asignado");
			}
		}
	}
}