namespace TaskAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Currencies", "sa", c => c.String());
            AddColumn("dbo.CustomerAccounts", "Acc_Number", c => c.String(maxLength: 12));
            AddColumn("dbo.Customers", "CustomerNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "CustomerNumber");
            DropColumn("dbo.CustomerAccounts", "Acc_Number");
            DropColumn("dbo.Currencies", "sa");
        }
    }
}
