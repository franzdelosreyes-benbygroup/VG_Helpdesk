using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

                string sql = @"SELECT  a.ticket_code, a.[status], b.description_section, c.description_category, d.description_natureofprob, 
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

                ws.Column(8).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(11).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(12).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)

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

                string sql = @"SELECT a.ticket_code, a.[status], b.description_section, c.description_category, d.description_natureofprob, 
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

                ws.Column(8).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(11).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(12).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)

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

                string sql = @"SELECT  a.ticket_code, a.[status], b.description_section, c.description_category, d.description_natureofprob, 
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

                ws.Column(8).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(11).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(12).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)

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

                string sql = @"SELECT a.ticket_code, a.[status], CONCAT(DATEDIFF(hour, MAX(i.created_max),MAX(h.max_created_at)), ' hours') AS DateAged,
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


                                WHERE a.approval_transactional_level = '3' 

                                GROUP BY a.ticket_code, a.approval_transactional_level, a.[status], 
                                b.description_section, c.description_category, d.description_natureofprob, 
                                e.[description],
                                CONCAT(f.employee_first_name, ' ', f.employee_last_name),
                                CONCAT(g.employee_first_name, ' ', g.employee_last_name),
                                a.created_at, a.is_with_third_party, a.third_party_name, 
                                a.third_party_date_given, a.third_party_date_received, a.proposed_remarks
								ORDER BY a.ticket_code ASC;";

                _dt = clsQueries.fetchData(sql);

                ws.Cells["A5"].LoadFromDataTable(_dt, false);

                ws.Column(8).Style.Numberformat.Format = "MM/dd/yyyy hh:mm AM/PM"; // Column H (created_at)
                ws.Column(11).Style.Numberformat.Format = "MM/dd/yyyy"; // Column J (third_party_date_given)
                ws.Column(12).Style.Numberformat.Format = "MM/dd/yyyy"; // Column K (third_party_date_received)

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

        }

        protected void lnkTixRawReport_Click(object sender, EventArgs e)
        {

        }

        private void GenerateChangeScheduleWithValues(string PC)
        {
            //Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MerchPortal2NewDesign.Template..xlsx");
            //ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);

            //var filename = "ChangeSchedule_" + PC + "_" + DateTime.Now.ToString("yyyyMMdd HHmmss");
            //ExcelWorksheet xlsht = pck.Workbook.Worksheets[1];

            //xlsht.Protection.AllowSelectLockedCells = false;
            ////xlsht.Protection.SetPassword("Benby123");
            //xlsht.Cells["A:XFD"].Style.Locked = true;

            ////HEADER1
            //xlsht.Cells["A1:C1"].Style.Locked = true;
            //xlsht.Cells["E1"].Style.Locked = true;
            //xlsht.Cells["F1"].Style.Locked = true;
            ////HEADER2
            //xlsht.Cells["A3:R3"].Style.Locked = true;
            //xlsht.Cells["R1:R500"].Style.Locked = false;

            ////RESTDAY COLUMN
            //xlsht.Cells["D1"].Style.Locked = false;
            ////MONDAY COLUMN
            //xlsht.Cells["F4:F500"].Style.Locked = false;
            ////TUESDAY COLUMN
            //xlsht.Cells["G4:G500"].Style.Locked = false;
            ////WEDNESDAY COLUMN
            //xlsht.Cells["H4:H500"].Style.Locked = false;
            ////THURSDAY COLUMN
            //xlsht.Cells["H4:H500"].Style.Locked = false;
            ////THURSDAY COLUMN
            //xlsht.Cells["I4:I500"].Style.Locked = false;
            ////FRIDAY COLUMN
            //xlsht.Cells["J4:J500"].Style.Locked = false;
            ////SATURDAY COLUMN
            //xlsht.Cells["K4:K500"].Style.Locked = false;
            ////SUNDAY COLUMN
            //xlsht.Cells["L4:L500"].Style.Locked = false;
            ////TIME IN COLUMN
            //xlsht.Cells["M4:M500"].Style.Locked = false;
            ////TIME OUT COLUMN
            //xlsht.Cells["N4:N500"].Style.Locked = false;


            //xlsht.Cells[1, 2].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


            //string sql = "EXEC SP_Load_PlantillaDetails8 @PlantillaCode='" + PC + "'";
            //DataTable dt = new DataTable();
            //dt = Actions.GetData(sql);
            //for (int x = 0; x < dt.Rows.Count; x++)
            //{
            //    int x1 = x + 4;
            //    xlsht.Cells[x1, 1].Value = dt.Rows[x]["PlantillaCode"].ToString();
            //    xlsht.Cells[x1, 2].Value = dt.Rows[x]["Storecode"].ToString();
            //    xlsht.Cells[x1, 3].Value = dt.Rows[x]["PlannedConversion"].ToString();
            //    xlsht.Cells[x1, 4].Value = dt.Rows[x]["PlannedDays"].ToString();
            //    xlsht.Cells[x1, 5].Value = dt.Rows[x]["PlannedHours"].ToString();
            //    xlsht.Cells[x1, 19].Value = dt.Rows[x]["Id"].ToString();
            //}

            //Response.ContentType = "application/vnd.ms-Excel";
            //Response.AddHeader("content-disposition", "attachment;  filename=" + filename + ".xlsx");
            //Response.BinaryWrite(pck.GetAsByteArray());
            //Response.Flush();
            //Response.End();
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

            lblNoOfTicketsResolved.Text = "No. of Tickets Fully Resolved: " + ticketCount.ToString();

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