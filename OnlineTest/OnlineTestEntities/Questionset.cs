using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestEntities
{
    public class Questionset
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int QuestionSetId { get; set; }

        public string QuestionSetCode { get; set; }

        /// <summary>
        /// Gets or sets the List of Technical Questions
        /// </summary>
        public virtual ICollection<Question> Questions { get; set; }

        /// <summary>
        /// Gets or sets the list of Quantative Questions
        /// </summary>
  
        public bool IsAlreadyUsed { get; set; }
    }
}
