<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="teaching.aspx.cs" Inherits="about" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


            <!-- slider -->
        <section id="about-us" class="page-section">
            <div class="container">

                <div class="col-md-4" data-animation="pulse">
                    <asp:Label ID="lbl_branch" runat="server" Text=""></asp:Label>
                    <div class="clearfix"></div>
                </div>
                
                <div class="col-md-8" data-animation="pulse">
                    <div class="row">
                        <div class="col-md-12 text-center" data-animation="fadeInUp">
                            <asp:Label ID="lblstaff" runat="server" Text=""></asp:Label>
                        </div>
                    </div> 
                </div>

            </div>
        </section>
		

</asp:Content>

