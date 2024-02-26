using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;

namespace SampleCoreApi.Entities
{
    [MultiTenant]
    public class MyAppUser : IdentityUser<Guid>
    {
        public string CustomTag { get; set; }
    }
}
