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
    public partial class wfStudent : System.Web.UI.Page
    {
        //Instance of DataContext object
        BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        /// <summary>
        /// When user logs in successfully, the Login.aspx page will redirect tot this page.
        /// If this page is accessed without authentication, the user will be redirected to Login.aspx page
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
                    //grab the user name. Ex: studentNumber@bit.com
                    //get the studentNumber by removing @bit.com from user name.
                    long studentNum = long.Parse(BusinessRules.extractDescription(Page.User.Identity.Name,"@"));

                    try
                    {
                        //get student object
                        Student student = db.Students
                                              .Where(x => x.StudentNumber == studentNum)
                                              .SingleOrDefault();
                        Session["Student"] = student;
                        //binding to Label
                        lblStudentName.Text = student.FullName;
                        
                        // Get list of registered courses - in Courses table
                        IQueryable<int> lstCourseId =
                                           from registration in db.Registrations
                                           join course in db.Courses on registration.CourseId equals course.CourseId
                                           where registration.StudentId == student.StudentId
                                           select course.CourseId;

                        IQueryable<Course> query = from course in db.Courses
                                                   where lstCourseId.Contains(course.CourseId)
                                                   select course;

                        Session["RegisteredCourses"] = query;
                        //binding Gridview datasoure to the result
                        //need tolist for gridviews otherwise runtime error
                        gvCourses.DataSource = query.ToList();
                        //need this line otherwise no data
                        this.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblErrorStudent.Visible = true;
                        lblErrorStudent.Text = ex.Message;
                    }
                }
                else
                {
                    //if not logged in redirect to login page
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
        }

        /// <summary>
        /// Event handle for Selected Index Change.
        /// Event raises when user click "Select" on gvCourses Gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CourseNumber"] = gvCourses.Rows[gvCourses.SelectedIndex].Cells[1].Text;
            Response.Redirect("~/wfDrop.aspx");
        }

        /// <summary>
        /// Then user click the link button to Register the Course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnRegisterCourse_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfRegister.aspx");
        }
    }
}