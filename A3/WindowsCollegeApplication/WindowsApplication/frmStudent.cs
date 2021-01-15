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
        protected static BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

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

            studentBindingSource.DataSource = constructorData.student;

            lnkUpdate.Enabled = true;
            lnkDetails.Enabled = true;


            IQueryable<Registration> query = (from results in db.Registrations
                                              where results.StudentId == constructorData.student.StudentId
                                              select results);

            lnkUpdate.Enabled = true;
            lnkDetails.Enabled = true;

            this.constructorData.student = constructorData.student;
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
            
            constructorData.registration = (Registration)registrationBindingSource.Current;
            //instance of frmTransaction passing constructor data
            frmGrading frmGrading = new frmGrading(constructorData);
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
            constructorData.registration = (Registration)registrationBindingSource.Current;
            //instance of frmHistory passing constructor data
            frmHistory frmHistory = new frmHistory(constructorData);
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
            long stdNum = Convert.ToInt64(studentNumberMaskedTextBox.Text);
           
            try
            {

                Student student = (from results in db.Students
                                   where results.StudentNumber == stdNum
                                   select results).SingleOrDefault();

                if (student != null )
                {
                    studentBindingSource.DataSource = student;

                    IQueryable<Registration> query = (from results in db.Registrations
                                                      where results.StudentId == student.StudentId
                                                      select results);

                    lnkUpdate.Enabled = true;
                    lnkDetails.Enabled = true;

                    constructorData.student = student;

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
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
            }
        }

        private void registrationNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Registration reg= (Registration)registrationBindingSource.Current;
            //IQueryable<Registration> query = (from results in db.Registrations
            //                                  where results.StudentId == ((Student) studentBindingSource.Current).StudentId
            //                                  select results);
            //registrationBindingSource.DataSource = query.ToList();
        }

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
