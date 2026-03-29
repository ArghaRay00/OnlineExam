using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Domain.Entities;

namespace OnlineExam.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.Email).HasMaxLength(255).IsRequired();
        builder.Property(u => u.Username).HasMaxLength(100).IsRequired();
        builder.Property(u => u.PasswordHash).HasMaxLength(512).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength(200);
        builder.Property(u => u.LastName).HasMaxLength(200);
        builder.Property(u => u.Role).HasConversion<string>().HasMaxLength(30);
    }
}

public class CollegeConfiguration : IEntityTypeConfiguration<College>
{
    public void Configure(EntityTypeBuilder<College> builder)
    {
        builder.Property(c => c.Name).HasMaxLength(300).IsRequired();
        builder.HasOne(c => c.Location).WithMany(l => l.Colleges).HasForeignKey(c => c.LocationId).OnDelete(DeleteBehavior.Restrict);
    }
}

public class ExaminationConfiguration : IEntityTypeConfiguration<Examination>
{
    public void Configure(EntityTypeBuilder<Examination> builder)
    {
        builder.HasIndex(e => e.ExamCode).IsUnique();
        builder.Property(e => e.ExamCode).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);
        builder.HasOne(e => e.College).WithMany(c => c.Examinations).HasForeignKey(e => e.CollegeId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Location).WithMany(l => l.Examinations).HasForeignKey(e => e.LocationId).OnDelete(DeleteBehavior.Restrict);
    }
}

public class TechnicalPanelConfiguration : IEntityTypeConfiguration<TechnicalPanel>
{
    public void Configure(EntityTypeBuilder<TechnicalPanel> builder)
    {
        builder.Property(t => t.Code).HasMaxLength(50).IsRequired();
        builder.HasMany(t => t.Members).WithMany(e => e.TechnicalPanels).UsingEntity("TechnicalPanelMembers");
    }
}

public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
{
    public void Configure(EntityTypeBuilder<ExamResult> builder)
    {
        builder.HasIndex(r => new { r.StudentId, r.ExaminationId }).IsUnique();
        builder.HasOne(r => r.Student).WithMany(s => s.Results).HasForeignKey(r => r.StudentId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(r => r.Examination).WithMany().HasForeignKey(r => r.ExaminationId).OnDelete(DeleteBehavior.Restrict);
    }
}
