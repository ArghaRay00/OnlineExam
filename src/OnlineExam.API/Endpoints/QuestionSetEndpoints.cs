using Carter;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Domain.Entities;
using OnlineExam.Domain.Interfaces;
using OnlineExam.Infrastructure.Data;

namespace OnlineExam.API.Endpoints;

public class QuestionSetEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/question-sets").WithTags("Exam - Question Sets").RequireAuthorization();

        group.MapGet("/", async (IRepository<QuestionSet> repo) =>
            Results.Ok(await repo.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id, AppDbContext db) =>
        {
            var set = await db.QuestionSets
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(s => s.Id == id);
            return set is not null ? Results.Ok(set) : Results.NotFound();
        });

        group.MapPost("/", async (QuestionSet questionSet, IRepository<QuestionSet> repo) =>
        {
            var created = await repo.AddAsync(questionSet);
            return Results.Created($"/api/question-sets/{created.Id}", created);
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<QuestionSet> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            await repo.DeleteAsync(existing);
            return Results.NoContent();
        });

        // Questions within a set
        group.MapGet("/{setId:int}/questions", async (int setId, IRepository<Question> repo) =>
            Results.Ok(await repo.FindAsync(q => q.QuestionSetId == setId)));

        group.MapPost("/{setId:int}/questions", async (int setId, Question question, IRepository<Question> repo) =>
        {
            question.QuestionSetId = setId;
            var created = await repo.AddAsync(question);
            return Results.Created($"/api/question-sets/{setId}/questions/{created.Id}", created);
        });
    }
}

public class QuestionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/questions").WithTags("Exam - Questions").RequireAuthorization();

        group.MapGet("/{id:int}", async (int id, IRepository<Question> repo) =>
            await repo.GetByIdAsync(id) is { } question ? Results.Ok(question) : Results.NotFound());

        group.MapPut("/{id:int}", async (int id, Question updated, IRepository<Question> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            existing.Text = updated.Text;
            existing.OptionA = updated.OptionA;
            existing.OptionB = updated.OptionB;
            existing.OptionC = updated.OptionC;
            existing.OptionD = updated.OptionD;
            existing.CorrectOption = updated.CorrectOption;
            await repo.UpdateAsync(existing);
            return Results.Ok(existing);
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<Question> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            await repo.DeleteAsync(existing);
            return Results.NoContent();
        });
    }
}
