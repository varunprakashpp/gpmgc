 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;

public partial class view_customers : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, condition, id, e_id, ip, startdate, enddate,acid;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Request.QueryString["acid"] != null || Request.QueryString["name"] != null)
            {
                Label lblheading = (Label)Master.FindControl("lblheading");
                lblheading.Text = " View academic cell news";

                id = Request.Cookies["id"].Value;
                acid = EncodeDecode.base64Decode(Request.QueryString["acid"]);
                lblacname.Text = EncodeDecode.base64Decode(Request.QueryString["name"]);

                if (!IsPostBack)
                {
                    if (Request.QueryString.Count > 2)
                    {
                        e_id = EncodeDecode.base64Decode(Request.QueryString["id"]);

                        if (Request.QueryString["type"] == "delete")
                        {

                            querry = " DELETE FROM tbl_news WHERE id=" + e_id;
                            int c = cc.Insert(querry);
                            if (c > 0)
                            {
                                try
                                {
                                    System.IO.DirectoryInfo dii = new DirectoryInfo(Server.MapPath("../uploads/academiccells/" + acid + "/" + e_id + "/"));
                                    foreach (FileInfo file in dii.GetFiles())
                                    {
                                        file.Delete();
                                    }
                                    dii.Delete();
                                }
                                catch (Exception rr)
                                {
                                }

                                Response.Write("<script>alert('Deleted successfully');window.location.assign('view_academiccells_news.aspx?acid=" + Request.QueryString["acid"] + "&name=" + Request.QueryString["name"] + "');</script>");
                            }
                        }

                    }
                    else
                    {
                        display();
                    }

                }
            }
        }
        catch (Exception t)
        {
            Response.Write(t);
        }
    }

    public void display()
    {

            querry = " SELECT id, heading, description, photo1, photo2, photo3, addedon, status, display_order FROM tbl_news";
            querry += " WHERE flag='" + acid + "' AND  tag='academiccells' ORDER BY cast(display_order as int) ASC";
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
            Label l_head = (Label)e.Row.FindControl("l_head");
            Label l_dis = (Label)e.Row.FindControl("l_dis");
            Label l_image1 = (Label)e.Row.FindControl("l_image1");
            Label l_image2 = (Label)e.Row.FindControl("l_image2");
            Label l_image3 = (Label)e.Row.FindControl("l_image3");
            Label l_action = (Label)e.Row.FindControl("l_action");
            Label l_status = (Label)e.Row.FindControl("l_status");
            Label l_date = (Label)e.Row.FindControl("l_date");

            string enciid = EncodeDecode.base64Encode(l_action.Text), ciid = l_action.Text;


            l_head.Text = "<a href='add_academiccells_news.aspx?id=" + enciid + "&type=view&acid=" + Request.QueryString["acid"] + "&name=" + Request.QueryString["name"] + "' title='View More' target='_blank'><b>" + l_head.Text + "</b></a>";

            string img = "../design/dist/img/no_img.jpg";
            if (l_image1.Text != "")
            {
                string path = "../uploads/academiccells/" + acid + "/" + ciid + "/" + l_image1.Text;
                if (File.Exists(Server.MapPath(path)))
                    img = path;
            }
            else if (l_image2.Text != "")
            {
                string path = "../uploads/academiccells/" + acid + "/" + ciid + "/" + l_image2.Text;
                if (File.Exists(Server.MapPath(path)))
                    img = path;
            }
            else
            {
                string path = "../uploads/academiccells/" + acid + "/" + ciid + "/" + l_image3.Text;
                if (File.Exists(Server.MapPath(path)))
                    img = path;
            }

            l_date.Text = Convert.ToDateTime(l_date.Text).ToString("dd MMMM yyyy");

            l_image1.Text = "<a href='"+img+"' target='_blank' title='Image'><img src='"+img+"' height='100px' weight='100px'/></a>";

            if (l_status.Text=="1")
                l_status.Text = "<font color='green'>Active</font>";
            else
                l_status.Text = "<font color='red'>Inactive</font>";

            l_action.Text = "";
            l_action.Text += "<a href='add_academiccells_news.aspx?id=" + enciid + "&type=view&acid=" + Request.QueryString["acid"] + "&name=" + Request.QueryString["name"] + "' title='View' class='btn btn-success' style='margin:2px;'><i class='fa fa-eye'></i></a>";
            l_action.Text += "<a href='add_academiccells_news.aspx?id=" + enciid + "&type=edit&acid=" + Request.QueryString["acid"] + "&name=" + Request.QueryString["name"] + "' title='Edit' class='btn btn-info' style='margin:2px;'><i class='fa fa-pencil'></i></a>";
            l_action.Text += "<a href='view_academiccells_news.aspx?id=" + enciid + "&type=delete&acid=" + Request.QueryString["acid"]+ "&name=" + Request.QueryString["name"] + "' title='Delete' class='btn btn-danger' onclick=\"return confirm('Are you sure ? ')\" style='margin:2px;'><i class='fa fa-trash'></i></a>";
            
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        display();
    }
}