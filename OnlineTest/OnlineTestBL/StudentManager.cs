using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestEntities;
using OnlineTestRepository;

namespace OnlineTestBL
{
    /// <summary>
    /// Manages the students (eg. Add , remove and take exam)
    /// </summary>
    public class StudentManager
    {
        public void CreateStudent(Student student)
        {
            using (var repository = new CommonRepository<Student>())
            {
               repository.Create(student);
            }
        }

        public IEnumerable<College> GetAllColleges()
        {
            using (var collegeRepo = new CommonRepository<College>())
            {
                return collegeRepo.GetAll();
            }
        }
        
    }
}
