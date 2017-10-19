<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="downloads.aspx.cs" Inherits="news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
    .pagger table {
    margin-top: 14px;
}
.pagger td a
{
    padding:4px 10px;
    background-color:rgb(209, 169, 35);
    color:#fff;
}
.pagger td span
{
    padding:4px 10px;
    background-color:#ec0707;
    color:#fff;
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



     <section id="about-us" class="page-section">
            <div class="container">
                <div class="col-md-10 col-lg-offset-1 download_wrap">
                <ul>
                  <asp:GridView ID="GridView1" runat="server" CssClass="grid1" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" style="width:100%;" GridLines="None" AllowPaging="True" 
                    onpageindexchanging="GridView1_PageIndexChanging" PageSize="10" ShowHeader="false">
                    <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                        <!------------------------------------> 
                        <li>
                            <asp:Label ID="lbldata" runat="server" Text='<%# Eval("heading") %>'></asp:Label>
                            <asp:HiddenField ID="hfup" runat="server" Value='<%# Eval("uploads") %>'></asp:HiddenField>
                            <asp:HiddenField ID="hfid" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                        </li>
                        <!------------------------------------>
                        </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                <PagerStyle CssClass="pagger" />
            </asp:GridView>
                </ul>
                </div>
            </div>
        </section>

</asp:Content>

