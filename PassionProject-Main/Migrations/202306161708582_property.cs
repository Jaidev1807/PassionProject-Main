namespace PassionProject_Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class property : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Type = c.String(),
                        Size = c.Int(nullable: false),
                        NoOfRooms = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PropertyId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Properties");
        }
    }
}
