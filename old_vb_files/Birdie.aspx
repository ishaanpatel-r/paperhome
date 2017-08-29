<%@ Page Title="" Language="VB" MasterPageFile="~/Navigation.master" AutoEventWireup="false" CodeFile="Birdie.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
    <div>
        <div class="col-sm-2">
            <ul class="nav nav-pills nav-stacked">
                <li class="active"><a href="#birdiechat" data-toggle="tab">Chat</a></li>
                <li><a href="#birdieact" data-toggle="tab">Activity</a></li>

                <%--
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">Online Status:<span class="caret"></span>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a href="#">Online Feelpals</a></li>
                        <li class="divider"></li>
                        <li><a href="#">Available</a></li>
                        <li><a href="#">Busy</a></li>
                        <li class="divider"></li>
                        <li><a href="#">Offline</a></li>
                    </ul>
                </li>--%>
            </ul>
        </div>
        <div class="col-sm-10 tab-content">
            <div class="row tab-pane fade" id="birdieact">
                <div class="col-sm-12">

                    <div class="col-sm-7 col-sm-offset-1" id="birdienotif">
                        <br />
                        <br />
                        <h2>Activity on You</h2>
                        <br />
                        <br />
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:Repeater ID="actnotifrep" runat="server">
                                    <ItemTemplate>
                                        <ul class="list-group activity-list">
                                            <li class="list-group-item"><i class="glyphicon <%# If(DataBinder.Eval(Container.DataItem, "type") = "started following", "glyphicon-user", If(DataBinder.Eval(Container.DataItem, "type") = "commented on a", "glyphicon-bookmark", If(DataBinder.Eval(Container.DataItem, "type") = "wrote a new", "glyphicon-pencil", If(DataBinder.Eval(Container.DataItem, "type") = "liked a", "glyphicon-heart", If(DataBinder.Eval(Container.DataItem, "type") = "updated", "glyphicon-cog", If(DataBinder.Eval(Container.DataItem, "type") = "uploaded", "glyphicon-film", If(DataBinder.Eval(Container.DataItem, "type") = "tagged", "glyphicon-bookmark", "glyphicon-bookmark")))))))%>"></i>&nbsp; <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;"
                                                style="text-decoration: none;">
                                                <%# Eval("uname")%></a>
                                                <%# Eval("type")%>
                                                <a style="text-decoration: none;">
                                                    <%# If(DataBinder.Eval(Container.DataItem, "type") = "started following", "you.", If(DataBinder.Eval(Container.DataItem, "type") = "commented on a", "post you wrote.", If(DataBinder.Eval(Container.DataItem, "type") = "wrote a new", "post.", If(DataBinder.Eval(Container.DataItem, "type") = "liked a", "post you wrote.", If(DataBinder.Eval(Container.DataItem, "type") = "updated", "their profile.", If(DataBinder.Eval(Container.DataItem, "type") = "uploaded", "new photos.", If(DataBinder.Eval(Container.DataItem, "type") = "tagged", "post.", "post you wrote.")))))))%></a>
                                                <br />
                                                <br />
                                                <span class="pull-right text-muted small time-line" data-livestamp="<%# Eval("date_time_written", "{0:f}")%>"></span>
                                                <br />
                                            </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-3 col-sm-offset-1" id="birdiereq">
                        <br />
                        <br />
                        <h2>Follow Requests</h2>
                        <br />
                        <br />
                        <ul class="list-group" id="contact-list">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="true" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Repeater ID="actreqrep" runat="server">
                                        <ItemTemplate>
                                            <div class="navbar-login">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;">
                                                            <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility'
                                                                class="img-responsive" /></a>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <p class="text-left">
                                                            <strong><a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>">
                                                                <%# Eval("fname")%>
                                                                <%# Eval("lname")%></a></strong>
                                                        </p>
                                                        <br />
                                                        <p class="text-left">
                                                            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-success btn-xs glyphicon glyphicon-ok"
                                                                OnCommand="accept" CommandArgument='<%# Eval("profile_id") %>'></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-danger  btn-xs glyphicon glyphicon-remove"
                                                                OnCommand="decline" CommandArgument='<%# Eval("profile_id") %>'></asp:LinkButton>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ul>
                    </div>

                </div>

                <div class="col-sm-12 well well-lg">

                    <a href="Activity.aspx" style="text-decoration: none;">
                        <center>
                            <p>
                                <strong>See All</strong>
                            </p>
                        </center>
                    </a>
                </div>
            </div>
            <div class="row tab-pane fade active in" id="birdiechat">
                <div class="col-sm-12">
                    <div id="#birdiemainchatmodule" class="col-sm-7 col-sm-offset-1">
                        <asp:UpdatePanel runat="server" ID="sendup" UpdateMode="always">
                            <ContentTemplate>
                                <div class="msg-wrap scrolldown" style="max-height: 300px; padding-left: 5px;">
                                    <div class="chat_box touchscroll chat_box_colors_a">
                                        <asp:Repeater ID="personmess" runat="server">
                                            <ItemTemplate>
                                                <div class="chat_message_wrapper <%# if (DataBinder.Eval(Container.DataItem, "Sender") = Session("idsess"), "chat_message_right","") %>">
                                                    <div class="chat_user_avatar">
                                                        <a href="#">
                                                            <img alt="<%# Eval("fname")%> <%# Eval("lname") %>" title="<%# Eval("fname")%> <%# Eval("lname") %>"
                                                                src="<%# Eval("dp_url") %>" class="md-user-image">
                                                        </a>
                                                    </div>
                                                    <ul class="chat_message">
                                                        <li>
                                                            <p>
                                                                <%# Eval("content") %><span class="chat_message_time" data-livestamp="<%# Eval("date_written", "{0:f}")%>"></span>
                                                            </p>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <!--chatbox-->
                                <div visible="true" runat="server" id="sendermess">
                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="senbut">
                                        <div class="send-wrap">
                                            <input type="text" class="form-control" id="sendmess_txt" runat="server" rows="3"
                                                placeholder="Write a reply..." />
                                        </div>
                                        <div class="btn-panel">
                                            <asp:LinkButton ID="senbut" OnClick="sendmess" runat="server" CssClass="col-sm-4 text-right btn end-message-btn pull-right">
                    <i class="fa fa-plus"></i> &nbsp;Send Message</asp:LinkButton>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <!--chatboxsend-->
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="senbut" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div id="#birdiefeelpalsmodule" class="col-sm-3  col-sm-offset-1 well well-lg">
                        <div class="col-sm-5">
                            <div class="search-form">
                                <div class="form-group has-feedback">
                                    <label for="search" class="sr-only">
                                        Search</label>
                                    <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seanot"
                                        runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp" onkeyup="SetDelay();" />
                                    <span class="glyphicon glyphicon-search form-control-feedback"></span>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <br />
                        <div class="list-group" style="max-height: 350px; min-height: 50px; overflow-y: scroll;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:Repeater ID="peeps" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="HyperLink1" OnCommand="thread_sel" runat="server" CommandArgument='<%# Eval("profile_id") %>'
                                                CssClass="list-group-item" Style="min-height: 123px">
                              
                                     <div class="list-group-item-heading col-lg-5">
                                        <h3>
                                           <%# Eval("fname") %>
                                            <br />
                                          <%# Eval("lname") %></h3>
                                        
                                    </div>
                                    <div class="col-sm-5 pull-right">
                                        <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility' class="img-responsive img-circle" />
                                    </div>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID='seanot' EventName="textchanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= seanot.ClientID %>', '');
        };

        function SetDelay() {
            setTimeout("RefreshUpdatePanel()", 100);
        }


        //handle message upload

        window.setInterval(function () {
            $.ajax({
                url: "handlecb.ashx",
                contentType: "application/json; charset=utf-8",
                data: { 'tick': 'wowie' },
                responseType: "json",
                success: OnComplete,
                error: OnFail
            });
            return false;

        }, 3000);



        function OnComplete(result) {

            if (!$.trim(result)) {

            }
            else {


                if (result == "yes") {


                    __doPostBack("<%= senbut.ClientID %>", "");

                }



            }
        }

        function OnFail(result) {


        }




    </script>
</asp:Content>

