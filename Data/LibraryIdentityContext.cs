using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ana_Stefan_Lab2.Data;

public class LibraryIdentityContext(DbContextOptions<LibraryIdentityContext> options) : IdentityDbContext<ApplicationUser>(options)
{
}
