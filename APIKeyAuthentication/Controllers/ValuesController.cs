using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIKeyAuthentication.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "ApiKeyPolicy")]
    public IActionResult GetValue()
    {
        return Ok("Policy Based Api");
    }
}
