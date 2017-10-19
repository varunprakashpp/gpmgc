<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="gallery_more.aspx.cs" Inherits="about" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <section id="works" class="page-section">
            <div class="container">
            		 
                <div class="mixed-grid pad gallery_wrap">
                     
                     <div class="col-lg-12">
                     <div class="galry_more_big">
                     	<div class="row">
                                <asp:Label ID="lblmaincon" runat="server" Text=""></asp:Label>
                        </div>
                        </div>
                     </div>

                    <div class="clearfix"></div>
                    <div class="masonry-grid grid-col-4">
                        <div class="grid-sizer"></div>
                        <asp:Label ID="lblcont" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </section>
		

</asp:Content>

