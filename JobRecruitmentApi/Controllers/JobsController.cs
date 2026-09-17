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
    
    [HttpGet("{id}")]
    public IActionResult GetJob(int id)
    {
        return Ok($"Job with id {id}");
    }
}