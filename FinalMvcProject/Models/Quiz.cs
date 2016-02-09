using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalMvcProject.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }
        [Display(Name = "Quiz Name")]
        public string Name { get; set; }
       
        public int CourseId { get; set; }
        [Display(Name = "Course")]
        public virtual Course Course { get; set; }
    }
}