using Carter;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Domain.Entities;
using OnlineExam.Domain.Interfaces;
using OnlineExam.Infrastructure.Data;

namespace OnlineExam.API.Endpoints;

public record StudentRegistrationRequest(
    string Usn,
    string Name,
    DateTime DateOfBirth,
    string Email,
    string Address,
    string Phone,
    double Aggregate,
    double Percentage12th,
    string College12th,
    double Percentage10th,
    string School10th,
    int CollegeId,
    string ExamCode
);

public record ExamQuestionDto(int QuestionId, string Text, string OptionA, string OptionB, string OptionC, string OptionD);

public record SubmitAnswerItem(int QuestionId, int SelectedOption);
public record SubmitExamRequest(List<SubmitAnswerItem> Answers);
public record ExamResultDto(int Score, int TotalQuestions, bool Passed, double CutoffScore);

public class ExamTakingEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exam-taking").WithTags("Student - Exam Taking");

        // Register for an exam (public — students don't have JWT yet)
        group.MapPost("/register", async (StudentRegistrationRequest request, AppDbContext db) =>
        {
            var exam = await db.Examinations.FirstOrDefaultAsync(e => e.ExamCode == request.ExamCode);
            if (exam is null) return Results.BadRequest("Invalid exam code");

            var existingStudent = await db.Students.FirstOrDefaultAsync(s => s.Usn == request.Usn && s.ExaminationId == exam.Id);
            if (existingStudent is not null) return Results.Conflict("Student already registered for this exam");

            var student = new Student
            {
                Usn = request.Usn,
                Name = request.Name,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email,
                Address = request.Address,
                Phone = request.Phone,
                Aggregate = request.Aggregate,
                Percentage12th = request.Percentage12th,
                College12th = request.College12th,
                Percentage10th = request.Percentage10th,
                School10th = request.School10th,
                CollegeId = request.CollegeId,
                ExaminationId = exam.Id
            };

            db.Students.Add(student);
            await db.SaveChangesAsync();

            return Results.Created($"/api/exam-taking/students/{student.Id}", new { student.Id, student.Name, student.Usn, ExamCode = request.ExamCode });
        });

        // Get questions for exam (no answer keys — student view)
        group.MapGet("/{examCode}/questions", async (string examCode, AppDbContext db) =>
        {
            var exam = await db.Examinations
                .Include(e => e.QuestionSet).ThenInclude(q => q!.Questions)
                .FirstOrDefaultAsync(e => e.ExamCode == examCode);

            if (exam?.QuestionSet?.Questions is null) return Results.NotFound("Exam or questions not found");

            var questions = exam.QuestionSet.Questions.Select(q => new ExamQuestionDto(
                q.Id, q.Text, q.OptionA, q.OptionB, q.OptionC, q.OptionD
            )).ToList();

            return Results.Ok(new { exam.ExamCode, exam.DurationMinutes, TotalQuestions = questions.Count, Questions = questions });
        });

        // Submit answers and get auto-graded result
        group.MapPost("/{examCode}/submit/{studentId:int}", async (string examCode, int studentId, SubmitExamRequest request, AppDbContext db) =>
        {
            var exam = await db.Examinations
                .Include(e => e.QuestionSet).ThenInclude(q => q!.Questions)
                .FirstOrDefaultAsync(e => e.ExamCode == examCode);

            if (exam?.QuestionSet?.Questions is null) return Results.NotFound("Exam not found");

            var student = await db.Students.FindAsync(studentId);
            if (student is null) return Results.NotFound("Student not found");

            // Check if already submitted
            var existingResult = await db.ExamResults.FirstOrDefaultAsync(r => r.StudentId == studentId && r.ExaminationId == exam.Id);
            if (existingResult is not null) return Results.Conflict("Exam already submitted");

            // Auto-grade
            var answerMap = exam.QuestionSet.Questions.ToDictionary(q => q.Id, q => q.CorrectOption);
            var score = 0;
            foreach (var answer in request.Answers)
            {
                if (answerMap.TryGetValue(answer.QuestionId, out var correct) && answer.SelectedOption == correct)
                    score++;
            }

            var totalQuestions = exam.QuestionSet.Questions.Count;
            var passed = score >= exam.CutoffScore;

            var result = new ExamResult
            {
                StudentId = studentId,
                ExaminationId = exam.Id,
                Score = score,
                TotalQuestions = totalQuestions,
                Passed = passed,
                SubmittedAt = DateTime.UtcNow
            };

            db.ExamResults.Add(result);
            await db.SaveChangesAsync();

            return Results.Ok(new ExamResultDto(score, totalQuestions, passed, exam.CutoffScore));
        });
    }
}
