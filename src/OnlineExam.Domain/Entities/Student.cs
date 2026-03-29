using OnlineExam.Domain.Common;

namespace OnlineExam.Domain.Entities;

public class Student : BaseEntity
{
    public string Usn { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public double Aggregate { get; set; }
    public double Percentage12th { get; set; }
    public string College12th { get; set; } = string.Empty;
    public double Percentage10th { get; set; }
    public string School10th { get; set; } = string.Empty;

    public int CollegeId { get; set; }
    public College College { get; set; } = null!;

    public int? ExaminationId { get; set; }
    public Examination? Examination { get; set; }

    public ICollection<ExamResult> Results { get; set; } = [];
}
