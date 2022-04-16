namespace RunescapeLootSim.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Item", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Item", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            AddColumn("dbo.Boss", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Boss", "UserId");
            AddForeignKey("dbo.Boss", "UserId", "dbo.ApplicationUser", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boss", "UserId", "dbo.ApplicationUser");
            DropIndex("dbo.Boss", new[] { "UserId" });
            DropColumn("dbo.Boss", "UserId");
            RenameIndex(table: "dbo.Item", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Item", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
