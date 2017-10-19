<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<section class="slider">
            <div class="tp-banner" style=""> 
                <ul>
                    <li data-delay="7000" data-transition="fade" data-slotamount="7" data-masterspeed="2000" >
                        <div class="elements">
                            <h2 class="tp-caption tp-resizeme customin customout title bold" data-x="right" data-y="260"
                            data-customin="x:0;y:0;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:0;scaleY:0;skewX:0;skewY:0;opacity:0;transformPerspective:600;transformOrigin:50% 50%;"
                            data-customout="x:0;y:0;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:1;scaleY:1;skewX:0;skewY:0;opacity:0;transformPerspective:600;transformOrigin:50% 50%;"
                            data-speed="700" data-start="1000" data-easing="Power3.easeInOut" data-endspeed="300"
                            style="z-index: 5">Welcome to
                            <span class="text-color text-shadow"> <br />GPMG College1</span>
                            </h2>
                        </div>
                        <img src="img/sections/slider/university_01.jpg" alt="" 
                        data-bgrepeat="no-repeat" />
                    </li>
                    <!-- Slide -->
                    <li data-delay="7000" data-transition="fade" data-slotamount="7" data-masterspeed="2000">
                        <div class="elements">
                            <h2 class="tp-caption tp-resizeme customin customout title bold" data-x="left" data-y="260"
                            data-customin="x:0;y:0;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:0;scaleY:0;skewX:0;skewY:0;opacity:0;transformPerspective:600;transformOrigin:50% 50%;"
                            data-customout="x:0;y:0;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:1;scaleY:1;skewX:0;skewY:0;opacity:0;transformPerspective:600;transformOrigin:50% 50%;"
                            data-speed="700" data-start="1000" data-easing="Power3.easeInOut" data-endspeed="300"
                            style="z-index: 5">Welcome to
                            <br />
                            <span class="text-color text-shadow">GPMG College</span></h2>
                        </div>
                        <img src="img/sections/slider/university_02.jpg" alt="" 
                        data-bgrepeat="no-repeat" />
                    </li>
                    <!-- Slide -->
                </ul>
                <div class="tp-bannertimer"></div>
            </div>
        </section>
   
        <section id="topbar3" class="top-bar-section top-bar-bg-color1">
        <div class="container">
                <div class="row" data-animation="">
                <marquee behaviour="scroll" width="100%" scrollamount="3" style="color:White;">
                    <asp:Label ID="lblflashnews" runat="server" Text=""></asp:Label>
                </marquee>
                </div>
        </div>
        </section>


         <section id="news" class="page-section">
            <div class="container">
                <div class="row">
                   
                    <div class="col-sm-12 col-md-4 testimonails">
                        <div class="section-title text-left" data-animation="fadeInUp">
                            <!-- Heading -->
                            <h2 class="title">Photo Gallery</h2>
                        </div>
                            <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                            <!-- Indicators -->
                            <ol class="carousel-indicators">
                                <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                                <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                    
                            </ol>
                            <!-- Wrapper for slides -->
                                <asp:Label ID="lblleftslider" runat="server" Text=""></asp:Label>
                            <!-- Controls -->
                            <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
                            <span class="fa fa-angle-left fa-2x" aria-hidden="true"></span> 
                            <span class="sr-only">Previous</span></a> 
                            <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
                            <span class="fa fa-angle-right fa-2x" aria-hidden="true"></span> 
                            <span class="sr-only">Next</span></a></div>
                    </div>
                     
                    <div class="col-sm-12 col-md-4">
                        <div class="section-title text-left" data-animation="fadeInUp">
                            <!-- Heading -->
                            <h2 class="title">Latest News</h2>
                        </div>
                        <asp:Label ID="lblnews" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="col-sm-12 col-md-4 testimonails">
                        <div class="section-title text-left" data-animation="fadeInUp">
                            <!-- Heading -->
                            <h2 class="title">Achievements</h2>
                        </div>
                        <div id="Div1" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#Div1" data-slide-to="0" class="active"></li>
                        </ol>
                        <!-- Wrapper for slides -->
                        <div class="carousel-inner" role="listbox">
                            <asp:Label ID="lblrightslider" runat="server" Text=""></asp:Label>
                        </div>
                        <!-- Controls -->
                        <a class="left carousel-control" href="#Div1" role="button" data-slide="prev">
                        <span class="fa fa-angle-left fa-2x" aria-hidden="true"></span> 
                        <span class="sr-only">Previous</span></a> 
                        <a class="right carousel-control" href="#Div1" role="button" data-slide="next">
                        <span class="fa fa-angle-right fa-2x" aria-hidden="true"></span> 
                        <span class="sr-only">Next</span></a>
                        </div>


                    </div>

                </div>
            </div>
        </section>


         <section id="works" class="page-section">
           <div class="image-bg content-in fixed" data-background="img/sections/bg/bg-4.jpg">
                <div class="overlay-strips"></div>
            </div>
           <div class="container">
                <div class="row">
           <div class="col-md-12 text-center">
                       
                         <div class="item col-md-4 col-sm-4">
                          <div class="client-details text-center">
                                <div class="client-image">
                                    <!-- Image --><br />
                                    <img class="img-circle" src="img/sections/2.jpg" width="180" height="180" alt="">
                                </div>
                                <div class="client-details">
                                <!-- Name -->
                                <strong class="text-color">Dr.Mathew George</strong> 
                                <!-- Company -->
                                 
                                <span  style="color:#fff;">M.Sc (Physics), PhD <br />Principal GPMGC</span></div>
                            </div>
                         </div>
                        <div class="item col-md-8  col-sm-8">
                         <div class="section-title">
                            <!-- Heading -->
                            <h2 class="title" style="color:#fff;">Principal's Message</h2>
                        </div>
                            <div class="no-border">
                                <blockquote class="small-text"  style="color:#fff;font-size:12px;"><i>"Education is the manifestation of required perfection which is already in man”. The cooperation of parents & guardians is inevitable and indispensable for the moulding of students in a desirable manner. I welcome you all to the most prominent Govinda Pai Memorial Govt.college. Our college should be a happy home for all our students. We are on the road to progress; so many things are still to be done. We respect basic human values and we are conscious of our duties and responsibilities. The mission of this college is to impart education to the young citizens, who are going to lead our country in future and also to let them be aware that a proper education shall be the stepping stone to acquire knowledge which shall set him free. Our aim is to nurture the students into self-confident and self-reliant individual. I once again welcome all young and aspiring learners to be a part of the reputation of this college. Let us strive together because together we can. Thanking you, Principal ".</i></blockquote>
                               
                            </div>
                           
                        </div>
                      
                    </div>
                    </div>
                    </div>
        </section>



        <!-- slider -->
        <section id="about-us" class="page-section">
            <div class="container">
            <div class="col-md-4" data-animation="pulse">
                        <!-- Image -->
                        <img src="img/sections/student-2.jpg" width="330" height="415" alt="" />
                    </div>
                    <div class="col-md-8" data-animation="pulse">
                       
                    
                <div class="section-title text-center" data-animation="fadeInUp">
                    <!-- Heading -->
                    <h2 class="title">Welcome to GOVINDA PAI MEMORIAL GOVT.COLLEGE</h2>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" data-animation="fadeInUp">
                        <!-- Text -->
                        <p class="title-description" style="text-align:justify;">
                        This institution was formally inaugurated in September 1980 by Sri. E K Nayanar, the then Chief Minister of Kerala in the presence of Sri. R Gundu Rao, the then Chief Minister of Karnataka. Sri. Baby John, Minister of Education, Govt. of Kerala, Dr. A Subba Rao, Minister of Irrigation, Govt. of Kerala and Sri. Veerappa Moily, Minister of Finance, Govt. of Karnataka. For over a year since the inception, the college functioned in the 'Vanabhojanasala' belonging to Sreemad Anantheshwara Temple. In October 1981, it was shifted to the semi finished building constructed in the compound of the late poet by the sponsoring committee of the college. In October 1990, the college got transferred to the present and permanent abode in the Hosabettu village of Manjeshwar, which forms the northern border of Kerala State. The college is about 25 km from Kasaragod, the district headquarters and 17 km away from the port city of Mangalore in Karnataka. The nearest railway station is Manjeshwar, hardly 1 km away from the campus.

                        </p>
                    </div>
                    
                    
                </div>
                </div>

                 <div class="col-md-12">
                        <div class="row services icons-circle">
                            <div class="item-box icons-color hover-black col-sm-4 col-md-4" data-animation="fadeInLeft">
                                <a href="#">
                                <!-- Icon -->
                                <i class="icon-book14 i-4x border-color"></i> 
                                <!-- Title -->
                                <h5 class="title">Aims and objectives</h5>
                                <!-- Text -->
                                <div>The college aims at equipping the students with innovative ideas and inspiring them to reach higher rungsof the social ladder</div></a>
                            </div>
                            <div class="item-box icons-color hover-black col-sm-4 col-md-4" data-animation="fadeInUp">
                                <a href="#">
                                <!-- Icon -->
                                <i class="icon-gear i-4x border-color"></i> 
                                <!-- Title -->
                                <h5 class="title">Anti Ragging Cell</h5>
                                <!-- Text -->
                                <div>Ragging is a criminal offence punishable by law. Ragging is strictly prohibited in all educational institutions.</div></a>
                            </div>
                            <div class="item-box icons-color hover-black col-sm-4 col-md-4" data-animation="fadeInLeft">
                                <a href="#">
                                <!-- Icon -->
                                <i class="icon-eye i-4x border-color"></i> 
                                <!-- Title -->
                                <h5 class="title">Vision and Mission</h5>
                                <!-- Text -->
                                <div>The mission of the college is to make distinctive and distinguished contribution to the cause of higher education</div></a>
                            </div>
                          
                        </div>
                    </div>
            </div>
        </section>
	


</asp:Content>

