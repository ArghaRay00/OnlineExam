using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineTestEntities
{
   public class StudentQuestions
    {
        [Key]
        public int StudentQuestionID { get; set; }

        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Questions { get; set; }

        public int AnswerKey { get; set; }
        [ForeignKey("AnswerKey")]
        public virtual Question QuestionAnswerKey { get; set; }


        public int? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Students { get; set; }

        public int IsAnswered { get; set; }
    }
}
