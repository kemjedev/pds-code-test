using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Web.Data;

public class ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options) : IdentityDbContext<ApplicationUser>(options)
{
}
