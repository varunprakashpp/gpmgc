using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class news : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    mail mm = new mail();
    static string querry, newstype, title, mail, cusmail, name, mob, content, body;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
        lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Contact Us</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li class='active'>Contact Us</li></ul></div></div>";

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        mail = "gpmgcm2@gmail.com";

        cusmail = safesql.SafeSqlLiterall(txt_email.Text.Replace("'", "`"), 2);
        name = safesql.SafeSqlLiterall(txt_name.Text.Replace("'", "`"), 2);
        mob = safesql.SafeSqlLiterall(txt_mob.Text.Replace("'", "`"), 2);
        content = safesql.SafeSqlLiterall(txt_feed.Text.Replace("'", "`"), 2);

        body = "NAME : " + name + "<br/> MOBILE : " + mob + "<br/> EMAIL : " + cusmail + "<br/><br/><p><b> FEEDBACK / ENQUIRY </b>:" + content + "</p> ";
        mm.sendMail(mail, "Feedback / Enquiry", body);

        txt_email.Text = "";
        txt_name.Text = "";
        txt_mob.Text = "";
        txt_feed.Text = "";

        Response.Write("<script>alert('Feedback or Enquiry submitted successfully');</script>");

    }

}