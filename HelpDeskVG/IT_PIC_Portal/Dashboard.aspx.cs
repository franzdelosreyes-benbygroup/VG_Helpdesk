using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime.Misc;
using HelpDeskVG.Classes;

namespace HelpDeskVG.IT_PIC_Portal
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmployeeNo"] == null)
                {
                    Response.Redirect("../Login.aspx");
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

                DisplaySectionFilter();
                DisplayPriority();
                DisplayEmployees();

                DisplayAcceptOrRejectTicket();
                DisplayAcceptedTicket();
                DisplayRejectedTicket();
                DisplayRejectedSolution();
                DisplayMyTickets();
                DisplayPendingApprovalResolved();
                DisplayRejectedTicketsByAdmin();

            }


        }

        protected void DisplayMyTickets()
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_ITPIC_DisplayMyTicket ";
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
            gvMyTicketList.Dispose();

        }

        protected void DisplayPendingApprovalResolved()
        {

            string sql = "";
            sql = "EXEC sp_vgHelpDesk_ITPIC_DisplayPendingApprovalResolved ";
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

            gvMyTicketPendingApproval.DataSource = dt;
            gvMyTicketPendingApproval.DataBind();
            lblPendingApprovalResolvedCount.Text = dt.Rows.Count.ToString();
            gvMyTicketPendingApproval.Dispose();

        }

        protected void DisplayRejectedTicketsByAdmin()
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_ITPIC_DisplayRejectedList ";
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

            gvMyTicketRejectedByAdmin.DataSource = dt;
            gvMyTicketRejectedByAdmin.DataBind();
            lblMyRejectedTicketCount.Text = dt.Rows.Count.ToString();
            gvMyTicketRejectedByAdmin.Dispose();
        }

        protected void DisplayAcceptOrRejectTicket()
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_ITPIC_DisplayAcceptOrRejectTicket ";
            sql += "@DateTo='" + txtFilterDateTo.Text.ToString() + "',";
            sql += "@DateFrom='" + txtFilterDateFrom.Text.ToString() + "',";
            sql += "@AssignedEmpNo='" + Session["EmployeeNo"].ToString() + "',";
            sql += "@ApprovalStatus='" + ddlTicketStatus.SelectedValue.ToString() + "',";
            sql += "@Priority='" + ddlPriorityFilter.SelectedValue.ToString() + "',";
            sql += "@TixCode='" + txtSearchTicket.Text.ToString() + "',";
            sql += "@NatureOfProb='" + ddlNatureOfProbFilter.SelectedValue.ToString() + "',";
            sql += "@Category='" + ddlCategoryFilter.SelectedValue.ToString() + "',";
            sql += "@Section='" + ddlSectionFilter.SelectedValue.ToString() + "',";
            sql += "@CreatedFor='" + ddlEmployeeVg.SelectedValue.ToString() + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvITPICAcceptOrRejectList.DataSource = dt;
            gvITPICAcceptOrRejectList.DataBind();
            lblITPICPendingTicketsToAcceptCount.Text = dt.Rows.Count.ToString();
            gvITPICAcceptOrRejectList.Dispose();
        }

        protected void DisplayAcceptedTicket()
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_ITPIC_DisplayAcceptedTicket ";
            sql += "@DateTo='" + txtFilterDateTo.Text.ToString() + "',";
            sql += "@DateFrom='" + txtFilterDateFrom.Text.ToString() + "',";
            sql += "@ApprovalStatus='" + ddlTicketStatus.SelectedValue.ToString() + "',";
            sql += "@Priority='" + ddlPriorityFilter.SelectedValue.ToString() + "',";
            sql += "@TixCode='" + txtSearchTicket.Text.ToString() + "',";
            sql += "@NatureOfProb='" + ddlNatureOfProbFilter.SelectedValue.ToString() + "',";
            sql += "@Category='" + ddlCategoryFilter.SelectedValue.ToString() + "',";
            sql += "@Section='" + ddlSectionFilter.SelectedValue.ToString() + "',";
            sql += "@CreatedBy='" + ddlEmployeeVg.SelectedValue.ToString() + "',";
            sql += "@AssignedEmpNo='" + Session["EmployeeNo"].ToString() + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvITPICAcceptedTickets.DataSource = dt;
            gvITPICAcceptedTickets.DataBind();
            lblAcceptedTicketCount.Text = dt.Rows.Count.ToString();
            gvITPICAcceptedTickets.Dispose();
        }

        protected void DisplayRejectedTicket()
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_ITPIC_DisplayRejectedTicket ";
            sql += "@DateTo='" + txtFilterDateTo.Text.ToString() + "',";
            sql += "@DateFrom='" + txtFilterDateFrom.Text.ToString() + "',";
            sql += "@ApprovalStatus='" + ddlTicketStatus.SelectedValue.ToString() + "',";
            sql += "@Priority='" + ddlPriorityFilter.SelectedValue.ToString() + "',";
            sql += "@TixCode='" + txtSearchTicket.Text.ToString() + "',";
            sql += "@NatureOfProb='" + ddlNatureOfProbFilter.SelectedValue.ToString() + "',";
            sql += "@Category='" + ddlCategoryFilter.SelectedValue.ToString() + "',";
            sql += "@Section='" + ddlSectionFilter.SelectedValue.ToString() + "',";
            sql += "@CreatedBy='" + ddlEmployeeVg.SelectedValue.ToString() + "',";
            sql += "@CreatedFor='" + ddlCreatedForMd.SelectedValue.ToString() + "',";
            sql += "@Assigned_Emp_No='" + Session["EmployeeNo"].ToString() + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvITPICRejectedTickets.DataSource = dt;
            gvITPICRejectedTickets.DataBind();
            lblITPICRejectedTicketToAdminCount.Text = dt.Rows.Count.ToString();
            gvITPICRejectedTickets.Dispose();
        }

        protected void DisplayRejectedSolution()
        {
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_ITPIC_DisplayRejectedSolution ";
            sql += "@DateTo='" + txtFilterDateTo.Text.ToString() + "',";
            sql += "@DateFrom='" + txtFilterDateFrom.Text.ToString() + "',";
            sql += "@ApprovalStatus='" + ddlTicketStatus.SelectedValue.ToString() + "',";
            sql += "@Priority='" + ddlPriorityFilter.SelectedValue.ToString() + "',";
            sql += "@TixCode='" + txtSearchTicket.Text.ToString() + "',";
            sql += "@NatureOfProb='" + ddlNatureOfProbFilter.SelectedValue.ToString() + "',";
            sql += "@Category='" + ddlCategoryFilter.SelectedValue.ToString() + "',";
            sql += "@Section='" + ddlSectionFilter.SelectedValue.ToString() + "',";
            sql += "@CreatedBy='" + ddlEmployeeVg.SelectedValue.ToString() + "',";
            sql += "@CreatedFor='" + ddlCreatedForMd.SelectedValue.ToString() + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvRejectedSolution.DataSource = dt;
            gvRejectedSolution.DataBind();
            lblITPICRejectedSolutionByUserCount.Text = dt.Rows.Count.ToString();
            gvRejectedSolution.Dispose();
        }

        protected void DisplaySectionFilter()
        {
            clsQueries.DisplaySection(ddlSectionFilter);
        }

        protected void DisplayPriority()
        {
            clsQueries.DisplayPriority(ddlPriorityFilter);
        }

        protected void DisplayEmployees()
        {
            clsQueries.DisplayEmployee(ddlEmployeeVg);
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

        protected void lnkMdITPICRejectTicketAdminToUser_Click(object sender, EventArgs e)
        {

        }

        protected void lnkRejectTicketUser_Click(object sender, EventArgs e)
        {
            txtITPICRejectTicketRemarks.Enabled = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "rejectITPICTicketRemarks();", true);
        }

        protected void lnkAcceptTicket_Click(object sender, EventArgs e)
        {
            string sql = "";

            string ticketHeader = hfMdTicketHeaderId.Value.ToString();


            sql = "EXEC sp_vgHelpDesk_ITPIC_AcceptTicket ";
            sql += "@TicketHeaderId ='" + ticketHeader + "',";
            sql += "@Transacted_By ='" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayAcceptOrRejectTicket();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();

            clsUtil.ShowToastr(this.Page, "Successfully Accepted the Ticket!", "success");

        }

        protected void gvITPICAcceptOrRejectList_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvITPICAcceptedTickets_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvITPICRejectedTickets_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkDetailsUserListTicket_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);

            HiddenField hfTicketHeaderIdITPIC = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdITPIC") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.[subject], a.[description], a.ticket_code, a.created_at, b.category_id, a.created_for, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for_text, g.attachment_id, g.[data], g.[file_name], g.description AS description_attachmentreport, g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
					WHERE a.approval_transactional_level = '3' AND a.is_active = '1' AND a.assigned_emp_no = " + Session["EmployeeNo"].ToString() + " AND a.ticket_id =" + hfTicketHeaderIdITPIC.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                if (hfTicketHeaderIdITPIC.ToString() == "")
                {
                    clsUtil.ShowToastr(this.Page, "There is no Transaction Existing", "warning");

                }
                else
                {
                    DateTime createdAt = Convert.ToDateTime(dt.Rows[0]["created_at"]);
                    txtCreatedAt.Text = createdAt.ToString("yyyy-MM-ddTHH:mm");
                    txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                    txtCreatedFor.Text = dt.Rows[0]["created_for_text"].ToString();
                    try
                    {
                        ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                        ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                        ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                        ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();
                        ddlCreatedForMd.SelectedValue = dt.Rows[0]["created_for"].ToString();

                    }
                    catch
                    {
                        ddlSectionMd.SelectedValue = "";
                        ddlCategoryMd.SelectedValue = "";
                        ddlNatureofprobMd.SelectedValue = "";
                        ddlPriorityMd.SelectedValue = "";
                        ddlCreatedForMd.SelectedValue = "";
                    }

                    txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                    txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();
                    txtAttachmentDescriptionMd.Text = dt.Rows[0]["description_attachmentreport"].ToString();

                    hfMdTicketHeaderId.Value = hfTicketHeaderIdITPIC.Value;

                    string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                    sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                    sql += "@TicketHeaderId ='" + ticketHeader + "'";

                    clsQueries.executeQuery(sql);

                    DataTable dtAttachment = new DataTable();
                    dtAttachment = clsQueries.fetchData(sql);


                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();

                    //lnkSaveReceivedDate.Visible = false;
                    //lnkProposedTicketResolution.Visible = false;
                    //lnkTagThisToThirdParty.Visible = false;
                    lnkSaveReceivedDate.Visible = false;
                    ddlPriorityMd.Enabled = false;
                    ddlSectionMd.Enabled = false;
                    txtAttachmentDescriptionMd.Enabled = false;
                    fuUploadAttachmentInEdit.Visible = false;
                    txtNewAttachmentInEdit.Visible = false;
                    lnkAcceptTicket.Visible = true;
                    lnkRejectTicketUser.Visible = true;
                    lnkAcceptWithThirdParty.Visible = true;
                    lnkProposedTicketResolution.Visible = false;
                    lnkEditDetails.Visible = false;
                    lnkReject3rdParty.Visible = false;
                    lnkITPICReject3rdParty.Visible = false;
                    lnkSaveAsDraft.Visible = false;

                    lnkTagThisToThirdParty.Visible = false;
                    lblAttachNewAttachment.Visible = false;
                    lblNewAttachmentInEdit.Visible = false;
                    txtNewAttachmentInEdit.Visible = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
                }
                dt.Dispose();
            }
            dt.Dispose();

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
                    cmd.CommandText = "SELECT [data], content_type, file_name FROM t_ProposedAttachment WHERE attachment_proposed_id=@attachment_id";
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

        protected void lnkMdITPICRejectTicketToAdmin_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfMdTicketHeaderId.Value.ToString();
            string sql = "";
            sql = "EXEC sp_vgHelpDesk_ITPIC_RejectTicket ";
            sql += "@TicketHeaderId = '" + ticketHeader + "',";
            sql += "@RejectRemarks = '" + clsUtil.replaceQuote(txtITPICRejectTicketRemarks.Text) + "',";
            sql += "@ITPIC_Assigned_Emp_No = '" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayAcceptOrRejectTicket();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();

            clsUtil.ShowToastr(this.Page, "Successfully Rejected the Ticket", "success");

        }

        protected void lnkDetailsRejectedTicketList_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);



            string sql = "";
            sql = @"SELECT a.subject, a.description, a.ticket_id, a.ticket_code, a.created_at, a.created_for, c.description_section, c.section_id, d.description_category, d.category_id, e.description_natureofprob, e.nature_of_prob_id, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_by, CONCAT(g.employee_first_name, ' ', g.employee_last_name) AS created_for_text, h.attachment_id, h.[data], h.[file_name], h.content_type, i.priority_id FROM t_TicketHeader AS a
                    INNER JOIN t_TicketStages AS b ON b.ticket_stage_id = a.ticket_stage_id
                    INNER JOIN m_Section AS c ON c.section_id = a.section_id
                    INNER JOIN m_Category AS d ON d.category_id = a.category_id
                    INNER JOIN m_NatureOfProblem AS e ON e.nature_of_prob_id = a.nature_of_problem_id
					INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_by
					INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS g ON g.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS h ON a.ticket_id = h.ticket_header_id
					LEFT JOIN m_Priority AS i ON i.priority_id = a.priority_id
                    WHERE a.approval_transactional_level = '5' AND a.[assigned_emp_no_log] =" + Session["EmployeeNo"].ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdITPICRejectedList") as HiddenField;

                if (hfTicketHeaderId.ToString() == "")
                {
                    clsUtil.ShowToastr(this.Page, "There is no Transaction Existing", "warning");

                }
                else
                {
                    DateTime createdAt = Convert.ToDateTime(dt.Rows[0]["created_at"]);
                    txtCreatedAt.Text = createdAt.ToString("yyyy-MM-ddTHH:mm");
                    txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                    txtCreatedFor.Text = dt.Rows[0]["created_for_text"].ToString();
                    txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                    txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();

                    try
                    {
                        ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                        ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                        ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                        ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();
                        ddlCreatedForMd.SelectedValue = dt.Rows[0]["created_for"].ToString();

                    }
                    catch
                    {
                        ddlSectionMd.SelectedValue = "";
                        ddlCategoryMd.SelectedValue = "";
                        ddlNatureofprobMd.SelectedValue = "";
                        ddlPriorityMd.SelectedValue = "";
                        ddlCreatedForMd.SelectedValue = "";
                    }


                    hfMdTicketHeaderId.Value = hfTicketHeaderId.Value;

                    string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                    sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                    sql += "@TicketHeaderId ='" + ticketHeader + "'";

                    clsQueries.executeQuery(sql);

                    DataTable dtAttachment = new DataTable();
                    dtAttachment = clsQueries.fetchData(sql);


                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();

                    txtSubjectMd.Enabled = false;
                    txtDescriptionMd.Enabled = false;
                    ddlSectionMd.Enabled = false;
                    ddlCategoryMd.Enabled = false;
                    ddlNatureofprobMd.Enabled = false;
                    ddlCreatedForMd.Enabled = false;
                    ddlPriorityMd.Enabled = false;
                    txtAttachmentDescriptionMd.Visible = false;
                    lblAttachDesccc.Visible = false;
                    lblAttachNewAttachment.Visible = false;
                    fuUploadAttachmentInEdit.Visible = false;
                    lblNewAttachmentInEdit.Visible = false;
                    txtNewAttachmentInEdit.Visible = false;

                    lnkReject3rdParty.Visible = false;
                    lnkAcceptTicket.Visible = false;
                    lnkRejectTicketUser.Visible = false;
                    lnkAcceptWithThirdParty.Visible = false;
                    lnkEditDetails.Visible = false;
                    lnkTagThisToThirdParty.Visible = false;
                    lnkSaveReceivedDate.Visible = false;
                    lnkProposedTicketResolution.Visible = false;
                    lnkSaveAsDraft.Visible = false;



                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
                }
            }

            dt.Dispose();
        }

        protected void lnkAcceptWithThirdParty_Click(object sender, EventArgs e)
        {
            lnkAcceptThirdPartMd.Visible = true;
            lnkUpdateToThirdPartyMd.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "acceptTicketThirdParty();", true);

        }

        protected void lnkAcceptThirdPartMd_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfMdTicketHeaderId.Value.ToString();

            string sql = "EXEC sp_vgHelpDesk_ITPIC_AcceptTicketThirdParty ";
            sql += "@TicketHeaderId = '" + ticketHeader + "',";
            sql += "@ThirdParty_Name = '" + txt3rdPartyName.Text + "',";
            sql += "@ThirdPartyDateGiven = '" + txtCalendarGivenTo.Text + "',";
            sql += "@Transacted_By = '" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayAcceptOrRejectTicket();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();
            DisplayMyTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();

            clsUtil.ShowToastr(this.Page, "Successfully Accepted the Ticket", "success");

            txt3rdPartyName.Text = "";
            txtCalendarGivenTo.Text = "";
        }

        protected void lnkAcceptedTicketDetails_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);

            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdAcceptedTicket") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.[subject], a.created_at, a.is_with_third_party, a.third_party_date_received, a.created_for, CONVERT(varchar, a.third_party_date_given, 120) AS third_party_date_given ,a.[description], a.ticket_code, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for_text, g.attachment_id, g.[data], g.[file_name], g.description AS descriptionreport, g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
                    WHERE a.approval_transactional_level = '4' AND a.[assigned_emp_no] =" + Session["EmployeeNo"].ToString() + " AND a.ticket_id =" + hfTicketHeaderId.Value.ToString();


            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);


            if (hfTicketHeaderId.ToString() == "")
            {
                clsUtil.ShowToastr(this.Page, "There is no Transaction Existing", "warning");
            }
            else
            {
                txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                txtCreatedFor.Text = dt.Rows[0]["created_for_text"].ToString();
                txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                txtAttachmentDescriptionMd.Text = dt.Rows[0]["descriptionreport"].ToString();
                string iswithThirdParty = dt.Rows[0]["is_with_third_party"].ToString();
                string iswithReceivedDateFromThirdParty = dt.Rows[0]["third_party_date_received"].ToString();
                hfMdTicketDateGiven3rdParty.Value = dt.Rows[0]["third_party_date_given"].ToString();
                DateTime createdAt = Convert.ToDateTime(dt.Rows[0]["created_at"]);
                txtCreatedAt.Text = createdAt.ToString("yyyy-MM-ddTHH:mm");

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


                hfMdTicketHeaderId.Value = hfTicketHeaderId.Value;

                string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                sql += "@TicketHeaderId ='" + ticketHeader + "'";

                clsQueries.executeQuery(sql);

                DataTable dtAttachment = new DataTable();
                dtAttachment = clsQueries.fetchData(sql);


                gvDownloadableAttachment.DataSource = dtAttachment;
                gvDownloadableAttachment.DataBind();

                if (iswithThirdParty == "False")
                {
                    txtSubjectMd.Enabled = false;
                    txtDescriptionMd.Enabled = false;
                    ddlSectionMd.Enabled = false;
                    ddlCategoryMd.Enabled = false;
                    ddlNatureofprobMd.Enabled = false;
                    ddlCreatedForMd.Enabled = false;


                    ddlPriorityMd.Enabled = false;
                    txtAttachmentDescriptionMd.Enabled = false;
                    lblAttachNewAttachment.Visible = false;
                    fuUploadAttachmentInEdit.Visible = false;
                    lblNewAttachmentInEdit.Visible = false;
                    txtNewAttachmentInEdit.Visible = false;
                    lnkAcceptTicket.Visible = false;
                    lnkRejectTicketUser.Visible = false;
                    lnkAcceptWithThirdParty.Visible = false;
                    lnkProposedTicketResolution.Visible = true;
                    lnkEditDetails.Visible = false;
                    lnkTagThisToThirdParty.Visible = true;
                    lnkSaveReceivedDate.Visible = false;
                    txtAttachmentDescriptionMd.Visible = false;
                    lblAttachDesccc.Visible = false;
                    lnkITPICReject3rdParty.Visible = false;
                    lnkSaveAsDraft.Visible = false;

                }

                else if (iswithThirdParty == "True")
                {
                    txtSubjectMd.Enabled = false;
                    txtDescriptionMd.Enabled = false;
                    ddlSectionMd.Enabled = false;
                    ddlCategoryMd.Enabled = false;
                    ddlNatureofprobMd.Enabled = false;
                    ddlCreatedForMd.Enabled = false;



                    ddlPriorityMd.Enabled = false;
                    txtAttachmentDescriptionMd.Enabled = false;
                    lblAttachNewAttachment.Visible = false;
                    fuUploadAttachmentInEdit.Visible = false;
                    lblNewAttachmentInEdit.Visible = false;
                    txtNewAttachmentInEdit.Visible = false;
                    lnkAcceptTicket.Visible = false;
                    lnkRejectTicketUser.Visible = false;
                    lnkAcceptWithThirdParty.Visible = false;
                    lnkProposedTicketResolution.Visible = true;
                    lnkEditDetails.Visible = false;
                    lnkTagThisToThirdParty.Visible = true;
                    txtAttachmentDescriptionMd.Visible = false;
                    lblAttachDesccc.Visible = false;
                    lnkTagThisToThirdParty.Visible = false;
                    lnkSaveReceivedDate.Visible = true;
                    lnkITPICReject3rdParty.Visible = true;
                    lnkSaveAsDraft.Visible = false;


                    if (iswithReceivedDateFromThirdParty != "")
                    {
                        lnkSaveReceivedDate.Visible = false;
                        lnkProposedTicketResolution.Visible = true;
                    }
                    else
                    {
                        lnkSaveReceivedDate.Visible = true;
                        lnkProposedTicketResolution.Visible = false;
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);

                dt.Dispose();
            }
        }

        protected void insertDetailsProposed()
        {

            string ticketHeader = hfMdTicketHeaderId.Value.ToString();

            string sql = "";

            sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsProposedSolution ";
            sql += " @TicketHeaderId='" + ticketHeader + "',";
            sql += " @ProposedRemarks='" + txtRemarksProposedSolution.Text + "',";
            sql += " @Transacted_By='" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

        }

        protected void insertProposedSolutionAttachment()
        {

            string ticketHeader = hfMdTicketHeaderId.Value.ToString();

            if (fuUploadAttachment.HasFile == false)
            {
                clsUtil.ShowToastr(this.Page, "Please Select a File!", "error");
                return;
            }
            int iFileSize = fuUploadAttachment.PostedFile.ContentLength;

            if (iFileSize > 5048576)
            {
                clsUtil.ShowToastr(this.Page, "The file uploaded is less than 5mb, Please Upload again!", "warning");
            }

            using (Stream fs = fuUploadAttachment.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string constr = ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        string query = "insert into t_ProposedAttachment(ticket_header_id,file_name,description,content_type,data,uploaded_by,created_at) values (@HDheaderId ,@FileName,@Description,@FileContentType,@FileBin,@Uploaded_By,CURRENT_TIMESTAMP)";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            string filename = fuUploadAttachment.PostedFile.FileName.Replace(",", "");
                            string contentType = fuUploadAttachment.PostedFile.ContentType;

                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@FileName", filename);
                            cmd.Parameters.AddWithValue("@FileContentType", contentType);
                            cmd.Parameters.AddWithValue("@Description", clsUtil.replaceQuote(txtAttachmentDescription.Text));
                            cmd.Parameters.AddWithValue("@HDheaderId", ticketHeader);
                            cmd.Parameters.AddWithValue("@Uploaded_By", Session["EmployeeNo"].ToString());
                            cmd.Parameters.AddWithValue("@FileBin", bytes);

                            con.Open();
                            cmd.CommandTimeout = 600;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        //gvHDUploadedAttachment.DataBind();
                    }
                }
            }

        }
        protected void insertDetailsProposedAgain()
        {

            string ticketHeader = hfTicketHeaderUserReject.Value.ToString();

            string sql = "";

            sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsProposedSolutionAgain ";
            sql += " @TicketHeaderId='" + ticketHeader + "',";
            sql += " @ProposedRemarks='" + clsUtil.replaceQuote(txtRemarksProposedSolutionAgain.Text) + "',";
            sql += " @Transacted_By='" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);
        }

        protected void insertProposedSolutionAttachmentAgain()
        {

            string ticketHeader = hfTicketHeaderUserReject.Value.ToString();

            if (fuUploadAttachmentAgain.HasFile == false)
            {
                clsUtil.ShowToastr(this.Page, "Please Select a File!", "error");
                return;
            }
            int iFileSize = fuUploadAttachmentAgain.PostedFile.ContentLength;

            if (iFileSize > 5048576)
            {
                clsUtil.ShowToastr(this.Page, "The file uploaded is less than 5mb, Please Upload again!", "warning");
            }

            using (Stream fs = fuUploadAttachmentAgain.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string constr = ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        string query = "insert into t_ProposedAttachment(ticket_header_id,file_name,description,content_type,data,uploaded_by,created_at) values (@HDheaderId ,@FileName,@Description,@FileContentType,@FileBin,@Uploaded_By,CURRENT_TIMESTAMP)";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            string filename = fuUploadAttachmentAgain.PostedFile.FileName.Replace(",", "");
                            string contentType = fuUploadAttachmentAgain.PostedFile.ContentType;

                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@FileName", filename);
                            cmd.Parameters.AddWithValue("@FileContentType", contentType);
                            cmd.Parameters.AddWithValue("@Description", clsUtil.replaceQuote(txtAttachmentDescriptionAgain.Text));
                            cmd.Parameters.AddWithValue("@HDheaderId", ticketHeader);
                            cmd.Parameters.AddWithValue("@Uploaded_By", Session["EmployeeNo"].ToString());
                            cmd.Parameters.AddWithValue("@FileBin", bytes);

                            con.Open();
                            cmd.CommandTimeout = 600;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        //gvHDUploadedAttachment.DataBind();

                    }
                }
            }
        }



        protected void lnkProposedTicketResolution_Click(object sender, EventArgs e)
        {
            fuUploadAttachment.Enabled = true;
            txtAttachmentDescription.Enabled = true;
            lnkSaveProposedSolution.Visible = true;

            string ticketHeaderId = hfMdTicketHeaderId.Value.ToString();

            string sql = "SELECT is_with_third_party, third_party_date_received FROM t_TicketHeader WHERE ticket_id =" + ticketHeaderId;
            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                string iswithThirdParty = dt.Rows[0]["is_with_third_party"].ToString();
                string iswithReceivedDateThirdParty = dt.Rows[0]["third_party_date_received"].ToString();

                if (iswithThirdParty == "False")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "resolvedTicket();", true);

                }

                else if (iswithThirdParty == "True")
                {
                    if (iswithReceivedDateThirdParty == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "validateDateReceivedThirdParty();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "resolvedTicket();", true);
                    }
                }
            }
        }

        protected void lnkSaveProposedSolution_Click(object sender, EventArgs e)
        {
            lnkResolveAgain.Enabled = false;
            lnkSaveProposedSolution.Enabled = true;

            string ticketHeaderId = hfMdTicketHeaderId.Value.ToString();

            if (fuUploadAttachment.HasFile)
            {
                insertProposedSolutionAttachment();
                insertDetailsProposed();

                txtRemarksProposedSolution.Text = "";
                txtAttachmentDescription.Text = "";

            }
            else
            {
                insertDetailsProposed();
                txtRemarksProposedSolution.Text = "";
                txtAttachmentDescription.Text = "";
            }

            DisplayAcceptOrRejectTicket();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();
            DisplayMyTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();

            clsUtil.ShowToastr(this.Page, "Successfully Saved as Resolved Ticket", "success");
        }


        protected void lnkRejectSolutionList_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);

            string sql = "";
            sql = @"SELECT a.subject, a.description, a.created_at, a.ticket_id, a.ticket_code, c.description_section, c.section_id, d.description_category, d.category_id, e.description_natureofprob, e.nature_of_prob_id, b.others, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_by, CONCAT(g.employee_first_name, ' ', g.employee_last_name) AS created_for, h.attachment_id, h.[data], h.[file_name], h.content_type, i.priority_id FROM t_TicketHeader AS a
                    INNER JOIN t_TicketStages AS b ON b.ticket_stage_id = a.ticket_stage_id
                    INNER JOIN m_Section AS c ON c.section_id = b.section_id
                    INNER JOIN m_Category AS d ON d.category_id = b.category_id
                    INNER JOIN m_NatureOfProblem AS e ON e.nature_of_prob_id = b.nature_of_problem_id
					INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_by
					INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS g ON g.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS h ON a.ticket_id = h.ticket_header_id
					LEFT JOIN m_Priority AS i ON i.priority_id = a.priority_id
                    WHERE a.approval_transactional_level = '7' AND a.[assigned_emp_no_log] =" + Session["EmployeeNo"].ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdITPICRejectedSolution") as HiddenField;

                if (hfTicketHeaderId.ToString() == "")
                {
                    clsUtil.ShowToastr(this.Page, "There is no Transaction Existing", "warning");

                }
                else
                {
                    txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                    txtCreatedFor.Text = dt.Rows[0]["created_for"].ToString();
                    txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                    txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();
                    DateTime createdAt = Convert.ToDateTime(dt.Rows[0]["created_at"]);
                    txtCreatedAt.Text = createdAt.ToString("yyyy-MM-ddTHH:mm");

                    try
                    {
                        ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString(); //all ddl pa try catch
                        ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString(); //try catch mo to tapos dapat blank value mangyari pag viewing  
                        ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                        ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();

                    }
                    catch
                    {
                        ddlSectionMd.SelectedValue = "";
                        ddlCategoryMd.SelectedValue = "";
                        ddlNatureofprobMd.SelectedValue = "";
                        ddlPriorityMd.SelectedValue = "";
                    }

                    hfMdTicketHeaderId.Value = hfTicketHeaderId.Value;

                    string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                    sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                    sql += "@TicketHeaderId ='" + ticketHeader + "'";

                    clsQueries.executeQuery(sql);

                    DataTable dtAttachment = new DataTable();
                    dtAttachment = clsQueries.fetchData(sql);


                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();

                    txtSubjectMd.Enabled = false;
                    txtDescriptionMd.Enabled = false;
                    ddlSectionMd.Enabled = false;
                    ddlCategoryMd.Enabled = false;
                    ddlNatureofprobMd.Enabled = false;

                    lnkAcceptTicket.Visible = false;
                    lnkRejectTicketUser.Visible = false;
                    lnkAcceptWithThirdParty.Visible = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
                }
            }

            dt.Dispose();
        }



        protected void lnkDownloadFileRejectedSolution_Click(object sender, EventArgs e)
        {

        }

        protected void lnkUserRejectRemarks_Click(object sender, EventArgs e)
        {
            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdITPICRejectedSolution") as HiddenField;

            string sql = "";
            sql = @"SELECT a.user_recent_rejected_solution_remarks, a.ticket_id,  h.attachment_id, h.[data], h.description AS description_attachment, h.[file_name], h.content_type FROM t_TicketHeader as a
                    LEFT JOIN t_AttachmentReport AS h ON a.ticket_id  =  h.ticket_header_id
					WHERE a.approval_transactional_level = '7' AND a.ticket_id=" + hfTicketHeaderId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            txtRejectProposedRemarks.Text = dt.Rows[0]["user_recent_rejected_solution_remarks"].ToString();
            txtAttachmentRejectDesc.Text = dt.Rows[0]["description_attachment"].ToString();


            txtRejectProposedRemarks.Enabled = false;
            txtAttachmentRejectDesc.Enabled = false;

            hfTicketHeaderUserReject.Value = hfTicketHeaderId.Value;

            string ticketHeader = hfTicketHeaderUserReject.Value.ToString();

            sql = "EXEC sp_vgHelpDesk_ITPIC_GetRejectedAttachmentDetails ";
            sql += "@TicketHeaderId ='" + ticketHeader + "'";

            clsQueries.executeQuery(sql);

            DataTable dtAttachment = new DataTable();
            dtAttachment = clsQueries.fetchData(sql);


            gvUserRejectSolutionAttachment.DataSource = dtAttachment;
            gvUserRejectSolutionAttachment.DataBind();
            gvUserRejectSolutionAttachment.Dispose();

            lnkResolveAgain.Visible = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "rejectedResolvedTicket();", true);
            dt.Dispose();

        }

        protected void lnkResolveAgain_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "resolvedTicketAgain();", true);
        }

        protected void gvRejectedSolution_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkUserRejectProposalDetails_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdITPICRejectedSolution") as HiddenField;

            string sql = "";
            sql = @"SELECT a.subject, a.description, a.ticket_id, a.ticket_code, a.created_at, c.description_section, c.section_id, d.description_category, d.category_id, e.description_natureofprob, e.nature_of_prob_id, b.others, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_by, CONCAT(g.employee_first_name, ' ', g.employee_last_name) AS created_for, h.attachment_id, h.[data], h.[file_name], h.content_type, i.priority_id FROM t_TicketHeader AS a
                    INNER JOIN t_TicketStages AS b ON b.ticket_stage_id = a.ticket_stage_id
                    INNER JOIN m_Section AS c ON c.section_id = b.section_id
                    INNER JOIN m_Category AS d ON d.category_id = b.category_id
                    INNER JOIN m_NatureOfProblem AS e ON e.nature_of_prob_id = b.nature_of_problem_id
					INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_by
					INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS g ON g.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS h ON a.ticket_id = h.ticket_header_id
					LEFT JOIN m_Priority AS i ON i.priority_id = a.priority_id
                    WHERE a.approval_transactional_level = '7' AND a.[assigned_emp_no] =" + Session["EmployeeNo"].ToString();

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
                    txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                    txtCreatedFor.Text = dt.Rows[0]["created_for"].ToString();
                    txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                    txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();
                    DateTime createdAt = Convert.ToDateTime(dt.Rows[0]["created_at"]);
                    txtCreatedAt.Text = createdAt.ToString("yyyy-MM-ddTHH:mm");

                    try
                    {
                        ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString(); //all ddl pa try catch
                        ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString(); //try catch mo to tapos dapat blank value mangyari pag viewing  
                        ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                        ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();

                    }
                    catch
                    {
                        ddlSectionMd.SelectedValue = "";
                        ddlCategoryMd.SelectedValue = "";
                        ddlNatureofprobMd.SelectedValue = "";
                        ddlPriorityMd.SelectedValue = "";
                    }

                    hfTicketHeaderUserReject.Value = hfTicketHeaderId.Value;

                    string ticketHeader = hfTicketHeaderUserReject.Value.ToString();

                    sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                    sql += "@TicketHeaderId ='" + ticketHeader + "'";

                    clsQueries.executeQuery(sql);

                    DataTable dtAttachment = new DataTable();
                    dtAttachment = clsQueries.fetchData(sql);


                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();

                    txtSubjectMd.Enabled = false;
                    txtDescriptionMd.Enabled = false;
                    ddlSectionMd.Enabled = false;
                    ddlCategoryMd.Enabled = false;
                    ddlNatureofprobMd.Enabled = false;

                    lnkSaveAsDraft.Visible = false;
                    lnkAcceptTicket.Visible = false;
                    lnkRejectTicketUser.Visible = false;
                    lnkAcceptWithThirdParty.Visible = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
                }
            }

            dt.Dispose();
        }

        protected void lnkSaveAsResolvedAgain_Click(object sender, EventArgs e)
        {
            if (fuUploadAttachmentAgain.HasFile)
            {
                insertProposedSolutionAttachmentAgain();
                insertDetailsProposedAgain();

                txtRemarksProposedSolutionAgain.Text = "";
                txtAttachmentDescriptionAgain.Text = "";
            }
            else
            {
                insertDetailsProposedAgain();
                txtAttachmentDescriptionAgain.Text = "";
            }

            DisplayAcceptOrRejectTicket();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();
            DisplayMyTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();

            clsUtil.ShowToastr(this.Page, "Successfully Saved as Resolved Ticket", "success");
        }

        protected void lnkDetailsMyTicket_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);

            HiddenField hfMyTicketITPIC = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderMyTicket") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.[subject], a.[description], a.approval_transactional_level, a.created_at, a.ticket_code, b.category_id, a.created_for, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
					WHERE a.approval_transactional_level IN ('0','1','2','3','4','5','6','7','8','9') AND a.created_for =" + Session["EmployeeNo"].ToString() + " AND ticket_id=" + hfMyTicketITPIC.Value.ToString();

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
                    DateTime createdAt = Convert.ToDateTime(dt.Rows[0]["created_at"]);
                    txtCreatedAt.Text = createdAt.ToString("yyyy-MM-ddTHH:mm");
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

                    txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                    txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();
                    string approvalLevel = dt.Rows[0]["approval_transactional_level"].ToString();

                    txtCreatedBy.Enabled = false;
                    ddlSectionMd.Enabled = true;
                    ddlCategoryMd.Enabled = false;
                    ddlNatureofprobMd.Enabled = false;
                    ddlPriorityMd.Enabled = true;
                    txtSubjectMd.Enabled = true;
                    txtDescriptionMd.Enabled = true;
                    ddlCreatedForMd.Enabled = true;


                    if (approvalLevel == "0")
                    {
                        lblAttachNewAttachment.Visible = true;
                        fuUploadAttachmentInEdit.Visible = true;
                        lblNewAttachmentInEdit.Visible = true;
                        txtNewAttachmentInEdit.Visible = true;
                        lnkEditDetails.Visible = true;
                        lnkSaveAsDraft.Visible = true;
                        txtCreatedAt.Enabled = true;



                        ddlCreatedForMd.Enabled = true;
                        txtSubjectMd.Enabled = true;
                        txtDescriptionMd.Enabled = true;
                        ddlSectionMd.Enabled = true;
                        ddlCategoryMd.Enabled = true;
                        ddlNatureofprobMd.Enabled = true;
                        lblAttachDesccc.Visible = false;
                        txtAttachmentDescriptionMd.Visible = false;
                        ddlPriorityMd.Enabled = true;
                    }

                    else if (approvalLevel == "2")
                    {
                        lblAttachNewAttachment.Visible = true;
                        fuUploadAttachmentInEdit.Visible = true;
                        lblNewAttachmentInEdit.Visible = true;
                        txtNewAttachmentInEdit.Visible = true;
                        lnkEditDetails.Visible = true;
                        lnkSaveAsDraft.Visible = false;
                        txtCreatedAt.Enabled = true;


                        ddlCreatedForMd.Enabled = true;
                        txtSubjectMd.Enabled = true;
                        txtDescriptionMd.Enabled = true;
                        ddlSectionMd.Enabled = true;
                        ddlCategoryMd.Enabled = true;
                        ddlNatureofprobMd.Enabled = true;
                        lblAttachDesccc.Visible = false;
                        txtAttachmentDescriptionMd.Visible = false;
                        ddlPriorityMd.Enabled = true;


                    }

                    else if (approvalLevel == "6")
                    {

                        lnkEditDetails.Visible = false;
                        clsUtil.ShowToastr(this.Page, "Check Pending List of Resolved Ticket to Accept the Proposed Solution!", "info");
                    }

                    else
                    {
                        fuUploadAttachmentInEdit.Visible = false;
                        lblAttachNewAttachment.Visible = false;
                        lnkEditDetails.Visible = false;
                        lnkSaveAsDraft.Visible = false;
                        lblNewAttachmentInEdit.Visible = false;
                        txtNewAttachmentInEdit.Visible = false;

                        ddlCreatedForMd.Enabled = false;
                        txtSubjectMd.Enabled = false;
                        txtDescriptionMd.Enabled = false;
                        ddlSectionMd.Enabled = false;
                        ddlCategoryMd.Enabled = false;
                        ddlNatureofprobMd.Enabled = false;
                        lblAttachDesccc.Visible = false;
                        txtAttachmentDescriptionMd.Visible = false;
                        ddlPriorityMd.Enabled = false;
                        txtCreatedAt.Enabled = false;


                    }



                    hfMdTicketHeaderId.Value = hfMyTicketITPIC.Value;

                    string ticketHeader = hfMdTicketHeaderId.Value.ToString();

                    sql = "EXEC sp_vgHelpDesk_Admin_GetAttachmentDetails ";
                    sql += "@TicketHeaderId ='" + ticketHeader + "'";

                    clsQueries.executeQuery(sql);

                    DataTable dtAttachment = new DataTable();
                    dtAttachment = clsQueries.fetchData(sql);


                    gvDownloadableAttachment.DataSource = dtAttachment;
                    gvDownloadableAttachment.DataBind();
                    lnkAcceptTicket.Visible = false;
                    lnkRejectTicketUser.Visible = false;
                    lnkAcceptWithThirdParty.Visible = false;
                    lnkProposedTicketResolution.Visible = false;
                    lnkTagThisToThirdParty.Visible = false;
                    lnkAcceptTicket.Visible = false;
                    lnkRejectTicketUser.Visible = false;
                    lnkSaveReceivedDate.Visible = false;
                    lnkReject3rdParty.Visible = false;


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
                }
                dt.Dispose();
            }
            dt.Dispose();
        }

        protected void gvMyTicketList_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkEditDetails_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfMdTicketHeaderId.Value.ToString();
            string sql = "";
            DateTime createdAt = DateTime.Parse(txtCreatedAt.Text);


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
                if (txtCreatedAt.Text != "")
                {
                    if (ddlCreatedForMd.SelectedValue == "")
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_UpdateDetailsTicket ";
                        sql += "@TicketHeaderId ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@CreatedAt= '" + createdAt.ToString("yyyy-MM-dd HH:mm:ss tt") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";
                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }

                    else
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_UpdateDetailsTicket ";
                        sql += "@TicketHeaderId ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + createdAt.ToString("yyyy-MM-dd HH:mm:ss tt") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";
                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                }
                else
                {
                    if (ddlCreatedForMd.SelectedValue == "")
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_UpdateDetailsTicket ";
                        sql += "@TicketHeaderId ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";
                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                    else
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_UpdateDetailsTicket ";
                        sql += "@TicketHeaderId ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";
                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                }
            }
            else
            {
                if (txtCreatedAt.Text != "")
                {
                    if (ddlCreatedForMd.SelectedValue == "")
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_UpdateDetailsTicket ";
                        sql += "@TicketHeaderId ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@CreatedAt= '" + createdAt.ToString("yyyy-MM-dd HH:mm:ss tt") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";
                        clsQueries.executeQuery(sql);


                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }

                    else
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_UpdateDetailsTicket ";
                        sql += "@TicketHeaderId ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + createdAt.ToString("yyyy-MM-dd HH:mm:ss tt") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";
                        clsQueries.executeQuery(sql);


                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                }

                else
                {
                    if (ddlCreatedForMd.SelectedValue == "")
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_UpdateDetailsTicket ";
                        sql += "@TicketHeaderId ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";
                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                    else
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_UpdateDetailsTicket ";
                        sql += "@TicketHeaderId ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";
                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                }
            }

            Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
            clsUtil.ShowToastr(this.Page, "Please Try Again!", "warning");

        }

        protected void lnkTagThisToThirdParty_Click(object sender, EventArgs e)
        {
            lnkAcceptThirdPartMd.Visible = false;
            lnkUpdateToThirdPartyMd.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "acceptTicketThirdParty();", true);

        }

        protected void lnkUpdateToThirdPartyMd_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfMdTicketHeaderId.Value.ToString();

            string sql = "EXEC sp_vgHelpDesk_ITPIC_UpdateTicketAsThirdParty ";
            sql += "@TicketHeaderId = '" + ticketHeader + "',";
            sql += "@ThirdParty_Name = '" + txt3rdPartyName.Text + "',";
            sql += "@ThirdPartyDateGiven = '" + txtCalendarGivenTo.Text + "',";
            sql += "@Transacted_By = '" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayAcceptOrRejectTicket();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();

            clsUtil.ShowToastr(this.Page, "Successfully Updated the Ticket", "success");

            txt3rdPartyName.Text = "";
            txtCalendarGivenTo.Text = "";
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

        protected void lnkFilterMyTicket_Click(object sender, EventArgs e)
        {
            DisplayMyTickets();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();
            DisplayMyTickets();
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

        protected void lnkTicketApprovalUser_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);

            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdAcceptTicket") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.approval_transactional_level ,a.[subject], a.created_at ,a.created_for, a.[description], a.ticket_code, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for_text, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
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
            DateTime createdAt = Convert.ToDateTime(dt.Rows[0]["created_at"]);
            txtCreatedAt.Text = createdAt.ToString("yyyy-MM-ddTHH:mm");
            txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
            txtCreatedFor.Text = dt.Rows[0]["created_for_text"].ToString();
            txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
            txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();

            ddlCreatedForMd.Visible = true;

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
            lnkAcceptWithThirdParty.Visible = false;
            lnkEditDetails.Visible = false;
            lnkTagThisToThirdParty.Visible = false;
            lnkSaveReceivedDate.Visible = false;
            lnkProposedTicketResolution.Visible = false;
            lnkReject3rdParty.Visible = false;
            lnkITPICReject3rdParty.Visible = false;
            lnkAcceptTicket.Visible = false;
            lnkRejectTicketUser.Visible = false;
            lnkSaveAsDraft.Visible = false;

            lblAttachmentDescription.Visible = false;
            lblAttachDesccc.Visible = false;
            txtAttachmentDescriptionMd.Visible = false;
            lblAttachNewAttachment.Visible = false;
            fuUploadAttachmentInEdit.Visible = false;
            lblNewAttachmentInEdit.Visible = false;
            txtNewAttachmentInEdit.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);

            dt.Dispose();
        }

        protected void lnkTicketApprovalResolvedDetails_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);
            clsQueries.DisplayEmployee(ddlCreatedForMd);

            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdAcceptTicket") as HiddenField;

            string sql = "";
            sql = @"SELECT a.subject, a.description, a.ticket_id, a.created_for, a.created_at, a.ticket_code, a.proposed_remarks, h.attachment_proposed_id, h.[data], h.description AS description_attachment, h.[file_name], h.content_type FROM t_TicketHeader AS a 
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
            lnkSaveReceivedDate.Visible = false;
            lnkTagThisToThirdParty.Visible = false;
            lnkEditDetails.Visible = false;
            lnkProposedTicketResolution.Visible = false;
            lnkTagThisToThirdParty.Visible = false;
            lnkAcceptWithThirdParty.Visible = false;
            lnkAcceptTicket.Visible = false;
            lnkRejectTicketUser.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "resolvedDetailsModal();", true);

            dt.Dispose();
        }

        protected void gvMyTicketPendingApproval_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkAcceptResolvedTicket_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfTicketHeaderIdforResolved.Value.ToString();

            string sql = "";

            sql = "EXEC sp_vgHelpDesk_User_AcceptResolvedTicket";
            sql += " @TicketHeaderId ='" + ticketHeader + "',";
            sql += " @Transacted_By ='" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            clsUtil.ShowToastr(this.Page, "Successfully Accepted the Ticket!", "success");

            DisplayAcceptOrRejectTicket();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();
            DisplayMyTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();
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

                clsUtil.ShowToastr(this.Page, "Successfully Rejected the Solution!", "success");

                DisplayAcceptOrRejectTicket();
                DisplayAcceptedTicket();
                DisplayRejectedTicket();
                DisplayRejectedSolution();
                DisplayMyTickets();
                DisplayPendingApprovalResolved();
                DisplayRejectedTicketsByAdmin();

                txtRejectRemarks.Text = "";
                txtAttachmentDescReject.Text = "";
            }

            else
            {
                string sql = "";
                sql = "EXEC sp_vgHelpDesk_User_RejectResolvedTicket ";
                sql += "@Ticket_Header_Id='" + ticketHeaderId + "',";
                sql += "@UserRejectSolutionRemarks='" + clsUtil.replaceQuote(txtRejectRemarks.Text) + "',";
                sql += "@Transacted_By='" + Session["EmployeeNo"].ToString() + "'";

                clsQueries.executeQuery(sql);

                clsUtil.ShowToastr(this.Page, "Successfully Rejected the Solution!", "success");

                DisplayAcceptOrRejectTicket();
                DisplayAcceptedTicket();
                DisplayRejectedTicket();
                DisplayRejectedSolution();
                DisplayMyTickets();
                DisplayPendingApprovalResolved();
                DisplayRejectedTicketsByAdmin();

                txtRejectRemarks.Text = "";
                txtAttachmentDescReject.Text = "";
            }
        }

        protected void gvMyTicketRejectedByAdmin_PageIndexChanged(object sender, EventArgs e)
        {

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
            sql = @"SELECT a.ticket_id, a.[subject], a.[description], a.ticket_code, a.created_at ,a.created_for, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for_text, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
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
                DateTime createdAt = Convert.ToDateTime(dt.Rows[0]["created_at"]);
                txtCreatedAt.Text = createdAt.ToString("yyyy-MM-ddTHH:mm");


                txtCreatedBy.Enabled = false;
                txtCreatedFor.Enabled = true;
                txtSubjectMd.Enabled = true;
                txtDescriptionMd.Enabled = true;
                txtAttachmentDescriptionMd.Enabled = false;
                ddlSectionMd.Enabled = true;
                ddlCategoryMd.Enabled = true;
                ddlNatureofprobMd.Enabled = true;
                ddlPriorityMd.Enabled = true;
                ddlCreatedForMd.Enabled = true;

                lnkSaveAsDraft.Visible = false;
                lnkAcceptTicket.Visible = false;
                lnkRejectTicketUser.Visible = false;
                lnkAcceptWithThirdParty.Visible = false;
                lnkProposedTicketResolution.Visible = false;
                lnkTagThisToThirdParty.Visible = false;
                lnkAcceptTicket.Visible = false;
                lnkRejectTicketUser.Visible = false;
                lnkSaveReceivedDate.Visible = false;
                lnkReject3rdParty.Visible = false;


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

        protected void lnkSaveDateReceived3rdPt_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfMdTicketHeaderId.Value.ToString();

            string sql = "EXEC sp_vgHelpDesk_ITPIC_ReceivedTicketThirdParty ";
            sql += "@TicketHeaderId = '" + ticketHeader + "',";
            sql += "@ThirdPartyDateReceived = '" + txt3rdPtReceivedDate.Text + "',";
            sql += "@Transacted_By = '" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayAcceptOrRejectTicket();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();
            DisplayMyTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();

            clsUtil.ShowToastr(this.Page, "Successfully Accepted the Ticket", "success");

            txt3rdPtReceivedDate.Text = "";
        }

        protected void lnkSaveReceivedDate_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "received3rdPartyITPIC();", true);
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

        protected void gvITPICAcceptOrRejectList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvITPICAcceptOrRejectList.PageIndex = e.NewPageIndex;
            DisplayAcceptOrRejectTicket();
        }

        protected void gvITPICAcceptedTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvITPICAcceptedTickets.PageIndex = e.NewPageIndex;
            DisplayAcceptedTicket();
        }

        protected void gvITPICRejectedTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvITPICRejectedTickets.PageIndex = e.NewPageIndex;
            DisplayRejectedTicket();
        }

        protected void gvRejectedSolution_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRejectedSolution.PageIndex = e.NewPageIndex;
            DisplayRejectedSolution();
        }

        protected void lnkDownloadFileRejectAttachment_Click(object sender, EventArgs e)
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

        protected void lnkITPICReject3rdParty_Click(object sender, EventArgs e)
        {
            lnkReject3rdParty.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "reject3rdPartyITPIC();", true);
        }
        protected void lnkReject3rdParty_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfMdTicketHeaderId.Value.ToString();

            string sql = "EXEC sp_vgHelpDesk_ITPIC_RejectSolutionThirdParty ";
            sql += "@TicketHeaderId = '" + ticketHeader + "',";
            sql += "@ThirdParty_RejectRemarks = '" + clsUtil.replaceQuote(txt3rdPtRejectReason.Text) + "',";
            sql += "@ThirdPartyDateRejected = '" + txt3rdPartyRejectedDate.Text + "',";
            sql += "@Transacted_By = '" + Session["EmployeeNo"].ToString() + "'";

            clsQueries.executeQuery(sql);

            DisplayAcceptOrRejectTicket();
            DisplayAcceptedTicket();
            DisplayRejectedTicket();
            DisplayRejectedSolution();
            DisplayMyTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();

            clsUtil.ShowToastr(this.Page, "Successfully Rejected", "success");

            txt3rdPtRejectReason.Text = "";
            txt3rdPartyRejectedDate.Text = "";
        }

        protected void lnkViewHistory_Click(object sender, EventArgs e)
        {
            HiddenField hfMyTicketITPIC = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderMyTicket") as HiddenField;

            string ticketHeader = hfMyTicketITPIC.Value.ToString();

            string sql = "EXEC sp_vgHelpDesk_ViewTicketHistory ";
            sql += "@TicketHeader ='" + ticketHeader + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                gvTicketHistory.DataSource = dt;
                gvTicketHistory.DataBind();
                dt.Dispose();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showTicketHistory();", true);

            }

            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        protected void lnkViewHistory3rdParty_Click(object sender, EventArgs e)
        {
            HiddenField hfMyTicketITPIC = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdAcceptedTicket") as HiddenField;

            string ticketHeader = hfMyTicketITPIC.Value.ToString();

            string sql = "EXEC sp_vgHelpDesk_ITPIC_View3rdPartyHistory ";
            sql += "@TicketHeader ='" + ticketHeader + "'";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                gvThirdPartyLogsHistory.DataSource = dt;
                gvThirdPartyLogsHistory.DataBind();
                dt.Dispose();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showTicketHistory3rdPtLogs();", true);

            }
        }

        protected void lnkDownloadFile1_Click(object sender, EventArgs e)
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

        protected void lnkSaveAsDraft_Click(object sender, EventArgs e)
        {
            string ticketHeader = hfMdTicketHeaderId.Value.ToString();
            string sql = "";
            DateTime createdAt = DateTime.Parse(txtCreatedAt.Text);


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
                if (txtCreatedAt.Text != "")
                {
                    if (ddlCreatedForMd.SelectedValue == "")
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsDraftTicket ";
                        sql += "@Ticket_Header_Id ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + createdAt.ToString("yyyy-MM-dd HH:mm:ss tt") + "',";
                        sql += "@CreatedFor= '" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "',";
                        sql += "@Employee_No='" + Session["EmployeeNo"].ToString() + "'";

                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                    else
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsDraftTicket ";
                        sql += "@Ticket_Header_Id ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + createdAt.ToString("yyyy-MM-dd HH:mm:ss tt") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "',";
                        sql += "@Employee_No='" + ddlCreatedForMd.SelectedValue + "'";

                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                }

                else
                {
                    if (ddlCreatedForMd.SelectedValue == "")
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsDraftTicket ";
                        sql += "@Ticket_Header_Id ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sql += "@CreatedFor= '" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "',";
                        sql += "@Employee_No='" + Session["EmployeeNo"].ToString() + "'";

                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                    else
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsDraftTicket ";
                        sql += "@Ticket_Header_Id ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "',";
                        sql += "@Employee_No='" + ddlCreatedForMd.SelectedValue + "'";

                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                }
                clsUtil.ShowToastr(this.Page, "Please Try Again!", "warning");
            }
            else
            {
                if (txtCreatedAt.Text != "")
                {
                    if (ddlCreatedForMd.SelectedValue == "")
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsDraftTicket ";
                        sql += "@Ticket_Header_Id ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + createdAt.ToString("yyyy-MM-dd HH:mm:ss tt") + "',";
                        sql += "@CreatedFor= '" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "',";
                        sql += "@Employee_No='" + Session["EmployeeNo"].ToString() + "'";

                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                    else
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsDraftTicket ";
                        sql += "@Ticket_Header_Id ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + createdAt.ToString("yyyy-MM-dd HH:mm:ss tt") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "',";
                        sql += "@Employee_No='" + ddlCreatedForMd.SelectedValue + "'";

                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                }

                else
                {
                    if (ddlCreatedForMd.SelectedValue == "")
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsDraftTicket ";
                        sql += "@Ticket_Header_Id ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sql += "@CreatedFor= '" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "',";
                        sql += "@Employee_No='" + Session["EmployeeNo"].ToString() + "'";

                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                    else
                    {
                        sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsDraftTicket ";
                        sql += "@Ticket_Header_Id ='" + ticketHeader + "',";
                        sql += "@TransactedBy='" + Session["EmployeeNo"].ToString() + "',";
                        sql += "@Description='" + clsUtil.replaceQuote(txtDescriptionMd.Text) + "',";
                        sql += "@Subject='" + clsUtil.replaceQuote(txtSubjectMd.Text) + "',";
                        sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                        sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                        sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                        sql += "@CreatedFor= '" + ddlCreatedForMd.SelectedValue + "',";
                        sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "',";
                        sql += "@Employee_No='" + ddlCreatedForMd.SelectedValue + "'";

                        clsQueries.executeQuery(sql);

                        Session["ToastrMessage"] = "Successfully Edited Details!";
                        Session["ToastrType"] = "success";

                        Response.Redirect("../IT_PIC_Portal/Dashboard.aspx");
                    }
                }
            }
        }
    }
}