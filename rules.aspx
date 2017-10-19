<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rules.aspx.cs" Inherits="about" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <section id="about-us" class="page-section">
        <div class="container">
            <div class="col-md-12" data-animation="pulse">
                <div class="row">
                    <div class="col-md-12 text-center" data-animation="fadeInUp">
                        <div class="title-description" style="text-align:justify;text-align:left;">
                            <asp:Label ID="lbldata" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>

