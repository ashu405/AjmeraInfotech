using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AjmeraPracticalAssessment.Classes.DAL
{
    public class DataAccessLayer
    {
        DataSet DS;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        #region Constructor
        public DataAccessLayer()
        {

        }
        #endregion

        public DataSet GetBookData(string Query, string BookID = null)
        {
            DS = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_BookMaster", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Query", Query);
                        cmd.Parameters.AddWithValue("@BookID", BookID);
                       
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(DS);
                        con.Close();
                        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {
                            return DS;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return DS;
        }
    }
}