using System.Security.Claims;

namespace JobRecruitmentApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        
        return int.Parse(userIdClaim!.Value);
    }
}