namespace PassionProject_Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        BuyerInfo = c.String(),
                        TransactionType = c.String(),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AgentId = c.Int(nullable: false),
                        PropertyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Agents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.AgentId)
                .Index(t => t.PropertyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.Transactions", "AgentId", "dbo.Agents");
            DropIndex("dbo.Transactions", new[] { "PropertyId" });
            DropIndex("dbo.Transactions", new[] { "AgentId" });
            DropTable("dbo.Transactions");
        }
    }
}
