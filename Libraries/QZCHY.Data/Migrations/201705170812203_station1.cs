namespace QZCHY.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class station1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubStationElectricLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubStationId = c.Int(nullable: false),
                        ElectricLineId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ElectricLine", t => t.ElectricLineId, cascadeDelete: true)
                .ForeignKey("dbo.SubStation", t => t.SubStationId, cascadeDelete: true)
                .Index(t => t.SubStationId)
                .Index(t => t.ElectricLineId);
            
            CreateTable(
                "dbo.SubStation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        Path = c.Geography(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubStationElectricLines", "SubStationId", "dbo.SubStation");
            DropForeignKey("dbo.SubStationElectricLines", "ElectricLineId", "dbo.ElectricLine");
            DropIndex("dbo.SubStationElectricLines", new[] { "ElectricLineId" });
            DropIndex("dbo.SubStationElectricLines", new[] { "SubStationId" });
            DropTable("dbo.SubStation");
            DropTable("dbo.SubStationElectricLines");
        }
    }
}
