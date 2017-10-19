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
                binddepartment();
                display();
            }
        }
        else
            Response.Redirect("Default.aspx");
    }

    private void binddepartment()
    {
        querry = " select  id,name from tbl_basics where flag='academiccells' order by CAST(display_order AS int) ASC ";
        DataSet ds1 = cc.joinselect(querry);
        lbldept.Text =" <ul class='ul_teach'>";
        if (ds1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                string path = "academicnews.aspx?type=" + EncodeDecode.base64Encode(ds1.Tables[0].Rows[i].ItemArray[1].ToString()) + "&id=" + EncodeDecode.base64Encode(ds1.Tables[0].Rows[i].ItemArray[0].ToString()) ; 
                if (nid == ds1.Tables[0].Rows[i].ItemArray[0].ToString())
                    lbldept.Text += " <li class='active'>";
                else
                    lbldept.Text += " <li> ";
                lbldept.Text += " <a href='"+ path +"'><i class='fa fa-arrow-circle-right'></i>" + ds1.Tables[0].Rows[i].ItemArray[1].ToString() + "</a></li> ";
            }
        }
        else
        {
            Response.Write("<script>alert('There is no academics to show');window.location.assign('Default.aspx');");
        }
        lbldept.Text += " </ul> ";
        ds1.Dispose();
    }

    public void display()
    {
        querry = " SELECT  id,heading,addedon,description";
        querry += " ,photo1,photo2,photo3 ";
        querry += " FROM tbl_news WHERE flag='" + nid + "' AND tag='academiccells' AND status='1' ";
        querry += " ORDER BY CAST(addedon AS date) DESC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        ds.Dispose();





            //if (ds.Tables[0].Rows[0].ItemArray[3].ToString() != "")
            //{
            //    string path1 = "uploads/departments/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[0].ItemArray[3].ToString();
            //    if (File.Exists(Server.MapPath(path1)))
            //    {
            //        image += " <div class='item  active'><img src='" + path1 + "' width='885' height='512' alt='' title=''></div>";
            //        i++;
            //    }
            //}

            //if (ds.Tables[0].Rows[0].ItemArray[4].ToString() != "")
            //{
            //    string path1 = "uploads/departments/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[0].ItemArray[4].ToString();
            //    if (File.Exists(Server.MapPath(path1)))
            //    {
            //        string stat = "";
            //        if (i == 0)
            //            stat = "active";
            //        image += " <div class='item  " + stat + "'><img src='" + path1 + "' width='885' height='512' alt='' title=''></div>";
            //        i++;
            //    }
            //}

            //if (ds.Tables[0].Rows[0].ItemArray[5].ToString() != "")
            //{
            //    string path1 = "uploads/departments/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[0].ItemArray[5].ToString();
            //    if (File.Exists(Server.MapPath(path1)))
            //    {
            //        string stat = "";
            //        if (i == 0)
            //            stat = "active";
            //        image += " <div class='item " + stat + "'><img src='" + path1 + "' width='885' height='512' alt='' title=''></div>";
            //        i++;
            //    }
            //}
            //if (i == 0)
            //{
            //    image = "<div class='item  active'><img src='img/gallery/gl_02.jpg' width='885' height='512' alt='' title=''></div> ";
            //}

            //for (int k = 0; k < i; k++)
            //    indicator += "<li data-target='#carousel-example-generic' data-slide-to='"+k+"' class=''></li>";

            //if (i > 1)
            //{
            //    control = "<a class='left carousel-control' href='#carousel-example-generic' role='button' data-slide='prev'><span class='fa fa-angle-left fa-2x' aria-hidden='true'></span><span class='sr-only'>Previous</span></a>  ";
            //    control += "<a class='right carousel-control' href='#carousel-example-generic' role='button' data-slide='next'><span class='fa fa-angle-right fa-2x' aria-hidden='true'></span><span class='sr-only'>Next</span></a> ";
            //}

            //lblcont.Text = " ";
            //lblcont.Text +=" <h2 class='post-title'><a href=''>"+ head +"</a></h2> ";
            //lblcont.Text += " <div style='width:100%;height:512px;overflow: hidden;'><div class='post-image'> ";
            //lblcont.Text +=" <div id='carousel-example-generic' class='carousel slide' data-ride='carousel'> ";
            //lblcont.Text +=" <ol class='carousel-indicators'> ";
            //lblcont.Text += indicator ;
            //lblcont.Text +=" </ol> ";
            //lblcont.Text +=" <div class='carousel-inner' role='listbox'> ";
            //lblcont.Text += image ;
            //lblcont.Text +=" </div> ";
            //lblcont.Text += control;
            //lblcont.Text +=" </div> ";
            //lblcont.Text +=" </div></div> ";

            //lblcont.Text += " <div class='post-content'>" + cont + "</div> ";

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

            if (hfimg1.Value != "" || hfimg2.Value != "" || hfimg3.Value != "")
            {
                if (hfimg1.Value != "")
                {
                    string path1 = "uploads/departments/" + hfid.Value + "/" + hfimg1.Value;
                    if (File.Exists(Server.MapPath(path1)))
                    {
                        image += " <li><div><div class='img-dept'><div class='grid-item '> <img src='" + path1 + "' alt='Gallery' class='img-responsive' /> ";
                        image += " <div class='img-overlay'></div><div class='figcaption'><div class='caption-block'> </div><a href='" + path1 + "' data-rel='prettyPhoto[portfolio]'> <i class='fa fa-search'></i> </a> </div> ";
                        image += " </div></div></div></li> ";
                    }
                }

                if (hfimg2.Value != "")
                {
                    string path1 = "uploads/departments/" + hfid.Value + "/" + hfimg2.Value;
                    if (File.Exists(Server.MapPath(path1)))
                    {
                        image += " <li><div><div class='img-dept'><div class='grid-item '> <img src='" + path1 + "' alt='Gallery' class='img-responsive' /> ";
                        image += " <div class='img-overlay'></div><div class='figcaption'><div class='caption-block'> </div><a href='" + path1 + "' data-rel='prettyPhoto[portfolio]'> <i class='fa fa-search'></i> </a> </div> ";
                        image += " </div></div></div></li> ";
                    }
                }

                if (hfimg3.Value != "")
                {
                    string path1 = "uploads/departments/" + hfid.Value + "/" + hfimg3.Value;
                    if (File.Exists(Server.MapPath(path1)))
                    {
                        image += " <li><div><div class='img-dept'><div class='grid-item '> <img src='" + path1 + "' alt='Gallery' class='img-responsive' /> ";
                        image += " <div class='img-overlay'></div><div class='figcaption'><div class='caption-block'> </div><a href='" + path1 + "' data-rel='prettyPhoto[portfolio]'> <i class='fa fa-search'></i> </a> </div> ";
                        image += " </div></div></div></li> ";
                    }
                }
            }

            if (image != "")
                image = "<div class='clearfix'></div> <div class='related_img'> <ul class='ul_related_img'>" + image + "</ul></div>";

            lbldata.Text = " ";
            lbldata.Text += " <h2 class='post-title'><a href=''>" + head + "</a></h2> ";
            lbldata.Text += image;
            lbldata.Text += " <div class='post-content'>" + cont + "</div> ";

        }
    }
}