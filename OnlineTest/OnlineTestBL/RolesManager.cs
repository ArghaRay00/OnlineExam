using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestEntities;
using OnlineTestRepository;

namespace OnlineTestBL
{
    public class RolesManager
    {
        public IEnumerable<Roles> GetRoles()
        {
            using (var RolesRepo = new CommonRepository<Roles>())
            {
                return RolesRepo.GetAll();
            }
        }
    }
}
