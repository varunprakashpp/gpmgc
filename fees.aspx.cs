using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class about : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
        lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Fees</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li >Admission </li><li class='active'>Fees</li></ul></div></div>";

        querry = " SELECT TOP 1 name FROM tbl_basics WHERE flag='fees' ORDER BY  CAST(display_order AS int) ASC";
        DataSet ds=cc.joinselect(querry);
        if(ds.Tables[0].Rows.Count>0)
        {
            lbldata.Text = EncodeDecode.base64Decode(ds.Tables[0].Rows[0].ItemArray[0].ToString()).Replace("[%]", "%");
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
        ds.Dispose();
    }
}