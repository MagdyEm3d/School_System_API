using Microsoft.EntityFrameworkCore;
using School_System_APII.Models;

namespace School_System_APII.Connection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Instractor> instractors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instractor>()
            .HasOne(i => i.Subject)
            .WithOne(s => s.Instractor)
            .HasForeignKey<Instractor>(i => i.SubjectId);
            modelBuilder.Entity<Instractor>().HasData
                (
                    new Instractor { InstractorId=1,InstractorName="Saber",Email="Saber@gmail.com",Salary=1000,Password="Saber@123",SubjectId=1}
                     
                );

            modelBuilder.Entity<Subject>().HasData
                (
                    new Subject { SubjectId=1,SubjectName="Arabic",Password="Arabic@123",InstractorId=1}
                    
                );
            modelBuilder.Entity<Student>().HasData
                (
                    new Student { StudentId=1,UserName="Magdy7",FullName="Magdy Emad",Email="Magdy@gmail.com",Password="Magdy@123",SubjectId=1,InstractorId=1}
                );
            base.OnModelCreating(modelBuilder);
        }


    }
}
