using Carter;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Domain.Entities;
using OnlineExam.Domain.Interfaces;
using OnlineExam.Infrastructure.Data;

namespace OnlineExam.API.Endpoints;

public record CreatePanelRequest(string Code, int[] EmployeeIds);

public class TechnicalPanelEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/technical-panels").WithTags("Exam - Technical Panels").RequireAuthorization();

        group.MapGet("/", async (AppDbContext db) =>
            Results.Ok(await db.TechnicalPanels.Include(p => p.Members).ToListAsync()));

        group.MapGet("/{id:int}", async (int id, AppDbContext db) =>
        {
            var panel = await db.TechnicalPanels.Include(p => p.Members).FirstOrDefaultAsync(p => p.Id == id);
            return panel is not null ? Results.Ok(panel) : Results.NotFound();
        });

        group.MapPost("/", async (CreatePanelRequest request, AppDbContext db) =>
        {
            var employees = await db.Employees
                .Where(e => request.EmployeeIds.Contains(e.Id))
                .ToListAsync();

            if (employees.Count != request.EmployeeIds.Length)
                return Results.BadRequest("One or more employee IDs are invalid");

            var panel = new TechnicalPanel
            {
                Code = request.Code,
                Members = employees
            };

            db.TechnicalPanels.Add(panel);
            await db.SaveChangesAsync();

            return Results.Created($"/api/technical-panels/{panel.Id}", panel);
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<TechnicalPanel> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            await repo.DeleteAsync(existing);
            return Results.NoContent();
        });
    }
}
