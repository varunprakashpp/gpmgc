<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="gallery.aspx.cs" Inherits="about" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


<style type="text/css">
        
        .page_enabled, .page_disabled
        {
            display: inline-block;
            padding: 5px 10px;
            min-width: 20px;
            line-height: 20px;
            text-align: center;
            text-decoration: none;
            border: 1px solid #e1e1e1;
            border-radius:4px;
        }
        .page_enabled
        {
            background-color: #eee;
            color: #000;
        }
        .page_disabled
        {
            background-color: #9b822d;
            color: #fff !important;
        }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <section id="works" class="page-section">
            <div class="container">
            		 
                <div class="mixed-grid pad gallery_wrap">
                     
                    <div class="clearfix"></div>
                    <div class="masonry-grid grid-col-4">
                        <div class="grid-sizer"></div>

                        <asp:Repeater ID="rptCustomers" runat="server" onitemdatabound="rptCustomers_ItemDataBound">
                        <ItemTemplate>
                        <!------------------------------------>
                        <asp:Label ID="lblcont" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfid" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                <asp:HiddenField ID="hfhead" runat="server" Value='<%# Eval("heading") %>'></asp:HiddenField>
                                <asp:HiddenField ID="hfcphoto" runat="server" Value='<%# Eval("cover_photo") %>'></asp:HiddenField>
                        <!------------------------------------>
                        </ItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>
                <hr />
                <div>
                    <asp:Repeater ID="rptPager" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </section>
		

</asp:Content>

