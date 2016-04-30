using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestEntities
{
    public class Employee : IDomainModel
    {
        /// <summary>
        /// ID of the Employee
        /// </summary>
        [Key]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Name of the Employee
        /// </summary>
        public String EmployeeName { get; set; }

        /// <summary>
        /// Address of the Employee
        /// </summary>
        public String EmployeeAddress { get; set; }

        /// <summary>
        /// Number of the Employee
        /// </summary>
        public int EmployeeNumber { get; set; }


        public int CompanyId { get; set; }

        /// <summary>
        /// Company of the Employee
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        public virtual ICollection<Technicalpanel> TechnicalPanels { get; set; }

        public int Id
        {
            get { return EmployeeId; }
        }
    }
}
 