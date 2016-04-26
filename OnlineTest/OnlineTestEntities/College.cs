using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestEntities
{
    public class College
    {
        [Key]
        public int CollegeId { get; set; }

        [DisplayName("Name")]
        public string CollegeName { get; set; }
        [DisplayName("Address")]
        public string CollegeAddr { get; set; }

        [DisplayName("Phone Number")]
        public int CollegePhno { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Examination> Examinations { get; set; }

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
    }
}