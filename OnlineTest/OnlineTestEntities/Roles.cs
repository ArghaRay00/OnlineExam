using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestEntities
{
    public class Roles
    {
        [Key]
        public int RolesID { get; set; }
        public string RolesName { get; set; }
    }
}
