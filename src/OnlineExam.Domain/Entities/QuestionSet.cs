using OnlineExam.Domain.Common;

namespace OnlineExam.Domain.Entities;

public class QuestionSet : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public bool IsUsed { get; set; }

    public ICollection<Question> Questions { get; set; } = [];
}
