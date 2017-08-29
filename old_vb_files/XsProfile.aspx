<%@ Page Title="" Language="VB" MasterPageFile="~/Profile.master" AutoEventWireup="false"
    EnableSessionState="True" CodeFile="XsProfile.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Info" runat="Server">

    <div class="tab-content">
        <div class="tab-pane fade active in" id="mainpro">
            <asp:Repeater runat="server" DataSourceID="sqldatasource3">
                <ItemTemplate>
                    <div class="jumbotron" style="width: 100%; height: 500px; background-color: #f3f3f3; background-image: url(<%#Eval("cover_url") %>); background-position: center; background-size: cover; overflow: hidden;">
                </ItemTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="sqldatasource3" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                SelectCommand="SELECT * FROM [userinfo] WHERE ([profile_id] = @me)">
                <SelectParameters>
                    <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
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
                    <a data-toggle="tab" style="border-radius: 9px; opacity: 0.9;" href="#photospro"
                        class="col-sm-12 list-group-item pull-right">
                        <center>
                            <i class="fa fa-camera-retro "></i>&nbsp; See Photos</center>
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
                                <div id="music" class="col-sm-6 col-sm-offset-3 tab-pane fade active in">
                                    <br />
                                    <ul class="nav nav-pills nav-stacked" align="center">
                                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="fav_music_data">
                                            <ItemTemplate>
                                                <li class="disabled"><a href="#">
                                                    <%# Eval("m_name")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="fav_music_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM ([music_data] INNER JOIN [music_junc] ON [music_data].[music_d_id]=[music_junc].[music_d_id]) WHERE ([profile_id] = @me)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ul>
                                </div>
                                <div id="movies" class="col-sm-6 col-sm-offset-3 tab-pane fade">
                                    <br />
                                    <ul class="nav nav-pills nav-stacked" align="center">
                                        <asp:Repeater ID="Repeater2" runat="server" DataSourceID="fav_movies_data">
                                            <ItemTemplate>
                                                <li class="disabled"><a href="#">
                                                    <%# Eval("d_name")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="fav_movies_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM ([movies_data] INNER JOIN [movies_junc] ON [movies_data].[movies_d_id]=[movies_junc].[movies_d_id]) WHERE ([profile_id] = @me)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ul>
                                </div>
                                <div id="books" class="col-sm-6 col-sm-offset-3 tab-pane fade">
                                    <br />
                                    <ul class="nav nav-pills nav-stacked" align="center">
                                        <asp:Repeater ID="Repeater3" runat="server" DataSourceID="fav_books_data">
                                            <ItemTemplate>
                                                <li class="disabled"><a href="#">
                                                    <%# Eval("d_name")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="fav_books_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM ([books_data] INNER JOIN [books_junc] ON [books_data].[books_d_id]=[books_junc].[books_d_id]) WHERE ([profile_id] = @me)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ul>
                                </div>
                                <div id="food" class="col-sm-6 col-sm-offset-3 tab-pane fade">
                                    <br />
                                    <ul class="nav nav-pills nav-stacked" align="center">
                                        <asp:Repeater ID="Repeater4" runat="server" DataSourceID="fav_food_data">
                                            <ItemTemplate>
                                                <li class="disabled"><a href="#">
                                                    <%# Eval("d_name")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="fav_food_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM ([food_data] INNER JOIN [food_junc] ON [food_data].[food_d_id]=[food_junc].[food_d_id]) WHERE ([profile_id] = @me)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ul>
                                </div>
                                <div id="tvshows" class="col-sm-6 col-sm-offset-3 tab-pane fade">
                                    <br />
                                    <ul class="nav nav-pills nav-stacked" align="center">
                                        <asp:Repeater ID="Repeater5" runat="server" DataSourceID="fav_tv_data">
                                            <ItemTemplate>
                                                <li class="disabled"><a href="#">
                                                    <%# Eval("tv_d")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="fav_tv_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM ([tv_data] INNER JOIN [tv_junc] ON [tv_data].[tv_d_id]=[tv_junc].[tv_d_id]) WHERE ([profile_id] = @me)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ul>
                                </div>
                                <div id="sports" class="col-sm-6 col-sm-offset-3 tab-pane fade">
                                    <br />
                                    <ul class="nav nav-pills nav-stacked" align="center">
                                        <asp:Repeater ID="Repeater6" runat="server" DataSourceID="fav_team_data">
                                            <ItemTemplate>
                                                <li class="disabled"><a href="#">
                                                    <%# Eval("team_d")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="fav_team_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM ([team_data] INNER JOIN [team_junc] ON [team_data].[team_d_id]=[team_junc].[team_d_id]) WHERE ([profile_id] = @me)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ul>
                                </div>
                                <div id="places" class="col-sm-6 col-sm-offset-3 tab-pane fade">
                                    <br />
                                    <ul class="nav nav-pills nav-stacked" align="center">
                                        <asp:Repeater ID="Repeater7" runat="server" DataSourceID="fav_places_data">
                                            <ItemTemplate>
                                                <li class="disabled"><a href="#">
                                                    <%# Eval("places_d")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="fav_places_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM ([places_data] INNER JOIN [places_junc] ON [places_data].[places_d_id]=[places_junc].[places_d_id]) WHERE ([profile_id] = 2)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ul>
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
                                    <br />
                                    <ul class="nav nav-pills nav-stacked" align="center">
                                        <asp:Repeater ID="Repeater22" DataSourceID="hobbies_data" runat="server">
                                            <ItemTemplate>
                                                <li class="disabled"><a href="#">
                                                    <%# Eval("int_fav")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="hobbies_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM ([int_fav_junc] INNER JOIN [int_fav] ON [int_fav_junc].[int_fav_id]=[int_fav].[int_fav_id]) WHERE ([profile_id] = @me)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ul>
                                </div>
                                <div class="tab-pane fade" id="int">
                                    <br />
                                    <ul class="nav nav-pills nav-stacked" align="center">
                                        <asp:Repeater ID="Repeater32" DataSourceID="int_data" runat="server">
                                            <ItemTemplate>
                                                <li class="disabled"><a href="#">
                                                    <%# Eval("int_name")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:SqlDataSource ID="int_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM ([int_junc] INNER JOIN [int] ON [int_junc].[int_id]=[int].[int_id]) WHERE ([profile_id] = @me)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ul>
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
                            <asp:SqlDataSource ID="test_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                SelectCommand="SELECT * FROM ([userinfo] LEFT JOIN [tests] ON [userinfo].[profile_id]=[tests].[from]) WHERE ([to] = @me) ORDER BY [date_written] DESC">
                                <SelectParameters>
                                    <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
                <div id="baseinfo" class="tab-pane fade" style="opacity: 0.9;">
                    <ul class="pagination col-sm-12">
                        <li class="col-sm-offset-1" href="#homeob" data-toggle="tab" style="color: White; cursor: pointer;">
                            <h5>&larr;&nbsp;Back To OpenBook</h5>
                        </li>
                    </ul>
                    <asp:Repeater ID="Repeater12" runat="server" DataSourceID="sqldatasource3">
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
                                            all the time, when <% Response.Write(Session("sexdisp2"))%> isn't
                                            <%# Eval("mode_2") %>.</h6>

                                    </div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:SqlDataSource ID="sqldatasource4" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                        SelectCommand="SELECT * FROM [userinfo] WHERE ([profile_id] = @me)">
                        <SelectParameters>
                            <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
        <h1>
            <% Response.Write(Session("sexdisp"))%>
            Writes &nbsp;
        </h1>
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
                <div id="foll" class="col-sm-12">
                    <br />
                    <asp:Repeater ID="Repeater1x" runat="server" DataSourceID="following_pro_data">
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
                    <asp:SqlDataSource ID="following_pro_data" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                        SelectCommand="SELECT TOP 3 * FROM ([userinfo] LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([group_to_following] = @me)">
                        <SelectParameters>
                            <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <a data-toggle="tab" href="#follpro" style="text-decoration: none" class="btn btn-primary fa fa-users pull-right">&nbsp; See All</a>
                    <br />
                    <br />
                    <br />
                </div>
            </div>
            <!--FEED-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="container-fluid main-container col-sm-8" style="padding: 0px 0px 0px 110px">
                        <div class="well-lg" id="res_div" style="display: none" runat="server">
                            <div class="jumbotron">
                                <h2>&nbsp; &nbsp; You cannot see these writes as the user has restricted from sharing
                                    with
                                    <%Response.Write(Session("sharestatus"))%>.</h2>
                            </div>
                        </div>
                        <div class="row masonry-container" id="feeddiv" runat="server">
                            <asp:Repeater ID="Repeater8" runat="server">
                                <ItemTemplate>
                                    <div class="col-sm-6 item">
                                        <div class="panel panel-default">
                                            <div class="panel-heading" style="background-color: #f9fcfe;">
                                                <h3><a id="HyperLink1" href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>"
                                                    style="text-decoration: none;">
                                                    <%# Eval("uname")%></a></h3>
                                                -<span> <%# Eval("hashes")%></span>
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
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <div class="blog-comment" style="max-height: 200px; overflow-y: scroll; background-color: #f7fbfd;">
                                                            <ul class="comments">
                                                                <br />
                                                                <asp:Repeater ID="Repeater2x" runat="server">
                                                                    <ItemTemplate>
                                                                        <li class="clearfix">
                                                                            <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" class="avatar col-sm-2" style="text-decoration: none; padding-top: 25px;">
                                                                                <img alt=' ' src='<%# Eval("dp_url")%>' /></a>
                                                                            <div class="post-comments">
                                                                                <p class="meta"><a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;"><%# Eval("uname")%></a><asp:LinkButton Visible='<%# if (DataBinder.Eval(Container.DataItem, "profile_id") = Session("idsess"), "True","False") %>' ID="LinkButton2" runat="server" CommandArgument='<%# Eval("comment_id") %>' OnCommand="delcom" CssClass="pull-right" ToolTip="Delete comment." Style="text-decoration: none; color: #008cba; padding-top: 5px;">&nbsp;<i class="fa fa-trash"></i>&nbsp;</asp:LinkButton>&nbsp;<span class="pull-right" data-livestamp="<%# Eval("date_written_c", "{0:f}")%>"></span></p>
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
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="like_post" CssClass="btn btn-danger btn-xs pull-right"
                                                    CommandArgument='<%# Eval("writes_id") %>'>
                                            <i class="glyphicon glyphicon-heart"></i></asp:LinkButton>
                                                <span class="toggler fa fa-chevron-down pull-left" onclick="var tog = $(this); var secondDiv = tog.parent().prev();var firstDiv = secondDiv.prev();var $container = $('.masonry-container');firstDiv.children('p').toggleClass('hide');return false;secondDiv.toggleClass('hide');tog.toggleClass('fa fa-chevron-up fa fa-chevron-down'); $container.masonry({columnWidth: '.item',itemSelector: '.item'});alert('ahan');" style="color: #008cba">&nbsp;Comments</span>
                                            </div>
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!--/.masonry-container  -->
                    </div>
                    <!--/.masonry-container  -->
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID='seareq' EventName="textchanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <script type="text/javascript">

            function RefreshUpdatePanelreq() {
                __doPostBack('<%= seareq.ClientID %>', '');
            };

            function SetDelayreq() {
                setTimeout("RefreshUpdatePanelreq()", 100);
            }
        </script>
    </div>
    <div class="tab-pane fade in" id="photospro">
        <asp:Repeater ID="Repeater9" runat="server" DataSourceID="sqldatasource1">
            <ItemTemplate>
                <h1>Photos of <a href="#mainpro" data-toggle="tab" style="text-decoration: none">
                    <%# Eval("fname")%></a></h1>
                <br />
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource ID="sqldatasource1" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
            SelectCommand="SELECT * FROM [userinfo] WHERE [profile_id] = @me">
            <SelectParameters>
                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div class="col-sm-5">
            <div class="search-form">
                <div class="form-group has-feedback">
                    <label for="search" class="sr-only">
                        Search</label>
                    <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seapho"
                        runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp_pho" onkeyup="SetDelaypho();" />
                    <span class="glyphicon glyphicon-search form-control-feedback"></span>
                </div>
            </div>
        </div>
        <hr />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="col-sm-2">
                    <ul class="list-group">
                        <br />
                        <br />
                        <asp:LinkButton ID="allclick" OnClick="allpic" CssClass="list-group-item" runat="server">All</asp:LinkButton>
                        <asp:LinkButton ID="youclick" OnClick="youpic" CssClass="list-group-item" runat="server">
                            <asp:Label runat="server" ID="pho_disp_sex"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="tripclick" OnClick="trippic" CssClass="list-group-item" runat="server">Trips</asp:LinkButton>
                        <asp:LinkButton ID="parclick" OnClick="parpic" CssClass="list-group-item" runat="server">Parties</asp:LinkButton>
                        <asp:LinkButton ID="eveclick" OnClick="evepic" CssClass="list-group-item" runat="server">Events</asp:LinkButton>
                        <asp:LinkButton ID="obclick" OnClick="obpic" CssClass="list-group-item" runat="server">Openbook</asp:LinkButton>
                        <asp:LinkButton ID="dpclick" OnClick="dppic" CssClass="list-group-item" runat="server">Profile Photos</asp:LinkButton>

                    </ul>
                </div>
                <div class="col-sm-10 col-sm-offset-0" id="youtabpics">
                    <div class="well-lg" id="Div3" style="display: none" runat="server">
                        <div class="jumbotron">
                            <h2>&nbsp; &nbsp; You cannot see these pictures as the user has restricted from sharing
                                with
                                <%Response.Write(Session("sharestatus"))%>.</h2>
                        </div>
                    </div>
                    <div class="well-lg" id="empdiv" style="display: none" runat="server">
                        <div class="jumbotron">
                            <h2>&nbsp; &nbsp; No pictures here. :O</h2>
                        </div>
                    </div>
                    <!-- The container for the list of example images -->
                    <div id="links">
                        <div id="polaroid" class="row masonry-container polaroid" style="padding-left: 40px;">
                            <asp:Repeater ID="gallery" runat="server">
                                <ItemTemplate>
                                    <figure class="item">
                                             <a href="img/<%# Eval("user_path") %>/<%# Eval("folder_type_path") %>/<%# Eval("pic_name") %>"
                                                    title="<%# Eval("caption") %>" data-gallery style="text-decoration: none;">
