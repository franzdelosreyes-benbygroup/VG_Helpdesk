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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["s"] == "logout")
            {
                Session.Clear();
                if (!IsPostBack)
                {
                    subLoadCredentials();
                }
            }
        }

        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            subLoadCredentials();

        }

        private void subLoadCredentials()
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_Login @employee_code='" + txtEmployeeNo.Value + "',@employee_login_password='" + txtPassword.Value + "'";

            DataTable dt = new DataTable();
            //dt = clsDB.ts_getdata(sql);
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {

                bool isActive = Convert.ToBoolean(dt.Rows[0]["is_active"]);
                if (isActive)
                {
                    string sqlCheckAccess = "";
                    sqlCheckAccess = " EXEC sp_vgHelpDesk_Login_CheckIfHasAccess @EmployeeNo='" + txtEmployeeNo.Value + "'";
                    DataTable dtCheckAccess = new DataTable();
                    dtCheckAccess = clsQueries.fetchData(sqlCheckAccess);

                    if (dtCheckAccess.Rows.Count > 0)
                    {
                        string sqlAuditLogin = "";
                        string roleStatus = dt.Rows[0]["RoleStatus"].ToString();

                        if (roleStatus == "ADMINISTRATOR")
                        {
                            Session["EmployeeNo"] = dt.Rows[0]["employee_code"].ToString();
                            Session["EmployeePosition"] = dt.Rows[0]["employee_position"].ToString();
                            Session["EmployeeEmail"] = dt.Rows[0]["employee_email"].ToString();
                            Session["FirstName"] = dt.Rows[0]["employee_first_name"].ToString();
                            Session["LocationId"] = dt.Rows[0]["location_id"].ToString();
                            Session["LevelId"] = dt.Rows[0]["level_id"].ToString();
                            Session["RoleStatus"] = dt.Rows[0]["RoleStatus"].ToString();

                            clsQueries.executeQuery(sql);



                            sqlAuditLogin = " EXEC sp_auditlogs_login ";
                            sqlAuditLogin += "@LoginBy ='" + Session["EmployeeNo"].ToString() + "'";

                            clsQueries.executeQuery(sqlAuditLogin);

                            Response.Redirect("Dashboard.aspx");

                        }
                        else if (roleStatus == "IT PIC")
                        {
                            Session["EmployeeNo"] = dt.Rows[0]["employee_code"].ToString();
                            Session["EmployeePosition"] = dt.Rows[0]["employee_position"].ToString();
                            Session["EmployeeEmail"] = dt.Rows[0]["employee_email"].ToString();
                            Session["FirstName"] = dt.Rows[0]["employee_first_name"].ToString();
                            Session["LocationId"] = dt.Rows[0]["location_id"].ToString();
                            Session["LevelId"] = dt.Rows[0]["level_id"].ToString();
                            Session["RoleStatus"] = dt.Rows[0]["RoleStatus"].ToString();

                            clsQueries.executeQuery(sql);

                            sqlAuditLogin = " EXEC sp_auditlogs_login ";
                            sqlAuditLogin += "@LoginBy ='" + Session["EmployeeNo"].ToString() + "'";

                            clsQueries.executeQuery(sqlAuditLogin);

                            Response.Redirect("~/IT_PIC_Portal/Dashboard.aspx");
                        }
                        else if (roleStatus == "DUAL ROLE")
                        {
                            Session["EmployeeNo"] = dt.Rows[0]["employee_code"].ToString();
                            Session["EmployeePosition"] = dt.Rows[0]["employee_position"].ToString();
                            Session["EmployeeEmail"] = dt.Rows[0]["employee_email"].ToString();
                            Session["FirstName"] = dt.Rows[0]["employee_first_name"].ToString();
                            Session["LocationId"] = dt.Rows[0]["location_id"].ToString();
                            Session["LevelId"] = dt.Rows[0]["level_id"].ToString();
                            Session["RoleStatus"] = dt.Rows[0]["RoleStatus"].ToString();

                            clsQueries.executeQuery(sql);

                            sqlAuditLogin = " EXEC sp_auditlogs_login ";
                            sqlAuditLogin += "@LoginBy ='" + Session["EmployeeNo"].ToString() + "'";

                            clsQueries.executeQuery(sqlAuditLogin);

                            Response.Redirect("Dashboard.aspx");
                        }

                        else if (roleStatus == "" || roleStatus == null)
                        {
                            Session["EmployeeNo"] = dt.Rows[0]["employee_code"].ToString();
                            Session["EmployeePosition"] = dt.Rows[0]["employee_position"].ToString();
                            Session["EmployeeEmail"] = dt.Rows[0]["employee_email"].ToString();
                            Session["FirstName"] = dt.Rows[0]["employee_first_name"].ToString();
                            Session["LocationId"] = dt.Rows[0]["location_id"].ToString();
                            Session["LevelId"] = dt.Rows[0]["level_id"].ToString();

                            clsQueries.executeQuery(sql);
                            sqlAuditLogin = " EXEC sp_auditlogs_login ";
                            sqlAuditLogin += "@LoginBy ='" + Session["EmployeeNo"].ToString() + "'";

                            clsQueries.executeQuery(sqlAuditLogin);

                            Response.Redirect("~/User_Portal/Dashboard.aspx");

                        }
                    }
                    else
                    {
                        clsUtil.ShowToastr(this.Page, "You are not allowed to access this portal. Please contact HR", "error");
                    }
                }
            }
            else
            {
                clsUtil.ShowToastr(this.Page, "Invalid Employee No. or Password", "error");
            }

            //    string sql = "";

            //    sql = "SELECT * FROM vw_logindetails WHERE host_mac_address='" + GetMacAddress() + "' AND expiry_datetime>CURRENT_TIMESTAMP";

            //    DataTable dt = clsLogin.fetchData(sql);

            //    if (dt.Rows.Count > 0)
            //    {
            //        string username = dt.Rows[0]["saved_username"].ToString();
            //        string password = dt.Rows[0]["decrypted_password"].ToString();

            //        sql = "EXEC sp_employee_login @employee_code='" + username + "',@employee_login_password='" + password + "'";

            //        DataTable dtLogin = clsLogin.fetchData_EmpMaster(sql);

            //        if (dt.Rows.Count > 0)
            //        {
            //            if (dt.Rows[0]["IsActive"].ToString().Trim() == "X")
            //            {
            //                Session["EmployeeNo"] = dt.Rows[0]["EmployeeNo"].ToString();
            //                Session["EmployeeName"] = dt.Rows[0]["EmployeeName"].ToString();
            //                Session["EmployeePosition"] = dt.Rows[0]["Position"].ToString();
            //                Session["EmployeeEmail"] = dt.Rows[0]["EmailAddress"].ToString();
            //                Session["FirstName"] = dt.Rows[0]["FirstName"].ToString();

            //                Response.Redirect("CollectionReport.aspx");
            //            }
            //            else
            //            {
            //                clsUtil.ShowToastr(this.Page, "Invalid Employee No. or Password", "error");
            //            }
            //        }
            //        else
            //        {
            //            clsUtil.ShowToastr(this.Page, "Invalid Employee No. or Password", "error");
            //        }

            //        if (dtLogin.Rows.Count > 0)
            //        {
            //            dt.Dispose();
            //            Response.Redirect("https://vgsciportal.com/VG_LOGIN/Dashboard.aspx");
            //        }
            //        else
            //        {
            //            dt.Dispose();
            //            Response.Redirect("https://vgsciportal.com/VG_LOGIN/Login.aspx");

            //        }
            //    }
            //    else
            //    {
            //        dt.Dispose();
            //        Response.Redirect("https://vgsciportal.com/VG_LOGIN/Login.aspx");
            //    }
            //}
        }
    }
}