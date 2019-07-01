namespace ModelProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rajesh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Designation = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeModels");
        }
    }
}
