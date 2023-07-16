using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace GbsoDev.TechTest.Library.MSTest.ServicesTest
{
	[TestClass()]
	public class AutorServiceTest
	{
		private Mock<IAutorDal> mockAutorDal;
		//private AutorService autorSerevice;
		private DateTime createdDate;
		private List<Autor> autores;
		private Random random;
		private CustomServiceCollection serviceCollection;
		[TestInitialize]
		public void Initialize()
		{
			serviceCollection = TestInitializer.ServiceProvider;
			mockAutorDal = new Mock<IAutorDal>();
			createdDate = DateTime.Now;
			random = new Random();
			autores = new List<Autor>();
			for (int i = 1; i <= 10; i++)
			{
				var autor = new Autor
				{
					Id = i,
					Nombre = "Nombre_" + i.ToString(),
					Apellidos = "Apellidos_" + i.ToString(),
					CreatedDate = DateTime.Now.AddDays(random.Next(1, 30))
				};
				autores.Add(autor);
			}
		}

		[TestMethod("Register Ok - Registra autor y retorna nueva instancia con id aumentado y CreateData establecido")]
		public void Register_Ok()
		{
			//Arrange
			var randomId = autores.OrderBy(y => y.Id).Last().Id + random.Next(1, 30);
			var newInput = new Autor()
			{
				Id = default,
				Nombre = "Gerson Brain",
				Apellidos = "Sánchez Ospina",
				CreatedDate = default
			};

			mockAutorDal.Setup(x => x.Register(newInput))
				.Returns(() =>
				{
					newInput.Id = randomId;
					newInput.CreatedDate = createdDate;
					autores.Add(newInput);
					return newInput;
				});

			serviceCollection.CustomAddScoped(mockAutorDal.Object);

			var autorSerevice = new AutorService(serviceCollection.BuildServiceProvider());
			//Act
			var actual = autorSerevice.Set(newInput);

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Id == randomId, "Ultimo id + n asignado");
			Assert.AreEqual(actual.CreatedDate, createdDate, "Fecha de creación establecida");
			Assert.AreEqual(actual, newInput, "Las instancias de la entrada y la salida no son la misma");
		}

		[TestMethod("Delete Ok - Elimina un autor")]
		public void Delete_Ok()
		{
			//Arrange
			var randomId = random.Next(1, 10);
			var toDeleteInput = autores.First(x => x.Id == randomId);

			mockAutorDal.Setup(x => x.Delete(toDeleteInput))
				.Callback(() =>
				{
					autores.Remove(toDeleteInput);
				});

			serviceCollection.CustomAddScoped(mockAutorDal.Object);
			var autorSerevice = new AutorService(serviceCollection.BuildServiceProvider());

			//Act
			autorSerevice.Delete(toDeleteInput);
			//Assert
			Assert.IsTrue(!autores.Any(x => x.Id == toDeleteInput.Id), "Autor Eliminado");
		}

		[TestMethod("Update Ok - Actualizar un autor y retorna la misma instancia")]
		public void Update_Ok()
		{
			//Arrange
			var randomId = random.Next(1, 10);
			var entry = autores.First(x => x.Id == randomId);
			var toUpdateInput = new Autor()
			{
				Id = randomId,
				Nombre = "Nombre Actualizado",
				Apellidos = "Apellidos Actualizados",
			};
			
			mockAutorDal.Setup(x => x.Update(toUpdateInput))
				.Returns(() =>
				{
					entry.Nombre = toUpdateInput.Nombre;
					entry.Apellidos = toUpdateInput.Apellidos;
					return toUpdateInput = entry;
				});
			
			serviceCollection.CustomAddScoped(mockAutorDal.Object);
			var autorService = new AutorService(serviceCollection.BuildServiceProvider());
			//Act

			var actual = autorService.Update(toUpdateInput);
			//Assert

			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Id == randomId, "El Id no debe cambiar luego de actualizar");
			Assert.AreEqual(actual.CreatedDate, entry.CreatedDate, "La fecha de creacion no debe cambia");
			Assert.AreEqual(actual, toUpdateInput, "Las instancias de la entrada y la salida no son la misma");
		}

		[TestMethod("GetById Ok - Obtener un autor por su id")]
		public void GetById_Ok()
		{
			//Arrange
			var randomId = random.Next(1, 10);
			var entry = autores.First(x => x.Id == randomId);
			mockAutorDal.Setup(x => x.GetById(randomId))
				.Returns(() => autores.First(x => x.Id == randomId));

			serviceCollection.CustomAddScoped(mockAutorDal.Object);
			var autorService = new AutorService(serviceCollection.BuildServiceProvider());

			//Act
			var actual = autorService.GetById(randomId);

			//Assert
			Assert.IsNotNull(actual, "El resultado no puede ser null");
			Assert.AreEqual(actual, entry, "Las instancias de la entrada y la salida no son la misma");
		}

		[TestMethod("List Ok - Obtener una lista")]
		public void GetBy_Ok()
		{
			//Arrange
			mockAutorDal.Setup(x => x.List())
				.Returns(() => autores.ToArray());

			serviceCollection.CustomAddScoped(mockAutorDal.Object);
			var autorService = new AutorService(serviceCollection.BuildServiceProvider());

			//Act
			var actual = autorService.Get();

			//Assert
			Assert.IsNotNull(actual, "El resultado no puede ser null");
			Assert.IsTrue(actual.Count() == autores.Count(), "Debe devolver la totalidad de los datos");
		}
	}
}