<img src="img/<%# Eval("user_path") %>/<%# Eval("folder_type_path") %>/<%# Eval("pic_name") %>" alt="<%# Eval("caption") %>" class="img-responsive" /> </a>
<figcaption>&nbsp;</figcaption>
</figure>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <!-- The Bootstrap Image Gallery lightbox, should be a child element of the document body -->
                    <div id="blueimp-gallery" class="blueimp-gallery blueimp-gallery-controls" data-use-bootstrap-modal="false">
                        <div class="slides">
                        </div>
                        <a class="prev">‹</a> <a class="next">›</a> <a class="close">×</a> <a class="play-pause"></a>
                        <h3 class="title"></h3>
                        <ol class="indicator">
                        </ol>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="allclick" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="youclick" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="tripclick" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="parclick" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="eveclick" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="obclick" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="dpclick" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID='seapho' EventName="textchanged" />
            </Triggers>
        </asp:UpdatePanel>
        <script type="text/javascript">
            function RefreshUpdatePanelpho() {
                __doPostBack('<%= seapho.ClientID %>', '');
            };

            function SetDelaypho() {
                setTimeout("RefreshUpdatePanelpho()", 100);
            }
        </script>
    </div>
    <div class="tab-pane fade in" id="follpro">
        <asp:Repeater ID="Repeater10" runat="server" DataSourceID="sqldatasource1">
            <ItemTemplate>
                <h1>
                    <a href="#mainpro" data-toggle="tab" style="text-decoration: none">
                        <%# Eval("fname")%>
                    </a>Is Following</h1>
                <br />
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource ID="sqldatasource2" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
            SelectCommand="SELECT * FROM [userinfo] WHERE [profile_id] = @me">
            <SelectParameters>
                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div class="col-sm-5">
            <div class="search-form">
                <div class="form-group has-feedback">
                    <label for="search" class="sr-only">
                        Search</label>
                    <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seafoll"
                        runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp_foll" onkeyup="SetDelayfoll();" />
                    <span class="glyphicon glyphicon-search form-control-feedback"></span>
                </div>
            </div>
        </div>
        <!-- baaki -->
        <hr />
        <br />
        <ul class="nav nav-tabs">
            <li class="active"><a href="#xfollowing" data-toggle="tab" aria-expanded="true">Following</a></li>
        </ul>
        <br />
        <div id="Div4" class="tab-content">
            <div class="tab-pane fade active in" id="following">
                <div class="container col-sm-6 col-sm-offset-1">

                    <div class="panel panel-default" style="border: none;">
                        <ul class="list-group" id="contact-list">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                <ContentTemplate>
                                    <asp:Repeater ID="Repeater11" runat="server">
                                        <ItemTemplate>
                                            <div class="panel panel-default panel-body">
                                                <div class="list-group-item" style="min-height: 160px">
                                                    <div class="list-group-item-heading col-sm-5">
                                                        <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;">
                                                            <h3><%# Eval("fname")%>
                                                                <br />
                                                                <%# Eval("lname")%></h3>
                                                        </a>
                                                        <div id="op" class="panel panel-body fade in pull-left">
                                                            <a class="btn btn-success fa fa-comments-o" data-toggle="modal" data-target="#<%# Eval("profile_id") %>messmodal"></a>&nbsp; <a href="#" class="btn btn-primary fa fa-plus" data-toggle="modal" data-target="#<%# Eval("fname")%>fmodal"></a></a>
                                                        </div>
                                                    </div>
                                                    <div class="list-group-item-text col-sm-3 pull-right">
                                                        <asp:LinkButton ID="dude" runat="server" CommandArgument='<%# Eval("profile_id") %>'>
                                                    <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility' class="img-circle" />
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal fade" id="<%# Eval("fname")%>fmodal">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                ×</button>
                                                            <br />
                                                        </div>
                                                        <div class="modal-body">
                                                            <p>
                                                                Start follwing
                                                                    <%# Eval("fname")%>
                                                                    ?
                                                            </p>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                                Cancel</button>
                                                            <asp:LinkButton CssClass="btn btn-primary" ID="LinkButton2" runat="server" OnCommand="f"
                                                                CommandArgument='<%# Eval("profile_id") %>'>
                                                        Yes</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--Unfollow Modal-->
                                            <div class="modal fade" id="<%# Eval("profile_id") %>messmodal">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                ×</button>
                                                            <br />
                                                            <div class="modal-title">
                                                                New Message to
                                                                    <%# Eval("fname")%>
                                                            </div>
                                                        </div>
                                                        <div class="modal-body">
                                                            <input type="text" class="form-control" id="messtxt" runat="server" />
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                                Cancel</button>
                                                            <asp:LinkButton CssClass="btn btn-primary" ID="LinkButton1" runat="server" OnCommand="sendmess"
                                                                CommandArgument='<%# Eval("profile_id") %>'>
                                                        Send</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- New Message Modal-->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID='seafoll' EventName="textchanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ul>
                    </div>
                </div>

            </div>
        </div>
        <div class="panel panel-info col-sm-3 pull-right">
            <div class="panel-heading">
                <h3 class="panel-title">Recent Activity</h3>
            </div>
            <div class="panel-body" style="max-height: 350px; min-height: 50px; overflow-y: scroll;">
                <asp:Repeater ID="notifrepeater" runat="server">
                    <ItemTemplate>
                        <ul class="list-group activity-list">
                            <li class="list-group-item"><i class="glyphicon <%# If(DataBinder.Eval(Container.DataItem, "type") = "started following", "glyphicon-user", If(DataBinder.Eval(Container.DataItem, "type") = "commented on a", "glyphicon-bookmark", If(DataBinder.Eval(Container.DataItem, "type") = "wrote a new", "glyphicon-pencil", If(DataBinder.Eval(Container.DataItem, "type") = "liked a", "glyphicon-heart", If(DataBinder.Eval(Container.DataItem, "type") = "updated", "glyphicon-cog", If(DataBinder.Eval(Container.DataItem, "type") = "uploaded", "glyphicon-film", If(DataBinder.Eval(Container.DataItem, "type") = "tagged", "glyphicon-bookmark", "glyphicon-bookmark")))))))%>"></i>&nbsp; <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;"
                                style="text-decoration: none;">
                                <%# Eval("uname")%></a>
                                <%# Eval("type")%>
                               <a style="text-decoration: none;" href='<%# If(DataBinder.Eval(Container.DataItem, "type") = "tagged", "seepost.aspx?id=" & DataBinder.Eval(Container.DataItem, "post"), If(DataBinder.Eval(Container.DataItem, "type") = "wrote a new", "seepost.aspx?id=" & DataBinder.Eval(Container.DataItem, "post"), If(DataBinder.Eval(Container.DataItem, "type") = "liked a", "seepost.aspx?id=" & DataBinder.Eval(Container.DataItem, "post"), If(DataBinder.Eval(Container.DataItem, "type") = "commented on a", "seepost.aspx?id=" & DataBinder.Eval(Container.DataItem, "post"),If(DataBinder.Eval(Container.DataItem, "type") = "published a", "seepost.aspx?id=" & DataBinder.Eval(Container.DataItem, "post"), If(DataBinder.Eval(Container.DataItem, "type") = "uploaded new", "XsProfile.aspx?profileid=" & DataBinder.Eval(Container.DataItem, "doer"), If(DataBinder.Eval(Container.DataItem, "type") = "started following", "XsProfile.aspx?profileid=" & DataBinder.Eval(Container.DataItem, "doee"), "#")))))))%>'>
                                    <%# If(DataBinder.Eval(Container.DataItem, "type") = "started following", "new people.", If(DataBinder.Eval(Container.DataItem, "type") = "commented on a", "post.", If(DataBinder.Eval(Container.DataItem, "type") = "wrote a new", "post.", If(DataBinder.Eval(Container.DataItem, "type") = "liked a", "post.", If(DataBinder.Eval(Container.DataItem, "type") = "updated", "their profile.", If(DataBinder.Eval(Container.DataItem, "type") = "uploaded new", "photos.", If(DataBinder.Eval(Container.DataItem, "type") = "tagged", If(DataBinder.Eval(Container.DataItem, "doee") = Session("idsess"), "you in a post.", "someone in a post."), "post.")))))))%></a>
                                <br />
                                <br />
                                <span class="pull-right text-muted small time-line" data-livestamp="<%# Eval("date_time_written", "{0:f}")%>"></span>
                                <br />
                            </li>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <script type="text/javascript">
            function RefreshUpdatePanelfoll() {
                __doPostBack('<%= seafoll.ClientID %>', '');
            };

            function SetDelayfoll() {
                setTimeout("RefreshUpdatePanelfoll()", 100);
            }


        </script>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pro" runat="Server">
    <ul id="menu" class="sidebar-nav nav-pills nav-stacked">
        <asp:Repeater runat="server" ID="pro_rep" DataSourceID="proinfo">
            <ItemTemplate>
                <br />
                <br /><br /><br />
                <li class="col-sm-12">
                    <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility'
                        class="img-responsive" />
                </li>
                <center>
                    <div class="col-sm-12">
                        <p class="text-muted">
                            <i class="fa fa-quote-left fa-2x" style="color: #008cba"></i>&nbsp;&nbsp;&nbsp;<%# Eval("about_me")%>
                        </p>
                    </div>
                    <div>
                        <center>
                            <h3 style="color: #008cba">-
                                <%# Eval("fname")%>
                                <%# Eval("lname")%></h3>
                        </center>
                    </div>
                </center>
                <!--<li class="well-sm">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <h4>
                                Lives in:
                                <%# Eval("city")%></h4>
                            <h6>
                                &nbsp; &nbsp;Call
                                <% Response.Write(Session("sexnum"))%>
                                at
                                <%# Eval("cell_no")%></h6>
                            <h6>
                                &nbsp; &nbsp;Born on
                                <%# Eval("dob", "{0:D}")%></h6>
                            <h6>
                                &nbsp; &nbsp;And is
                                <%Response.Write(Session("1stmodefeel"))%>
                                all the time, when he isn't
                                <%Response.Write(Session("2ndmodefeel"))%>.</h6>
                        </div>
                    </div>
                </li> -->
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater DataSourceID="proinfo" runat="server" ID="follbutts" Visible="false">
            <ItemTemplate>
                <center>

                    <a href="#" class="fa fa-star btn btn-info btn-circle" data-toggle="modal" data-target="#<%# Eval("profile_id")%>writetestmodal"></a><span class="block">&nbsp;</span><a href="#" class="fa fa-envelope btn btn-info btn-circle"
                        data-toggle="modal" data-target="#<%# Eval("profile_id")%>messmodal"></a>
                    <br />
                    <a href="#" class="fa fa-minus btn btn-warning btn-circle" data-toggle="modal" data-target="#<%# Eval("fname")%>unfmodal"></a>
                    <br />
                    <br />
                    <a href="#" class="fa fa-exclamation btn btn-danger btn-circle" data-toggle="modal"
                        data-target="#<%# Eval("profile_id")%>repmodal"></a>
                </center>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater DataSourceID="proinfo" runat="server" ID="nfollbutts" Visible="false">
            <ItemTemplate>
                <div id="nfoll" runat="server">
                    <center>

                        <a href="#" id="reqfolbut" class="fa fa-plus btn btn-info btn-circle" data-toggle="modal" data-target='#<%# Eval("profile_id")%>fmodal'></a>
                        <br />
                        <br />
                        <a href="#" class="fa fa-exclamation btn btn-danger btn-circle" data-toggle="modal"
                            data-target="#<%# Eval("profile_id")%>repmodal"></a>
                    </center>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource ID="proinfo" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
            SelectCommand="SELECT * FROM [userinfo] WHERE ([profile_id] = @me)">
            <SelectParameters>
                <asp:SessionParameter Name="me" SessionField="prosess" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
      
            <br />
            <br />
            <li><a href="Feelpals.aspx"><span class="fa-stack fa-lg pull-left"><i class="fa fa-users fa-stack-1x "></i></span>Feelpals</a> </li>
            <li><a href="Messages.aspx"><span class="fa-stack fa-lg pull-left"><i class="fa fa-comments fa-stack-1x "></i></span>Messages</a></li>
            <li><a href="MyPhotos.aspx"><span class="fa-stack fa-lg pull-left"><i class="fa fa-camera-retro fa-stack-1x "></i></span>Photos</a> </li>
            <li><a href="MyDiary.aspx"><span class="fa-stack fa-lg pull-left"><i class="fa fa-book fa-stack-1x "></i></span>My Diary</a> </li>
            <li><a href="Explore.aspx"><span class="fa-stack fa-lg pull-left"><i class="fa fa-search fa-stack-1x "></i></span>Explore</a> </li>

      
    </ul>
</asp:Content>
