using Carter;
using OnlineExam.Domain.Entities;
using OnlineExam.Domain.Interfaces;

namespace OnlineExam.API.Endpoints;

public class LocationEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/locations").WithTags("Admin - Locations").RequireAuthorization();

        group.MapGet("/", async (IRepository<Location> repo) =>
            Results.Ok(await repo.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id, IRepository<Location> repo) =>
            await repo.GetByIdAsync(id) is { } location ? Results.Ok(location) : Results.NotFound());

        group.MapPost("/", async (Location location, IRepository<Location> repo) =>
        {
            var created = await repo.AddAsync(location);
            return Results.Created($"/api/locations/{created.Id}", created);
        });

        group.MapPut("/{id:int}", async (int id, Location updated, IRepository<Location> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            existing.Name = updated.Name;
            existing.State = updated.State;
            await repo.UpdateAsync(existing);
            return Results.Ok(existing);
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<Location> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            await repo.DeleteAsync(existing);
            return Results.NoContent();
        });
    }
}

public class CompanyEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/companies").WithTags("Admin - Companies").RequireAuthorization();

        group.MapGet("/", async (IRepository<Company> repo) =>
            Results.Ok(await repo.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id, IRepository<Company> repo) =>
            await repo.GetByIdAsync(id) is { } company ? Results.Ok(company) : Results.NotFound());

        group.MapPost("/", async (Company company, IRepository<Company> repo) =>
        {
            var created = await repo.AddAsync(company);
            return Results.Created($"/api/companies/{created.Id}", created);
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<Company> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            await repo.DeleteAsync(existing);
            return Results.NoContent();
        });
    }
}

public class CollegeEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/colleges").WithTags("Admin - Colleges").RequireAuthorization();

        group.MapGet("/", async (IRepository<College> repo) =>
            Results.Ok(await repo.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id, IRepository<College> repo) =>
            await repo.GetByIdAsync(id) is { } college ? Results.Ok(college) : Results.NotFound());

        group.MapPost("/", async (College college, IRepository<College> repo) =>
        {
            var created = await repo.AddAsync(college);
            return Results.Created($"/api/colleges/{created.Id}", created);
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<College> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            await repo.DeleteAsync(existing);
            return Results.NoContent();
        });
    }
}

public class EmployeeEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employees").WithTags("Admin - Employees").RequireAuthorization();

        group.MapGet("/", async (IRepository<Employee> repo) =>
            Results.Ok(await repo.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id, IRepository<Employee> repo) =>
            await repo.GetByIdAsync(id) is { } employee ? Results.Ok(employee) : Results.NotFound());

        group.MapGet("/by-location/{locationId:int}", async (int locationId, IRepository<Employee> repo) =>
            Results.Ok(await repo.FindAsync(e => e.LocationId == locationId)));

        group.MapPost("/", async (Employee employee, IRepository<Employee> repo) =>
        {
            var created = await repo.AddAsync(employee);
            return Results.Created($"/api/employees/{created.Id}", created);
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<Employee> repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            await repo.DeleteAsync(existing);
            return Results.NoContent();
        });
    }
}
