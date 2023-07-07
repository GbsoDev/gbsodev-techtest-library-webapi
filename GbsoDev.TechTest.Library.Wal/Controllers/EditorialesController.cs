using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Mol;
using Microsoft.AspNetCore.Mvc;

namespace GbsoDev.TechTest.Library.Wal.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EditorialesController : BaseController
	{
		public IEditorialService EditorialService { get => editorialService.Value; }
		private readonly Lazy<IEditorialService> editorialService;

		public EditorialesController(IServiceProvider serviceProvider, Lazy<IEditorialService> noteService) : base(serviceProvider)
		{
			this.editorialService = noteService;
		}

		[HttpGet("note")]
		[ProducesResponseType(typeof(IEnumerable<EditorialModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Get()
		{
			var editoriales = EditorialService.Get();
			var noteResults = Mapper.Map<IEnumerable<Editorial>, IEnumerable<EditorialModel>>(editoriales);
			return new OkObjectResult(noteResults);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(EditorialModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetById(int id)
		{
			var note = EditorialService.GetById(id);
			var noteResult = Mapper.Map<Editorial, EditorialModel>(note);
			return new OkObjectResult(noteResult);
		}

		[HttpPost("note")]
		[ProducesResponseType(typeof(EditorialModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Post([FromBody] EditorialModel entityModel)
		{
			var note = Mapper.Map<EditorialModel, Editorial>(entityModel);
			var noteResult = EditorialService.Set(note);
			return new OkObjectResult(noteResult);
		}

		[HttpPut()]
		[ProducesResponseType(typeof(EditorialModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Put([FromBody] EditorialModel personModel)
		{
			var note = Mapper.Map<EditorialModel, Editorial>(personModel);
			var noteResult = EditorialService.Update(note);
			return new OkObjectResult(noteResult);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Delete(int id)
		{
			EditorialService.DeleteById(id);
			return new OkResult();
		}
	}
}
