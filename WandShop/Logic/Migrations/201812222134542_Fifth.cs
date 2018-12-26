namespace Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fifth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Login", c => c.String());
            AddColumn("dbo.Players", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Password");
            DropColumn("dbo.Players", "Login");
        }
    }
}
