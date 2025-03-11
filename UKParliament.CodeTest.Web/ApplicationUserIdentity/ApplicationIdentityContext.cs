using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Web.ApplicationUserIdentity;

public class ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options) : IdentityDbContext<ApplicationUser>(options)
{
}
