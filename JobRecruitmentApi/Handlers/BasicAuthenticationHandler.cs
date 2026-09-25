using System.Security.Claims;
using System.Text;
using JobRecruitmentApi.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace JobRecruitmentApi.Handlers;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserLookup _userLookup;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IUserLookup userLookup)
        : base(options, logger, encoder)
    {
        _userLookup = userLookup;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            return AuthenticateResult.NoResult();
        }

        if (!authorizationHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.NoResult();
        }

        var encodedCredentials = authorizationHeader["Basic ".Length..].Trim();
        
        string credentials;

        try
        {
            var credentialBytes = Convert.FromBase64String(encodedCredentials);
            credentials = Encoding.UTF8.GetString(credentialBytes);
        }
        catch (FormatException)
        {
            return AuthenticateResult.Fail("Invalid Basic authentication header.");
        }

        var separatorIndex = credentials.IndexOf(':');
        
        if (separatorIndex <= 0)
        {
            return AuthenticateResult.Fail("Invalid Basic authentication credentials.");
        }

        var userName = credentials[..separatorIndex];

        var password = credentials[(separatorIndex + 1)..];

        var user = await _userLookup.FindByCredentialsAsync(userName, password);

        if (user is null)
        {
            return AuthenticateResult.Fail("Invalid username or password.");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);

        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        
        return AuthenticateResult.Success(ticket);
    }
}