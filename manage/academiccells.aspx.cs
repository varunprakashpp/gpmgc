using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class add_academiccells : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, condition,id,e_id,date,addedby,ip,type,ext;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            Label lblheading = (Label)Master.FindControl("lblheading");
            lblheading.Text = "Academic cells";

            id = Request.Cookies["id"].Value;
            date = DateTime.Now.AddHours(5).AddMinutes(30).ToString();
            addedby = Request.Cookies["id"].Value;
            ip = getclientIP();
            type = "admin";

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null & Request.QueryString["type"] != null)
                {
                    e_id = EncodeDecode.base64Decode(Request.QueryString["id"]);
                    if (Request.QueryString["type"] == "delete")
                    {
                        querry = " DELETE FROM tbl_basics WHERE id=" + e_id;
                        querry += " DELETE FROM tbl_news WHERE flag='" + e_id +"' and tag='academiccells' ";
                        int c = cc.Insert(querry);
                        if (c > 0)
                        {
                            string filepath = "../uploads/academiccells/" + e_id;
                            if (System.IO.Directory.Exists(Server.MapPath(filepath)))
                            {
                                try
                                {
                                    System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(filepath));

                                    foreach (FileInfo file in di.GetFiles())
                                    {
                                        file.Delete();
                                    }
                                    foreach (DirectoryInfo dir in di.GetDirectories())
                                    {
                                        dir.Delete(true);
                                    }
                                    di.Delete(true);
                                }
                                catch (Exception rr)
                                {
                                }
                            }

                            Response.Write("<script>alert('Deleted successfully');window.location.assign('academiccells.aspx');</script>");
                        }
                    }

                    assign();
                }

                display();
            }
        }
        catch (Exception t)
        {
            Response.Write(t);
        }
    }

    public void assign()
    {
        querry = "select  id,name,display_order from tbl_basics where flag='academiccells' and id=" + e_id;
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_head.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            txt_diso.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            Button1.Text = "Update";
        }
        ds.Dispose();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ext = "";
            if (Request.QueryString.Count > 0)
            {
                ext = " and id<>" + e_id;
            }
            string query = "select id from tbl_basics where name='" + safesql.SafeSqlLiterall(txt_head.Text, 2) + "' and flag='academiccells'" + ext;
            DataSet ds = cc.joinselect(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label lblmsg = (Label)Master.FindControl("lblmsg");
                string msg = "Already exist !! Try another name";
                lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
                return;
            }

            if (Request.QueryString.Count > 0)
            {
                querry = "update tbl_basics set ";
                querry += " name='" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "'";
                querry += ",display_order='" + safesql.SafeSqlLiterall(txt_diso.Text.Replace("'", "`"), 2) + "'";
                querry += ",modifiedby='" + id + "'";
                querry += ",modifiedon='" + date + "'";
                querry += ",modifiedtype='admin'";
                querry += " where id=" + e_id ;
                int q = cc.Insert(querry);

                if (q > 0)
                {
                    Response.Write("<script>alert('Updated successfully');window.location.assign('academiccells.aspx');</script>");
                }
                else
                {
                    display();
                    Label lblmsg = (Label)Master.FindControl("lblmsg");
                    string msg = " Updation failed !";
                    lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
                }
            }
            else
            {

                querry = "insert into tbl_basics (name,display_order,flag,addedby,addedon,addedtype,ip) ";
                querry += " values('" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "'";
                querry += " ,'" + safesql.SafeSqlLiterall(txt_diso.Text.Replace("'", "`"), 2) + "','academiccells'";
                querry += " ,'" + id + "','" + date + "','admin','"+ip+"')";
                int q = cc.Insert(querry);

                if (q > 0)
                {  
                    clear();

                    display();
                    Label lblmsg = (Label)Master.FindControl("lblmsg");
                    string msg = "Added successfully!";
                    lblmsg.Text = "<div class='box box-success box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";

                }
                else
                {
                    display();
                    Label lblmsg = (Label)Master.FindControl("lblmsg");
                    string msg = "Adding failed !";
                    lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
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
        querry = " SELECT id,name,display_order FROM tbl_basics WHERE flag='academiccells'  ORDER BY CAST(display_order AS int) ASC";
        DataSet ds = cc.joinselect(querry);
        lbl_tablebody.Text = "";

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lbl_tablebody.Text += "<tr>  <td>" + (i + 1) + "</td>";
                lbl_tablebody.Text += "<td>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</td>";
                lbl_tablebody.Text += "<td>" +  ds.Tables[0].Rows[i].ItemArray[2].ToString() + "</td>";
                lbl_tablebody.Text += "<td>";
                lbl_tablebody.Text += "<a href='academiccells.aspx?id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&type=edit' title='Edit' class='btn btn-warning' style='margin:2px;'><i class='fa fa-edit'></i></a>";
                lbl_tablebody.Text += "<a href='academiccells.aspx?id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&type=delete' title='Delete' class='btn btn-danger' onclick=\"return confirm('Are you sure ? Deleting will effects all related data ,if exists.')\"  style='margin:2px;'><i class='fa fa-trash'></i></a>";
                lbl_tablebody.Text += "<br/><a href='add_academiccells_news.aspx?acid=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&name=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[1].ToString()) + "' title='Add news' class='btn btn-success' style='margin:2px;'><i class='fa fa-file'></i></a>";
                lbl_tablebody.Text += "<a href='view_academiccells_news.aspx?acid=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&name=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[1].ToString()) + "' title='View news' class='btn btn-warning' style='margin:2px;'><i class='fa fa-eye'></i></a>";
                lbl_tablebody.Text += "</td>  </tr>";
            }
        }
        ds.Dispose();
    }

    public void clear()
    {
        txt_head.Text = "";
        txt_diso.Text = "";

    }

    private string getclientIP()
    {
        string sIPAddress = null;
        sIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(sIPAddress))
            sIPAddress = Request.ServerVariables["REMOTE_ADDR"];

        return sIPAddress;
    }
}