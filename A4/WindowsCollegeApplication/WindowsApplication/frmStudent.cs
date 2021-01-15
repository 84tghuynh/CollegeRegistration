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


namespace WindowsApplication
{
    public partial class frmStudent : Form
    {
        ///Given: Student and Registration data will be retrieved
        ///in this form and passed throughout application
        ///These variables will be used to store the current
        ///Student and selected Registration
        ConstructorData constructorData = new ConstructorData();
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        public frmStudent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when returning to frmStudent
        /// from another form.  This constructor will pass back
        /// specific information about the student and registration
        /// based on activites taking place in another form
        /// </summary>
        /// <param name="constructorData">Student data passed among forms</param>
        public frmStudent(ConstructorData constructorData)
        {
            InitializeComponent();

            //further code to be added
            this.constructorData = constructorData;
            //Student student = (from results in db.Students
            //                   where results.StudentNumber == this.constructorData.student.StudentNumber
            //                   select results).SingleOrDefault();

            this.studentNumberMaskedTextBox.Text = this.constructorData.student.StudentNumber.ToString();
            //  studentNumberMaskedTextBox_Leave(this, EventArgs.Empty);

            // MessageBox.Show("Backing student", this.constructorData.student.GPAStateId.ToString(), MessageBoxButtons.OK);

            ///***Giang  11/09/2019 5:08 PM ***///
            studentBindingSource.DataSource = this.constructorData.student;

           // MessageBox.Show("Backing student after assign DS", this.constructorData.student.GPAStateId.ToString(), MessageBoxButtons.OK);

            IQueryable<Registration> query = (from results in db.Registrations
                                              where results.StudentId == this.constructorData.student.StudentId
                                              select results);

            lnkUpdate.Enabled = true;
            lnkDetails.Enabled = true;

            // this.constructorData.student = constructorData.student;
            if (query.Count() > 0)
            {
                registrationBindingSource.DataSource = query.ToList();

            }
            else
            {
                lnkUpdate.Enabled = false;
                lnkDetails.Enabled = false;
                registrationBindingSource.Clear();
            }

        }

        /// <summary>
        /// given: open grading form passing constructor data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //////////// Get the Registration
            //////////int registrationId = registrationNumberComboBox.SelectedIndex;

            //////////Registration registration = (from results in db.Registrations
            //////////                   where results.RegistrationId == registrationId
            //////////                             select results).SingleOrDefault();


            this.constructorData.registration = (Registration)registrationBindingSource.Current;
            this.constructorData.student = (Student)studentBindingSource.Current;


            //MessageBox.Show("Go to Update Grade", this.constructorData.student.GPAStateId.ToString(), MessageBoxButtons.OK);
            //instance of frmTransaction passing constructor data
            frmGrading frmGrading = new frmGrading(this.constructorData);
            //open in frame
            frmGrading.MdiParent = this.MdiParent;
            //show form
            frmGrading.Show();
            this.Close();
        }

        /// <summary>
        /// given: open history form passing data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.constructorData.registration = (Registration)registrationBindingSource.Current;
            this.constructorData.student = (Student)studentBindingSource.Current;

            //MessageBox.Show("Go to history_view", this.constructorData.student.GPAStateId.ToString(), MessageBoxButtons.OK);
            //instance of frmHistory passing constructor data
            frmHistory frmHistory = new frmHistory(this.constructorData);
            //open in frame
            frmHistory.MdiParent = this.MdiParent;
            //show form
            frmHistory.Show();

            this.Close();
        }

        /// <summary>
        /// given:  opens form in top right of frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStudent_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);
        }
    
        /// <summary>
        /// Student number input then leave event fire out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                long stdNum = long.Parse(studentNumberMaskedTextBox.Text);

                //MessageBox.Show("StudentNumber", stdNum.ToString(), MessageBoxButtons.OK);

                //MessageBox.Show("Leave before query student", this.constructorData.student.GPAStateId.ToString(), MessageBoxButtons.OK);
                Student student = null;

               
                student = (from results in db.Students
                                   where results.StudentNumber == stdNum
                                   select results).SingleOrDefault();

                //MessageBox.Show("Leave envent after query student before if", student.GPAStateId.ToString(), MessageBoxButtons.OK);

                if (student != null )
                {
                    studentBindingSource.DataSource = student;

                    //MessageBox.Show("Leave after query student in if", student.GPAStateId.ToString(), MessageBoxButtons.OK);

                    //MessageBox.Show("binding studentsource  " +  student.ToString(), student.ToString(), MessageBoxButtons.OK);

                    IQueryable<Registration> query = (from results in db.Registrations
                                                      where results.StudentId == student.StudentId
                                                      select results);

                    lnkUpdate.Enabled = true;
                    lnkDetails.Enabled = true;

                   // this.constructorData.student = student;

                    if (query.Count() >0)
                    {
                        registrationBindingSource.DataSource = query.ToList();

                    }
                    else
                    {
                        lnkUpdate.Enabled = false;
                        lnkDetails.Enabled = false;
                        registrationBindingSource.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("StudentNumber does not exist", "Invalid Student Number", MessageBoxButtons.OK);
                    lnkUpdate.Enabled = false;
                    lnkDetails.Enabled = false;
                    studentBindingSource.Clear();
                    registrationBindingSource.Clear();
                    studentNumberMaskedTextBox.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error StudentNumber does not exist", "Invalid Student Number " + ex.Message, MessageBoxButtons.OK);
                lnkUpdate.Enabled = false;
                lnkDetails.Enabled = false;
                studentBindingSource.Clear();
                registrationBindingSource.Clear();
                studentNumberMaskedTextBox.Focus();

                //MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// Using to update the constructorData Registratio.
        /// Not used now.
        /// </summary>
        private void setRegistration()
        {
            // Get the Registration
            int registrationId = registrationNumberComboBox.SelectedIndex;

            Registration registration = (from results in db.Registrations
                                         where results.RegistrationId == registrationId
                                         select results).SingleOrDefault();

            //constructorData.registration = registration;
        }
    }
}
