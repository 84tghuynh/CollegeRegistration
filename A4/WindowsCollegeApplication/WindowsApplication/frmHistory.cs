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
    public partial class frmHistory : Form
    {
        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData = new ConstructorData();
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();
        

        public frmHistory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when called from 
        /// frmStudent.  This constructor will receive 
        /// specific information about the student and registration
        /// further code required:  
        /// </summary>
        /// <param name="student">specific student instance</param>
        /// <param name="registration">specific registration instance</param>
        public frmHistory(ConstructorData constructorData)
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
        /// Form is loaded when user click View Details on Student Form
        /// Gridview will be filled with the Registration list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHistory_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            studentBindingSource.DataSource = constructorData.student;
            try {
                var query = (from registrations in db.Registrations
                             join courses in db.Courses
                             on registrations.CourseId equals courses.CourseId
                             where registrations.StudentId == constructorData.student.StudentId
                             select new
                             {
                                 RegistrationNumber = registrations.RegistrationNumber,
                                 RegistrationDate = registrations.RegistrationDate,
                                 Course = courses.Title,
                                 Grade = registrations.Grade,
                                 Notes = registrations.Notes
                             }
                              );
                if (query.Count() > 0)
                {
                    //registrationBindingSource.DataSource = constructorData.registration.ToList();

                    registrationBindingSource.DataSource = query.ToList();

                }
                else
                {
                    registrationBindingSource.Clear();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
            }
        }
    }
}
