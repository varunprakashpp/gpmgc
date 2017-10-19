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

public partial class _Default : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry;
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlContainerControl myObject;
        myObject = (HtmlContainerControl)Master.FindControl("mainpagehead");
        myObject.Style.Add("display", "none");

        flashnews();
        latestnews();
        photogallery_achivement();
    }

    public void flashnews()
    {
        querry = " SELECT  TOP 10 id,heading FROM tbl_news WHERE flag='flashnews' ORDER BY id DESC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblflashnews.Text = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lblflashnews.Text += "<span class='icon-refresh2 text-color'></span> <a href='news_more.aspx?id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&type=flashnews' title='View more'>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</a> &nbsp;&nbsp;&nbsp;";
            }
        }
        ds.Dispose();
    }

    public void latestnews()
    {
        querry = " SELECT  TOP 3 id,heading,addedon";
        querry += " ,(CASE WHEN ISNULL(photo1, '') = '' THEN (CASE WHEN ISNULL(photo2, '') = '' THEN (CASE WHEN ISNULL(photo3, '') = '' THEN (CASE WHEN ISNULL(photo4, '') = '' THEN '' ELSE photo4 END) ELSE photo3 END) ELSE photo2 END) ELSE photo1 END) AS photo";
        querry += " FROM tbl_news WHERE flag='news' ORDER BY id DESC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblnews.Text = "<ul class='latest-posts'>";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string img = "img/sections/blog/1.jpg", head = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                if (ds.Tables[0].Rows[i].ItemArray[3].ToString() != "")
                {
                    string path = "uploads/news/" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[i].ItemArray[3].ToString();
                    if (File.Exists(Server.MapPath(path)))
                        img = path;
                }

                if (head.Length > 76)
                    head = head.Substring(0, 76) + "...";

                if (i % 2 == 1 || i == 0)
                    lblnews.Text += "<li data-animation='fadeInLeft'> ";
                else
                    lblnews.Text += "<li data-animation='fadeInUp'> ";
                lblnews.Text += "<a href='news_more.aspx?id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&type=news' title='View more'><div class='post-thumb'> ";
                lblnews.Text += "<img class='img-rounded' src='"+ img +"' alt='' title='' width='84' height='84' /> ";
                lblnews.Text += "</div> ";
                lblnews.Text += "<div class='post-details'><div class='description'> ";
                lblnews.Text += head;
                lblnews.Text += "</div> ";
                lblnews.Text += "<div class='meta'><span class='time'><i class='fa fa-calendar'></i> " + Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]).ToString("dd.MMM.yyyy") + "</span></div> ";
                lblnews.Text += "</div></a> ";
                lblnews.Text += "</li> ";

            }
            lblnews.Text += "</ul>";
        }
        ds.Dispose();
    }

    public void photogallery_achivement()
    {
        querry = " SELECT  TOP 2 id,cover_photo FROM tbl_album WHERE cover_photo<>''   AND status='1' ORDER BY id DESC";

        querry += " SELECT  id, (CASE WHEN ISNULL(photo1, '') = '' THEN (CASE WHEN ISNULL(photo2, '') = '' THEN (CASE WHEN ISNULL(photo3, '') = '' THEN (CASE WHEN ISNULL(photo4, '') = '' THEN '' ELSE photo4 END) ELSE photo3 END) ELSE photo2 END) ELSE photo1 END) AS photo";
        querry += " FROM tbl_news WHERE flag='achievements' AND status='1'  ORDER BY id DESC";

        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblleftslider.Text = "";
            lblleftslider.Text += "<div style='width:100%;height:258px;overflow: hidden;'><div class='carousel-inner' role='listbox'> ";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string img = "img/sections/gallery1.jpg", act = "";
                if (ds.Tables[0].Rows[i].ItemArray[1].ToString() != "")
                {
                    string path = "uploads/album/" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    if (File.Exists(Server.MapPath(path)))
                        img = path;
                }

                if (i == 0)
                    act = "active";
                lblleftslider.Text += "<div class='item " + act + "'><a href='gallery.aspx'  title='View more'><img src='" + img + "' width='400' height='300' alt='' title='' /> </a></div>";
            }
            lblleftslider.Text += "</div></div> ";
        }
        else
        {
            lblleftslider.Text += "<div style='width:100%;height:258px;overflow: hidden;'><div class='item active'><a href='gallery.aspx'  title='View more'><img src='img/sections/gallery1.jpg' width='400' height='300' alt='' title='' /></a></div> ";
            lblleftslider.Text += "<div class='item'><a href='gallery.aspx'  title='View more'><img src='img/sections/gallery2.jpg' width='400' height='300' alt='' title='' /></a></div></div> ";
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            lblrightslider.Text = "";
            lblrightslider.Text += "<div style='width:100%;height:258px;overflow: hidden;'><div class='carousel-inner' role='listbox'> ";

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                string img = "img/sections/gallery1.jpg", act = "";
                if (ds.Tables[1].Rows[i].ItemArray[1].ToString() != "")
                {
                    string path = "uploads/achievements/" + ds.Tables[1].Rows[i].ItemArray[0].ToString() + "/" + ds.Tables[1].Rows[i].ItemArray[1].ToString();
                    if (File.Exists(Server.MapPath(path)))
                        img = path;
                }

                if (i == 0)
                    act = "active";
                lblrightslider.Text += "<div class='item " + act + "'><a href='achievements.aspx'  title='View more'> <img src='" + img + "' width='400' height='300' alt='' title='' /> </a></div>";
            }
            lblrightslider.Text += "</div></div> ";
        }
        else
        {
            lblrightslider.Text += "<div style='width:100%;height:258px;overflow: hidden;'><div class='item active'><a href='achievements.aspx'  title='View more'><img src='img/sections/achievement1.png' width='400' height='300' alt='' title='' /></a></div></div> ";
        }
        ds.Dispose();
    }
}