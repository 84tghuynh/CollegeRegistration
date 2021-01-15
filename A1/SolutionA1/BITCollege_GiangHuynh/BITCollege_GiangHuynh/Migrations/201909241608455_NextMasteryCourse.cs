namespace BITCollege_GiangHuynh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NextMasteryCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NextMasteryCourses",
                c => new
                    {
                        NextMasteryCourseId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextMasteryCourseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NextMasteryCourses");
        }
    }
}
