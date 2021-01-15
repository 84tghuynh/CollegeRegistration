using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BITCollege_GiangHuynh;
using BITCollege_GiangHuynh.Models;
using Utility;

namespace WindowsApplication
{
    class Batch
    {
        private String inputFileName { get; set; }
        private String logFileName { get; set; }
        private String logData { get; set; }

        public const int SUCCESS_REGISTRATION = 0;
        public const int ERROR_UPDATE = -300;
        public const int EXCEEDED_MAXATTEMPTS = -200;
        public const int UNGRADED_REGISTRATION = -100;

        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        /// <summary>
        ///  This method is used to verify the attributes of the xml file’s root element. 
        ///   If any of the attributes produce an error, the file is NOT to be processed.
        ///     o If any of the validation described below fails, throw an exception with an appropriate message 
        ///       otherwise proceed with the validation
        ///  Define an XDocument object and populate this object with the contents of the current file (inputFileName)
        ///  Define an XElement object and populate this object with the data found within 
        ///   the student_update element of the xml file
        ///   
        /// Validation Requirements
        ///  The XElement object must have 3 attributes
        ///     o Note: You can assume if there are 3 attributes, the attribute names are correct
        ///  The date attribute of the XElement object must be equal to today’s date
        ///  The program attribute must match one of the program acronym values within the Program Entity class
        ///     o Important: The Windows application includes class called Program. As such, 
        ///     when referring to the Program entity class, it will be necessary to qualify the statement, 
        ///     otherwise, the compiler will assume the Windows project’s Program class.
        ///  The checksum attribute must match the sum of all student_no elements in the file
        ///  If any of the above validation fails, throw an exception with an appropriate message
        /// </summary>
        private void processHeader()
        {
            try
            {
                XDocument xDocDetails = XDocument.Load(this.inputFileName);
                XElement student_updateElement = xDocDetails.Element("student_update");

                if (student_updateElement.Attributes().Count() == 3)
                {
                    // Date Validation
                    XAttribute dateAttr = student_updateElement.Attribute("date");
                    string dateValue = dateAttr.Value;

                    if (!dateValue.Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        throw new ArgumentException("**ERROR** " + this.inputFileName+ " Value of Date attribute is not today" + "\r\n");
                    }

                    // ProgramAcronym Validation
                    XAttribute programAttr = student_updateElement.Attribute("program");
                    string programValue = programAttr.Value;

                    String programAcronym = (from r in db.Programs
                                             where r.ProgramAcronym == programValue
                                             select r.ProgramAcronym).SingleOrDefault();

                    if(programAcronym == null)
                    {
                        throw new ArgumentException("**ERROR** " + this.inputFileName + " Value of Program attribute doesn't exist" + "\r\n");
                    }

                    // Checksum Validation
                    XAttribute checksumAttr = student_updateElement.Attribute("checksum");
                    string checksumValue = checksumAttr.Value;
                    IEnumerable<XElement> studentNumbers = student_updateElement.Descendants("student_no");
                    int studentNoSum = 0;
                    foreach (XElement student_no in studentNumbers)
                    {
                        studentNoSum += int.Parse(student_no.Value);
                    }

                    if(!checksumValue.Equals(studentNoSum.ToString()))
                    {
                        throw new ArgumentException("**ERROR** " + this.inputFileName + " Incorrect checksum" + "\r\n");
                    }
                }
                else
                {
                  // Throw an exception with an appropriate message
                  throw new ArgumentException("**ERROR** " + this.inputFileName + " Student_update must have 3 attributes. The current number of attributes is " + student_updateElement.Attributes().Count() + "\r\n");
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentException("**ERROR** " + this.inputFileName + " processHeader \r\n" + ex.Message);
            }
        }

        /// <summary>
        ///  This method is used to verify the contents of the detail records in the input file.
        ///   if any of records produce an error, that record will be skipped, but the file processing will continue
        ///  Define an XDocument object and populate this object with the contents of the current file (inputFileName)
        ///   
        ///  Define an IEnumerable<XElement> LINQ-to-XML query against the XDocument object:
        ///     o The result set should only include transaction elements
        ///     o This result set will be the basis of the next query
        /// Validation Requirements
        ///  The following validation must be completed in the order listed.
        /// 
        /// </summary>
        private void processDetails()
        {
            try
            {
                XDocument xDocDetails = XDocument.Load(this.inputFileName);
                IEnumerable<XElement> transactions = xDocDetails.Descendants("transaction");

                // Validation 7 child nodes of transaction
                IEnumerable<XElement> sevenChildNodes = transactions.Where(transaction => transaction.Nodes().Count() == 7);

                processErrors(transactions, sevenChildNodes, "**ERROR** Each transaction must have 7 child nodes");

                // Validation Program match the program atribute of the root element
                XElement student_updateElement = xDocDetails.Element("student_update");
                XAttribute programAttr = student_updateElement.Attribute("program");
                string programValue = programAttr.Value;

                IEnumerable<XElement> programAcronyms = sevenChildNodes.Where(transaction => transaction.Element("program").Value == programValue);
                processErrors(sevenChildNodes, programAcronyms, "**ERROR** The Program must match the program atribute of the root element");
                
                // Validation the type node within each transaction node must be numeric
                double tmp;
                IEnumerable<XElement> numericTypeNodes = programAcronyms.Where(transaction => double.TryParse(transaction.Element("type").Value, out tmp));
                processErrors(programAcronyms, numericTypeNodes, "**ERROR** The type node within each transaction node must be numeric");

                // The grade nodes within each transaction node must be numeric or have the value of ‘*’. 
                IEnumerable< XElement > gradeNodes = numericTypeNodes.Where(transaction => ((double.TryParse(transaction.Element("grade").Value, out tmp))
                                                                        || transaction.Element("grade").Value == "*"));
                processErrors(numericTypeNodes, gradeNodes, "**ERROR** The grade nodes within each transaction node must be numeric or have the value of ‘*’.");

                // The type node within each transaction node must have a value of 1 or 2. 
                IEnumerable<XElement> valueTypeNodes = gradeNodes.Where(transaction => ((transaction.Element("type").Value == "1")
                                                                                  || transaction.Element("type").Value == "2"));
                processErrors(gradeNodes, valueTypeNodes, "**ERROR** The type node within each transaction node must have a value of 1 or 2.");

                // The grade node for course registrations (type node = 1) within each transaction node must have a value of “*”.
                // The grade node for course grading (type node = 2) within each transaction node must have a value between 0 and 100 inclusive.
                IEnumerable<XElement> valueGradeNodes = valueTypeNodes.Where(transaction => ((transaction.Element("type").Value == "1" && transaction.Element("grade").Value == "*")
                                                              || (transaction.Element("type").Value == "2" && ((double.TryParse(transaction.Element("grade").Value, out tmp) && tmp >= 0) && (double.TryParse(transaction.Element("grade").Value, out tmp) && tmp <= 100)))));
                processErrors(valueTypeNodes, valueGradeNodes, "**ERROR** The grade node for course registrations (type node = 1) is  a value of *, for course grading (type node = 2) is a value between 0 and 100");

                // The student_no node within each transaction node must exist in the database.
                IEnumerable<long> studentNumbers = from students in db.Students
                                                   select students.StudentNumber;

                IEnumerable<XElement> studentNumberNodes = valueGradeNodes.Where(transaction => studentNumbers.Contains(long.Parse(transaction.Element("student_no").Value)));

                processErrors(valueGradeNodes, studentNumberNodes, "**ERROR** The student_no node within each transaction node must exist in the database.");

                // The course_no node within each transaction node must be a “*” for grading(type 2) or it must exist in the database.
                IEnumerable<String> courseNumbers = from courses in db.Courses
                                                    select courses.CourseNumber;
                IEnumerable<XElement> courseNumberNodes = studentNumberNodes.Where(transaction => (transaction.Element("type").Value == "2" && transaction.Element("course_no").Value == "*")
                                                                                              || (transaction.Element("type").Value == "1" && courseNumbers.Contains(transaction.Element("course_no").Value))
                                                                                  );
                processErrors(studentNumberNodes, courseNumberNodes, "**ERROR** The course_no node within each transaction node must be a “*” for grading(type 2) or it must exist in the database.");

                // The registration_no node within each transaction node must be a “*” for course registration (type 1) or it must exist in the database.
                IEnumerable<long> registrationNumbers = from registrations in db.Registrations
                                                        select registrations.RegistrationNumber;
                IEnumerable<XElement> registrationNumberNodes = courseNumberNodes.Where(transaction => (transaction.Element("type").Value == "1" && transaction.Element("registration_no").Value == "*")
                                                                                                    || (transaction.Element("type").Value == "2" && registrationNumbers.Contains(long.Parse(transaction.Element("registration_no").Value)))
                                                                                        );
                processErrors(courseNumberNodes, registrationNumberNodes, "**ERROR** The registration_no node within each transaction node must be a “*” for course registration (type 1) or it must exist in the database.");

                processTransactions(registrationNumberNodes);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("**ERROR** " + this.inputFileName + " processDetails \r\n" + ex.Message + "\r\n");
            }
        }

        /// <summary>
        ///  This method will process all errors found within the current file being processed
        ///  This method will be called after each round of detail record validation
        ///  Compare the records from the beforeQuery with those from the afterQuery. Any records that do not exist in both failed that round of validation
        ///  Process each of the records that failed validation by adding relevant information to the logData property
        ///     o When writing to logData, ensure the error stands out and data is readable. See Figure 5 Include the following:
        ///          InputFileName(class property)
        ///          Program
        ///          Student Number
        ///          Course Number
        ///          RegistrationNumber
        ///          Type
        ///          Grade
        ///          Notes
        ///          Number of Nodes
        ///          The string message passed to this method
        /// </summary>
        /// <param name="beforeQuery">represents the records that existed before the round of validation</param>
        /// <param name="afterQuery">represents the records that remained after the round of validation</param>
        /// <param name="message">represents the error message that is to be written to the log file based on the record failing the round of validation</param>
        private void processErrors(IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, String message)
        {
            try
            {
                IEnumerable<XElement> errList = beforeQuery.Where(   before => !afterQuery.Any(  after => XNode.DeepEquals(before, after)   )   );

                foreach (XElement xele in errList)
                {
                    logData += "-------ERROR-------\r\n";
                    logData += "File: " + this.inputFileName + "\r\n";
                    logData += "Program: " + xele.Element("program") + "\r\n";
                    logData += "Student Number: " + xele.Element("student_no") + "\r\n";
                    logData += "Course Number: " + xele.Element("course_no") + "\n";
                    logData += "Registration Number: " + xele.Element("registration_no") + "\r\n";
                    logData += "Type: " + xele.Element("type") + "\r\n";
                    logData += "Grade: " + xele.Element("grade") + "\r\n";
                    logData += "Notes: " + xele.Element("notes") + "\r\n";
                    logData += "Nodes: " + xele.Nodes().Count() + "\r\n";
                    logData += message + "\r\n";
                    logData += "-------------------\r\n\r\n";
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("**Error** processErrors \r\n" + ex.Message + "\r\n");
            }
        }
        /// <summary>
        /// This method is used to process all valid transaction records
        /// 
        /// Return codes:  registerCourse
        ///     o If the registration is successful, return a value of 0
        ///     o If an exception occurs while updating - Registration, return a value of -300
        ///     o If the student has exceeded the MaximumAttempts of a Mastery course - Registration , return a value of -200
        ///     o If the student already has an ungraded registration for this course - Registration, return a value of -100
        /// </summary>
        /// <param name="transactionRecords"></param>
        private void processTransactions(IEnumerable<XElement> transactionRecords)
        {
            try {

                SRCollegeRegistration.ICollegeRegistration localWsCollegeRegistration = new SRCollegeRegistration.CollegeRegistrationClient();

                foreach (XElement transaction in transactionRecords)
                {
                    // Registration
                    if(transaction.Element("type").Value == "1")
                    {
                        try
                        {
                            long student_no = long.Parse(transaction.Element("student_no").Value);
                            String course_no = transaction.Element("course_no").Value;

                            Student student = db.Students
                                          .Where(x => x.StudentNumber == student_no)
                                          .SingleOrDefault();
                            Course course = db.Courses
                                         .Where(x => x.CourseNumber == course_no)
                                         .SingleOrDefault();

                            int errorCorde = localWsCollegeRegistration.registerCourse(student.StudentId,
                                                                      course.CourseId,
                                                                      transaction.Element("notes").Value);
                            // Successful Registration student 10010821 course G-100006
                            // Successful Registration student 10010821 course M-50032
                            // ERROR: Student cannot register for a course in which there is already an ungraded registration.
                            switch (errorCorde)
                            {
                                case SUCCESS_REGISTRATION:
                                    {
                                        this.logData += "Successful Registration student " + transaction.Element("student_no").Value + " course " + transaction.Element("course_no").Value + "\r\n";
                                        break;
                                    }
                                case UNGRADED_REGISTRATION:
                                    {
                                        this.logData += "ERROR: " + BusinessRules.registerError(errorCorde) + "\r\n";
                                        break;
                                    }
                                case EXCEEDED_MAXATTEMPTS:
                                    {
                                        this.logData += "ERROR: " + BusinessRules.registerError(errorCorde) + "\r\n";
                                        break;
                                    }
                                case ERROR_UPDATE:
                                    {
                                        this.logData += "ERROR: " + BusinessRules.registerError(errorCorde) + "\r\n";
                                        break;
                                    }
                                default:
                                    {
                                        this.logData += "ERROR: " + BusinessRules.registerError(errorCorde) + "\r\n";
                                        break;
                                    }

                            }
                        }catch(Exception ex)
                        {
                            this.logData += "ERROR:  registerCourse " + ex.Message + "\r\n";
                        }
                   
                    }

                    // Grading
                    if (transaction.Element("type").Value == "2")
                    {
                        try
                        {

                            double grade = Convert.ToDouble(transaction.Element("grade").Value)/100;
                                
                               /// Double.Parse(transaction.Element("grade").Value)/100;
                            int registration_no = int.Parse(transaction.Element("registration_no").Value);

                            Registration registration = db.Registrations
                                           .Where(x => x.RegistrationNumber == registration_no)
                                           .SingleOrDefault();

                            String notes = transaction.Element("notes").Value;
                           

                            localWsCollegeRegistration.updateGrade(grade, registration.RegistrationId, notes);
                                                                 
                            //this.logData
                            //grade 90.4 applied to student 10010821 for registration 214
                            //grade 7.1 applied to student 10010823 for registration 216
                            this.logData += "grade " + transaction.Element("grade").Value + " applied to student " + transaction.Element("student_no").Value + " for registration " + transaction.Element("registration_no").Value + "\r\n";
                        }catch(Exception ex)
                        {
                            this.logData += "ERROR:  updateGrade " + ex.Message + "\r\n";
                        }
                    }
                }
            }catch(Exception ex)
            {
                throw new ArgumentException("**ERROR** " + this.inputFileName + "  processTransactions \r\n" + ex.Message + "\r\n");
            }
        }

        /// <summary>
        ///  This method will be called when a file has been processed
        ///  NOTE: Processing of a single transmission file may be completed under two circumstances:
        ///     1. When a transmission is processed(whether successfully or unsuccessfully)
        ///     2. When the attributes of the transmission file indicate that the file is not to be processed
        ///  When a file has been processed, it will be renamed to include the word “COMPLETE” in the filename. 
        ///     This is done to ensure that the file isn’t accidentally processed a second time.
        ///     
        /// NOTE: writeLogData() will be called in the Process linklabel: linkClicked Event
        /// NOTE:  PAIR OF METHOD: processTranmission & writeLogData 
        /// NOTE:  WILL BE CALLED AT THE SAME TIME
        /// </summary>
        /// <returns></returns>
        public String writeLogData()
        {
            String log = "";
            if (File.Exists("COMPLETE-" + this.inputFileName))
            {
                File.Delete("COMPLETE-" + this.inputFileName);
            }

            if (File.Exists(inputFileName))
            {
                File.Move(inputFileName, "COMPLETE-" + this.inputFileName);
                StreamWriter swWrite = new StreamWriter(this.logFileName, true);
                swWrite.Write(this.logData);
                swWrite.Close();

            }
            log = this.logData;
            this.logData = "";
            this.logFileName = "";

            return log;
        }

        /// <summary>
        ///  This method will initiate the batch process by determining the appropriate filename and then proceeding with the HEADER and DETAIL processing
        ///  Formulate the inputFileName that is to be processed based on the filename format provided above
        ///     o Use properties of the DateTime class along with the programAcronym argument to create the file name
        ///     o Ensure that the.xml extension is included in this filename
        ///          (E.g.: “2019-159-VT.xml”)
        ///  Formulate logFileName that is to be used to store log data
        ///     o The logFileName property is to be set to the value of the string “LOG” concatenated with the file name defined in the previous step, replacing the extension with .txt
        ///          (E.g.: “LOG 2019-159-VT.txt”)
        ///  Determine whether a file exists matching the input inputFileName created above (not the LOG filename)
        ///  If the file does not exist:
        ///     o Append a relevant message to logData indicating that the file does not exist.Include the inputFileName in this message
        ///  If the file does exist:
        ///     o Call the processHeader method defined below
        ///     o Call the processDetails method defined below
        /// If an exception occurs during this method, append a relevant message to logData indicating the reason for the exception (use the Message property of the exception instance).    
        ///         
        /// 
        /// </summary>
        /// <param name="programAcronym"></param>
        /// <param name="key"></param>

        public void processTranmission(String programAcronym, String key)
        {
            // Create the inputFileName
            //  (E.g.: “2019-159-VT.xml”)
            String strYear = DateTime.Today.Year.ToString();
            String strDayOfYear = DateTime.Today.DayOfYear.ToString();


            this.inputFileName = strYear + '-' + strDayOfYear + '-' + programAcronym + ".xml";
            this.logFileName = "LOG " + strYear + '-' + strDayOfYear + '-' + programAcronym + ".txt";

            String inputFileNameEncrypted = this.inputFileName + ".encrypted";
            try
            {
                if (File.Exists(inputFileNameEncrypted))
                {
                    Encryption.decrypt(inputFileNameEncrypted, this.inputFileName, key);
                    if (File.Exists(this.inputFileName))
                    {
                        processHeader();
                        processDetails();
                    }
                    else
                    {
                        // Append a relevant message to logData indicating that 
                        // the file does not exist.Include the inputFileName in this message
                        this.logData += "The file " + this.inputFileName + " does not exist \r\n";
                    }
                }
                else
                {
                    // Append a relevant message to logData indicating that 
                    // the file does not exist.Include the inputFileName in this message
                    this.logData += "The file " + inputFileNameEncrypted + " does not exist \r\n";
                }

            }catch(Exception ex)
            {
                this.logData += ex.Message + "\n";
            }

        }

        /// <summary>
        /// No encryption
        /// </summary>
        /// <param name="programAcronym"></param>
        /// <param name="key"></param>
        public void processTranmission_Origin(String programAcronym, String key)
        {
            // Create the inputFileName
            //  (E.g.: “2019-159-VT.xml”)
            String strYear = DateTime.Today.Year.ToString();
            String strDayOfYear = DateTime.Today.DayOfYear.ToString();


            this.inputFileName = strYear + '-' + strDayOfYear + '-' + programAcronym + ".xml";
            this.logFileName = "LOG " + strYear + '-' + strDayOfYear + '-' + programAcronym + ".txt";

            try
            {

                if (File.Exists(this.inputFileName))
                {
                    processHeader();
                    processDetails();
                }
                else
                {
                    // Append a relevant message to logData indicating that 
                    // the file does not exist.Include the inputFileName in this message
                    this.logData += "The file " + this.inputFileName + " does not exist \r\n";
                }

            }
            catch (Exception ex)
            {
                this.logData += ex.Message + "\n";
            }

        }


    }
}
