using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class news : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, newstype, title;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
        lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Downloads</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li class='active'>Downloads</li></ul></div></div>";


        if (!IsPostBack)
        {
            display();
        }
    }

    public void display()
    {
        querry = " SELECT  id,heading,uploads";
        querry += " FROM tbl_downloads WHERE flag='downloads' AND status='1' ";
        querry += " ORDER BY CAST(display_order AS int) ASC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
            Response.Write("<script>alert('No download files found !!'); window.location.assign('default.aspx');</script>");
        ds.Dispose();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        display();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbldata = (Label)e.Row.FindControl("lbldata");
            HiddenField hfup = (HiddenField)e.Row.FindControl("hfup");
            HiddenField hfid = (HiddenField)e.Row.FindControl("hfid");
            string rpt = hfup.Value;
            if (rpt != "")
            {
                string path = "uploads/downloads/" + hfid.Value + "/" + rpt;
                if (File.Exists(Server.MapPath(path)))
                {
                    lbldata.Text = " <div class='col-sm-9'><h3>" + lbldata.Text + "</h3></div> ";
                    lbldata.Text += " <div class='col-sm-3'><a href='" + path + "' class='downlaod_btn' title='Download'><img src='img/btn_dwnd.png'></a></div> ";
                }
                else
                    e.Row.Visible = false;
            }
            else
                e.Row.Visible = false;
        }
    }

}