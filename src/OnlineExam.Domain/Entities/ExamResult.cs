using OnlineExam.Domain.Common;

namespace OnlineExam.Domain.Entities;

public class ExamResult : BaseEntity
{
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public bool Passed { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int ExaminationId { get; set; }
    public Examination Examination { get; set; } = null!;
}
