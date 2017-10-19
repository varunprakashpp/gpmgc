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
        lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Gallery</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li class='active'>Gallery</li></ul></div></div>";

        display();
        
    }

    public void display()
    {
        querry = " SELECT id,heading,cover_photo FROM tbl_album WHERE flag='album' ORDER BY  CAST(display_order AS int) ASC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblcont.Text = " ";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string cphoto = "img/sections/no_img_ach.png";
                if (ds.Tables[0].Rows[i].ItemArray[2].ToString() != "")
                {
                    string path = "uploads/album/" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    if (File.Exists(Server.MapPath(path)))
                        cphoto = path;
                }

                lblcont.Text += " <div class='grid-item '> ";
                lblcont.Text += " <img src='"+ cphoto + "' alt='Gallery' class='img-responsive' /> ";
                lblcont.Text += " <div class='img-overlay'></div> ";
                lblcont.Text += " <div class='figcaption'><div class='caption-block'><h4 class='glr_txt'>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</h4></div> ";
                lblcont.Text += " <a href='gallery_more.aspx?id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "'><i class='fa fa-link'></i></a></div> ";
                lblcont.Text += " </div> ";
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
        ds.Dispose();
    }
}