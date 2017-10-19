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
            lblheading.Text = "Downloads";

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
                        querry = "DELETE FROM tbl_downloads WHERE id=" + e_id;
                        int c = cc.Insert(querry);
                        if (c > 0)
                        {
                            try
                            {
                                System.IO.DirectoryInfo dii = new DirectoryInfo(Server.MapPath("../uploads/downloads/" + e_id + "/"));
                                foreach (FileInfo file in dii.GetFiles())
                                {
                                    file.Delete();
                                }
                                dii.Delete();
                            }
                            catch (Exception rr)
                            {
                            }
                            Response.Write("<script>alert('Deleted successfully');window.location.assign('downloads.aspx');</script>");
                        }
                        else
                            Response.Write("<script>alert('Error in deletion !!');window.location.assign('downloads.aspx');</script>");
                    }
                    else
                    {
                        assign();
                    }
                }
                else
                {
                    fu_rpt.Attributes.Add("class", "form-control pull-right validate[required]");
                    lblink.Text = "*";
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
        querry = "select  id,heading,display_order,status,uploads from tbl_downloads where flag='downloads' and  id=" + e_id ;
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_head.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            txt_diso.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();

            string rpt = ds.Tables[0].Rows[0].ItemArray[4].ToString(),rptpath="../design/dist/img/no_img.jpg";

            if (rpt != "")
            {
                string path = "../uploads/downloads/" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + rpt;
                if (File.Exists(Server.MapPath(path)))
                {
                    if (rpt.Split('.').Last() == "pdf")
                        rptpath = "../design/dist/img/file/pdf.png";
                    else if (rpt.Split('.').Last() == "doc" || path.Split('.').Last() == "docx")
                        rptpath = "../design/dist/img/file/doc.png";
                    else if (rpt.Split('.').Last() == "rar")
                        rptpath = "../design/dist/img/file/rar.png";
                    else if (rpt.Split('.').Last() == "zip")
                        rptpath = "../design/dist/img/file/zip.png";
                    else
                        rptpath = "../design/dist/img/file/no_img.png";
                    
                    rpt = path;
                }
            }
            lbl_rpt.Text = "<a href='" + rpt + "' title='photo1' target='_blank'><img src='" + rptpath + "' width='80px'/></a><br/>";

            rbl_stat.Enabled = true;
            rbl_stat.SelectedValue = ds.Tables[0].Rows[0].ItemArray[3].ToString();

            Button1.Text = "Update";
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
                querry = "update tbl_downloads set ";
                querry += " heading='" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "'";
                querry += ",display_order='" + diso + "'";
                querry += ",status='" + safesql.SafeSqlLiterall(rbl_stat.SelectedValue.Replace("'", "`"), 2) + "'";
                querry += ",modifiedby='" + addedby + "'";
                querry += ",modifiedon='" + date + "'";
                querry += ",modifiedtype='"+ type +"'";
                querry += " where id=" + e_id ;
                int q = cc.Insert(querry);

                if (q > 0)
                {
                    if (fu_rpt.HasFile )
                        rptsave(e_id);

                    Response.Write("<script>alert('Updated successfully');window.location.assign('downloads.aspx');</script>");
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

                querry = "insert into tbl_downloads (heading, display_order, status, flag, addedby, addedon, addedtype, ip) ";
                querry += " values('" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "','" + diso + "','1','downloads'";
                querry += " ,'" + addedby + "','" + date + "','"+ type +"','"+ip+"')";
                int q = cc.Insert(querry);

                if (q > 0)
                {
                    querry = "select max(id) from tbl_downloads where heading='" + safesql.SafeSqlLiterall(txt_head.Text.Replace("'", "`"), 2) + "' and addedby='" + addedby + "' and addedtype='" + type + "' and addedon='" + date + "' and flag='downloads' ";
                    DataSet ds1 = cc.joinselect(querry);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        e_id = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                        if (fu_rpt.HasFile )
                            rptsave(e_id);
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
                        string msg = " Data added, File adding failed !";
                        lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
               
                    }
                    ds1.Dispose();
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

    private void rptsave(string eid)
    {
        if (fu_rpt.HasFile)
        {
            string dir_path = Server.MapPath("../uploads/downloads/" + e_id + "/");
            if (!System.IO.Directory.Exists(dir_path))
                System.IO.Directory.CreateDirectory(dir_path);

            fu_rpt.SaveAs(Server.MapPath("../uploads/downloads/" + e_id + "/" + fu_rpt.FileName));

            querry = " UPDATE tbl_downloads SET uploads='" + fu_rpt.FileName + "' WHERE id=" + eid;
            int c = cc.Insert(querry);
        }
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


    public void display()
    {
        querry = " SELECT id,heading,display_order,status, uploads FROM tbl_downloads WHERE flag='downloads'  ORDER BY CAST(display_order AS int) ASC";
        DataSet ds = cc.joinselect(querry);
        lbl_tablebody.Text = "";

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string rpt = ds.Tables[0].Rows[i].ItemArray[4].ToString(), rptpath = "../design/dist/img/no_img.jpg",status="";
                if (rpt != "")
                {
                    string path = "../uploads/downloads/" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "/" + rpt;
                    if (File.Exists(Server.MapPath(path)))
                    {
                        if (rpt.Split('.').Last() == "pdf")
                            rptpath = "../design/dist/img/file/pdf.png";
                        else if (rpt.Split('.').Last() == "doc" || path.Split('.').Last() == "docx")
                            rptpath = "../design/dist/img/file/doc.png";
                        else if (rpt.Split('.').Last() == "rar")
                            rptpath = "../design/dist/img/file/rar.png";
                        else if (rpt.Split('.').Last() == "zip")
                            rptpath = "../design/dist/img/file/zip.png";
                        else
                            rptpath = "../design/dist/img/file/no_img.png";

                        rpt = path;
                    }
                }

                if (ds.Tables[0].Rows[i].ItemArray[3].ToString() == "1")
                    status = "<font color='green'><i class='fa fa-check-circle' ></i>&emsp;Active</font>";
                else
                    status = "<font color='red'><i class='fa fa-times-circle' ></i>&emsp;Inactive</font>";

                lbl_tablebody.Text += "<tr>  <td>" + (i + 1) + "</td>";
                lbl_tablebody.Text += "<td>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "<br/>" + status + "</td>";
                lbl_tablebody.Text += "<td>" + ds.Tables[0].Rows[i].ItemArray[2].ToString() + "</td>";
                lbl_tablebody.Text += "<td><a href='" + rpt + "' title='photo1' target='_blank'><img src='" + rptpath + "' width='45px'/></a></td>";
                lbl_tablebody.Text += "<td>";
                lbl_tablebody.Text += "<a href='downloads.aspx?id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&type=edit' title='Edit' class='btn btn-warning' style='margin:2px;'><i class='fa fa-edit'></i></a>";
                lbl_tablebody.Text += "<a href='downloads.aspx?id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "&type=delete' title='Delete' class='btn btn-danger' onclick=\"return confirm('Are you sure ? ')\"  style='margin:2px;'><i class='fa fa-trash'></i></a>";
                lbl_tablebody.Text += "</td>  </tr>";
            }
        }
        ds.Dispose();
    }
}