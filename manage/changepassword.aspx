<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="add_employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 
        
<section class="content">
    <div class="row">
        <div class="col-md-12">


<div class="box box-primary search-job01">
<div class="box-header with-border"><h3 class="box-title"> Change password</h3></div>

<div class="" >
 <div class="tab-content ">

          <div class="col-md-12" style="margin-top: 20px;">
            <div class="col-md-2">
                <div class="form-group">
                <label>Old password : <span style="color: red;">*</span></label>
                </div>
            </div>
            <div class="col-md-6">
              <div class="form-group">
                <div >
                   <asp:TextBox ID="txt_old" class="form-control pull-right validate[required]" runat="server" TextMode="Password" Width="100%"></asp:TextBox>
                </div>
              </div>
            </div>  
          </div>

          <div class="col-md-12" style="margin-top: 20px;">
            <div class="col-md-2">
                <div class="form-group">
                <label>New password : <span style="color: red;">*</span></label>
                </div>
            </div>
            <div class="col-md-6">
              <div class="form-group">
                <div >
                   <asp:TextBox ID="txt_new" class="form-control pull-right validate[required]" runat="server" TextMode="Password"></asp:TextBox>
                </div>
              </div>
            </div>  
          </div>

          <div class="col-md-12" style="margin-top: 20px;">
            <div class="col-md-2">
                <div class="form-group">
                <label>Confirm password : <span style="color: red;">*</span></label>
                </div>
            </div>
            <div class="col-md-6">
              <div class="form-group">
                <div >
                   <asp:TextBox ID="txt_con" class="" runat="server" TextMode="Password"></asp:TextBox>
                </div>
              </div>
            </div>  
          </div>

          <div class="col-md-12" style="margin-top: 5px;">
            <div class="col-md-8">
              <div class="form-group">
                <div >
                   <asp:Button ID="Button1" runat="server" class="btn btn-success pull-right" OnClientClick="jQuery('#form1').validationEngine();" style="float:right;margin-top: 5px;"
                                 Text="Save Changes" onclick="Button1_Click" />
                </div>
              </div>
            </div> 
            <div class="col-md-4"> &emsp;</div>
          </div>
 </div>
</div>
		
		
	
<div class="nav-tabs-custom">
    <div class="tab-content"><div class="post"><div class="user-block"></div></div></div>
</div>
</div>
	
        </div>
    </div>
</section>

</asp:Content>

