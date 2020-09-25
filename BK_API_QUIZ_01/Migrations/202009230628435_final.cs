namespace BK_API_QUIZ_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Numerics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuizId = c.Int(nullable: false),
                        KeyContent = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.SelectFromLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsCorrect = c.Boolean(nullable: false),
                        ShuffleAns = c.Boolean(nullable: false),
                        Content = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.Sequences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        order = c.Int(nullable: false),
                        Content = c.String(),
                        Image = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            AddColumn("dbo.Matchings", "Image", c => c.String());
            DropColumn("dbo.Essays", "GradeInfo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Essays", "GradeInfo", c => c.String());
            DropForeignKey("dbo.Sequences", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.SelectFromLists", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Numerics", "QuizId", "dbo.Quizs");
            DropIndex("dbo.Sequences", new[] { "QuizId" });
            DropIndex("dbo.SelectFromLists", new[] { "QuizId" });
            DropIndex("dbo.Numerics", new[] { "QuizId" });
            DropColumn("dbo.Matchings", "Image");
            DropTable("dbo.Sequences");
            DropTable("dbo.SelectFromLists");
            DropTable("dbo.Numerics");
        }
    }
}
