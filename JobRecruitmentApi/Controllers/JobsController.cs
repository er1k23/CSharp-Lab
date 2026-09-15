using Microsoft.AspNetCore.Mvc;

namespace JobRecruitmentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetJobs()
    {
        return Ok("Jobs endpoint works!");
    }
}