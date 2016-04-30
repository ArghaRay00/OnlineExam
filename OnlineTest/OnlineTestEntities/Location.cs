using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestEntities
{
   public class Location :IDomainModel
    {   [Key]
        public int LocationId { get; set; }

        [DisplayName("Location")]
        public string LocationName { get; set; }
        
        [DisplayName("State")]
        public string LocationState { get; set; }
        public virtual ICollection<Examination> Examinations { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<College> Colleges { get; set; }

        public int Id
        {
            get { return LocationId; }
        }
    }
}
