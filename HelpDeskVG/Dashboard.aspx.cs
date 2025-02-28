using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpDeskVG.Classes;
using static System.Collections.Specialized.BitVector32;

namespace HelpDeskVG
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplaySectionFilter();
 
                DisplayPriority();
                DisplayEmployees();
                DisplayEmployeesCreatedFor();
                DisplayEmployeeITPIC();
                DisplayITPICAssignTo();


                DisplayMyTickets();
                DisplayPendingApprovalResolved();
                DisplayRejectedTicketsByAdmin();
                DisplayUsersTickets();
                DisplayITPICReassignTickets();
                DisplayAssignedTickets();
                DisplayRejectedList();
            }
        }
        protected void DisplayPriority()
        {
            clsQueries.DisplayPriority(ddlPriorityFilter);
        }
        protected void DisplayEmployeesCreatedFor()
        {
            clsQueries.DisplayEmployee(ddlCreatedForVg);
        }
        protected void DisplayEmployees()
        {
            clsQueries.DisplayEmployee(ddlEmployeeVg);
        }
        protected void DisplayEmployeeITPIC()
        {
            clsQueries.DisplayITPICEmployee(ddlAssignToEmpITMd);
        }

        protected void DisplayITPICAssignTo()
        {
            clsQueries.DisplayITPICEmployee(ddlMdEmployeeITPIC);
        }

        protected void DisplayAttachments()
        {

            string sql = "";
            sql = "SELECT [file_name], content_type, [data] FROM t_AttachmentReport  WHERE ticket_id = ticketHeaderId";
            clsQueries.executeQuery(sql);
        }

        protected void DisplaySectionFilter()
        {
            clsQueries.DisplaySection(ddlSectionFilter);
        }

        protected void DisplayCategoryFilter()
        {
            string sql = "";
            sql = @"SELECT category_id, section_id, [description_category] FROM m_Category WHERE is_active = '1' AND section_id = " + ddlSectionFilter.SelectedValue;

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            ddlCategoryFilter.DataSource = dt;
            ddlCategoryFilter.DataTextField = "description_category";
            ddlCategoryFilter.DataValueField = "category_id";

            ddlCategoryFilter.DataBind();

            dt.Dispose();

            ddlCategoryFilter.Items.Insert(0, new ListItem("Please Select", ""));
        }

        protected void DisplayNatureOfProblemFilter()
        {
            string sql = "";
            sql = @"SELECT nature_of_prob_id, [description_natureofprob], [category_id], [section_id] FROM m_NatureOfProblem WHERE is_active = '1' AND category_id = " + ddlCategoryFilter.SelectedValue + "AND section_id =" + ddlSectionFilter.SelectedValue;

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            ddlNatureOfProbFilter.DataSource = dt;
            ddlNatureOfProbFilter.DataTextField = "description_natureofprob";
            ddlNatureOfProbFilter.DataValueField = "nature_of_prob_id";
            ddlNatureOfProbFilter.DataBind();

            dt.Dispose();

            ddlNatureOfProbFilter.Items.Insert(0, new ListItem("Please Select", ""));
        }

        protected void DisplayCategory()
        {
            string sql = "";
            sql = @"SELECT category_id, section_id, [description_category] FROM m_Category WHERE is_active = '1' AND section_id = " + ddlSectionMd.SelectedValue;

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            ddlCategoryMd.DataSource = dt;
            ddlCategoryMd.DataTextField = "description_category";
            ddlCategoryMd.DataValueField = "category_id";

            ddlCategoryMd.DataBind();

            dt.Dispose();

            ddlCategoryMd.Items.Insert(0, new ListItem("Please Select", ""));

        }

        protected void DisplayNatureOfProblem()
        {
            string sql = "";
            sql = @"SELECT nature_of_prob_id, [description_natureofprob], [category_id], [section_id] FROM m_NatureOfProblem WHERE is_active = '1' AND category_id = " + ddlCategoryMd.SelectedValue + "AND section_id =" + ddlSectionMd.SelectedValue;

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            ddlNatureofprobMd.DataSource = dt;
            ddlNatureofprobMd.DataTextField = "description_natureofprob";
            ddlNatureofprobMd.DataValueField = "nature_of_prob_id";
            ddlNatureofprobMd.DataBind();

            dt.Dispose();

            ddlNatureofprobMd.Items.Insert(0, new ListItem("Please Select", ""));
        }

        protected void DisplayITEmployee()
        {
            clsQueries.DisplayITPICEmployee(ddlAssignToEmpITMd);
        }

        protected void DisplayMyTickets()
        {
            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_DisplayMyTicket ";
            sql += "@DateTo='" + txtFilterDateTo.Text.ToString() + "',";
            sql += "@DateFrom='" + txtFilterDateFrom.Text.ToString() + "',";
            sql += "@ApprovalStatus='" + ddlTicketStatus.SelectedValue.ToString() + "',";
            sql += "@Priority='" + ddlPriorityFilter.SelectedValue.ToString() + "',";
            sql += "@TixCode='" + txtSearchTicket.Text.ToString() + "',";
            sql += "@NatureOfProb='" + ddlNatureOfProbFilter.SelectedValue.ToString() + "',";
            sql += "@Category='" + ddlCategoryFilter.SelectedValue.ToString() + "',";
            sql += "@Section='" + ddlSectionFilter.SelectedValue.ToString() + "',";
            sql += "@CreatedBy='" + ddlEmployeeVg.SelectedValue.ToString() + "',";
            sql += "@CreatedFor='" + Session["EmployeeNo"].ToString() + "'";    

            DataTable dt = new DataTable();

            dt = clsQueries.fetchData(sql);

            gvMyTicketList.DataSource = dt;
            gvMyTicketList.DataBind();
            lblMyCreatedTicketCount.Text = dt.Rows.Count.ToString();

            dt.Dispose();


        }
        protected void DisplayPendingApprovalResolved()
        {

            string sql = "";
            sql = @"SELECT a.[description], [status], ticket_id ,ticket_code, a.created_at, b.description_category, c.description_section, d.description_natureofprob, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_for, CONCAT(f.description, ' || ', f.alloted_hour,'HRS') AS priority_level FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_for 
					LEFT JOIN m_Priority AS f ON f.priority_id = a.priority_id
                    WHERE a.approval_transactional_level = '6' AND a.created_for =" + Session["EmployeeNo"].ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvMyTicketPendingApproval.DataSource = dt;
            gvMyTicketPendingApproval.DataBind();
            lblPendingApprovalResolvedCount.Text = dt.Rows.Count.ToString();

            gvMyTicketPendingApproval.Dispose();

        }

        protected void DisplayRejectedTicketsByAdmin()
        {
            string sql = "";
            sql = @"SELECT a.ticket_id, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, a.created_at, a.ticket_code, a.admin_recent_reject_remarks, a.itpic_recent_reject_remarks, a.[description], a.itpic_recent_reject_remarks, a.admin_recent_reject_remarks, CONCAT(f.description, ' || ', f.alloted_hour,'HRS') AS priority_level FROM t_TicketHeader AS a INNER JOIN t_TicketStages AS b ON b.ticket_stage_id = a.ticket_stage_id 
            LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
			LEFT JOIN m_Priority AS f ON f.priority_id = a.priority_id
            WHERE a.approval_transactional_level = '2' AND a.created_for =" + Session["EmployeeNo"].ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvMyTicketRejectedByAdmin.DataSource = dt;
            gvMyTicketRejectedByAdmin.DataBind();
            lblMyRejectedTicketCount.Text = dt.Rows.Count.ToString();

            gvMyTicketRejectedByAdmin.Dispose();
        }

        protected void DisplayUsersTickets()
        {
            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_DisplayUsersTicket ";
            sql += "@DateTo='" + txtFilterDateTo.Text.ToString() + "',";
            sql += "@DateFrom='" + txtFilterDateFrom.Text.ToString() + "',";
            sql += "@ApprovalStatus='" + ddlTicketStatus.SelectedValue.ToString() + "',";
            sql += "@Priority='" + ddlPriorityFilter.SelectedValue.ToString() + "',";
            sql += "@TixCode='" + txtSearchTicket.Text.ToString() + "',";
            sql += "@NatureOfProb='" + ddlNatureOfProbFilter.SelectedValue.ToString() + "',";
            sql += "@Category='" + ddlCategoryFilter.SelectedValue.ToString() + "',";
            sql += "@Section='" + ddlSectionFilter.SelectedValue.ToString() + "',";
            sql += "@CreatedBy='" + ddlEmployeeVg.SelectedValue.ToString() + "',";
            sql += "@CreatedFor='" + ddlCreatedForVg.SelectedValue.ToString() + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvAdminTicketList.DataSource = dt;
            gvAdminTicketList.DataBind();
            lblAssignTicketCount.Text = dt.Rows.Count.ToString();
            gvAdminTicketList.Dispose();
        }

        protected void DisplayITPICReassignTickets()
        {
            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_DisplayITPICReassignTickets ";
            sql += "@DateTo='" + txtFilterDateTo.Text.ToString() + "',";
            sql += "@DateFrom='" + txtFilterDateFrom.Text.ToString() + "',";
            sql += "@ApprovalStatus='" + ddlTicketStatus.SelectedValue.ToString() + "',";
            sql += "@Priority='" + ddlPriorityFilter.SelectedValue.ToString() + "',";
            sql += "@TixCode='" + txtSearchTicket.Text.ToString() + "',";
            sql += "@NatureOfProb='" + ddlNatureOfProbFilter.SelectedValue.ToString() + "',";
            sql += "@Category='" + ddlCategoryFilter.SelectedValue.ToString() + "',";
            sql += "@Section='" + ddlSectionFilter.SelectedValue.ToString() + "',";
            sql += "@CreatedBy='" + ddlEmployeeVg.SelectedValue.ToString() + "',";
            sql += "@CreatedFor='" + ddlCreatedForVg.SelectedValue.ToString() + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvAdminForReassignTicketList.DataSource = dt;
            gvAdminForReassignTicketList.DataBind();
            lblReassignTicketCount.Text = dt.Rows.Count.ToString();
            gvAdminForReassignTicketList.Dispose();
        }

        protected void DisplayAssignedTickets()
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_Admin_DisplayAssignedTickets ";
            sql += "@DateTo='" + txtFilterDateTo.Text.ToString() + "',";
            sql += "@DateFrom='" + txtFilterDateFrom.Text.ToString() + "',";
            sql += "@ApprovalStatus='" + ddlTicketStatus.SelectedValue.ToString() + "',";
            sql += "@Priority='" + ddlPriorityFilter.SelectedValue.ToString() + "',";
            sql += "@TixCode='" + txtSearchTicket.Text.ToString() + "',";
            sql += "@NatureOfProb='" + ddlNatureOfProbFilter.SelectedValue.ToString() + "',";
            sql += "@Category='" + ddlCategoryFilter.SelectedValue.ToString() + "',";
            sql += "@Section='" + ddlSectionFilter.SelectedValue.ToString() + "',";
            sql += "@CreatedBy='" + ddlEmployeeVg.SelectedValue.ToString() + "',";
            sql += "@CreatedFor='" + ddlCreatedForVg.SelectedValue.ToString() + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvAdminAssignedTicketList.DataSource = dt;
            gvAdminAssignedTicketList.DataBind();
            ITPICAssignedTicketCount.Text = dt.Rows.Count.ToString();
            gvAdminAssignedTicketList.Dispose();
        }

        protected void DisplayRejectedList()
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_Admin_DisplayRejectedList ";
            sql += "@DateTo='" + txtFilterDateTo.Text.ToString() + "',";
            sql += "@DateFrom='" + txtFilterDateFrom.Text.ToString() + "',";
            sql += "@ApprovalStatus='" + ddlTicketStatus.SelectedValue.ToString() + "',";
            sql += "@Priority='" + ddlPriorityFilter.SelectedValue.ToString() + "',";
            sql += "@TixCode='" + txtSearchTicket.Text.ToString() + "',";
            sql += "@NatureOfProb='" + ddlNatureOfProbFilter.SelectedValue.ToString() + "',";
            sql += "@Category='" + ddlCategoryFilter.SelectedValue.ToString() + "',";
            sql += "@Section='" + ddlSectionFilter.SelectedValue.ToString() + "',";
            sql += "@CreatedBy='" + ddlEmployeeVg.SelectedValue.ToString() + "',";
            sql += "@CreatedFor='" + ddlCreatedForVg.SelectedValue.ToString() + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvAdminForRejectedTicketList.DataSource = dt;
            gvAdminForRejectedTicketList.DataBind();
            lblAdminRejectedTicketToUserCount.Text = dt.Rows.Count.ToString();
            gvAdminForRejectedTicketList.Dispose();
        }

        protected void btnRejectTicket_Click(object sender, EventArgs e)
        {

        }

        protected void gvAdminTicketList_PageIndexChanged(object sender, EventArgs e)
        {
        }

        protected void gvAdminForReassignTicketList_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvAdminForRejectedTicketList_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvAdminAssignedTicketList_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {

        }

        protected void btnAssignToReassign_Click(object sender, EventArgs e)
        {

        }

        protected void btnRejectTicketReassign_Click(object sender, EventArgs e)
        {

        }

        protected void lnkUserTickets_Click(object sender, EventArgs e)
        {
            DisplayUsersTickets();

        }

        protected void lnkReassignITPIC_Click(object sender, EventArgs e)
        {
            DisplayITPICReassignTickets();
        }


        protected void lnkDetailsUserListTicket_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);

            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderId") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.[subject], a.[description], a.created_for, a.ticket_code, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, g.attachment_id, g.description AS description_attachmentreport, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
					WHERE a.approval_transactional_level = '1' AND a.ticket_id =" + hfTicketHeaderId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {

                if (hfTicketHeaderId.ToString() == "")
                {
                    clsUtil.ShowToastr(this.Page, "There is no Transaction Existing", "warning");

                }
                else
                {
                    try
                    {
                        ddlCreatedForMd.SelectedValue = dt.Rows[0]["created_for"].ToString();
                        ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                        ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                        ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                        ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();

                    }
                    catch
                    {
                        ddlCreatedForMd.SelectedValue = "";
                        ddlSectionMd.SelectedValue = "";
                        ddlCategoryMd.SelectedValue = "";
                        ddlNatureofprobMd.SelectedValue = "";
                        ddlPriorityMd.SelectedValue = "";
                    }


                    txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                    txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                    txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();
                    txtAttachmentDescriptionMd.Text = dt.Rows[0]["description_attachmentreport"].ToString();


                    hfMdTicketHeaderId.Value = hfTicketHeaderId.Value;

                    string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                    sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                    sql += "@TicketHeaderId ='" + ticketHeader + "'";

                    clsQueries.executeQuery(sql);

                    DataTable dtAttachment = new DataTable();
                    dtAttachment = clsQueries.fetchData(sql);

      
                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();
                    gvDownloadableAttachment.Dispose();

                    txtAttachmentDescriptionMd.Enabled = false;
                    lblassigntomd.Visible = false;
                    ddlAssignToEmpITMd.Visible = false;
                    lnkEditDetails.Visible = true;
                    lnkAssignTicketToITPIC.Visible = true;
                    lnkRejectTicketUser.Visible = true;
                    lnkAcceptTicketProposal.Visible = false;
                    lnkRejectTicketProposal.Visible = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
                }
                dt.Dispose();

            }

            dt.Dispose();
        }

        protected void lnkDetailsReassignITPIC_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);


            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdReassignITPIC") as HiddenField;

            hfMdTicketHeaderId.Value = hfTicketHeaderId.Value;

            string sql = "";
            sql = @"SELECT a.subject, a.description, a.ticket_id, a.ticket_code, a.created_for, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
                    LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
					WHERE a.approval_transactional_level = '5' AND a.ticket_id =" + hfTicketHeaderId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlCreatedForMd.SelectedValue = dt.Rows[0]["created_for"].ToString();
                    ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                    ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                    ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                    ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();

                }
                catch
                {
                    ddlCreatedForMd.SelectedValue = "";
                    ddlSectionMd.SelectedValue = "";
                    ddlCategoryMd.SelectedValue = "";
                    ddlNatureofprobMd.SelectedValue = "";
                    ddlPriorityMd.SelectedValue = "";
                }

                txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();


                string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                sql += "@TicketHeaderId ='" + ticketHeader + "'";

                clsQueries.executeQuery(sql);

                DataTable dtAttachment = new DataTable();
                dtAttachment = clsQueries.fetchData(sql);

                if (dtAttachment.Rows.Count > 0)
                {
                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();

                }
                lnkRejectTicketProposal.Visible = false;
                lnkAcceptTicketProposal.Visible = false;
                lnkEditDetails.Visible = true;
                lnkAssignTicketToITPIC.Visible = true;
                lnkRejectTicketUser.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
            dt.Dispose();
        }

        protected void lnkDetailsAssignedTicketList_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdAssignedTicketList") as HiddenField;


            string sql = "";
            sql = @"SELECT a.ticket_id, a.[subject], a.[description], a.ticket_code, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
					WHERE a.approval_transactional_level = '4' AND a.ticket_id =" + hfTicketHeaderId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                txtCreatedFor.Text = dt.Rows[0]["created_for"].ToString();
                ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                ddlCategoryMd.SelectedValue  = dt.Rows[0]["category_id"].ToString();
                ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();
                ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();


                string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                sql += "@TicketHeaderId ='" + ticketHeader + "'";

                clsQueries.executeQuery(sql);

                DataTable dtAttachment = new DataTable();
                dtAttachment = clsQueries.fetchData(sql);

                if (dtAttachment.Rows.Count > 0)
                {
                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();

                }
                txtDescriptionMd.Enabled = false;
                txtSubjectMd.Enabled = false;
                ddlCategoryMd.Enabled = false;
                ddlNatureofprobMd.Enabled = false;
                ddlSectionMd.Enabled = false;
                gvDownloadableAttachment.Enabled = true;

                lnkEditDetails.Visible = false;
                lnkAssignTicketToITPIC.Visible = false;
                lnkRejectTicketUser.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
            dt.Dispose();
        }

        protected void lnkDetailsRejectedTicketList_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);

            string sql = "";
            sql = @"SELECT a.ticket_id, a.[subject], a.[description], a.ticket_code, b.category_id, a.created_for, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for_text, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
					LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id

					WHERE a.approval_transactional_level = '2'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdRejectedList") as HiddenField;

                if (hfTicketHeaderId.ToString() == "")
                {
                    clsUtil.ShowToastr(this.Page, "There is no Transaction Existing", "warning");

                }
                else
                {
                    try
                    {
                        ddlCreatedForMd.SelectedValue = dt.Rows[0]["created_for"].ToString();
                        ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                        ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                        ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                        ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();

                    }
                    catch
                    {
                        ddlCreatedForMd.SelectedValue = "";
                        ddlSectionMd.SelectedValue = "";
                        ddlCategoryMd.SelectedValue = "";
                        ddlNatureofprobMd.SelectedValue = "";
                        ddlPriorityMd.SelectedValue = "";
                    }

                    txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                    txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                    txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();


                    hfMdTicketHeaderId.Value = hfTicketHeaderId.Value;

                    txtSubjectMd.Enabled = false;
                    txtDescriptionMd.Enabled = false;
                    ddlSectionMd.Enabled = false;
                    ddlCategoryMd.Enabled = false;
                    ddlNatureofprobMd.Enabled = false;
                    lnkEditDetails.Visible = false;
                    lnkAssignTicketToITPIC.Visible = false;
                    lnkRejectTicketUser.Visible = false;
                    lnkAcceptTicketProposal.Visible = false;
                    lnkRejectTicketProposal.Visible = false;

                    string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                    sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                    sql += "@TicketHeaderId ='" + ticketHeader + "'";

                    clsQueries.executeQuery(sql);

                    DataTable dtAttachment = new DataTable();
                    dtAttachment = clsQueries.fetchData(sql);

  
                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();

                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
                }
            }

            dt.Dispose();
        }

        protected void lnkEditDetails_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfMdTicketHeaderId.Value.ToString();
            string sql = "";

            if (fuUploadAttachmentInEdit.HasFile)
            {
                int iFileSize = fuUploadAttachmentInEdit.PostedFile.ContentLength;

                if (iFileSize > 5048576)
                {
                    clsUtil.ShowToastr(this.Page, "The file uploaded is less than 5mb, Please Upload again!", "warning");
                }

                using (Stream fs = fuUploadAttachmentInEdit.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string constr = ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString;

                        using (SqlConnection con = new SqlConnection(constr))
                        {

                            string query = "insert into t_AttachmentReport(ticket_header_id,file_name,description,content_type,data,uploaded_by,created_at) values (@HDheaderId ,@FileName,@Description,@FileContentType,@FileBin,@Uploaded_By,CURRENT_TIMESTAMP)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                string filename = fuUploadAttachmentInEdit.PostedFile.FileName.Replace(",", "");
                                string contentType = fuUploadAttachmentInEdit.PostedFile.ContentType;

                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@FileName", filename);
                                cmd.Parameters.AddWithValue("@FileContentType", contentType);
                                cmd.Parameters.AddWithValue("@Description", clsUtil.replaceQuote(txtNewAttachmentInEdit.Text));
                                cmd.Parameters.AddWithValue("@HDheaderId", ticketHeader);
                                cmd.Parameters.AddWithValue("@Uploaded_By", Session["EmployeeNo"].ToString());
                                cmd.Parameters.AddWithValue("@FileBin", bytes);

                                con.Open();
                                cmd.CommandTimeout = 600;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }

                sql = "EXEC sp_vgHelpDesk_Admin_UpdateDetailsTicket ";
                sql += "@TicketHeaderId ='" + ticketHeader + "',";
                sql += "@Admin_Emp_No='" + Session["EmployeeNo"].ToString() + "',";
                sql += "@Assigned_Emp_no='" + ddlAssignToEmpITMd.SelectedValue + "',";
                sql += "@Description='" + txtDescriptionMd.Text + "',";
                sql += "@Subject='" + txtSubjectMd.Text + "',";
                sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";

                clsQueries.executeQuery(sql);

                DisplayMyTickets();

                clsUtil.ShowToastr(this.Page, "Successfully Edited the Ticket!", "success");
            }
            else
            {
                sql = "EXEC sp_vgHelpDesk_Admin_UpdateDetailsTicket ";
                sql += "@TicketHeaderId ='" + ticketHeader + "',";
                sql += "@Admin_Emp_No='" + Session["EmployeeNo"].ToString() + "',";
                sql += "@Assigned_Emp_no='" + ddlAssignToEmpITMd.SelectedValue + "',";
                sql += "@Description='" + txtDescriptionMd.Text + "',";
                sql += "@Subject='" + txtSubjectMd.Text + "',";
                sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";

                clsQueries.executeQuery(sql);

                DisplayMyTickets();

                clsUtil.ShowToastr(this.Page, "Successfully Edited the Ticket!", "success");
            }

            DisplayMyTickets();
            DisplayUsersTickets();
            DisplayITPICReassignTickets();
            DisplayAssignedTickets();
        }

        protected void lnkRejectTicketUser_Click(object sender, EventArgs e)
        {

            txtRejectTicketRemarks.Enabled = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "rejectUserTicketRemarks();", true);
        }

        protected void lnkAssignTicketToITPIC_Click(object sender, EventArgs e)
        {
            DisplayEmployeeITPIC();
            ddlMdEmployeeITPIC.Enabled = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "assignITPICModal();", true);

        }

        protected void lnkAssignITPICTicket_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfMdTicketHeaderId.Value.ToString();
            string sql = "";

            sql = "EXEC sp_vgHelpDesk_Admin_AssignTicketToITPIC ";
            sql += "@TicketHeaderId = '" + ticketHeader + "',";
            sql += "@Assigned_Emp_No = '" + ddlMdEmployeeITPIC.SelectedValue + "',";
            sql += "@Admin_Assignor_Emp_No = '" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayUsersTickets();
            DisplayRejectedList();
            DisplayAssignedTickets();
            DisplayITPICReassignTickets();

            clsUtil.ShowToastr(this.Page, "Successfully Assigned IT PIC for the Ticket", "success");

        }

        protected void lnkMdRejectTicketAdminToUser_Click(object sender, EventArgs e)
        {

            string ticketHeader = hfMdTicketHeaderId.Value.ToString();

            string sql = "";
            sql = "EXEC sp_vgHelpDesk_Admin_RejectTicketUser ";
            sql += "@TicketHeaderId = '" + ticketHeader + "',";
            sql += "@RejectRemarks = '" + txtRejectTicketRemarks.Text + "',";
            sql += "@Admin_Assignor_Emp_No = '" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayUsersTickets();
            DisplayRejectedList();
            DisplayAssignedTickets();
            DisplayITPICReassignTickets();

            clsUtil.ShowToastr(this.Page, "Successfully Rejected the Ticket", "success");
            //Response.Redirect("Dashboard.aspx");

        }

        protected void lnkViewFile_Click(object sender, EventArgs e)
        {
            string attachment_id = (sender as LinkButton).CommandArgument.ToString();
            string sql = "";

            sql = " SELECT [data]";

        }

        protected void lnkDownloadFile_Click(object sender, EventArgs e)
        {
            string attachment_id = (sender as LinkButton).CommandArgument.ToString();
            byte[] bytes;
            string file_name, content_type;
            string constr = ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT [data], content_type, file_name FROM t_AttachmentReport WHERE attachment_id=@attachment_id";
                    cmd.Parameters.AddWithValue("@attachment_id", attachment_id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["data"];
                        content_type = sdr["content_type"].ToString();
                        file_name = sdr["file_name"].ToString();
                    }
                    con.Close();
                }
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = content_type;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + file_name);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        protected void gvDownloadableAttachment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewFile")
            {
                int attachmentId = Convert.ToInt32(e.CommandArgument);
                ViewFile(attachmentId);
            }
            else if (e.CommandName == "DownloadFile")
            {
                int attachmentId = Convert.ToInt32(e.CommandArgument);
                DownloadFile(attachmentId);
            }
        }

        private void ViewFile(int attachmentId)
        {
            DataTable dtFile = GetAttachmentById(attachmentId);

            if (dtFile.Rows.Count > 0)
            {
                byte[] fileData = (byte[])dtFile.Rows[0]["data"];
                string contentType = dtFile.Rows[0]["content_type"].ToString();

                Response.Clear();
                Response.ContentType = contentType;
                Response.AddHeader("Content-Length", fileData.Length.ToString());
                Response.BinaryWrite(fileData);
                Response.End();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FileNotFound", "alert('File not found.');", true);
            }
        }


        private void DownloadFile(int attachmentId)
        {
            DataTable dtFile = GetAttachmentById(attachmentId);

            if (dtFile.Rows.Count > 0)
            {
                byte[] fileData = (byte[])dtFile.Rows[0]["data"];
                string contentType = dtFile.Rows[0]["content_type"].ToString();
                string fileName = dtFile.Rows[0]["file_name"].ToString();

                Response.Clear();
                Response.ContentType = contentType;
                Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.AddHeader("Content-Length", fileData.Length.ToString());
                Response.BinaryWrite(fileData);
                Response.End();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FileNotFound", "alert('File not found.');", true);
            }
        }
        private DataTable GetAttachmentById(int attachmentId)
        {
            string sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentById @AttachmentId = " + attachmentId;
            return clsQueries.fetchData(sql);
        }

        protected void lnkDetailsMyTicket_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);
            clsQueries.DisplayITPICEmployee(ddlAssignToEmpITMd);

            HiddenField hfMyTicketITPIC = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderMyTicket") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.[subject], a.[description], a.assigned_emp_no, a.approval_transactional_level, a.ticket_code, a.created_for, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
					WHERE a.approval_transactional_level IN ('0', '3', '1', '4', '8', '9') AND a.created_for =" + Session["EmployeeNo"].ToString( ) + " AND ticket_id=" + hfMyTicketITPIC.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                if (hfMyTicketITPIC.Value.ToString() == "")
                {
                    clsUtil.ShowToastr(this.Page, "There is no Transaction Existing", "warning");

                }
                else
                {
                    txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                    txtCreatedFor.Text = dt.Rows[0]["created_for"].ToString();
                    try
                    {
                        ddlCreatedForMd.SelectedValue = dt.Rows[0]["created_for"].ToString();
                        ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                        ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                        ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                        ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();
                        ddlAssignToEmpITMd.SelectedValue = dt.Rows[0]["assigned_emp_no"].ToString();
                     
                    }
                    catch
                    {
                        ddlCreatedForMd.SelectedValue = "";
                        ddlSectionMd.SelectedValue = "";
                        ddlCategoryMd.SelectedValue = "";
                        ddlNatureofprobMd.SelectedValue = "";
                        ddlPriorityMd.SelectedValue = "";
                        ddlMdEmployeeITPIC.SelectedValue = "";
                    }

                    string approvalLevel = dt.Rows[0]["approval_transactional_level"].ToString();


                    txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                    txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();

                    hfMdTicketHeaderId.Value = hfMyTicketITPIC.Value;

                    string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                    sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                    sql += "@TicketHeaderId ='" + ticketHeader + "'";

                    clsQueries.executeQuery(sql);

                    DataTable dtAttachment = new DataTable();
                    dtAttachment = clsQueries.fetchData(sql);


                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();


                    if (approvalLevel == "0")
                    {
                        lnkAcceptTicketProposal.Visible = false;
                        lnkRejectTicketProposal.Visible = false;
                        lnkAssignTicketToITPIC.Visible = false;
                        lnkRejectTicketUser.Visible = false;
                        txtAttachmentDescriptionMd.Enabled = false;
                        lnkEditDetails.Visible = true;
                    }
                    else if (approvalLevel == "2")
                    {
                        lnkAcceptTicketProposal.Visible = false;
                        lnkRejectTicketProposal.Visible = false;
                        txtAttachmentDescriptionMd.Enabled = false;
                        lnkAssignTicketToITPIC.Visible = false;
                        lnkRejectTicketUser.Visible=false;
                        lnkEditDetails.Visible = true;
                    }
                    else
                    {
                        lnkAcceptTicketProposal.Visible = false;
                        lnkRejectTicketProposal.Visible = false;
                        txtCreatedFor.Visible = false;
                        lblAttachNewAttachment.Visible = false;
                        fuUploadAttachmentInEdit.Visible = false;
                        lnkAssignTicketToITPIC.Visible = false;
                        lnkRejectTicketUser.Visible = false;
                        lnkEditDetails.Visible = false;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
                }
                dt.Dispose();
            }
            dt.Dispose();
        }

        protected void ddlSectionMd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSectionMd.SelectedValue == "")
            {
                ddlNatureofprobMd.SelectedValue = "";
                ddlCategoryMd.SelectedValue = "";

                ddlCategoryMd.Enabled = false;
                ddlNatureofprobMd.Enabled = false;
            }

            else
            {
                DisplayCategory();
                ddlNatureofprobMd.SelectedValue = "";

                ddlCategoryMd.Enabled = true;
                ddlNatureofprobMd.Enabled = false;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
        }

        protected void ddlNatureofprobMd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";

            if (ddlCategoryMd.SelectedValue == "")
            {
                ddlNatureofprobMd.SelectedValue = "";

                ddlNatureofprobMd.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);

            }
            else
            {
                sql = "SELECT * FROM m_NatureOfProblem WHERE is_active = '1' AND nature_of_prob_id = " + ddlNatureofprobMd.SelectedValue;

                DataTable dt = new DataTable();

                dt = clsQueries.fetchData(sql);

                if (dt.Rows.Count > 0)
                {
                    ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                    ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                }

                dt.Dispose();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
        }

        protected void lnkDeleteDraft_Click(object sender, EventArgs e)
        {
            string sql = "";
            HiddenField hfIsDraftId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfIsDraftId") as HiddenField;
            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderMyTicket") as HiddenField;


            sql = "EXEC sp_vgHelpDesk_IsDraftDeleteTicket ";
            sql += "@TicketHeaderId='" + hfTicketHeaderId.Value.ToString() + "',";
            sql += "@IsDraft='" + hfIsDraftId.Value.ToString() + "'";

            clsQueries.executeQuery(sql);

            clsUtil.ShowToastr(this.Page, "Successfully Deleted the Ticket!", "success");

            DisplayMyTickets();
        }

        protected void ddlCategoryMd_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCategoryMd.SelectedValue == "")
            {
                ddlNatureofprobMd.SelectedValue = "";

                ddlCategoryMd.Enabled = false;
                ddlNatureofprobMd.Enabled = false;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
            else
            {

                DisplayNatureOfProblem();

                ddlNatureofprobMd.SelectedValue = "";
                ddlSectionMd.Enabled = true;
                ddlNatureofprobMd.Enabled = true;

                string sql = "";


                sql = @"SELECT category_id, section_id, [description_category] FROM m_Category WHERE is_active = '1' AND section_id = " + ddlSectionMd.SelectedValue;

                DataTable dt = new DataTable();

                dt = clsQueries.fetchData(sql);

                if (dt.Rows.Count > 0)
                {
                    ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                }

                dt.Dispose();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
        }

        protected void lnkFilterMyTicket_Click(object sender, EventArgs e)
        {
            DisplayMyTickets();
            DisplayUsersTickets();
            DisplayITPICReassignTickets();
            DisplayAssignedTickets();
            DisplayRejectedList();
        }

        protected void ddlSectionFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSectionFilter.SelectedValue == "")
            {
                ddlNatureOfProbFilter.SelectedValue = "";
                ddlCategoryFilter.SelectedValue = "";

                ddlCategoryFilter.Enabled = false;
                ddlNatureOfProbFilter.Enabled = false;
            }

            else
            {
                DisplayCategoryFilter();
                ddlNatureOfProbFilter.SelectedValue = "";

                ddlCategoryFilter.Enabled = true;
                ddlNatureOfProbFilter.Enabled = false;
            }

        }

        protected void ddlCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoryFilter.SelectedValue == "")
            {
                ddlNatureOfProbFilter.SelectedValue = "";

                ddlCategoryFilter.Enabled = false;
                ddlNatureOfProbFilter.Enabled = false;

            }
            else
            {

                DisplayNatureOfProblemFilter();

                ddlNatureOfProbFilter.SelectedValue = "";
                ddlSectionFilter.Enabled = true;
                ddlNatureOfProbFilter.Enabled = true;

                string sql = "";


                sql = @"SELECT category_id, section_id, [description_category] FROM m_Category WHERE is_active = '1' AND section_id = " + ddlSectionFilter.SelectedValue;

                DataTable dt = new DataTable();

                dt = clsQueries.fetchData(sql);

                if (dt.Rows.Count > 0)
                {
                    ddlSectionFilter.SelectedValue = dt.Rows[0]["section_id"].ToString();
                }

                dt.Dispose();
            }
        }

        protected void ddlNatureOfProbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";

            if (ddlNatureOfProbFilter.SelectedValue == "")
            {
                ddlNatureOfProbFilter.Enabled = false;

                DisplayNatureOfProblemFilter();
            }
            else
            {
                sql = "SELECT * FROM m_NatureOfProblem WHERE is_active = '1' AND nature_of_prob_id = " + ddlNatureOfProbFilter.SelectedValue;

                DataTable dt = new DataTable();

                dt = clsQueries.fetchData(sql);

                if (dt.Rows.Count > 0)
                {
                    ddlSectionFilter.SelectedValue = dt.Rows[0]["section_id"].ToString();
                    ddlCategoryFilter.SelectedValue = dt.Rows[0]["category_id"].ToString();
                }

                dt.Dispose();

            }
        }

        protected void gvMyTicketPendingApproval_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkTicketApprovalUser_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);

            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdAcceptTicket") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.approval_transactional_level ,a.[subject], a.created_for, a.[description], a.ticket_code, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for_text, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
					WHERE a.approval_transactional_level = '6' AND a.created_for =" + Session["EmployeeNo"].ToString() + " AND a.ticket_id=" + hfTicketHeaderId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);


            try
            {
                ddlCreatedForMd.SelectedValue = dt.Rows[0]["created_for"].ToString();
                ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();

            }
            catch
            {
                ddlCreatedForMd.SelectedValue = "";
                ddlSectionMd.SelectedValue = "";
                ddlCategoryMd.SelectedValue = "";
                ddlNatureofprobMd.SelectedValue = "";
                ddlPriorityMd.SelectedValue = "";
            }

            txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
            txtCreatedFor.Text = dt.Rows[0]["created_for"].ToString();
            txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
            txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();


            txtCreatedBy.Enabled = false;
            txtCreatedFor.Enabled = false;
            txtSubjectMd.Enabled = false;
            txtDescriptionMd.Enabled = false;
            ddlSectionMd.Enabled = false;
            ddlCategoryMd.Enabled = false;
            ddlNatureofprobMd.Enabled = false;
            ddlPriorityMd.Enabled = false;

            hfMdTicketHeaderId.Value = hfTicketHeaderId.Value;

            string ticketHeader = hfMdTicketHeaderId.Value.ToString();

            sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
            sql += "@TicketHeaderId ='" + ticketHeader + "'";

            clsQueries.executeQuery(sql);

            DataTable dtAttachment = new DataTable();
            dtAttachment = clsQueries.fetchData(sql);

            gvDownloadableAttachment.DataSource = dtAttachment;
            gvDownloadableAttachment.DataBind();
            gvDownloadableAttachment.Dispose();


            lnkEditDetails.Visible = false;
            lnkRejectTicketUser.Visible = false;
            lnkAssignTicketToITPIC.Visible = false;
            lnkRejectTicketProposal.Visible = false;
            lnkAcceptTicketProposal.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);

            dt.Dispose();
        }

        protected void lnkTicketApprovalResolvedDetails_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);

            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdAcceptTicket") as HiddenField;

            string sql = "";
            sql = @"SELECT a.subject, a.description, a.ticket_id, a.ticket_code, a.proposed_remarks, h.attachment_proposed_id, h.[data], h.description AS description_attachment, h.[file_name], h.content_type FROM t_TicketHeader AS a 
                    INNER JOIN t_TicketStages AS b ON b.ticket_stage_id = a.ticket_stage_id
					LEFT JOIN t_ProposedAttachment AS h ON a.ticket_id  =  h.ticket_header_id
					WHERE a.approval_transactional_level = '6' AND a.created_for =" + Session["EmployeeNo"].ToString() + " AND a.ticket_id=" + hfTicketHeaderId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);
            txtDescriptionAttachmentProposed.Text = dt.Rows[0]["description_attachment"].ToString();
            txtProposedRemarksMd.Text = dt.Rows[0]["proposed_remarks"].ToString();


            txtProposedRemarksMd.Enabled = false;
            txtDescriptionAttachmentProposed.Enabled = false;

            hfTicketHeaderIdforResolved.Value = hfTicketHeaderId.Value;

            string ticketHeader = hfTicketHeaderIdforResolved.Value.ToString();

            sql = "EXEC sp_vgHelpDesk_User_GetProposedAttachmentDetails ";
            sql += "@TicketHeaderId ='" + ticketHeader + "'";

            clsQueries.executeQuery(sql);

            DataTable dtAttachment = new DataTable();
            dtAttachment = clsQueries.fetchData(sql);


            gvDownloadAttachmentInResolved.DataSource = dtAttachment;
            gvDownloadAttachmentInResolved.DataBind();
            gvDownloadAttachmentInResolved.Dispose();

            lnkAcceptResolvedTicket.Visible = true;
            lnkRejectResolvedTicket.Visible = true;
            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "resolvedDetailsModal();", true);

            dt.Dispose();


        }

        protected void lnkAcceptResolvedTicket_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfTicketHeaderIdforResolved.Value.ToString();

            string sql = "";

            sql = "EXEC sp_vgHelpDesk_User_AcceptResolvedTicket";
            sql += " @TicketHeaderId ='" + ticketHeader + "',";
            sql += " @Transacted_By ='" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayMyTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();
            DisplayUsersTickets();
            DisplayITPICReassignTickets();
            DisplayAssignedTickets();
            DisplayRejectedList();
        }

        protected void lnkRejectResolvedTicket_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "rejectSolutionModal();", true);

        }

        protected void lnkUserRejectProposedSolution_Click(object sender, EventArgs e)
        {
            hfUserRejectHeaderID.Value = hfTicketHeaderIdforResolved.Value;

            string ticketHeaderId = hfUserRejectHeaderID.Value.ToString();

            if (fuUploadAttachmentReject.HasFile)
            {
                int iFileSize = fuUploadAttachmentReject.PostedFile.ContentLength;

                if (iFileSize > 5048576)
                {
                    clsUtil.ShowToastr(this.Page, "The file uploaded is less than 5mb, Please Upload again!", "warning");
                }

                using (Stream fs = fuUploadAttachmentReject.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string constr = ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString;

                        using (SqlConnection con = new SqlConnection(constr))
                        {

                            string query = "insert into t_AttachmentReport(ticket_header_id,file_name,description,content_type,data,uploaded_by,created_at) values (@HDheaderId ,@FileName,@Description,@FileContentType,@FileBin,@Uploaded_By,CURRENT_TIMESTAMP)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                string filename = fuUploadAttachmentReject.PostedFile.FileName.Replace(",", "");
                                string contentType = fuUploadAttachmentReject.PostedFile.ContentType;

                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@FileName", filename);
                                cmd.Parameters.AddWithValue("@FileContentType", contentType);
                                cmd.Parameters.AddWithValue("@Description", clsUtil.replaceQuote(txtAttachmentDescReject.Text));
                                cmd.Parameters.AddWithValue("@HDheaderId", ticketHeaderId);
                                cmd.Parameters.AddWithValue("@Uploaded_By", Session["EmployeeNo"].ToString());
                                cmd.Parameters.AddWithValue("@FileBin", bytes);

                                con.Open();
                                cmd.CommandTimeout = 600;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            gvHDUploadedAttachmentReject.DataBind();

                        }
                    }
                }
                string sql = "";
                sql = "EXEC sp_vgHelpDesk_User_RejectResolvedTicket ";
                sql += "@Ticket_Header_Id='" + ticketHeaderId + "',";
                sql += "@UserRejectSolutionRemarks='" + clsUtil.replaceQuote(txtRejectRemarks.Text) + "',";
                sql += "@Transacted_By='" + Session["EmployeeNo"].ToString() + "'";

                clsQueries.executeQuery(sql);

                DisplayMyTickets();
                DisplayPendingApprovalResolved();
                DisplayRejectedTicketsByAdmin();
                DisplayUsersTickets();
                DisplayITPICReassignTickets();
                DisplayAssignedTickets();
                DisplayRejectedList();
            }

            else
            {
                string sql = "";
                sql = "EXEC sp_vgHelpDesk_User_RejectResolvedTicket ";
                sql += "@Ticket_Header_Id='" + ticketHeaderId + "'";
                sql += "@UserRejectSolutionRemarks='" + clsUtil.replaceQuote(txtRejectRemarks.Text) + "',";
                sql += "@Transacted_By='" + Session["EmployeeNo"].ToString() + "'";

                clsQueries.executeQuery(sql);

                DisplayMyTickets();
                DisplayPendingApprovalResolved();
                DisplayRejectedTicketsByAdmin();
                DisplayUsersTickets();
                DisplayITPICReassignTickets();
                DisplayAssignedTickets();
                DisplayRejectedList();
            }
        }

        protected void lnkMyTicketRejectedTicketList_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);

            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdRejectedList") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.[subject], a.[description], a.ticket_code, a.created_for, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for_text, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
					WHERE a.approval_transactional_level = '2' AND a.created_for =" + Session["EmployeeNo"].ToString() + " AND a.ticket_id=" + hfTicketHeaderId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (hfTicketHeaderId.ToString() == "")
            {
                clsUtil.ShowToastr(this.Page, "There is no Transaction Existing", "warning");
            }

            else
            {
                try
                {
                    ddlCreatedForMd.SelectedValue = dt.Rows[0]["created_for"].ToString();
                    ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                    ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                    ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                    ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();
                }

                catch
                {
                    ddlCreatedForMd.SelectedValue = "";
                    ddlSectionMd.SelectedValue = "";
                    ddlCategoryMd.SelectedValue = "";
                    ddlNatureofprobMd.SelectedValue = "";
                    ddlPriorityMd.SelectedValue = "";
                }

                txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                txtCreatedFor.Text = dt.Rows[0]["created_for_text"].ToString();
                txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();

                txtCreatedBy.Enabled = true;
                txtCreatedFor.Enabled = true;
                txtSubjectMd.Enabled = true;
                txtDescriptionMd.Enabled = true;
                txtAttachmentDescriptionMd.Enabled = false;
                ddlSectionMd.Enabled = true;
                ddlCategoryMd.Enabled = true;
                ddlNatureofprobMd.Enabled = true;
                ddlPriorityMd.Enabled = true;


                lnkAcceptTicketProposal.Visible = false;
                lnkRejectTicketProposal.Visible = false;
                lnkAssignTicketToITPIC.Visible = false;
                lnkRejectTicketUser.Visible = false;
                lnkEditDetails.Visible = true;

                hfMdTicketHeaderId.Value = hfTicketHeaderId.Value;

                string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                sql += "@TicketHeaderId ='" + ticketHeader + "'";

                clsQueries.executeQuery(sql);

                DataTable dtAttachment = new DataTable();
                dtAttachment = clsQueries.fetchData(sql);

                gvDownloadableAttachment.DataSource = dtAttachment;
                gvDownloadableAttachment.DataBind();
                gvDownloadableAttachment.Dispose();


                lnkEditDetails.Visible = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
            dt.Dispose();
        }

        protected void lnkAcceptTicketProposal_Click(object sender, EventArgs e)
        {

            string ticketHeader = hfTicketHeaderIdforResolved.Value.ToString();

            string sql = "";

            sql = "EXEC sp_vgHelpDesk_User_AcceptResolvedTicket";
            sql += " @TicketHeaderId ='" + ticketHeader + "',";
            sql += " @Transacted_By ='" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayMyTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();
            DisplayUsersTickets();
            DisplayITPICReassignTickets();
            DisplayAssignedTickets();
            DisplayRejectedList();

            clsUtil.ShowToastr(this.Page, "Successfully Accepted the Ticket!", "success");
        }

        protected void lnkRejectTicketProposal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "rejectSolutionModal();", true);
        }

        protected void gvMyTicketRejectedByAdmin_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvMyTicketList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMyTicketList.PageIndex = e.NewPageIndex;
            DisplayMyTickets();
        }

        protected void gvMyTicketPendingApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMyTicketPendingApproval.PageIndex = e.NewPageIndex;
            DisplayPendingApprovalResolved();
        }

        protected void gvMyTicketRejectedByAdmin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMyTicketRejectedByAdmin.PageIndex = e.NewPageIndex;
            DisplayRejectedTicketsByAdmin();
        }

        protected void gvAdminTicketList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdminTicketList.PageIndex = e.NewPageIndex;
            DisplayUsersTickets();

        }

        protected void gvAdminForReassignTicketList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdminForReassignTicketList.PageIndex = e.NewPageIndex;
            DisplayITPICReassignTickets();
        }

        protected void gvAdminAssignedTicketList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdminAssignedTicketList.PageIndex = e.NewPageIndex;
            DisplayAssignedTickets();
        }

        protected void gvAdminForRejectedTicketList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdminForRejectedTicketList.PageIndex = e.NewPageIndex;
            DisplayRejectedList();
        }
    }
}