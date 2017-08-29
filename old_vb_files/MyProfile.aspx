<%@ Page Title="" Language="VB" MasterPageFile="~/mProfile.master" AutoEventWireup="false"
    EnableSessionState="True" CodeFile="MyProfile.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Info" runat="Server">
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="sqldatasource3">
        <ItemTemplate>
            <div class="jumbotron" style="width: 100%; height: 500px; background-color: #f3f3f3; background-image: url(<%#Eval("cover_url") %>); background-position: center; background-size: cover; overflow: hidden;">
        </ItemTemplate>
    </asp:Repeater>
    
    <asp:sqldatasource ID="sqldatasource3" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
        SelectCommand="SELECT * FROM [userinfo] WHERE ([profile_id] = @me)">
        <SelectParameters>
            <asp:SessionParameter Name="me" SessionField="idsess" Type="String" />
        </SelectParameters>
    </asp:sqldatasource>
    <div class="tab-content">
        <div class="tab-pane fade active in" id="homeob">
            <a href="#favnint" data-toggle="tab" class="col-sm-3 list-group-item" style="border-radius: 9px; opacity: 0.9;">
                <center>
                    <i class="fa fa-heart"></i>&nbsp; Favourites & Interests</center>
            </a><a href="#baseinfo" data-toggle="tab" class="col-sm-4 col-sm-offset-1 list-group-item"
                style="border-radius: 9px; opacity: 0.9;">
                <center>
                    <i class="fa fa-user"></i>&nbsp; Basic Info</center>
            </a><a href="#testies" data-toggle="tab" class="col-sm-3 col-sm-offset-1 list-group-item"
                style="border-radius: 9px; opacity: 0.9;">
                <center>
                    <i class="fa fa-star"></i>&nbsp; Testimonials</center>
            </a>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <a id="A3" runat="server" class="col-sm-12 list-group-item pull-right" style="border-radius: 9px; opacity: 0.3;"
                onclick="openupcover();">
                <center>
                    <i class="fa fa-camera-retro "></i>&nbsp; Change Cover</center>
            </a>
        </div>
        <div id="favnint" class="tab-pane fade" style="opacity: 0.9;">
            <ul class="pagination col-sm-12">
                <li class="col-sm-offset-1" href="#homeob" data-toggle="tab" style="color: White; cursor: pointer;">
                    <h5>&larr;&nbsp;Back To OpenBook</h5>
                </li>
            </ul>
            <div class="panel panel-default  col-sm-6">
                <div class="panel-heading dropdown-toggle" data-toggle="dropdown">
                    Favourites
                </div>
                <div align="center">
                    <div class="tab-content content">
                        <div id="music" class="tab-pane fade active in">
                            <div class="col-sm-12 pull-left">
                                <br />
                                <ul class="nav nav-pills nav-stacked">
                                    <asp:Repeater ID="music_rep" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <div class="input-group">
                                                <input type="text" runat="server" id="text7" class="form-control col-sm-3" value='<%# Eval("m_name")%>'
                                                    enableviewstate="true" />
                                                <span class="input-group-addon">
                                                    <asp:Label runat="server" ID="label_music" Text='<%# Eval("m_primary")%>' Visible="false"></asp:Label></span>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                            <asp:LinkButton ID="mucha" OnClick="changemu" CssClass="btn-sm btn-info" Style="border-radius: 5px; text-decoration: none;"
                                runat="server"> Update Music Favourites
                            </asp:LinkButton>
                        </div>
                        <div id="movies" class="tab-pane fade in">
                            <div class="col-sm-12 pull-left">
                                <br />
                                <ul class="nav nav-pills nav-stacked">
                                    <asp:Repeater ID="movies_rep" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <div class="input-group">
                                                <input type="text" runat="server" id="text7" class="form-control col-sm-3" value='<%# Eval("d_name")%>'
                                                    enableviewstate="true" />
                                                <span class="input-group-addon">
                                                    <asp:Label runat="server" ID="label_movies" Text='<%# Eval("mo_primary")%>' Visible="false"></asp:Label></span>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                            <asp:LinkButton ID="mocha" OnClick="changemo" CssClass="btn-sm btn-info" Style="border-radius: 5px; text-decoration: none;"
                                runat="server"> Update Movie Favourites
                            </asp:LinkButton>
                        </div>
                        <div id="books" class="tab-pane fade in">
                            <div class="col-sm-12 pull-left">
                                <br />
                                <ul class="nav nav-pills nav-stacked">
                                    <asp:Repeater ID="books_rep" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <div class="input-group">
                                                <input type="text" runat="server" id="text7" class="form-control col-sm-3" value='<%# Eval("d_name")%>'
                                                    enableviewstate="true" />
                                                <span class="input-group-addon">
                                                    <asp:Label runat="server" ID="label_books" Text='<%# Eval("bo_primary")%>' Visible="false"></asp:Label></span>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                            <asp:LinkButton ID="bocha" OnClick="changebo" CssClass="btn-sm btn-info" Style="border-radius: 5px; text-decoration: none;"
                                runat="server"> Update Books Favourites
                            </asp:LinkButton>
                        </div>
                        <div id="food" class="tab-pane fade in">
                            <div class="col-sm-12 pull-left">
                                <br />
                                <ul class="nav nav-pills nav-stacked">
                                    <asp:Repeater ID="food_rep" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <div class="input-group">
                                                <input type="text" runat="server" id="text7" class="form-control col-sm-3" value='<%# Eval("d_name")%>'
                                                    enableviewstate="true" />
                                                <span class="input-group-addon">
                                                    <asp:Label runat="server" ID="label_food" Text='<%# Eval("fo_primary")%>' Visible="false"></asp:Label></span>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                            <asp:LinkButton ID="focha" OnClick="changefo" CssClass="btn-sm btn-info" Style="border-radius: 5px; text-decoration: none;"
                                runat="server"> Update Food Favourites
                            </asp:LinkButton>
                        </div>
                        <div id="tvshows" class="tab-pane fade in">
                            <div class="col-sm-12 pull-left">
                                <br />
                                <ul class="nav nav-pills nav-stacked">
                                    <asp:Repeater ID="tv_rep" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <div class="input-group">
                                                <input type="text" runat="server" id="text7" class="form-control col-sm-3" value='<%# Eval("tv_d")%>'
                                                    enableviewstate="true" />
                                                <span class="input-group-addon">
                                                    <asp:Label runat="server" ID="label_tv" Text='<%# Eval("tv_primary")%>' Visible="false"></asp:Label></span>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                            <asp:LinkButton ID="tvcha" OnClick="changetv" CssClass="btn-sm btn-info" Style="border-radius: 5px; text-decoration: none;"
                                runat="server"> Update TV Favourites
                            </asp:LinkButton>
                        </div>
                        <div id="sports" class="tab-pane fade in">
                            <div class="col-sm-12 pull-left">
                                <br />
                                <ul class="nav nav-pills nav-stacked">
                                    <asp:Repeater ID="teams_rep" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <div class="input-group">
                                                <input type="text" runat="server" id="text7" class="form-control col-sm-3" value='<%# Eval("team_d")%>'
                                                    enableviewstate="true" />
                                                <span class="input-group-addon">
                                                    <asp:Label runat="server" ID="label_te" Text='<%# Eval("te_primary")%>' Visible="false"></asp:Label></span>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                            <asp:LinkButton ID="techa" OnClick="changeteams" CssClass="btn-sm btn-info" Style="border-radius: 5px; text-decoration: none;"
                                runat="server"> Update Team Favourites
                            </asp:LinkButton>
                        </div>
                        <div id="places" class="tab-pane fade in">
                            <div class="col-sm-12 pull-left">
                                <br />
                                <ul class="nav nav-pills nav-stacked">
                                    <asp:Repeater ID="places_rep" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <div class="input-group">
                                                <input type="text" runat="server" id="text7" class="form-control col-sm-3" value='<%# Eval("places_d")%>'
                                                    enableviewstate="true" />
                                                <span class="input-group-addon">
                                                    <asp:Label runat="server" ID="label_pl" Text='<%# Eval("pl_primary")%>' Visible="false"></asp:Label></span>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                            <asp:LinkButton ID="plcha" OnClick="changeplaces" CssClass="btn-sm btn-info" Style="border-radius: 5px; text-decoration: none;"
                                runat="server"> Update Place Favourites
                            </asp:LinkButton>
                        </div>
                        <ul class="pagination">
                            <li class="active"><a href="#music" data-toggle="tab">Music</a></li>
                            <li><a href="#movies" data-toggle="tab">Movies</a></li>
                            <li><a href="#books" data-toggle="tab">Books</a></li>
                            <li><a href="#food" data-toggle="tab">Food</a></li>
                            <li><a href="#tvshows" data-toggle="tab">TV Shows</a></li>
                            <li><a href="#sports" data-toggle="tab">Team/Sport</a></li>
                            <li><a href="#places" data-toggle="tab">Places</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="panel panel-default col-sm-5 col-sm-offset-1">
                <div class="panel-heading dropdown-toggle" data-toggle="dropdown">
                    Hobbies & Interests
                </div>
                <div align="center">
                    <!-- INT AND INTFAVS FILLING-->
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane fade active in" id="int_fav">
                            <div class="col-sm-12 pull-left">
                                <br />
                                <ul class="nav nav-pills nav-stacked" align="center">
                                    <asp:Repeater ID="hobb_rep" EnableViewState="true" runat="server">
                                        <ItemTemplate>
                                            <div class="input-group">
                                                <input type="text" runat="server" id="text7" class="form-control col-sm-3" value='<%# Eval("int_fav")%>'
                                                    enableviewstate="true" />
                                                <span class="input-group-addon">
                                                    <asp:Label runat="server" ID="label_hobbies" Text='<%# Eval("fav_primary")%>' Visible="false"></asp:Label></span>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                            <asp:LinkButton ID="hocha" OnClick="changehobbs" CssClass="btn-sm btn-info" Style="border-radius: 5px; text-decoration: none;"
                                runat="server"> Update Hobbies
                            </asp:LinkButton>
                        </div>
                        <div class="tab-pane fade" id="int">
                            <div class="col-sm-12 pull-left">
                                <br />
                                <ul class="nav nav-pills nav-stacked" align="center">
                                    <asp:Repeater ID="int_rep" EnableViewState="true" runat="server">
                                        <ItemTemplate>
                                            <div class="input-group">
                                                <input type="text" runat="server" id="text7" class="form-control col-sm-3" value='<%# Eval("int_name")%>'
                                                    enableviewstate="true" />
                                                <span class="input-group-addon">
                                                    <asp:Label runat="server" ID="label_int" Text='<%# Eval("in_primary")%>' Visible="false"></asp:Label></span>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </div>
                            <asp:LinkButton ID="incha" OnClick="changeint" CssClass="btn-sm btn-info" Style="border-radius: 5px; text-decoration: none;"
                                runat="server"> Update Interests
                            </asp:LinkButton>
                        </div>
                        <ul class="pagination">
                            <li class="active"><a href="#int_fav" data-toggle="tab">Hobbies</a></li>
                            <li><a href="#int" data-toggle="tab">Interests</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div id="testies" class="tab-pane fade" style="opacity: 0.9;">
            <ul class="pagination col-sm-12">
                <li class="col-sm-offset-1" href="#homeob" data-toggle="tab" style="color: White; cursor: pointer;">
                    <h5>&larr;&nbsp;Back To OpenBook</h5>
                </li>
            </ul>
            <div class="panel panel-default col-sm-8 col-sm-offset-2">
                <div class="panel-heading dropdown-toggle" data-toggle="dropdown">
                    Testimonials
                </div>
                <div class="panel-body" style="max-height: 300px; overflow-y: scroll;">
                    <asp:Repeater ID="Repeater42" DataSourceID="test_data" runat="server">
                        <ItemTemplate>
                            <div class="col-sm-12">
                                <blockquote id="Blockquote1" class="col-sm-9">
                                    <p>
                                        <%# Eval("content")%>
                                    </p>
                                    <small>Someone
                                        <%# Eval("mode_1")%>
                                        named <cite title="Source Title"><a href="XsProfile.aspx" style="text-decoration: none">
                                            <%# Eval("fname")%>&nbsp;<%# Eval("lname")%></a></cite></small>
                                    <br />
                                </blockquote>
                                <div class="col-sm-3">
                                    <input id="input-21b" value="<%# Eval("rating")%>" type="number" class="rating disabled"
                                        min="0" max="5" step="0.2" data-size="xs" /><br />
                                    <h6 class="text-muted">
                                        <i class="fa fa-clock-o"></i>&nbsp;&nbsp;<span data-livestamp="<%# Eval("date_written", "{0:f}")%>"></h6>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:sqldatasource ID="test_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                        SelectCommand="SELECT * FROM ([userinfo] LEFT JOIN [tests] ON [userinfo].[profile_id]=[tests].[from]) WHERE ([to] = @me) ORDER BY [date_written] DESC">
                        <SelectParameters>
                            <asp:SessionParameter Name="me" SessionField="idsess" Type="String" />
                        </SelectParameters>
                    </asp:sqldatasource>
                </div>
            </div>
        </div>
        <div id="baseinfo" class="tab-pane fade" style="opacity: 0.9;">
            <ul class="pagination col-sm-12">
                <li class="col-sm-offset-1" href="#homeob" data-toggle="tab" style="color: White; cursor: pointer;">
                    <h5>&larr;&nbsp;Back To OpenBook</h5>
                </li>
            </ul>
            <asp:Repeater runat="server" DataSourceID="sqldatasource3">
                <ItemTemplate>
                    <li class="well-sm col-sm-6" style="list-style: none;">
                        <div class="panel panel-info">
                            <div class="panel-body">
                                <h4>Lives in:
                                    <%# Eval("city")%></h4>
                                <h6>&nbsp; &nbsp;Call
                                    <% Response.Write(Session("sexnum"))%>
                                    at
                                    <%# Eval("cell_no") %></h6>
                                <h6>&nbsp; &nbsp;Born on
                                    <%# Eval("dob", "{0:D}")%></h6>
                                <h6>&nbsp; &nbsp;And is
                                    <%# Eval("mode_1") %>
                                    all the time, when he isn't
                                    <%# Eval("mode_2") %>.</h6>
                                <h6>Generally likes:</h6>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    </div>
    <h1>Your Writes</h1>
    <br />
    <div class="col-sm-5">
        <div class="search-form">
            <div class="form-group has-feedback">
                <label for="search" class="sr-only">
                    Search</label>
                <asp:TextBox type="text" class="form-control inputsea" ID="seareq" runat="server"
                    AutoPostBack="True" OnTextChanged="txtSearch_KeyUp" onkeyup="SetDelayreq();" />
                <span class="glyphicon glyphicon-search form-control-feedback"></span>
            </div>
        </div>
    </div>
    <hr />
    <br />
    <!--WRITES BAR-->
    <div id="writes">
        <br />
        <!--FOLLOWING BAR -->
        <div class="panel panel-info col-sm-3 col-sm-offset-1 pull-right">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-target="#foll" href="#foll" class="collapsed" style="text-decoration: none"
                        id="afoll">Following </a>
                </h4>
            </div>
            <!--HTML FOR FRIENDBOX!-->
            <div id="foll" class="col-sm-12" style="border: none;">
              <br />
                    <asp:Repeater ID="Repeater8" DataSourceID="following_pro_data" runat="server">
                        <ItemTemplate>
                              <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>">
                            <div class="panel panel-default panel-body">
                          
                                <div class="list-group-item-heading col-sm-5">
                                    <h3>
                                        <%# Eval("fname")%><br />
                                        <%# Eval("lname")%></h3>
                                </div>
                                <div class="col-sm-6 pull-right">
                                    <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility'
                                        class="img-responsive img-circle" />
                                </div>
                           
                                </div>
                                   </a>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:sqldatasource ID="following_pro_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                        SelectCommand="SELECT TOP 3 * FROM ([userinfo] LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([group_to_following] = @me)">
                        <SelectParameters>
                            <asp:SessionParameter Name="me" SessionField="idsess" Type="String" />
                        </SelectParameters>
                    </asp:sqldatasource>
              
                <a href="Feelpals.aspx" class="btn btn-primary fa fa-users pull-right">&nbsp; See All</a>
                <br /><br /><br />
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!--FEED-->
                <div class="container main-container col-sm-8" style="padding: 0px 0px 0px 110px">
                    <div role="tabpanel" class="tab-pane active" id="panel-1">
                        <div class="row masonry-container">
                            <asp:Repeater ID="Repeater9" runat="server">
                                <ItemTemplate>
                                    <div class="col-sm-6 item">
                                        <div class="panel panel-default">
                                            <div class="panel-heading" style="background-color: #f9fcfe;">
                                                <span> <%# Eval("hashes")%></span>
                                            </div>
                                            <div class="panel-image">
                                                <center style="background-color: black;">
                                                    <img class="img-responsive enlargewrite" src='<%# Eval("img_att") %>'></center>
                                                <div style="padding-left: 10px;">
                                                    <h2><a style="text-decoration: none">
                                                        <%# Eval("date_written", "{0:MMMM d, yyyy}")%></a></h2>
                                                    <h5 class="text-muted">&nbsp;&nbsp;&nbsp;feeling
                                            <%# Eval("feeling_type")%></h5>
                                                    <br />
                                                    <pre style="background-color: White; border: 0px;"><%# Eval("content")%></pre>

                                                </div>
                                                <hr />
                                            </div>
                                            <div class="panel-body hide" style="padding: 0;">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <div class="blog-comment" style="max-height: 200px; overflow-y: scroll; background-color: #f7fbfd;">
                                                            <ul class="comments">
                                                                <br />
                                                                <asp:Repeater ID="Repeater2" runat="server">
                                                                    <ItemTemplate>
                                                                        <li class="clearfix">
                                                                            <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" class="avatar col-sm-2" style="text-decoration: none; padding-top: 25px;">
                                                                                <img alt=' ' src='<%# Eval("dp_url")%>' /></a>
                                                                            <div class="post-comments">
                                                                               <p class="meta"><a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;"><%# Eval("uname")%></a><asp:LinkButton visible='<%# if (DataBinder.Eval(Container.DataItem, "profile_id") = Session("idsess"), "True","False") %>' id="LinkButton2" runat="server" commandargument='<%# Eval("comment_id") %>' oncommand="delcom" Cssclass="pull-right" tooltip="Delete comment." style="text-decoration:none; color:#008cba; padding-top:5px;">&nbsp;<i class="fa fa-trash"></i>&nbsp;</asp:LinkButton>&nbsp;<span class="pull-right" data-livestamp="<%# Eval("date_written_c", "{0:f}")%>"></span></p>
                                                                                <p style="font-family: 'PT Sans Caption', sans-serif !important; line-height: 1.6; color: inherit; font-weight: lighter;">
                                                                                    <%# Eval("content_c")%>
                                                                                </p>
                                                                            </div>
                                                                        </li>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ul>
                                                        </div>
                                                        <hr />
                                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="LinkButton6">
                                                            <div class="input-group">
                                                                <input class="form-control pi" id="Text1" runat="server" type="text" placeholder="..." />
                                                                <span class="input-group-addon">
                                                                    <asp:LinkButton ID="LinkButton6" runat="server" OnCommand="comment_post" CssClass="btn btn-success btn-xs fa fa-plus"
                                                                        CommandArgument='<%# Eval("writes_id") %>'>
                                                                    </asp:LinkButton>
                                                                </span>
                                                            </div>
                                                        </asp:Panel>
                                                        <br />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID='LinkButton6' EventName="command" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="panel-footer clearfix" style="background-color: #d9edf7">
                                                <asp:LinkButton ID="LinkButton3" runat="server" OnCommand="unpub" CssClass="btn btn-primary btn-xs pull-right"
                                                    CommandArgument='<%# Eval("writes_id") %>'>
                                            <i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                                <span class="toggler fa fa-chevron-down pull-left" onclick="var tog = $(this); var secondDiv = tog.parent().prev();var firstDiv = secondDiv.prev();var $container = $('.masonry-container');firstDiv.children('p').toggleClass('hide');return false;secondDiv.toggleClass('hide');tog.toggleClass('fa fa-chevron-up fa fa-chevron-down'); $container.masonry({columnWidth: '.item',itemSelector: '.item'});alert('ahan');" style="color: #008cba">&nbsp;Comments</span>
                                            </div>
                                        </div>
                                     
                                    </div>
                                  
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!--/.masonry-container  -->
                    </div>
                    <!--/.tab-panel -->
                </div>
                <!--/.masonry-container  -->
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID='seareq' EventName="textchanged" />
                <asp:AsyncPostBackTrigger ControlID='mucha' EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='mocha' EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='bocha' EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='focha' EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='tvcha' EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='techa' EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='plcha' EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='hocha' EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='incha' EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='incha' EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Repeater runat="server" ID="feed_modals">
            <ItemTemplate>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <script type="text/javascript">

        function RefreshUpdatePanelreq() {
            __doPostBack('<%= seareq.ClientID %>', '');
        };

        function SetDelayreq() {
            setTimeout("RefreshUpdatePanelreq()", 100);
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mypro" runat="Server">
    <ul id="menu" class="sidebar-nav nav-pills nav-stacked">
        <asp:Repeater ID="proinfoid" DataSourceID="proinfo" runat="server">
            <ItemTemplate>
                <br />
                <br /><br /><br />
                <li class="col-sm-12">
                    <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility'
                        class="img-responsive" onclick="openupdp();" />
                </li>
                <br />
            </ItemTemplate>
        </asp:Repeater>
        <asp:sqldatasource ID="proinfo" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
            SelectCommand="SELECT * FROM [userinfo] WHERE ([profile_id] = @me)">
            <SelectParameters>
                <asp:SessionParameter Name="me" SessionField="idsess" Type="String" />
            </SelectParameters>
        </asp:sqldatasource>
        <!--  <asp:LinkButton CssClass="col-sm-6 btn btn-info" ID="LinkButton3s3" runat="server"
            OnClick="btn_upload_Click">
                                                        Change Picture</asp:LinkButton>
        <asp:FileUpload ID="FileUpload1" CssClass="col-sm-6 btn btn-info" runat="server" />
        <br /> -->
        <asp:Repeater ID="Repeater10" runat="server">
            <ItemTemplate>
                <center>
                    <asp:UpdatePanel ID="statup" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="tab-content">
                                <div id="currstat" class=" tab-pane active fade in">
                                    <div class="col-sm-12">
                                        <p class="text-muted">
                                            <i class="fa fa-quote-left fa-2x" style="color: #008cba"></i>&nbsp;&nbsp;&nbsp;<%# Eval("about_me")%>
                                        </p>
                                    </div>
                                    <a href="#changestat" class="fa fa-pencil btn btn-success btn-circle" data-toggle="tab"></a>
                                </div>
                                <div id="changestat" class="col-sm-12 tab-pane fade">
                                    <i class="fa fa-quote-left fa-2x" style="color: #eee6d8"></i>
                                    <br />
                                    <br />
                                    <input type="text" class="form-control" id="stattext" runat="server" value='<%# Eval("about_me")%>' />
                                    <br />
                                    <asp:LinkButton ID="savestat" runat="server" CssClass="btn btn-success col-sm-6"
                                        OnClick="changestatus">Update</asp:LinkButton>
                                    <a href="#currstat" class="btn btn-default col-sm-6" data-toggle="tab">Cancel</a>
                                    <div>
                                        <br />
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="savestat" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div>
                        <center>
                            <h3 style="color: #008cba">-
                                <%# Eval("fname")%>
                                <%# Eval("lname")%></h3>
                        </center>
                    </div>
                </center>
                <li><a href="Settings.aspx"><span class="fa-stack fa-lg pull-left"><i class="fa fa-cog fa-stack-1x "></i></span>Edit Profile</a> </li>
                <br />
                <br />
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
