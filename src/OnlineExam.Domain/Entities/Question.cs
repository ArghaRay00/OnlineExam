using OnlineExam.Domain.Common;

namespace OnlineExam.Domain.Entities;

public class Question : BaseEntity
{
    public string Text { get; set; } = string.Empty;
    public string OptionA { get; set; } = string.Empty;
    public string OptionB { get; set; } = string.Empty;
    public string OptionC { get; set; } = string.Empty;
    public string OptionD { get; set; } = string.Empty;
    public int CorrectOption { get; set; } // 1=A, 2=B, 3=C, 4=D

    public int QuestionSetId { get; set; }
    public QuestionSet QuestionSet { get; set; } = null!;
}
