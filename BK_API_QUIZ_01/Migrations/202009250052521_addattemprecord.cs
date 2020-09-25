namespace BK_API_QUIZ_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattemprecord : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Essays", "QuizId", "dbo.Quizs");
            DropIndex("dbo.Essays", new[] { "QuizId" });
            AddColumn("dbo.Exam_attemp", "Record", c => c.String());
            DropTable("dbo.Essays");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Essays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Response = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Exam_attemp", "Record");
            CreateIndex("dbo.Essays", "QuizId");
            AddForeignKey("dbo.Essays", "QuizId", "dbo.Quizs", "Id", cascadeDelete: true);
        }
    }
}
