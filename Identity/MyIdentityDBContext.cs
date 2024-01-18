using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace Portal_MovilEsales_API.Identity
{
    public class MyIdentityDBContext : IdentityDbContext
    {
        public MyIdentityDBContext(DbContextOptions<MyIdentityDBContext> options)
            : base(options) 
        { 
        
        }
    }
}
