namespace OnlineTestRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedTechnicalPanel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TechnicalpanelEmployees", newName: "TechnicalPanelEmployee");
            RenameColumn(table: "dbo.TechnicalPanelEmployee", name: "Technicalpanel_TechnicalpanelId", newName: "TechnicalpanelRefId");
            RenameColumn(table: "dbo.TechnicalPanelEmployee", name: "Employee_EmployeeId", newName: "EmployeeRefId");
            RenameIndex(table: "dbo.TechnicalPanelEmployee", name: "IX_Technicalpanel_TechnicalpanelId", newName: "IX_TechnicalpanelRefId");
            RenameIndex(table: "dbo.TechnicalPanelEmployee", name: "IX_Employee_EmployeeId", newName: "IX_EmployeeRefId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TechnicalPanelEmployee", name: "IX_EmployeeRefId", newName: "IX_Employee_EmployeeId");
            RenameIndex(table: "dbo.TechnicalPanelEmployee", name: "IX_TechnicalpanelRefId", newName: "IX_Technicalpanel_TechnicalpanelId");
            RenameColumn(table: "dbo.TechnicalPanelEmployee", name: "EmployeeRefId", newName: "Employee_EmployeeId");
            RenameColumn(table: "dbo.TechnicalPanelEmployee", name: "TechnicalpanelRefId", newName: "Technicalpanel_TechnicalpanelId");
            RenameTable(name: "dbo.TechnicalPanelEmployee", newName: "TechnicalpanelEmployees");
        }
    }
}
