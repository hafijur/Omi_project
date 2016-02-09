using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FinalMvcProject.DAL;
using FinalMvcProject.Models;

namespace FinalMvcProject.Controllers
{
    public class QuestionsController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();
        private int a;

        public ActionResult GetQuestions(int quizId)
        {
            var question = db.Questions.Where(x => x.QuizId == quizId);
            var list = new List<dynamic>();
            foreach (var item in question)
            {
                list.Add(new { QuestionFor = item.QuestionFor, First = item.First,Second=item.Second,Third=item.Third,Forth=item.Forth,Answer=item.Answer });
            }
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public ActionResult QuizNumber()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuizNumber(int? number)
        {
            TempData["number"] = number;
            return RedirectToAction("CreateQuiz");
        }
        public ActionResult GetQuiz(int courseId)
        {
            var quiz = db.Quizes.Where(x => x.CourseId == courseId);
            var list = new List<dynamic>();
            foreach (var item in quiz)
            {
              list.Add(new {Id=item.QuizId,Name=item.Name});  
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCourses(int id)
        {
            var courses = db.Courses.Where(x=>x.DepartmentId==id);
            var courselist = new List<dynamic>();
            foreach (var item in courses)
            {
                courselist.Add(new {CourseId = item.CourseId, Name = item.Name});
            }
            return Json(courselist,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetResult(string[] Answer,int id)
        {
            int cournt = 0;
            var chech = db.Questions.Find(id);
            for (int i = 0; i < 3; i++)
            {
                if (chech.Answer==Answer[i])
                {
                    cournt++;
                }
            }
            return null;
        }


        public ActionResult InsertQuiz(Quiz q)
        {

            db.Quizes.Add(q);
            db.SaveChanges();
            var b = db.Quizes.ToList();
            int c = b.Count;
            a= b[c-1].QuizId;
            
           
            TempData["message"] = "Successfully added";
            return Json(a);
        }
        public ActionResult Test(Question[] ob,int id)
        {
            foreach (var item in ob)
            {
                item.QuizId = id;
                db.Questions.Add(item);
                db.SaveChanges();
            }
            


            return RedirectToAction("CreateQuiz");

        }

        public ActionResult CheckDuplicate(string name)
        {
            bool found = false;
            var a = db.Quizes.ToList();
            foreach (var quiz in a)
            {
                if (quiz.Name.Equals(name))
                {
                    found = true;
                    break;
                }
            }
            return Json(found);
        }
        public ActionResult CreateQuiz()
        {
            ViewBag.Department = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.number = TempData["number"];
            return View();
            
        }
        public ActionResult Exam2()
        {

            ViewBag.Department = new SelectList(db.Departments, "DepartmentId", "Name");
            return View();
        }

        public ActionResult LoadQuestions(int id)
        {
            var a = db.Quizes.Find(id);
            
            return RedirectToAction("Exam2",a);
           
        }

       

        public ActionResult FormView()
        {
            return View();
        }
        // GET: Questions

    

        public ActionResult Exam()
        {
            return View();
        }

    
       

        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormView([Bind(Include = "QuestionId,QuestionFor,First,Second,Third,Forth,Answer")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionId,QuestionFor,First,Second,Third,Forth,Answer")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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
