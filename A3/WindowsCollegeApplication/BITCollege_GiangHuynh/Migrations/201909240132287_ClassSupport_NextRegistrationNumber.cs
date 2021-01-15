namespace BITCollege_GiangHuynh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassSupport_NextRegistrationNumber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NextRegistrationNumbers",
                c => new
                    {
                        NextRegistrationNumberId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextRegistrationNumberId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NextRegistrationNumbers");
        }
    }
}
