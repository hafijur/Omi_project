using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalMvcProject.Models
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext() : base("FinalProjectApp")
        {
            
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
    }
}