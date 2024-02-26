using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;

namespace SampleCoreApi.Entities
{
    [MultiTenant]
    public class MyAppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
