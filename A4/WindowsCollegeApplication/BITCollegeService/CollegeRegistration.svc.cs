using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BITCollege_GiangHuynh.Models;
using Utility;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CollegeRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CollegeRegistration.svc or CollegeRegistration.svc.cs at the Solution Explorer and start debugging.
    public class CollegeRegistration : ICollegeRegistration
    {
        //Instance of DataContext object
        BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();
        public const int SUCCESS_REGISTRATION = 0;
        public const int ERROR_UPDATE = -300;
        public const int EXCEEDED_MAXATTEMPTS = -200;
        public const int UNGRADED_REGISTRATION = -100;

        /// <summary>
        /// Use LINQ-SQL-Server (Lambda or Traditional Syntax) to retrieve the Registration record from the database which corresponds to the method argument.
        ///  Remove the record from the database.
        ///  If an exception occurs, return a value of false, otherwise return a value of true.
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        public Boolean dropCourse(int registrationId)
        {
            try
            {

                Registration registration = db.Registrations
                                            .Where(x => x.RegistrationId == registrationId)
                                            .SingleOrDefault();
                db.Registrations.Remove(registration);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// This method will make use of various return codes to indicate success or failure.
        /// In the event, that the course registration fails, the return code will indicate the reason
        /// A registration will not be permitted under the following circumstances.
        ///     o When the student already has an incomplete registration for the same course. A registration is incomplete if the grade value is null
        ///     o When a student is registering for a Mastery course and this registration would exceed the maximum attempts value for the Mastery course.
        ///     Note: all previous attempts would be stored in the Registrations table.
        /// If the Registration is valid, create a Registration object and populate its properties with values pertinent to this registration.
        ///     o For the StudentId, CourseId and Notes fields, use the method arguments
        ///     o For the RegistrationDate, use today’s date
        ///     o Use predefined functionality to generate the RegistrationNumber
        /// Add the Registration object to the database and persist to the database
        /// Return codes:
        ///     o If the registration is successful, return a value of 0
        ///     o If an exception occurs while updating, return a value of -300
        ///     o If the student has exceeded the MaximumAttempts of a Mastery course, return a value of -200
        ///     o If the student already has an ungraded registration for this course, return a value of -100
        /// Update the Student record such that :
        ///     o The TuitionFees associated with the course to which the Registration applies, are added to the OutstandingFees for the student.
        ///            Ensure that the student is charged the appropriate fees based on the RateAdjustment method of the Student’s GPAState
        ///     o Persist this change to the database
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public int registerCourse(int studentId, int courseId, string notes)
        {
             IQueryable<Registration> registrations = db.Registrations
                                             .Where(x => x.StudentId == studentId)
                                             .Where(x => x.CourseId == courseId);

            int count = 0;
            foreach(Registration registration in registrations)
            {
                if (registration.Grade == null) return UNGRADED_REGISTRATION;

                if (registration.Course.CourseType == "Mastery") count++;
            }

            if( count > 0 )
            {
                MasteryCourse course = (MasteryCourse)db.Courses
                                        .Where(x => x.CourseId == courseId)
                                        .SingleOrDefault();

                if (count >= course.MaximumAttempts) return EXCEEDED_MAXATTEMPTS;
            }
         
            try
            {
                Registration newRegistration = new Registration();

                newRegistration.StudentId = studentId;
                newRegistration.CourseId = courseId;
                newRegistration.Notes = notes;
                newRegistration.RegistrationDate = DateTime.Today;
                newRegistration.setNextRegistrationNumber();
                db.Registrations.Add(newRegistration);
                db.SaveChanges();

                Student student = db.Students
                                    .Where(x => x.StudentId == studentId)
                                    .SingleOrDefault();
                Course course = db.Courses
                                    .Where(x => x.CourseId == courseId)
                                    .SingleOrDefault();

                //student.OutstandingFees += 1;

                //student.OutstandingFees += student.GPAState.tuitionRateAdjustment(student) * newRegistration.Course.TuitionAmount;
                student.OutstandingFees += student.GPAState.tuitionRateAdjustment(student) * course.TuitionAmount;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
              
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ERROR_UPDATE;
            }

            return SUCCESS_REGISTRATION;
        }
        /// <summary>
        /// Use LINQ-SQL-Server (Lambda or Traditional Syntax) to retrieve the Registration record from the database 
        /// which corresponds to the method argument.
        ///   Set the Grade property of the Registration record to the value of the grade argument
        ///   Modify the Notes property with the value of the method argument.
        ///   Persist the updated grade to the database
        ///   Call the calculateGPA method (below) passing appropriate argument
        ///   Persist this change to the database
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="registrationId"></param>
        /// <param name="notes"></param>
        public void updateGrade(double grade, int registrationId, string notes)
        {
            try
            {

                Registration registration = db.Registrations
                                            .Where(x => x.RegistrationId == registrationId)
                                            .SingleOrDefault();

                registration.Grade = grade;
                registration.Notes = notes;
                System.Diagnostics.Debug.WriteLine(notes);

                db.SaveChanges();
                this.calculteGPA(registration.StudentId);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                 //e.Message;
                    
            }
        }
        /// <summary>
        ///  Use LINQ-SQL-Server (Lambda or Traditional Syntax) to obtain all Registration records containing a Grade value 
        ///   that is not null which belong to the student represented by the method argument
        ///  Iterate through all Registration records
        ///     o Obtain the Grade for the registration
        ///     o Obtain the Course information for the registration
        ///     o Use given functionality in the Utility project¡¦s BusinessRules class to :
        ///         - Determine the Course Type (as an Enum)
        ///         - Use the Course Type and grade to determine the corresponding Grade Point Value for the grade
        ///    o Exclude any Audit courses from the GPA calculation
        ///    o The GPA Formula is as follows:
        ///         - Multiply each registration¡¦s GradePointValue by the corresponding CreditHours
        ///         - Determine Total Grade Point Value by Accumulating the above value for all registrations
        ///         - Determine the Total Credit Hours by Accumulating the CreditHours for all registration
        ///         - Divide the Total Grade Point Value by the Total Credit Hours
        ///  Obtain the student record to which the newly calculated GPA applies
        ///  Set the GradePointAverage property of the Student record to the newly calculated GPA
        ///  Ensure that any changes to the Student’s GradePointAverage cause the student to be placed in the appropriate GradePointState
        ///  Persist these changes to the database.
        ///  Return the calculated Grade Point Average to the calling routine.
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>GPA</returns>
        public double calculteGPA(int studentId)
        {
            IQueryable<Registration> registrations = db.Registrations
                                                    .Where(x => x.StudentId == studentId)
                                                    .Where(x => x.Grade != null);

            double totalGradePoint = 0;
            double totalCreditHours = 0;

            int count = registrations.Count();

            foreach(Registration registration in registrations)
            {
                if(registration.Course.CourseType == "Graded")
                {
                    totalCreditHours += registration.Course.CreditHours;
                    totalGradePoint += BusinessRules.gradeLookup((double)registration.Grade, CourseType.GRADED) * registration.Course.CreditHours;
                }

                if (registration.Course.CourseType == "Mastery")
                {
                    totalCreditHours += registration.Course.CreditHours;
                    totalGradePoint += BusinessRules.gradeLookup((double)registration.Grade, CourseType.MASTERY) * registration.Course.CreditHours;
                }
            }
            double GPA = 0;
               
            if(totalCreditHours > 0) GPA = totalGradePoint / totalCreditHours;

            Student student = db.Students
                              .Where(x => x.StudentId == studentId)
                              .SingleOrDefault();

            student.GradePointAverage = GPA;
            student.changeState();
            //try
            //{
               
               
            //    //db.SaveChanges();
            //}
            //catch(Exception e)
            //{
            //    return ERROR_UPDATE;
            //}
           
            return GPA;
        }
    }
}
