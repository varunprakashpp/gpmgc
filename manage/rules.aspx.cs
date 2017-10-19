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

public partial class add_category : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, condition,id,e_id,date,addedby,ip,type,ext;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            Label lblheading = (Label)Master.FindControl("lblheading");
            lblheading.Text = "Rules";

            id = Request.Cookies["id"].Value;
            date = DateTime.Now.AddHours(5).AddMinutes(30).ToString();
            addedby = Request.Cookies["id"].Value;
            ip = getclientIP();
            type = "admin";

            txt_head.config.toolbar = new object[]
			{
				new object[] { "Source", "-"},
                new object[] { "Bold", "Italic", "Underline","Subscript","Superscript" },
                new object[] { "Link","Unlink","Anchor","JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
                new object[] { "NumberedList","BulletedList","Blockquote","Image", "Table","Styles","Format","Font", "Size"},
		        new object[] { "TextColor", "BGColor" }	

			};

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null & Request.QueryString["type"] != null)
                {
                    e_id = EncodeDecode.base64Decode(Request.QueryString["id"]);
                    if (Request.QueryString["type"] == "delete")
                    {
                        querry = " DELETE FROM tbl_basics WHERE id=" + e_id;
                        int c = cc.Insert(querry);
                        if (c > 0)
                        {
                            Response.Write("<script>alert('Deleted successfully');window.location.assign('rules.aspx');</script>");
                        }
                    }

                }
                assign();
            }
        }
        catch (Exception t)
        {
            Response.Write(t);
        }
    }

    public void assign()
    {
        querry = "select  id,name from tbl_basics where flag='rules' ";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_head.Text = EncodeDecode.base64Decode(ds.Tables[0].Rows[0].ItemArray[1].ToString());
            Button1.Text = "Update";
        }
        ds.Dispose();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            //ext = "";
            //if (Request.QueryString.Count > 0)
            //{
            //    ext = " and id<>" + e_id;
            //}
            //string query = "select id from tbl_basics where name='" + EncodeDecode.base64Encode(safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2)) + "' and flag='rules' " + ext;
            //DataSet ds = cc.joinselect(query);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    Label lblmsg = (Label)Master.FindControl("lblmsg");
            //    string msg = "Already exist !! Try another data";
            //    lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
            //    return;
            //}

            if (Button1.Text == "Update")
            {
                querry = "update tbl_basics set ";
                querry += " name='" + EncodeDecode.base64Encode(safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`").Replace("[%]", "%"), 2)) + "'";
                querry += ",display_order='100'";
                querry += ",modifiedby='" + id + "'";
                querry += ",modifiedon='" + date + "'";
                querry += ",modifiedtype='admin'  where flag='rules' ";
                int q = cc.Insert(querry);

                if (q > 0)
                {
                    Response.Write("<script>alert('Updated successfully');window.location.assign('rules.aspx');</script>");
                }
                else
                {
                    Label lblmsg = (Label)Master.FindControl("lblmsg");
                    string msg = " Updation failed !";
                    lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
                }
            }
            else
            {

                querry = "insert into tbl_basics (name,flag,addedby,addedon,addedtype,ip) ";
                querry += " values('" + EncodeDecode.base64Encode(safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`").Replace("[%]", "%"), 2)) + "','rules'";
                querry += " ,'" + id + "','" + date + "','admin','"+ip+"')";
                int q = cc.Insert(querry);

                if (q > 0)
                {  
                    clear();

                    assign();
                    Label lblmsg = (Label)Master.FindControl("lblmsg");
                    string msg = "Added successfully!";
                    lblmsg.Text = "<div class='box box-success box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";

                }
                else
                {
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


    public void clear()
    {
        txt_head.Text = "";
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