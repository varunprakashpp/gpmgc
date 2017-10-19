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
            lblheading.Text = "Gallery" ;

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
                    assign();
                    if (Request.QueryString["type"] == "view")
                    {
                        lockall();
                    }
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
        divbutton.Style.Add("display","none");
        pnl_img1.Visible = false;
        rbl_stat.Enabled = false;
    }

    public void assign()
    {
        querry = "select  id,heading,description,cover_photo,display_order,status from tbl_album where flag='album' and  id=" + e_id ;
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_head.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            txt_des.Text = EncodeDecode.base64Decode(ds.Tables[0].Rows[0].ItemArray[2].ToString()) ;
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

            Button1.Text = "Update";
            fu_img1.Attributes.Add("class","");
        }
        ds.Dispose();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = getclientIP(),diso="100";
        
        try
        {
            if (txt_diso.Text != "")
                diso = safesql.SafeSqlLiterall(txt_diso.Text.Replace("'", "`"), 2);

            if (Request.QueryString.Count > 0)
            {
                querry = "update tbl_album set ";
                querry += " heading='" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "'";
                querry += ",description='" + EncodeDecode.base64Encode(safesql.SafeSqlLiterall(txt_des.Text.Replace("'", "`"), 2)) + "'";
                querry += ",display_order='" + diso + "'";
                querry += ",status='" + safesql.SafeSqlLiterall(rbl_stat.SelectedValue.Replace("'", "`"), 2) + "'";
                querry += ",modifiedby='" + id + "'";
                querry += ",modifiedon='" + date + "'";
                querry += ",modifiedtype='admin'";
                querry += " where id=" + e_id ;
                int q = cc.Insert(querry);

                if (q > 0)
                {
                    if (fu_img1.HasFile )
                        coverimagesave(e_id);

                    Response.Write("<script>alert('Updated successfully');window.location.assign('view_album.aspx');</script>");
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

                querry = "insert into tbl_album (heading, description,display_order,status,flag,addedby,addedon,addedtype,ip) ";
                querry += " values('" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "'";
                querry += " ,'" + EncodeDecode.base64Encode(safesql.SafeSqlLiterall(txt_des.Text.Replace("'", "`"), 2)) + "'";
                querry += " ,'" + diso + "','1','album'";
                querry += " ,'" + id + "','" + date + "','admin','"+ip+"')";
                int q = cc.Insert(querry);

                if (q > 0)
                {

                    querry = "select max(id) from tbl_album where heading='" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "' and description='" + EncodeDecode.base64Encode(safesql.SafeSqlLiterall(txt_des.Text.Replace("'", "`"), 2)) + "' and addedby='" + id + "' and addedtype='admin' and addedon='" + date + "' and flag='album' ";
                    DataSet ds1 = cc.joinselect(querry);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        e_id = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                        if (fu_img1.HasFile )
                            coverimagesave(e_id);
                        clear();

                        Response.Write("<script>alert('Added successfully!');window.location.assign('add_albumphoto.aspx?id="+ EncodeDecode.base64Encode(e_id) +"');</script>");
                        //Label lblmsg = (Label)Master.FindControl("lblmsg");
                        //string msg = "Added successfully!";
                        //lblmsg.Text = "<div class='box box-success box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";

                    }
                    else
                    {
                        Label lblmsg = (Label)Master.FindControl("lblmsg");
                        string msg = " Data added, image adding failed !";
                        lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
               
                    }
                    ds1.Dispose();
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
        txt_des.Text = "";
        txt_diso.Text = "";

    }


    private void coverimagesave(string eid)
    {
        string image = "";

        if (fu_img1.HasFile)
        {
            string extension = System.IO.Path.GetExtension(fu_img1.FileName);
            string dir_path = Server.MapPath("../uploads/album/" + e_id + "/");
            if (!System.IO.Directory.Exists(dir_path))
                System.IO.Directory.CreateDirectory(dir_path);

            HttpPostedFile file = fu_img1.PostedFile;
            int max = 500, min = 300;

            saveimage("../uploads/album/" + e_id + "/" + e_id + "_cover" + extension, file,max,min);

            image += " cover_photo='"+ e_id + "_cover" + extension + "'";
        }
        
        if (image != "")
        {
            querry = " UPDATE tbl_album SET " + image + " WHERE id=" + eid;
            int c = cc.Insert(querry);
        }
    }

    private string getclientIP()
    {
        string sIPAddress = null;
        sIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(sIPAddress))
            sIPAddress = Request.ServerVariables["REMOTE_ADDR"];

        return sIPAddress;
    }


    public void saveimage(string path, HttpPostedFile file,int max,int min)
    {
        if (file.ContentLength > 0)
        {
            string ext = System.IO.Path.GetExtension(file.FileName);

            if (new string[] { ".png", ".jpg", ".jpeg", ".gif" }.Contains(ext.ToLower()))
            {
                //Load the image into Bitmap Object
                Bitmap uploadedImage = new Bitmap(file.InputStream);

                int maxWidth = max;
                int maxHeight = min;

                //Resize the image
                Bitmap resizedImage = GetScaledPicture(uploadedImage, maxWidth, maxHeight);
                resizedImage.Save(Server.MapPath(path), uploadedImage.RawFormat);
            }
        }
    }


    protected Bitmap GetScaledPicture(Bitmap source, int maxWidth, int maxHeight)
    {
        int width, height;
        float aspectRatio = (float)source.Width / (float)source.Height;

        if ((maxHeight > 0) && (maxWidth > 0))
        {
            if ((source.Width < maxWidth) && (source.Height < maxHeight))
            {
                //Return unchanged image
                return source;
            }
            else if (aspectRatio > 1)
            {
                // Calculated width and height,
                // and recalcuate if the height exceeds maxHeight
                width = maxWidth;
                height = (int)(width / aspectRatio);
                if (height > maxHeight)
                {
                    height = maxHeight;
                    width = (int)(height * aspectRatio);
                }
            }
            else
            {
                // Calculated width and height,
                // and recalcuate if the width exceeds maxWidth
                height = maxHeight;
                width = (int)(height * aspectRatio);
                if (width > maxWidth)
                {
                    width = maxWidth;
                    height = (int)(width / aspectRatio);
                }
            }
        }
        else if ((maxHeight == 0) && (source.Width > maxWidth))
        {
            // If MaxHeight is not provided (unlimited), and
            // the source width exceeds maxWidth,
            // then recalculate height
            width = maxWidth;
            height = (int)(width / aspectRatio);
        }
        else if ((maxWidth == 0) && (source.Height > maxHeight))
        {
            // If MaxWidth is not provided (unlimited), and the
            // source height exceeds maxHeight, then
            // recalculate width
            height = maxHeight;
            width = (int)(height * aspectRatio);
        }
        else
        {
            //Return unchanged image
            return source;
        }

        Bitmap newImage = GetResizedImage(source, width, height);
        return newImage;
    }

    protected Bitmap GetResizedImage(Bitmap source, int width, int height)
    {
        //This function creates the thumbnail image.
        //The logic is to create a blank image and to
        // draw the source image onto it

        Bitmap thumb = new Bitmap(width, height);
        Graphics gr = Graphics.FromImage(thumb);

        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
        gr.SmoothingMode = SmoothingMode.HighQuality;
        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
        gr.CompositingQuality = CompositingQuality.HighQuality;

        gr.DrawImage(source, 0, 0, width, height);
        return thumb;
    }
}