namespace mah9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.PDF", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Subjects", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Videos", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PDF", "PDFurl", c => c.String(nullable: false));
            AlterColumn("dbo.PDF", "PDFName", c => c.String(nullable: false));
            AlterColumn("dbo.Videos", "VideoURL", c => c.String(nullable: false));
            AlterColumn("dbo.Videos", "VideoName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "VideoName", c => c.String());
            AlterColumn("dbo.Videos", "VideoURL", c => c.String());
            AlterColumn("dbo.PDF", "PDFName", c => c.String());
            AlterColumn("dbo.PDF", "PDFurl", c => c.String());
            DropColumn("dbo.Videos", "Date");
            DropColumn("dbo.AspNetUsers", "Date");
            DropColumn("dbo.Subjects", "Date");
            DropColumn("dbo.PDF", "Date");
            DropColumn("dbo.Classes", "Date");
        }
    }
}
