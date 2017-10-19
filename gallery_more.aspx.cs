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
    static string querry,pid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            pid = EncodeDecode.base64Decode(Request.QueryString["id"]);
            Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
            lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Gallery More</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li>Gallery</li><li class='active'>Gallery More</li></ul></div></div>";

            display();
        }
        else
            Response.Redirect("Default.aspx");
        
    }

    public void display()
    {
        querry = " SELECT id,heading,cover_photo, description FROM tbl_album WHERE flag='album' AND id="+pid;
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string cphoto = "img/sections/no_img_ach.png";
            if (ds.Tables[0].Rows[0].ItemArray[2].ToString() != "")
            {
                string path = "uploads/album/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[0].ItemArray[2].ToString();
                if (File.Exists(Server.MapPath(path)))
                    cphoto = path;
            }

            lblmaincon.Text = " ";
            lblmaincon.Text += " <div class='col-lg-7 col-md-7 col-sm-6'><img src='"+cphoto+"'/></div> ";
            lblmaincon.Text += " <div class='col-lg-5 col-md-5 col-sm-6'><h2 class='glrY-txt'>" + ds.Tables[0].Rows[0].ItemArray[1].ToString() + "</h2><p>" + EncodeDecode.base64Decode(ds.Tables[0].Rows[0].ItemArray[3].ToString()) + "</p></div> ";

            display_data();
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
        ds.Dispose();
    }

    public void display_data()
    {
        querry = " SELECT id,heading,photo FROM tbl_album_photos WHERE album_id='" + pid + "' AND status='1' ORDER BY  CAST(display_order AS int) ASC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblcont.Text = " ";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string cphoto = "img/sections/no_img_ach.png";
                if (ds.Tables[0].Rows[i].ItemArray[2].ToString() != "")
                {
                    string path = "uploads/album/" + pid + "/photos/" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    if (File.Exists(Server.MapPath(path)))
                        cphoto = path;
                }

                lblcont.Text += " <div class='grid-item '><div style='width:100%;height:198px;overflow: hidden;'> ";
                lblcont.Text += " <img src='" + cphoto + "' alt='Gallery' class='img-responsive' /> ";
                lblcont.Text += " <div class='img-overlay'></div> ";
                lblcont.Text += " <div class='figcaption'><div class='caption-block'><h4 class='glr_txt'>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</h4></div> ";
                lblcont.Text += " <a href='" + cphoto + "' data-rel='prettyPhoto[portfolio]'><i class='fa fa-search'></i></a> ";
                lblcont.Text += " </div> </div></div> ";
            }
        }
        else
        {
            Response.Write("<script>alert('No photos available in this gallery !!');window.location.assign('gallery.aspx');</script>");
        }
        ds.Dispose();
    }
}