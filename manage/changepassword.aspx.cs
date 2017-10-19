using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

public partial class add_employee : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, old, pass, confirm;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Label lbl_phead = (Label)Master.FindControl("lblheading");

            lbl_phead.Text = "Change Password";
            txt_con.Attributes.Add("class", "form-control pull-right validate[required,equals[" + txt_new.ClientID + "]");


        }
        catch (Exception t)
        {
            Response.Write(t);
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        old = safesql.SafeSqlLiterall(txt_old.Text, 2);
        pass = safesql.SafeSqlLiterall(txt_new.Text, 2);
        confirm = safesql.SafeSqlLiterall(txt_con.Text, 2);


        string query = "select id,username,password from tbl_login";
        string condition = " and password='" + old + "' and username='" + Request.Cookies["username"].Value + "' ";
        DataSet ds = cc.select(query, condition);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string query3 = "update tbl_login set password='" + pass + "'where username='" + Request.Cookies["username"].Value + "'";
            int a = cc.Insert(query3);


            Label lblmsg = (Label)Master.FindControl("lblmsg");
            string msg = "Password Updated Successfully!!  !!!!";
            lblmsg.Text = "<div class='box box-success box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";

            txt_old.Text = "";
            txt_new.Text = "";
            txt_con.Text = "";

        }
        else
        {

            Label lblmsg = (Label)Master.FindControl("lblmsg");
            string msg = "The Old Password is Worng!!!!";
            lblmsg.Text = "<div class='box box-danger box-solid'><div class='box-header with-border'><h3 class='box-title'>" + msg + "</h3><div class='box-tools pull-right'><button type='button' class='btn btn-box-tool' data-widget='remove'><i class='fa fa-times'></i></button></div></div></div>";
            txt_old.Text = "";
            txt_new.Text = "";
            txt_con.Text = "";
        }
    }

}