using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Country_DAL
/// </summary>
public class Country_DAL
{
    string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
	public Country_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet select(string parameters, string parameters1)
    {

        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("selection", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter dAd = new SqlDataAdapter();

       

        DataSet dSet = new DataSet();

        try
        {
            cmd.Parameters.AddWithValue("@parameters", parameters);
            cmd.Parameters.AddWithValue("@parameters1", parameters1);
            dAd.SelectCommand = cmd;
            dAd.Fill(dSet);

            return dSet;

        }

        catch
        {

            throw;

        }

        finally
        {

            dSet.Dispose();

            dAd.Dispose();

            conn.Close();

            conn.Dispose();

        }

    }
    public int Insert(string parameters)
    {

        SqlConnection con = new SqlConnection(connStr);

        con.Open();

        SqlCommand Cmd = new SqlCommand("insertion", con);

        Cmd.CommandType = CommandType.StoredProcedure;

        try
        {

            Cmd.Parameters.AddWithValue("@parameters", parameters);

            return Cmd.ExecuteNonQuery();

        }

        catch
        {

            throw;

        }

        finally
        {

            Cmd.Dispose();

            con.Close();

            con.Dispose();

        }

    }
    public string Getdistributor_id()
    {
       
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand cmd = new SqlCommand("select MAX(id) from tbl_distributor", conn);
        string a = Convert.ToString(cmd.ExecuteScalar());
        if (a == "")
        {
            return "1";
        }
        else
        {
           // int aa = Convert.ToInt32(a) + 1;
            int aa = Convert.ToInt32(a);
            return "" + aa;
        }
    }

   
    public DataSet joinselect(string parameters)
    {

        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("joinselect", conn);

        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter dAd = new SqlDataAdapter();



        DataSet dSet = new DataSet();

        try
        {
            cmd.Parameters.AddWithValue("@parameters", parameters);
            dAd.SelectCommand = cmd;
            dAd.Fill(dSet);

            return dSet;

        }

        catch
        {

            throw;

        }

        finally
        {

            dSet.Dispose();

            dAd.Dispose();

            conn.Close();

            conn.Dispose();

        }

    }


   
}
