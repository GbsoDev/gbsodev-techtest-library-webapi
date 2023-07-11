using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Mol;
using Microsoft.AspNetCore.Mvc;

namespace GbsoDev.TechTest.Library.Wal.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]
	public class AuthController : BaseController
	{
		private IAuthService AuthService;

		public AuthController(IServiceProvider serviceProvider, IAuthService authService) : base(serviceProvider)
		{
			this.AuthService = authService;
		}

		[HttpOptions]
		public IActionResult Options()
		{
			return NoContent();
		}

		[HttpPost]
		[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Post([FromBody] AuthModel authModel)
		{
			var Auth = Mapper.Map<AuthModel, Usuario>(authModel);

			var authResult = AuthService.ValidateLogin(Auth);
			if(authResult != null)
			{
				return new OkObjectResult(authResult);
			}else
			{
				return new BadRequestResult();
			}
		}
	}
}
