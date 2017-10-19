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
    static string querry, newstype, title;
    private int pagesize = 9;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            newstype = Request.QueryString["type"];
            if (newstype == "news")
                title = "News";
            else if (newstype == "flashnews")
                title = "Flash news";
            Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
            lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>" + title + "</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li class='active'>" + title + "</li></ul></div></div>";

            if (!IsPostBack)
            {
                display(1);
            }
        }
        else
            Response.Redirect("Default.aspx");
    }

    public void display(int pageIndex)
    {
        querry = " SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT 100)) AS RowNumber,id,heading,addedon,description";
        querry += " ,(CASE WHEN ISNULL(photo1, '') = '' THEN (CASE WHEN ISNULL(photo2, '') = '' THEN (CASE WHEN ISNULL(photo3, '') = '' THEN (CASE WHEN ISNULL(photo4, '') = '' THEN '' ELSE photo4 END) ELSE photo3 END) ELSE photo2 END) ELSE photo1 END) AS photo";
        querry += " INTO #Results FROM   tbl_news WHERE flag='" + newstype + "'  AND status='1'";
        querry += " ORDER BY CAST(addedon AS date) DESC";

        querry += " DECLARE @PageCount INT";
        querry += " DECLARE @RecordCount INT";
        querry += " SELECT @RecordCount = COUNT(*) FROM #Results";
        querry += " SET @PageCount = @RecordCount ";
        querry += " SELECT @PageCount,* FROM #Results";
        querry += " WHERE RowNumber BETWEEN(" + pageIndex + " -1) * " + pagesize + " + 1 AND(((" + pageIndex + " -1) * " + pagesize + " + 1) + " + pagesize + ") - 1";

        querry += " DROP TABLE #Results";

        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.PopulatePager(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]), pageIndex);
            rptCustomers.DataSource = ds;
            rptCustomers.DataBind();
        }
        ds.Dispose();
    }

    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pagesize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.display(pageIndex);
    }

    protected void rptCustomers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lcontent = (Label)e.Item.FindControl("lcontent");
            HiddenField hfhead = (HiddenField)e.Item.FindControl("hfhead");
            HiddenField hfdate = (HiddenField)e.Item.FindControl("hfdate");
            HiddenField hfdesc = (HiddenField)e.Item.FindControl("hfdesc");
            HiddenField hfphoto = (HiddenField)e.Item.FindControl("hfphoto");

            string head = hfhead.Value, cont = hfdesc.Value;
            string photo = "img/sections/no_img.png", adate = Convert.ToDateTime(hfdate.Value).ToString("dd.MMM.yyyy");
            string path = "news_more.aspx?id=" + EncodeDecode.base64Encode(lcontent.Text) + "&type=news";

            cont = EncodeDecode.base64Decode(cont);
            if (cont.Length > 131)
                cont = cont.Substring(0, 131) + "...";


            if (hfphoto.Value != "")
            {
                string path1 = "uploads/news/" + lcontent.Text + "/" + hfphoto.Value;
                if (File.Exists(Server.MapPath(path1)))
                    photo = path1;
            }

            lcontent.Text = "";
            lcontent.Text += " <div class='col-sm-4'><div class='news_blk'> ";
            lcontent.Text += " <div style='width:100%;height:185px;overflow: hidden;'><p class='text-center'><a href='" + photo + "' class='opacity' ><img src='" + photo + "' width='370' height='185' alt=''></a></p></div> ";
            lcontent.Text += " <h5 class='bottom-margin-10'><a href='" + path + "' class='black'>" + head + "</a></h5> ";
            lcontent.Text += " <p class='date'>" + adate + "</p> ";
            lcontent.Text += " <p class='news_desc'>" + cont + "</p> ";
            lcontent.Text += " </div></div> ";
        }
    }

}