using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace OnlineTestApp.Models
{
    public class ExamDetails
    {
        
        public int ExaminationId { get; set; }

        /// <summary>
        /// Date of Exam
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? ExaminationDate { get; set; }

        /// <summary>
        /// Cutoff Marks
        /// </summary>
        public double ExamCutoff { get; set; }

        /// <summary>
        /// Duration of Exam in minutes
        /// </summary>
        public string ExamDuration { get; set; }





        public int? TechnicalPanleId { get; set; }
      

        /// <summary>
        /// Technical Panel for the Exam
        /// </summary 



        public int? QuestionsetId { get; set; }
       
        /// <summary>
        /// Question Set for the Exam
        /// </summary>
    }
}