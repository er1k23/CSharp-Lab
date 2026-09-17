using Microsoft.AspNetCore.Mvc;
using JobRecruitmentApi.Models;
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
    public IActionResult GetJob([FromRoute] int id)
    {
        return Ok($"Job with id {id}");
    }
    
    [HttpGet("search")]
    public IActionResult SearchJobs([FromQuery] string? title)
    {
        return Ok($"Searching for: {title}");
    }

    [HttpPost]
    public IActionResult CreateJob([FromBody] CreateJobRequest request)
    {
        return Ok(request);
    }
}