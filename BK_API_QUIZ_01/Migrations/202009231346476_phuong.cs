namespace BK_API_QUIZ_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phuong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CerName = c.String(),
                        CerDescription = c.String(),
                        CerImage = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateModify = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamName = c.String(),
                        Intro = c.String(),
                        TimeOpen = c.DateTime(nullable: false),
                        TimeClose = c.DateTime(nullable: false),
                        Attemps = c.Int(nullable: false),
                        GradeMethod = c.Boolean(nullable: false),
                        SumGrade = c.Int(nullable: false),
                        GradetoPass = c.Int(nullable: false),
                        TimeCreate = c.DateTime(nullable: false),
                        TimeUpdate = c.DateTime(nullable: false),
                        TimeLimit = c.Int(nullable: false),
                        Pass = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exam_attemp",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ExamId = c.Int(nullable: false),
                        UserId = c.Int(),
                        attempt = c.Int(nullable: false),
                        SumGrade = c.Int(nullable: false),
                        TimeStart = c.DateTime(nullable: false),
                        TimeFinish = c.DateTime(nullable: false),
                        Preview = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .Index(t => t.ExamId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FullName = c.String(),
                        Image = c.String(),
                        Birthday = c.DateTime(),
                        Sex = c.Int(),
                        Address = c.String(),
                        Phonenumber = c.String(),
                        Email = c.String(),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exam_slot",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamId = c.Int(nullable: false),
                        QuizId = c.Int(nullable: false),
                        Quizslot = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.ExamId)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QType = c.String(),
                        QName = c.String(),
                        QuestionText = c.String(),
                        QSkill = c.String(),
                        DefaultMask = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimeCreate = c.DateTime(nullable: false),
                        TimeUpdate = c.DateTime(nullable: false),
                        QLevel = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DragDrops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsCorrect = c.Boolean(nullable: false),
                        Content = c.String(),
                        Image = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.Essays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Response = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
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
            
            CreateTable(
                "dbo.Matchings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        Image = c.String(),
                        AnswerText = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.MultipleChoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsCorrect = c.Boolean(nullable: false),
                        ShuffleAns = c.Boolean(nullable: false),
                        Content = c.String(),
                        Image = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.MultipleResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsCorrect = c.Boolean(nullable: false),
                        ShuffleAns = c.Boolean(nullable: false),
                        Content = c.String(),
                        Image = c.String(),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
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
            
            CreateTable(
                "dbo.ShortAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuizId = c.Int(nullable: false),
                        KeyContent = c.String(),
                        CaseSensitive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.TrueFalses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuizId = c.Int(nullable: false),
                        IsTrue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.CertificateUsers",
                c => new
                    {
                        CertificateId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CerFinish = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CertificateId, t.UserId })
                .ForeignKey("dbo.Certificates", t => t.CertificateId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CertificateId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Exam_grade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamId = c.Int(nullable: false),
                        UserId = c.Int(),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ExamId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Exam_override",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamId = c.Int(nullable: false),
                        UserId = c.Int(),
                        Attemps = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ExamId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ExamCertificates",
                c => new
                    {
                        Exam_Id = c.Int(nullable: false),
                        Certificate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Exam_Id, t.Certificate_Id })
                .ForeignKey("dbo.Exams", t => t.Exam_Id, cascadeDelete: true)
                .ForeignKey("dbo.Certificates", t => t.Certificate_Id, cascadeDelete: true)
                .Index(t => t.Exam_Id)
                .Index(t => t.Certificate_Id);
            
            CreateTable(
                "dbo.QuizExams",
                c => new
                    {
                        Quiz_Id = c.Int(nullable: false),
                        Exam_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Quiz_Id, t.Exam_Id })
                .ForeignKey("dbo.Quizs", t => t.Quiz_Id, cascadeDelete: true)
                .ForeignKey("dbo.Exams", t => t.Exam_Id, cascadeDelete: true)
                .Index(t => t.Quiz_Id)
                .Index(t => t.Exam_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exam_override", "UserId", "dbo.Users");
            DropForeignKey("dbo.Exam_override", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exam_grade", "UserId", "dbo.Users");
            DropForeignKey("dbo.Exam_grade", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.CertificateUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.CertificateUsers", "CertificateId", "dbo.Certificates");
            DropForeignKey("dbo.Exam_slot", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.TrueFalses", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.ShortAnswers", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Sequences", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.SelectFromLists", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Numerics", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.MultipleResponses", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.MultipleChoices", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Matchings", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.FillinBlanks", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.QuizExams", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.QuizExams", "Quiz_Id", "dbo.Quizs");
            DropForeignKey("dbo.Essays", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.DragDrops", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Exam_slot", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exam_attemp", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exam_attemp", "UserId", "dbo.Users");
            DropForeignKey("dbo.ExamCertificates", "Certificate_Id", "dbo.Certificates");
            DropForeignKey("dbo.ExamCertificates", "Exam_Id", "dbo.Exams");
            DropIndex("dbo.QuizExams", new[] { "Exam_Id" });
            DropIndex("dbo.QuizExams", new[] { "Quiz_Id" });
            DropIndex("dbo.ExamCertificates", new[] { "Certificate_Id" });
            DropIndex("dbo.ExamCertificates", new[] { "Exam_Id" });
            DropIndex("dbo.Exam_override", new[] { "UserId" });
            DropIndex("dbo.Exam_override", new[] { "ExamId" });
            DropIndex("dbo.Exam_grade", new[] { "UserId" });
            DropIndex("dbo.Exam_grade", new[] { "ExamId" });
            DropIndex("dbo.CertificateUsers", new[] { "UserId" });
            DropIndex("dbo.CertificateUsers", new[] { "CertificateId" });
            DropIndex("dbo.TrueFalses", new[] { "QuizId" });
            DropIndex("dbo.ShortAnswers", new[] { "QuizId" });
            DropIndex("dbo.Sequences", new[] { "QuizId" });
            DropIndex("dbo.SelectFromLists", new[] { "QuizId" });
            DropIndex("dbo.Numerics", new[] { "QuizId" });
            DropIndex("dbo.MultipleResponses", new[] { "QuizId" });
            DropIndex("dbo.MultipleChoices", new[] { "QuizId" });
            DropIndex("dbo.Matchings", new[] { "QuizId" });
            DropIndex("dbo.FillinBlanks", new[] { "QuizId" });
            DropIndex("dbo.Essays", new[] { "QuizId" });
            DropIndex("dbo.DragDrops", new[] { "QuizId" });
            DropIndex("dbo.Exam_slot", new[] { "QuizId" });
            DropIndex("dbo.Exam_slot", new[] { "ExamId" });
            DropIndex("dbo.Exam_attemp", new[] { "UserId" });
            DropIndex("dbo.Exam_attemp", new[] { "ExamId" });
            DropTable("dbo.QuizExams");
            DropTable("dbo.ExamCertificates");
            DropTable("dbo.Exam_override");
            DropTable("dbo.Exam_grade");
            DropTable("dbo.CertificateUsers");
            DropTable("dbo.TrueFalses");
            DropTable("dbo.ShortAnswers");
            DropTable("dbo.Sequences");
            DropTable("dbo.SelectFromLists");
            DropTable("dbo.Numerics");
            DropTable("dbo.MultipleResponses");
            DropTable("dbo.MultipleChoices");
            DropTable("dbo.Matchings");
            DropTable("dbo.FillinBlanks");
            DropTable("dbo.Essays");
            DropTable("dbo.DragDrops");
            DropTable("dbo.Quizs");
            DropTable("dbo.Exam_slot");
            DropTable("dbo.Users");
            DropTable("dbo.Exam_attemp");
            DropTable("dbo.Exams");
            DropTable("dbo.Certificates");
        }
    }
}
