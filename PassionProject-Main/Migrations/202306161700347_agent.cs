namespace PassionProject_Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        AgentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ConatactInfo = c.String(),
                        Sepcialization = c.String(),
                        CommissionRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PerfomanceMetrics = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AgentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Agents");
        }
    }
}
