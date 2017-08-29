<%@ Page Title="" Language="VB" MasterPageFile="~/UI.master" AutoEventWireup="false" CodeFile="Explore.aspx.vb" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body_cont" Runat="Server">
    <div class="col-sm-offset-2">
    <h1>Explore
    </h1>
    <br />
    <div class="tab-pane active fade in col-sm-12" id="innsbut">
        <a class="fa fa-eye btn btn-info btn-circle" data-toggle="collapse" data-target="#inns"></a><span class="text-muted">&nbsp;&nbsp;More Search Options.</span>
        <br />
        <br />
    </div>
    <div class="collapse col-sm-8 col-sm-offset-2" id="inns">
        <div class="panel-body">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#posts" data-toggle="tab" aria-expanded="true">Posts</a></li>
                <li class=""><a href="#friends" data-toggle="tab" aria-expanded="false">Friends</a></li>
            </ul>
            <div id="myTabContent" class="tab-content">
                <!-- SEARCH POST MODULE-->
                <div class="tab-pane fade active in" id="posts">
                    <!-- FACTION-WISE SEARCH-->
                    <br />
                    <!-- HASHTAG-WISE SEARCH-->
                    <div class="form-group col-sm-6">
                        <label for="textArea" class="col-sm-2 control-label">
                            Hashtags:</label>
                        <br />
                        <div>
                            <input id="post_hash_txt" class="form-control" runat="server" placeholder="#chocolatedipped #deepfried #sandals"
                                type="text" />
                            <span class="help-block">Search speific to a hashtag?</span>
                        </div>
                    </div>
                    <div class="form-group col-sm-6">
                        <!-- FEELING-WISE SEARCH-->
                        <label for="select" class="col-sm-2 control-label">
                            Feeling:</label>
                        <input id="post_feel_txt" class="form-control" runat="server" placeholder="See a particularly feel-y post?"
                            type="text" />
                    </div>
                    <!-- LOCATION BASED SEARCH-->
                    <div class="col-sm-10 col-sm-offset-2">
                        <asp:LinkButton ID="possearch" runat="server" OnClick="search_posts" CssClass="btn btn-default">
                                Search</asp:LinkButton>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
                <!-- SEARCH FRIEND MODULE-->
                <div class="tab-pane fade" id="friends">
                    <!-- MOSTLY-FEELING-WISE SEARCH-->
                    <div class="form-group col-sm-6">
                        <br />
                        <label for="select" class="col-sm-12 control-label">
                            Mostly Feeling:</label>
                        <br />
                        <input type="text" class="form-control" id="feel_pals_txt" runat="server" placeholder="Search by feelings too?" />
                    </div>
                    <!-- COMFORT-WISE SEARCH -->
                    <div class="form-group col-sm-6">
                        <br />
                        <br />
                        <label>
                            <input type="checkbox" id="sim_int_pals_check" runat="server" />
                            Similar Hobbies & Interests?
                        </label>
                        <br />
                        <label>
                            <input type="checkbox" id="sim_fav_pals_check" runat="server" />
                            Similar Favourites?
                        </label>
                        <br />
                        <br />
                    </div>
                    <!-- LOCATION BASED SEARCH-->
                    <div class="form-group">
                        <div class="col-sm-10 col-sm-offset-2">
                            <asp:LinkButton ID="fpsea" runat="server" OnClick="search_friends" CssClass="btn btn-default">
                                    Search</asp:LinkButton>
                            <asp:LinkButton ID="fpseanm" runat="server" OnClick="search_friends_nearme" CssClass="btn btn-primary">
                                    Search Near Me!</asp:LinkButton>
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
    </div>
    <div class="col-sm-5">
        <div class="search-form">
            <div class="form-group has-feedback">
                <label for="search" class="sr-only">
                    Search</label>
                <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seahome"
                    placeholder="A John Doe could be search here." runat="server" AutoPostBack="True"
                    OnTextChanged="txtSearch_KeyUp" onkeyup="SetDelay();" />
                <span class="glyphicon glyphicon-search form-control-feedback"></span>
            </div>
        </div>
    </div>
    </div> <br /> <br /> <br />
    <hr />
    <br /><br /><br /></div>
    <%--<div class="tab-content">
        <div class="tab-pane active fade in col-sm-12" id="innsbut">
            <a href="#inns" class="fa fa-eye btn btn-info btn-circle" data-toggle="tab"></a>
            <br />
            <br />
        </div>
        <div class="tab-pane fade col-sm-12" id="inns">
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Inner Soul"> </asp:TextBox><span
                class="input-group-addon">
                <p>
                    Try this: Find me a guy to grab a bite with!</p>
                <asp:LinkButton ID="aiclick" runat="server" OnClick="ai" CssClass="btn btn-success fa fa-plus"> </asp:LinkButton><a
                    href="#innsbut" class="fa fa-remove pull-left" style="text-decoration: none;"
                    data-toggle="tab"> </a></span>
            <br />
            <br />
        </div>
    </div>--%>
    <!-- SEARCH OPTIONS-->

    <div class="tab-content col-sm-offset-1 col-sm-10">
        <div class="tab-pane fade active in" id="topr">
            <ul class="nav nav-tabs col-sm-12">
                <li class="active"><a href="#pyl" data-toggle="tab">Posts You'd Like</a></li>
                <li class=""><a href="#fpa" data-toggle="tab">People Around</a></li>
                <li class=""><a href="#new" data-toggle="tab">Trending</a></li>
            </ul>
            <div class="well-lg" id="empdiv" style="display: none" runat="server">
                <div class="jumbotron">
                    <h2>&nbsp; &nbsp; Your search returned zero results. :O</h2>
                </div>
            </div>
            <div class="tab-content" id="newexp">
                <div class="tab-pane fade active in" id="pyl">

                    <br />
                    <br />
                    <div class="row masonry-container">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="False" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="tab-pane fade active in  col-sm-12" runat="server" id="s_cows">
                                    <div class="row masonry-container">
                                        <br />
                                        <div role="tabpanel" class="tab-pane active" id="Div4">
                                            <asp:Repeater runat="server" ID="s_cows_fill">
                                                <ItemTemplate>
                                                    <div class="col-sm-4 item">
                                                        <div class="thumbnail col-sm-12">
                                                            <div>
                                                                <br />
                                                                <legend class="col-sm-12"><a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;">
                                                                    <%# Eval("uname")%></a> </legend>
                                                                <div class="col-sm-8">
                                                                    <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility' />
                                                                    <br />
                                                                    <br />
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <h3>
                                                                        <%# Eval("fname")%>
                                                                        <%# Eval("lname")%></h3>
                                                                    <p style="min-height: 100px">
                                                                        <%# Eval("about_me")%>
                                                                    </p>
                                                                    <br />
                                                                    <br />
                                                                    <p class="text-muted">
                                                                        Lives In:
                                                <%#Eval("city") %>
                                                                        <a href="#" class="btn btn-primary btn-xs pull-right" role="button"><i class='glyphicon glyphicon-remove'></i></a>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                    <!--/.masonry-container  -->
                                </div>
                                <div id="s_posts" runat="server" class="tab-pane fade active in">
                                    <div class="container main-container col-sm-12">
                                        <div role="tabpanel" class="tab-pane active" id="Div3">
                                            <div class="row masonry-container">
                                                <asp:Repeater ID="s_post_fill" runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-sm-4 item">
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
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
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
                                                                    <span class="toggler fa fa-chevron-down pull-left" onclick="var tog = $(this); var secondDiv = tog.parent().prev();var firstDiv = secondDiv.prev();var $container = $('.masonry-container');firstDiv.children('p').toggleClass('hide');secondDiv.toggleClass('hide');tog.toggleClass('fa fa-chevron-up fa fa-chevron-down'); $container.masonry({columnWidth: '.item',itemSelector: '.item'});return false;" style="color: #008cba">&nbsp;Comments</span>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <!--Explore Post Viewing Modal-->
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <!--/.item  -->
                                            </div>
                                            <!--/.masonry-container  -->
                                        </div>
                                        <!--/.tab-panel -->
                                    </div>
                                    <!--/.masonry-container  -->
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID='seahome' EventName="textchanged" />
                                <asp:AsyncPostBackTrigger ControlID='fpsea' EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID='fpseanm' EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID='possearch' EventName="click" />
                                <%--<asp:AsyncPostBackTrigger ControlID='aiclick' EventName="click" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="tab-pane fade" id="fpa">
                    <br />

                    <div class="container main-container col-sm-12">
                        <br />
                        <div role="tabpanel" class="tab-pane active" id="Div1">
                            <div class="row masonry-container">
                                <asp:Repeater ID="newlymades" runat="server" DataSourceID="newlymadepros">
                                    <ItemTemplate>
                                        <div class="col-sm-4 item">
                                            <div class="thumbnail col-sm-12">
                                                <div>
                                                    <br />
                                                    <legend class="col-sm-12"><a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;">
                                                        <%# Eval("uname")%></a> </legend>
                                                    <div class="col-sm-8">
                                                        <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility' />
                                                        <br />
                                                        <br />
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <h3>
                                                            <%# Eval("fname")%>
                                                            <%# Eval("lname")%></h3>
                                                        <p style="min-height: 100px">
                                                            <%# Eval("about_me")%>
                                                        </p>
                                                        <br />
                                                        <br />
                                                        <p class="text-muted">
                                                            Lives In:
                                                <%#Eval("city") %>
                                                            <a href="#" class="btn btn-primary btn-xs pull-right" role="button"><i class='glyphicon glyphicon-remove'></i></a>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <!--/.item  -->
                                <asp:SqlDataSource ID="newlymadepros" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                    SelectCommand="SELECT TOP 20 * FROM [userinfo] ORDER BY [join_date] DESC"></asp:SqlDataSource>
                            </div>
                            <!--/.masonry-container  -->
                        </div>
                        <!--/.tab-panel -->
                    </div>
                    <!--/.masonry-container  -->
                </div>
                <div class="tab-pane fade" id="new">
                    <div class="main-container col-sm-12">

                        <br />
                        <br />

                        <div class="row masonry-container">

                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Repeater runat="server" ID="pyl_fill">
                                        <ItemTemplate>
                                            <div class="col-sm-4 item">
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
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
                                                            <ContentTemplate>
                                                                <div class="blog-comment" style="max-height: 200px; overflow-y: scroll; background-color: #f7fbfd;">
                                                                    <ul class="comments">
                                                                        <br />
                                                                        <asp:Repeater ID="Repeater2_pyl" runat="server">
                                                                            <ItemTemplate>
                                                                                <li class="clearfix">
                                                                                    <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" class="avatar col-sm-2" style="text-decoration: none; padding-top: 25px;">
                                                                                        <img alt=' ' src='<%# Eval("dp_url")%>' /></a>
                                                                                    <div class="post-comments">
                                                                                        <p class="meta"><a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;"><%# Eval("uname")%></a><asp:LinkButton Visible='<%# if (DataBinder.Eval(Container.DataItem, "profile_id") = Session("idsess"), "True","False") %>' ID="LinkButton2" runat="server" CommandArgument='<%# Eval("comment_id") %>' OnCommand="delcom_pyl" CssClass="pull-right" ToolTip="Delete comment." Style="text-decoration: none; color: #008cba; padding-top: 5px;">&nbsp;<i class="fa fa-trash"></i>&nbsp;</asp:LinkButton>&nbsp;<span class="pull-right" data-livestamp="<%# Eval("date_written_c", "{0:f}")%>"></span></p>
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
                                                                <asp:Panel ID="Panel1_pyl" runat="server" DefaultButton="LinkButton6_pyl">
                                                                    <div class="input-group">
                                                                        <input class="form-control pi" id="Text1_pyl" runat="server" type="text" placeholder="..." />
                                                                        <span class="input-group-addon">
                                                                            <asp:LinkButton ID="LinkButton6_pyl" runat="server" OnCommand="comment_post_pyl" CssClass="btn btn-success btn-xs fa fa-plus"
                                                                                CommandArgument='<%# Eval("writes_id") %>'>
                                                                            </asp:LinkButton>
                                                                        </span>
                                                                    </div>
                                                                </asp:Panel>
                                                                <br />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID='LinkButton6_pyl' EventName="command" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </div>
                                                    <div class="panel-footer clearfix" style="background-color: #d9edf7">
                                                        <asp:LinkButton ID="LinkButton1_pyl" runat="server" OnCommand="like_post" CssClass="btn btn-danger btn-xs pull-right"
                                                            CommandArgument='<%# Eval("writes_id") %>'>
                                            <i class="glyphicon glyphicon-heart"></i></asp:LinkButton>
                                                        <span class="toggler fa fa-chevron-down pull-left" onclick="var tog = $(this); var secondDiv = tog.parent().prev();var firstDiv = secondDiv.prev();var $container = $('.masonry-container');firstDiv.children('p').toggleClass('hide');secondDiv.toggleClass('hide');tog.toggleClass('fa fa-chevron-up fa fa-chevron-down'); $container.masonry({columnWidth: '.item',itemSelector: '.item'});return false;" style="color: #008cba">&nbsp;Comments</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </ItemTemplate>

                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>

        </div>


    </div>
    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= seahome.ClientID %>', '');
        };

        function SetDelay() {
            setTimeout("RefreshUpdatePanel()", 100);
        }

        function newtab(urlred) {
            window.open(urlred);
        }

        function necompstep1() {
            setTimeout(function () {
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-bottom-right",
                    "preventDuplicates": true,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "300",
                    "timeOut": "4300",
                    "extendedTimeOut": "3000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                toastr.success("Yay! :D <br /> So here's a list of people who you might know.");
                setTimeout(function () {
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-bottom-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "300",
                        "timeOut": "4300",
                        "extendedTimeOut": "3000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }
                    toastr.info("They might have studied with you I guess! :O");
                    setTimeout(function () {
                        toastr.options = {
                            "closeButton": false,
                            "debug": false,
                            "newestOnTop": false,
                            "progressBar": false,
                            "positionClass": "toast-bottom-right",
                            "preventDuplicates": true,
                            "onclick": null,
                            "showDuration": "300",
                            "hideDuration": "300",
                            "timeOut": "4300",
                            "extendedTimeOut": "3000",
                            "showEasing": "swing",
                            "hideEasing": "linear",
                            "showMethod": "fadeIn",
                            "hideMethod": "fadeOut"
                        }
                        toastr.success("You go, you awesome thing! <br /> I\'m right here with ya. :)");
                    }, 5000)
                }, 3000)
            }, 3000)

        }

        function necompstep2() {
            setTimeout(function () {
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-bottom-right",
                    "preventDuplicates": true,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "300",
                    "timeOut": "4300",
                    "extendedTimeOut": "3000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                toastr.success("Yay! :D <br /> So here's some folks from your city.");
                setTimeout(function () {
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-bottom-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "300",
                        "timeOut": "4300",
                        "extendedTimeOut": "3000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }
                    toastr.info("Let\'s get started! :O");
                    setTimeout(function () {
                        toastr.options = {
                            "closeButton": false,
                            "debug": false,
                            "newestOnTop": false,
                            "progressBar": false,
                            "positionClass": "toast-bottom-right",
                            "preventDuplicates": true,
                            "onclick": null,
                            "showDuration": "300",
                            "hideDuration": "300",
                            "timeOut": "4300",
                            "extendedTimeOut": "3000",
                            "showEasing": "swing",
                            "hideEasing": "linear",
                            "showMethod": "fadeIn",
                            "hideMethod": "fadeOut"
                        }
                        toastr.success("You can always search for more people! <br /> I\'ll be right here with ya. :)");
                    }, 5000)
                }, 3000)
            }, 3000)

        }

        function necompfail() {
            setTimeout(function () {
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-bottom-right",
                    "preventDuplicates": true,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "300",
                    "timeOut": "4300",
                    "extendedTimeOut": "3000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                toastr.info("Whoops!<br /> Apparently you\'re too old or too young. :c ");
                setTimeout(function () {
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-bottom-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "300",
                        "timeOut": "4300",
                        "extendedTimeOut": "3000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }
                    toastr.info("Let\'s use the search bar, shall we!? :O");
                    setTimeout(function () {
                        toastr.options = {
                            "closeButton": false,
                            "debug": false,
                            "newestOnTop": false,
                            "progressBar": false,
                            "positionClass": "toast-bottom-right",
                            "preventDuplicates": true,
                            "onclick": null,
                            "showDuration": "300",
                            "hideDuration": "300",
                            "timeOut": "10300",
                            "extendedTimeOut": "10000",
                            "showEasing": "swing",
                            "hideEasing": "linear",
                            "showMethod": "fadeIn",
                            "hideMethod": "fadeOut"
                        }
                        toastr.success("You can always search for more people using the tab beneath it! <br /> I\'ll be right here with ya. :)");
                    }, 5000)
                }, 3000)
            }, 3000)

        }

    </script>

</asp:Content>

