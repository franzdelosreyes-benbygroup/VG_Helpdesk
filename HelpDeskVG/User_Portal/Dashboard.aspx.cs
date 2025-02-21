using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpDeskVG.Classes;

namespace HelpDeskVG.User_Portal
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                DisplayUserTickets();
                DisplayPendingApprovalResolved();
                DisplayRejectedTicketsByAdmin();
                DisplaySection();
            }
        }

        public void UserDisplayAttachments()
        {

            string sql = "";
            DataTable dt = new DataTable();

            sql = @"EXEC sp_vgHelpDesk_GetAttachmentFiles @employee_no = '" + Session["EmployeeNo"].ToString() + "'";

            dt = clsQueries.fetchData(sql);
            //gvTicketsList.DataSource = dt;
            //gvTicketsList.DataBind();

            dt.Dispose();

        }

        protected void DisplayUserTickets()
        {
            string sql = "";
            sql = @"SELECT a.[description], [status], ticket_id ,ticket_code, a.created_at, a.approval_transactional_level, b.description_category, c.description_section, d.description_natureofprob, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_for, CONCAT(f.description, ' || ', f.alloted_hour,'HRS') AS priority_level,
                    CASE WHEN a.approval_transactional_level = 0 THEN 'True' ELSE 'False' END AS is_draft
                    FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_for
					LEFT JOIN m_Priority AS f ON f.priority_id = a.priority_id
                    WHERE a.created_for =" + Session["EmployeeNo"].ToString() + " AND a.approval_transactional_level IN ('0', '1','3','4', '8', '9')";

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvUserTicketList.DataSource = dt;
            gvUserTicketList.DataBind();
            gvUserTicketList.Dispose();
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

            gvUserPendingApproval.DataSource = dt;
            gvUserPendingApproval.DataBind();
            gvUserPendingApproval.Dispose();

        }

        protected void DisplaySection()
        {
            clsQueries.DisplaySection(ddlSectionMd);
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

        protected void DisplayRejectedTicketsByAdmin()
        {
            string sql = "";
            sql = @"SELECT a.ticket_id, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, a.created_at, a.ticket_code, a.admin_recent_reject_remarks, a.itpic_recent_reject_remarks, a.[description], a.itpic_recent_reject_remarks, a.admin_recent_reject_remarks, CONCAT(f.description, ' || ', f.alloted_hour,'HRS') AS priority_level FROM t_TicketHeader AS a INNER JOIN t_TicketStages AS b ON b.ticket_stage_id = a.ticket_stage_id 
            LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
			LEFT JOIN m_Priority AS f ON f.priority_id = a.priority_id
            
            WHERE a.approval_transactional_level = '2' AND a.created_for =" + Session["EmployeeNo"].ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            gvRejectedTicketByAdmin.DataSource = dt;
            gvRejectedTicketByAdmin.DataBind();
            gvRejectedTicketByAdmin.Dispose();
        }

        protected void gvUserTicketList_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvUserPendingApproval_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvRejectedTicketByAdmin_PageIndexChanged(object sender, EventArgs e)
        {

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
            sql = @"SELECT a.ticket_id, a.approval_transactional_level ,a.[subject], a.[description], a.ticket_code, a.created_for, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for_name, g.attachment_id, g.[data], g.[file_name], g.description AS description_attachment, g.content_type, a.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id
					WHERE a.approval_transactional_level IN ('0', '1', '3', '4', '8', '9') AND a.created_for =" + Session["EmployeeNo"].ToString() + " AND ticket_id=" + hfTicketHeaderId.Value.ToString();

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

                string approvalLevel = dt.Rows[0]["approval_transactional_level"].ToString();

                txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                txtCreatedFor.Text = dt.Rows[0]["created_for_name"].ToString();
         
                txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                txtOthers.Text = dt.Rows[0]["others"].ToString();
                txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();
                txtAttachmentDescriptionMd.Text = dt.Rows[0]["description_attachment"].ToString();

                txtCreatedFor.Visible = false;

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

                if (approvalLevel == "4")
                {
                    ddlCategoryMd.Enabled = false;
                    lblAttachNewAttachment.Visible = false;
                    fuUploadAttachmentInEdit.Visible = false;
                    lnkEditDetails.Visible = false;
                }
                else if (approvalLevel == "8")
                {
                    ddlCategoryMd.Enabled = false;
                    lblAttachNewAttachment.Visible = false;
                    fuUploadAttachmentInEdit.Visible = false;
                    lnkEditDetails.Visible = false;
                }
                else if(approvalLevel == "1")
                {
                    ddlCategoryMd.Enabled = false;
                    lblAttachNewAttachment.Visible = false;
                    fuUploadAttachmentInEdit.Visible = false;
                    lnkEditDetails.Visible = false;
                }
                else if (approvalLevel == "9")
                {
                    ddlCategoryMd.Enabled = false;
                    lblAttachNewAttachment.Visible = false;
                    fuUploadAttachmentInEdit.Visible = false;
                    lnkEditDetails.Visible = false;
                }
                else if(approvalLevel == "3")
                {
                    ddlCategoryMd.Enabled = false;
                    lblAttachNewAttachment.Visible = false;
                    fuUploadAttachmentInEdit.Visible = false;
                    lnkEditDetails.Visible = false;
                }
                else 
                {
                    lnkEditDetails.Visible = true;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
            dt.Dispose();
        }

        protected void lnkTicketApprovalUser_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);

            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdAcceptTicket") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.approval_transactional_level ,a.[subject], a.[description], a.ticket_code, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
                    LEFT JOIN m_Category AS b ON b.category_id = a.category_id
                    LEFT JOIN m_Section AS c ON c.section_id = a.section_id
                    LEFT JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS e ON e.employee_code = a.created_by
                    LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_for
                    LEFT JOIN t_AttachmentReport AS g ON a.ticket_id  =  g.ticket_header_id
					LEFT JOIN m_Priority AS h ON h.priority_id = a.priority_id


					WHERE a.approval_transactional_level = '6' AND a.created_for =" + Session["EmployeeNo"].ToString() + " AND a.ticket_id=" +hfTicketHeaderId.Value.ToString();

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);


                txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
                txtCreatedFor.Text = dt.Rows[0]["created_for"].ToString();
                ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
                ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
                ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
                txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
                txtOthers.Text = dt.Rows[0]["others"].ToString();
                txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();
                ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();


                txtCreatedBy.Enabled = false;
                txtCreatedFor.Enabled = false;
                txtSubjectMd.Enabled = false;
                txtOthers.Enabled = false;
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
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);

            dt.Dispose();
        }
    
        

        protected void lnkDetailsRejectedTicketList_Click(object sender, EventArgs e)
        {
            clsQueries.DisplaySection(ddlSectionMd);
            clsQueries.DisplayCategory(ddlCategoryMd);
            clsQueries.DisplayNatureOfProblem(ddlNatureofprobMd);
            clsQueries.DisplayPriority(ddlPriorityMd);

            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderIdRejectedList") as HiddenField;

            string sql = "";
            sql = @"SELECT a.ticket_id, a.[subject], a.[description], a.ticket_code, b.category_id, c.section_id, d.nature_of_prob_id, a.others, CONCAT(e.employee_first_name, ' ', e.employee_last_name) AS created_by, CONCAT(f.employee_first_name, ' ', f.employee_last_name) AS created_for, g.attachment_id, g.[data], g.[file_name], g.content_type, h.priority_id FROM t_TicketHeader AS a
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


            txtCreatedBy.Text = dt.Rows[0]["created_by"].ToString();
            txtCreatedFor.Text = dt.Rows[0]["created_for"].ToString();
            ddlSectionMd.SelectedValue = dt.Rows[0]["section_id"].ToString();
            ddlCategoryMd.SelectedValue = dt.Rows[0]["category_id"].ToString();
            ddlNatureofprobMd.SelectedValue = dt.Rows[0]["nature_of_prob_id"].ToString();
            txtSubjectMd.Text = dt.Rows[0]["subject"].ToString();
            txtOthers.Text = dt.Rows[0]["others"].ToString();
            txtDescriptionMd.Text = dt.Rows[0]["description"].ToString();
            ddlPriorityMd.SelectedValue = dt.Rows[0]["priority_id"].ToString();


            txtCreatedBy.Enabled = true;
            txtCreatedFor.Enabled = true;
            txtSubjectMd.Enabled = true;
            txtOthers.Enabled = true;
            txtDescriptionMd.Enabled = true;
            ddlSectionMd.Enabled = true;
            ddlCategoryMd.Enabled = true;
            ddlNatureofprobMd.Enabled = true;
            ddlPriorityMd.Enabled = true;

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


            DisplayUserTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();

        }

        protected void lnkRejectResolvedTicket_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "rejectSolutionModal();", true);
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
                                cmd.Parameters.AddWithValue("@Description", clsUtil.replaceQuote(txtAttachmentDescriptionMd.Text));
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


                sql = "EXEC sp_vgHelpDesk_User_UpdateDetailsTicket ";
                sql += "@TicketHeaderId ='" + ticketHeader + "',";
                sql += "@Transacted_By='" + Session["EmployeeNo"].ToString() + "',";
                sql += "@Description='" + txtDescriptionMd.Text + "',";
                sql += "@Subject='" + txtSubjectMd.Text + "',";
                sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";
                clsQueries.executeQuery(sql);

            }
            else
            {

                sql = "EXEC sp_vgHelpDesk_User_UpdateDetailsTicket ";
                sql += "@TicketHeaderId ='" + ticketHeader + "',";
                sql += "@Transacted_By='" + Session["EmployeeNo"].ToString() + "',";
                sql += "@Description='" + txtDescriptionMd.Text + "',";
                sql += "@Subject='" + txtSubjectMd.Text + "',";
                sql += "@Section= '" + ddlSectionMd.SelectedValue + "',";
                sql += "@Category= '" + ddlCategoryMd.SelectedValue + "',";
                sql += "@Priority= '" + ddlPriorityMd.SelectedValue + "',";
                sql += "@NatureOfProblem='" + ddlNatureofprobMd.SelectedValue + "'";

                clsQueries.executeQuery(sql);

                Response.Redirect("../User_Portal/Dashboard.aspx");

            }

            Response.Redirect("../User_Portal/Dashboard.aspx");
            clsUtil.ShowToastr(this.Page, "Please Try Again!", "warning");

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

        protected void lnkUserRejectProposedSolution_Click(object sender, EventArgs e)
        {

            hfUserRejectHeaderID.Value = hfTicketHeaderIdforResolved.Value;

            string ticketHeaderId = hfUserRejectHeaderID.Value.ToString();

            if (fuUploadAttachment.HasFile)
            {
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

                            string query = "insert into t_AttachmentReport(ticket_header_id,file_name,description,content_type,data,uploaded_by,created_at) values (@HDheaderId ,@FileName,@Description,@FileContentType,@FileBin,@Uploaded_By,CURRENT_TIMESTAMP)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                string filename = fuUploadAttachment.PostedFile.FileName.Replace(",", "");
                                string contentType = fuUploadAttachment.PostedFile.ContentType;

                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@FileName", filename);
                                cmd.Parameters.AddWithValue("@FileContentType", contentType);
                                cmd.Parameters.AddWithValue("@Description", clsUtil.replaceQuote(txtAttachmentDescription.Text));
                                cmd.Parameters.AddWithValue("@HDheaderId", ticketHeaderId);
                                cmd.Parameters.AddWithValue("@Uploaded_By", Session["EmployeeNo"].ToString());
                                cmd.Parameters.AddWithValue("@FileBin", bytes);

                                con.Open();
                                cmd.CommandTimeout = 600;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            gvHDUploadedAttachment.DataBind();

                        }
                    }
                }
                string sql = "";
                sql = "EXEC sp_vgHelpDesk_User_RejectResolvedTicket ";
                sql += "@Ticket_Header_Id='" + ticketHeaderId + "',";
                sql += "@UserRejectSolutionRemarks='" + clsUtil.replaceQuote(txtAttachmentDescription.Text) + "',";
                sql += "@Transacted_By='" + Session["EmployeeNo"].ToString() + "'";

                clsQueries.executeQuery(sql);

                DisplayUserTickets();
                DisplayPendingApprovalResolved();
                DisplayRejectedTicketsByAdmin();
            }

            else
            {
                string sql = "";
                sql = "EXEC sp_vgHelpDesk_User_RejectResolvedTicket ";
                sql += "@Ticket_Header_Id='" + ticketHeaderId + "'";
                sql += "@UserRejectSolutionRemarks='" + clsUtil.replaceQuote(txtAttachmentDescription.Text) + "',";
                sql += "@Transacted_By='" + Session["EmployeeNo"].ToString() + "'";


                clsQueries.executeQuery(sql);
            }
            DisplayUserTickets();
            DisplayPendingApprovalResolved();
            DisplayRejectedTicketsByAdmin();
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

            if(ddlCategoryMd.SelectedValue == "")
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

            if(ddlCategoryMd.SelectedValue == "")
            {
                ddlNatureofprobMd.SelectedValue = "";

                ddlNatureofprobMd.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);

            }
            else { 
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
            HiddenField hfTicketHeaderId = (((LinkButton)sender).NamingContainer as GridViewRow).FindControl("hfTicketHeaderId") as HiddenField;


            sql = "EXEC sp_vgHelpDesk_IsDraftDeleteTicket ";
            sql += "@TicketHeaderId='" + hfTicketHeaderId.Value.ToString() + "',";
            sql += "@IsDraft='" + hfIsDraftId.Value.ToString() + "'";

            clsQueries.executeQuery(sql);

            clsUtil.ShowToastr(this.Page, "Successfully Deleted the Ticket!", "success");

            DisplayUserTickets();
        }
    }
}