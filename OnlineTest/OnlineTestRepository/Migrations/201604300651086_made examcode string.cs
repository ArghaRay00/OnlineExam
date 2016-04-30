namespace OnlineTestRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeexamcodestring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Examinations", "ExamCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Examinations", "ExamCode", c => c.Int(nullable: false));
        }
    }
}
