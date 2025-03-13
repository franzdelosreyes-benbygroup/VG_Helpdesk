using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDeskVG
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkTixClosedReport_Click(object sender, EventArgs e)
        {
            string _course_series = (sender as LinkButton).CommandArgument;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LearningSystem.Reports.ReportEmployeeNotEnrolled.xlsx");
            ExcelPackage pck = new ExcelPackage(stream);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (pck)
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets["NotEnrolled"];
                DataTable _dtCourseTitle = new DataTable();

                _dtCourseTitle = _tActions.GetCourseDetailsBySeriesID(_course_series);
                ws.Cells["A1"].Value = _dtCourseTitle.Rows[0]["course_title"].ToString();
                ws.Cells["B1"].Value = DateTime.Now.ToString();

                DataTable _dt = new DataTable();
                _dt = _tActions.GetNotEnrolledInCourse(_course_series);

                ws.Cells["A3"].LoadFromDataTable(_dt, false);

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;  filename=NotEnrolledReports" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx");
                response.BinaryWrite(pck.GetAsByteArray());
                response.Flush();
                response.End();
            }
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
    }
}