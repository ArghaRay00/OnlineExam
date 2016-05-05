using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineTestApp.Models
{
    public class StudentRegistration
    {
        [DisplayName("USN")]
        public string StudentUsn { get; set; }

        [DisplayName("Name")]
        public string StudentName { get; set; }
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

        public int CollegeID { get; set; }
        public string ExamCode { get; set; }
    }
}