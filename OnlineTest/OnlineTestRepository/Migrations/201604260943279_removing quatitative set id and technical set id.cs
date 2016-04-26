namespace OnlineTestRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingquatitativesetidandtechnicalsetid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "TechnicalQuestionSetID");
            DropColumn("dbo.Questions", "QuantitativeQuestionSetID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "QuantitativeQuestionSetID", c => c.Int());
            AddColumn("dbo.Questions", "TechnicalQuestionSetID", c => c.Int());
        }
    }
}
