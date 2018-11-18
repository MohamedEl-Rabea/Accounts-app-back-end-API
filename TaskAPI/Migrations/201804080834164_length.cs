namespace TaskAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Class_Code", "Code", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Class_Code", "Code", c => c.String(maxLength: 2));
        }
    }
}
