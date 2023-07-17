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
	public class EditorialesController : BaseController
	{
		public IEditorialService EditorialService { get => editorialService.Value; }
		private readonly Lazy<IEditorialService> editorialService;

		public EditorialesController(IServiceProvider serviceProvider, Lazy<IEditorialService> editorialService) : base(serviceProvider)
		{
			this.editorialService = editorialService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<EditorialDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Get()
		{
			var editoriales = EditorialService.Get();
			var editorialResults = Mapper.Map<IEnumerable<Editorial>, IEnumerable<EditorialDto>>(editoriales);
			return new OkObjectResult(editorialResults);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(EditorialDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetById(int id)
		{
			var editorial = EditorialService.GetById(id);
			var editorialResult = Mapper.Map<Editorial, EditorialDto>(editorial);
			return new OkObjectResult(editorialResult);
		}

		[HttpPost]
		[ProducesResponseType(typeof(EditorialDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Post([FromBody] EditorialDto entityModel)
		{
			var editorial = Mapper.Map<EditorialDto, Editorial>(entityModel);
			var editorialResult = Mapper.Map<Editorial, EditorialDto>(EditorialService.Set(editorial));
			return new OkObjectResult(editorialResult);
		}

		[HttpPut()]
		[ProducesResponseType(typeof(EditorialDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Put([FromBody] EditorialDto personModel)
		{
			var editorial = Mapper.Map<EditorialDto, Editorial>(personModel);
			var editorialResult = Mapper.Map<Editorial, EditorialDto>(EditorialService.Update(editorial));
			return new OkObjectResult(editorialResult);
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
