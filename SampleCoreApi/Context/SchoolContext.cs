using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleCoreApi.Entities;

namespace SampleCoreApi.Context
{
    public class SchoolContext : MultiTenantIdentityDbContext<MyAppUser, MyAppRole, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SchoolContext(ITenantInfo tenantInfo, DbContextOptions<SchoolContext> options, IHttpContextAccessor httpContextAccessor) : base(tenantInfo, options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    } 
    
}
