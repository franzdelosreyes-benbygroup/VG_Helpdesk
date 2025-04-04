using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web; 
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpDeskVG.Classes;
using OfficeOpenXml;

namespace HelpDeskVG
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["EmployeeNo"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                CountClosedTicket();
                CountFullyResolved();
                CountAutoClose();
                CountTicketsUnresolved();
            }
        }

        protected void lnkTixClosedReport_Click(object sender, EventArgs e)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HelpDeskVG.ReportFiles.CLOSEDTICKET.xlsx");
            ExcelPackage pck = new ExcelPackage(stream);

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (pck)
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets["ClosedTicket"];
                ws.Cells["C1"].Value = DateTime.Now.ToString();

                DataTable _dt = new DataTable();

                string sql = @"SELECT  a.ticket_code, a.[status], a.subject, a.description, b.description_section, c.description_category, d.description_natureofprob, 
                            e.[description] AS priority_desc, CONCAT(g.employee_first_name, ' ', g.employee_last_name) AS ticket_owner, 
                            a.created_at, a.is_with_third_party, a.third_party_name, 
                            a.third_party_date_given, a.third_party_date_received, a.proposed_remarks FROM t_TicketHeader AS a

                            INNER JOIN m_Section AS b ON b.section_id = a.section_id
                            INNER JOIN m_Category AS c ON c.category_id = a.category_id
                            INNER JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                            INNER JOIN m_Priority AS e ON e.priority_id = a.priority_id
                            INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS g ON g.employee_code = a.created_for
                            WHERE a.approval_transactional_level = '8' AND a.[status] = 'CLOSED' ORDER BY a.ticket_code ASC";

                _dt = clsQueries.fetchData(sql);

                ws.Cells["A4"].LoadFromDataTable(_dt, false);

                ws.Column(3).Style.WrapText = false;
                ws.Column(4).Style.WrapText = false;

                ws.Column(10).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(13).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(14).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)
                ws.Column(15).Style.WrapText = false;


                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;  filename=ClosedTicketsReport" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx");
                response.BinaryWrite(pck.GetAsByteArray());
                response.Flush();
                response.End();
            }
        }

        protected void lnkTixFullyResolved_Click(object sender, EventArgs e)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HelpDeskVG.ReportFiles.TICKETRESOLVED.xlsx");
            ExcelPackage pck = new ExcelPackage(stream);

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (pck)
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets["ResolvedTicket"];
                ws.Cells["C1"].Value = DateTime.Now.ToString();

                DataTable _dt = new DataTable();

                string sql = @"SELECT a.ticket_code, a.[status], a.subject, a.description, b.description_section, c.description_category, d.description_natureofprob, 
                              e.[description] AS priority_desc, CONCAT(g.employee_first_name, ' ', g.employee_last_name) AS ticket_owner, 
                              a.created_at, a.is_with_third_party, a.third_party_name, 
                              a.third_party_date_given, a.third_party_date_received, a.proposed_remarks FROM t_TicketHeader AS a

                              INNER JOIN m_Section AS b ON b.section_id = a.section_id
                              INNER JOIN m_Category AS c ON c.category_id = a.category_id
                              INNER JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                              INNER JOIN m_Priority AS e ON e.priority_id = a.priority_id
                              INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_by
                              INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS g ON g.employee_code = a.created_for
                              WHERE a.approval_transactional_level = '6' AND a.[status] = 'RESOLVED' ORDER BY a.ticket_code ASC";

                _dt = clsQueries.fetchData(sql);

                ws.Cells["A5"].LoadFromDataTable(_dt, false);

                ws.Column(3).Style.WrapText = false;
                ws.Column(4).Style.WrapText = false;
                ws.Column(10).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(13).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(14).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)
                ws.Column(15).Style.WrapText = false;

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;  filename=TICKETRESOLVED" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx");
                response.BinaryWrite(pck.GetAsByteArray());
                response.Flush();
                response.End();
            }
        }

        protected void lnkTixAutoClosed_Click(object sender, EventArgs e)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HelpDeskVG.ReportFiles.AUTOCLOSEDTICKETS.xlsx");
            ExcelPackage pck = new ExcelPackage(stream);

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (pck)
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets["AutoClosedTicket"];
                ws.Cells["C1"].Value = DateTime.Now.ToString();

                DataTable _dt = new DataTable();

                string sql = @"SELECT  a.ticket_code, a.[status], a.subject, a.description, b.description_section, c.description_category, d.description_natureofprob, 
                              e.[description] AS priority_desc, CONCAT(g.employee_first_name, ' ', g.employee_last_name) AS ticket_owner, 
                              a.created_at, a.is_with_third_party, a.third_party_name, 
                              a.third_party_date_given, a.third_party_date_received, a.proposed_remarks FROM t_TicketHeader AS a

                              INNER JOIN m_Section AS b ON b.section_id = a.section_id
                              INNER JOIN m_Category AS c ON c.category_id = a.category_id
                              INNER JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                              INNER JOIN m_Priority AS e ON e.priority_id = a.priority_id
                              INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_by
                              INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS g ON g.employee_code = a.created_for
                              WHERE a.approval_transactional_level = '9' ORDER BY a.ticket_code ASC";

                _dt = clsQueries.fetchData(sql);

                ws.Cells["A5"].LoadFromDataTable(_dt, false);

                ws.Column(3).Style.WrapText = false;
                ws.Column(4).Style.WrapText = false;
                ws.Column(10).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(13).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(14).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)
                ws.Column(15).Style.WrapText = false;

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;  filename=AUTOCLOSEDTICKETS" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx");
                response.BinaryWrite(pck.GetAsByteArray());
                response.Flush();
                response.End();
            }
        }

        protected void lnkTixReportTimeToBeAssigned_Click(object sender, EventArgs e)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HelpDeskVG.ReportFiles.TIMETAKENTOBEASSIGNED.xlsx");
            ExcelPackage pck = new ExcelPackage(stream);

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (pck)
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets["TimeTakenToBeAssignedTicket"];
                ws.Cells["C1"].Value = DateTime.Now.ToString();

                DataTable _dt = new DataTable();

                string sql = @"SELECT a.ticket_code, a.[status], i.created_max, h.max_created_at,CONCAT(DATEDIFF(hour, MAX(i.created_max),MAX(h.max_created_at)), ' hours') AS DateAged,
                                a.subject, a.description,
                                b.description_section, c.description_category, d.description_natureofprob, 
                                e.[description] AS priority_desc, CONCAT(g.employee_first_name, ' ', g.employee_last_name) AS ticket_owner, 
                                a.created_at, a.is_with_third_party, a.third_party_name, 
                                a.third_party_date_given, a.third_party_date_received, a.proposed_remarks
                                FROM t_TicketHeader AS a

                                INNER JOIN m_Section AS b ON b.section_id = a.section_id
                                INNER JOIN m_Category AS c ON c.category_id = a.category_id
                                INNER JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                                INNER JOIN m_Priority AS e ON e.priority_id = a.priority_id
                                INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_by
                                INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS g ON g.employee_code = a.created_for
                                LEFT JOIN (
                                SELECT ticket_code, MAX(created_at) AS max_created_at 
                                FROM t_TicketStages 
                                WHERE [status] = 'ASSIGNMENT CONFIRMATION'
                                GROUP BY ticket_code
                                ) AS h ON h.ticket_code = a.ticket_code  
                                LEFT JOIN (
                                SELECT ticket_code, MAX(created_at) AS created_max 
                                FROM t_TicketStages 
                                WHERE [status] = 'FOR ASSIGNING'
                                GROUP BY ticket_code
                                ) AS i ON i.ticket_code = a.ticket_code

                                GROUP BY a.ticket_code, a.approval_transactional_level, a.[status], 
                                a.subject, a.description,
                                b.description_section, c.description_category, d.description_natureofprob, 
                                e.[description],
                                CONCAT(f.employee_first_name, ' ', f.employee_last_name),
                                CONCAT(g.employee_first_name, ' ', g.employee_last_name),
								h.max_created_at, i.created_max,
                                a.created_at, a.is_with_third_party, a.third_party_name, 
                                a.third_party_date_given, a.third_party_date_received, a.proposed_remarks
								ORDER BY a.ticket_code ASC;";

                _dt = clsQueries.fetchData(sql);

                ws.Cells["A5"].LoadFromDataTable(_dt, false);


                ws.Column(3).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column C (for assigning date)
                ws.Column(4).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column D (for confirmation date)
                ws.Column(6).Style.WrapText = false;
                ws.Column(7).Style.WrapText = false;
                ws.Column(13).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(16).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(17).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)
                ws.Column(18).Style.WrapText = false;

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;  filename=TIMETAKENTOBEASSIGNED" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx");
                response.BinaryWrite(pck.GetAsByteArray());
                response.Flush();
                response.End();
            }
        }

        protected void lnkTixUnresolved_Click(object sender, EventArgs e)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HelpDeskVG.ReportFiles.TICKETSNOTRESOLVED.xlsx");
            ExcelPackage pck = new ExcelPackage(stream);

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (pck)
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets["UnresolvedTicket"];
                ws.Cells["C1"].Value = DateTime.Now.ToString();

                DataTable _dt = new DataTable();

                string sql = @"SELECT a.ticket_code, a.[status], a.subject, a.description, b.description_section, c.description_category, d.description_natureofprob, 
                              e.[description] AS priority_desc, CONCAT(g.employee_first_name, ' ', g.employee_last_name) AS ticket_owner, 
                              a.created_at, a.is_with_third_party, a.third_party_name, 
                              a.third_party_date_given, a.third_party_date_received, a.proposed_remarks FROM t_TicketHeader AS a

                              INNER JOIN m_Section AS b ON b.section_id = a.section_id
                              INNER JOIN m_Category AS c ON c.category_id = a.category_id
                              INNER JOIN m_NatureOfProblem AS d ON d.nature_of_prob_id = a.nature_of_problem_id
                              INNER JOIN m_Priority AS e ON e.priority_id = a.priority_id
                              INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS f ON f.employee_code = a.created_by
                              INNER JOIN dbVG_EmployeeMaster.dbo.m_employee AS g ON g.employee_code = a.created_for
                              WHERE a.approval_transactional_level = '7' AND a.[status] = 'NOT RESOLVED' ORDER BY a.ticket_code ASC";

                _dt = clsQueries.fetchData(sql);

                ws.Cells["A5"].LoadFromDataTable(_dt, false);
                ws.Column(3).Style.WrapText = false;
                ws.Column(4).Style.WrapText = false;
                ws.Column(10).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(13).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(14).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)
                ws.Column(15).Style.WrapText = false;

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;  filename=TICKETSNOTRESOLVED" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx");
                response.BinaryWrite(pck.GetAsByteArray());
                response.Flush();
                response.End();
            }
        }

        protected void lnkTixRawReport_Click(object sender, EventArgs e)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HelpDeskVG.ReportFiles.RAWREPORT.xlsx");
            ExcelPackage pck = new ExcelPackage(stream);

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (pck)
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets["RawReport"];
                ws.Cells["C1"].Value = DateTime.Now.ToString();

                DataTable _dt = new DataTable();

                string sql = @"SELECT a.ticket_code, a.[status], a.created_at,
							CONCAT (b.employee_first_name, ' ', b.employee_last_name) as transacted_by,
							CONCAT (c.employee_first_name, ' ', c.employee_last_name) as assigned_pic,
							CONCAT (d.employee_first_name, ' ', d.employee_last_name) as admin_assignor,
							e.subject, e.description,
							f.description_section, g.description_category, h.description_natureofprob, i.description AS priority_description,
							e.is_with_third_party, e.third_party_date_given, e.third_party_date_received,
							a.itpic_recent_solution_remarks, a.itpic_previous_solution_remarks, a.itpic_recent_reject_ticket_remarks, a.itpic_previous_reject_ticket_remarks,
							a.user_reject_solution_remarks, a.user_reject_previous_solution_remarks, a.admin_recent_reject_ticket_remarks, a.admin_previous_reject_ticket_remarks


							FROM t_TicketStages AS a

							LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS b ON b.employee_code = a.transacted_by
							LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS c ON c.employee_code = a.assigned_pic_employee_no
							LEFT JOIN dbVG_EmployeeMaster.dbo.m_employee AS d ON d.employee_code = a.administrator_assignor_emp_no 
							LEFT JOIN t_TicketHeader AS e ON e.ticket_code = a.ticket_code  
							LEFT JOIN m_Section AS f ON f.section_id = e.section_id
							LEFT JOIN m_Category AS g ON g.category_id = e.category_id
							LEFT JOIN m_NatureOfProblem AS h ON h.nature_of_prob_id = e.nature_of_problem_id
							LEFT JOIN m_Priority AS i ON i.priority_id = e.priority_id
							ORDER BY a.ticket_code, a.created_at ASC";

                _dt = clsQueries.fetchData(sql);

                ws.Cells["A5"].LoadFromDataTable(_dt, false);

                ws.Column(3).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(7).Style.WrapText = false;
                ws.Column(8).Style.WrapText = false;

                ws.Column(14).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(15).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)
                ws.Column(16).Style.WrapText = false;
                ws.Column(17).Style.WrapText = false;
                ws.Column(18).Style.WrapText = false;
                ws.Column(19).Style.WrapText = false;
                ws.Column(20).Style.WrapText = false;
                ws.Column(21).Style.WrapText = false;
                ws.Column(22).Style.WrapText = false;
                ws.Column(23).Style.WrapText = false;

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;  filename=RAWREPORT" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx");
                response.BinaryWrite(pck.GetAsByteArray());
                response.Flush();
                response.End();
            }
        }

        protected void CountClosedTicket()
        {
            string sql = " SELECT COUNT(a.ticket_code) FROM t_TicketHeader AS a WHERE a.approval_transactional_level = '8' AND a.[status] = 'CLOSED' ";
            clsQueries.fetchData(sql);
            DataTable dt = new DataTable();

            dt = clsQueries.fetchData(sql);

            int ticketCount = Convert.ToInt32(dt.Rows[0][0]);

            lblNoOfTixClosed.Text = "No. of Tickets Fully Closed: " + ticketCount.ToString();

            dt.Dispose();

        }

        protected void CountFullyResolved()
        {
            string sql = " SELECT COUNT(a.ticket_code) FROM t_TicketHeader AS a WHERE a.approval_transactional_level = '6' AND a.[status] = 'RESOLVED' ";
            clsQueries.fetchData(sql);
            DataTable dt = new DataTable();

            dt = clsQueries.fetchData(sql);

            int ticketCount = Convert.ToInt32(dt.Rows[0][0]);

            lblNoOfTicketsResolved.Text = "No. of Tickets Resolved: " + ticketCount.ToString();

            dt.Dispose();
        }

        protected void CountAutoClose()
        {
            string sql = " SELECT COUNT(a.ticket_code) FROM t_TicketHeader AS a WHERE a.approval_transactional_level = '9' AND a.[status] = 'AUTO CLOSED' ";
            clsQueries.fetchData(sql);
            DataTable dt = new DataTable();

            dt = clsQueries.fetchData(sql);

            int ticketCount = Convert.ToInt32(dt.Rows[0][0]);

            lblNoOfTicketsAutoClosed.Text = "No. of Tickets Auto Closed: " + ticketCount.ToString();

            dt.Dispose();
        }
       

        protected void CountTicketsUnresolved()
        {
            string sql = " SELECT COUNT(a.ticket_code) FROM t_TicketHeader AS a WHERE a.approval_transactional_level = '7' AND a.[status] = 'NOT RESOLVED' ";
            clsQueries.fetchData(sql);
            DataTable dt = new DataTable();

            dt = clsQueries.fetchData(sql);

            int ticketCount = Convert.ToInt32(dt.Rows[0][0]);

            lblTixUnresolved.Text = "No. of Tickets Unresolved: " + ticketCount.ToString();
            dt.Dispose();

        }
    }
}