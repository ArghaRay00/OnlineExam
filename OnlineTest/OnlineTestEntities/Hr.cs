using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestEntities
{
    public class Hr
    {
        /// <summary>
        /// Name of Hr
        /// </summary>
        public String HrName { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int HrId { get; set; }


        public int CompanyId { get; set; }

        /// <summary>
        /// Company of HR
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
  
    }
}
