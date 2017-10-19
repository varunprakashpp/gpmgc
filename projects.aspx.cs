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
        lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Research Projects</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li class='active'>Research Projects</li></ul></div></div>";


        display();
    }

    public void display()
    {
        querry = " SELECT  id,heading,project_team,description";
        querry += " ,photo1,photo2,photo3 ";
        querry += " FROM tbl_projects WHERE flag='research'  AND status='1'";
        querry += " ORDER BY CAST(display_order AS INT) ASC";
        DataSet ds = cc.joinselect(querry);
        GridView1.DataSource = ds;
        GridView1.DataBind();
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
            Label lblimage = (Label)e.Row.FindControl("lblimage");
            Label lbldata = (Label)e.Row.FindControl("lbldata");
            HiddenField hfhead = (HiddenField)e.Row.FindControl("hfhead");
            HiddenField hfid = (HiddenField)e.Row.FindControl("hfid");
            HiddenField hfdes = (HiddenField)e.Row.FindControl("hfdes");
            HiddenField hfpt = (HiddenField)e.Row.FindControl("hfpt");
            HiddenField hfimg1 = (HiddenField)e.Row.FindControl("hfimg1");
            HiddenField hfimg2 = (HiddenField)e.Row.FindControl("hfimg2");
            HiddenField hfimg3 = (HiddenField)e.Row.FindControl("hfimg3");

            string head = hfhead.Value, cont = hfdes.Value, team = EncodeDecode.base64Decode(hfpt.Value);
            string photo = "img/sections/no_img.png", indicator = "", image = "", control = "";
            string path = "projects_more.aspx?id=" + EncodeDecode.base64Encode(hfid.Value) ;
            int i = 0;

            cont = EncodeDecode.base64Decode(cont);
            if (cont.Length > 1000)
                cont = cont.Substring(0, 1000) + "....";

            indicator = "<ol class='carousel-indicators'>";
            image = "<div id='carousel-example-generic" + hfid.Value + "' class='carousel slide' data-ride='carousel'><div class='carousel-inner' role='listbox'>";

            if (hfimg1.Value != "")
            {
                string path1 = "uploads/research/" + hfid.Value + "/" + hfimg1.Value;
                if (File.Exists(Server.MapPath(path1)))
                {
                    indicator += "<li data-target='#carousel-example-generic" + hfid.Value + "' data-slide-to='" + i + "' class='active'></li>";
                    image += " <div class='item active'> <img src='" + path1 + "' width='800' height='570' alt='' title=''></div>";
                    i++;
                }
            }

            if (hfimg2.Value != "")
            {
                string path1 = "uploads/research/" + hfid.Value + "/" + hfimg2.Value;
                if (File.Exists(Server.MapPath(path1)))
                {
                    string stat = "";
                    if (i == 0)
                        stat = "active";
                    indicator += "<li data-target='#carousel-example-generic" + hfid.Value + "' data-slide-to='" + i + "' class='" + stat + "'></li>";
                    image += " <div class='item " + stat + "'> <img src='" + path1 + "' width='800' height='570' alt='' title=''></div>";
                    i++;
                }
            }

            if (hfimg3.Value != "")
            {
                string path1 = "uploads/research/" + hfid.Value + "/" + hfimg3.Value;
                if (File.Exists(Server.MapPath(path1)))
                {
                    string stat = "";
                    if (i == 0)
                        stat = "active";
                    indicator += "<li data-target='#carousel-example-generic" + hfid.Value + "' data-slide-to='" + i + "' class='" + stat + "'></li>";
                    image += " <div class='item " + stat + "'> <img src='" + path1 + "' width='800' height='570' alt='' title=''></div>";
                    i++;
                }
            }


            if (i > 1)
            {
                control = "<a class='left carousel-control' href='#carousel-example-generic" + hfid.Value + "' role='button' data-slide='prev'><span class='fa fa-angle-left fa-2x' aria-hidden='true'></span><span class='sr-only'>Previous</span></a>";
                control += "<a class='right carousel-control' href='#carousel-example-generic" + hfid.Value + "' role='button' data-slide='next'><span class='fa fa-angle-right fa-2x' aria-hidden='true'></span><span class='sr-only'>Next</span></a>";
            }


            if (i == 0)
            {
                image += " <div class='item active'> <img src='img/sections/about/img1.jpg' width='800' height='570' alt='' title=''></div>";
                indicator += "<li data-target='#carousel-example-generic' data-slide-to='0' class='active'></li>";

            }


            indicator += "</ol>";
            image += "</div></div>";


            lblimage.Text = indicator + image + control;

            lbldata.Text = "";
            lbldata.Text += "<a href='" + path + "' title='View more'><h4>" + hfhead.Value + "</h4> ";
            lbldata.Text += " <p style='color:#09C'> <i class='fa fa-user'></i>&nbsp;&nbsp;&nbsp;" + team + " </p> ";
            lbldata.Text += " <p>" + cont + "</p></a>";


        }
    }

}