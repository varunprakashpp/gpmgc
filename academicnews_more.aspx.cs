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
    static string querry, newstype,title,nid,pid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null && Request.QueryString["id"] != null && Request.QueryString["nid"] != null)
        {
            nid = EncodeDecode.base64Decode(Request.QueryString["nid"]);
            newstype = EncodeDecode.base64Decode(Request.QueryString["type"]);
            pid = EncodeDecode.base64Decode(Request.QueryString["id"]);

            Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
            lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Academic News More</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li >Academic</li><li >" + EncodeDecode.base64Decode(Request.QueryString["type"]) + "</li><li class='active'>Academic News More</li></ul></div></div>";


            display();
            related();
        }
        else
            Response.Redirect("Default.aspx");
    }

    public void display()
    {
        querry = " SELECT  id,heading,addedon,description";
        querry += " ,photo1, photo2, photo3";
        querry += " FROM tbl_news WHERE id=" + pid + " AND status='1'";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            
            string head = ds.Tables[0].Rows[0].ItemArray[1].ToString(), cont = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            string photo = "img/gallery/gl_02.jpg", adate = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[2]).ToString("MMM dd yyyy");
            string image = "", control = "", indicator = "";
            int i = 0;

            cont = EncodeDecode.base64Decode(cont);

            if (ds.Tables[0].Rows[0].ItemArray[4].ToString() != "")
            {
                string path1 = "uploads/academiccells/" + nid + "/" + pid + "/" + ds.Tables[0].Rows[0].ItemArray[4].ToString();
                if (File.Exists(Server.MapPath(path1)))
                {
                    string stat = "";
                    if (i == 0)
                        stat = "active";
                    image += " <div class='item  " + stat + "'><img src='" + path1 + "' width='885' height='512' alt='' title=''></div>";
                    i++;
                }
            }

            if (ds.Tables[0].Rows[0].ItemArray[5].ToString() != "")
            {
                string path1 = "uploads/academiccells/" + nid + "/" + pid + "/" + ds.Tables[0].Rows[0].ItemArray[5].ToString();
                if (File.Exists(Server.MapPath(path1)))
                {
                    string stat = "";
                    if (i == 0)
                        stat = "active";
                    image += " <div class='item " + stat + "'><img src='" + path1 + "' width='885' height='512' alt='' title=''></div>";
                    i++;
                }
            }
            if (ds.Tables[0].Rows[0].ItemArray[6].ToString() != "")
            {
                string path1 = "uploads/academiccells/" + nid + "/" + pid + "/" + ds.Tables[0].Rows[0].ItemArray[6].ToString();
                if (File.Exists(Server.MapPath(path1)))
                {
                    image += " <div class='item  active'><img src='" + path1 + "' width='885' height='512' alt='' title=''></div>";
                    i++;
                }
            }

            if (i == 0)
            {
                image = "<div class='item  active'><img src='img/gallery/gl_02.jpg' width='885' height='512' alt='' title=''></div> ";
            }

            for (int k = 0; k < i; k++)
                indicator += "<li data-target='#carousel-example-generic' data-slide-to='" + k + "' class=''></li>";

            if (i > 1)
            {
                control = "<a class='left carousel-control' href='#carousel-example-generic' role='button' data-slide='prev'><span class='fa fa-angle-left fa-2x' aria-hidden='true'></span><span class='sr-only'>Previous</span></a>  ";
                control += "<a class='right carousel-control' href='#carousel-example-generic' role='button' data-slide='next'><span class='fa fa-angle-right fa-2x' aria-hidden='true'></span><span class='sr-only'>Next</span></a> ";
            }

            lblcontent.Text = "";
            lblcontent.Text += "<h2 class='post-title'><a href='' class='black'>" + head + "</a></h2> ";
            lblcontent.Text += "<p style='margin-bottom:25px;' class='date'>" + adate + "</p> ";
            lblcontent.Text += " <div class='post-image'> ";
            lblcontent.Text += " <div id='carousel-example-generic' class='carousel slide' data-ride='carousel'> ";
            lblcontent.Text += " <ol class='carousel-indicators'> ";
            lblcontent.Text += indicator;
            lblcontent.Text += " </ol> ";
            lblcontent.Text += " <div class='carousel-inner' role='listbox'> ";
            lblcontent.Text += image;
            lblcontent.Text += " </div> ";
            lblcontent.Text += control;
            lblcontent.Text += " </div> ";
            lblcontent.Text += " </div> ";
            lblcontent.Text += "<div class='clearfix'></div> ";
            lblcontent.Text += "<p class='news_desc text-justify'>" + cont + "</p> ";

        }
    }

    public void related()
    {
        querry = " SELECT TOP 3 id,heading,addedon,description";
        querry += " FROM tbl_news WHERE flag='" + nid + "' AND tag='academiccells' AND id<>" + pid;
        querry += " ORDER BY CAST(addedon AS date) DESC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string head = ds.Tables[0].Rows[i].ItemArray[1].ToString(), cont = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                string adate = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]).ToString("MMM dd yyyy");
                string path = "academicnews_more.aspx?id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&type=" + Request.QueryString["type"] + "&nid=" + Request.QueryString["nid"];

                cont = EncodeDecode.base64Decode(cont);
                if (cont.Length > 120)
                    cont = cont.Substring(0, 120) + "...";
                
                lblrelate.Text += " <div class='rel_newsbl'> ";
                lblrelate.Text += " <a href='"+ path +"'><h4 class='rel_hd'>" + head + "</h4> <p class='date'>" + adate + "</p> <p>" + cont + "</p></a> ";
                lblrelate.Text += " </div> ";

            }
        }
        ds.Dispose();
    }

}