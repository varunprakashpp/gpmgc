<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="add_academiccells_news.aspx.cs" Inherits="add_category" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
 
        
<section class="content" style="min-height:150px;">
    <div class="row">
    <div class="col-md-12">
        <div class="box box-primary search-job01">
        <div class="box-header with-border"><h3 class="box-title">Add & Update</h3>   </div>

        <div class="nav-tabs-custom">
            <div class="tab-content ">

                    <div class="col-md-12"  >

                        <div class="col-md-6">
                            <div class="form-group">
                            <label><br />Heading : <span style="color: red;">*</span></label>
                            <div >
                                <asp:TextBox ID="txt_head" class="form-control pull-right validate[required]" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                            <label><br />Display order : <span style="color: red;">*</span></label>
                            <div >
                                <asp:TextBox ID="txt_diso" class="form-control pull-right validate[required]" runat="server" Width="100%"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txt_diso"> </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-md-12"><div class="col-md-12">
                        <div class="form-group">
                        <label><br />Description : <span style="color: red;">*</span></label>
                        <div >
                            <%--<asp:TextBox ID="txt_des" class="form-control pull-right validate[required]" Rows="4" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>--%>
                            <CKEditor:CKEditorControl ID="txt_des" EnterMode="DIV" runat="server" class="validate[required]" Height="200px"  Width="100%" BasePath="ckeditor"
                        CausesValidation="True"></CKEditor:CKEditorControl>
                        </div>
                        </div>
                    </div></div>

                    <div class="col-md-12"  >

                        <div class="col-md-4"  >
                            <div class="form-group">
                            <label><br />Image 1: </label>
                            <asp:Label ID="lbl_img1" runat="server" Text=""></asp:Label>
                            
                            <asp:Panel ID="pnl_img1" runat="server">
                            <asp:FileUpload ID="fu_img1" class="form-control pull-right "  runat="server" />
                            <p class="help-block">Browse Photo <font color="red">*500px X 300px</font></p></asp:Panel>
                            </div>
                        </div>

                        <div class="col-md-4"  >
                            <div class="form-group">
                            <label><br />Image 2: </label>
                            <asp:Label ID="lbl_img2" runat="server" Text=""></asp:Label>
                            
                            <asp:Panel ID="pnl_img2" runat="server">
                            <asp:FileUpload ID="fu_img2" class="form-control pull-right "  runat="server" />
                            <p class="help-block">Browse Photo <font color="red">*500px X 300px</font></p></asp:Panel>
                            </div>
                        </div>

                        <div class="col-md-4"  >
                            <div class="form-group">
                            <label><br />Image 3: </label>
                            <asp:Label ID="lbl_img3" runat="server" Text=""></asp:Label>
                           
                            <asp:Panel ID="pnl_img3" runat="server">
                            <asp:FileUpload ID="fu_img3" class="form-control pull-right "  runat="server" />
                            <p class="help-block">Browse Photo <font color="red">*500px X 300px</font></p></asp:Panel>
                            </div>
                        </div>

                    </div>
                    
                    <div class="col-md-12" >
                     <div class="col-md-6">
                        <div class="form-group">
                        <label><br />Status: </label>
                            <asp:RadioButtonList ID="rbl_stat" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="1" Selected="True">&emsp;Active&emsp;</asp:ListItem>
                                <asp:ListItem Value="0">Inactive&emsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                     </div>
                    <div class="col-md-6" id="divbutton" runat="server">
                        <div class="form-group">
                        <label><br />&nbsp;</label>
                        <div >
                        <asp:Button ID="Button1" runat="server" class="btn btn-success pull-right" OnClientClick="jQuery('#form1').validationEngine();" style="float:right;"
                                            Text="   Save   " onclick="Button1_Click"/>
                        </div>
                    </div>
                    </div>
                </div>

           </div>
        </div>

        <div class="nav-tabs-custom"><div class="tab-content"><div class="post"><div class="user-block"></div></div></div></div>

        </div>
    </div>
    </div>
</section>


</asp:Content>

