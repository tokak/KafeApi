using Microsoft.AspNetCore.Identity;

namespace KafeApi.Persistence.Context.Identity
{
    public class AppIdentityUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
