namespace TaskAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Class_Code",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Acc_Type = c.String(maxLength: 2),
                        Code = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        CurrencyName = c.String(),
                        ISO_Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.CustomerAccounts",
                c => new
                    {
                        Acc_ID = c.Int(nullable: false, identity: true),
                        Acc_Type = c.String(maxLength: 2),
                        Class_Code = c.String(maxLength: 3),
                        Openning_Balance = c.Double(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Acc_ID)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        OpenDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.ExchangeRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromCurrencyId = c.Int(nullable: false),
                        ToCurrencyId = c.Int(nullable: false),
                        Operator = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.FromCurrencyId)
                .ForeignKey("dbo.Currencies", t => t.ToCurrencyId)
                .Index(t => t.FromCurrencyId)
                .Index(t => t.ToCurrencyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExchangeRates", "ToCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.ExchangeRates", "FromCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.CustomerAccounts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerAccounts", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.ExchangeRates", new[] { "ToCurrencyId" });
            DropIndex("dbo.ExchangeRates", new[] { "FromCurrencyId" });
            DropIndex("dbo.CustomerAccounts", new[] { "CurrencyId" });
            DropIndex("dbo.CustomerAccounts", new[] { "CustomerId" });
            DropTable("dbo.ExchangeRates");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerAccounts");
            DropTable("dbo.Currencies");
            DropTable("dbo.Class_Code");
        }
    }
}
