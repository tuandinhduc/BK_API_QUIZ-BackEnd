namespace BK_API_QUIZ_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FillinBlanks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FillinBlanks", "QuizId", "dbo.Quizs");
            DropIndex("dbo.FillinBlanks", new[] { "QuizId" });
            DropTable("dbo.FillinBlanks");
        }
    }
}
