<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="view_album_details.aspx.cs" Inherits="add_category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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

<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
 
        
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
                            <asp:TextBox ID="txt_des" class="form-control pull-right validate[required]" Rows="4" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </div>
                        </div>
                    </div></div>

                    <div class="col-md-12"  >

                        <div class="col-md-6"  >
                            <div class="form-group">
                            <label><br />Cover photo: </label>
                            <asp:Label ID="lbl_img1" runat="server" Text=""></asp:Label>
                            
                            </div>
                        </div>

                        <div class="col-md-6">
                        <div class="form-group">
                        <label><br />Status: </label>
                            <asp:RadioButtonList ID="rbl_stat" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="1" Selected="True">&emsp;Active&emsp;</asp:ListItem>
                                <asp:ListItem Value="0">Inactive&emsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                       </div>
                    </div>

                    <asp:Label ID="lbl_image" runat="server" Text=""></asp:Label>
           </div>
        </div>

        <div class="nav-tabs-custom"><div class="tab-content"><div class="post"><div class="user-block"></div></div></div></div>

        </div>
    </div>



    </div>
</section>


</asp:Content>

