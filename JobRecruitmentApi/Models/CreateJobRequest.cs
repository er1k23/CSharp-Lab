using System.ComponentModel.DataAnnotations;

namespace JobRecruitmentApi.Models;

public class CreateJobRequest
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;
    [Range(0,double.MaxValue)]
    public decimal Salary { get; set; }
}