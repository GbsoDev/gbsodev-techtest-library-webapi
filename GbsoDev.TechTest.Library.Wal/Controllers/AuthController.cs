using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Mol;
using Microsoft.AspNetCore.Mvc;

namespace GbsoDev.TechTest.Library.Wal.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]
	public class authController : BaseController
	{
		private IAuthService AuthService => authService.Value;
		private readonly Lazy<IAuthService> authService;

		public authController(IServiceProvider serviceProvider, Lazy<IAuthService> authService) : base(serviceProvider)
		{
			this.authService = authService;
		}
		[HttpPost]
		[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Post([FromBody] AuthModel authModel)
		{
			var Auth = Mapper.Map<AuthModel, User>(authModel);

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
