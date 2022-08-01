using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API.Controllers
{
    public class AccountController : ControllerBase
    {
        [Authorize]
        [ApiController]
        [Route("[controller]")]
    }
}
