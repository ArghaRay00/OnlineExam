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


        public IEnumerable<Location> GetAllLocations()
        {
            using (var locationRepo = new CommonRepository<Location>())
            {
                return locationRepo.GetAll();
            }
        }
    }
}
