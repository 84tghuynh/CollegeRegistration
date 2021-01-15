using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BITCollege_GiangHuynh.Models;
using Utility;

namespace BITCollegeSite
{
    public partial class wfRegister : System.Web.UI.Page
    {
        //Instance of DataContext object
        BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        public const int SUCCESS_REGISTRATION = 0;
        public const int ERROR_UPDATE = -300;
        public const int EXCEEDED_MAXATTEMPTS = -200;
        public const int UNGRADED_REGISTRATION = -100;

        /// <summary>
        /// Loading the Register Form when user click Register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //only run on initial page load
            if (!IsPostBack)
            {
                //only proceed if user has logged in
                if (this.Page.User.Identity.IsAuthenticated)
                {
                    try
                    {

                        Student student = (Student)Session["Student"];
                        //binding to Label
                        lblRegStudentName.Text = student.FullName;

                        IQueryable<Course> query = from course in db.Courses
                                                   select course;

                        ddlCourse.DataSource = query.ToList();
                        ddlCourse.DataTextField = "Title";
                        ddlCourse.DataValueField = "CourseId";

                        this.DataBind();
                    }catch(Exception ex)
                    {
                        lblErrorRegister.Visible = true;
                        lblErrorRegister.Text = ex.Message;
                    }

                }
            }
        }
        /// <summary>
        /// Register Course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnRegister_Click(object sender, EventArgs e)
        {
            Student student = (Student)Session["Student"];
            string note = txtNote.Text;

            int courseId = Convert.ToInt32(ddlCourse.SelectedItem.Value);
                //Convert.ToInt32(ddlCourse.DataValueField);

            SRCollegeRegistration.ICollegeRegistration localWsCollegeRegistration = new SRCollegeRegistration.CollegeRegistrationClient();

            int resultCode = localWsCollegeRegistration.registerCourse(student.StudentId,courseId,note);
            
            switch(resultCode)
            {
                case SUCCESS_REGISTRATION:
                    Response.Redirect("~/wfStudent.aspx");
                    break;
                case UNGRADED_REGISTRATION:
                    lblErrorRegister.Visible = true;
                    lblErrorRegister.Text = BusinessRules.registerError(UNGRADED_REGISTRATION);
                    break;
                case EXCEEDED_MAXATTEMPTS:
                    lblErrorRegister.Visible = true;
                    lblErrorRegister.Text = BusinessRules.registerError(EXCEEDED_MAXATTEMPTS);
                    break;
                case ERROR_UPDATE:
                    lblErrorRegister.Visible = true;
                    lblErrorRegister.Text = BusinessRules.registerError(ERROR_UPDATE);
                    break;
                default:
                    lblErrorRegister.Visible = true;
                    lblErrorRegister.Text = BusinessRules.registerError(resultCode);
                    break;
            }
        }

        /// <summary>
        /// Then user click the link button : Return to Registration Listing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnReturnRegistrationList_Click(object sender, EventArgs e)
        {
              
                    Response.Redirect("~/wfStudent.aspx");
            
        }
    }
}