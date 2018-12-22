namespace Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerRounds", "WandsProducedAmount", c => c.Int(nullable: false));
            AddColumn("dbo.PlayerRounds", "WandsReservesAmount", c => c.Int(nullable: false));
            DropColumn("dbo.PlayerRounds", "WandsAmount");
            DropColumn("dbo.PlayerRounds", "RemainingWandsAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayerRounds", "RemainingWandsAmount", c => c.Int(nullable: false));
            AddColumn("dbo.PlayerRounds", "WandsAmount", c => c.Int(nullable: false));
            DropColumn("dbo.PlayerRounds", "WandsReservesAmount");
            DropColumn("dbo.PlayerRounds", "WandsProducedAmount");
        }
    }
}
