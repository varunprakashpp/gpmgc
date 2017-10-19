<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="academiccells.aspx.cs" Inherits="add_academiccells" %>

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

                        <div class="col-md-4">
                            <div class="form-group">
                            <label><br />Academic cell : <span style="color: red;">*</span></label>
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
                        

                        <div class="col-md-4" id="divbutton" runat="server">
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

    <div class="row">
        <div class="col-md-12">
          <div class="box box-primary search-job01">
            <div class="box-header  ">
              <h3 class="box-title">View academic cells</h3>
            </div>
            <div >
              <div class="box-body">
                <table id="example1" class="table table-bordered table-striped">
                  <thead>
                    <tr>
                      <th width="50">Sl No</th>
                      <th>Academic cell </th>
                      <th>Display order </th>
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

