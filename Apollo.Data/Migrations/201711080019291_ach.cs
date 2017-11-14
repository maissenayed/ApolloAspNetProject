namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ach : DbMigration
    {
        public override void Up()
        {

            //CreateTable(
            //    "dbo.NewsLetters",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            To = c.String(unicode: false),
            //            Subject = c.String(unicode: false),
            //            msg = c.String(unicode: false),
            //            nbrecivers = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.NewsLettersOpens",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            IdNewsletter_Id = c.Int(),
            //            IdUser_id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ID)

            //    .Index(t => t.IdNewsletter_Id)
            //    .Index(t => t.IdUser_id);

            //AddForeignKey("dbo.NewsLettersOpens", "IdNewsletter_Id", "dbo.NewsLetters", "id");
            //AddForeignKey("dbo.NewsLettersOpens", "IdUser_id", "apollo.user", "id");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsLettersOpens", "IdUser_id", "apollo.user");
            DropForeignKey("dbo.NewsLettersOpens", "IdNewsletter_Id", "dbo.NewsLetters");
            
            DropIndex("dbo.NewsLettersOpens", new[] { "IdUser_id" });
            DropIndex("dbo.NewsLettersOpens", new[] { "IdNewsletter_Id" });
            
            DropTable("dbo.NewsLettersOpens");
            DropTable("dbo.NewsLetters");
            
        }
    }
}