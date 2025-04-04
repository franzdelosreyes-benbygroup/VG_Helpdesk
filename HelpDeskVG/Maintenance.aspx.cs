using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpDeskVG.Classes;

namespace HelpDeskVG
{
    public partial class Maintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmployeeNo"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                if (Session["ToastrMessage"] != null)
                {
                    string message = Session["ToastrMessage"].ToString();
                    string type = Session["ToastrType"].ToString();

                    // Show Toastr
                    clsUtil.ShowToastr(this.Page, message, type);

                    Session.Remove("ToastrMessage");
                    Session.Remove("ToastrType");
                }

                ddlCategory2.Enabled = false;

                DisplaySection();
                DisplaySection2();

                DisplayPriorityList();
                DisplaySectionList();
                DisplayCategoryList();
                DisplayNatureOfProbList();
                DisplayRoleAdminandITPIC();
                DisplayRole();

                clsQueries.DisplayITPICEmployeeMaintenance(ddlEmployeeIT);

            }
        }
        protected void lnkRolesandPermission_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "assignRoleModal();", true);
        }

        protected void DisplayRoleAdminandITPIC()
        {
            string sql = "";

            sql = @"SELECT CONCAT (b.employee_first_name, ' ', b.employee_last_name) AS employee_info, b.employee_position, a.role_name, a.employee_no, a.is_active, 
                      CASE WHEN	a.is_active = '1' THEN 'active'
                      WHEN a.is_active = '0' THEN 'inactive'
                      END AS admin_status
                      FROM m_Admin AS a
                      INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS b ON b.employee_code = a.employee_no ORDER BY admin_status ASC";

            
            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);


            gvRolesandPermission.DataSource = dt;
            gvRolesandPermission.DataBind();
            gvRolesandPermission.Dispose();

           
        }

        protected void DisplaySectionList()
        {
            string sql = "";

            sql = "SELECT * FROM m_Section WHERE is_active = '1' ORDER BY is_active DESC";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvSectionList.DataSource = dt;
            gvSectionList.DataBind();
            gvSectionList.Dispose();
        }

        protected void DisplayCategoryList()
        {
            string sql = "";

            sql = @"SELECT a.description_category, a.created_at, a.created_by, b.description_section, a.category_id FROM m_Category AS a
                    INNER JOIN m_Section AS b ON b.section_id = a.section_id 					
                    WHERE b.is_active = '1' AND a.is_active = '1' ORDER BY a.is_active DESC";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvCategoryList.DataSource = dt;
            gvCategoryList.DataBind();
            gvCategoryList.Dispose();

        }

        protected void DisplayNatureOfProbList()
        {
            string sql = "";
            sql = @"SELECT DISTINCT a.nature_of_prob_id ,a.created_at, a.created_by, b.description_section, c.description_category, a.description_natureofprob, a.is_active
                    FROM m_NatureOfProblem AS a
                    INNER JOIN m_Section AS b ON a.section_id = b.section_id
                    LEFT JOIN m_Category AS c ON a.category_id = c.category_id
                    WHERE a.is_active = '1' AND b.is_active = '1' AND c.is_active = '1' ORDER BY a.is_active DESC";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvNatureOfProbList.DataSource = dt;
            gvNatureOfProbList.DataBind();
            gvNatureOfProbList.Dispose();

        }

        protected void DisplayPriorityList()
        {
            string sql = "";
            sql = @"SELECT description, alloted_hour, created_at, color_code, priority_id FROM m_Priority WHERE is_active = '1'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvPriorityList.DataSource = dt;
            gvPriorityList.DataBind();
            gvPriorityList.Dispose();
        }

        protected void DisplaySection()
        {
            clsQueries.DisplaySection(ddlSection);
        }


        protected void DisplaySection2()
        {
            clsQueries.DisplaySection(ddlSection2);
        }

        protected void DisplayCategory2()
        {
            string sql = "";
            sql = @"SELECT category_id, section_id, [description_category] FROM m_Category WHERE is_active = '1' AND section_id = " + ddlSection2.SelectedValue;

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            ddlCategory2.DataSource = dt;
            ddlCategory2.DataTextField = "description_category";
            ddlCategory2.DataValueField = "category_id";

            ddlCategory2.DataBind();

            dt.Dispose();

            ddlCategory2.Items.Insert(0, new ListItem("Please Select", ""));
        }

        protected void DisplayRole()
        {
            string sql = "";

            sql = "SELECT role_id, role_name FROM m_Role WHERE is_active='1'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            ddlRole.DataSource = dt;
            ddlRole.DataTextField = "role_name";
            ddlRole.DataValueField = "role_id";
            ddlRole.DataBind();

            dt.Dispose();

            ddlRole.Items.Insert(0, new ListItem("Please Select", ""));

        }

        protected void lnkAssignRole_Click(object sender, EventArgs e)
        {
            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_InsertNewRole ";
            sql += " @EmployeeNo='" + ddlEmployeeIT.SelectedValue + "',";
            sql += " @Role='" + ddlRole.SelectedValue + "'";

            clsQueries.executeQuery(sql);

            DisplayRoleAdminandITPIC();

            Session["ToastrMessage"] = "Successfully Assigned Role to the User!";
            Session["ToastrType"] = "success";

            Response.Redirect("Maintenance.aspx");

        }

        protected void lnkAddSection_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "addSectionModal();", true);
        }

        protected void lnkAddCategory_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "addCategoryModal();", true);

        }


        protected void lnkAddNatureOfProb_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "addNatureOfProbModal();", true);

        }

        protected void lnkAddNewPriority_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "addPriorityModal();", true);

        }

        protected void lnkSaveNewSection_Click(object sender, EventArgs e)
        {
            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_InsertNewSection ";
            sql += " @CreatedBy='" + Session["EmployeeNo"].ToString() + "',";
            sql += " @Section='" + txtNewSection.Text + "'";

            clsQueries.executeQuery(sql);

            txtNewSection.Text = "";

            Session["ToastrMessage"] = "Successfully Added New Section!";
            Session["ToastrType"] = "success";

            Response.Redirect("Maintenance.aspx");


        }

        protected void lnkSaveCategory_Click(object sender, EventArgs e)
        {
            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_InsertNewCategory ";
            sql += " @CreatedBy='" + Session["EmployeeNo"].ToString() + "',";
            sql += " @SectionId='" + ddlSection.SelectedValue + "',";
            sql += " @Category='" + txtNewCategory.Text + "'";

            clsQueries.executeQuery(sql);

            Session["ToastrMessage"] = "Successfully Added New Category!";
            Session["ToastrType"] = "success";

            ddlSection.SelectedValue = "";
            txtNewCategory.Text = "";

            Response.Redirect("Maintenance.aspx");


        }

        protected void ddlSection2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayCategory2();
            ddlCategory2.Enabled = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "addNatureOfProbModal();", true);

        }

        protected void lnkSaveNatureofProb_Click(object sender, EventArgs e)
        {
            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_InsertNewNatureOfProb ";
            sql += " @CreatedBy='" + Session["EmployeeNo"].ToString() + "',";
            sql += " @SectionId='" + ddlSection2.SelectedValue + "',";
            sql += " @CategoryId='" + ddlCategory2.SelectedValue + "',";
            sql += " @NatureOfProb='" + txtNewNatureOfProb.Text + "'";

            clsQueries.executeQuery(sql);

            Session["ToastrMessage"] = "Successfully Added New Nature of Problem!";
            Session["ToastrType"] = "success";

            ddlSection2.SelectedValue = "";
            ddlCategory2.SelectedValue = "";
            txtNewNatureOfProb.Text = "";

            Response.Redirect("Maintenance.aspx");

        }

        protected void lnkSaveNewPrio_Click(object sender, EventArgs e)
        {
            int allocatedHours = Convert.ToInt32(txtNewAllotedHrs.Text);
            string sql = "";
            if (allocatedHours > 0)
            {

                string colorCode = ddlPriorityColor.SelectedValue;

                sql = "EXEC sp_vgHelpDesk_Admin_InsertNewPriority ";
                sql += " @CreatedBy='" + Session["EmployeeNo"].ToString() + "',";
                sql += " @DescPriority='" + txtNewDescPriority.Text + "',";
                sql += " @AllotedHrs='" + txtNewAllotedHrs.Text + "',";
                sql += " @ColorCode='" + colorCode + "'";

                clsQueries.executeQuery(sql);

                txtNewAllotedHrs.Text = "";
                txtNewDescPriority.Text = "";
                ddlPriorityColor.SelectedValue = "";

                clsUtil.ShowToastr(this.Page, "Successfully Added New Priority", "success");

                DisplayPriorityList();
                DisplaySectionList();
                DisplayCategoryList();
                DisplayNatureOfProbList();
                DisplayRoleAdminandITPIC();
                DisplayRole();

            }
            else
            {
                clsUtil.ShowToastr(this.Page, "Please Insert Integer Only", "warning");
            }

        }

        protected void lnkEditSection_Click(object sender, EventArgs e)
        {

            HiddenField hfSectionId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfSectionId") as HiddenField;

            hfMdSectionId.Value = hfSectionId.Value;

            string sql = "";

            sql = "SELECT description_section from m_Section WHERE is_active = '1' AND section_id =" + hfMdSectionId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            txtEditSection.Text = dt.Rows[0]["description_section"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editSectionModal();", true);

        }

        protected void lnkSaveNewEditedSection_Click(object sender, EventArgs e)
        {
            string sectionId = hfMdSectionId.Value.ToString();

            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_UpdateEditedSection ";
            sql += " @SectionId= '" + sectionId + "',";
            sql += " @NewEditedSection= '" + txtEditSection.Text + "'";

            clsQueries.executeQuery(sql);

            clsUtil.ShowToastr(this.Page, "Successfully Edited Section", "success");

            DisplayPriorityList();
            DisplaySectionList();
            DisplayCategoryList();
            DisplayNatureOfProbList();
            DisplayRoleAdminandITPIC();
            DisplayRole();
        }

        protected void lnkEditCategory_Click(object sender, EventArgs e)
        {
            HiddenField hfCategoryId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfCategoryId") as HiddenField;
            hfMdCategoryId.Value = hfCategoryId.Value;

            string sql = "";

            sql = "SELECT a.description_category, b.description_section, a.section_id FROM m_Category AS a INNER JOIN m_Section AS b ON b.section_id = a.section_id WHERE a.is_active = '1' AND category_id =" + hfMdCategoryId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            txtEditSection2.Text = dt.Rows[0]["description_section"].ToString();
            txtEditCategory2.Text = dt.Rows[0]["description_category"].ToString();


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editCategoryModal();", true);

        }

        protected void lnkSaveEditedCategory_Click(object sender, EventArgs e)
        {
            string categoryId = hfMdCategoryId.Value.ToString();

            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_UpdateEditedCategory ";
            sql += " @CategoryId = '" + categoryId + "',";
            sql += " @NewEditedCategory = '" + txtEditCategory2.Text + "'";

            clsQueries.executeQuery(sql);

            clsUtil.ShowToastr(this.Page, "Successfully Edited Category", "success");

            DisplayPriorityList();
            DisplaySectionList();
            DisplayCategoryList();
            DisplayNatureOfProbList();
            DisplayRoleAdminandITPIC();
            DisplayRole();
        }

        protected void lnkEditNatureOfProb_Click(object sender, EventArgs e)
        {
            HiddenField hfNatureOfProbId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfNatureOfProb") as HiddenField;
            hfMdNatureOfProbId.Value = hfNatureOfProbId.Value;

            string sql = "";

            sql = @"SELECT a.nature_of_prob_id, a.description_natureofprob, b.description_category, c.description_section FROM m_NatureOfProblem AS a
                    INNER JOIN m_Category AS b ON b.category_id = a.category_id
                    INNER JOIN m_Section AS c ON c.section_id = c.section_id 
                    WHERE a.is_active = '1' AND nature_of_prob_id =" + hfMdNatureOfProbId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);


            txtEditSection3.Text = dt.Rows[0]["description_section"].ToString();
            txtEditCategory3.Text = dt.Rows[0]["description_category"].ToString();
            txtEditNatureOfProb.Text = dt.Rows[0]["description_natureofprob"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editNatureOfProblemModal();", true);

        }

        protected void lnkSavedEditedNatureofProb_Click(object sender, EventArgs e)
        {

            string natureofprobId = hfMdNatureOfProbId.Value.ToString();

            string sql = "";

            sql = " EXEC sp_vgHelpDesk_Admin_UpdateEditedNatureOfProblem ";
            sql += " @NatureOfProbId = '" + natureofprobId + "',";
            sql += " @NewEditedNatureOfProb = '" + txtEditNatureOfProb.Text + "'";

            clsQueries.executeQuery(sql);

            clsUtil.ShowToastr(this.Page, "Successfully Edited Nature of Problem", "success");

            DisplayPriorityList();
            DisplaySectionList();
            DisplayCategoryList();
            DisplayNatureOfProbList();
            DisplayRoleAdminandITPIC();
            DisplayRole();
        }

        protected void lnkEditPriority_Click(object sender, EventArgs e)
        {
            HiddenField hfPriorityId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfPriority") as HiddenField;
            hfMdPriorityId.Value = hfPriorityId.Value;

            string sql = "";

            sql = "SELECT [description], color_code, alloted_hour FROM m_Priority WHERE is_active = '1' AND priority_id=" + hfMdPriorityId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            txtNewEditedAllotedHr.Text = dt.Rows[0]["alloted_hour"].ToString();
            txtNewEditedDescriptionPriority.Text = dt.Rows[0]["description"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editPriorityModal();", true);

        }

        protected void lnkSaveEditedPriority_Click(object sender, EventArgs e)
        {
            string priorityId = hfMdPriorityId.Value.ToString();
            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_UpdateEditedPriority";
            sql += " @PriorityId = '" + priorityId + "',";
            sql += " @NewEditedPriorityDesc = '" + txtNewEditedDescriptionPriority.Text + "',";
            sql += " @NewEditedAllotedHrs = '" + txtNewEditedAllotedHr.Text + "',";
            sql += " @ColorCode = '" + ddlEditColorPriority.SelectedValue + "'";

            clsQueries.executeQuery(sql);

            clsUtil.ShowToastr(this.Page, "Successfully Edited Priority", "success");

            DisplayPriorityList();
            DisplaySectionList();
            DisplayCategoryList();
            DisplayNatureOfProbList();
            DisplayRoleAdminandITPIC();
            DisplayRole();

        }

        protected void lnkDeactivate_Click(object sender, EventArgs e)
        {
            HiddenField hfIsActiveId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfAdminIsActive") as HiddenField;
            HiddenField hfAdminEmpId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfAdminEmployeeId") as HiddenField;

            if (Session["EmployeeNo"].ToString() == hfAdminEmpId.Value.ToString())
            {
                clsUtil.ShowToastr(this.Page, "You cannot delete your own Account! Try again", "error");
            }
            else
            {
                string sql = "";

                if (hfIsActiveId.Value.ToString() == "active")
                {
                    sql = "EXEC sp_vgHelpDesk_Admin_UpdateRoleStatusInactive ";
                    sql += " @EmployeeNo='" + hfAdminEmpId.Value.ToString() + "'";

                    clsQueries.executeQuery(sql);
                    Session["ToastrMessage"] = "Successfully Deleted Role to the User!";
                    Session["ToastrType"] = "success";

                    Response.Redirect("Maintenance.aspx");
                }
            }
        }

        protected void lnkActivate_Click(object sender, EventArgs e)
        {
            HiddenField hfIsActiveId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfAdminIsActive") as HiddenField;
            HiddenField hfAdminEmpId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfAdminEmployeeId") as HiddenField;

            string sql = "";

            if (hfIsActiveId.Value.ToString() == "inactive")
            {
                sql = "EXEC sp_vgHelpDesk_Admin_UpdateRoleStatusActive ";
                sql += " @EmployeeNo='" + hfAdminEmpId.Value.ToString() + "'";

                clsQueries.executeQuery(sql);
            }

            DisplayPriorityList();
            DisplaySectionList();
            DisplayCategoryList();
            DisplayNatureOfProbList();
            DisplayRoleAdminandITPIC();
            DisplayRole();

        }

        protected void lnkDeleteSection_Click(object sender, EventArgs e)
        {
            HiddenField hfSectionId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfSectionId") as HiddenField;

            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_UpdateSectionInactive ";
            sql += " @SectionId='" + hfSectionId.Value.ToString() + "'";

            clsQueries.executeQuery(sql);

            Session["ToastrMessage"] = "Successfully Deleted Section!";
            Session["ToastrType"] = "success";

            Response.Redirect("Maintenance.aspx");
        }

        protected void lnkDeleteCategory_Click(object sender, EventArgs e)
        {

            HiddenField hfCategoryId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfCategoryId") as HiddenField;

            string sql = "";

            sql = " EXEC sp_vgHelpDesk_Admin_UpdateCategoryInactive";
            sql += " @CategoryId='" + hfCategoryId.Value.ToString() + "'";

            clsQueries.executeQuery(sql);

            Session["ToastrMessage"] = "Successfully Deleted Category!";
            Session["ToastrType"] = "success";

            Response.Redirect("Maintenance.aspx");

        }

        protected void lnkDeleteNatureOfProb_Click(object sender, EventArgs e)
        {
            HiddenField hfNatureOfProb = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfNatureOfProb") as HiddenField;

            string sql = "";

            sql = " EXEC sp_vgHelpDesk_Admin_UpdateNatureOfProblemInactive ";
            sql += " @NatureOfProbId='" + hfNatureOfProb.Value.ToString() + "'";

            clsQueries.executeQuery(sql);

            Session["ToastrMessage"] = "Successfully Deleted Nature of Problem!";
            Session["ToastrType"] = "success";

            Response.Redirect("Maintenance.aspx");
        }

        protected void lnkDeletePriority_Click(object sender, EventArgs e)
        {
            HiddenField hfPriorityId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfPriority") as HiddenField;

            string sql = "";

            sql = " EXEC sp_vgHelpDesk_Admin_UpdatePriorityInactive  ";
            sql += " @PriorityId='" + hfPriorityId.Value.ToString() + "'";

            clsQueries.executeQuery(sql);

            Session["ToastrMessage"] = "Successfully Deleted Priority!";
            Session["ToastrType"] = "success";

            Response.Redirect("Maintenance.aspx");
        }
    }
}