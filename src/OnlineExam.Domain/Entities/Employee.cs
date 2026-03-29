using OnlineExam.Domain.Common;

namespace OnlineExam.Domain.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;

    public ICollection<TechnicalPanel> TechnicalPanels { get; set; } = [];
}
