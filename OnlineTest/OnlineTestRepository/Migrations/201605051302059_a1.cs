namespace OnlineTestRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Questionset_QuestionSetId1", "dbo.Questionsets");
            DropForeignKey("dbo.Questions", "Questionset_QuestionSetId", "dbo.Questionsets");
            DropIndex("dbo.Questions", new[] { "Questionset_QuestionSetId" });
            DropIndex("dbo.Questions", new[] { "Questionset_QuestionSetId1" });
            DropColumn("dbo.Questions", "QuestionSetId");
            RenameColumn(table: "dbo.Questions", name: "Questionset_QuestionSetId", newName: "QuestionSetId");
            AddColumn("dbo.Marks", "Markss", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "QuestionSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "QuestionSetId");
            AddForeignKey("dbo.Questions", "QuestionSetId", "dbo.Questionsets", "QuestionSetId", cascadeDelete: true);
            DropColumn("dbo.Questions", "Questionset_QuestionSetId1");
            DropColumn("dbo.Marks", "MarksApti");
            DropColumn("dbo.Marks", "MarksTech");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Marks", "MarksTech", c => c.Int(nullable: false));
            AddColumn("dbo.Marks", "MarksApti", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Questionset_QuestionSetId1", c => c.Int());
            DropForeignKey("dbo.Questions", "QuestionSetId", "dbo.Questionsets");
            DropIndex("dbo.Questions", new[] { "QuestionSetId" });
            AlterColumn("dbo.Questions", "QuestionSetId", c => c.Int());
            DropColumn("dbo.Marks", "Markss");
            RenameColumn(table: "dbo.Questions", name: "QuestionSetId", newName: "Questionset_QuestionSetId");
            AddColumn("dbo.Questions", "QuestionSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "Questionset_QuestionSetId1");
            CreateIndex("dbo.Questions", "Questionset_QuestionSetId");
            AddForeignKey("dbo.Questions", "Questionset_QuestionSetId", "dbo.Questionsets", "QuestionSetId");
            AddForeignKey("dbo.Questions", "Questionset_QuestionSetId1", "dbo.Questionsets", "QuestionSetId");
        }
    }
}
