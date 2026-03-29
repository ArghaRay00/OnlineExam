# Online Exam System

A complete online examination platform built with ASP.NET MVC 5 and Entity Framework. Admins create exams with question sets, assign technical panels, and register colleges. Students register, take MCQ tests, and get scored automatically. Results can be pulled by exam code or filtered by college.

Built this during college to learn n-tier architecture with the repository pattern in .NET — the codebase is cleanly separated into four projects (Web App, Business Logic, Entities, Repository) instead of dumping everything in one project.

## What it does

**Admin side:**
- Register colleges and companies
- Create question sets (group of MCQ questions with answer keys)
- Create technical panels (exam invigilators)
- Set up exams — assign question set, panel, college, date, duration, cutoff
- View reports — results ranked by marks, students grouped by college

**Student side:**
- Register for an exam with USN, name, DOB, contact
- Take the exam — MCQ format with options A/B/C/D, timed
- Auto-graded against answer keys, marks stored

**Reporting:**
- Results report — filter by exam code, shows all students ranked by score
- Students report — filter by college, sorted by DOB

## Architecture

```
┌─────────────────────────────────────────────────────┐
│  OnlineTestApp (ASP.NET MVC 5)                      │
│  Controllers → Views (Razor) → ViewModels           │
│  8 Controllers, 39 Views, Bootstrap UI              │
├─────────────────────────────────────────────────────┤
│  OnlineTestBL (Business Logic)                      │
│  11 Manager classes — StudentManager,               │
│  ExaminationManager, PanelManager, LoginManager...  │
├─────────────────────────────────────────────────────┤
│  OnlineTestRepository (Data Access)                 │
│  Generic Repository pattern + EF6 DbContext         │
│  9 Code-First migrations                            │
├─────────────────────────────────────────────────────┤
│  OnlineTestEntities (Domain Models)                 │
│  15 entities: Student, Examination, Question,       │
│  QuestionSet, College, Marks, Employee, Panel...    │
├─────────────────────────────────────────────────────┤
│  SQL Server LocalDB                                 │
└─────────────────────────────────────────────────────┘
```

## Tech Stack

- **ASP.NET MVC 5** on .NET Framework 4.5.1
- **Entity Framework 6.1.3** — Code-First with migrations
- **SQL Server LocalDB** — Development database
- **Razor** — Server-side view engine
- **Bootstrap 3** + jQuery — Frontend UI
- **AutoMapper** — DTO/ViewModel mapping
- **Repository Pattern** — Generic + Common repository for data access

## Key Entities

| Entity | Purpose |
|--------|---------|
| `Student` | USN, name, DOB, email, college, marks |
| `Examination` | Exam code, date, duration, cutoff, question set, panel |
| `Question` | Question text, 4 options (A-D), answer key |
| `QuestionSet` | Group of questions, reuse tracking |
| `College` | Institutions registering students |
| `TechnicalPanel` | Exam invigilators/panel members |
| `Marks` | Score per student per exam |
| `Employee` / `Hr` | Staff roles for administration |

## Project Structure

```
OnlineTest/
├── OnlineTestApp/            # MVC web app (controllers, views, models)
│   ├── Controllers/          # 8 controllers (Admin, Exam, Login, Reports, Questions...)
│   ├── Views/                # 39 Razor views across modules
│   ├── Models/               # ViewModels and DTOs
│   ├── Filters/              # Custom authorization attribute
│   └── Content/Scripts/      # Bootstrap, jQuery, CSS
├── OnlineTestBL/             # Business logic layer (11 Manager classes)
├── OnlineTestEntities/       # Domain entities (15 models)
└── OnlineTestRepository/     # EF6 DbContext, generic repository, migrations
```

## Running it

1. Open `OnlineTest/OnlineTest.sln` in Visual Studio 2015+
2. Restore NuGet packages
3. Update connection string in `Web.config` if needed (defaults to LocalDB)
4. Run migrations: `Update-Database` in Package Manager Console
5. F5 to run

## Note

Built in 2016 as a college project. ASP.NET MVC 5 on .NET Framework is legacy now (modern equivalent is ASP.NET Core), but the architecture patterns — n-tier separation, repository pattern, code-first migrations, role-based auth — are still relevant and widely used.
