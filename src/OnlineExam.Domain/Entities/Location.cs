using OnlineExam.Domain.Common;

namespace OnlineExam.Domain.Entities;

public class Location : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;

    public ICollection<College> Colleges { get; set; } = [];
    public ICollection<Employee> Employees { get; set; } = [];
    public ICollection<Examination> Examinations { get; set; } = [];
}
