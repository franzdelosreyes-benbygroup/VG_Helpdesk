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

namespace HelpDeskVG.IT_PIC_Portal
{
    public partial class CreateTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmployeeNo"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                if (ddlSection.SelectedValue == "" || ddlSection.SelectedValue == "0")
                {
                    ddlCategory.Enabled = false;
                    ddlNatureOfProblem.Enabled = false;
                }

                else
                {
                    ddlCategory.Enabled = true;
                    ddlNatureOfProblem.Enabled = false;
                }
                DisplayPriorityLevel();
                DisplaySection();
                DisplayEmployees();
                fx_LoadUserAttachment();
            }
        }

        protected void lnkSaveTicket_Click(object sender, EventArgs e)
        {
            if (fuUploadAttachment.HasFile)
            {
                insertAttachment();
                insertDetailsTicket();
            }
            else
            {
                insertDetailsTicket();
            }

            clsUtil.ShowToastr(this.Page, "Ticket Successfully Saved!", "success");
            Response.Redirect("Dashboard.aspx");
        }

        private void fx_LoadUserAttachment()
        {
            string sql = "";
            string ticketHeaderId = Request.QueryString["Id"];
            sql = "SELECT * FROM t_AttachmentReport WHERE ticket_header_id =" + ticketHeaderId;
            DataTable dt = clsQueries.fetchData(sql);

            gvHDUploadedAttachment.DataSource = dt;
            gvHDUploadedAttachment.DataBind();
        }

        protected void DisplayEmployees()
        {
            clsQueries.DisplayEmployee(ddlEmployee);
        }
        protected void DisplayPriorityLevel()
        {
            clsQueries.DisplayPriority(ddlPriority);
        }

        protected void DisplaySection()
        {
            clsQueries.DisplaySection(ddlSection);
        }

        protected void DisplayCategory()
        {
            string sql = "";
            sql = @"SELECT category_id, section_id, [description_category] FROM m_Category WHERE is_active = '1' AND section_id = " + ddlSection.SelectedValue;

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "description_category";
            ddlCategory.DataValueField = "category_id";

            ddlCategory.DataBind();

            dt.Dispose();

            ddlCategory.Items.Insert(0, new ListItem("Please Select", ""));

        }

        protected void DisplayNatureOfProblem()
        {
            string sql = "";
            sql = @"SELECT nature_of_prob_id, [description_natureofprob], [category_id], [section_id] FROM m_NatureOfProblem WHERE is_active = '1' AND category_id = " + ddlCategory.SelectedValue + "AND section_id =" + ddlSection.SelectedValue;

            DataTable dt = new DataTable();
            dt = clsQueries.fetchData(sql);

            ddlNatureOfProblem.DataSource = dt;
            ddlNatureOfProblem.DataTextField = "description_natureofprob";
            ddlNatureOfProblem.DataValueField = "nature_of_prob_id";
            ddlNatureOfProblem.DataBind();

            dt.Dispose();

            ddlNatureOfProblem.Items.Insert(0, new ListItem("Please Select", ""));
        }


        protected void insertAttachment()
        {
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

                        string query = "insert into t_AttachmentReport(ticket_header_id,file_name,description,content_type,data,uploaded_by,created_at) values (@HDheaderId ,@FileName,@Description,@FileContentType,@FileBin,@Uploaded_By,CURRENT_TIMESTAMP)";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            string filename = fuUploadAttachment.PostedFile.FileName.Replace(",", "");
                            string contentType = fuUploadAttachment.PostedFile.ContentType;

                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@FileName", filename);
                            cmd.Parameters.AddWithValue("@FileContentType", contentType);
                            cmd.Parameters.AddWithValue("@Description", clsUtil.replaceQuote(txtAttachmentDescription.Text));
                            cmd.Parameters.AddWithValue("@HDheaderId", Request.QueryString["Id"].ToString());
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

        }

        protected void insertDetailsTicket()
        {
            string sql = "";

            if (txtCreatedAt.Text != "")
            {
                if (ddlEmployee.SelectedValue == "")
                {
                    sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsTicket ";
                    sql += "@Ticket_Header_Id='" + Request.QueryString["Id"] + "',";
                    sql += "@Description='" + clsUtil.replaceQuote(txtDescription.Text) + "',";
                    sql += "@Subject='" + clsUtil.replaceQuote(txtSubject.Text) + "',";
                    sql += "@Section= '" + ddlSection.SelectedValue + "',";
                    sql += "@Category= '" + ddlCategory.SelectedValue + "',";
                    sql += "@Priority= '" + ddlPriority.SelectedValue + "',";
                    sql += "@NatureOfProblem='" + ddlNatureOfProblem.SelectedValue + "',";
                    sql += "@Employee_No='" + Session["EmployeeNo"].ToString() + "',";
                    sql += "@CreatedAt= '" + txtCreatedAt.Text + " 00:00:00',";
                    sql += "@TransactedBy ='" + Session["EmployeeNo"].ToString() + "'";

                }
                else
                {
                    sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsTicket ";
                    sql += "@Ticket_Header_Id='" + Request.QueryString["Id"] + "',";
                    sql += "@Description='" + clsUtil.replaceQuote(txtDescription.Text) + "',";
                    sql += "@Subject='" + clsUtil.replaceQuote(txtSubject.Text) + "',";
                    sql += "@Section= '" + ddlSection.SelectedValue + "',";
                    sql += "@Category= '" + ddlCategory.SelectedValue + "',";
                    sql += "@Priority= '" + ddlPriority.SelectedValue + "',";
                    sql += "@NatureOfProblem ='" + ddlNatureOfProblem.SelectedValue + "',";
                    sql += "@Employee_No='" + Session["EmployeeNo"].ToString() + "',";
                    sql += "@CreatedAt= '" + txtCreatedAt.Text + " 00:00:00',";
                    sql += "@TransactedBy ='" + Session["EmployeeNo"].ToString() + "'";

                }
            }

            else
            {
                if (ddlEmployee.SelectedValue == "")
                {
                    sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsTicket ";
                    sql += "@Ticket_Header_Id='" + Request.QueryString["Id"] + "',";
                    sql += "@Description='" + clsUtil.replaceQuote(txtDescription.Text) + "',";
                    sql += "@Subject='" + clsUtil.replaceQuote(txtSubject.Text) + "',";
                    sql += "@Section= '" + ddlSection.SelectedValue + "',";
                    sql += "@Category= '" + ddlCategory.SelectedValue + "',";
                    sql += "@Priority= '" + ddlPriority.SelectedValue + "',";
                    sql += "@NatureOfProblem='" + ddlNatureOfProblem.SelectedValue + "',";
                    sql += "@Employee_No='" + Session["EmployeeNo"].ToString() + "',";
                    sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    sql += "@TransactedBy ='" + Session["EmployeeNo"].ToString() + "'";

                }
                else
                {
                    sql = "EXEC sp_vgHelpDesk_ITPIC_InsertDetailsTicket ";
                    sql += "@Ticket_Header_Id='" + Request.QueryString["Id"] + "',";
                    sql += "@Description='" + clsUtil.replaceQuote(txtDescription.Text) + "',";
                    sql += "@Subject='" + clsUtil.replaceQuote(txtSubject.Text) + "',";
                    sql += "@Section= '" + ddlSection.SelectedValue + "',";
                    sql += "@Category= '" + ddlCategory.SelectedValue + "',";
                    sql += "@Priority= '" + ddlPriority.SelectedValue + "',";
                    sql += "@NatureOfProblem ='" + ddlNatureOfProblem.SelectedValue + "',";
                    sql += "@Employee_No='" + Session["EmployeeNo"].ToString() + "',";
                    sql += "@CreatedAt= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    sql += "@TransactedBy ='" + Session["EmployeeNo"].ToString() + "'";

                }
            }

            clsQueries.executeQuery(sql);
            clsUtil.ShowToastr(this.Page, "Ticket Successfully Saved!", "success");
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSection.SelectedValue == "")
            {
                ddlNatureOfProblem.SelectedValue = "";
                ddlCategory.SelectedValue = "";

                ddlCategory.Enabled = false;
                ddlNatureOfProblem.Enabled = false;
            }

            else
            {
                DisplayCategory();
                ddlNatureOfProblem.SelectedValue = "";

                ddlCategory.Enabled = true;
                ddlNatureOfProblem.Enabled = false;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "")
            {
                ddlNatureOfProblem.SelectedValue = "";

                ddlCategory.Enabled = false;
                ddlNatureOfProblem.Enabled = false;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
            else
            {

                DisplayNatureOfProblem();

                ddlNatureOfProblem.SelectedValue = "";
                ddlSection.Enabled = true;
                ddlNatureOfProblem.Enabled = true;

                string sql = "";


                sql = @"SELECT category_id, section_id, [description_category] FROM m_Category WHERE is_active = '1' AND section_id = " + ddlSection.SelectedValue;

                DataTable dt = new DataTable();

                dt = clsQueries.fetchData(sql);

                if (dt.Rows.Count > 0)
                {
                    ddlSection.SelectedValue = dt.Rows[0]["section_id"].ToString();
                }

                dt.Dispose();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
        }

        protected void ddlNatureOfProblem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";

            if (ddlNatureOfProblem.SelectedValue == "")
            {
                ddlNatureOfProblem.SelectedValue = "";

                ddlNatureOfProblem.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);

            }
            else
            {
                sql = "SELECT * FROM m_NatureOfProblem WHERE is_active = '1' AND nature_of_prob_id = " + ddlNatureOfProblem.SelectedValue;

                DataTable dt = new DataTable();

                dt = clsQueries.fetchData(sql);

                if (dt.Rows.Count > 0)
                {
                    ddlSection.SelectedValue = dt.Rows[0]["section_id"].ToString();
                    ddlCategory.SelectedValue = dt.Rows[0]["category_id"].ToString();
                }

                dt.Dispose();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "detailsModal();", true);
            }
        }
    }
}