using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalMvcProject.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionFor { get; set; }
        public string First { get; set; }
        public string Second { get; set; }
        public string Third { get; set; }
        public string Forth { get; set; }
        public string Answer { get; set; }
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        
        


    }
}