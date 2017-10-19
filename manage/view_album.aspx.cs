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
    static string querry, condition, id, e_id, ip, startdate, enddate;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Label lblheading = (Label)Master.FindControl("lblheading");
            lblheading.Text = " View gallery";

            id = Request.Cookies["id"].Value;


            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    e_id=EncodeDecode.base64Decode(Request.QueryString["id"]);

                    if (Request.QueryString["type"] == "delete")
                    {

                        querry = " DELETE FROM tbl_album WHERE id=" + e_id;
                        querry += " DELETE FROM tbl_album_photos WHERE album_id='" + e_id +"'";
                        int c = cc.Insert(querry);
                        if (c > 0)
                        {
                            try
                            {
                                System.IO.DirectoryInfo dii = new DirectoryInfo(Server.MapPath("../uploads/album/" + e_id + "/"));
                                foreach (FileInfo file in dii.GetFiles())
                                {
                                    file.Delete();
                                }
                                foreach (DirectoryInfo dir in dii.GetDirectories())
                                {
                                    dir.Delete(true);
                                }
                                dii.Delete();
                            }
                            catch (Exception rr)
                            {
                            }

                            Response.Write("<script>alert('Deleted successfully');window.location.assign('view_album.aspx');</script>");
                        }
                    }

                }
                else
                {
                    display();
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

            querry = " SELECT id, heading, cover_photo, addedon, status, display_order FROM tbl_album";
            querry += " WHERE flag='album' ORDER BY cast(display_order as int) ASC";
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
            Label l_image1 = (Label)e.Row.FindControl("l_image1");
            Label l_action = (Label)e.Row.FindControl("l_action");
            Label l_status = (Label)e.Row.FindControl("l_status");

            string enciid = EncodeDecode.base64Encode(l_action.Text), ciid = l_action.Text;

            string img = "../design/dist/img/no_img.jpg";
            if (l_image1.Text != "")
            {
                string path = "../uploads/album/" + ciid + "/" + l_image1.Text;
                if (File.Exists(Server.MapPath(path)))
                    img = path;
            }

            l_image1.Text = "<a href='"+img+"' target='_blank' title='Image'><img src='"+img+"' height='100px' weight='100px'/></a>";

            if (l_status.Text=="1")
                l_status.Text = "<font color='green'>Active</font>";
            else
                l_status.Text = "<font color='red'>Inactive</font>";

            l_action.Text = "";
            l_action.Text += "<a href='view_album_details.aspx?id=" + enciid + "' title='View details' class='btn btn-success' style='margin:2px;'><i class='fa fa-eye'></i></a>";
            l_action.Text += "<a href='add_album.aspx?id=" + enciid + "&type=edit' title='Edit album details' class='btn btn-info' style='margin:2px;'><i class='fa fa-pencil'></i></a>";
            l_action.Text += "<a href='add_albumphoto.aspx?id=" + enciid + "' title='Edit photos' class='btn btn-warning' style='margin:2px;'><i class='fa fa-file-image-o'></i></a>";
            l_action.Text += "<a href='view_album.aspx?id=" + enciid + "&type=delete' title='Delete' class='btn btn-danger' onclick=\"return confirm('Are you sure ? ')\" style='margin:2px;'><i class='fa fa-trash'></i></a>";
            
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        display();
    }
}