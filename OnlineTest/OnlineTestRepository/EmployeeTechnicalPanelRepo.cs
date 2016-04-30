using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestEntities;

namespace OnlineTestRepository
{
    public class EmployeeTechnicalPanelRepo :IDisposable
    {
        private readonly ExamDbContext context;

        public EmployeeTechnicalPanelRepo()
        {
            context=new ExamDbContext();
        }

        public Technicalpanel Create(Technicalpanel panel)
        {
            var newEntry = context.Technicalpanels.Add(panel);
            context.SaveChanges();
            return newEntry;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees.AsEnumerable<Employee>();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
