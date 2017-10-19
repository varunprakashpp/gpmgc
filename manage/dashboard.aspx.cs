using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

public partial class manage_dashboard : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, condition, id, e_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Label lblheading = (Label)Master.FindControl("lblheading");
            lblheading.Text = " Dashboard";

            id = Request.Cookies["id"].Value;
            
            if (!IsPostBack)
            {
                
            }


        }
        catch (Exception t)
        {
            Response.Write(t);
        }
    }


}