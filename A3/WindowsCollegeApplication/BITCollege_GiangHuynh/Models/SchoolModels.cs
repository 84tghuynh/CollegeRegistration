using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility;
using System.Data.SqlClient;
using System.Data;

namespace BITCollege_GiangHuynh.Models
{
    /// <summary>
    /// GPAState model corresponds with GPAStates table in database
    /// </summary>
    public abstract class GPAState
    {
        // Declare to use to achieve instance of the derived classes
        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int GPAStateId { get; set; }
        /// <summary>
        /// 2 decimal placess
        /// </summary>
        [Required]
        [Display(Name = "Lower\nLimit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public double LowerLimit { get; set; }

        [Required]
        [Display(Name = "Upper\nLimit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public double UpperLimit { get; set; }

        [Required]
        [Display(Name = "Tuition\nRate\nFactor")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F3}")]
        public double TuitionRateFactor { get; set; }

        /// <summary>
        /// Readonly Property
        /// Return Name of State
        /// Ex: Suspended, Probation, Regular, Honours
        /// </summary>
        [Display(Name = "GPA\nState")]
        public string Description
        {
            get
            {
                return BusinessRules.extractDescription(this.GetType().Name, "State");
            }
        }

        /// <summary>
        ///  The tuitionRateAdjustment method will be implemented in their derived class
        ///  The subtype class will override this method
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public virtual double tuitionRateAdjustment(Student student)
        {
            return 0;
        }
        /// <summary>
        /// The stateChangeCheck methods will be implemented in their derived class. 
        /// It will be overriden by the derived classes.
        /// </summary>
        /// <param name="student"></param>
        public virtual void stateChangeCheck(Student student) { }

        //navigation properties
        //use ICollection for * on class diagram
        public virtual ICollection<Student> Student { get; set; }
    }

    /// <summary>
    /// SuspendedState model corresponds with SuspendedStates table in database
    /// </summary>
    public class SuspendedState : GPAState
    {
        private static SuspendedState suspendedState;
        /// <summary>
        /// GradePointAverage boundary for the SuspendedState between 0.00 and 1.00
        /// Tuition Rate Factor of the SuspendedState is 1.1
        /// </summary>
        private SuspendedState()
        {
            this.LowerLimit = 0.00;
            this.UpperLimit = 1.00;
            this.TuitionRateFactor = 1.1;
        }

        /// <summary>
        /// Singleton implementation for SuspendedState
        /// It just returns one and only SuspendedState object.
        /// This Object is retrieved from Memmory or Database
        /// </summary>
        /// <returns></returns>
        public static SuspendedState getInstance()
        {
            if (suspendedState == null)
            {
                suspendedState = db.SuspendedStates.SingleOrDefault();
                if (suspendedState != null) return suspendedState;

                suspendedState = new SuspendedState();

                // Populate this object to Database - one record in GPAStates table (Just One time)
                db.GPAStates.Add(suspendedState);
                db.SaveChanges();
            }
            return suspendedState;
        }

        /// <summary>
        /// The tuitionRateAdjustment method will return a new TuitionRateFactor. 
        /// The new TuitionRateFactor is adjusted by following rules:
        /// 
        /// The Student’s GradePointAverage has dropped below 0.75, 
        /// each newly registered course is increased by 2% above default premium.
        /// 
        /// The Student’s GradePointAverage has dropped below 0.50, 
        /// each newly registered course is increased by 5% above default premium.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override double tuitionRateAdjustment(Student student)
        {
            if (student.GradePointAverage >= 0.50 && student.GradePointAverage < 0.75)
                return this.TuitionRateFactor + 0.02;
            else if (student.GradePointAverage < 0.50)
                return this.TuitionRateFactor + 0.05;

            return this.TuitionRateFactor;
        }

        /// <summary>
        /// The stateChangeCheck method allow student to move to the next ProbationState
        /// Or still keep the student in SuspendedState
        /// </summary>
        /// <param name="student"></param>
        //public override void stateChangeCheck_V0(Student student)
        //{
        //    if (student.GradePointAverage > this.UpperLimit)
        //        ProbationState.getInstance().stateChangeCheck(student);
        //    else
        //        student.GPAStateId = SuspendedState.getInstance().GPAStateId;
        //}

        /// <summary>
        /// The stateChangeCheck method allow student to move to the next ProbationState
        /// Or still keep the student in SuspendedState
        /// </summary>
        /// <param name="student"></param>
        public override void stateChangeCheck(Student student)
        {
            if (student.GradePointAverage > this.UpperLimit)
                student.GPAStateId = ProbationState.getInstance().GPAStateId;
            else
                student.GPAStateId = SuspendedState.getInstance().GPAStateId;
        }
    }

    /// <summary>
    /// ProbationState model corresponds with ProbationStates table in database
    /// </summary>
    public class ProbationState : GPAState
    {
        private static ProbationState probationState;

        /// <summary>
        /// GradePointAverage boundary for the ProbationState between 1.00 and 2.00
        /// Tuition Rate Factor of the ProbationState is 1.075
        /// </summary>
        private ProbationState()
        {
            this.LowerLimit = 1.00;
            this.UpperLimit = 2.00;
            this.TuitionRateFactor = 1.075;
        }

        /// <summary>
        /// Singleton implementation for ProbationState
        /// It just returns one and only ProbationState object.
        /// This Object is retrieved from Memmory or Database
        /// </summary>
        /// <returns></returns>
        public static ProbationState getInstance()
        {
            if (probationState == null)
            {
                probationState = db.ProbationStates.SingleOrDefault();
                if (probationState != null) return probationState;

                probationState = new ProbationState();

                // Populate this object to Database - one record in GPAStates table (Just One time)
                db.GPAStates.Add(probationState);
                db.SaveChanges();
            }
            return probationState;
        }

        /// <summary>
        /// the tuitionRateAdjustment method will be implemented as the following rules below
        /// The Student has completed 5 or more courses, 
        /// tuition for each newly registered course is increased by only 3.5%.
        /// The new tuitionRateFactor is 1.035
        /// 
        /// Else, ProbationState students will pay an additional 7.5% for each newly registered course.
        /// The tuitionRateFactor is 1.075
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override double tuitionRateAdjustment(Student student)
        {
            IQueryable<Registration> completedCourses = from registration in db.Registrations
                                                        where registration.StudentId == student.StudentId
                                                        where registration.Grade != null
                                                        select registration;

            //Lambda
            //completedCourses = db.Registrations.Where(reg => reg.StudentId == student.StudentId)
            //                                   .Where(reg => reg.Grade != null);

            //Lambda
            //completedCourses = db.Registrations.Where(reg => reg.StudentId == student.StudentId && reg.Grade != null);


            if (completedCourses.Count() >= 5)
            {
                return 1.035;
            }

            return this.TuitionRateFactor;
        }

        /// <summary>
        /// The stateChangeCheck method allow student
        ///     to move to the next RegularState
        /// Or  to move back the previous SuspendedState
        /// Or still keep the student in ProbationState
        /// GradePointAverage == null moves the student to SuspendedState
        /// </summary>
        /// <param name="student"></param>
        public override void stateChangeCheck(Student student)
        {
            if (student.GradePointAverage > this.UpperLimit)
                // RegularState.getInstance().stateChangeCheck(student);
                student.GPAStateId = RegularState.getInstance().GPAStateId;
            else if (student.GradePointAverage < this.LowerLimit)
                //SuspendedState.getInstance().stateChangeCheck(student);
                student.GPAStateId = SuspendedState.getInstance().GPAStateId;
            else if (student.GradePointAverage != null)
                student.GPAStateId = ProbationState.getInstance().GPAStateId;
            else if (student.GradePointAverage == null)
                student.GPAStateId = SuspendedState.getInstance().GPAStateId;
        }
    }

    /// <summary>
    /// RegularState model corresponds with RegularStates table in database
    /// </summary>
    public class RegularState : GPAState
    {
        private static RegularState regularState;

        /// <summary>
        /// GradePointAverage boundary for the RegularState between 2.00 and 3.70
        /// Tuition Rate Factor of the RegularState is 1.00
        /// </summary>
        private RegularState()
        {
            this.LowerLimit = 2.00;
            this.UpperLimit = 3.70;
            this.TuitionRateFactor = 1.00;
        }

        /// <summary>
        /// Singleton implementation for RegularState
        /// It just returns one and only RegularState object.
        /// This Object is retrieved from Memmory or Database
        /// </summary>
        /// <returns></returns>
        public static RegularState getInstance()
        {
            if (regularState == null)
            {
                regularState = db.RegularStates.SingleOrDefault();
                if (regularState != null) return regularState;

                regularState = new RegularState();

                // Populate this object to Database - one record in GPAStates table (Just One time)
                db.GPAStates.Add(regularState);
                db.SaveChanges();
            }
            return regularState;
        }

        /// <summary>
        /// For Regular GPAState Students, the tuition for each newly registered course 
        /// is the actual TuitionAmount for the specified course
        /// That is, no adjustments are made to the cost of the tuition.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override double tuitionRateAdjustment(Student student)
        {
            return this.TuitionRateFactor;
        }

        /// <summary>
        /// The stateChangeCheck method allow student
        ///     to move to the next HonoursState
        /// Or  to move back the previous ProbationState
        /// Or still keep the student in RegularState
        /// GradePointAverage == null moves the student to SuspendedState
        /// </summary>
        /// <param name="student"></param>
        public override void stateChangeCheck(Student student)
        {
            if (student.GradePointAverage > this.UpperLimit)
                // HonoursState.getInstance().stateChangeCheck(student);
                student.GPAStateId = HonoursState.getInstance().GPAStateId;
            else if (student.GradePointAverage < this.LowerLimit)
                // ProbationState.getInstance().stateChangeCheck(student);
                student.GPAStateId = ProbationState.getInstance().GPAStateId;
            else if (student.GradePointAverage != null)
                student.GPAStateId = RegularState.getInstance().GPAStateId;
            else if (student.GradePointAverage == null)
                student.GPAStateId = SuspendedState.getInstance().GPAStateId;
        }
    }

    /// <summary>
    /// HonoursState model corresponds with HonoursStates table in database
    /// </summary>
    public class HonoursState : GPAState
    {
        private static HonoursState honoursState;

        /// <summary>
        /// GradePointAverage boundary for the HonoursState between 3.70 and 4.50
        /// Tuition Rate Factor of the HonoursState is 0.90
        /// </summary>
        private HonoursState()
        {
            this.LowerLimit = 3.70;
            this.UpperLimit = 4.50;
            this.TuitionRateFactor = 0.90;
        }

        /// <summary>
        /// Singleton implementation for HonoursState
        /// It just returns one and only HonoursState object.
        /// This Object is retrieved from Memmory or Database
        /// </summary>
        /// <returns></returns>
        public static HonoursState getInstance()
        {
            if (honoursState == null)
            {
                honoursState = db.HonoursStates.SingleOrDefault();

                if (honoursState != null) return honoursState;

                honoursState = new HonoursState();

                // Populate this object to Database - one record in GPAStates table (Just One time)
                db.GPAStates.Add(honoursState);
                db.SaveChanges();

            }
            return honoursState;
        }

        /// <summary>
        /// the tuitionRateAdjustment method will be implemented as the following rules below
        /// Each newly registered course is already discounted by 10%.
        /// Having completed 5 or more courses, tuition for each newly registered course is discounted by 15%.
        /// If the Student’s GPA is above 4.25, the student will receive an additional 2% discount.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override double tuitionRateAdjustment(Student student)
        {
            IQueryable<Registration> completedCourses = from registration in db.Registrations
                                                        where registration.StudentId == student.StudentId
                                                        where registration.Grade != null
                                                        select registration;

            if (completedCourses.Count() >= 5)
            {
                if (student.GradePointAverage <= 4.25)
                    return 0.85;
                else return 0.83;
            }

            if (student.GradePointAverage > 4.25) return 0.88;

            return this.TuitionRateFactor;
        }
        /// <summary>
        /// The stateChangeCheck method allow student
        ///  to move back the previous ProbationState
        /// Or still keep the student in HonoursState
        /// GradePointAverage == null moves the student to SuspendedState
        /// </summary>
        /// <param name="student"></param>
        public override void stateChangeCheck(Student student)
        {
            if (student.GradePointAverage < this.LowerLimit)
                // RegularState.getInstance().stateChangeCheck(student);
                student.GPAStateId = RegularState.getInstance().GPAStateId;
            else if (student.GradePointAverage != null)
                student.GPAStateId = HonoursState.getInstance().GPAStateId;
            else if (student.GradePointAverage == null)
                student.GPAStateId = SuspendedState.getInstance().GPAStateId;
        }
    }


    /// <summary>
    /// Student model corresponds with Students table in database
    /// </summary>
    public class Student
    {
        // Declare to use to achieve instance of the derived classes
        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StudentId { get; set; }

        //Foreign key name must match associated nav.prop.name
        [Required]
        [ForeignKey("GPAState")]
        public int GPAStateId { get; set; }

        [ForeignKey("Program")]
        public int? ProgramId { get; set; }

        //[Required]
        //[Range(10000000, 99999999)]
        [Display(Name = "Student\nNumber")]
        public long StudentNumber { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "First\nName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "Last\nName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string Address { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        [RegularExpression("^AB|BC|MB|NB|NL|NS|ON|PE|QC|SK|NT|NU|YT$", ErrorMessage = "Need TWO uppercase letters")]
        public string Province { get; set; }

        [Required]
        [StringLength(7)]
        [Display(Name = "Postal\nCode")]
        [RegularExpression("^[A-CEGHJ-NPR-TVXY][0-9][A-CEGHJ-NPR-TV-Z]\\s[0-9][A-CEGHJ-NPR-TV-Z][0-9]$", ErrorMessage = "7 uppercase characters,do not include the letters D, F, I, O, Q or U, " +
            "<br/> the first position also does not make use of the letters W or Z. <br/>For example: A0B 4N5")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Date\nCreated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateCreated { get; set; }

        [Range(0, 4.5)]
        [Display(Name = "Grade\nPoint\nAverage")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public double? GradePointAverage { get; set; }

        [Display(Name = "Outstanding\nFees")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double OutstandingFees { get; set; }
        public string Notes { get; set; }

        /// <summary>
        /// For the Student Model, the FullName and FullAddress properties are readonly. 
        /// Implement these methods such that they return values as shown in Figure 3
        /// </summary>
        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        /// <summary>
        /// For the Student Model, the FullName and FullAddress properties are readonly. 
        /// Implement these methods such that they return values as shown in Figure 3
        /// </summary>
        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return string.Format("{0} {1} {2}, {3}", Address, City, Province, PostalCode);
            }
        }

        /// <summary>
        /// Set StudentNumber use the static method nextNumber
        /// The StudentNumber start from 20000000. 
        /// This value was created in NextStudentNumbers singleton class
        /// nextNumber's parameter corresponds with the name of table in the Database 
        /// </summary>
        public void setNextStudentNumber()
        {
            this.StudentNumber = (long)StoredProcedures.nextNumber("NextStudentNumbers");
        }

        /// <summary>
        /// The changeState method will initiate the process of ensuring that 
        /// the Student is always associated with the correct state
        /// This method will eventually be called whenever the GradePointAverage of a Student changes
        /// </summary>
        //public void changeState_v0()
        //{
        //    GPAState oldState, newState;
        //    //oldState = (from state in db.GPAStates
        //    //                    where state.GPAStateId == this.GPAStateId
        //    //                    select state).SingleOrDefault();

        //    oldState = (db.GPAStates.Where(state => state.GPAStateId == this.GPAStateId)).SingleOrDefault();
        //    //do
        //    //{
        //    //    newState = oldState;
        //        oldState.stateChangeCheck(this);
        //    //    oldState = (from state in db.GPAStates
        //    //                where state.GPAStateId == this.GPAStateId
        //    //                select state).SingleOrDefault();
        //    //}
        //    //while (oldState != newState);
        //}

        /// <summary>
        /// The changeState method will initiate the process of ensuring that 
        /// the Student is always associated with the correct state
        /// This method will eventually be called whenever the GradePointAverage of a Student changes
        /// This method is called in Student Controller for Create & Edit method
        /// </summary>
        public void changeState()
        {
            GPAState oldState, newState;
            //oldState = (from state in db.GPAStates
            //                    where state.GPAStateId == this.GPAStateId
            //                    select state).SingleOrDefault();

            oldState = (db.GPAStates.Where(state => state.GPAStateId == this.GPAStateId)).SingleOrDefault();
            do
            {
                newState = oldState;
                oldState.stateChangeCheck(this);
                oldState = (from state in db.GPAStates
                            where state.GPAStateId == this.GPAStateId
                            select state).SingleOrDefault();
            }
            while (oldState != newState);
        }

        //navigation properties
        //use ICollection for * on class diagram
        public virtual Program Program { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }
        public virtual ICollection<StudentCard> studentCard { get; set; }


        //navigation property - coded for a 1 on the class diagram
        //virtual will allow for .notation access to related tables
        public virtual GPAState GPAState { get; set; }
    }
    /// <summary>
    /// Program model corresponds with Programs table in database
    /// </summary>
    public class Program
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProgramId { get; set; }

        [Required]
        [Display(Name = "Program")]
        public string ProgramAcronym { get; set; }

        [Required]
        [Display(Name = "Program\nName")]
        public string Description { get; set; }

        //navigation properties
        //use ICollection for * on class diagram
        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
    /// <summary>
    /// Registration corresponds with Registrations in table
    /// </summary>
    public class Registration
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RegistrationId { get; set; }

        //[Required]
        [Display(Name = "Registration\nNumber")]
        public long RegistrationNumber { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Registration\nDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime RegistrationDate { get; set; }

        [Range(0, 100)]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Ungraded")]
        public double? Grade { get; set; }

        public string Notes { get; set; }

        /// <summary>
        /// Set RegistrationNumber use the static method nextNumber
        /// The RegistrationNumber start from 700. 
        /// This value was created in NextRegistrationNumber singleton class
        /// nextNumber's parameter corresponds with the name of table in the Database
        /// </summary>
        public void setNextRegistrationNumber()
        {
            this.RegistrationNumber = (long)StoredProcedures.nextNumber("NextRegistrationNumbers");
        }

        //navigation property - coded for a 1 on the class diagram
        //virtual will allow for .notation access to related tables
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }

    /// <summary>
    /// Course model correponds with Courses table in database
    /// </summary>
    public abstract class Course
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseId { get; set; }

        [ForeignKey("Program")]
        public int? ProgramId { get; set; } 

        [Display(Name = "Course\nNumber")]
        public String CourseNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Credit\nHours")]
        public double CreditHours { get; set; }

        [Required]
        [Display(Name = "Tuition\nAmount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public double TuitionAmount { get; set; }

        /// <summary>
        /// For the Course model, the CourseType property is readonly. 
        /// This method will return type of Course.
        /// Ex: Graded, Audit, Mastery
        /// </summary>
        [Display(Name = "Course\nType")]
        public string CourseType
        {
            get
            {
                return BusinessRules.extractDescription(this.GetType().Name, "Course");
            }
        }

        public string Notes { get; set; }
        /// <summary>
        /// This method will be implemented in the derived classes to set Next coursenumber
        /// </summary>
        public abstract void setNextCourseNumber();

        //navigation properties
        //use ICollection for * on class diagram
        public virtual Program Program { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }
    }

    /// <summary>
    /// GradedCourse model correponds with GradedCourses in database
    /// </summary>
    public class GradedCourse : Course
    {
        [Required]
        [Display(Name = "Assignment\nWeight")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        public double AssignmentWeight { get; set; }

        [Required]
        [Display(Name = "Midterm\nWeight")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        public double MidtermWeight { get; set; }

        [Required]
        [Display(Name = "Final\nWeight")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        public double FinalWeight { get; set; }

        /// <summary>
        /// The CourseNumber to the value of “G-“ followed by the value 
        /// returned from the nextNumber static method defined in the static class StoredProcedures.
        /// E.g. “G-200000”
        /// The Graded CourseNumber starts from 200000. 
        /// This value was created in the NextGradedCourse singleton class
        /// nextNumber's parameter corresponds with the name of table in the Database
        /// </summary>
        public override void setNextCourseNumber()
        {
            this.CourseNumber = "G-" + StoredProcedures.nextNumber("NextGradedCourses").ToString();
        }
    }

    /// <summary>
    /// AuditCourse model correponds with AuditCourses in database
    /// </summary>
    public class AuditCourse : Course
    {
        /// <summary>
        /// The CourseNumber to the value of “A-“ followed by the value 
        /// returned from the nextNumber static method defined in the static class StoredProcedures.
        /// E.g. “A-2000”
        /// The Audit CourseNumber starts from 2000. 
        /// This value was created in the NextAuditCourse singleton class
        /// nextNumber's parameter corresponds with the name of table in the Database
        /// </summary>
        public override void setNextCourseNumber()
        {
            this.CourseNumber = "A-" + StoredProcedures.nextNumber("NextAuditCourses").ToString();
        }
    }

    /// <summary>
    /// MasteryCourse model correponds with MasteryCourses in database
    /// </summary>
    public class MasteryCourse : Course
    {
        [Required]
        [Display(Name = "Maximum\nAttempts")]
        public int MaximumAttempts { get; set; }

        /// <summary>
        /// The CourseNumber to the value of “A-“ followed by the value 
        /// returned from the nextNumber static method defined in the static class StoredProcedures.
        /// E.g. “M-20000”
        /// The Mastery CourseNumber starts from 20000. 
        /// This value was created in the NextMasteryCourse singleton class
        /// nextNumber's parameter corresponds with the name of table in the Database
        /// </summary>
        public override void setNextCourseNumber()
        {
            this.CourseNumber = "M-" + StoredProcedures.nextNumber("NextMasteryCourses").ToString();
        }
    }

    /// <summary>
    /// This class is reponsible for returning a next student number 
    /// The start value is 20000000
    /// This is a singleton class
    /// NextStudentNumber model correponds with NextStudentNumbers in database
    /// </summary>
    public class NextStudentNumber
    {
        // Declare to use to achieve instance of the classes
        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();
        private static NextStudentNumber nextStudentNumber;

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextStudentNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        /// <summary>
        /// The start value of the next Available number is 20000000
        /// </summary>
        private NextStudentNumber()
        {
            this.NextAvailableNumber = 20000000;
        }

        /// <summary>
        /// This method is a Singleton implementation for NextStudentNumber class
        /// It just returns one and only NextStudentNumber object.
        /// This Object is retrieved from Memmory or Database
        /// </summary>
        /// <returns></returns>
        public static NextStudentNumber getInstance()
        {

            if (nextStudentNumber == null)
            {
                nextStudentNumber = db.NextStudentNumbers.SingleOrDefault();
                if (nextStudentNumber != null) return nextStudentNumber;

                nextStudentNumber = new NextStudentNumber();

                // Populate this object to Database - one record in NextStudentNumbers table (Just One time)
                db.NextStudentNumbers.Add(nextStudentNumber);
                db.SaveChanges();
            }
            return nextStudentNumber;
        }
    }

    /// <summary>
    /// This class is reponsible for returning a next Graded Course number 
    /// The start value is 200000
    /// This is a singleton class
    /// NextGradedCourse model correponds with NextGradedCourses in database
    /// </summary>
    public class NextGradedCourse
    {
        // Declare to use to achieve instance of the classes
        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();
        private static NextGradedCourse nextGradedCourse;

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextGradedCourseId { get; set; }

        public long NextAvailableNumber { get; set; }

        /// <summary>
        /// The start value of the next Available number is 200000
        /// </summary>
        private NextGradedCourse()
        {
            this.NextAvailableNumber = 200000;
        }

        /// <summary>
        /// This method is a Singleton implementation for NextGradedCourse class
        /// It just returns one and only NextGradedCourse object.
        /// This Object is retrieved from Memmory or Database
        /// </summary>
        /// <returns></returns>
        public static NextGradedCourse getInstance()
        {
            if (nextGradedCourse == null)
            {
                nextGradedCourse = db.NextGradedCourses.SingleOrDefault();
                if (nextGradedCourse != null) return nextGradedCourse;

                nextGradedCourse = new NextGradedCourse();

                // Populate this object to Database - one record in NextGradedCourse table (Just One time)
                db.NextGradedCourses.Add(nextGradedCourse);
                db.SaveChanges();
            }
            return nextGradedCourse;
        }
    }

    /// <summary>
    /// This class is reponsible for returning a next Audit Course number 
    /// The start value is 2000
    /// This is a singleton class
    /// NextAuditCourse model correponds with NextAuditCourses in database
    /// </summary>
    public class NextAuditCourse
    {
        // Declare to use to achieve instance of the classes
        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();
        private static NextAuditCourse nextAuditCourse;

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextAuditCourseId { get; set; }

        public long NextAvailableNumber { get; set; }

        /// <summary>
        /// Default value of the next Availbe number is 2000
        /// </summary>
        private NextAuditCourse()
        {
            this.NextAvailableNumber = 2000;
        }

        /// <summary>
        /// This method is a Singleton implementation for NextAuditCourse class
        /// It just returns one and only NextAuditCourse object.
        /// This Object is retrieved from Memmory or Database
        /// </summary>
        /// <returns></returns>
        public static NextAuditCourse getInstance()
        {
            if (nextAuditCourse == null)
            {
                nextAuditCourse = db.NextAuditCourses.SingleOrDefault();
                if (nextAuditCourse != null) return nextAuditCourse;

                nextAuditCourse = new NextAuditCourse();

                // Populate this object to Database - one record in NextAuditCourse table (Just One time)
                db.NextAuditCourses.Add(nextAuditCourse);
                db.SaveChanges();
            }
            return nextAuditCourse;
        }
    }

    /// <summary>
    /// This class is reponsible for returning a next Mastery Course number 
    /// The start value is 20000
    /// This is a singleton class
    /// NextMasteryCourse model correponds with NextMasteryCourse in database
    /// </summary>
    public class NextMasteryCourse
    {
        // Declare to use to achieve instance of the classes
        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();
        private static NextMasteryCourse nextMasteryCourse;

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextMasteryCourseId { get; set; }

        public long NextAvailableNumber { get; set; }

        /// <summary>
        /// Default value of the next Availbe number is 20000
        /// </summary>
        private NextMasteryCourse()
        {
            this.NextAvailableNumber = 20000;
        }

        /// <summary>
        /// This method is a Singleton implementation for NextMasteryCourse class
        /// It just returns one and only NextMasteryCourse object.
        /// This Object is retrieved from Memmory or Database
        /// </summary>
        /// <returns></returns>
        public static NextMasteryCourse getInstance()
        {
            if (nextMasteryCourse == null)
            {
                nextMasteryCourse = db.NextMasteryCourses.SingleOrDefault();
                if (nextMasteryCourse != null) return nextMasteryCourse;

                nextMasteryCourse = new NextMasteryCourse();

                // Populate this object to Database - one record in NextAuditCourse table (Just One time)
                db.NextMasteryCourses.Add(nextMasteryCourse);
                db.SaveChanges();
            }
            return nextMasteryCourse;
        }
    }

    /// <summary>
    /// This class is reponsible for returning a next Mastery Course number 
    /// The start value is 700
    /// This is a singleton class
    /// NextRegistrationNumber model correponds with NextRegistrationNumbers in database
    /// </summary>
    public class NextRegistrationNumber
    {
        // Declare to use to achieve instance of the classes
        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();
        private static NextRegistrationNumber nextRegistrationNumber;

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextRegistrationNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        /// <summary>
        /// The start value of the next Available number is 700
        /// </summary>
        private NextRegistrationNumber()
        {
            this.NextAvailableNumber = 700;
        }

        /// <summary>
        /// This method is a Singleton implementation for NextRegistrationNumber class
        /// It just returns one and only NextRegistrationNumber object.
        /// This Object is retrieved from Memmory or Database
        /// </summary>
        /// <returns></returns>
        public static NextRegistrationNumber getInstance()
        {
            if (nextRegistrationNumber == null)
            {
                nextRegistrationNumber = db.NextRegistrationNumbers.SingleOrDefault();
                if (nextRegistrationNumber != null) return nextRegistrationNumber;

                nextRegistrationNumber = new NextRegistrationNumber();

                // Populate this object to Database - one record in NextStudentNumbers table (Just One time)
                db.NextRegistrationNumbers.Add(nextRegistrationNumber);
                db.SaveChanges();
            }
            return nextRegistrationNumber;
        }
    }

    /// <summary>
    /// StudentCard model correponds with StudentCards in database
    /// </summary>
    public class StudentCard
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentCardId { get; set; }

        [Required]
        [ForeignKey("student")]
        [Display(Name = "Student")]
        public int StudentId { get; set; }

        [Display(Name = "Card\nNumber")]
        public long CardNumber { get; set; }

        //navigation property - coded for a 1 on the class diagram
        //virtual will allow for .notation access to related tables
        public virtual Student student { get; set; }

    }
    /// <summary>
    /// This class is reponsible for working with the store procedure in the database
    /// </summary>
    public static class StoredProcedures
    {
        /// <summary>
        /// This method calls the "next_number" stored procedure in database to get the next number.
        /// The next number of each type of table is different.
        /// Ex: Table Name:  NextStudentNumbers, NextRegistrationNumbers, 
        /// NextGradedCourses, NextAuditCourses, NextMasteryCourses
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static long? nextNumber(string tableName)
        {
            // Prepare connection
            SqlConnection connection = new SqlConnection("Data Source=localhost; Initial Catalog = BITCollege_GiangHuynhContext; Integrated Security = True");

            long? returnValue = 0;

            // Create a SQL command use the next_number store procedure
            SqlCommand storedProcedure = new SqlCommand("next_number", connection);
            // Declare Command Type is StoredProcedure 
            // To specify "next_number" in SqlCommand constructor above is a store procedure
            storedProcedure.CommandType = CommandType.StoredProcedure;
            // Pass tablename as a parameter into store procedure
            storedProcedure.Parameters.AddWithValue("@TableName", tableName);
            // Prepare an output parameter
            SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            // Add the output parameter to SQL command
            storedProcedure.Parameters.Add(outputParameter);
            // Connection to Database
            connection.Open();
            // Execute the store procedure
            storedProcedure.ExecuteNonQuery();
            connection.Close();
            // Retrieve the retrunned value from the output parameter of the store procedure
            // Returned value could be null
            returnValue = (long?)outputParameter.Value;
            return returnValue;
        }
    }
}