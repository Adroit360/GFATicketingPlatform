using GFATicketing.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GFATicketing.Data.DbContext
{
    public class GFATicketingDbContext : IdentityDbContext<ApplicationUser>
    {
        public GFATicketingDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
