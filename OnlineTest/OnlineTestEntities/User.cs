using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTestEntities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public String Username { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }

        public Boolean IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual List<Role> Roles { get; set; }


    }
}