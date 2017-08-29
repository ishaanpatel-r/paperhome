<%@ Page Title="" Language="VB" MasterPageFile="~/UI.master" AutoEventWireup="false" CodeFile="Activity.aspx.vb" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body_cont" Runat="Server">
     <center><br /><br />
    <h1>Activity</h1>
    <br />
    <br />
    <ul class="nav nav-tabs col-sm-8 col-sm-offset-2">
        <li class="active"><a href="#notifications" data-toggle="tab">Notifications</a></li>
        <li><a href="#requests" data-toggle="tab">Requests</a></li>
        <li><a href="#log" data-toggle="tab">Your Liked posts!</a></li>
    </ul></center>
    <div id="myTabContent" class="tab-content col-sm-8 col-sm-offset-2">
        <div class="tab-pane fade active in" id="notifications">
            <br />
            <br />
            <div class="col-sm-5">
                <div class="search-form">
                    <div class="form-group has-feedback">
                        <label for="search" class="sr-only">
                            Search</label>
                        <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seanot"
                            runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp_not" onkeyup="SetDelaynot();" />
                        <span class="glyphicon glyphicon-search form-control-feedback"></span>
                    </div>
                </div>
            </div>
            <hr />
            <br />
            <div class="col-sm-8 col-sm-offset-1">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:Repeater ID="notifrepeater" runat="server">
                            <ItemTemplate>
                                <ul class="list-group activity-list">
                                    <li class="list-group-item"><i class="glyphicon <%# If(DataBinder.Eval(Container.DataItem, "type") = "started following", "glyphicon-user", If(DataBinder.Eval(Container.DataItem, "type") = "commented on a", "glyphicon-bookmark", If(DataBinder.Eval(Container.DataItem, "type") = "wrote a new", "glyphicon-pencil", If(DataBinder.Eval(Container.DataItem, "type") = "liked a", "glyphicon-heart", If(DataBinder.Eval(Container.DataItem, "type") = "updated", "glyphicon-cog", If(DataBinder.Eval(Container.DataItem, "type") = "uploaded", "glyphicon-film", If(DataBinder.Eval(Container.DataItem, "type") = "tagged", "glyphicon-bookmark", "glyphicon-bookmark")))))))%>"></i>&nbsp;
                                       <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;" style="text-decoration: none;"><%# Eval("uname")%></a>
                                        <%# Eval("type")%>
                                        <a style="text-decoration: none;" href='<%# If(DataBinder.Eval(Container.DataItem, "type") = "tagged", "seepost.aspx?id=" & DataBinder.Eval(Container.DataItem, "post"), If(DataBinder.Eval(Container.DataItem, "type") = "liked a", "seepost.aspx?id=" & DataBinder.Eval(Container.DataItem, "post"), If(DataBinder.Eval(Container.DataItem, "type") = "commented on a", "seepost.aspx?id=" & DataBinder.Eval(Container.DataItem, "post"), "")))%>'>
                                            <%# If(DataBinder.Eval(Container.DataItem, "type") = "started following", "you.", If(DataBinder.Eval(Container.DataItem, "type") = "commented on a", "post you wrote.",If(DataBinder.Eval(Container.DataItem, "type") = "liked a", "post you wrote.",If(DataBinder.Eval(Container.DataItem, "type") = "tagged", "you in a post.", "UNDEFINED"))))%></a>
                                        <br />
                                        <br />
                                        <span class="pull-right text-muted small time-line" data-livestamp="<%# Eval("date_time_written", "{0:f}")%>"></span>
                                        <br />
                                    </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID='seanot' EventName="textchanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="tab-pane fade" id="requests">
            <br />
            <br />
            <div class="col-sm-5">
                <div class="search-form">
                    <div class="form-group has-feedback">
                        <label for="search" class="sr-only">
                            Search</label>
                        <asp:TextBox type="text" class="form-control inputsea" ID="seareq" runat="server"
                            AutoPostBack="True" OnTextChanged="txtSearch_KeyUp_req" onkeyup="SetDelayreq();" />
                        <span class="glyphicon glyphicon-search form-control-feedback"></span>
                    </div>
                </div>
            </div>
            <hr />
            <br />
            <div class="container">
                <br />
                <div class="row">
                    <div class="panel panel-default col-sm-7 col-sm-offset-1">
                        <ul class="list-group" id="contact-list">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="true" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Repeater ID="repreq" runat="server">
                                        <ItemTemplate>
                                            <li class="list-group-item">
                                                <div href="#" class="list-group-item" style="min-height: 160px">
                                                    <div class="list-group-item-heading col-sm-9">
                                                        <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;" style="text-decoration: none;">
                                                            <h3><%# Eval("fname")%>
                                                                <br />
                                                                <%# Eval("lname")%></h3>
                                                        </a>
                                                        <div id="op" class="panel panel-body fade in pull-left">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success btn-xs glyphicon glyphicon-ok"
                                                                OnCommand="accept" CommandArgument='<%# Eval("profile_id") %>'></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-danger  btn-xs glyphicon glyphicon-remove"
                                                                OnCommand="decline" CommandArgument='<%# Eval("profile_id") %>'></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;" style="text-decoration: none;" class="list-group-item  col-sm-3">
                                                            <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility'
                                                                class="img-responsive img-circle" />
                                                        </a>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID='seareq' EventName="textchanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-pane fade" id="log">
            <br />
            <br />
            <div class="col-sm-5">
                <div class="search-form">
                    <div class="form-group has-feedback">
                        <label for="search" class="sr-only">
                            Search</label>
                        <asp:TextBox type="text" class="form-control inputsea" name="search" ID="sealog"
                            runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp_log" onkeyup="SetDelay();" />
                        <span class="glyphicon glyphicon-search form-control-feedback"></span>
                    </div>
                </div>
            </div>
            <hr />
            <br />
            <asp:UpdatePanel runat="server" ID="UpdatePanel3" ChildrenAsTriggers="True">
                <ContentTemplate>
                    <div id="favposts" class="active fade in">
                        <br />
                        <br />


                        <div class="masonry-container col-sm-offset-1 ">
                            <asp:Repeater ID="liked_writes" runat="server">
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
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="unlike_post" CssClass="btn btn-primary btn-xs pull-right"
                                                    CommandArgument='<%# Eval("writes_id") %>'>
                                            <i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                                <span class="toggler fa fa-chevron-down pull-left" onclick="var tog = $(this); var secondDiv = tog.parent().prev();var firstDiv = secondDiv.prev();var $container = $('.masonry-container');firstDiv.children('p').toggleClass('hide');secondDiv.toggleClass('hide');tog.toggleClass('fa fa-chevron-up fa fa-chevron-down'); $container.masonry({columnWidth: '.item',itemSelector: '.item'});return false;" style="color: #008cba">&nbsp;Comments</span>
                                            </div>
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!--/.masonry-container  -->

                        <!--/.tab-panel -->

                        <!--/.masonry-container  -->
                    </div>
                    <!--FAV POSTS-->
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID='sealog' EventName="textchanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= sealog.ClientID %>', '');
        };

        function SetDelay() {
            setTimeout("RefreshUpdatePanel()", 100);
        }

        function RefreshUpdatePanelreq() {
            __doPostBack('<%= seareq.ClientID %>', '');
        };

        function SetDelayreq() {
            setTimeout("RefreshUpdatePanelreq()", 100);
        }

        function RefreshUpdatePanelnot() {
            __doPostBack('<%= seanot.ClientID %>', '');
        };

        function SetDelaynot() {
            setTimeout("RefreshUpdatePanelnot()", 100);
        }
    </script>
</asp:Content>

