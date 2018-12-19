namespace Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Innitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        MaxRounds = c.Int(nullable: false),
                        CurrentRound = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.PlayerParts",
                c => new
                    {
                        PlayerPartId = c.Int(nullable: false, identity: true),
                        CurrentRound = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        GameParamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerPartId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.GameParams", t => t.GameParamId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.GameParamId);
            
            CreateTable(
                "dbo.GameParams",
                c => new
                    {
                        GameParamId = c.Int(nullable: false, identity: true),
                        Tax = c.Double(nullable: false),
                        Dividend = c.Double(nullable: false),
                        OwnContribution = c.Double(nullable: false),
                        ForeignShares = c.Double(nullable: false),
                        Loan = c.Double(nullable: false),
                        InterestRate = c.Double(nullable: false),
                        BuildingCost = c.Double(nullable: false),
                        WoodPriceLow = c.Double(nullable: false),
                        WoodPriceMedium = c.Double(nullable: false),
                        WoodPriceHigh = c.Double(nullable: false),
                        CrystalPrice = c.Double(nullable: false),
                        CrystalPriceConst = c.Double(nullable: false),
                        WoodConsumption = c.Double(nullable: false),
                        CrystalConsumption = c.Double(nullable: false),
                        PerfectStickConsumption = c.Double(nullable: false),
                        PerfectGemConsumption = c.Double(nullable: false),
                        WoodAmountLow = c.Int(nullable: false),
                        WoodAmountHigh = c.Int(nullable: false),
                        HoursPerPeriod = c.Int(nullable: false),
                        EmploymentPrice = c.Double(nullable: false),
                        DismissalPrice = c.Double(nullable: false),
                        WorkerDwarfPrice = c.Double(nullable: false),
                        WorkerElfPrice = c.Double(nullable: false),
                        WorkerHumanPrice = c.Double(nullable: false),
                        WorkerDwarfEfficiency = c.Double(nullable: false),
                        WorkerElfEfficiency = c.Double(nullable: false),
                        WorkerHumanEfficiency = c.Double(nullable: false),
                        MachinePrice = c.Double(nullable: false),
                        CrystalEfficiency = c.Int(nullable: false),
                        Depreciation = c.Double(nullable: false),
                        MaxCrystalEfficiency = c.Double(nullable: false),
                        QualityFading = c.Double(nullable: false),
                        AdFading = c.Double(nullable: false),
                        ManagementCosts = c.Double(nullable: false),
                        TransportCosts = c.Double(nullable: false),
                        GeneralMaterialRateCosts = c.Double(nullable: false),
                        GeneralProcessingRateCosts = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GameParamId);
            
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        RoundId = c.Int(nullable: false, identity: true),
                        PlayerPartId = c.Int(nullable: false),
                        Gold = c.Double(nullable: false),
                        LoanRemaining = c.Double(nullable: false),
                        WoodReserves = c.Int(nullable: false),
                        CrystalReserves = c.Int(nullable: false),
                        WoodPurchased = c.Int(nullable: false),
                        CrystalPurchased = c.Int(nullable: false),
                        WoodAverage = c.Double(nullable: false),
                        CrystalAverage = c.Double(nullable: false),
                        WoodAveragePrevious = c.Double(nullable: false),
                        CrystalAveragePrevious = c.Double(nullable: false),
                        MachinesOwned = c.Int(nullable: false),
                        MachinesPurchased = c.Int(nullable: false),
                        MachinesSold = c.Int(nullable: false),
                        DwarfWorkers = c.Int(nullable: false),
                        ElfWorkers = c.Int(nullable: false),
                        HumanWorkers = c.Int(nullable: false),
                        DwarfWorkersEmployed = c.Int(nullable: false),
                        ElfWorkersEmployed = c.Int(nullable: false),
                        HumanWorkersEmployed = c.Int(nullable: false),
                        DwarfWorkersDismissed = c.Int(nullable: false),
                        ElfWorkersDismissed = c.Int(nullable: false),
                        HumanWorkersDismissed = c.Int(nullable: false),
                        QualityExpense = c.Double(nullable: false),
                        AdExpense = c.Double(nullable: false),
                        QualityExpensePrevious = c.Double(nullable: false),
                        AdExpensePrevious = c.Double(nullable: false),
                        LoanPaid = c.Double(nullable: false),
                        LoanTaken = c.Double(nullable: false),
                        WandPrice = c.Double(nullable: false),
                        WandsAmount = c.Int(nullable: false),
                        Income = c.Double(nullable: false),
                        WandsSoldAmount = c.Int(nullable: false),
                        RemainingWandsAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoundId)
                .ForeignKey("dbo.PlayerParts", t => t.PlayerPartId, cascadeDelete: true)
                .Index(t => t.PlayerPartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rounds", "PlayerPartId", "dbo.PlayerParts");
            DropForeignKey("dbo.PlayerParts", "GameParamId", "dbo.GameParams");
            DropForeignKey("dbo.PlayerParts", "GameId", "dbo.Games");
            DropIndex("dbo.Rounds", new[] { "PlayerPartId" });
            DropIndex("dbo.PlayerParts", new[] { "GameParamId" });
            DropIndex("dbo.PlayerParts", new[] { "GameId" });
            DropTable("dbo.Rounds");
            DropTable("dbo.GameParams");
            DropTable("dbo.PlayerParts");
            DropTable("dbo.Games");
        }
    }
}
