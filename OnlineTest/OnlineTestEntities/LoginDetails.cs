using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestEntities
{
    public class LoginDetails
    {
        [Key]
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
