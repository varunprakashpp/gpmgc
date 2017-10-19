﻿using System;
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
    static string querry, newstype, title, nid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null && Request.QueryString["id"] != null)
        {
            nid = EncodeDecode.base64Decode(Request.QueryString["id"]);
            newstype = EncodeDecode.base64Decode(Request.QueryString["type"]);

            Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
            lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>" + newstype + "</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li>Academic</li><li class='active'>" + newstype + "</li></ul></div></div>";


            if (!IsPostBack)
            {
                display();
            }
        }
        else
            Response.Redirect("Default.aspx");
    }

    public void display()
    {
        querry = " SELECT  id,heading,addedon,description";
        querry += " ,photo1,photo2,photo3 ";
        querry += " FROM tbl_news WHERE flag='" + nid + "' AND tag='academiccells' AND status='1' ";
        querry += " ORDER BY CAST(addedon AS date) DESC";
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
            Label lbldata = (Label)e.Row.FindControl("lbldata");
            HiddenField hfhead = (HiddenField)e.Row.FindControl("hfhead");
            HiddenField hfid = (HiddenField)e.Row.FindControl("hfid");
            HiddenField hfdes = (HiddenField)e.Row.FindControl("hfdes");
            HiddenField hfimg1 = (HiddenField)e.Row.FindControl("hfimg1");
            HiddenField hfimg2 = (HiddenField)e.Row.FindControl("hfimg2");
            HiddenField hfimg3 = (HiddenField)e.Row.FindControl("hfimg3");

            string head = hfhead.Value, cont = hfdes.Value;
            string photo = "img/gallery/gl_02.jpg", indicator = "", image = "", control = "";
            int i = 0;

            cont = EncodeDecode.base64Decode(cont);

            indicator = "<ol class='carousel-indicators'>";
            image = "<div id='carousel-example-generic" + hfid.Value + "' class='carousel slide' data-ride='carousel'><div class='carousel-inner' role='listbox'>";

            if (hfimg1.Value != "")
            {
                string path1 = "uploads/academiccells/" + hfid.Value + "/" + hfimg1.Value;
                if (File.Exists(Server.MapPath(path1)))
                {
                    indicator += "<li data-target='#carousel-example-generic" + hfid.Value + "' data-slide-to='" + i + "' class='active'></li>";
                    image += " <div class='item active'> <img src='" + path1 + "' width='800' height='570' alt='' title=''></div>";
                    i++;
                }
            }

            if (hfimg2.Value != "")
            {
                string path1 = "uploads/academiccells/" + hfid.Value + "/" + hfimg2.Value;
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
                string path1 = "uploads/academiccells/" + hfid.Value + "/" + hfimg3.Value;
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

            lbldata.Text = "";
            lbldata.Text += "<h2 class='post-title'><a href='' class='black'>" + hfhead.Value + "</a></h2> ";
            lbldata.Text += " <div style='width:100%;height:351px;overflow: hidden;'><div class='post-image'> ";
            lbldata.Text += " <div id='carousel-example-generic' class='carousel slide' data-ride='carousel'> ";
            lbldata.Text += indicator+image+control;
            lbldata.Text += " </div> ";
            lbldata.Text += " </div></div> ";
            lbldata.Text += "<div class='clearfix'></div> ";
            lbldata.Text += "<p class='news_desc text-justify'>" + cont + "</p> ";

        }
    }

}