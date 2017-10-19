<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="fees.aspx.cs" Inherits="add_category" %>
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

                        <div class="col-md-12">
                            <div class="form-group">
                            <label><br />Fees : <span style="color: red;">*</span></label>
                            <div >
                                <%--<asp:TextBox ID="txt_head" class="form-control pull-right validate[required]" runat="server" Rows="4" Width="100%" TextMode="MultiLine"></asp:TextBox>--%>
                                <CKEditor:CKEditorControl ID="txt_head" EnterMode="DIV" runat="server" class="validate[required]" Height="200px"  Width="100%" BasePath="ckeditor"
                        CausesValidation="True"></CKEditor:CKEditorControl>
                            </div>
                            </div>
                        </div>

                        

                        <div class="col-md-12" id="divbutton" runat="server">
                        <div class="form-group">
                        <label><br />&nbsp;</label>
                        <div >
                        <asp:Button ID="Button1" runat="server" class="btn btn-success pull-right" OnClientClick="jQuery('#form1').validationEngine();" style="float:right;"
                                            Text="Submit" onclick="Button1_Click"/>
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

