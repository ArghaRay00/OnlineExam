using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestEntities;
using OnlineTestRepository;


namespace OnlineTestBL
{
    public class QuestionSetManager
    {
        public void CreateQuestion(Question question)
        {
            {
                using (var repository = new CommonRepository<Question>())
                {
                    repository.Create(question);
                }
            }

        }
        public void ModifyQuestion(Question question)
        {
            {
                using (var repository = new CommonRepository<Question>())
                {
                    repository.Update(question);
                }
            }

        }
        public void DeleteQuestion(Question question)
        {
            {
                using (var repository = new CommonRepository<Question>())
                {
                    repository.Delete(question);
                }
            }

        }

        /// <summary>
        /// manages the question set add operation
        /// </summary>
        /// <param name="questionset"></param>
        public void CreateQuestionSet(Questionset questionset)
        {
            {
                using (var repository = new CommonRepository<Questionset>())
                {
                    repository.Create(questionset);
                }
            }

        }
        /// <summary>
        /// manages the question set modify operation
        /// </summary>
        /// <param name="questionset"></param>
        public void ModifyQuestionSet(Questionset questionset)
        {
            {
                using (var repository = new CommonRepository<Questionset>())
                {
                    repository.Update(questionset);
                }
            }

        }
        /// <summary>
        /// manages the question set delete operation 
        /// </summary>
        /// <param name="questionset"></param>
        public void DeleteQuestionSet(Questionset questionset)
        {
            {
                using (var repository = new CommonRepository<Questionset>())
                {
                    repository.Delete(questionset);
                }
            }

        }
        public IEnumerable<Questionset> GetAllQuestionSet()
        {
            using (var QuestionSetAssign = new CommonRepository<Questionset>())
            {
                return QuestionSetAssign.GetAll();
            }
        }
        public IEnumerable<Question> GetAllQuestions()
        {
            using (var QuestionAssign = new CommonRepository<Question>())
            {
                return QuestionAssign.GetAll();
            }
        }

        public Question GetallQuestion(int id)
        {
            using (var QuestionAssign = new CommonRepository<Question>())
            {
                return QuestionAssign.Get(id);
            }
        }

        public Questionset GetparticularQuestionSet(int id)
        {
            using (var QuestionAssign = new CommonRepository<Questionset>())
            {
                return QuestionAssign.Get(id);
            }
        }

        public void UpdateStudentQuestionsList(StudentQuestions sq) {
            {
                using (var repository = new CommonRepository<StudentQuestions>())
                {
                    repository.Update(sq);
                }
            }
        }
    }
}
