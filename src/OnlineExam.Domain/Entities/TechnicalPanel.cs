using OnlineExam.Domain.Common;

namespace OnlineExam.Domain.Entities;

public class TechnicalPanel : BaseEntity
{
    public string Code { get; set; } = string.Empty;

    public ICollection<Employee> Members { get; set; } = [];
    public ICollection<Examination> Examinations { get; set; } = [];
}
