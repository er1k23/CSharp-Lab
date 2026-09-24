using System.ComponentModel.DataAnnotations;

namespace JobRecruitmentApi.Contracts.Auth;

public class RegisterRequest
{

    [Required] 
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
    
}