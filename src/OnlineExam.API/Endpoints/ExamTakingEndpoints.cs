using Carter;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Exams.Commands;
using OnlineExam.Domain.Entities;
using OnlineExam.Infrastructure.Data;

namespace OnlineExam.API.Endpoints;

public record StudentRegistrationRequest(
    string Usn, string Name, DateTime DateOfBirth, string Email,
    string Address, string Phone, double Aggregate, double Percentage12th,
    string College12th, double Percentage10th, string School10th,
    int CollegeId, string ExamCode
);

public record SubmitExamRequest(List<AnswerSubmission> Answers);

public class ExamTakingEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exam-taking").WithTags("Student - Exam Taking");

        group.MapPost("/register", RegisterStudent);
        group.MapGet("/{examCode}/questions", GetQuestions);
        group.MapPost("/{examCode}/submit/{studentId:int}", SubmitExam);
    }

    private static async Task<IResult> RegisterStudent(StudentRegistrationRequest request, AppDbContext db)
    {
        var exam = await db.Examinations.FirstOrDefaultAsync(e => e.ExamCode == request.ExamCode);
        if (exam is null) return Results.BadRequest("Invalid exam code");

        var exists = await db.Students.AnyAsync(s => s.Usn == request.Usn && s.ExaminationId == exam.Id);
        if (exists) return Results.Conflict("Student already registered for this exam");

        var student = new Student
        {
            Usn = request.Usn, Name = request.Name, DateOfBirth = request.DateOfBirth,
            Email = request.Email, Address = request.Address, Phone = request.Phone,
            Aggregate = request.Aggregate, Percentage12th = request.Percentage12th,
            College12th = request.College12th, Percentage10th = request.Percentage10th,
            School10th = request.School10th, CollegeId = request.CollegeId,
            ExaminationId = exam.Id
        };

        db.Students.Add(student);
        await db.SaveChangesAsync();

        return Results.Created($"/api/exam-taking/students/{student.Id}",
            new { student.Id, student.Name, student.Usn, request.ExamCode });
    }

    private static async Task<IResult> GetQuestions(string examCode, AppDbContext db)
    {
        var exam = await db.Examinations
            .Include(e => e.QuestionSet).ThenInclude(q => q!.Questions)
            .FirstOrDefaultAsync(e => e.ExamCode == examCode);

        if (exam?.QuestionSet?.Questions is null) return Results.NotFound("Exam or questions not found");

        var questions = exam.QuestionSet.Questions.Select(q => new
        {
            q.Id, q.Text, q.OptionA, q.OptionB, q.OptionC, q.OptionD
        });

        return Results.Ok(new { exam.ExamCode, exam.DurationMinutes, TotalQuestions = questions.Count(), Questions = questions });
    }

    private static async Task<IResult> SubmitExam(string examCode, int studentId, SubmitExamRequest request, AppDbContext db)
    {
        var exam = await db.Examinations
            .Include(e => e.QuestionSet).ThenInclude(q => q!.Questions)
            .FirstOrDefaultAsync(e => e.ExamCode == examCode);

        if (exam?.QuestionSet?.Questions is null) return Results.NotFound("Exam not found");
        if (await db.Students.FindAsync(studentId) is null) return Results.NotFound("Student not found");
        if (await db.ExamResults.AnyAsync(r => r.StudentId == studentId && r.ExaminationId == exam.Id))
            return Results.Conflict("Exam already submitted");

        var grading = ExamGradingService.Grade(exam.QuestionSet.Questions, request.Answers, exam.CutoffScore);

        db.ExamResults.Add(new ExamResult
        {
            StudentId = studentId, ExaminationId = exam.Id,
            Score = grading.Score, TotalQuestions = grading.TotalQuestions,
            Passed = grading.Passed, SubmittedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        return Results.Ok(new { grading.Score, grading.TotalQuestions, grading.Passed, exam.CutoffScore });
    }
}
