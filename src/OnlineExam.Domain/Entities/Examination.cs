using OnlineExam.Domain.Common;
using OnlineExam.Domain.Enums;

namespace OnlineExam.Domain.Entities;

public class Examination : BaseEntity
{
    public string ExamCode { get; set; } = string.Empty;
    public DateTime? ExamDate { get; set; }
    public double CutoffScore { get; set; }
    public int DurationMinutes { get; set; }
    public ExamStatus Status { get; set; } = ExamStatus.Draft;

    public int? QuestionSetId { get; set; }
    public QuestionSet? QuestionSet { get; set; }

    public int? TechnicalPanelId { get; set; }
    public TechnicalPanel? TechnicalPanel { get; set; }

    public int CollegeId { get; set; }
    public College College { get; set; } = null!;

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;

    public ICollection<Student> Students { get; set; } = [];
}
