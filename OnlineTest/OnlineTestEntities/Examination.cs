using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;

namespace OnlineTestEntities
{
    public class Examination :IDomainModel
    {
        /// <summary>
        /// ID 
        /// </summary>
        [Key]
        public int ExaminationId { get; set; }

        /// <summary>
        /// Exam Code
        /// </summary>
        public string ExamCode { get; set; }

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
        /// </summary>
        [ForeignKey("TechnicalPanleId")]
        public virtual Technicalpanel Technicalpanel { get; set; }

        public int? QuestionsetId { get; set; }

        /// <summary>
        /// Question Set for the Exam
        /// </summary>
        [ForeignKey("QuestionsetId")]
        public virtual Questionset Questionset { get; set; }

        public int CollegeId { get; set; }

        [ForeignKey("CollegeId")]
        public virtual College College { get; set; }

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public int Id
        {
            get { return ExaminationId; }
        }
    }
}
