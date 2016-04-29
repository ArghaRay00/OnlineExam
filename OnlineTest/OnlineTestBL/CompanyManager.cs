using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestEntities;
using OnlineTestRepository;
namespace OnlineTestBL
{
  public  class CompanyManager
    {

        public void CreateCompany(Company company)
        {
            using (var repository = new CommonRepository<Company>())
            {
                repository.Create(company);
            }
        }

        public IEnumerable<Company> GetAllCompany()
        {
            using (var repository = new CommonRepository<Company>())
            {
                return repository.GetAll().ToList();
            }
        }
    }

}

