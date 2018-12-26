namespace Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerRounds", "PlayerRoundNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlayerRounds", "PlayerRoundNumber");
        }
    }
}
