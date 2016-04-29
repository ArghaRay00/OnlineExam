namespace OnlineTestRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examcodeaddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Examinations", "ExamCode", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Examinations", "ExamCode");
        }
    }
}
