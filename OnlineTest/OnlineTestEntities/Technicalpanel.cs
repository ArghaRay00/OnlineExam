using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestEntities
{
    public class Technicalpanel :IDomainModel
    {

        public Technicalpanel()
        {
            this.Employees=new HashSet<Employee>();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int TechnicalpanelId { get; set; }

        public string TechnicalPanelCode { get; set; }

        /// <summary>
        /// Gets or Sets the List of Emplyees of Technical Panel
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }

        ///// <summary>
        ///// Exam to which the Technical Panel belongs to
        ///// </summary>
        //public virtual Examination Examination { get; set; }
        public int Id
        {
            get { return TechnicalpanelId; }
        }
    }
}
