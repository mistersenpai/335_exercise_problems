//using Microsoft.AspNetCore.Authentication;
//using L14_ex.Data;
//using Microsoft.Extensions.Options;
//using System.Text.Encodings.Web;
//using System.Net.Http.Headers;
//using System.Text;

//namespace L14_ex.Handlers
//{
//    public class AdminHandler: AuthenticationHandler<AuthenticationSchemeOptions>
//    {
//        private readonly IL14Repo _repository;

//        public AdminHandler(IL14Repo repository, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock): base(options, logger, encoder, clock)
//        { 
//             _repository = repository; 
//        }

//        protected override async Task<AuthenticateResult> HandleAuthenticationAsync()
//        {
//            if (!Request.Headers.ContainsKey("Authorization"))
//            {
//                Response.Headers.Add("WWW-Authenticate", "Basic");
//                return AuthenticateResult.Fail("Authorization header not found");
//            }
//            else
//            {
//                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
//                var credentialBy = Convert.FromBase64String(authHeader.Parameter);
//                var credentials = Encoding.UTF8.GetString(credentialBy).Split(":");
//                var username = credentials[0];
//                var password = credentials[1];

//                if (_repository)
//            }
//        }
    

    
//    }
//}
