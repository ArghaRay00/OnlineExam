using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestEntities
{
   public class Student
    {
        [Key]
        public int StudentId { get; set; }

       public string StudentName { get; set; }
        public string StudentUsn  { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StudentDob { get; set; }

        public string StudentEmail { get; set; }
        public string StudentAddr { get; set; }
        public string StudentMob { get; set; }
        public double StudentAgg { get; set; }
        public double StudentPer12th { get; set; }
        public string StudentCollege12th { get; set; }
        public double StudentPer10th { get; set; }
        public string StudentSchool { get; set; }

       public int? MarksId { get; set; }

        [ForeignKey("MarksId")]
        public virtual Marks Marks { get; set; }

        public int CollegeId { get; set; }

       [ForeignKey("CollegeId")]
        public virtual College College { get; set; }


       public int? ExamId { get; set; }

       [ForeignKey("ExamId")]
        public virtual Examination Exam { get; set; }
   }

}

