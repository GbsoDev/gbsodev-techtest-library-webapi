using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Mol;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace GbsoDev.TechTest.Library.Wal.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AutoresController : BaseController
	{
		private IAutorService AutorService => autorService.Value;
		private readonly Lazy<IAutorService> autorService;
		private ILibroService LibroService => libroService.Value;
		private readonly Lazy<ILibroService> libroService;

		public AutoresController(IServiceProvider serviceProvider, Lazy<IAutorService> autorService, Lazy<ILibroService> libroService) : base(serviceProvider)
		{
			this.autorService = autorService;
			this.libroService = libroService;
		}

		[HttpPost]
		[ProducesResponseType(typeof(AutorModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Post([FromBody] AutorModel entityModel)
		{
			var entity = Mapper.Map<AutorModel, Autor> (entityModel);
			var entityResult =  Mapper.Map<Autor, AutorModel>(AutorService.Set(entity));
			return new OkObjectResult(entityResult);
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<AutorModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Get()
		{
			var entity = AutorService.Get();
			var entityResult = Mapper.Map<IEnumerable<Autor>, IEnumerable<AutorModel>>(entity);
			return new OkObjectResult(entityResult);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(AutorModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetById(int id)
		{
			var entity = AutorService.GetById(id);
			var entityResult = Mapper.Map<Autor, AutorModel>(entity);
			return new OkObjectResult(entityResult);
		}

		[HttpPut]
		[ProducesResponseType(typeof(AutorModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Put([FromBody] AutorModel autorModel)
		{
			var entity = Mapper.Map<AutorModel, Autor>(autorModel);
			var entityResult = Mapper.Map<Autor, AutorModel>(AutorService.Update(entity));
			return new OkObjectResult(entityResult);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(AutorModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Delete(int id)
		{
			AutorService.DeleteById(id);
			return new OkResult();
		}



		[HttpGet("{id}/libros")]
		[ProducesResponseType(typeof(List<LibroModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetNotesByLibro(int id)
		{
			var libros = LibroService.GetByAutorId(id);
			var noteResult = Mapper.Map<List<Libro>, List<LibroModel>>(libros);
			return new OkObjectResult(noteResult);
		}
	}
}
