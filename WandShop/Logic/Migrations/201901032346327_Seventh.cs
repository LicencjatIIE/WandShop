namespace Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seventh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "MaxLoan", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "MaxLoan");
        }
    }
}
