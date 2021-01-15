namespace BITCollege_GiangHuynh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NextGradedCourses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NextGradedCourses",
                c => new
                    {
                        NextGradedCourseId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextGradedCourseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NextGradedCourses");
        }
    }
}
