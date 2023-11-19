using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace finalTaskItra.Controllers
{
    public class AuthOptions
    {
        private readonly IConfiguration _config;
        public AuthOptions(IConfiguration config)
        {
            _config = config;
        }
        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Auth"]));
    }
}
