<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="academicnews_more.aspx.cs" Inherits="news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <section id="service" class="page-section placement_cell">
            <div class="container">
                
                <div class="row">
                    <div class="col-md-12">
                        <div class="row"><!--head 42, desc 120-->
                            <div class=" col-lg-4 col-sm-4">
                            	<h4 style="border-bottom:1px solid #ddd; padding-bottom:10px;">Related News</h4>
                                <asp:Label ID="lblrelate" runat="server" Text=""></asp:Label>
                            </div>
                            <div class=" col-lg-8 col-sm-8">
                                <div>
                                    <asp:Label ID="lblcontent" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            
                            
                            
                        </div>
                         
                    </div>
                </div>
            </div>
        </section>

</asp:Content>

