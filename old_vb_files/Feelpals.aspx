<%@ Page Title="" Language="VB" MasterPageFile="~/UI.master" AutoEventWireup="false" CodeFile="Feelpals.aspx.vb" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body_cont" Runat="Server">
    <center><br /><br />
    <h1>Feelpals</h1>
    <br />
    <br />
    <ul class="nav nav-tabs col-sm-8 col-sm-offset-2">
        <li class="active"><a href="#following" data-toggle="tab">Following</a></li>
        <li class=""><a href="#followers" data-toggle="tab">Followers</a></li>
    </ul></center>
    <div id="myTabContent" class="tab-content col-sm-8 col-sm-offset-2">
        <div class="tab-pane fade active in" id="following">
            <br />
            <br />
            <div class="col-sm-5">
                <div class="search-form">
                    <div class="form-group has-feedback">
                        <label for="search" class="sr-only">
                            Search</label>
                        <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seaing"
                            runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp_ing" onkeyup="SetDelay();" />
                        <span class="glyphicon glyphicon-search form-control-feedback"></span>
                    </div>
                </div>
            </div>
            <hr />
            <br />
            <br />
            <div class="container">
<div class="panel panel-default col-sm-7 col-sm-offset-1 " style="border: none;">
                    <ul class="list-group" id="contact-list">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <div class="panel panel-default panel-body">
                                            <div class="list-group-item-heading col-sm-9">
                                                <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;">
                                                    <h3><%# Eval("fname")%>
                                                        <br />
                                                        <%# Eval("lname")%></h3>
                                                </a>
                                                <div id="op" class="panel panel-body fade in pull-left">
                                                    <a class="btn btn-success fa fa-comments-o" data-toggle="modal" data-target="#<%# Eval("profile_id") %>messmodal"></a>&nbsp; <a class="btn btn-danger fa fa-user-times" data-toggle="modal" data-target="#<%# Eval("fname")%>unfmodal"></a>
                                                </div>
                                            </div>
                                            <div class="list-group-item-text col-sm-3">
                                                <asp:LinkButton ID="dude1" OnCommand="fpra" runat="server" CommandArgument='<%# Eval("profile_id") %>'
                                                    CssClass="list-group-item">
                                                    <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility' class="img-circle" />
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="modal fade" id='<%# Eval("fname")%>unfmodal'>
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            ×</button>
                                                        <br />
                                                    </div>
                                                    <div class="modal-body">
                                                        <p>
                                                            Stop follwing
                                                                <%# Eval("fname")%>
                                                                ?
                                                        </p>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-default" data-dismiss="modal">
                                                            Cancel</button>
                                                        <asp:LinkButton CssClass="btn btn-primary" ID="LinkButton2" runat="server" OnCommand="unf"
                                                            CommandArgument='<%# Eval("profile_id") %>'>
                                                        Yes</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--Unfollow Modal-->
                                        <div class="modal fade" id='<%# Eval("profile_id") %>messmodal'>
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
                                <asp:AsyncPostBackTrigger ControlID='seaing' EventName="textchanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ul>
                </div>
                <asp:UpdatePanel ID="hehe" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-info col-sm-3 pull-right" id="fp_ra" runat="server" visible="false">
                <asp:Repeater ID="Repeater9" runat="server" DataSourceID="sqldatasource1">
                    <ItemTemplate>
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <%# Eval("fname")%>'s Recent Activity</h3>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="sqldatasource1" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                    SelectCommand="SELECT * FROM [userinfo] WHERE ([profile_id] = @fps)">
                    <SelectParameters>
                        <asp:SessionParameter Name="fps" SessionField="fpra_sess" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <div class="panel-body" style="max-height: 300px; min-height: 50px; overflow-y: scroll;">
                    <asp:Repeater ID="notifdisp" runat="server">
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
                <!-- Liked someone's post-->
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
            </div>
        </div>
        <div class="tab-pane fade in" id="followers">
            <br />
            <br />
            <div class="col-sm-5">
                <div class="search-form">
                    <div class="form-group has-feedback">
                        <label for="search" class="sr-only">
                            Search</label>
                        <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seaers"
                            runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp_ers" onkeyup="SetDelay();" />
                        <span class="glyphicon glyphicon-search form-control-feedback"></span>
                    </div>
                </div>
            </div>
            <hr />
            <br />
            <br />
            <div class="container">

                <div class="panel panel-default col-sm-7 col-sm-offset-1 " style="border: none;">
                    <ul class="list-group" id="Ul1">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="false" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <div class="panel panel-default panel-body">
                                            <div class="list-group-item-heading col-sm-9">
                                                <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;">
                                                    <h3><%# Eval("fname")%>
                                                        <br />
                                                        <%# Eval("lname")%></h3>
                                                </a>
                                                <div id="Div4" class="panel panel-body fade in pull-left">
                                                    <a href="#" data-target="#<%# Eval("profile_id") %>messmodal2" data-toggle="modal"
                                                        class="btn btn-success fa fa-comments-o"></a>&nbsp; <a href="#" data-target="#<%# Eval("fname")%>removefoll"
                                                            data-toggle="modal" class="btn btn-danger fa fa-user-times"></a>
                                                </div>
                                            </div>
                                            <div class="list-group-item-text col-sm-3">
                                                <asp:LinkButton ID="dude2" OnCommand="fpra" runat="server" CommandArgument='<%# Eval("profile_id") %>'
                                                    CssClass="list-group-item">
                                                    <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility' class="img-circle" />
                                                </a>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="modal fade" id='<%# Eval("profile_id") %>messmodal2'>
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
                                                        <input type="text" class="form-control" id="messtxt2" runat="server" />
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-default" data-dismiss="modal">
                                                            Cancel</button>
                                                        <asp:LinkButton CssClass="btn btn-primary" ID="LinkButton1" runat="server" OnCommand="sendmess2"
                                                            CommandArgument='<%# Eval("profile_id") %>'>
                                                        Send</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- New Message Modal-->
                                        <div class="modal fade" id='<%# Eval("fname")%>removefoll'>
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            ×</button>
                                                        <br />
                                                    </div>
                                                    <div class="modal-body">
                                                        <p>
                                                            Stop
                                                                <%# Eval("fname")%>
                                                                from follwing you?
                                                        </p>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-default" data-dismiss="modal">
                                                            Cancel</button>
                                                        <asp:LinkButton CssClass="btn btn-primary" ID="LinkButton2" runat="server" OnCommand="stopf"
                                                            CommandArgument='<%# Eval("profile_id") %>'>
                                                        Yes</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--Remove Follower Modal-->
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID='seaers' EventName="textchanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ul>
                </div>

            </div>
        </div>
    </div>
    <!--SELF_RA_GRID -->
    
    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= seaing.ClientID %>', '');
        };

        function SetDelay() {
            setTimeout("RefreshUpdatePanel()", 100);
        }
    </script>
    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= seaers.ClientID %>', '');
        };

        function SetDelay() {
            setTimeout("RefreshUpdatePanel()", 100);
        }
    </script>
</asp:Content>

