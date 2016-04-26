using OnlineTestEntities;

namespace OnlineTestApp.Models
{
    public class AnswerChoice
    {
        public int AnswerChoiceId { get; set; }
        public string Choices { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }

    }
}