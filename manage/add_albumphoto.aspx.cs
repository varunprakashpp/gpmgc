using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

public partial class add_category : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, condition, id, e_id, date, addedby, ip, type;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            Label lblheading = (Label)Master.FindControl("lblheading");
            lblheading.Text = "Gallery photos";

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

                    display();
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


    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        string ip = getclientIP();
        
        try
        {
            if (fu_img.HasFiles)
            {
                string dir_path = Server.MapPath("../uploads/album/" + e_id + "/photos/");
                if (!System.IO.Directory.Exists(dir_path))
                    System.IO.Directory.CreateDirectory(dir_path);

                querry = "";
                foreach (HttpPostedFile postedFile in fu_img.PostedFiles)
                {
                    string fileName = Path.GetFileName(postedFile.FileName).Replace(",", "_");
                    if (File.Exists(dir_path + fileName))
                    {
                        fileName = Path.GetFileName(postedFile.FileName).Replace(",", "_") + "(1)" + Path.GetExtension(postedFile.FileName);
                    }
                    postedFile.SaveAs(dir_path + fileName);

                    querry += " INSERT INTO tbl_album_photos (album_id, heading, photo, display_order, status, addedon, addedby, addedtype,ip )";
                    querry += " VALUES('" + e_id + "','','" + fileName + "','100','1','" + date + "','" + addedby + "','" + type + "','" + ip + "')";
                }
                if (querry != "")
                {
                    int q = cc.Insert(querry);
                    if (q > 0)
                    {
                        Label lblmsg = (Label)Master.FindControl("lblmsg");
                        string msg = "Added successfully!";
                        lblmsg.Text = "<div class='box box-success box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";

                        display();

                    }
                }

            }
            else
            {
                Label lblmsg = (Label)Master.FindControl("lblmsg");
                string msg = "No files found !";
                lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";

            }
        }
        catch (Exception t)
        {
            Response.Write(t);
        }
    }


    public void display()
    {
        querry = " SELECT id, album_id, heading, photo, display_order, status FROM tbl_album_photos";
        querry += " WHERE album_id='" + e_id + "' ORDER BY cast(display_order as int) DESC";
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
            Label limage = (Label)e.Row.FindControl("limage");

            string img = "../design/dist/img/no_img.jpg";
            if (limage.Text != "")
            {
                string path = "../uploads/album/" + e_id + "/photos/" + limage.Text;
                if (File.Exists(Server.MapPath(path)))
                    img = path;
            }
            limage.Text = "<a href='" + img + "' target='_blank' title='Image'><img src='" + img + "' height='100px' weight='100px'/></a>";

            
        }
    }


    protected void btnDlt_Click(object sender, EventArgs e)
    {
        GridViewRow GrdRow = (GridViewRow)((Button)sender).NamingContainer;

        HiddenField hfid = (HiddenField)GrdRow.Cells[4].FindControl("hfid");
        HiddenField photo = (HiddenField)GrdRow.Cells[1].FindControl("photo");
        Label lproduct = (Label)GrdRow.Cells[1].FindControl("lproduct");
        Label lqty = (Label)GrdRow.Cells[1].FindControl("lqty");

        querry = "DELETE FROM tbl_album_photos WHERE album_id='" + e_id + "' AND id=" + hfid.Value;
        int c = cc.Insert(querry);
        if (c > 0)
        {
            string newpath = Server.MapPath("../uploads/album/" + e_id + "/photos/" + photo);
            if (System.IO.Directory.Exists(newpath))
                if (File.Exists(newpath))
                    File.Delete(newpath);

            Response.Write("<script>alert('Deleted successfully');window.location.assign('add_albumphoto.aspx?id=" + Request.QueryString["id"] + "');</script>");
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

    protected void btn_Savall_Click(object sender, EventArgs e)
    {
        querry = "";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HiddenField hfid = (HiddenField)GridView1.Rows[i].FindControl("hfid");
            HiddenField hfhead = (HiddenField)GridView1.Rows[i].FindControl("hfhead");
            HiddenField hfdiso = (HiddenField)GridView1.Rows[i].FindControl("hfdiso");

            TextBox txthead = (TextBox)GridView1.Rows[i].FindControl("txthead");
            TextBox txtdiso = (TextBox)GridView1.Rows[i].FindControl("txtdiso");

            if (txthead.Text != hfhead.Value || txtdiso.Text != hfdiso.Value )
            {
                querry += " UPDATE tbl_album_photos SET heading='" + txthead.Text + "',display_order='" + txtdiso.Text + "'";
                querry += " WHERE id=" + hfid.Value;
            }

            if (querry != "")
            {
                int c = cc.Insert(querry);
                if (c > 0)
                    Response.Write("<script>alert('Updated successfully');window.location.assign('view_album.aspx');</script>");
            }

            Response.Write("<script>alert('Updated successfully');window.location.assign('view_album.aspx');</script>");
        }
    }


}