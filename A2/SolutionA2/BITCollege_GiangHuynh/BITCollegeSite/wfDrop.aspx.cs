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
    public partial class wfDrop : System.Web.UI.Page
    {
        //Instance of DataContext object
        BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        /// <summary>
        /// Loading the detail of the list registered courses.
        /// Having a Index Page if a student has more than a registration for a course number
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
                    Display_RegistrationDetail();
                }
                else
                {
                    //if not logged in redirect to login page
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
        }

        /// <summary>
        /// Then user click the link button : Return to Registration Listing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnRegistrationListing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfStudent.aspx");
        }

        /// <summary>
        /// Event handle for PageIndexChanging.
        /// Event raises when user click "index" on the bottom bar of dvRegistration DetailView
        /// This will change detail view of registration to new page.
        /// The detail view will be loaded with new data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvRegistration_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            // Update PageIndex to SelectedPageIndex
            dvRegistration.PageIndex = e.NewPageIndex;

            // Display a registration detail
            Display_RegistrationDetail();

        }

        /// <summary>
        /// This function display the Registration detail of a Course number.
        /// A Course number could be registered many times by a student.
        /// </summary>
        protected void Display_RegistrationDetail()
        {
            try
            {
                string courseNum = (string)Session["CourseNumber"];

                Student student = (Student)Session["Student"];

                //get Course object
                Course course = db.Courses
                                      .Where(x => x.CourseNumber == courseNum)
                                      .SingleOrDefault();

                IQueryable<Registration> query = db.Registrations
                                                .Where(x => x.StudentId == student.StudentId)
                                                .Where(x => x.CourseId == course.CourseId);

                if (!query.Any())
                {
                    Response.Redirect("~/wfStudent.aspx");
                }

                //binding DetailView datasoure to the result
                //need tolist for gridviews otherwise runtime error
                dvRegistration.DataSource = query.ToList();
                //need this line otherwise no data
                this.DataBind();

                // link button Drop will be disabled if Grade is null = '&nbsp;'
                if (dvRegistration.Rows[4].Cells[1].Text == "&nbsp;")
                    lnkBtnDrop.Enabled = true;
                else lnkBtnDrop.Enabled = false;
            }
            catch (Exception ex)
            {
                lblErrorDrop.Visible = true;
                lblErrorDrop.Text = ex.Message;

            }
        }
        /// <summary>
        /// Drop a registration if Grade is null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void lnkBtnDrop_Click(object sender, EventArgs e)
        {
           
            try
            {
                long registrationNumber = Convert.ToInt64(dvRegistration.Rows[0].Cells[1].Text);
                Registration registration = db.Registrations
                                        .Where(x => x.RegistrationNumber == registrationNumber)
                                        .SingleOrDefault();

                SRCollegeRegistration.ICollegeRegistration localWsCollegeRegistration = new SRCollegeRegistration.CollegeRegistrationClient();

                bool resultCode = localWsCollegeRegistration.dropCourse(registration.RegistrationId);
                if (!resultCode)
                {
                    lblErrorDrop.Visible = true;
                    lblErrorDrop.Text = "Error in dropCourse";
                }
                else
                {
                    Display_RegistrationDetail();
                }

            }
            catch(Exception ex)
            {
                lblErrorDrop.Visible = true;
                lblErrorDrop.Text = "Error in dropCourse" + ex.Message;
            }
            



        }
    }
}