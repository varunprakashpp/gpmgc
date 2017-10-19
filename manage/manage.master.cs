using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

public partial class manage : System.Web.UI.MasterPage
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, id, type;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["secluded"] != null)
            {
                if (Request.Cookies["secluded"].Value != "%G71@P&Gc185Mf1702AdMiN")
                {
                    Response.Redirect("../adminlogin.aspx");
                }
                else
                {
                }
            }
            else
            {
                Response.Redirect("../adminlogin.aspx");
            }
        }

        lbl_logtype.Text = "<b>" + Request.Cookies["logtype"].Value.ToUpper() + "</b>";

    }


    protected void lnkb_logout1_Click(object sender, EventArgs e)
    {
        HttpCookie ck1 = new HttpCookie("username", "");
        Response.Cookies.Add(ck1);

        HttpCookie ck2 = new HttpCookie("secluded", "");
        Response.Cookies.Add(ck2);

        HttpCookie ck3 = new HttpCookie("id", "");
        Response.Cookies.Add(ck3);

        HttpCookie ck4 = new HttpCookie("logtype", "");
        Response.Cookies.Add(ck4);

        Response.Redirect("../adminlogin.aspx");
    }
}
