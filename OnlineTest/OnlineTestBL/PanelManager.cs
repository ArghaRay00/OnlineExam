using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestEntities;
using OnlineTestRepository;
namespace OnlineTestBL
{
   public class PanelManager
   {
       private readonly EmployeeTechnicalPanelRepo commonRepo;

       public PanelManager()
       {
           commonRepo=new EmployeeTechnicalPanelRepo();
       }

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

       //public void CreateTechnicalPanel(Technicalpanel panel)
       // {
       //     using (var repository = new CommonRepository<Technicalpanel>())
       //     {
       //         repository.Create(panel);
       //     }
       // }

       public Technicalpanel CreateAndGetTechnicalPanel(Technicalpanel panel)
       {
               return commonRepo.Create(panel);
       }

       public IEnumerable<Employee> GetEmployeesBylocation(int locationId)
       {
               var list = commonRepo.GetAllEmployees();

               if (list.Any())
               {
                   return list.Where(e => e.LocationId == locationId).ToList();
               }
               return new List<Employee>();
       }
    }
}
