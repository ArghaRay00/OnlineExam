using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace OnlineTestEntities
{
    public class Question
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int QuestionId { get; set; }

        public string QuestionName { get; set; }

        /// <summary>
        /// OPtion 1
        /// </summary>
        public String Option1 { get; set; }

        /// <summary>
        /// Option 2
        /// </summary>
        public String Option2 { get; set; }

        /// <summary>
        /// Option 3
        /// </summary>
        public String Option3 { get; set; }

        /// <summary>
        /// Option 4
        /// </summary>
        public String Option4 { get; set; }

        /// <summary>
        /// Correct Option 
        /// </summary>
        public int AnswerKey { get; set; }

        public int QuestionSetId { get; set; }
       
       

       
     

       }
}
