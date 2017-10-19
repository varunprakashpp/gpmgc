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
            lblheading.Text = "Staff" ;

            id = Request.Cookies["id"].Value;
            date = DateTime.Now.AddHours(5).AddMinutes(30).ToString();
            addedby = Request.Cookies["id"].Value;
            ip = getclientIP();
            type = "admin";

            if (!IsPostBack)
            {
                ddl_dept.Attributes.Add("class", "form-control pull-right validate[required]");
                binddepartment();
                txt_diso.Text = "100";
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

    private void binddepartment()
    {
        querry = "SELECT id,heading FROM tbl_news WHERE flag='departments' ORDER BY cast(display_order as int) ASC";
        DataSet ds1 = cc.joinselect(querry);
        ddl_dept.Items.Clear();
        if (ds1.Tables[0].Rows.Count > 0)
        {
            ddl_dept.DataSource = ds1;
            ddl_dept.DataTextField = "heading";
            ddl_dept.DataValueField = "id";
            ddl_dept.DataBind();
        }
        ddl_dept.Items.Insert(0, new ListItem("Select department", ""));
    }

    public void lockall()
    {
        txt_name.Attributes.Add("readonly", "readonly");
        txt_dept.Attributes.Add("readonly", "readonly");
        txt_email.Attributes.Add("readonly", "readonly");
        txt_des.Attributes.Add("readonly", "readonly");
        txt_diso.Attributes.Add("readonly", "readonly");
        txt_mobile.Attributes.Add("readonly", "readonly");
        divbutton.Style.Add("display", "none");
        pnl_img1.Visible = false;
        rbl_stat.Enabled = false;
        rbl_cat.Enabled = false;
        rbl_gender.Enabled = false;
        ddl_dept.Enabled = false;
    }

    public void assign()
    {
        querry = "select  id,name, designation, category, department, email, mobile, photo, display_order, status, gender from tbl_staff where id=" + e_id;
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_name.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            txt_des.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            rbl_cat.SelectedValue = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            if (ds.Tables[0].Rows[0].ItemArray[3].ToString() == "teaching")
            {
                ddl_dept.Attributes.Add("class", "form-control pull-right select2 validate[required]"); 
                txt_dept.Attributes.Add("class", "form-control pull-right ");
                ddl_dept.Style.Add("display", "block");
                txt_dept.Style.Add("display", "none");
                ddl_dept.Enabled = true;

                ddl_dept.SelectedValue = ds.Tables[0].Rows[0].ItemArray[4].ToString();
            }
            else
            {
                ddl_dept.Attributes.Add("class", "form-control pull-right select2 ");
                txt_dept.Attributes.Add("class", "form-control pull-right validate[required]");
                ddl_dept.Style.Add("display", "none");
                txt_dept.Style.Add("display", "block");
                ddl_dept.Enabled = false;

                txt_dept.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
            }

            txt_email.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
            txt_mobile.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
            txt_diso.Text = ds.Tables[0].Rows[0].ItemArray[8].ToString();
            
            rbl_stat.Enabled = true;
            rbl_stat.SelectedValue = ds.Tables[0].Rows[0].ItemArray[9].ToString();

            rbl_gender.SelectedValue = ds.Tables[0].Rows[0].ItemArray[10].ToString();


            string cphoto = ds.Tables[0].Rows[0].ItemArray[7].ToString();
            string src = "../design/dist/img/no_img.jpg";

            if (cphoto != "")
            {
                string path = "../uploads/staff/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + cphoto;
                if (File.Exists(Server.MapPath(path)))
                    cphoto = path;
                else
                    cphoto = src;
            }


            lbl_img1.Text = "<a href='" + cphoto + "' title='file' target='_blank'><img src='" + cphoto + "' width='80px'/></a><br/><br/>";
            
            Button1.Text = "Update";
            fu_img1.Attributes.Add("class", "");
        }
        ds.Dispose();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = getclientIP(), diso = "100";

        try
        {
            string ext = "",dept="",cat="",gender="";
            cat = safesql.SafeSqlLiterall(rbl_cat.SelectedValue, 2);
            if(cat=="teaching")
                dept = safesql.SafeSqlLiterall(ddl_dept.SelectedValue, 2);
            else
                dept = safesql.SafeSqlLiterall(txt_dept.Text, 2);

            gender = safesql.SafeSqlLiterall(rbl_gender.SelectedValue, 2);

            if (Request.QueryString.Count > 0)
            {
                ext = " and id<>" + e_id;
            }
            string query = "select id from tbl_staff where name='" + safesql.SafeSqlLiterall(txt_name.Text, 2) + "' AND category='" + cat + "'";
            query += " AND department='" + dept + "' AND designation='" + safesql.SafeSqlLiterall(txt_des.Text, 2) + "'"  + ext;
            DataSet ds = cc.joinselect(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label lblmsg = (Label)Master.FindControl("lblmsg");
                string msg = "Staff name already exists same category ,department or designation!! Try another name ";
                lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
                return;
            }


            if (txt_diso.Text != "")
                diso = safesql.SafeSqlLiterall(txt_diso.Text.Replace("'", "`"), 2);

            if (Request.QueryString.Count > 0)
            {
                querry = "update tbl_staff set ";
                querry += " name ='" + safesql.SafeSqlLiterall(txt_name.Text.Replace("'", "`"), 2) + "'";
                querry += ",designation='" + safesql.SafeSqlLiterall(txt_des.Text.Replace("'", "`"), 2) + "'";
                querry += ",category='" + cat + "'";
                querry += ",department='" + dept + "'";
                querry += ",gender='" + gender + "'";
                querry += ",email='" + safesql.SafeSqlLiterall(txt_email.Text.Replace("'", "`"), 2) + "'";
                querry += ",mobile='" + safesql.SafeSqlLiterall(txt_mobile.Text.Replace("'", "`"), 2) + "'";
                querry += ",display_order='" + diso + "'";
                querry += ",status='" + safesql.SafeSqlLiterall(rbl_stat.SelectedValue.Replace("'", "`"), 2) + "'";
                querry += ",modifiedby='" + id + "'";
                querry += ",modifiedon='" + date + "'";
                querry += ",modifiedtype='admin'";
                querry += " where id=" + e_id;
                int q = cc.Insert(querry);

                if (q > 0)
                {
                    if (fu_img1.HasFile)
                        coverimagesave(e_id);

                    Response.Write("<script>alert('Updated successfully');window.location.assign('view_staff.aspx');</script>");
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

                querry = "insert into tbl_staff (name, designation, category, department, email, mobile, display_order, status, gender,  addedby, addedon, addedtype, ip) ";
                querry += " values('" + safesql.SafeSqlLiterall(txt_name.Text.Replace("'", "`"), 2) + "'";
                querry += " ,'" + safesql.SafeSqlLiterall(txt_des.Text.Replace("'", "`"), 2) + "'";
                querry += " ,'" + cat + "','" + dept + "'";
                querry += " ,'" + safesql.SafeSqlLiterall(txt_email.Text.Replace("'", "`"), 2) + "','" + safesql.SafeSqlLiterall(txt_mobile.Text.Replace("'", "`"), 2) + "'";
                querry += " ,'" + diso + "','1','" + gender + "'";
                querry += " ,'" + id + "','" + date + "','admin','" + ip + "')";
                int q = cc.Insert(querry);

                if (q > 0)
                {
                    querry = " select max(id) from tbl_staff where name='" + safesql.SafeSqlLiterall(txt_name.Text, 2) + "' AND category='" + cat + "'";
                    querry += " AND department='" + dept + "' AND designation='" + safesql.SafeSqlLiterall(txt_des.Text, 2) + "' and addedby='" + id + "' and addedtype='admin' and addedon='" + date + "' ";

                    DataSet ds1 = cc.joinselect(querry);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        e_id = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                        if (fu_img1.HasFile)
                            coverimagesave(e_id);
                        clear();

                        Label lblmsg = (Label)Master.FindControl("lblmsg");
                        string msg = "Added successfully!";
                        lblmsg.Text = "<div class='box box-success box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";

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
        txt_name.Text = "";
        txt_dept.Text = "";
        txt_email.Text = "";
        txt_des.Text = "";
        txt_diso.Text = "";
        txt_mobile.Text = "";
        binddepartment();
    }


    private void coverimagesave(string eid)
    {
        string image = "";

        if (fu_img1.HasFile)
        {
            string extension = System.IO.Path.GetExtension(fu_img1.FileName);
            string dir_path = Server.MapPath("../uploads/staff/" + e_id + "/");
            if (!System.IO.Directory.Exists(dir_path))
                System.IO.Directory.CreateDirectory(dir_path);

            HttpPostedFile file = fu_img1.PostedFile;
            int max = 500, min = 300;

            saveimage("../uploads/staff/" + e_id + "/" + e_id + extension, file, max, min);

            image += " photo='" + e_id + extension + "'";
        }

        if (image != "")
        {
            querry = " UPDATE tbl_staff SET " + image + " WHERE id=" + eid;
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


    public void saveimage(string path, HttpPostedFile file, int max, int min)
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
    protected void rbl_cat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_cat.SelectedValue == "teaching")
        {
            ddl_dept.Style.Add("display", "block");
            txt_dept.Style.Add("display", "none");
            ddl_dept.Enabled = true;
        }
        else
        {
            ddl_dept.Style.Add("display", "none");
            txt_dept.Style.Add("display", "block");
            ddl_dept.Enabled = false;
        }
    }
}