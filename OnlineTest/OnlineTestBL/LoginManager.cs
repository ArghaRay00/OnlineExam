using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestRepository;
using OnlineTestEntities;
using System.Data.Entity;

namespace OnlineTestBL
{
   public class LoginManager
    {
       private ExamDbContext db = new ExamDbContext();
        public IEnumerable<User> GetloginUsers()
        {
            using (var LoginRepo = new CommonRepository<User>())
            {
                List<User> LoginUsers = db.Users.Include(q => q.Roles).ToList();
                return LoginUsers;
            }


        }


    }
}
