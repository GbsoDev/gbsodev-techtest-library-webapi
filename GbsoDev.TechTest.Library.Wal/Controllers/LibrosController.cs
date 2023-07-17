using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Dtol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GbsoDev.TechTest.Library.Wal.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[Controller]")]
	public class LibrosController : BaseController
	{
		private ILibroService LibroService => libroService.Value;
		private readonly Lazy<ILibroService> libroService;

		public LibrosController(IServiceProvider serviceProvider, Lazy<ILibroService> libroService) : base(serviceProvider)
		{
			this.libroService = libroService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<LibroDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Get()
		{
			var libros = LibroService.Get();
			var acounResults = Mapper.Map<IEnumerable<Libro>, IEnumerable<LibroDto>>(libros);
			return new OkObjectResult(acounResults);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(LibroDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetById(long id)
		{
			var libro = LibroService.GetById(id);
			var libroResult = Mapper.Map<Libro, LibroDto>(libro);
			return new OkObjectResult(libroResult);
		}

		[HttpPost]
		[ProducesResponseType(typeof(LibroDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Post([FromBody] LibroDto entityModel)
		{
			var libro = Mapper.Map<LibroDto, Libro>(entityModel);
			var libroResult = Mapper.Map<Libro, LibroDto>(LibroService.Set(libro));
			return new OkObjectResult(libroResult);
		}

		[HttpPut]
		[ProducesResponseType(typeof(LibroDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Put([FromBody] LibroDto libroModel)
		{
			var libro = Mapper.Map<LibroDto, Libro>(libroModel);
			var updated = LibroService.Update(libro);
			var libroResult = Mapper.Map<Libro, LibroDto>(updated);
			return new OkObjectResult(libroResult);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Delete(long id)
		{
			LibroService.DeleteById(id);
			return new OkResult();
		}
	}
}
