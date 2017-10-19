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
    static string querry, condition,id,e_id,date,addedby,ip,type;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            Label lblheading = (Label)Master.FindControl("lblheading");
            lblheading.Text = "Gallery details";

            id = Request.Cookies["id"].Value;
            date = DateTime.Now.AddHours(5).AddMinutes(30).ToString();
            addedby = Request.Cookies["id"].Value;
            ip = getclientIP();
            type = "admin";

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null )
                {
                    e_id = EncodeDecode.base64Decode(Request.QueryString["id"]);
                    assign();
                    lockall();
                }
                else
                {
                    Response.Write("<script>alert('Please select an gallery !');window.location.assign('view_album.aspx');</script>");
                }
            }
        }
        catch (Exception t)
        {
            Response.Write(t);
        }
    }


    public void lockall()
    {
        txt_head.Attributes.Add("readonly","readonly");
        txt_des.Attributes.Add("readonly", "readonly");
        txt_diso.Attributes.Add("readonly", "readonly");
        rbl_stat.Enabled = false;
    }

    public void assign()
    {
        querry = "select  id,heading,description,cover_photo,display_order,status from tbl_album where flag='album' and  id=" + e_id ;
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_head.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            txt_des.Text = EncodeDecode.base64Decode( ds.Tables[0].Rows[0].ItemArray[2].ToString());
            txt_diso.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();

            string cphoto = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            string icon = "", src = "../design/dist/img/no_img.jpg";

            if (cphoto != "")
            {
                string path = "../uploads/album/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + cphoto;
                if (File.Exists(Server.MapPath(path)))
                    cphoto = path;
                else
                    cphoto = icon;
            }


            lbl_img1.Text = "<a href='" + cphoto + "' title='file' target='_blank'><img src='"+cphoto+"' width='80px'/></a><br/><br/>";

            rbl_stat.Enabled = true;
            rbl_stat.SelectedValue = ds.Tables[0].Rows[0].ItemArray[4].ToString();


            querry = "select  id, heading, photo, display_order, status from  tbl_album_photos where album_id='" + e_id + "'";
            DataSet ds1 = cc.joinselect(querry);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lbl_image.Text = "";
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    lbl_image.Text += "<div class='col-lg-2 col-md-2 col-sm-2' style='margin-top:10px;'>";
                    string path = "../uploads/album/" + e_id + "/photos/" + ds1.Tables[0].Rows[i].ItemArray[2].ToString(),status="";
                    if (File.Exists(Server.MapPath(path)))
                    {
                        if (ds1.Tables[0].Rows[i].ItemArray[4].ToString() == "1")
                            status = "<font color='green'>Active</font>";
                        else
                            status = "<font color='red'>Inactive</font>";
                        lbl_image.Text += "<div class='img_wrap'>";
                        
                        lbl_image.Text += "<img src='" + path + "' height='100px' width='100%'/>";
                        lbl_image.Text += "</div>";
                        lbl_image.Text += "<div >";
                        lbl_image.Text += "Heading :<b>" + ds1.Tables[0].Rows[i].ItemArray[1].ToString() + "</b>&nbsp;<br/>";
                        lbl_image.Text += "Display order :<b>" + ds1.Tables[0].Rows[i].ItemArray[3].ToString() + "</b>&nbsp;<br/>";
                        lbl_image.Text += "Status :<b>" + status + "</b>&nbsp;";
                        lbl_image.Text += "</div>";
                    }
                    lbl_image.Text += "</div>";
                }
               
            }

        }
        ds.Dispose();
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