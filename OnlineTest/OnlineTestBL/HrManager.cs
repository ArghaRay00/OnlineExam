using OnlineTestEntities;
using OnlineTestRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestBL
{
    public class HrManager
    {
        public void CreateHr(Hr Hr)
        {
            using (var repository = new CommonRepository<Hr>())
            {
                repository.Create(Hr);
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
            using(var repository=new CommonRepository<Company>())
            {
                repository.Create(company);
            }
        }
    }
}
