# Online Exam System

A complete online examination platform. Admins create exams with question sets, assign technical panels, and register colleges. Students register, take MCQ tests, and get scored automatically. Results can be pulled by exam code or filtered by college.

Originally built in 2016 with .NET Framework 4.5.1. Modernized in 2026 to .NET 9 with Clean Architecture.

## Version History

| Branch | Stack | Status |
|--------|-------|--------|
| `v1-legacy` | ASP.NET MVC 5, EF6, SQL Server LocalDB, .NET Framework 4.5.1 | Preserved (original code) |
| `v2-modern` / `master` | ASP.NET Core 9, EF Core 9, PostgreSQL, Carter, MediatR, JWT | Current |

### What changed in v2

| Area | v1 (2016) | v2 (2026) |
|------|-----------|-----------|
| Runtime | .NET Framework 4.5.1 | .NET 9 |
| Web framework | ASP.NET MVC 5 (Razor views) | ASP.NET Core 9 (REST API + Carter) |
| ORM | Entity Framework 6 | EF Core 9 |
| Database | SQL Server LocalDB (Windows only) | PostgreSQL (cross-platform, Docker) |
| Auth | Forms auth + cookies | JWT + BCrypt |
| Passwords | Plaintext | BCrypt hashed |
| DI | None (manual instantiation) | Built-in Microsoft DI |
| Validation | None | FluentValidation |
| CQRS | None | MediatR |
| Logging | Console.WriteLine | Serilog (structured) |
| API docs | None | Scalar (OpenAPI) |
| Architecture | 3-tier (tightly coupled) | Clean Architecture (Domain → Application → Infrastructure → API) |
| Entities | 17 (with duplicates) | 11 (consolidated) |
| Tests | None | xUnit + FluentAssertions |

## What it does

**Admin flow:**
1. Create locations, companies, colleges
2. Create question sets and add MCQ questions (4 options + answer key)
3. Create technical panels by selecting employees
4. Create examinations — assign question set, panel, college, date, duration, cutoff
5. Assign panel to exam
6. View reports — results ranked by score, students by college

**Student flow:**
1. Register for an exam with USN, personal details, and exam code
2. Get questions (answer keys are never exposed to the student)
3. Submit answers — auto-graded against answer keys, pass/fail against cutoff
4. Result stored with score, total, and pass status

## API Endpoints

### Auth
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | /api/auth/register | Public | Register user with BCrypt hash, returns JWT |
| POST | /api/auth/login | Public | Login, returns JWT |

### Admin — Setup
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET/POST/PUT/DELETE | /api/locations | JWT | Location CRUD |
| GET/POST/DELETE | /api/companies | JWT | Company CRUD |
| GET/POST/DELETE | /api/colleges | JWT | College CRUD |
| GET/POST/DELETE | /api/employees | JWT | Employee CRUD + filter by location |

### Admin — Exam Management
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET/POST/DELETE | /api/question-sets | JWT | Question set CRUD |
| GET/POST | /api/question-sets/:id/questions | JWT | Questions within a set |
| GET/PUT/DELETE | /api/questions/:id | JWT | Individual question CRUD |
| GET/POST/DELETE | /api/technical-panels | JWT | Panel CRUD (M:M with employees) |
| GET/POST | /api/exams | JWT | Exam CRUD (marks question set as used) |
| GET | /api/exams/by-code/:code | JWT | Lookup exam by code |
| POST | /api/exams/:id/assign-panel | JWT | Assign panel to exam |

### Student — Exam Taking
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | /api/exam-taking/register | Public | Register for exam with USN + exam code |
| GET | /api/exam-taking/:examCode/questions | Public | Get questions (no answer keys) |
| POST | /api/exam-taking/:examCode/submit/:studentId | Public | Submit answers, auto-grade |

### Reports
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | /api/reports/results/:examCode | JWT | Results ranked by score with pass % |
| GET | /api/reports/students/:collegeId | JWT | Students by college with avg scores |

## Tech Stack

- **ASP.NET Core 9** — Minimal API with Carter endpoint modules
- **EF Core 9** — Code-First with PostgreSQL
- **MediatR** — CQRS pattern for auth commands
- **FluentValidation** — Request validation
- **Mapster** — Object mapping
- **BCrypt** — Password hashing
- **JWT Bearer** — Authentication
- **Serilog** — Structured logging
- **Scalar** — OpenAPI documentation (at /scalar)
- **xUnit + FluentAssertions** — Testing
- **Docker** — PostgreSQL container

## Project Structure

```
src/
├── OnlineExam.Domain/              # Entities, enums, interfaces (zero deps)
├── OnlineExam.Application/         # Commands, handlers, validators (MediatR + FluentValidation)
├── OnlineExam.Infrastructure/      # EF Core, repositories, JWT, DI setup
└── OnlineExam.API/                 # Carter endpoints, Serilog, Scalar, Program.cs

tests/
├── OnlineExam.Domain.Tests/
└── OnlineExam.Application.Tests/
```

## Running it

```bash
# Prerequisites: .NET 9 SDK, Docker

# Start PostgreSQL
docker compose up -d

# Run the API
cd src/OnlineExam.API
dotnet run                    # http://localhost:5000

# API docs
open http://localhost:5000/scalar
```

## Entities

| Entity | Purpose |
|--------|---------|
| User | Auth — username, email, BCrypt hash, role (Admin/HR/TechnicalPanelist/Student) |
| Location | Geographic locations for colleges and exams |
| Company | Companies running recruitment exams |
| College | Colleges registering students |
| Employee | Staff assigned to technical panels |
| TechnicalPanel | Exam invigilators (M:M with employees) |
| QuestionSet | Group of MCQ questions (marked as used when assigned to exam) |
| Question | MCQ — text, 4 options (A/B/C/D), correct option |
| Examination | Exam setup — code, date, duration, cutoff, college, panel, question set |
| Student | Registered exam takers with academic details |
| ExamResult | Score per student per exam — auto-graded, pass/fail |
