using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("redirct/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class RedirectController : BaseController
    {
        [HttpGet]
        public ActionResult Error (int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
