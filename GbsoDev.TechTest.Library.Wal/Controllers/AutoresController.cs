using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Dtol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GbsoDev.TechTest.Library.Wal.Controllers
{
	[Authorize]
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
		[ProducesResponseType(typeof(AutorDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Post([FromBody] AutorDto entityModel)
		{
			var entity = Mapper.Map<AutorDto, Autor> (entityModel);
			var entityResult =  Mapper.Map<Autor, AutorDto>(AutorService.Set(entity));
			return new OkObjectResult(entityResult);
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<AutorDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Get()
		{
			var entity = AutorService.Get();
			var entityResult = Mapper.Map<IEnumerable<Autor>, IEnumerable<AutorDto>>(entity);
			return new OkObjectResult(entityResult);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(AutorDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetById(int id)
		{
			var entity = AutorService.GetById(id);
			var entityResult = Mapper.Map<Autor, AutorDto>(entity);
			return new OkObjectResult(entityResult);
		}

		[HttpPut]
		[ProducesResponseType(typeof(AutorDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Put([FromBody] AutorDto autorModel)
		{
			var entity = Mapper.Map<AutorDto, Autor>(autorModel);
			var entityResult = Mapper.Map<Autor, AutorDto>(AutorService.Update(entity));
			return new OkObjectResult(entityResult);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(AutorDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Delete(int id)
		{
			AutorService.DeleteById(id);
			return new OkResult();
		}



		[HttpGet("{id}/libros")]
		[ProducesResponseType(typeof(List<LibroDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetNotesByLibro(int id)
		{
			var libros = LibroService.GetByAutorId(id);
			var noteResult = Mapper.Map<List<Libro>, List<LibroDto>>(libros);
			return new OkObjectResult(noteResult);
		}
	}
}
