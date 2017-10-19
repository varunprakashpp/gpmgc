using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MasterPage : System.Web.UI.MasterPage
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            menu();
        }
    }

    public void menu()
    {
        string academic = "";
        querry = " select  id,name from tbl_basics where flag='academiccells' order by CAST(display_order AS int) ASC ";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            academic += " <li class=''><a href='#' class='has-submenu'>Academic<span class='sub-arrow'>...</span></a> ";
            academic += " <ul class='dropdown-menu'> ";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                academic += " <li><a href='academicnews.aspx?type=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[1].ToString()) + "&id=" + EncodeDecode.base64Encode(ds.Tables[0].Rows[i].ItemArray[0].ToString()) + "'>" + ds.Tables[0].Rows[i].ItemArray[1].ToString() + "</a></li> ";
            }
            academic += " </ul></li> ";
        }
        ds.Dispose();



        lblmenu.Text += " <ul class='nav navbar-nav sm' data-smartmenus-id='15057129074183728'> ";
        lblmenu.Text += " <li class=''><a href='Default.aspx' class=''>Home</a></li> ";

        lblmenu.Text += " <li class=''><a href='#' class='has-submenu'>About<span class='sub-arrow'>...</span></a> ";
        lblmenu.Text += " <ul class='dropdown-menu'> ";
        lblmenu.Text += " <li><a href='about.aspx'>About GPMGC</a></li> ";
        lblmenu.Text += " <li><a href='vision.aspx'>Mission &amp; Vision</a></li> ";
        lblmenu.Text += " <li><a href='achievements.aspx'>Achievement</a></li> ";
        lblmenu.Text += " </ul></li> ";

        lblmenu.Text += " <li class=''><a href='#' class='has-submenu'>Administration<span class='sub-arrow'>...</span></a> ";
        lblmenu.Text += " <ul class='dropdown-menu'> ";
        lblmenu.Text += " <li><a href='teaching.aspx?type=teaching'>Teaching</a></li> ";
        lblmenu.Text += " <li><a href='teaching.aspx?type=nonteaching'>Non Teaching</a></li> ";
        lblmenu.Text += " </ul></li> ";

        lblmenu.Text += " <li class=''><a href='departments.aspx' class=''>Department</a></li> ";

        lblmenu.Text += " <li class=''><a href='#' class='has-submenu'>Admission<span class='sub-arrow'>...</span></a> ";
        lblmenu.Text += " <ul class='dropdown-menu'> ";
        lblmenu.Text += " <li><a href='procedure.aspx'>Procedures</a></li> ";
        lblmenu.Text += " <li><a href='fees.aspx'>Fees</a></li> ";
        lblmenu.Text += " <li><a href='rules.aspx'>Rules</a></li> ";
        lblmenu.Text += " </ul></li> ";

        lblmenu.Text += academic ;

        lblmenu.Text += " <li class=''><a href='projects.aspx' class=''>Research Projects</a></li> ";
        lblmenu.Text += " <li class=''><a href='gallery.aspx' class=''>Gallery</a></li> ";
        lblmenu.Text += " <li class=''><a href='news.aspx?type=news' class=''>News</a></li> ";
        lblmenu.Text += " <li class=''><a href='downloads.aspx' class=''>Downloads</a></li> ";
        lblmenu.Text += " <li class=''><a href='contact.aspx' class=''>Contact Us</a></li> ";

        lblmenu.Text += " </ul> ";
    }
}
