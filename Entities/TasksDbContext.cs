using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Entities
{
    public partial class TasksDbContext : DbContext
    {
        public TasksDbContext()
        {
        }

        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) 
        { 
        }

        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }
    }
}
