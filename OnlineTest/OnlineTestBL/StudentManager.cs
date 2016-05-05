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
        public Student CreateStudent(Student student)
        {
            using (var repository = new CommonRepository<Student>())
            {
              return repository.CreateAndGet(student);
            }
        }

        public IEnumerable<College> GetAllColleges()
        {
            using (var collegeRepo = new CommonRepository<College>())
            {
                return collegeRepo.GetAll();
            }
        }

        public Student GetStudent(int studentID)
        {
            using (var repository = new CommonRepository<Student>())
            {
                return repository.Get(studentID);
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var repository = new CommonRepository<Student>())
            {
                repository.Update(student);
            }
        }

        public void UpdateStudentsMarks(int studentID, int marksId, int examinationId)
        {
            using (var repository = new CommonRepository<Student>())
            {
                var student = repository.Get(studentID);
                student.MarksId = marksId;
                student.ExamId = examinationId;
                repository.Update(student);
            }
        }
    }
}
