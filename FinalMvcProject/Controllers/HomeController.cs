using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalMvcProject.Models;
using File = FinalMvcProject.Models.File;

namespace FinalMvcProject.Controllers
{
    public class HomeController : Controller
    {
        UniversityDbContext db=new UniversityDbContext();
        [HttpPost]
       

       

        public void GetAllFile()
        {
            Json(db.Files.ToList());
        }

      
        public ActionResult Index()
        {
            return View();
        }

       
    }
}