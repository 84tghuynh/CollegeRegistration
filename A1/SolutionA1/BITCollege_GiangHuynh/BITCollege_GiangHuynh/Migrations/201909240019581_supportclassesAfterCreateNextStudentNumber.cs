namespace BITCollege_GiangHuynh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supportclassesAfterCreateNextStudentNumber : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Courses",
            //    c => new
            //        {
            //            CourseId = c.Int(nullable: false, identity: true),
            //            ProgramId = c.Int(),
            //            CourseNumber = c.String(),
            //            Title = c.String(nullable: false),
            //            CreditHours = c.Double(nullable: false),
            //            TuitionAmount = c.Double(nullable: false),
            //            Notes = c.String(),
            //            AssignmentWeight = c.Double(),
            //            MidtermWeight = c.Double(),
            //            FinalWeight = c.Double(),
            //            MaximumAttempts = c.Int(),
            //            Discriminator = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.CourseId)
            //    .ForeignKey("dbo.Programs", t => t.ProgramId)
            //    .Index(t => t.ProgramId);
            
            //CreateTable(
            //    "dbo.Programs",
            //    c => new
            //        {
            //            ProgramId = c.Int(nullable: false, identity: true),
            //            ProgramAcronym = c.String(nullable: false),
            //            Description = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ProgramId);
            
            //CreateTable(
            //    "dbo.Registrations",
            //    c => new
            //        {
            //            RegistrationId = c.Int(nullable: false, identity: true),
            //            RegistrationNumber = c.Long(nullable: false),
            //            StudentId = c.Int(nullable: false),
            //            CourseId = c.Int(nullable: false),
            //            RegistrationDate = c.DateTime(nullable: false),
            //            Grade = c.Double(),
            //            Notes = c.String(),
            //        })
            //    .PrimaryKey(t => t.RegistrationId)
            //    .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
            //    .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
            //    .Index(t => t.StudentId)
            //    .Index(t => t.CourseId);
            
            //CreateTable(
            //    "dbo.Students",
            //    c => new
            //        {
            //            StudentId = c.Int(nullable: false, identity: true),
            //            GPAStateId = c.Int(nullable: false),
            //            ProgramId = c.Int(),
            //            StudentNumber = c.Long(nullable: false),
            //            FirstName = c.String(nullable: false, maxLength: 35),
            //            LastName = c.String(nullable: false, maxLength: 35),
            //            Address = c.String(nullable: false, maxLength: 35),
            //            City = c.String(nullable: false, maxLength: 35),
            //            Province = c.String(nullable: false, maxLength: 2),
            //            PostalCode = c.String(nullable: false, maxLength: 7),
            //            DateCreated = c.DateTime(nullable: false),
            //            GradePointAverage = c.Double(),
            //            OutstandingFees = c.Double(nullable: false),
            //            Notes = c.String(),
            //        })
            //    .PrimaryKey(t => t.StudentId)
            //    .ForeignKey("dbo.GPAStates", t => t.GPAStateId, cascadeDelete: true)
            //    .ForeignKey("dbo.Programs", t => t.ProgramId)
            //    .Index(t => t.GPAStateId)
            //    .Index(t => t.ProgramId);
            
            //CreateTable(
            //    "dbo.GPAStates",
            //    c => new
            //        {
            //            GPAStateId = c.Int(nullable: false, identity: true),
            //            LowerLimit = c.Double(nullable: false),
            //            UpperLimit = c.Double(nullable: false),
            //            TuitionRateFactor = c.Double(nullable: false),
            //            Discriminator = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.GPAStateId);
            
            //CreateTable(
            //    "dbo.StudentCards",
            //    c => new
            //        {
            //            StudentCardId = c.Int(nullable: false, identity: true),
            //            StudentId = c.Int(nullable: false),
            //            CardNumber = c.Long(nullable: false),
            //        })
            //    .PrimaryKey(t => t.StudentCardId)
            //    .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
            //    .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.NextStudentNumbers",
                c => new
                    {
                        NextStudentNumberId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextStudentNumberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registrations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCards", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Students", "GPAStateId", "dbo.GPAStates");
            DropForeignKey("dbo.Registrations", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "ProgramId", "dbo.Programs");
            DropIndex("dbo.StudentCards", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "ProgramId" });
            DropIndex("dbo.Students", new[] { "GPAStateId" });
            DropIndex("dbo.Registrations", new[] { "CourseId" });
            DropIndex("dbo.Registrations", new[] { "StudentId" });
            DropIndex("dbo.Courses", new[] { "ProgramId" });
            DropTable("dbo.NextStudentNumbers");
            DropTable("dbo.StudentCards");
            DropTable("dbo.GPAStates");
            DropTable("dbo.Students");
            DropTable("dbo.Registrations");
            DropTable("dbo.Programs");
            DropTable("dbo.Courses");
        }
    }
}
