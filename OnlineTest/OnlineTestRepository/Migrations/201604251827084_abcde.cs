namespace OnlineTestRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abcde : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colleges",
                c => new
                    {
                        CollegeId = c.Int(nullable: false, identity: true),
                        CollegeName = c.String(),
                        CollegeAddr = c.String(),
                        CollegePhno = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CollegeId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Examinations",
                c => new
                    {
                        ExaminationId = c.Int(nullable: false, identity: true),
                        ExaminationDate = c.DateTime(),
                        ExamCutoff = c.Double(nullable: false),
                        ExamDuration = c.String(),
                        TechnicalPanleId = c.Int(),
                        QuestionsetId = c.Int(),
                        CollegeId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExaminationId)
                .ForeignKey("dbo.Colleges", t => t.CollegeId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.Questionsets", t => t.QuestionsetId)
                .ForeignKey("dbo.Technicalpanels", t => t.TechnicalPanleId)
                .Index(t => t.TechnicalPanleId)
                .Index(t => t.QuestionsetId)
                .Index(t => t.CollegeId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        LocationState = c.String(),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        EmployeeAddress = c.String(),
                        EmployeeNumber = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Hrs",
                c => new
                    {
                        HrId = c.Int(nullable: false, identity: true),
                        HrName = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HrId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Technicalpanels",
                c => new
                    {
                        TechnicalpanelId = c.Int(nullable: false, identity: true),
                        TechnicalPanelCode = c.String(),
                    })
                .PrimaryKey(t => t.TechnicalpanelId);
            
            CreateTable(
                "dbo.Questionsets",
                c => new
                    {
                        QuestionSetId = c.Int(nullable: false, identity: true),
                        QuestionSetCode = c.String(),
                        IsAlreadyUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionSetId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionName = c.String(),
                        Option1 = c.String(),
                        Option2 = c.String(),
                        Option3 = c.String(),
                        Option4 = c.String(),
                        AnswerKey = c.Int(nullable: false),
                        QuestionSetId = c.Int(nullable: false),
                        TechnicalQuestionSetID = c.Int(),
                        QuantitativeQuestionSetID = c.Int(),
                        Questionset_QuestionSetId = c.Int(),
                        Questionset_QuestionSetId1 = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Questionsets", t => t.Questionset_QuestionSetId)
                .ForeignKey("dbo.Questionsets", t => t.Questionset_QuestionSetId1)
                .Index(t => t.Questionset_QuestionSetId)
                .Index(t => t.Questionset_QuestionSetId1);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                        StudentUsn = c.String(),
                        StudentDob = c.DateTime(nullable: false),
                        StudentEmail = c.String(),
                        StudentAddr = c.String(),
                        StudentMob = c.String(),
                        StudentAgg = c.Double(nullable: false),
                        StudentPer12th = c.Double(nullable: false),
                        StudentCollege12th = c.String(),
                        StudentPer10th = c.Double(nullable: false),
                        StudentSchool = c.String(),
                        MarksId = c.Int(),
                        CollegeId = c.Int(nullable: false),
                        ExamId = c.Int(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Colleges", t => t.CollegeId, cascadeDelete: true)
                .ForeignKey("dbo.Examinations", t => t.ExamId)
                .ForeignKey("dbo.Marks", t => t.MarksId)
                .Index(t => t.MarksId)
                .Index(t => t.CollegeId)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        MarksId = c.Int(nullable: false, identity: true),
                        MarksApti = c.Int(nullable: false),
                        MarksTech = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MarksId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.StudentQuestions",
                c => new
                    {
                        StudentQuestionID = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(),
                        AnswerKey = c.Int(nullable: false),
                        StudentId = c.Int(),
                        IsAnswered = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentQuestionID)
                .ForeignKey("dbo.Questions", t => t.AnswerKey, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.QuestionId)
                .Index(t => t.AnswerKey)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.TechnicalpanelEmployees",
                c => new
                    {
                        Technicalpanel_TechnicalpanelId = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Technicalpanel_TechnicalpanelId, t.Employee_EmployeeId })
                .ForeignKey("dbo.Technicalpanels", t => t.Technicalpanel_TechnicalpanelId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId, cascadeDelete: true)
                .Index(t => t.Technicalpanel_TechnicalpanelId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentQuestions", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.StudentQuestions", "AnswerKey", "dbo.Questions");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Colleges", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Examinations", "TechnicalPanleId", "dbo.Technicalpanels");
            DropForeignKey("dbo.Students", "MarksId", "dbo.Marks");
            DropForeignKey("dbo.Students", "ExamId", "dbo.Examinations");
            DropForeignKey("dbo.Students", "CollegeId", "dbo.Colleges");
            DropForeignKey("dbo.Examinations", "QuestionsetId", "dbo.Questionsets");
            DropForeignKey("dbo.Questions", "Questionset_QuestionSetId1", "dbo.Questionsets");
            DropForeignKey("dbo.Questions", "Questionset_QuestionSetId", "dbo.Questionsets");
            DropForeignKey("dbo.Examinations", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.TechnicalpanelEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.TechnicalpanelEmployees", "Technicalpanel_TechnicalpanelId", "dbo.Technicalpanels");
            DropForeignKey("dbo.Employees", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Hrs", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Examinations", "CollegeId", "dbo.Colleges");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.TechnicalpanelEmployees", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.TechnicalpanelEmployees", new[] { "Technicalpanel_TechnicalpanelId" });
            DropIndex("dbo.StudentQuestions", new[] { "StudentId" });
            DropIndex("dbo.StudentQuestions", new[] { "AnswerKey" });
            DropIndex("dbo.StudentQuestions", new[] { "QuestionId" });
            DropIndex("dbo.Students", new[] { "ExamId" });
            DropIndex("dbo.Students", new[] { "CollegeId" });
            DropIndex("dbo.Students", new[] { "MarksId" });
            DropIndex("dbo.Questions", new[] { "Questionset_QuestionSetId1" });
            DropIndex("dbo.Questions", new[] { "Questionset_QuestionSetId" });
            DropIndex("dbo.Hrs", new[] { "CompanyId" });
            DropIndex("dbo.Employees", new[] { "LocationId" });
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropIndex("dbo.Examinations", new[] { "LocationId" });
            DropIndex("dbo.Examinations", new[] { "CollegeId" });
            DropIndex("dbo.Examinations", new[] { "QuestionsetId" });
            DropIndex("dbo.Examinations", new[] { "TechnicalPanleId" });
            DropIndex("dbo.Colleges", new[] { "LocationId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.TechnicalpanelEmployees");
            DropTable("dbo.StudentQuestions");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Marks");
            DropTable("dbo.Students");
            DropTable("dbo.Questions");
            DropTable("dbo.Questionsets");
            DropTable("dbo.Technicalpanels");
            DropTable("dbo.Hrs");
            DropTable("dbo.Companies");
            DropTable("dbo.Employees");
            DropTable("dbo.Locations");
            DropTable("dbo.Examinations");
            DropTable("dbo.Colleges");
        }
    }
}
