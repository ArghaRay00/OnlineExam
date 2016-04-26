using OnlineTestEntities;
using OnlineTestRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestBL
{
    public class ExaminationManager
    {
        public void CreateExamination(Examination Examination)
        {
            using (var repository = new CommonRepository<Examination>())
            {
                repository.Create(Examination);
            }
        }
        
        public IEnumerable<College> GetAllColleges()
        {
            using (var collegeRepo = new CommonRepository<College>())
            {
                return collegeRepo.GetAll();
            }
        }
        public IEnumerable<Technicalpanel> GetAllTechnicalpanels()
        {
            using (var technicalRepo = new CommonRepository<Technicalpanel>())
            {
                return technicalRepo.GetAll();
            }
        }

    }
}