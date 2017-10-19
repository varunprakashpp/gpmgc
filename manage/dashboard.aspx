<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="manage_dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style>
        	.dash_nav {
				border:1px solid #ddd; 
				padding:10px;
				margin-bottom:10px;
				border-radius:3px;
				-webkit-border-radius:3px;
				-moz-border-radius:3px;
				-o-border-radius:3px;
				background:#359bba;
				}
			.dash_nav:hover {
				background:#427d94;
				border:1px solid #3e7388; }
			
			.dash_nav h4 {
				margin-top:5px;
				color:#fff}
			.dash_nav h4 .fa {
				color:#fff}
			.dash_nav a {
				color:#eee}
			.dash_nav a:hover {
				color:#fff}
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<section class="content">
          <div class="row">
          		<div class="col-md-12">
                      <div class="box box-warning">
                      	<div class="box-body">
                        	<br/> 
                        	<div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-navicon"></i>&nbsp;&nbsp;News</h4>
                                    <a href="add_news.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    <a href="view_news.aspx" class=" "><i class="fa fa-eye"></i>&nbsp;&nbsp;View</a>
                           		 </div>
                            </div>
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-archive"></i>&nbsp;&nbsp;Achievements</h4>
                                    <a href="add_achievements.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    <a href="view_achievements.aspx"><i class="fa fa-eye"></i>&nbsp;&nbsp;View</a>
                           		 </div>
                            </div>
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-save"></i>&nbsp;&nbsp;Department</h4>
                                    <a href="add_departments.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    <a href="view_departments.aspx"><i class="fa fa-eye"></i>&nbsp;&nbsp;View</a>
                           		 </div>
                            </div>
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-neuter"></i>&nbsp;&nbsp;Flash News</h4>
                                    <a href="add_flashnews.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    <a href="view_flashnews.aspx"><i class="fa fa-eye"></i>&nbsp;&nbsp;View</a>
                           		 </div>
                            </div>
                            
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-user"></i>&nbsp;&nbsp;Staff</h4>
                                    <a href="add_staff.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    <a href="view_staff.aspx"><i class="fa fa-eye"></i>&nbsp;&nbsp;View</a>
                           		 </div>
                            </div>
                            
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-search"></i>&nbsp;&nbsp;Research Project</h4>
                                    <a href="add_research_project.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    <a href="view_research_project.aspx"><i class="fa fa-eye"></i>&nbsp;&nbsp;View</a>
                           		 </div>
                            </div>
                            
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-camera"></i>&nbsp;&nbsp;Gallery</h4>
                                    <a href="add_album.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    <a href="view_album.aspx"><i class="fa fa-eye"></i>&nbsp;&nbsp;View</a>
                           		 </div>
                            </div>
                            
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-book"></i>&nbsp;&nbsp;Academic Cell</h4>
                                    <a href="academiccells.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                   
                           		 </div>
                            </div>
                              
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-rupee"></i>&nbsp;&nbsp;Fees</h4>
                                    <a href="fees.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                   
                           		 </div>
                            </div>
                            
                              
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-random"></i>&nbsp;&nbsp;Rules</h4>
                                    <a href="rules.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    
                           		 </div>
                            </div>
                            
                              
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-renren"></i>&nbsp;&nbsp;Procedure</h4>
                                    <a href="procedure.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    
                           		 </div>
                            </div>
                            
                            <div class="col-lg-3 col-md-6 col-sm-6">
                            	<div class="dash_nav">
                                
                                    <h4><i class="fa fa-download"></i>&nbsp;&nbsp;Downloads</h4>
                                    <a href="downloads.aspx"><i class="fa fa-plus"></i>&nbsp;&nbsp;Add</a>&nbsp;&nbsp;&nbsp;
                                    
                           		 </div>
                            </div>  
                             
                            
                            <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
                        </div>
                        
                      </div>
              </div>
          </div>
        </section>

</asp:Content>

