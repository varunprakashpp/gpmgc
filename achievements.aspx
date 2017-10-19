<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="achievements.aspx.cs" Inherits="news" %>

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



     <section id="meet-the-team" class="page-section">
            <div class="container">
                <div class="grid_body">
                  <asp:GridView ID="GridView1" runat="server" CssClass="grid1" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" style="width:100%;" GridLines="None" AllowPaging="True" 
                    onpageindexchanging="GridView1_PageIndexChanging" PageSize="5" ShowHeader="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                        <!------------------------------------> 
                        <div class="row">
                            <div class="col-sm-4 col-md-4">
                                    <asp:Label ID="lblimage" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hfimg1" runat="server"  Value='<%# Eval("photo1") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfimg2" runat="server" Value='<%# Eval("photo2") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfimg3" runat="server" Value='<%# Eval("photo3") %>'></asp:HiddenField>
                            </div>
                            <div class="col-sm-8 col-md-8">
                                <asp:Label ID="lbldata" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfhead" runat="server" Value='<%# Eval("heading") %>'></asp:HiddenField>
                                <asp:HiddenField ID="hfdes" runat="server" Value='<%# Eval("description") %>'></asp:HiddenField>
                                <asp:HiddenField ID="hfid" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                            </div>
                        </div>
                        <hr />
                <!------------------------------------>
                        </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagger" />
            </asp:GridView>
                </div>
            </div>
        </section>

</asp:Content>

