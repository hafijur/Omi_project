using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using FinalMvcProject.Models;

namespace FinalMvcProject.Controllers
{
    public class FilesController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public JsonResult LoadCourses(int? id)
        {
            var courses = db.Courses.Include(x => x.Department).Where(x => x.DepartmentId == id);
            return Json(courses, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadFiles(int id)
        {
            var files = db.Files.Where(x => x.CourseId == id).ToList();
            var b = new List<dynamic>();

            foreach (var item in files)
            {
                b.Add(new { Name = item.Name, Type = item.Type, Size = item.Size, Path = item.Path, CourseId = item.CourseId, DepartmentId = item.DepartmentId });
            }
            return Json(b, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FileView()
        {

            ViewBag.Courses = new SelectList(db.Courses, "CourseId", "Name");
            return View();
        }


        // GET: Files
        public ActionResult UpLoad(HttpPostedFileBase file, int CourseId)
        {

            var ab = new File();
            ab.Path = Server.MapPath("~/files/" + file.FileName);
            ab.Name = file.FileName;
            ab.Type = file.ContentType;
            ab.Size = file.ContentLength;
            ab.CourseId = CourseId;
            //var seperator=ab.Type.Split('/');
            //string t = seperator[0];

            file.SaveAs(ab.Path);
            db.Files.Add(ab);
            db.SaveChanges();
            TempData["message"] = "File Uploaded ";
            ViewBag.path = ab.Path;
            return RedirectToAction("Create");
        }
        // C:\Users\Hafij\Desktop\FinalMvcProject\FinalMvcProject\files\
        public FileResult Download(string name)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\Keya\Desktop\FinalMvcProject\FinalMvcProject\files\" + name);
            //string fileName = "Screenshot_2.png";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
        public ActionResult Index()
        {
            var files = db.Files.Include(f => f.Course);
            return View(files.ToList());
        }

        // GET: Files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name");
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FileId,Name,Type,Size,Path,CourseId")] File file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Files.Add(file);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", file.CourseId);
        //    return View(file);
        //}

        // GET: Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", file.CourseId);
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileId,Name,Type,Size,Path,CourseId")] File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", file.CourseId);
            return View(file);
        }

        // GET: Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            File file = db.Files.Find(id);
            db.Files.Remove(file);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
