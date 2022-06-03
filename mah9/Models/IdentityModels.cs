using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mah9.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string Class { get; set; }
        public string Password { get; set; }
        public int SeconID { get; set; }

        public string FullName { get; set; }

        public DateTime Date { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<ClassModel> Class_ { get; set; }
        public DbSet<SubjectModel> Subject_ { get; set; }
        public DbSet<VideoModel> Video_ { get; set; }
        public DbSet<PDFModel> PDF_ { get; set; }
        public DbSet<UsersCopyModel> UserCopy_ { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClassModel>().ToTable("Classes");
            modelBuilder.Entity<SubjectModel>().ToTable("Subjects");
            modelBuilder.Entity<VideoModel>().ToTable("Videos");
            modelBuilder.Entity<PDFModel>().ToTable("PDF");
            modelBuilder.Entity<UsersCopyModel>().ToTable("UserCopy");


        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}