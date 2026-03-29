# OnlineExam — Modernization Plan

## Current State (v1-legacy branch)

ASP.NET MVC 5 on .NET Framework 4.5.1 with Entity Framework 6 and SQL Server LocalDB. Built in 2016. 3-tier architecture with repository pattern. 17 entities, 11 managers, 8 controllers, 39 Razor views.

### What the app does

**Admin flow:**
1. Create locations, companies, colleges
2. Create question sets → add questions (MCQ with 4 options + answer key)
3. Create technical panels (select employees by location)
4. Create examinations (assign question set, panel, college, date, duration, cutoff)
5. View reports (results by exam code, students by college)

**Student flow:**
1. Register for exam (USN, name, DOB, contact, college)
2. Take exam (MCQ, radio buttons for A/B/C/D)
3. Auto-graded against answer keys, marks stored

**Auth:**
- Forms authentication with custom principal
- Roles: HR, Technical Panelist
- Custom authorize attribute

### Issues in current code

- Passwords stored plaintext (no hashing)
- Session-based state (exam ID, student ID stored in Session)
- CommonRepository creates new DbContext per method call (not per request)
- No async/await anywhere
- No dependency injection (managers directly instantiate repos)
- Duplicate entities (Role vs Roles, User vs LoginDetails)
- No input validation, no error handling, no logging
- Typo in entity: TechnicalPanleId (should be TechnicalPanelId)

---

## Target State (v2-modern branch)

### Tech Stack

| Layer | v1 (legacy) | v2 (modern) |
|-------|-------------|-------------|
| Runtime | .NET Framework 4.5.1 | .NET 9 |
| Web framework | ASP.NET MVC 5 | ASP.NET Core 9 (REST API) |
| ORM | Entity Framework 6 | EF Core 9 |
| Database | SQL Server LocalDB | PostgreSQL (Docker) |
| Auth | Forms auth + cookie | JWT (same pattern as Globo) |
| DI | None | Built-in Microsoft DI |
| Testing | None | xUnit + FluentAssertions |
| Validation | None | FluentValidation |
| Logging | None | Serilog |
| API docs | None | Swagger/OpenAPI |
| Password | Plaintext | BCrypt |
| Architecture | 3-tier (tightly coupled) | Clean Architecture (Domain → Application → Infrastructure → API) |

### Project Structure

```
OnlineExam/
├── src/
│   ├── OnlineExam.Domain/           # Entities, interfaces, value objects
│   ├── OnlineExam.Application/      # Use cases, DTOs, validators
│   ├── OnlineExam.Infrastructure/   # EF Core, repositories, auth, seed data
│   └── OnlineExam.API/              # Controllers, middleware, startup
├── tests/
│   ├── OnlineExam.Domain.Tests/
│   ├── OnlineExam.Application.Tests/
│   └── OnlineExam.API.Tests/
├── docker-compose.yml               # PostgreSQL
└── OnlineExam.sln
```

### Entity Consolidation

**v1 (17 entities with duplicates) → v2 (11 clean entities)**

| v2 Entity | From v1 | Changes |
|-----------|---------|---------|
| User | User + LoginDetails + Roles | Unified, BCrypt password, Role enum |
| Student | Student | Cleaned up, proper FK naming |
| College | College | Same, implements BaseEntity |
| Company | Company | Same |
| Location | Location | Same |
| Employee | Employee | Same |
| TechnicalPanel | Technicalpanel | Fixed typo, clean naming |
| Examination | Examination | Fixed FK typos, proper relations |
| QuestionSet | Questionset | Clean naming |
| Question | Question | Same, belongs to QuestionSet |
| ExamResult | Marks + StudentQuestions | Consolidated into one result entity |

### API Endpoints (REST, replaces MVC views)

**Auth:**
- POST /api/auth/register — signup with BCrypt
- POST /api/auth/login — returns JWT
- GET /api/auth/me — current user

**Admin — Exams:**
- POST /api/exams — create exam
- GET /api/exams — list all exams
- GET /api/exams/:id — exam detail

**Admin — Questions:**
- POST /api/question-sets — create set
- GET /api/question-sets — list sets
- POST /api/question-sets/:id/questions — add question
- GET /api/question-sets/:id/questions — list questions in set
- PUT /api/questions/:id — update question
- DELETE /api/questions/:id — delete question

**Admin — Setup:**
- CRUD for /api/colleges, /api/companies, /api/locations, /api/employees
- POST /api/technical-panels — create panel from employees
- POST /api/exams/:id/assign-panel — assign panel to exam

**Student:**
- POST /api/exams/:examCode/register — register for exam
- GET /api/exams/:examCode/take — get questions
- POST /api/exams/:examCode/submit — submit answers, auto-grade

**Reports:**
- GET /api/reports/results?examCode=X — results by exam
- GET /api/reports/students?college=X — students by college

---

## Implementation Phases

### Phase 1 — Foundation (Session 1)
- Scaffold .NET 9 solution with Clean Architecture
- Domain entities (11 clean entities)
- EF Core DbContext with PostgreSQL
- Migrations
- Docker compose for PostgreSQL

### Phase 2 — Auth + Admin CRUD (Session 2)
- JWT auth (register, login, me)
- BCrypt password hashing
- College, Company, Location, Employee CRUD
- Seed data

### Phase 3 — Exam Management (Session 3)
- Question sets + questions CRUD
- Technical panels
- Examination creation + panel assignment

### Phase 4 — Exam Taking (Session 4)
- Student registration
- Get questions for exam
- Submit answers + auto-grading
- ExamResult storage

### Phase 5 — Reports + Polish (Session 5)
- Results report, students report
- Swagger docs
- xUnit tests
- README update with full history

---

## Branch Strategy

| Branch | What | Status |
|--------|------|--------|
| v1-legacy | Original .NET Framework 4.5.1 code | Preserved |
| master | Latest stable (v2 after merge) | Will be updated |
| v2-modern | .NET 9 modernization | To be created |
