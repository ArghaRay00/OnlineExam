using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestEntities;
using OnlineTestRepository;
namespace OnlineTestBL
{
   public class PanelManager
    {
        public IEnumerable<Examination> GetAllExam()
        {
            using (var repository = new CommonRepository<Examination>())
            {
                return repository.GetAll().ToList();
            }
        }
        public Examination GetExam(int examid)
        {
            using (var repository = new CommonRepository<Examination>())
            {
                return repository.Get(examid);
            }
        }
    }
}
