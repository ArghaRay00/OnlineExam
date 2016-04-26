using OnlineTestEntities;
using OnlineTestRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestBL
{
    public class CollegeManager
    {
        public void CreateCollege(College College)
        {
            using (var repository = new CommonRepository<College>())
            {
                repository.Create(College);
            }
        }
    }
}
