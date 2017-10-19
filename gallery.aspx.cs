using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class about : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry;
    private int pagesize = 16;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl_mainpagehead = (Label)Master.FindControl("lbl_mainpagehead");
        lbl_mainpagehead.Text = "<div class='container'><h1 class='title'>Gallery</h1></div><div class='breadcrumb-box'><div class='container'><ul class='breadcrumb'><li><a href='Default.aspx'>Home</a></li><li class='active'>Gallery</li></ul></div></div>";

        display(1);
        
    }

    public void display(int pageIndex)
    {
        querry = " SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT 100)) AS RowNumber,id,heading,cover_photo";
        querry += " INTO #Results FROM   tbl_album WHERE flag='album'  AND status='1'";
        querry += " ORDER BY CAST(display_order AS int) ASC";

        querry += " DECLARE @PageCount INT";
        querry += " DECLARE @RecordCount INT";
        querry += " SELECT @RecordCount = COUNT(*) FROM #Results";
        querry += " SET @PageCount = @RecordCount ";
        querry += " SELECT @PageCount,* FROM #Results";
        querry += " WHERE RowNumber BETWEEN(" + pageIndex + " -1) * " + pagesize + " + 1 AND(((" + pageIndex + " -1) * " + pagesize + " + 1) + " + pagesize + ") - 1";

        querry += " DROP TABLE #Results";

        DataSet ds = cc.joinselect(querry);

        this.PopulatePager(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]), pageIndex);
        rptCustomers.DataSource = ds;
        rptCustomers.DataBind();

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
            Label lblcont = (Label)e.Item.FindControl("lblcont");
            HiddenField hfid = (HiddenField)e.Item.FindControl("hfid");
            HiddenField hfhead = (HiddenField)e.Item.FindControl("hfhead");
            HiddenField hfcphoto = (HiddenField)e.Item.FindControl("hfcphoto");

            string cphoto = "img/sections/no_img_ach.png";
            if (hfcphoto.Value != "")
            {
                string path = "uploads/album/" + hfid.Value + "/" + hfcphoto.Value;
                if (File.Exists(Server.MapPath(path)))
                    cphoto = path;
            }

            lblcont.Text = " <div class='grid-item '><div style='width:100%;height:198px;overflow: hidden;'> ";
            lblcont.Text += " <img src='" + cphoto + "' alt='Gallery' class='img-responsive' /> ";
            lblcont.Text += " <div class='img-overlay'></div> ";
            lblcont.Text += " <div class='figcaption'><div class='caption-block'><h4 class='glr_txt'>" + hfhead.Value + "</h4></div> ";
            lblcont.Text += " <a href='gallery_more.aspx?id=" + EncodeDecode.base64Encode(hfid.Value) + "'><i class='fa fa-link'></i></a></div> ";
            lblcont.Text += " </div> </div>";
        }
    }
}