using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures
{
    public  class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled= true ;
        }

        public DbSet<User> User{ get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<FamilyTree> FamilyTree { get; set; }
        public DbSet<FamilyGroup> FamilyGroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
