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
    static string querry, newstype,title;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            //news,flashnews
            newstype = Request.QueryString["type"];
            if (newstype == "news")
                title = "News";
            else if (newstype == "flashnews")
                title = "Flash news";
            Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
            lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>" + title.ToUpper() + "</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li class='active'>" + title + "</li></ul></div></div>";


            display();
        }
        else
            Response.Redirect("Default.aspx");
    }

    public void display()
    {
        querry = " SELECT  id,heading,addedon,description";
        querry += " ,(CASE WHEN ISNULL(photo1, '') = '' THEN (CASE WHEN ISNULL(photo2, '') = '' THEN (CASE WHEN ISNULL(photo3, '') = '' THEN (CASE WHEN ISNULL(photo4, '') = '' THEN '' ELSE photo4 END) ELSE photo3 END) ELSE photo2 END) ELSE photo1 END) AS photo";
        querry += " FROM tbl_news WHERE flag='" + newstype + "' ";
        querry += " ORDER BY CAST(addedon AS date) DESC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string divstart = "", divend = "", head = ds.Tables[0].Rows[i].ItemArray[1].ToString(), cont = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                string photo = "img/sections/no_img.png", adate = Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[2]).ToString("dd.mm.yyyy");
                string path = "news_more.aspx?id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&type=" + newstype + "";

                cont = EncodeDecode.base64Decode(cont);
                if (cont.Length > 131)
                    cont = cont.Substring(0, 131) + "...";


                if (ds.Tables[0].Rows[i].ItemArray[4].ToString() != "")
                {
                    string path1 = "uploads/" + newstype + "/" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[i].ItemArray[4].ToString();
                    if (File.Exists(Server.MapPath(path1)))
                        photo = path1;
                }


                lblcontent.Text += " <div class='col-sm-4'><div class='news_blk'> ";
                lblcontent.Text += " <p class='text-center'><a href='"+photo+"' class='opacity' data-rel='prettyPhoto[portfolio]'><img src='"+photo+"' width='370' height='185' alt=''></a></p> ";
                lblcontent.Text += " <h5 class='bottom-margin-10'><a href='"+path+"' class='black'>"+head+"</a></h5> ";
                lblcontent.Text += " <p class='date'>" + adate + "</p> ";
                lblcontent.Text += " <p  class='news_desc'>" + cont + "</p> ";
                lblcontent.Text += " </div></div> ";

            }
        }
    }

}