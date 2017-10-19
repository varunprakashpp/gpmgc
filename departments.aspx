<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="departments.aspx.cs" Inherits="news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 
      <section class="page-section">
            <div class="container">
                <div class="row">
                	
                    <div class="col-sm-12 col-md-8 post-list" data-animation="pulse">
                    	<div class="departmetnt_pro">
                        <div class="post-item">
                        	<asp:Label ID="lblcont" runat="server" Text=""></asp:Label>
                        </div>
                     	</div>
                    </div>
                     <div class="col-md-4" data-animation="pulse">
                      	<asp:Label ID="lbldept" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </section>
		

</asp:Content>

