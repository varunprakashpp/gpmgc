<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="add_staff.aspx.cs" Inherits="add_category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript">
      var _validFileExtensions = [".jpg", ".jpeg", ".png", ".gif"];
      function ValidateSingleInput(oInput) {
          if (oInput.type == "file") {
              var sFileName = oInput.value;
              if (sFileName.length > 0) {
                  var blnValid = false;
                  for (var j = 0; j < _validFileExtensions.length; j++) {
                      var sCurExtension = _validFileExtensions[j];
                      if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                          blnValid = true;
                          break;
                      }
                  }

                  if (!blnValid) {
                      alert("Not a valid file !");
                      oInput.value = "";
                      return false;
                  }
              }
          }
          return true;
      }


  </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
 
        
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
                            <label><br />Category : <span style="color: red;">*</span></label>
                            <div >
                                <asp:RadioButtonList ID="rbl_cat" runat="server" 
                                    RepeatDirection="Horizontal" CellPadding="3" CellSpacing="3" 
                                    onselectedindexchanged="rbl_cat_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="teaching" Selected="True">&nbsp;Teaching&emsp; </asp:ListItem>
                                    <asp:ListItem Value="nonteaching">&nbsp;Non teaching</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                            <label><br />Department : <span style="color: red;">*</span></label>
                            <div >
                               <asp:DropDownList ID="ddl_dept" class="form-control pull-right " runat="server" style="display:block;">
                               </asp:DropDownList>
                            
                               <asp:TextBox ID="txt_dept" class="form-control pull-right " runat="server" Width="100%" style="display:none;" placeholder="Enter department"></asp:TextBox>
                            </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-md-12"  >

                        <div class="col-md-6">
                            <div class="form-group">
                            <label><br />Name : <span style="color: red;">*</span></label>
                            <div >
                                <asp:TextBox ID="txt_name" class="form-control pull-right validate[required]" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                            <label><br />Designation : <span style="color: red;">*</span></label>
                            <div >
                                <asp:TextBox ID="txt_des" class="form-control pull-right validate[required]" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            </div>
                        </div>

                    </div>

                     <div class="col-md-12"  >

                        <div class="col-md-6">
                            <div class="form-group">
                            <label><br />Email : <span style="color: red;">*</span></label>
                            <div >
                                <asp:TextBox ID="txt_email" class="form-control pull-right validate[custom[email],required]" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                            <label><br />Mobile : <span style="color: red;">*</span></label>
                            <div >
                                <asp:TextBox ID="txt_mobile" class="form-control pull-right validate[required,maxSize[10],custom[onlyNumberSp],minSize[10]]"  MaxLength="10" runat="server" Width="100%" ></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txt_mobile"> </asp:FilteredTextBoxExtender>
                            </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12"  >
                        <div class="col-md-6">
                        <div class="form-group">
                        <label><br />Status: </label>
                            <asp:RadioButtonList ID="rbl_stat" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="1" Selected="True">&emsp;Active&emsp;</asp:ListItem>
                                <asp:ListItem Value="0">Inactive&emsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                       </div>

                       <div class="col-md-6">
                        <div class="form-group">
                        <label><br />Gender: </label>
                            <asp:RadioButtonList ID="rbl_gender" runat="server" RepeatDirection="Horizontal" >
                                <asp:ListItem Value="Male" Selected="True">&emsp;Male&emsp;</asp:ListItem>
                                <asp:ListItem Value="Female">Female&emsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                       </div>

                    </div>


                    <div class="col-md-12"  >

                        <div class="col-md-6"  >
                            <div class="form-group">
                            <label><br />Photo: <span style="color: red;">*</span></label>
                            <asp:Label ID="lbl_img1" runat="server" Text=""></asp:Label>
                            
                            <asp:Panel ID="pnl_img1" runat="server">
                            <asp:FileUpload ID="fu_img1" class="form-control pull-right  validate[required]"  runat="server" onchange="ValidateSingleInput(this);"/>
                            <p class="help-block">Browse file <font color="red">*(500px X 300px)</font></p></asp:Panel>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                            <label><br />Display order : <span style="color: red;">*</span></label>
                            <div >
                                <asp:TextBox ID="txt_diso" class="form-control pull-right validate[required]" runat="server" Width="100%"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txt_diso"> </asp:FilteredTextBoxExtender>
                            </div>
                            </div>
                        </div>
                        
                    </div>
                    
                    <div class="col-md-12" >
                     <div class="col-md-6">&emsp;</div>
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

