namespace Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameParams", "GameParamId", "dbo.Games");
            DropIndex("dbo.GameParams", new[] { "GameParamId" });
            AddColumn("dbo.Games", "Tax", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "Dividend", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "OwnContribution", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "ForeignShares", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "Loan", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "InterestRate", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "BuildingCost", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WoodPriceLow", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WoodPriceMedium", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WoodPriceHigh", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "CrystalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "CrystalPriceConst", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WoodConsumption", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "CrystalConsumption", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "PerfectStickConsumption", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "PerfectGemConsumption", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WoodAmountLow", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "WoodAmountHigh", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "HoursPerPeriod", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "EmploymentPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "DismissalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WorkerDwarfPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WorkerElfPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WorkerHumanPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WorkerDwarfEfficiency", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WorkerElfEfficiency", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "WorkerHumanEfficiency", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "MachinePrice", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "CrystalEfficiency", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "Depreciation", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "MaxCrystalEfficiency", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "QualityFading", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "AdFading", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "ManagementCosts", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "TransportCosts", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "GeneralMaterialRateCosts", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "GeneralProcessingRateCosts", c => c.Double(nullable: false));
            DropTable("dbo.GameParams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GameParams",
                c => new
                    {
                        GameParamId = c.Int(nullable: false),
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
            
            DropColumn("dbo.Games", "GeneralProcessingRateCosts");
            DropColumn("dbo.Games", "GeneralMaterialRateCosts");
            DropColumn("dbo.Games", "TransportCosts");
            DropColumn("dbo.Games", "ManagementCosts");
            DropColumn("dbo.Games", "AdFading");
            DropColumn("dbo.Games", "QualityFading");
            DropColumn("dbo.Games", "MaxCrystalEfficiency");
            DropColumn("dbo.Games", "Depreciation");
            DropColumn("dbo.Games", "CrystalEfficiency");
            DropColumn("dbo.Games", "MachinePrice");
            DropColumn("dbo.Games", "WorkerHumanEfficiency");
            DropColumn("dbo.Games", "WorkerElfEfficiency");
            DropColumn("dbo.Games", "WorkerDwarfEfficiency");
            DropColumn("dbo.Games", "WorkerHumanPrice");
            DropColumn("dbo.Games", "WorkerElfPrice");
            DropColumn("dbo.Games", "WorkerDwarfPrice");
            DropColumn("dbo.Games", "DismissalPrice");
            DropColumn("dbo.Games", "EmploymentPrice");
            DropColumn("dbo.Games", "HoursPerPeriod");
            DropColumn("dbo.Games", "WoodAmountHigh");
            DropColumn("dbo.Games", "WoodAmountLow");
            DropColumn("dbo.Games", "PerfectGemConsumption");
            DropColumn("dbo.Games", "PerfectStickConsumption");
            DropColumn("dbo.Games", "CrystalConsumption");
            DropColumn("dbo.Games", "WoodConsumption");
            DropColumn("dbo.Games", "CrystalPriceConst");
            DropColumn("dbo.Games", "CrystalPrice");
            DropColumn("dbo.Games", "WoodPriceHigh");
            DropColumn("dbo.Games", "WoodPriceMedium");
            DropColumn("dbo.Games", "WoodPriceLow");
            DropColumn("dbo.Games", "BuildingCost");
            DropColumn("dbo.Games", "InterestRate");
            DropColumn("dbo.Games", "Loan");
            DropColumn("dbo.Games", "ForeignShares");
            DropColumn("dbo.Games", "OwnContribution");
            DropColumn("dbo.Games", "Dividend");
            DropColumn("dbo.Games", "Tax");
            CreateIndex("dbo.GameParams", "GameParamId");
            AddForeignKey("dbo.GameParams", "GameParamId", "dbo.Games", "GameId");
        }
    }
}
