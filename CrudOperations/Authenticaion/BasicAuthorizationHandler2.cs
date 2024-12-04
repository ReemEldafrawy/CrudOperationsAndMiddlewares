using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CrudOperations.Authenticaion
{
    public class BasicAuthorizationHandler2 : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthorizationHandler2(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {



        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var autheader))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid schema"));
            }
            if (!autheader.Scheme.Equals("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid schema"));
            }


            var encoding = autheader.Parameter;
            var decoding = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encoding));
            var usernameandpassword = decoding.Split(':');
            if (usernameandpassword[0] != "admin" || usernameandpassword[1] != "password")
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalod pass word and user name"));
            }
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name,usernameandpassword[0]),
                new Claim(ClaimTypes.NameIdentifier,"1")
            });
            var priciple= new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(priciple, "Basic ");
            return Task.FromResult(AuthenticateResult.Success(ticket));










        }
    }
}
