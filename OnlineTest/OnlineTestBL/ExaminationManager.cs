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

            if (Examination.QuestionsetId != null)
            {
                using (var repository = new CommonRepository<Questionset>())
                {
                    var questionSet = repository.Get(Examination.QuestionsetId.Value);
                    questionSet.IsAlreadyUsed = true;
                    repository.Update(questionSet);
                }
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

        public IEnumerable<Questionset> GetUnusedQuestionSet()
        {
            using (var questionsetRepo = new CommonRepository<Questionset>())
            {
                return questionsetRepo.GetAll().Where(qset => !qset.IsAlreadyUsed);
            }
        }



    }
}