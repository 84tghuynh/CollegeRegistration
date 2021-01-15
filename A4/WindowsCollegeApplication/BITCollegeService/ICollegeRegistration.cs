using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICollegeRegistration" in both code and config file together.
    [ServiceContract]
    public interface ICollegeRegistration
    {

        /// <summary>
        /// Registration record from the database,
        /// Then remove the record from the database.
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        [OperationContract]
        Boolean dropCourse(int registrationId);

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
        [OperationContract]
        int registerCourse(int studentId, int courseId, string notes);

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
        [OperationContract]
        void updateGrade(double grade, int registrationId, string notes);

    }
}
