using OnlineExam.Domain.Common;

namespace OnlineExam.Domain.Entities;

public class College : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;

    public ICollection<Student> Students { get; set; } = [];
    public ICollection<Examination> Examinations { get; set; } = [];
}
