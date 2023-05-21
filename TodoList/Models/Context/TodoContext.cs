using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Models.Context
{
	public class TodoContext : IdentityDbContext
	{



		public TodoContext(DbContextOptions dbContextOptions)
		: base(dbContextOptions)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			// call the base if you are using Identity service.
			// Important
			base.OnModelCreating(builder);

			// Code here ...
		}
	}
}
