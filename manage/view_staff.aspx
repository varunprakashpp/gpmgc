<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="view_staff.aspx.cs" Inherits="view_customers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style>
.grid1 tbody tr:nth-child(1)
{
     border: 2px solid #f4f4f4;
     color: #501313;
      box-sizing: border-box;
}
.grid1 table tbody tr:nth-child(1),.pagger tbody tr:nth-child(1)
{
    display:block;
}
.grid1 table tbody tr td
{
    padding:11px;
}
.grid1 box-body
{
    padding:0px 10px;
}

.grid1 td, th {
    padding: 5px;
    border: 2px solid #f4f4f4;
}
.pagger table
{margin-left:10px;
 margin-top:10px;
 margin-bottom:10px;
    }
.pagger table tbody tr td
{
    background-color:#f9f9f9;
    text-align:center;
    width:30px;
    height:30px;
    border:1px solid #dfdfdf;
    margin-top:5px;
   
    
}
.pagger table tbody tr td span
{
    background-color:#337ab7;
    width:20px;
    height:30px;
    color :#fff;
    padding:5px 12px;
   
    
  
}
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<section class="content">
<div class="row">

    <div class="col-xs-12">
        
        <section class="content">
      <div class="row">
		  
		<div class="box">

            <div class="box-header"><h3 class="box-title"> Staff</h3></div>

            <div class="box-body"><div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server" CssClass="grid1" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" style="width:100%;" GridLines="None" AllowPaging="True" 
                    onpageindexchanging="GridView1_PageIndexChanging" PageSize="10">
                <Columns>
                    <asp:TemplateField HeaderText="Slno">
                        <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %> 
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                                    <asp:Label ID="l_image1" Text='<%# Eval("photo") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Staff details">
                        <ItemTemplate>
                                    <asp:Label ID="l_name" Text='<%# Eval("name") %>' runat="server"></asp:Label>
                                    <asp:Label ID="l_gender" Text='<%# Eval("gender") %>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="l_cat" Text='<%# Eval("category") %>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="l_status" Text='<%# Eval("status") %>' runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Work details">
                        <ItemTemplate>
                                    <asp:Label ID="l_dep" Text='<%# Eval("department") %>' runat="server"></asp:Label>
                                    <asp:Label ID="l_des" Text='<%# Eval("designation") %>' runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Contact info">
                        <ItemTemplate>
                                    <i class="fa fa-envelope" aria-hidden="true"></i>&emsp;<asp:Label ID="l_email" Text='<%# Eval("email") %>' runat="server"></asp:Label>
                                    <br /><i class="fa fa-mobile" aria-hidden="true"></i>&emsp;<asp:Label ID="l_mobile" Text='<%# Eval("mobile") %>' runat="server" ></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                                    <asp:Label ID="l_action" Text='<%# Eval("id") %>' runat="server" ></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>


                </Columns>
                <PagerStyle CssClass="pagger" />
            </asp:GridView>
            </div></div>

        </div>    <!-----------orders---------------------->

       </div>
    </section>

    </div>

</div>
</section>

</asp:Content>

