using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class delete_item : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    string id = safesql.SafeSqlLiterall(Request.QueryString["id"].ToString(), 2);


                    if (Request.QueryString["type"] == "panchayathabout")
                    {
                        id = EncodeDecode.base64Decode(id);
                        string img = EncodeDecode.base64Decode(Request.QueryString["img"]);

                        string querry = " UPDATE tbl_panchayath_about SET";
                        querry += " images = SUBSTRING(REPLACE(',' + images, '," + img + ",', ','), 2, LEN(REPLACE(',' + images, '," + img + ",', ',')))";
                        querry += " WHERE id=" + id;
                        int q1 = cc.Insert(querry);
                           
                        try
                        {
                            string spath = Server.MapPath("../uploads/panchayath_about/" + id + "/" + img);
                            if (File.Exists(spath))
                            {
                                File.Delete(spath);
                            }
                        }
                        catch (Exception rr)
                        {
                        }

                        Response.Write("<script>alert('Deleted successfully!');location.replace('add_panchayath_about.aspx?id=" + Request.QueryString["id"] + "&type=edit');</script>");
                    }


                }
                else
                {
                    Response.Redirect("dashboard.aspx");
                }
            }
        }
        catch (Exception t)
        {
            Response.Write(t);
        }
    }
}