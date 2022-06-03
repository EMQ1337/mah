namespace mah9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "OldClassName", c => c.String());
            AddColumn("dbo.Subjects", "Oldsubject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Oldsubject");
            DropColumn("dbo.Classes", "OldClassName");
        }
    }
}
