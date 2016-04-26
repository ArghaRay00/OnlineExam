using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineTestEntities
{
    public class Company
    {
        /// <summary>
        /// Name of the Company
        /// </summary>
        public String CompanyName{ get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        [Key]
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or Sets the list of HR
        /// </summary>
        public virtual ICollection<Hr> Hrs { get; set; }

        /// <summary>
        /// Gets or Sets the List of Employees
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }
   }
}
