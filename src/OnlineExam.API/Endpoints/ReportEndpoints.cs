using Carter;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Data;

namespace OnlineExam.API.Endpoints;

public class ReportEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/reports").WithTags("Reports").RequireAuthorization();

        // Results by exam code — ranked by score descending
        group.MapGet("/results/{examCode}", async (string examCode, AppDbContext db) =>
        {
            var exam = await db.Examinations.FirstOrDefaultAsync(e => e.ExamCode == examCode);
            if (exam is null) return Results.NotFound("Exam not found");

            var results = await db.ExamResults
                .Where(r => r.ExaminationId == exam.Id)
                .Include(r => r.Student)
                .OrderByDescending(r => r.Score)
                .Select(r => new
                {
                    r.Student.Name,
                    r.Student.Usn,
                    r.Student.Email,
                    r.Score,
                    r.TotalQuestions,
                    Percentage = r.TotalQuestions > 0 ? Math.Round((double)r.Score / r.TotalQuestions * 100, 1) : 0,
                    r.Passed,
                    r.SubmittedAt
                })
                .ToListAsync();

            return Results.Ok(new
            {
                exam.ExamCode,
                exam.ExamDate,
                exam.CutoffScore,
                TotalStudents = results.Count,
                PassedCount = results.Count(r => r.Passed),
                Results = results
            });
        });

        // Students by college — sorted by name
        group.MapGet("/students/{collegeId:int}", async (int collegeId, AppDbContext db) =>
        {
            var college = await db.Colleges.FindAsync(collegeId);
            if (college is null) return Results.NotFound("College not found");

            var students = await db.Students
                .Where(s => s.CollegeId == collegeId)
                .Include(s => s.Results)
                .OrderBy(s => s.Name)
                .Select(s => new
                {
                    s.Name,
                    s.Usn,
                    s.Email,
                    s.DateOfBirth,
                    s.Phone,
                    s.Aggregate,
                    ExamsAttempted = s.Results.Count,
                    AverageScore = s.Results.Any() ? Math.Round(s.Results.Average(r => (double)r.Score / r.TotalQuestions * 100), 1) : 0
                })
                .ToListAsync();

            return Results.Ok(new
            {
                CollegeName = college.Name,
                TotalStudents = students.Count,
                Students = students
            });
        });
    }
}
