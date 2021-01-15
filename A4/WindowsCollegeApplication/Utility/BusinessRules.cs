using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Utility   
{
    /// <summary>
    /// given:  Enum listing course types
    /// </summary>
    public enum CourseType
    {
        GRADED, MASTERY, AUDIT
    }

    /// <summary>
    /// given:  Struct to align letter grades with gradepoint values
    /// Acts as an ENUM but with double values
    /// </summary>
    public struct GradePointValue
    {
        public const double A_PLUS = 4.5;
        public const double A = 4;
        public const double B_PLUS = 3.5;
        public const double B = 3;
        public const double C_PLUS = 2.5;
        public const double C = 2;
        public const double D = 1;
        public const double F = 0;
        public const double PASS = 4;
        public const double FAIL = 0;
        public const double INCOMPLETE = -1;
    }

    public static class BusinessRules
    {
        const string UNDEFINED = "";

        /// <summary>
        /// Given:
        /// defines the mask display format for the various course types
        /// </summary>
        /// <param name="accountType">string course type name</param>
        /// <returns>string format</returns>
        public static string courseFormat(string courseType)
        {
            string[] COURSE_TYPE = { "Audit", "Mastery", "Graded"};
            string[] COURSE_MASK = { ">L-00-00", ">L-00-0-00", ">L-00-00-00" };


            //initial format (empty string)
            string format = UNDEFINED;

            //compare account type to predefined types
            for (int i = 0; i < COURSE_TYPE.Length; i++)
            {
                //if a match, return the corresonding mask
                if(courseType.ToLower() == COURSE_TYPE[i].ToLower())
                {
                    format = COURSE_MASK[i];
                    break;
                }
            }
            //return the mask or empty string
            return format;
        }


        /// <summary>
        /// Given:
        /// CourseTypeLookup:  Matches string description
        /// with CourseType enum
        /// </summary>
        /// <param name="courseDescription">String description of course</param>
        /// <returns>CourseType enum</returns>
        public static CourseType courseTypeLookup(string courseDescription)
        {
            CourseType courseType = CourseType.AUDIT;

            //switch course.CourseType
            switch (courseDescription)
            {
                case "Graded":
                    courseType = CourseType.GRADED;
                    break;
                case "Mastery":
                    courseType = CourseType.MASTERY;
                    break;
                default:
                    courseType = CourseType.AUDIT;
                    break;
            }

            return courseType;
        }


        /// <summary>
        /// Given:  Looks up letter grade based on course type and earned grade
        /// </summary>
        /// <param name="grade">double earned grade</param>
        /// <param name="courseType">uses course type enum</param>
        /// <returns></returns>
        public static double gradeLookup(double grade, CourseType courseType)
        {
            double gradePoint = GradePointValue.INCOMPLETE;

            switch (courseType)
            {
                case CourseType.GRADED:
                    {
                        if (grade >= .90)
                        {
                            gradePoint = GradePointValue.A_PLUS;
                        }
                        else if (grade >= .80)
                        {
                            gradePoint = GradePointValue.A;
                        }
                        else if (grade >= .75)
                        {
                            gradePoint = GradePointValue.B_PLUS;
                        }
                        else if (grade >= .70)
                        {
                            gradePoint = GradePointValue.B;
                        }
                        else if (grade >= .65)
                        {
                            gradePoint = GradePointValue.C_PLUS;
                        }
                        else if (grade >= .60)
                        {
                            gradePoint = GradePointValue.C;
                        }
                        else if (grade >= .50)
                        {
                            gradePoint = GradePointValue.D;
                        }
                        else
                        {
                            gradePoint = GradePointValue.F;
                        }
                        break;
                    }
                case CourseType.MASTERY:
                    {
                        gradePoint = grade >= .75 ? GradePointValue.PASS : GradePointValue.FAIL;
                        break;
                    }
                default:
                    {
                        gradePoint = GradePointValue.INCOMPLETE;
                        break;
                    }
            }

            return gradePoint;

        }

        /// <summary>
        /// This function return a substring from index 0 to pattern in description
        /// Ex: description = "MasteryCourse"
        /// Pattern="Course"
        /// Return "Mastery"
        /// </summary>
        /// <param name="description"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string extractDescription(string description, string pattern)
        {
            int len = description.IndexOf(pattern);

            return description.Substring(0, len);

        }
        /// <summary>
        /// Return a description of the known code
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static string registerError(int errorCode)
        {
            switch(errorCode)
            {
                case -100:
                    return "Student cannot register for a course in which there is already an ungraded registration.";
                case -200:
                    return "Student has exceeded maximum attempts on mastery course.";
                case -300:
                    return "An error has occurred while updating the registration.";
                default:
                    return "Unknown error";
            }
        }

    }

    /// <summary>
    /// This class use for Encryption
    /// </summary>
    public static class Encryption
    {
        /// <summary>
        /// This function uses for encrypting a plaintext file to an encrypted file.
        /// Algorithm: DES, Key-64bit
        /// </summary>
        /// <param name="unencryptedFileName"></param>
        /// <param name="encryptedFileName"></param>
        /// <param name="key"></param>
        public static void encrypt(string unencryptedFileName, string encryptedFileName, string key)
        {
            // Create a FileStream object based on the unencrypted file name with an Open filemode, and a Read file access
            FileStream PlainTextFileStream = new FileStream(unencryptedFileName, FileMode.Open, FileAccess.Read);

            // Create a second FileStream object based on the (eventual) encrypted file name with a Create filemode and a Write file access
            FileStream EncryptedFileStream = new FileStream(encryptedFileName, FileMode.Create, FileAccess.Write);

            // Create a DESCryptoServiceProvider object
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(key);

            // Create a ICryptoTransform object using the CreateEncryptor method of the DESCryptoServiceProvider object
            ICryptoTransform desEncrypt = DES.CreateEncryptor();

            // Create a CryptoStream object using the (eventual) encrypted filestream, the ICryptoTransform object, 
            // and using Write CryptoStreamMode
            CryptoStream cryptostreamEncr = new CryptoStream(EncryptedFileStream, desEncrypt, CryptoStreamMode.Write);

            // Define a byte array based on the Length of the unencrypted filestream
            byte[] bytearray = new byte[PlainTextFileStream.Length];

            // Read data using the unencrypted filestream
            //  o Read this data into the byte array with no offset using the entire length of the bytearray
            PlainTextFileStream.Read(bytearray, 0, bytearray.Length);

            // Write the encrypted data using the CryptoStream object
            //  o Write the bytestream to the encrypted filestream with no offset using the entire length
            //    of the bytearray
            cryptostreamEncr.Write(bytearray, 0, bytearray.Length);

            // Close the cryptostream object
            cryptostreamEncr.Close();

            // Close the unencrypted filestream
            PlainTextFileStream.Close();

            // Close the encrypted filestream
            EncryptedFileStream.Close();

        }

        /// <summary>
        /// This function uses for decrypte an encrypted file with DES, Key-64bit Algorithm to a plaintext file.
        /// </summary>
        /// <param name="encryptedFileName"></param>
        /// <param name="unencryptedFileName"></param>
        /// <param name="key"></param>
        public static void decrypt(string encryptedFileName, string unencryptedFileName, string key)
        {
            // Create a StreamWriter object based on the decrypted filename
            // FileStream PlainTextFileStream = new FileStream(unencryptedFileName, FileMode.Open, FileAccess.Write);

            // Create a DESCryptoServiceProvider object
            // Use the AsciiEncoding.Ascii.GetBytes method to encode the string Key argument into a sequence of bytes
            // Set the DESCryptoServiceProvider’s Key property to this sequence of bytes
            // Set the DESCryptoServiceProvider’s IV(initialization vector) property to this sequence of bytes
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(key);

            // Create a FileStream object based on the encrypted filename with an Open filemode, and a Read fileaccess
            FileStream EncryptedFileStream = new FileStream(encryptedFileName, FileMode.Open, FileAccess.Read);

            // Create a ICryptoTransform object using the CreateDecryptor method of the DESCryptoServiceProvider object
            ICryptoTransform desEncrypt = DES.CreateDecryptor();

            // Create a CryptoStream object using the encrypted file filestream, the ICryptoTransform object, and 
            // using Read CryptoStreamMode
            CryptoStream cryptostreamDecr = new CryptoStream(EncryptedFileStream, desEncrypt, CryptoStreamMode.Read);

            // Using the decrypted StreamWriter object, Write a new (decrypted) file based on the CryptoStream object
            // o Use the StreamReader’s ReadToEnd method to read the entire contents of the CryptoStream object
            StreamWriter swDecrypted = new StreamWriter(unencryptedFileName);
            swDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            swDecrypted.Flush();
            swDecrypted.Close();



        }
    }

}
