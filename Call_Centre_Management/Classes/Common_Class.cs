using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Call_Centre_Management.Classes
{
    public class Common_Class
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter adp;
        public int return_nonquery(Dictionary<string, object> dict, string proc_name)
        {
            int i = 0;
            try
            {
                con.Open();
                cmd = new SqlCommand(proc_name, con);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> kvp in dict)
                {
                    cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return i;
        }
        public DataSet return_dataset(Dictionary<string, object> dict, string proc)
        {
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                cmd = new SqlCommand(proc, con);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> kvp in dict)
                {
                    cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }

                // cmd.ExecuteReader();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                adp.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {

                cmd.Dispose();
                con.Close();
            }
            return ds;
        }
        public DataTable return_datatable(Dictionary<string, object> dict, string proc)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                cmd = new SqlCommand(proc, con);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> kvp in dict)
                {
                    cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                adp.Dispose();
            }
            catch (Exception ex)
            { }
            finally
            {

                cmd.Dispose();
                con.Close();
            }
            return dt;
        }
        public DataSet return_dataset_query(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.ExecuteReader();
                adp.Fill(ds);
                adp.Dispose();
            }
            catch (Exception ex)
            { }
            finally
            {

                cmd.Dispose();
                con.Close();
            }
            return ds;
        }

        public string encrypt_string(string str)
        {
            byte[] b;
            try
            {
                b = Convert.FromBase64String(str);
                str = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (Exception ex)
            {
                str = "";
            }
            return str;
        }
        public string decrypt_string(string str)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            string encrypted = Convert.ToBase64String(b);

            return str;
        }

        public static DataSet RunProc(string procName, SqlParameter sqlParam)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
            try
            {
                string logMessage = Environment.NewLine;
                //if (proceduresToLog.Contains(procName))
                //{
                if (HttpContext.Current != null)
                {
                    if (Convert.ToString(HttpContext.Current.Session["userName"]) != null)
                    {
                        logMessage += "UserId: " + Convert.ToString(HttpContext.Current.Session["userId"]) + Environment.NewLine;
                        logMessage += "UserName: " + Convert.ToString(HttpContext.Current.Session["userName"]) + Environment.NewLine;
                    }
                }
                logMessage += "ProcName: " + procName + Environment.NewLine;
                logMessage += sqlParam.ParameterName + ": " + sqlParam.Value + Environment.NewLine;

                //  JivoEventSource.Log.WriteInfo(logMessage);
                //}

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(procName, conn);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(sqlParam);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                //conn.Close();
                return ds;
            }
            catch (Exception e)
            {
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    //  new SlackClient().sendMessage(procName, e.Message, SlackColor.danger);
                    //new TeamsClient().sendMessage(procName, e.Message, TeamsColor.danger);
                }
                //JivoEventSource.Log.WriteError(e.ToString());
                //  JivoEventSource.Log.WriteError(logMessage);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}