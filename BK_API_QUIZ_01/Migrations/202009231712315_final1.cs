namespace BK_API_QUIZ_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exams", "SumGrade", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exams", "SumGrade", c => c.Int(nullable: false));
        }
    }
}
