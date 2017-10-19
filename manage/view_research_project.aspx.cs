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
            lblheading.Text = " View research projects";

            id = Request.Cookies["id"].Value;


            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    e_id=EncodeDecode.base64Decode(Request.QueryString["id"]);

                    if (Request.QueryString["type"] == "delete")
                    {

                        querry = " DELETE FROM tbl_projects WHERE id=" + e_id;
                        int c = cc.Insert(querry);
                        if (c > 0)
                        {
                            try
                            {
                                System.IO.DirectoryInfo dii = new DirectoryInfo(Server.MapPath("../uploads/research/" + e_id + "/"));
                                foreach (FileInfo file in dii.GetFiles())
                                {
                                    file.Delete();
                                }
                                dii.Delete();
                            }
                            catch (Exception rr)
                            {
                            }

                            Response.Write("<script>alert('Deleted successfully');window.location.assign('view_research_project.aspx');</script>");
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

            querry = " SELECT id, heading, description, photo1, photo2, photo3, addedon, status, project_team, project_report FROM tbl_projects";
            querry += " WHERE flag='research' ORDER BY cast(display_order as int) ASC";
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
            Label l_file = (Label)e.Row.FindControl("l_file");
            Label l_team = (Label)e.Row.FindControl("l_team");
            Label l_image1 = (Label)e.Row.FindControl("l_image1");
            Label l_image2 = (Label)e.Row.FindControl("l_image2");
            Label l_image3 = (Label)e.Row.FindControl("l_image3");
            Label l_action = (Label)e.Row.FindControl("l_action");
            Label l_status = (Label)e.Row.FindControl("l_status");
            Label l_date = (Label)e.Row.FindControl("l_date");

            string enciid = EncodeDecode.base64Encode(l_action.Text), ciid = l_action.Text;


            l_head.Text = "<a href='add_research_projects.aspx?id=" + enciid + "&type=view' title='View More' target='_blank'><b>" + l_head.Text + "</b></a>";

            string img = "../design/dist/img/no_img.jpg";
            if (l_image1.Text != "")
            {
                string path = "../uploads/research/" + ciid + "/" + l_image1.Text;
                if (File.Exists(Server.MapPath(path)))
                    img = path;
            }
            else if (l_image2.Text != "")
            {
                string path = "../uploads/research/" + ciid + "/" + l_image2.Text;
                if (File.Exists(Server.MapPath(path)))
                    img = path;
            }
            else
            {
                string path = "../uploads/research/" + ciid + "/" + l_image3.Text;
                if (File.Exists(Server.MapPath(path)))
                    img = path;
            }

            l_date.Text = Convert.ToDateTime(l_date.Text).ToString("dd MMMM yyyy");

            l_image1.Text = "<a href='"+img+"' target='_blank' title='Image'><img src='"+img+"' height='100px' weight='100px'/></a>";

            if (l_status.Text=="1")
                l_status.Text = "<font color='green'>Active</font>";
            else
                l_status.Text = "<font color='red'>Inactive</font>";

            if (l_team.Text != "")
                l_team.Text = EncodeDecode.base64Decode(l_team.Text);

            string img1 = "../design/dist/img/file/no_img.png", path1="";
            if (l_file.Text != "")
            {
                path1 = "../uploads/research/" + ciid + "/" + l_file.Text;
                if (File.Exists(Server.MapPath(path1)))
                {
                    //".pdf", ".doc", ".docx", ".ppt", ".pptx"
                    if ((l_file.Text).Split('.').Last() == "pdf")
                        img1 = "../design/dist/img/file/pdf.png";
                    else if ((l_file.Text).Split('.').Last() == "doc" || (l_file.Text).Split('.').Last() == "docx")
                        img1 = "../design/dist/img/file/doc.png";
                    else if ((l_file.Text).Split('.').Last() == "ppt" || (l_file.Text).Split('.').Last() == "pptx")
                        img1 = "../design/dist/img/file/ppt.png";

                }
            }

            l_file.Text = "<a href='" + path1 + "' title='Download report'><img src='" + img1 + "' weight='80px'/></a>";


            l_action.Text = "";
            l_action.Text += "<a href='add_research_project.aspx?id=" + enciid + "&type=view' title='View' class='btn btn-success' style='margin:2px;'><i class='fa fa-eye'></i></a>";
            l_action.Text += "<a href='add_research_project.aspx?id=" + enciid + "&type=edit' title='Edit' class='btn btn-info' style='margin:2px;'><i class='fa fa-pencil'></i></a>";
            l_action.Text += "<a href='view_research_project.aspx?id=" + enciid + "&type=delete' title='Delete' class='btn btn-danger' onclick=\"return confirm('Are you sure ? ')\" style='margin:2px;'><i class='fa fa-trash'></i></a>";
            
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        display();
    }
}