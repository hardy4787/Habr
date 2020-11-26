using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthProject.Helpers
{
    public class AuthOptions
    {
        public string Secret { get; set; }
        public SymmetricSecurityKey GetSymetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
