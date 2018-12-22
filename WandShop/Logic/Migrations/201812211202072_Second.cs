namespace Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerParts", "GameParamId", "dbo.GameParams");
            DropForeignKey("dbo.Rounds", "PlayerPartId", "dbo.PlayerParts");
            DropIndex("dbo.PlayerParts", new[] { "GameParamId" });
            DropIndex("dbo.Rounds", new[] { "PlayerPartId" });
            DropPrimaryKey("dbo.GameParams");
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.PlayerParts", t => t.PlayerId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.PlayerRounds",
                c => new
                    {
                        PlayerRoundId = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.PlayerRoundId)
                .ForeignKey("dbo.PlayerParts", t => t.PlayerPartId, cascadeDelete: true)
                .Index(t => t.PlayerPartId);
            
            AddColumn("dbo.Rounds", "GameId", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "Tax", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "Dividend", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "InterestRate", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WoodPriceLow", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WoodPriceMedium", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WoodPriceHigh", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "CrystalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "CrystalPriceConst", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WoodConsumption", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "CrystalConsumption", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "PerfectStickConsumption", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "PerfectGemConsumption", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WoodAmountLow", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "WoodAmountHigh", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "HoursPerPeriod", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "EmploymentPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "DismissalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WorkerDwarfPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WorkerElfPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WorkerHumanPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WorkerDwarfEfficiency", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WorkerElfEfficiency", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WorkerHumanEfficiency", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "MachinePrice", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "CrystalEfficiency", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "Depreciation", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "MaxCrystalEfficiency", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "QualityFading", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "AdFading", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "ManagementCosts", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "TransportCosts", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "GeneralMaterialRateCosts", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "GeneralProcessingRateCosts", c => c.Double(nullable: false));
            AlterColumn("dbo.GameParams", "GameParamId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.GameParams", "GameParamId");
            CreateIndex("dbo.GameParams", "GameParamId");
            CreateIndex("dbo.Rounds", "GameId");
            AddForeignKey("dbo.GameParams", "GameParamId", "dbo.Games", "GameId");
            AddForeignKey("dbo.Rounds", "GameId", "dbo.Games", "GameId", cascadeDelete: true);
            DropColumn("dbo.PlayerParts", "GameParamId");
            DropColumn("dbo.Rounds", "PlayerPartId");
            DropColumn("dbo.Rounds", "Gold");
            DropColumn("dbo.Rounds", "LoanRemaining");
            DropColumn("dbo.Rounds", "WoodReserves");
            DropColumn("dbo.Rounds", "CrystalReserves");
            DropColumn("dbo.Rounds", "WoodPurchased");
            DropColumn("dbo.Rounds", "CrystalPurchased");
            DropColumn("dbo.Rounds", "WoodAverage");
            DropColumn("dbo.Rounds", "CrystalAverage");
            DropColumn("dbo.Rounds", "WoodAveragePrevious");
            DropColumn("dbo.Rounds", "CrystalAveragePrevious");
            DropColumn("dbo.Rounds", "MachinesOwned");
            DropColumn("dbo.Rounds", "MachinesPurchased");
            DropColumn("dbo.Rounds", "MachinesSold");
            DropColumn("dbo.Rounds", "DwarfWorkers");
            DropColumn("dbo.Rounds", "ElfWorkers");
            DropColumn("dbo.Rounds", "HumanWorkers");
            DropColumn("dbo.Rounds", "DwarfWorkersEmployed");
            DropColumn("dbo.Rounds", "ElfWorkersEmployed");
            DropColumn("dbo.Rounds", "HumanWorkersEmployed");
            DropColumn("dbo.Rounds", "DwarfWorkersDismissed");
            DropColumn("dbo.Rounds", "ElfWorkersDismissed");
            DropColumn("dbo.Rounds", "HumanWorkersDismissed");
            DropColumn("dbo.Rounds", "QualityExpense");
            DropColumn("dbo.Rounds", "AdExpense");
            DropColumn("dbo.Rounds", "QualityExpensePrevious");
            DropColumn("dbo.Rounds", "AdExpensePrevious");
            DropColumn("dbo.Rounds", "LoanPaid");
            DropColumn("dbo.Rounds", "LoanTaken");
            DropColumn("dbo.Rounds", "WandPrice");
            DropColumn("dbo.Rounds", "WandsAmount");
            DropColumn("dbo.Rounds", "Income");
            DropColumn("dbo.Rounds", "WandsSoldAmount");
            DropColumn("dbo.Rounds", "RemainingWandsAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rounds", "RemainingWandsAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "WandsSoldAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "Income", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WandsAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "WandPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "LoanTaken", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "LoanPaid", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "AdExpensePrevious", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "QualityExpensePrevious", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "AdExpense", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "QualityExpense", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "HumanWorkersDismissed", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "ElfWorkersDismissed", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "DwarfWorkersDismissed", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "HumanWorkersEmployed", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "ElfWorkersEmployed", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "DwarfWorkersEmployed", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "HumanWorkers", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "ElfWorkers", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "DwarfWorkers", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "MachinesSold", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "MachinesPurchased", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "MachinesOwned", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "CrystalAveragePrevious", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WoodAveragePrevious", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "CrystalAverage", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "WoodAverage", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "CrystalPurchased", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "WoodPurchased", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "CrystalReserves", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "WoodReserves", c => c.Int(nullable: false));
            AddColumn("dbo.Rounds", "LoanRemaining", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "Gold", c => c.Double(nullable: false));
            AddColumn("dbo.Rounds", "PlayerPartId", c => c.Int(nullable: false));
            AddColumn("dbo.PlayerParts", "GameParamId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Rounds", "GameId", "dbo.Games");
            DropForeignKey("dbo.PlayerRounds", "PlayerPartId", "dbo.PlayerParts");
            DropForeignKey("dbo.Players", "PlayerId", "dbo.PlayerParts");
            DropForeignKey("dbo.GameParams", "GameParamId", "dbo.Games");
            DropIndex("dbo.Rounds", new[] { "GameId" });
            DropIndex("dbo.PlayerRounds", new[] { "PlayerPartId" });
            DropIndex("dbo.Players", new[] { "PlayerId" });
            DropIndex("dbo.GameParams", new[] { "GameParamId" });
            DropPrimaryKey("dbo.GameParams");
            AlterColumn("dbo.GameParams", "GameParamId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Rounds", "GeneralProcessingRateCosts");
            DropColumn("dbo.Rounds", "GeneralMaterialRateCosts");
            DropColumn("dbo.Rounds", "TransportCosts");
            DropColumn("dbo.Rounds", "ManagementCosts");
            DropColumn("dbo.Rounds", "AdFading");
            DropColumn("dbo.Rounds", "QualityFading");
            DropColumn("dbo.Rounds", "MaxCrystalEfficiency");
            DropColumn("dbo.Rounds", "Depreciation");
            DropColumn("dbo.Rounds", "CrystalEfficiency");
            DropColumn("dbo.Rounds", "MachinePrice");
            DropColumn("dbo.Rounds", "WorkerHumanEfficiency");
            DropColumn("dbo.Rounds", "WorkerElfEfficiency");
            DropColumn("dbo.Rounds", "WorkerDwarfEfficiency");
            DropColumn("dbo.Rounds", "WorkerHumanPrice");
            DropColumn("dbo.Rounds", "WorkerElfPrice");
            DropColumn("dbo.Rounds", "WorkerDwarfPrice");
            DropColumn("dbo.Rounds", "DismissalPrice");
            DropColumn("dbo.Rounds", "EmploymentPrice");
            DropColumn("dbo.Rounds", "HoursPerPeriod");
            DropColumn("dbo.Rounds", "WoodAmountHigh");
            DropColumn("dbo.Rounds", "WoodAmountLow");
            DropColumn("dbo.Rounds", "PerfectGemConsumption");
            DropColumn("dbo.Rounds", "PerfectStickConsumption");
            DropColumn("dbo.Rounds", "CrystalConsumption");
            DropColumn("dbo.Rounds", "WoodConsumption");
            DropColumn("dbo.Rounds", "CrystalPriceConst");
            DropColumn("dbo.Rounds", "CrystalPrice");
            DropColumn("dbo.Rounds", "WoodPriceHigh");
            DropColumn("dbo.Rounds", "WoodPriceMedium");
            DropColumn("dbo.Rounds", "WoodPriceLow");
            DropColumn("dbo.Rounds", "InterestRate");
            DropColumn("dbo.Rounds", "Dividend");
            DropColumn("dbo.Rounds", "Tax");
            DropColumn("dbo.Rounds", "GameId");
            DropTable("dbo.PlayerRounds");
            DropTable("dbo.Players");
            AddPrimaryKey("dbo.GameParams", "GameParamId");
            CreateIndex("dbo.Rounds", "PlayerPartId");
            CreateIndex("dbo.PlayerParts", "GameParamId");
            AddForeignKey("dbo.Rounds", "PlayerPartId", "dbo.PlayerParts", "PlayerPartId", cascadeDelete: true);
            AddForeignKey("dbo.PlayerParts", "GameParamId", "dbo.GameParams", "GameParamId", cascadeDelete: true);
        }
    }
}
