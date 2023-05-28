using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace TodoList.Models.Context
{
    public class TodoContext : IdentityDbContext
    {



        public TodoContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        public DbSet<TodoTask> TodoTasks { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoUser> TodoUsers{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoList>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<TodoTask>().Property(x => x.Id).ValueGeneratedOnAdd();

            // call the base if you are using Identity service.
            // Important
            builder.Entity<TodoList>()
    .HasMany(w => w.TodoTasks)
    .WithOne(w => w.TodoList)
    .HasForeignKey(w => w.TodoListId);
            
            builder.Entity<TodoList>()
    .HasOne(w => w.User)
    .WithMany(w => w.TodoList)
    .HasForeignKey(w => w.UserId);
            // Code here ...
            builder.Entity<TodoList>().HasKey(k => k.Id);
            builder.Entity<TodoTask>().HasKey(k => k.Id);
            base.OnModelCreating(builder);

        }
    }
}
