using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            }
        }

        protected void lnkTixClosedReport_Click(object sender, EventArgs e)
        {
            //string _course_series = (sender as LinkButton).CommandArgument;
            //Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LearningSystem.Reports.ReportEmployeeNotEnrolled.xlsx");
            //ExcelPackage pck = new ExcelPackage(stream);

            //ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            //using (pck)
            //{
            //    ExcelWorksheet ws = pck.Workbook.Worksheets["NotEnrolled"];
            //    DataTable _dtCourseTitle = new DataTable();

            //    _dtCourseTitle = _tActions.GetCourseDetailsBySeriesID(_course_series);
            //    ws.Cells["A1"].Value = _dtCourseTitle.Rows[0]["course_title"].ToString();
            //    ws.Cells["B1"].Value = DateTime.Now.ToString();

            //    DataTable _dt = new DataTable();
            //    _dt = _tActions.GetNotEnrolledInCourse(_course_series);                                  

            //    ws.Cells["A3"].LoadFromDataTable(_dt, false);

            //    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //    response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    response.AddHeader("content-disposition", "attachment;  filename=NotEnrolledReports" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx");
            //    response.BinaryWrite(pck.GetAsByteArray());
            //    response.Flush();
            //    response.End();
            //}
        }

        protected void lnkTixFullyResolved_Click(object sender, EventArgs e)
        {

        }

        protected void lnkTixAutoClosed_Click(object sender, EventArgs e)
        {

        }

        protected void lnkTixReportTimeToBeAssigned_Click(object sender, EventArgs e)
        {

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
            string sql = " SELECT COUNT(a.ticket_code) FROM t_TicketHeader AS a WHERE a.approval_transactional_level = '6' AND a.[status] = 'RESOLVED'";
            clsQueries.fetchData(sql);
            DataTable dt = new DataTable();

            dt = clsQueries.fetchData(sql);

            int ticketCount = Convert.ToInt32(dt.Rows[0][0]);

            lblNoOfTicketsResolved.Text = "No. of Tickets Fully Resovled: " + ticketCount.ToString();

            dt.Dispose();
        }

    }
}