using Carter;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Domain.Entities;
using OnlineExam.Domain.Enums;
using OnlineExam.Domain.Interfaces;
using OnlineExam.Infrastructure.Data;

namespace OnlineExam.API.Endpoints;

public record CreateExamRequest(
    string ExamCode,
    DateTime? ExamDate,
    double CutoffScore,
    int DurationMinutes,
    int QuestionSetId,
    int CollegeId,
    int LocationId
);

public record AssignPanelRequest(int TechnicalPanelId);

public class ExamEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exams").WithTags("Exam - Examinations").RequireAuthorization();

        group.MapGet("/", async (AppDbContext db) =>
            Results.Ok(await db.Examinations
                .Include(e => e.College)
                .Include(e => e.QuestionSet)
                .Include(e => e.TechnicalPanel)
                .ToListAsync()));

        group.MapGet("/{id:int}", async (int id, AppDbContext db) =>
        {
            var exam = await db.Examinations
                .Include(e => e.College)
                .Include(e => e.Location)
                .Include(e => e.QuestionSet).ThenInclude(q => q!.Questions)
                .Include(e => e.TechnicalPanel).ThenInclude(p => p!.Members)
                .Include(e => e.Students)
                .FirstOrDefaultAsync(e => e.Id == id);
            return exam is not null ? Results.Ok(exam) : Results.NotFound();
        });

        group.MapGet("/by-code/{code}", async (string code, AppDbContext db) =>
        {
            var exam = await db.Examinations
                .Include(e => e.College)
                .Include(e => e.QuestionSet).ThenInclude(q => q!.Questions)
                .FirstOrDefaultAsync(e => e.ExamCode == code);
            return exam is not null ? Results.Ok(exam) : Results.NotFound();
        });

        group.MapPost("/", async (CreateExamRequest request, AppDbContext db) =>
        {
            // Mark question set as used
            var questionSet = await db.QuestionSets.FindAsync(request.QuestionSetId);
            if (questionSet is null) return Results.BadRequest("Question set not found");
            if (questionSet.IsUsed) return Results.BadRequest("Question set is already used by another exam");

            questionSet.IsUsed = true;

            var exam = new Examination
            {
                ExamCode = request.ExamCode,
                ExamDate = request.ExamDate,
                CutoffScore = request.CutoffScore,
                DurationMinutes = request.DurationMinutes,
                QuestionSetId = request.QuestionSetId,
                CollegeId = request.CollegeId,
                LocationId = request.LocationId,
                Status = ExamStatus.Scheduled
            };

            db.Examinations.Add(exam);
            await db.SaveChangesAsync();

            return Results.Created($"/api/exams/{exam.Id}", exam);
        });

        group.MapPost("/{id:int}/assign-panel", async (int id, AssignPanelRequest request, AppDbContext db) =>
        {
            var exam = await db.Examinations.FindAsync(id);
            if (exam is null) return Results.NotFound();

            var panel = await db.TechnicalPanels.FindAsync(request.TechnicalPanelId);
            if (panel is null) return Results.BadRequest("Technical panel not found");

            exam.TechnicalPanelId = request.TechnicalPanelId;
            await db.SaveChangesAsync();

            return Results.Ok(exam);
        });
    }
}
