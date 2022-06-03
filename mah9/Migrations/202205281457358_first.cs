namespace mah9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                    })
                .PrimaryKey(t => t.ClassID);
            
            CreateTable(
                "dbo.PDF",
                c => new
                    {
                        PDFID = c.Int(nullable: false, identity: true),
                        PDFurl = c.String(),
                        PDFName = c.String(),
                        PDFSubject = c.String(),
                        PDFClass = c.String(),
                    })
                .PrimaryKey(t => t.PDFID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        subjectID = c.Int(nullable: false, identity: true),
                        subject = c.String(),
                    })
                .PrimaryKey(t => t.subjectID);
            
            CreateTable(
                "dbo.UserCopy",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        UserName = c.String(),
                        Class = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoID = c.Int(nullable: false, identity: true),
                        VideoURL = c.String(),
                        VideoName = c.String(),
                        VideoSubject = c.String(),
                        VideoClass = c.String(),
                    })
                .PrimaryKey(t => t.VideoID);
            
            AddColumn("dbo.AspNetUsers", "Class", c => c.String());
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddColumn("dbo.AspNetUsers", "SeconID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetUsers", "SeconID");
            DropColumn("dbo.AspNetUsers", "Password");
            DropColumn("dbo.AspNetUsers", "Class");
            DropTable("dbo.Videos");
            DropTable("dbo.UserCopy");
            DropTable("dbo.Subjects");
            DropTable("dbo.PDF");
            DropTable("dbo.Classes");
        }
    }
}
