namespace PortoGO.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Visit", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Visit", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Visit", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Visit", name: "UserId", newName: "User_Id");
        }
    }
}
