namespace JobRecruitmentApi.Models;

public class PatchJobRequest
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public decimal? Salary { get; set; }
}