namespace TaskAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Currencies", "sa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Currencies", "sa", c => c.String());
        }
    }
}
