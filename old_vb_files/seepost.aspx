<%@ Page Title="" Language="VB" MasterPageFile="~/Navigation.master" AutoEventWireup="false" CodeFile="seepost.aspx.vb" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" Runat="Server">
    <asp:Repeater runat="server" ID="Repeater1">
        <ItemTemplate>
    <div class="col-sm-8 col-sm-offset-2 item">
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
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
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
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="like_post" CssClass="btn btn-danger btn-xs pull-right"
                                                CommandArgument='<%# Eval("writes_id") %>'>
                                            <i class="glyphicon glyphicon-heart"></i></asp:LinkButton>
                                            <span class="toggler fa fa-chevron-down pull-left" onclick="var tog = $(this); var secondDiv = tog.parent().prev();var firstDiv = secondDiv.prev();var $container = $('.masonry-container');firstDiv.children('p').toggleClass('hide');secondDiv.toggleClass('hide');tog.toggleClass('fa fa-chevron-up fa fa-chevron-down'); $container.masonry({columnWidth: '.item',itemSelector: '.item'});return false;" style="color: #008cba">&nbsp;Comments</span>
                                        </div>
                                    </div>
                                </div>
            </ItemTemplate>
        </asp:Repeater>
</asp:Content>

