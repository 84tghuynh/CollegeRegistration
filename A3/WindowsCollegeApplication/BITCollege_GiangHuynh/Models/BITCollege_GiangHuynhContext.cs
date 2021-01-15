using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BITCollege_GiangHuynh.Models
{
    public class BITCollege_GiangHuynhContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BITCollege_GiangHuynhContext() : base("name=BITCollege_GiangHuynhContext")
        {
        }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.GPAState> GPAStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.Program> Programs { get; set; }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.Registration> Registrations { get; set; }

        // Add DBSet entries for the subtypes of Abstract classes: Course and GPAStates. To access tables in Databases
        // For Course subtype
        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.GradedCourse> GradedCourses { get; set; }
        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.AuditCourse> AuditCourses { get; set; }
        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.MasteryCourse> MasteryCourses { get; set; }

        // For GPAState subtype
        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.SuspendedState> SuspendedStates { get; set; }
        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.ProbationState> ProbationStates { get; set; }
        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.RegularState> RegularStates { get; set; }
        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.HonoursState> HonoursStates { get; set; }

        // Add DBSet entries for the support classes. To access tables in Databases
        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.NextStudentNumber> NextStudentNumbers { get; set; }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.NextRegistrationNumber> NextRegistrationNumbers { get; set; }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.NextGradedCourse> NextGradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.NextAuditCourse> NextAuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.NextMasteryCourse> NextMasteryCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_GiangHuynh.Models.StudentCard> StudentCards { get; set; }
    }
}
