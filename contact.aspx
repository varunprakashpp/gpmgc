<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="news" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


<link rel="stylesheet" href="design/css/validationEngine.jquery.css" type="text/css"/>
	
	<script src="design/js/jquery-1.8.2.min.js" type="text/javascript">
	</script>
    <script type='text/javascript'>
        var $jq = jQuery.noConflict();    
</script>
	<script src="design/js/jquery.validationEngine-en.js" type="text/javascript" charset="utf-8">
	</script>
	<script src="design/js/jquery.validationEngine.js" type="text/javascript" charset="utf-8">
	</script>
	<script type="text/javascript">
	    $jq(document).ready(function () {
	        $jq("#form1").validationEngine();
	        $jq("#aspnetForm").validationEngine();
	    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

         <section id="contact-us" class="page-section">
                <div class="container">
					<div class="row">
                        <div class="col-md-12 ">
							<div class="row text-center">
								<address class="col-sm-4 col-md-4">
									<i class="fa fa-map-marker i-9x icons-circle text-color light-bg hover-black"></i>
									<div class="title">Address</div>
									GOVINDA PAI MEMORIAL GOVERMNET COLLEGE MANJESHWARAM
(Established in 1980)
(PO) MANJESHWARAM
PIN- 671323
KASARAGOD (DIST)
KERALA
INDIA
								</address>
								<address class="col-sm-4 col-md-4">
									<i class="fa fa-microphone i-9x icons-circle text-color light-bg hover-black"></i>
									<div class="title">Phones</div>
									<div>Phone: 04998272670</div>
                                    <div>Fax: 04998272670</div>
								</address>
								<address class="col-sm-4 col-md-4">
									<i class="fa fa-envelope i-9x icons-circle text-color light-bg hover-black"></i>
									<div class="title">Email Addresses</div>
									<div><a href="mailto:gpmgcm2@gmail.com">gpmgcm2@gmail.com</a></div>
								</address>
							</div>
						</div>
					</div>
                    <hr class="pad-w10 ">
                    	<div class="row"> 
                        	<div class="col-lg-12">
                        			<div class="more_cont_info ">
                        		<div class="col-sm-4"> 
                                	<ul style="list-style-type:none; text-align:left; line-height:2em" >
                                        <li>Dr.Mathew George <br />(PRINCIPAL)</li>
                                        <hr />
                                        <li>GOVINDA PAI MEMORIAL GOVT.COLLEGE</li>
                                        <li>MANJESHWAR (PO)</li>
                                        <li>KASARAGOD (DT)</li>
                                        <li>KERALA - 671323.</li>
                                        <li>PHONE</li>
                                        <li>(OFFICE)      :04998272670</li>
                                        <li>MOB            :9447207591</li>
                                        <li>FAX .NO       :04998272670</li>
                                        <li>e –mail        :gpmgcm2@gmail.com</li>
                                      </ul>
                                </div>
                                <div class="col-sm-4"> 
                                
                                				<ul style="list-style-type:none; text-align:left; line-height:2em" >
                                                    <li>Smt. AMITHA S <br />(VICE PRINCIPAL)</li>
                                                     <hr />
                                                    <li>MOB    : 9496237243</li>
                                                    <li>Sri. SHIVASHANKARA P(Asst. Professor of Knaada)</li>
                                                    <li>PUBLIC INFORMATION OFFICER (RTI)</li>
                                                    <li>MOB               :9496359471</li>
                                                    <li>PHONE :04998272670</li>
                                                    <li>(OFFICE)</li>
                                                    <li>e-mail         
                                                      k.v.ravindran@ymail.com</li>
                                                  </ul>
                                </div>
                                <div class="col-sm-4"> 
                                				<ul style="list-style-type:none; text-align:left; line-height:2em"  >

                                                <li>ASST.PUBLIC INFORMATION-</li>
                                                <li>OFFICER (RTI)</li>
                                                <li>MOB       :04998272670</li>
                                                <li>e-mail (UGC)   :gpmgcm2@gmail.com</li>
                                              </ul>
                                
                                </div>
                               </div>
                             </div>
                        </div>
                    
					<hr class="pad-w10">
					<div class="row">
						<div class="col-md-4">
							<p class="form-message" style="display: none;"></p>
							<div class="contact-form">
								<!-- Form Begins -->
								<form role="form" name="contactform" id="contactform" method="post" action="php/contact-form.php">
										<!-- Field 1 -->
										<div class="input-text form-group">
                                            <asp:TextBox ID="txt_name" runat="server" name="contact_name" class="input-name form-control validate[required]" placeholder="Full Name" ></asp:TextBox>
										</div>
										<!-- Field 2 -->
										<div class="input-email form-group">
                                            <asp:TextBox ID="txt_email" runat="server" name="contact_email"  class="input-email form-control validate[required,custom[email]]"  placeholder="Email"></asp:TextBox>
										</div>
										<!-- Field 3 -->
										<div class="input-email form-group">
                                            <asp:TextBox ID="txt_mob" runat="server" name="contact_phone" MaxLength="12" class="input-phone form-control validate[required,maxSize[12],minSize[10],custom[onlyNumberSp]]"  placeholder="Phone"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txt_mob"> </asp:FilteredTextBoxExtender>
										</div>
										<!-- Field 4 -->
										<div class="textarea-message form-group">
                                            <asp:TextBox ID="txt_feed" runat="server" class=" textarea-message hight-82 form-control validate[required]" placeholder="Message" rows="2"  TextMode="MultiLine"  ></asp:TextBox>
										</div>
										<!-- Button -->
										
                                         <asp:Button ID="Button1" runat="server" Text="Submit" OnClientClick="jQuery('#form1').validationEngine();" class="btn btn-default btn-block"
                                            onclick="btn_submit_Click"/>
								</form>
								<!-- Form Ends -->
							</div>
						</div>						
						<div class="col-md-8">
							 
						<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3891.808956383792!2d74.89291171481938!3d12.725882791017368!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3ba360a258652353%3A0xd6ba7b5ebcfd3dac!2sGovinda+Pai+Memorial+Government+College!5e0!3m2!1sen!2sin!4v1507293975088" width="100%" height="320" frameborder="0" style="border:0" allowfullscreen></iframe>
                        </div><!-- map -->
				</div>
                </div>
            </section><!-- page-section -->

</asp:Content>

