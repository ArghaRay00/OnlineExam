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
        public virtual ICollection<Question> TechnicalQuestions { get; set; }

        /// <summary>
        /// Gets or sets the list of Quantative Questions
        /// </summary>
        public virtual ICollection<Question> QuantitativeQuestions { get; set; }

        /// <summary>
        /// Gets or Sets whether the Question Set is used or not
        /// </summary>
        public bool IsAlreadyUsed { get; set; }
    }
}
