using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

public partial class adminlogin : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session.Abandon();
                HttpCookie cookies = Context.Request.Cookies[FormsAuthentication.FormsCookieName];//Or Response
                cookies.Expires = DateTime.Now.AddDays(-1);
                Context.Response.Cookies.Add(cookies);
            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string query = "select id,username,password from tbl_login where username='" + safesql.SafeSqlLiterall(txt_name.Text, 2) + "' ";
        DataSet ds = cc.joinselect(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == safesql.SafeSqlLiterall(txt_pass.Text, 2))
            {
                FormsAuthentication.RedirectFromLoginPage(txt_name.Text, true);
                HttpCookie ck1 = new HttpCookie("username", txt_name.Text);
                Response.Cookies.Add(ck1);
                HttpCookie ck2 = new HttpCookie("id", ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                Response.Cookies.Add(ck2);

                HttpCookie ck3 = new HttpCookie("secluded", "%G71@P&Gc185Mf1702AdMiN");
                Response.Cookies.Add(ck3);

                HttpCookie ck4 = new HttpCookie("logtype", "admin");
                Response.Cookies.Add(ck4);

                Response.Redirect("manage/dashboard.aspx");
            }

            else
            {
                string msg = "Password is wrong!!!!";
                lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
            }

        }

        else
        {
            string msg = "Username Doesn't Exist!!!!";
            lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
        }

        ds.Dispose();
    }
  
}