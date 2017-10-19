<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="academicnews.aspx.cs" Inherits="news" %>

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
@media screen and (max-width: 767px)
{
.table-responsive>.table>tbody>tr>td, .table-responsive>.table>tbody>tr>th, .table-responsive>.table>tfoot>tr>td, .table-responsive>.table>tfoot>tr>th, .table-responsive>.table>thead>tr>td, .table-responsive>.table>thead>tr>th {
    white-space: inherit;
}
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 
      <section class="page-section">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12 col-md-8 post-list cms" data-animation="pulse">
                    <asp:GridView ID="GridView1" runat="server" CssClass="grid1" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" style="width:100%;" GridLines="None" AllowPaging="True" 
                    onpageindexchanging="GridView1_PageIndexChanging" PageSize="3" ShowHeader="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                        <!------------------------------------> 

                    	<div class="departmetnt_pro">
                        <div class="post-item">
                        	<asp:Label ID="lbldata" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfhead" runat="server" Value='<%# Eval("heading") %>'></asp:HiddenField>
                                <asp:HiddenField ID="hfdes" runat="server" Value='<%# Eval("description") %>'></asp:HiddenField>
                                <asp:HiddenField ID="hfid" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfimg1" runat="server"  Value='<%# Eval("photo1") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfimg2" runat="server" Value='<%# Eval("photo2") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfimg3" runat="server" Value='<%# Eval("photo3") %>'></asp:HiddenField>
                        </div>
                     	</div>
                        <!------------------------------------>
                        </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagger" />
                </asp:GridView>

                    </div>
                     <div class="col-md-4" data-animation="pulse">
                      	<asp:Label ID="lbldept" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </section>
		

</asp:Content>

