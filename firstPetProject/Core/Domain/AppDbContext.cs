using firstPetProject.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = firstPetProject.Core.Domain.Entities.Task;

namespace firstPetProject.Core.Domain
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
