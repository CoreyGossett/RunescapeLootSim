namespace RunescapeLootSim.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OSRS : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "Boss_BossId", "dbo.Boss");
            DropIndex("dbo.Item", new[] { "Boss_BossId" });
            DropColumn("dbo.Item", "Boss_BossId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "Boss_BossId", c => c.Int());
            CreateIndex("dbo.Item", "Boss_BossId");
            AddForeignKey("dbo.Item", "Boss_BossId", "dbo.Boss", "BossId");
        }
    }
}
