using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpDeskVG.Classes;

namespace HelpDeskVG
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string RoleStatus = Session["RoleStatus"].ToString();

            if (RoleStatus == "DUAL ROLE")
            {

                lnkSwitchRole.Visible = true;
                if (Session["EmployeeNo"] == null)
                {
                    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetNoStore();
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    string employeeNo = Session["EmployeeNo"].ToString();
                }
            }
            else
            {
                lnkSwitchRole.Visible = false;

                if (Session["EmployeeNo"] == null)
                {
                    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetNoStore();
                    Response.Redirect("Login.aspx");
                }

            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear all session data
            Session.Clear();
            Session.Abandon();

            // Optionally remove authentication cookie
            if (Request.Cookies["EmployeeNo"] != null)
            {
                HttpCookie cookie = new HttpCookie("EmployeeNo");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            // Redirect to the login page
            Response.Redirect("Login.aspx");
        }

        //protected void lnkCreateTicket_Click(object sender, EventArgs e)
        //{
        //    string sql = "";
        //    sql = "EXEC sp_vgHelpDesk_CreateIdforTicketHeader_SetDraftforStage @employee_no = '" + Session["EmployeeNo"].ToString() + "'";

        //    DataTable dt = new DataTable();
        //    dt = clsQueries.fetchData(sql);

        //    string _ticketHeaderId = "";
        //    if (dt.Rows.Count > 0)
        //    {
        //        _ticketHeaderId = dt.Rows[0][0].ToString();
        //    }

        //    Response.Redirect("~/CreateTicket.aspx?Id=" + _ticketHeaderId);
        //}

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkCreateTicket_Click(object sender, EventArgs e)
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_User_CreateIdforTicketHeader_SetDraftforStage @employee_no = '" + Session["EmployeeNo"].ToString() + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            string _ticketHeaderId = "";
            if (dt.Rows.Count > 0)
            {
                _ticketHeaderId = dt.Rows[0][0].ToString();
            }

            Response.Redirect("~/CreateTicket.aspx?Id=" + _ticketHeaderId);
        }

        protected void lnkReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports.aspx");
        }

        protected void lnkMaintenance_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Maintenance.aspx");
        }

        protected void lnkSwitchRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IT_PIC_Portal/Dashboard.aspx");
        }
    }
}