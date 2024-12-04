using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace CrudOperations.Authenticaion
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
           if(!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }
            var authheader = Request.Headers["Authorization"].ToString();
            if(!authheader.StartsWith("Basic ",StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult<AuthenticateResult>(AuthenticateResult.Fail("Invalid Schema"));
            }

            var encoding = authheader["Basic".Length..];
            var decoding = Encoding.UTF8.GetString(Convert.FromBase64String(encoding));
            var usernameandpass = decoding.Split(':');
            if (usernameandpass[0] != "admin" || usernameandpass[1] != "password")
            {

                return Task.FromResult(AuthenticateResult.Fail("Invalid Username and Pass"));

            }
            var identity = new ClaimsIdentity(new Claim[] {
             new Claim(ClaimTypes.Name,usernameandpass[0]),
             new Claim(ClaimTypes.NameIdentifier,"1")
            
            
            
            }

            ,"Basic");

            var principle= new ClaimsPrincipal(identity);
            var ticket=new AuthenticationTicket(principle,"Basic");
            return Task.FromResult(AuthenticateResult.Success(ticket));





        }
    }



}
