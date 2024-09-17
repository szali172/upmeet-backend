using Microsoft.EntityFrameworkCore;
using UpmeetBackend.Models;

namespace UpmeetBackend.Data;

public class UpmeetDbContext : DbContext
{
    public UpmeetDbContext(DbContextOptions<UpmeetDbContext> options) : base(options) { }

    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<Favorite> Favorites { get; set; }
    public virtual DbSet<User> Users { get; set; }
}
