using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class about : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
        lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Mission & Vision</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li >About </li><li class='active'>Mission & Vision</li></ul></div></div>";

    }
}