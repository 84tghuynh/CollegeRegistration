using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BITCollege_GiangHuynh.Models;
using Utility;

namespace WindowsApplication
{
    public partial class frmGrading : Form
    {
        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;
        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();
        public frmGrading()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when called from 
        /// frmStudent.  This constructor will receive 
        /// specific information about the student and registration
        /// 
        /// </summary>
        /// <param name="student">specific student instance</param>
        /// <param name="registration">specific registration instance</param>
        public frmGrading(ConstructorData constructorData)
        {
            InitializeComponent();
            this.constructorData = constructorData;
        }

        /// <summary>
        /// given: this code will navigate back to frmStudent with
        /// the specific student and registration data that launched
        /// this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to student with the data selected for this form
            frmStudent frmStudent = new frmStudent(constructorData);
            frmStudent.MdiParent = this.MdiParent;
            frmStudent.Show();
            this.Close();
        }

        /// <summary>
        /// Form is loaded when user click the Update Grade on Student Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGrading_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            try
            {

                courseNumberMaskedLabel.Mask = BusinessRules.courseFormat(constructorData.registration.Course.CourseType);

                studentBindingSource.DataSource = constructorData.student;

                registrationBindingSource.DataSource = constructorData.registration;

                if (constructorData.registration.Grade != null)
                {
                    gradeTextBox.Enabled = false;
                    lnkUpdate.Enabled = false;
                    lblExisting.Text = "Grading is not possible";
                    lblExisting.Visible = true;
                }
                else
                {
                    gradeTextBox.Enabled = true;
                    lnkUpdate.Enabled = true;
                    lblExisting.Visible = false;
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
            }

        }
        /// <summary>
        /// Event click to update Grade if Posible, then return Student from if successfully
        /// Update only happening when the Registration haven't got Grade yet.
        /// Grade in range 0-1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            double grade = -1;
            string strGrade = Numeric.ClearFormatting(gradeTextBox.Text,"%");
            strGrade = Numeric.ClearFormatting(strGrade, ",");
            
            try
            {
                if (Numeric.isNumeric(strGrade, System.Globalization.NumberStyles.AllowDecimalPoint))
                {
                    grade = Convert.ToDouble(strGrade)/100;
                    if (grade >= 0 && grade <= 1)
                    {
                        SRCollegeRegistration.ICollegeRegistration localWsCollegeRegistration = new SRCollegeRegistration.CollegeRegistrationClient();

                        localWsCollegeRegistration.updateGrade(grade, constructorData.registration.RegistrationId, "Update Grade");
                        Student student = (from results in db.Students
                                           where results.StudentNumber == this.constructorData.student.StudentNumber
                                           select results).SingleOrDefault();
                        this.constructorData.student = student;
                        frmStudent frmStudent = new frmStudent(this.constructorData);
                        frmStudent.MdiParent = this.MdiParent;
                        frmStudent.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Grade must be entered as a decimal value in range 0-1", "ERROR", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Grade must be a Number in range 0-1", "ERROR", MessageBoxButtons.OK);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
            }
        }
    }
}
