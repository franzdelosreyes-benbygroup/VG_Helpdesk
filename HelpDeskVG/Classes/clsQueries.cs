using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HelpDeskVG.Classes
{
    public class clsQueries
    {

        public static void executeQuery(string query)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static void executeQuery(string query, SqlParameter[] sqlParam)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static void executeSP(string query, SqlParameter[] sqlParam)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static DataTable fetchData(string query)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        public static DataTable fetchDataSP(string spname, SqlParameter[] sqlParam)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con_VG_Helpdesk"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = spname;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        public static void DisplayEmployee(DropDownList ddlEmployee)
        {
            string sql = "";
            sql = @"EXEC sp_vw_EmployeeMaster";

            DataTable dt = new DataTable();
            dt = fetchData(sql);

            ddlEmployee.DataSource = dt;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "employee_code";
            ddlEmployee.DataBind();
            dt.Dispose();
            ddlEmployee.Items.Insert(0, new ListItem("Please Select", ""));
        }

        public static void DisplayPriority(DropDownList ddlPriorityLevel)
        {
            string sql = "";
            sql = @"EXEC sp_vw_Priority";

            DataTable dt = new DataTable();
            dt = fetchData(sql);

            ddlPriorityLevel.DataSource = dt;
            ddlPriorityLevel.DataTextField = "description";
            ddlPriorityLevel.DataValueField = "priority_id";
            ddlPriorityLevel.DataBind();
            dt.Dispose();

            ddlPriorityLevel.Items.Insert(0, new ListItem("Please Select", ""));
        }

        public static void DisplaySection(DropDownList ddlSection)
        {
            string sql = "";
            sql = @"EXEC sp_vw_Section";


            DataTable dt = new DataTable();
            dt = fetchData(sql);

            ddlSection.DataSource = dt;
            ddlSection.DataTextField = "description_section";
            ddlSection.DataValueField = "section_id";
            ddlSection.DataBind();

            dt.Dispose();

            ddlSection.Items.Insert(0, new ListItem("Please Select", ""));

        }

   
        public static void DisplayCategory(DropDownList ddlCategory)
        {
            string sql = "";
            sql = @"EXEC sp_vw_Category";

            DataTable dt = new DataTable();
            dt = fetchData(sql);

            ddlCategory.DataSource = dt;  
            ddlCategory.DataTextField = "description_category";
            ddlCategory.DataValueField= "category_id";

            ddlCategory.DataBind();

            dt.Dispose();

            ddlCategory.Items.Insert(0, new ListItem("Please Select", ""));

        }

        public static void DisplayNatureOfProblem(DropDownList ddlNatureOfProblem)
        {
            string sql = "";
            sql = @"EXEC sp_vw_NatureOfProblem";

            DataTable dt = new DataTable();
            dt = fetchData(sql);

            ddlNatureOfProblem.DataSource = dt;
            ddlNatureOfProblem.DataTextField = "description_natureofprob";
            ddlNatureOfProblem.DataValueField = "nature_of_prob_id";
            ddlNatureOfProblem.DataBind();

            dt.Dispose();

            ddlNatureOfProblem.Items.Insert(0, new ListItem("Please Select", ""));

        }


        public static void DisplayITPICEmployee(DropDownList ddlEmployeeITPIC)
        {
            string sql = "";
            sql = @"EXEC sp_vw_EmployeeITPIC";


            DataTable dt = new DataTable();
            dt = fetchData(sql);

            ddlEmployeeITPIC.DataSource = dt;
            ddlEmployeeITPIC.DataTextField = "EmployeeName";
            ddlEmployeeITPIC.DataValueField = "employee_code";
            ddlEmployeeITPIC.DataBind();

            dt.Dispose();

            ddlEmployeeITPIC.Items.Insert(0, new ListItem("Please Select", ""));

        }
    }
}