using OnlineTestEntities;
using OnlineTestRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestBL
{
   public class EmployeeManager
    {
        public void CreateEmployee(Employee Employee)
        {
            using (var repository = new CommonRepository<Employee>())
            {
                repository.Create(Employee);
            }
        }

        public IEnumerable<Company> GetAllCompanys()
        {
            using (var companyRepo = new CommonRepository<Company>())
            {
                return companyRepo.GetAll();
            }
        }
        public void CreateCompany(Company company)
        {
            using (var repository = new CommonRepository<Company>())
            {
                repository.Create(company);
            }
        }


        public IEnumerable<Location> GetAllLocations()
        {
            using (var locationRepo = new CommonRepository<Location>())
            {
                return locationRepo.GetAll();
            }
        }

       public IEnumerable<Employee> GetEmployeesBylocation(int locationId)
       {
           using (var locationRepo = new EmployeeTechnicalPanelRepo())
           {
               var list = locationRepo.GetAllEmployees();

               if (list.Any())
               {
                   return list.Where(e => e.LocationId == locationId).ToList();
               }
               return new List<Employee>();
           }
       }
    }
}
