using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalMvcProject.Models;
using WebGrease.Css.Extensions;

namespace FinalMvcProject.Controllers
{
    public class DepartmentCourse
    {
        public string Name { get; set; }
        public List<AllowAnonymousAttribute> SubCourse { get; set; }
    }
    public class CourseNameId
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class CourseController : Controller
    {
       private UniversityDbContext db = new UniversityDbContext();



       public ActionResult DepartmentWiseCourse()
       {
          var dept= db.Departments.ToList();
          var total = new List<dynamic>();
          foreach (var item in dept)
          {
              var b = db.Courses.Where(y => y.DepartmentId == item.DepartmentId).Select(x => new { x.Name, x.CourseId }).ToList();
              var a = new {item.Name,b };
             total.Add(a);
          }
           return Json(total, JsonRequestBehavior.AllowGet);
       }
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Department).Include(c => c.Semester);
            return View(courses.ToList());
        }
        public ActionResult LoadAllCourses()
        {
            var courses = db.Courses;
            var list = new List<dynamic>();
            foreach (var course in courses)
            {
                list.Add(new { Id = course.CourseId, Name = course.Name });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hafijur()
        {
            var list = db.Courses.Include(c => c.Department).Include(c => c.Semester).ToList();

            return PartialView("~/Views/Shared/_AllCourseName.cshtml", list);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CourseId,Code,Name,Credit,Description,DepartmentId,SemesterId")] Course course)
        {
            course.Status = true;
            var list = from p in db.Semesters where p.SemesterId == course.SemesterId select p;
            foreach (var item in list)
            {
                course.SemName = item.Name;
            }
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                TempData["Success"] = "Course Added Successfully!!";
                return RedirectToAction("Create");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name", course.SemesterId);
            return View(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult IsUserCodeAvailable(string code)
        {
            var course = db.Courses.FirstOrDefault(x => x.Code == code);
            if (course != null)
            {
                return Json("Course Code is already registered ", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IsUserNameAvailable(string name)
        {
            var course = db.Courses.FirstOrDefault(x => x.Name == name);
            if (course != null)
            {
                return Json("Course Name is already registered ", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
