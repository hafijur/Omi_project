namespace FinalMvcProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false),
                        Credit = c.Double(nullable: false),
                        Description = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                        AssignTo = c.String(),
                        Status = c.Boolean(nullable: false),
                        SemName = c.String(),
                        Semester_SemesterId = c.Int(),
                        CourseSemister_SemesterId = c.Int(),
                        Semester_SemesterId1 = c.Int(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Semesters", t => t.Semester_SemesterId)
                .ForeignKey("dbo.Semesters", t => t.CourseSemister_SemesterId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.Semester_SemesterId1)
                .Index(t => t.DepartmentId)
                .Index(t => t.Semester_SemesterId)
                .Index(t => t.CourseSemister_SemesterId)
                .Index(t => t.Semester_SemesterId1);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        SemesterId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SemesterId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(),
                        ContactNo = c.String(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        CreditTaken = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: false)
                .Index(t => t.DesignationId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 7),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Size = c.Double(nullable: false),
                        Path = c.String(),
                        CourseId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionFor = c.String(),
                        First = c.String(),
                        Second = c.String(),
                        Third = c.String(),
                        Forth = c.String(),
                        Answer = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: false)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuizId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuizId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        RegistrationId = c.String(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        Teacher_TeacherId = c.Int(nullable: false),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_TeacherId, t.Course_CourseId })
                .ForeignKey("dbo.Teachers", t => t.Teacher_TeacherId, cascadeDelete: false)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: false)
                .Index(t => t.Teacher_TeacherId)
                .Index(t => t.Course_CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Questions", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Quizs", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Files", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Semester_SemesterId1", "dbo.Semesters");
            DropForeignKey("dbo.TeacherCourses", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.TeacherCourses", "Teacher_TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Teachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Courses", "CourseSemister_SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "Semester_SemesterId", "dbo.Semesters");
            DropIndex("dbo.TeacherCourses", new[] { "Course_CourseId" });
            DropIndex("dbo.TeacherCourses", new[] { "Teacher_TeacherId" });
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropIndex("dbo.Quizs", new[] { "CourseId" });
            DropIndex("dbo.Questions", new[] { "QuizId" });
            DropIndex("dbo.Files", new[] { "CourseId" });
            DropIndex("dbo.Teachers", new[] { "DepartmentId" });
            DropIndex("dbo.Teachers", new[] { "DesignationId" });
            DropIndex("dbo.Courses", new[] { "Semester_SemesterId1" });
            DropIndex("dbo.Courses", new[] { "CourseSemister_SemesterId" });
            DropIndex("dbo.Courses", new[] { "Semester_SemesterId" });
            DropIndex("dbo.Courses", new[] { "DepartmentId" });
            DropTable("dbo.TeacherCourses");
            DropTable("dbo.Students");
            DropTable("dbo.Quizs");
            DropTable("dbo.Questions");
            DropTable("dbo.Files");
            DropTable("dbo.Days");
            DropTable("dbo.Designations");
            DropTable("dbo.Departments");
            DropTable("dbo.Teachers");
            DropTable("dbo.Semesters");
            DropTable("dbo.Courses");
        }
    }
}
