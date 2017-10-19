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
            lblheading.Text = " View staff";

            id = Request.Cookies["id"].Value;


            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    e_id=EncodeDecode.base64Decode(Request.QueryString["id"]);

                    if (Request.QueryString["type"] == "delete")
                    {

                        querry = " DELETE FROM tbl_staff WHERE id=" + e_id;
                        int c = cc.Insert(querry);
                        if (c > 0)
                        {
                            try
                            {
                                System.IO.DirectoryInfo dii = new DirectoryInfo(Server.MapPath("../uploads/staff/" + e_id + "/"));
                                foreach (FileInfo file in dii.GetFiles())
                                {
                                    file.Delete();
                                }
                                //foreach (DirectoryInfo dir in dii.GetDirectories())
                                //{
                                //    dir.Delete(true);
                                //}
                                dii.Delete();
                            }
                            catch (Exception rr)
                            {
                            }

                            Response.Write("<script>alert('Deleted successfully');window.location.assign('view_staff.aspx');</script>");
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
        querry = " SELECT id, name, designation, category, email, mobile, photo, display_order, status, gender";
        querry += " ,(CASE WHEN category = 'teaching' THEN COALESCE (NULLIF ((SELECT  heading FROM tbl_news WHERE id = s.department), ''), '') ELSE s.department END) AS department ";
        querry += " FROM tbl_staff s ORDER BY cast(display_order as int) ASC";
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
            Label l_name = (Label)e.Row.FindControl("l_name");
            Label l_cat = (Label)e.Row.FindControl("l_cat");
            Label l_status = (Label)e.Row.FindControl("l_status");
            Label l_dep = (Label)e.Row.FindControl("l_dep");
            Label l_des = (Label)e.Row.FindControl("l_des");
            Label l_email = (Label)e.Row.FindControl("l_email");
            Label l_mobile = (Label)e.Row.FindControl("l_mobile");
            Label l_action = (Label)e.Row.FindControl("l_action");
            Label l_gender = (Label)e.Row.FindControl("l_gender");

            string enciid = EncodeDecode.base64Encode(l_action.Text), ciid = l_action.Text;

            string img = "../design/dist/img/no_img.jpg";
            if (l_image1.Text != "")
            {
                string path = "../uploads/staff/" + ciid + "/" + l_image1.Text;
                if (File.Exists(Server.MapPath(path)))
                    img = path;
            }

            l_image1.Text = "<a href='" + img + "' target='_blank' title='Image'><img src='" + img + "' height='100px' weight='100px'/></a>";

            if (l_status.Text == "1")
                l_status.Text = "<font color='green'><i class='fa fa-check-circle' ></i>&emsp;Active</font>";
            else
                l_status.Text = "<font color='red'><i class='fa fa-times-circle' ></i>&emsp;Inactive</font>";

            if (l_gender.Text == "Male")
                l_gender.Text = "<font color='red'><i class='fa fa-male' ></i>&emsp;</font>";
            else
                l_gender.Text = "<font color='#d06e08'><i class='fa fa-female' ></i>&emsp;</font>";

            if (l_cat.Text == "teaching")
                l_cat.Text = "<font color='green'>Teaching</font>";
            else
                l_cat.Text = "<font color='red'>Non teaching</font>";

            l_name.Text = l_gender.Text + l_name.Text + "<br/>" + l_status.Text + "<br/>" + l_cat.Text;


            l_dep.Text = l_des.Text + "<br/><i class='fa fa-university'></i>&emsp;<font color='#a7a499'>" + l_dep.Text + "</font>";


            l_action.Text = "";
            l_action.Text += "<a href='add_staff.aspx?id=" + enciid + "&type=view' title='View staff details' class='btn btn-info' style='margin:2px;'><i class='fa fa-eye'></i></a>";
            l_action.Text += "<a href='add_staff.aspx?id=" + enciid + "&type=edit' title='Edit staff details' class='btn btn-success' style='margin:2px;'><i class='fa fa-pencil'></i></a>";
            l_action.Text += "<a href='view_staff.aspx?id=" + enciid + "&type=delete' title='Delete' class='btn btn-danger' onclick=\"return confirm('Are you sure ? This will delete all related files.')\" style='margin:2px;'><i class='fa fa-trash'></i></a>";
            
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        display();
    }
}