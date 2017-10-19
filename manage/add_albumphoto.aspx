<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="add_albumphoto.aspx.cs" Inherits="add_category" %>

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

   <script>
       function displaylist() {
           var input = document.getElementById('<%=fu_img.ClientID %>'); 
           var output = document.getElementById('<%=lbl_files.ClientID %>');

           output.innerHTML = '<ul>';
           for (var i = 0; i < input.files.length; ++i) {
               output.innerHTML += '<li>' + input.files.item(i).name + '</li>';
           }
           output.innerHTML += '</ul>';
       }
  </script> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 
        
<section class="content" style="min-height:150px;">
    <div class="row">
    <div class="col-md-12">
        <div class="box box-primary search-job01">
        <div class="box-header with-border"><h3 class="box-title">Add & Update album photos</h3>   </div>

        <div class="nav-tabs-custom">
            <div class="tab-content ">

                <div class="col-md-12">
                 <asp:Panel ID="panelimage" runat="server">
                    <div class="col-md-6">
                        <div class="form-group">
                        <label><br />Upload images : </label>
                        <div >
                            <asp:FileUpload ID="fu_img" runat="server" AllowMultiple="true" onchange="javascript:displaylist()"></asp:FileUpload>
                            
                            <br /><asp:Label ID="lbl_files" runat="server" Text=""></asp:Label>
                        </div>
                        </div>
                    </div>

                    
                    <div class="col-md-12">
                        <div class="form-group">
                        <label><br />&nbsp;</label>
                        <div >
                        <asp:Button ID="Button1" runat="server" class="btn btn-success pull-right" OnClientClick="jQuery('#form1').validationEngine();" style="float:right;"
                                            Text="Save" onclick="ButtonSave_Click"/>
                        </div>
                    </div>
                   </div>
                 </asp:Panel>
                </div>

                <asp:Label ID="lbl_image" runat="server" Text=""></asp:Label>

           </div>
        </div>

        <div class="nav-tabs-custom"><div class="tab-content"><div class="post"><div class="user-block"></div></div></div></div>

        </div>
    </div>
    </div>
</section>


<section class="content">
    <div class="row">

    <div class="col-xs-12">
        
        <section class="content">
      <div class="row">
		  
		<div class="box">

            <div class="box-header"><h3 class="box-title"> Photos</h3></div>

            <div class="box-body"><div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server" CssClass="grid1" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" style="width:100%;" GridLines="None" AllowPaging="True" 
                    onpageindexchanging="GridView1_PageIndexChanging" PageSize="10" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="SL NO">
                        <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %> 
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                                    <asp:Label ID="limage" Text='<%# Eval("photo") %>' runat="server"></asp:Label>
                                    <asp:HiddenField ID="hfphoto" runat="server" Value='<%# Eval("photo") %>'></asp:HiddenField>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Heading">
                        <ItemTemplate>
                                    <asp:TextBox ID="txthead" class="form-control pull-right " runat="server" Text='<%# Eval("heading") %>' Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hfhead" runat="server" Value='<%# Eval("heading") %>'></asp:HiddenField>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Display order">
                        <ItemTemplate>
                                    <asp:TextBox ID="txtdiso" class="form-control pull-right " runat="server" Width="100%" Text='<%# Eval("display_order") %>'> </asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txtdiso"> </asp:FilteredTextBoxExtender>
                                    <asp:HiddenField ID="hfdiso" runat="server" Value='<%# Eval("display_order") %>'></asp:HiddenField>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                                    <asp:HiddenField ID="hfid" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                    <asp:Button runat="server" ID="btnDlt" Text="Delete" onclick="btnDlt_Click" class="btn btn-block btn-danger btn-sm"  CommandName="EmptyDataTemplate" />
                        </ItemTemplate>
                        <FooterTemplate >
                            <div class="formRow fluid ">
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-block btn-success" onclick="btn_Savall_Click"
                                 ToolTip="Update all" >
                               Save all</asp:LinkButton>
                            </div>
                            </FooterTemplate>
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

