using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;

public partial class about : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry,bid,type;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["type"] != null)
            {
                type=Request.QueryString["type"];

                if (type == "teaching")
                {
                    Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
                    lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Teaching</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li >Administration</li><li class='active'>Teaching</li></ul></div></div>";

                }
                else
                {
                    Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
                    lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Non Teaching</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li> Administration</li><li class='active'>Non teaching</li></ul></div></div>";

                }
                

                if (!IsPostBack)
                {
                    bid = "";
                    if (Request.QueryString["to"] != null)
                    {
                        bid = EncodeDecode.base64Decode(Request.QueryString["to"]);
                    }

                    if (type == "teaching")
                    {
                        bind_branch();
                        display();
                    }
                    else
                    {
                        bind_branch_nonteach();
                        display_nonteach();
                    }

                }
            }
            else
                Response.Redirect("Default.aspx");
        }
        catch (Exception t)
        {
            Response.Write(t);
        }
    }

    public void bind_branch()
    {
        querry = "SELECT id, heading FROM tbl_news WHERE flag='departments' ORDER BY CAST(display_order AS int) ASC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_branch.Text = "<ul class='ul_teach'>";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lbl_branch.Text += "<li";
                if (bid != "")
                {
                    if (bid == ds.Tables[0].Rows[i].ItemArray[0].ToString())
                    {
                        lbl_branch.Text += " class='active'";
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        lbl_branch.Text += " class='active'";
                        bid = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    }
                }
                lbl_branch.Text += " ><a href='teaching.aspx?type=teaching&to=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "'><i class='fa fa-arrow-circle-right'></i>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</a> </li>";
            }
            lbl_branch.Text += "</ul>";
        }
        ds.Dispose();
    }

    public void display()
    {
        querry = "SELECT id, name,designation,email,photo,display_order FROM tbl_staff WHERE category='teaching' ";
        querry += " AND status='1' AND department='" + bid + "' ";
        querry += " ORDER BY CAST(display_order AS int) ASC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            
            string hod = "", staff = "";
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string src = "img/staff/st_01.png";
                if (ds.Tables[0].Rows[i].ItemArray[4].ToString() != "")
                {
                    string path = "uploads/staff/" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[i].ItemArray[4].ToString();
                    if (File.Exists(Server.MapPath(path)))
                        src = path;
                }

                if (ds.Tables[0].Rows[i].ItemArray[5].ToString() == "1")
                {
                    hod = " <div class='teach_top'><ul class='ul_admin_bd'><li>";
                    hod += " <figure><img src='"+ src +"' width='145' height='164'/></figure> ";
                    hod += " <figcaption><h3>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</h3><p class='desc'>" + ds.Tables[0].Rows[i].ItemArray[2].ToString() + "</p><p class='email'>" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "</p><figcaption> ";
                    hod += " </li></ul></div><hr/> ";
                }
                else
                {
                    staff += " <li> ";
                    staff += " <figure><img src='" + src + "'  width='100' height='103'></figure> ";
                    staff += " <figcaption><h3>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</h3><p class='email'>" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "</p></figcaption> ";
                    staff += " </li>";
                }                
            }

            if(staff!="")
                staff = "<div class='teach_bottom'> <ul class='ul_admin_bd ul_other_staff'>"+ staff +"</ul></div>";

            lblstaff.Text = hod + staff;
        }
        ds.Dispose();
    }

    public void bind_branch_nonteach()
    {
        querry = " SELECT DISTINCT department FROM tbl_staff WHERE (category = 'nonteaching') AND (status = '1')";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_branch.Text = "<ul class='ul_teach'>";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lbl_branch.Text += "<li";
                if (bid != "")
                {
                    if (bid == ds.Tables[0].Rows[i].ItemArray[0].ToString())
                    {
                        lbl_branch.Text += " class='active'";
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        lbl_branch.Text += " class='active'";
                        bid = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    }
                }
                lbl_branch.Text += " ><a href='teaching.aspx?type=nonteaching&to=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "'><i class='fa fa-arrow-circle-right'></i>" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "</a> </li>";
            }
            lbl_branch.Text += "</ul>";
        }
        ds.Dispose();
    }

    public void display_nonteach()
    {
        querry = "SELECT id, name,designation,email,photo,display_order FROM tbl_staff WHERE category='nonteaching' ";
        querry += " AND status='1' AND department='" + bid + "' ";
        querry += " ORDER BY CAST(display_order AS int) ASC";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string hod = "", staff = "";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string src = "img/staff/st_01.png";
                if (ds.Tables[0].Rows[i].ItemArray[4].ToString() != "")
                {
                    string path = "uploads/staff/" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "/" + ds.Tables[0].Rows[i].ItemArray[4].ToString();
                    if (File.Exists(Server.MapPath(path)))
                        src = path;
                }

                if (ds.Tables[0].Rows[i].ItemArray[5].ToString() == "1")
                {
                    hod = " <div class='teach_top'><ul class='ul_admin_bd'><li>";
                    hod += " <figure><img src='" + src + "' width='145' height='164'/></figure> ";
                    hod += " <figcaption><h3>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</h3><p class='desc'>" + ds.Tables[0].Rows[i].ItemArray[2].ToString() + "</p><p class='email'>" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "</p><figcaption> ";
                    hod += " </li></ul></div><hr/> ";
                }
                else
                {
                    staff += " <li> ";
                    staff += " <figure><img src='" + src + "'  width='100' height='103'></figure> ";
                    staff += " <figcaption><h3>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</h3><p class='desc'>" + ds.Tables[0].Rows[i].ItemArray[2].ToString() + "</p><p class='email'>" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "</p></figcaption> ";
                    staff += " </li>";
                }
            }

            if (staff != "")
                staff = "<div class='teach_bottom'> <ul class='ul_admin_bd ul_other_staff'>" + staff + "</ul></div>";

            lblstaff.Text = hod + staff;
        }
        ds.Dispose();
    }

}