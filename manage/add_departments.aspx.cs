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
    static string querry, condition, id, e_id, date, addedby, ip, type, photo1, photo2, photo3;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            Label lblheading = (Label)Master.FindControl("lblheading");
            lblheading.Text = "Department";

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
        pnl_img2.Visible = false;
        pnl_img3.Visible = false;
        rbl_stat.Enabled = false;
    }

    public void assign()
    {
        querry = "select  id,heading,description,photo1,photo2,photo3,display_order,status from tbl_news where flag='departments' and  id=" + e_id ;
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_head.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            txt_des.Text = EncodeDecode.base64Decode( ds.Tables[0].Rows[0].ItemArray[2].ToString());
            txt_diso.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();

            photo1 = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            photo2 = ds.Tables[0].Rows[0].ItemArray[4].ToString();
            photo3 = ds.Tables[0].Rows[0].ItemArray[5].ToString();
            string icon = "../design/dist/img/no_img.jpg";

            if (photo1 != "")
            {
                string path = "../uploads/departments/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + photo1;
                if (File.Exists(Server.MapPath(path)))
                {
                    photo1 = path;
                    lbl_img1.Text = "<a href='" + photo1 + "' title='photo1' target='_blank'><img src='" + photo1 + "' width='80px'/></a>";
                    LinkButton1.Visible = true;
                }
            }
            if (photo2 != "")
            {
                string path = "../uploads/departments/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + photo2;
                if (File.Exists(Server.MapPath(path)))
                {
                    photo2 = path;
                    lbl_img2.Text = "<a href='" + photo2 + "' title='photo1' target='_blank'><img src='" + photo2 + "' width='80px'/></a>";
                    LinkButton2.Visible = true;
                }
            }
            if (photo3 != "")
            {
                string path = "../uploads/departments/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + photo3;
                if (File.Exists(Server.MapPath(path)))
                {
                    photo3 = path;
                    lbl_img3.Text = "<a href='" + photo3 + "' title='photo1' target='_blank'><img src='" + photo3 + "' width='80px'/></a>";
                    LinkButton3.Visible = true;
                }
            }

            rbl_stat.Enabled = true;
            rbl_stat.SelectedValue = ds.Tables[0].Rows[0].ItemArray[7].ToString();

            Button1.Text = "Update";
        }
        ds.Dispose();
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        querry = " UPDATE tbl_news SET photo1='' where flag='departments' and  id=" + e_id;
        int c = cc.Insert(querry);
        if (c > 0)
        {
            File.Delete(Server.MapPath(photo1));
            Response.Write("<script>alert('Deleted successfully');window.location.assign('add_departments.aspx?id=" + Request.QueryString["id"] + "&type=edit');</script>");
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        querry = " UPDATE tbl_news SET photo2='' where flag='departments' and  id=" + e_id;
        int c = cc.Insert(querry);
        if (c > 0)
        {
            File.Delete(Server.MapPath(photo2));
            Response.Write("<script>alert('Deleted successfully');window.location.assign('add_departments.aspx?id=" + Request.QueryString["id"] + "&type=edit');</script>");
        }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        querry = " UPDATE tbl_news SET photo3='' where flag='departments' and  id=" + e_id;
        int c = cc.Insert(querry);
        if (c > 0)
        {
            File.Delete(Server.MapPath(photo3));
            Response.Write("<script>alert('Deleted successfully');window.location.assign('add_departments.aspx?id=" + Request.QueryString["id"] + "&type=edit');</script>");
        }
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
                querry = "update tbl_news set ";
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
                    if (fu_img1.HasFile || fu_img2.HasFile || fu_img3.HasFile)
                        imagesave(e_id);

                    Response.Write("<script>alert('Updated successfully');window.location.assign('view_departments.aspx');</script>");
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

                querry = "insert into tbl_news (heading, description,display_order,status,flag,addedby,addedon,addedtype,ip) ";
                querry += " values('" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "'";
                querry += " ,'" + EncodeDecode.base64Encode(safesql.SafeSqlLiterall(txt_des.Text.Replace("'", "`"), 2)) + "'";
                querry += " ,'" + diso + "','1','departments'";
                querry += " ,'" + id + "','" + date + "','admin','"+ip+"')";
                int q = cc.Insert(querry);

                if (q > 0)
                {

                    querry = "select max(id) from tbl_news where heading='" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "' and description='" + EncodeDecode.base64Encode(safesql.SafeSqlLiterall(txt_des.Text.Replace("'", "`"), 2)) + "' and addedby='" + id + "' and addedtype='admin' and addedon='" + date + "' and flag='departments' ";
                    DataSet ds1 = cc.joinselect(querry);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        e_id = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                        if (fu_img1.HasFile || fu_img2.HasFile || fu_img3.HasFile)
                            imagesave(e_id);
                        clear();

                        Label lblmsg = (Label)Master.FindControl("lblmsg");
                        string msg = "Added successfully!";
                        lblmsg.Text = "<div class='box box-success box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";

                    }
                    else
                    {
                        Label lblmsg = (Label)Master.FindControl("lblmsg");
                        string msg = " Data added, Image adding failed !";
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

    private void imagesave(string eid)
    {
        string image = "";

        if (fu_img1.HasFile)
        {
            string extension = System.IO.Path.GetExtension(fu_img1.FileName);
            string dir_path = Server.MapPath("../uploads/departments/" + e_id + "/");
            if (!System.IO.Directory.Exists(dir_path))
                System.IO.Directory.CreateDirectory(dir_path);

            HttpPostedFile file = fu_img1.PostedFile;
            saveimage("../uploads/departments/" + e_id + "/photo1" + extension ,file);

            image += " photo1='photo1" + extension + "',";
        }
        if (fu_img2.HasFile)
        {
            string extension = System.IO.Path.GetExtension(fu_img2.FileName);
            string dir_path = Server.MapPath("../uploads/departments/" + e_id + "/");
            if (!System.IO.Directory.Exists(dir_path))
                System.IO.Directory.CreateDirectory(dir_path);

            HttpPostedFile file = fu_img2.PostedFile;
            saveimage("../uploads/departments/" + e_id + "/photo2" + extension, file);

            image += " photo2='photo2" + extension + "',";
        }
        if (fu_img3.HasFile)
        {
            string extension = System.IO.Path.GetExtension(fu_img3.FileName);
            string dir_path = Server.MapPath("../uploads/departments/" + e_id + "/");
            if (!System.IO.Directory.Exists(dir_path))
                System.IO.Directory.CreateDirectory(dir_path);

            HttpPostedFile file = fu_img3.PostedFile;
            saveimage("../uploads/departments/" + e_id + "/photo3" + extension, file);

            image += " photo3='photo3" + extension + "',";
        }

        if (image != "")
        {
            image = image.Remove(image.Length - 1, 1);
            querry = " UPDATE tbl_news SET "+image+" WHERE id="+eid;
            int c = cc.Insert(querry);
        }
    }
    
    public void clear()
    {
        txt_head.Text = "";
        txt_des.Text = "";
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


    public void saveimage(string path, HttpPostedFile file)
    {
        if (file.ContentLength > 0)
        {
            string ext = System.IO.Path.GetExtension(file.FileName);

            if (new string[] { ".png", ".jpg", "jpeg", "gif" }.Contains(ext.ToLower()))
            {
                //Load the image into Bitmap Object
                Bitmap uploadedImage = new Bitmap(file.InputStream);

                int maxWidth = 500;
                int maxHeight = 300;

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