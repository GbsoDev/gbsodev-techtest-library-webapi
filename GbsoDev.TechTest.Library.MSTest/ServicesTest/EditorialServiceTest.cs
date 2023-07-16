using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace GbsoDev.TechTest.Library.MSTest.ServicesTest
{
	[TestClass()]
	public class EditorialServiceTest
	{
		private Mock<IEditorialDal> mockEditorialDal;
		private DateTime createdDate;
		private List<Editorial> editoriales;
		private Random random;
		private CustomServiceCollection serviceCollection;

		[TestInitialize]
		public void Initialize()
		{
			serviceCollection = TestInitializer.ServiceProvider;
			mockEditorialDal = new Mock<IEditorialDal>();
			createdDate = DateTime.Now;
			random = new Random();
			editoriales = new List<Editorial>();
			for (int i = 1; i <= 10; i++)
			{
				var editorial = new Editorial
				{
					Id = i,
					Nombre = "Nombre_" + i.ToString(),
					Sede = "Sede_" + i.ToString(),
					CreatedDate = DateTime.Now.AddDays(random.Next(1, 30))
				};
				editoriales.Add(editorial);
			}
		}

		[TestMethod("Register Ok - Registra editorial y retorna nueva instancia con id aumentado y CreateData establecido")]
		public void Register_Ok()
		{
			//Arrange
			var randomId = editoriales.OrderBy(y => y.Id).Last().Id + random.Next(1, 30);
			var newInput = new Editorial()
			{
				Id = default,
				Nombre = "Nombre Editorial",
				Sede = "Sede Editorial",
				CreatedDate = default
			};

			mockEditorialDal.Setup(x => x.Register(newInput))
				.Returns(() =>
				{
					newInput.Id = randomId;
					newInput.CreatedDate = createdDate;
					editoriales.Add(newInput);
					return newInput;
				});

			serviceCollection.CustomAddScoped(mockEditorialDal.Object);

			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());
			//Act
			var actual = editorialService.Set(newInput);

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Id == randomId, "Ultimo id + n asignado");
			Assert.AreEqual(createdDate, actual.CreatedDate, "Fecha de creación establecida");
			Assert.AreEqual(newInput, actual, "Las instancias de la entrada y la salida no son la misma");
		}

		[TestMethod("Delete Ok - Elimina una editorial")]
		public void Delete_Ok()
		{
			//Arrange
			var randomId = random.Next(1, 10);
			var toDeleteInput = editoriales.First(x => x.Id == randomId);

			mockEditorialDal.Setup(x => x.Delete(toDeleteInput))
				.Callback(() =>
				{
					editoriales.Remove(toDeleteInput);
				});

			serviceCollection.CustomAddScoped(mockEditorialDal.Object);
			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());

			//Act
			editorialService.Delete(toDeleteInput);
			//Assert
			Assert.IsTrue(!editoriales.Any(x => x.Id == toDeleteInput.Id), "Editorial Eliminada");
		}

		[TestMethod("Update Ok - Actualizar una editorial y retorna la misma instancia")]
		public void Update_Ok()
		{
			//Arrange
			var randomId = random.Next(1, 10);
			var entry = editoriales.First(x => x.Id == randomId);
			var toUpdateInput = new Editorial()
			{
				Id = randomId,
				Nombre = "Nombre Actualizado",
				Sede = "Sede Actualizada",
			};

			mockEditorialDal.Setup(x => x.Update(toUpdateInput))
				.Returns(() =>
				{
					entry.Nombre = toUpdateInput.Nombre;
					entry.Sede = toUpdateInput.Sede;
					return toUpdateInput = entry;
				});

			serviceCollection.CustomAddScoped(mockEditorialDal.Object);
			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());
			//Act

			var actual = editorialService.Update(toUpdateInput);
			//Assert

			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Id == randomId, "El Id no debe cambiar luego de actualizar");
			Assert.AreEqual(entry.CreatedDate, actual.CreatedDate, "La fecha de creación no debe cambiar");
			Assert.AreEqual(toUpdateInput, actual, "Las instancias de la entrada y la salida no son la misma");
		}

		[TestMethod("GetById Ok - Obtener una editorial por su id")]
		public void GetById_Ok()
		{
			//Arrange
			var randomId = random.Next(1, 10);
			var entry = editoriales.First(x => x.Id == randomId);
			mockEditorialDal.Setup(x => x.GetById(randomId))
				.Returns(() => editoriales.First(x => x.Id == randomId));

			serviceCollection.CustomAddScoped(mockEditorialDal.Object);
			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());

			//Act
			var actual = editorialService.GetById(randomId);

			//Assert
			Assert.IsNotNull(actual, "El resultado no puede ser null");
			Assert.AreEqual(entry, actual, "Las instancias de la entrada y la salida no son la misma");
		}

		[TestMethod("List Ok - Obtener una lista")]
		public void GetBy_Ok()
		{
			//Arrange
			mockEditorialDal.Setup(x => x.List())
				.Returns(() => editoriales.ToArray());

			serviceCollection.CustomAddScoped(mockEditorialDal.Object);
			var editorialService = new EditorialService(serviceCollection.BuildServiceProvider());

			//Act
			var actual = editorialService.Get();

			//Assert
			Assert.IsNotNull(actual, "El resultado no puede ser null");
			Assert.IsTrue(actual.Count() == editoriales.Count(), "Debe devolver la totalidad de los datos");
		}
	}
}
