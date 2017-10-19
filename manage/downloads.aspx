<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="downloads.aspx.cs" Inherits="add_category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    var _validFileExtensions1 = [".pdf", ".doc", ".docx", ".zip", ".rar"];
    
    function ValidateSingleInput1(oInput) {
        if (oInput.type == "file") {
            var sFileName = oInput.value;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < _validFileExtensions1.length; j++) {
                    var sCurExtension = _validFileExtensions1[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                        break;
                    }
                }

                if (!blnValid) {
                    alert("File should be of format .pdf, .doc, .docx, .rar, .zip !");
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

<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
 
        
<section class="content" style="min-height:150px;">

    <div class="row">
    <div class="col-md-12">
        <div class="box box-primary search-job01">
        <div class="box-header with-border"><h3 class="box-title">Add & Update</h3>   </div>

        <div class="nav-tabs-custom">
            <div class="tab-content ">

                    <div class="col-md-12"  >

                        <div class="col-md-4">
                            <div class="form-group">
                            <label><br />Heading : <span style="color: red;">*</span></label>
                            <div >
                                <asp:TextBox ID="txt_head" class="form-control pull-right validate[required]" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                            <label><br />Display order : <span style="color: red;">*</span></label>
                            <div >
                                <asp:TextBox ID="txt_diso" class="form-control pull-right validate[required]" runat="server" Width="100%"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txt_diso"> </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                            <label><br />Uploads: <asp:Label ID="lblink" runat="server" Text="" style="color: red;"></asp:Label></label>
                            <asp:Label ID="lbl_rpt" runat="server" Text=""></asp:Label>
                            
                            <asp:Panel ID="pnl_file" runat="server">
                            <asp:FileUpload ID="fu_rpt" class="form-control pull-right "  onchange="ValidateSingleInput1(this);"  runat="server" />
                            <p class="help-block">Browse file <font color="red">*(.pdf, .doc, .docx, .zip, .rar)</font></p></asp:Panel>
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

    <div class="row">
        <div class="col-md-12">
          <div class="box box-primary search-job01">
            <div class="box-header  ">
              <h3 class="box-title">View downloads</h3>
            </div>
            <div >
              <div class="box-body">
                <table id="example1" class="table table-bordered table-striped">
                  <thead>
                    <tr>
                      <th width="50">Sl No</th>
                      <th>Heading </th>
                      <th>Display order</th>
                      <th>Downloads </th>
                      <th>Action</th>
                    </tr>
                  </thead>
                  <tbody>
                    <asp:Label ID="lbl_tablebody" runat="server" Text=""></asp:Label>
                  </tbody>
                  
                </table>
              </div>
            </div>
            <div class="nav-tabs-custom"><div class="tab-content"><div class="post"><div class="user-block"></div></div></div></div>
          </div>

          <div class="clearfix"></div><div class="control-sidebar-bg"></div>

        </div>
    </div>

</section>


</asp:Content>

