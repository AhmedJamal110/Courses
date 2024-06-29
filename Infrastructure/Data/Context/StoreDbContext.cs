using Entity.Identity;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data.Context
{
    public class StoreDbContext : IdentityDbContext<AppUser>
    {

        public StoreDbContext( DbContextOptions<StoreDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Name = "Student", NormalizedName = "STUDENT" },
                    new IdentityRole { Name = "Instructor", NormalizedName = "INSTRUCTOR"});

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });


            modelBuilder.Entity<UserCourse>().HasKey(uc => new { uc.AppUserId, uc.CourseId });
            
            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.AppUser)
                .WithMany(u => u.Courses)
                .HasForeignKey(uc => uc.AppUserId);

            modelBuilder.Entity<UserCourse>()
               .HasOne(uc => uc.Course)
               .WithMany(u => u.UserCourses)
               .HasForeignKey(uc => uc.CourseId);

        }


        public DbSet<Course> Courses  { get; set; }
        public DbSet<Requerment> Requerments { get; set; }
        public DbSet<Learning>  Learnings { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Lecture> Lectures  { get; set; }


    }
}
