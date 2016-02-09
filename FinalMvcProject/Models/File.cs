using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalMvcProject.Models
{
    public class File
    {
        public int FileId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Size { get; set; }
        public string Path { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public int DepartmentId { get; set; }
        

    }
}