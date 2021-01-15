namespace BITCollege_GiangHuynh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NextAuditCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NextAuditCourses",
                c => new
                    {
                        NextAuditCourseId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextAuditCourseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NextAuditCourses");
        }
    }
}
