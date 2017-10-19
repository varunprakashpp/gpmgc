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
    static string querry, newstype,title,nid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            nid = EncodeDecode.base64Decode(Request.QueryString["id"]);
            
            Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
            lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Project More</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li >Research Projects</li><li class='active'>Project More</li></ul></div></div>";

            display();
        }
        else
            Response.Redirect("Default.aspx");
    }

    public void display()
    {
        querry = " SELECT  id,heading,addedon,description,project_team";//4
        querry += " ,photo1, photo2, photo3, project_report";//8
        querry += " FROM tbl_projects WHERE id=" + nid;
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string head = ds.Tables[0].Rows[0].ItemArray[1].ToString(), cont = ds.Tables[0].Rows[0].ItemArray[3].ToString(), pteam = EncodeDecode.base64Decode(ds.Tables[0].Rows[0].ItemArray[4].ToString());
            string image = "", control = "",preport="", adate = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[2]).ToString("MMM dd yyyy");
            int i = 0;

            cont = EncodeDecode.base64Decode(cont);

            if (ds.Tables[0].Rows[0].ItemArray[8].ToString() != "")
            {
                string path1 = "uploads/research/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[0].ItemArray[8].ToString();
                if (File.Exists(Server.MapPath(path1)))
                {
                    preport = " <div class='clearfix'></div><a href='"+ path1 +"' class='download' title='Download'> <p><i class='fa fa-download'></i>&nbsp;&nbsp;Project Reports</p></a>";
                }
            }

            image = "<div id='project_more' class='carousel slide' data-ride='carousel'><div class='carousel-inner' role='listbox'> ";
            if (ds.Tables[0].Rows[0].ItemArray[5].ToString() != "")
            {
                string path1 = "uploads/research/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[0].ItemArray[5].ToString();
                if (File.Exists(Server.MapPath(path1)))
                {
                    image += " <div class='item  active'><img src='"+ path1 +"' width='800' height='570' alt='' title=''></div>";
                    i++;
                }
            }

            if (ds.Tables[0].Rows[0].ItemArray[6].ToString() != "")
            {
                string path1 = "uploads/research/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[0].ItemArray[6].ToString();
                if (File.Exists(Server.MapPath(path1)))
                {
                    string stat = "";
                    if (i == 0)
                        stat = "active";
                    image += " <div class='item  " + stat + "'><img src='" + path1 + "' width='800' height='570' alt='' title=''></div>";
                    i++;
                }
            } 
            
            if (ds.Tables[0].Rows[0].ItemArray[7].ToString() != "")
            {
                string path1 = "uploads/research/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[0].ItemArray[7].ToString();
                if (File.Exists(Server.MapPath(path1)))
                {
                    string stat = "";
                    if (i == 0)
                        stat = "active";
                    image += " <div class='item "+ stat +"'><img src='" + path1 + "' width='800' height='570' alt='' title=''></div>";
                    i++;
                }
            }
            if (i == 0)
                image += "<div class='item  active'><img src='img/sections/about/img1.jpg' width='800' height='570' alt='' title=''></div> ";

            image += "</div></div>";

            if (i > 1)
            {
                control = "<a class='left carousel-control' href='#project_more' role='button' data-slide='prev'><span class='fa fa-angle-left fa-2x' aria-hidden='true'></span><span class='sr-only'>Previous</span></a>  ";
                control += "<a class='right carousel-control' href='#project_more' role='button' data-slide='next'><span class='fa fa-angle-right fa-2x' aria-hidden='true'></span><span class='sr-only'>Next</span></a> ";
            }

            lblimage.Text = image + control;


            lbldet.Text +=" <h5 class='bottom-margin-10'><a href='' class='black'>"+ head +"</a></h5> ";
            lbldet.Text +=" <p class='date'>"+ adate +"</p> ";
            lbldet.Text +=" <div class='mfs'> ";
            lbldet.Text +=" <p style='color:#09C'> <i class='fa fa-user'></i>&nbsp;&nbsp;&nbsp;"+ pteam +" </p> ";
            lbldet.Text += preport;
            lbldet.Text +=" </div> ";
            lbldet.Text +=" <div class='clearfix'></div> ";
            lbldet.Text +=" <p class='news_desc text-justify'>"+ cont +"</p> ";

        }
        ds.Dispose();
    }

}