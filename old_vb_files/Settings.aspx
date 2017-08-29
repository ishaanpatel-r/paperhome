<%@ Page Title="" Language="VB" MasterPageFile="~/Navigation.master" AutoEventWireup="false"
    EnableSessionState="True" CodeFile="Settings.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
    <h1>
        Settings</h1>
    <!--SELECT OPTION PANEL-->
    <br />
    <div class="panel panel-info col-sm-3 pull-left">
        <div class="panel-heading">
            Sections:</div>
        <div class="panel-body">
            <div class="list-group">
                <a href="#general" data-toggle="tab" class="list-group-item">
                    <h4 class="list-group-item-heading">
                        General
                    </h4>
                    <p class="list-group-item-text">
                        Change your name, primary e-mail address & username.</p>
                </a><a href="#profile" data-toggle="tab" class="list-group-item">
                    <h4 class="list-group-item-heading">
                        Profile</h4>
                    <p class="list-group-item-text">
                        Update your favourites and other basic profile info.</p>
                </a>
            </div>
            <div class="list-group">
                <a href="#security" data-toggle="tab" class="list-group-item">
                    <h4 class="list-group-item-heading">
                        Security</h4>
                    <p class="list-group-item-text">
                        Set a new password, check your login log, delete your account.</p>
                </a>
            </div>
            <div class="list-group">
                <a href="#tagging" data-toggle="tab" class="list-group-item">
                    <h4 class="list-group-item-heading">
                        Privacy</h4>
                    <p class="list-group-item-text">
                        Set who can search for you, send you messages or send you a follow request. Also
                        who can see your photos and writes.</p>
                </a><a href="#blocking" data-toggle="tab" class="list-group-item">
                    <h4 class="list-group-item-heading">
                        Blocking</h4>
                    <p class="list-group-item-text">
                        Block a profile or report one to our support team.</p>
                </a>
            </div>
        </div>
    </div>
    <div class="tab-content">
        <!--GENERAL PANEL-->
        <div class="tab-pane fade" id="general">
            <div class="panel panel-info col-sm-8 pull-right">
                <div class="panel-heading">
                    General Settings:
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Repeater ID="gen_rep" runat="server" DataSourceID="prosetinfo">
                            <ItemTemplate>
                                <fieldset>
                                    <legend>Name</legend>
                                    <!--Change NAME-->
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="inputEmail" class="control-label">
                                                Name: <b>
                                                    <%# Eval("fname") %>
                                                    <%# Eval("lname") %>
                                                </b>
                                            </label>
                                            <a href="#changename" class="btn btn-default pull-right btn-xs" data-toggle="tab">Change
                                                <i class="glyphicon-pencil"></i></a>
                                        </div>
                                        <div class="tab-content" id="1">
                                            <div id="changename" class="col-sm-10 tab-pane fade">
                                                <div class="form-group">
                                                    <label for="inputEmail" class="col-sm-2 control-label">
                                                        First Name:</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" runat="server" class="form-control" id="fn_txt" value='<%# Eval("fname") %>' />
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail" class="col-sm-2 control-label">
                                                        Last Name:</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" runat="server" class="form-control" id="ln_txt" value='<%# Eval("lname") %>' />
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-sm-10 col-sm-offset-2">
                                                    <a href="Settings.aspx" style="text-decoration: none" class="btn btn-default btn-xs">
                                                        Cancel </a>
                                                    <asp:LinkButton CssClass="btn btn-success btn-xs" runat="server" OnClick="save_name">Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Change Username-->
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="inputEmail" class="control-label">
                                                Username: <b>
                                                    <%# Eval("uname") %>
                                                </b>
                                            </label>
                                            <a href="#changeusername" class="btn btn-default pull-right btn-xs" data-toggle="tab">
                                                Change <i class="glyphicon-pencil"></i></a>
                                        </div>
                                        <div class="tab-content" id="tc-un">
                                            <div id="changeusername" class="col-sm-10 tab-pane fade">
                                                <div class="form-group">
                                                    <label for="inputEmail" class="col-sm-2 control-label">
                                                        Username:</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control" id="un_txt" runat="server" value='<%# Eval("uname") %>' />
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-sm-10 col-sm-offset-2">
                                                    <a href="Settings.aspx" style="text-decoration: none" class="btn btn-default btn-xs">
                                                        Cancel </a>
                                                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success btn-xs" runat="server"
                                                        OnClick="save_uname">Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <legend>Email</legend>
                                    <!--Change email-->
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="inputEmail" class="control-label">
                                                Primary E-mail Address:<b>
                                                    <%# Eval("email") %>
                                                </b>
                                            </label>
                                            <a href="#changeprimaryemail" class="btn btn-default pull-right btn-xs" data-toggle="tab">
                                                Change <i class="glyphicon-pencil"></i></a>
                                        </div>
                                        <div class="tab-content">
                                            <div id="changeprimaryemail" class="col-sm-10 tab-pane fade">
                                                <div class="form-group">
                                                    <label for="inputEmail" class="col-sm-2 control-label">
                                                        E-Mail Address:</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control" id="em_txt" runat="server" value='<%# Eval("email") %>' />
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-sm-10 col-sm-offset-2">
                                                    <a href="Settings.aspx" style="text-decoration: none" class="btn btn-default btn-xs">
                                                        Cancel </a>
                                                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success btn-xs" runat="server"
                                                        OnClick="save_email">Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:sqldatasource ID="prosetinfo" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                            SelectCommand="SELECT * FROM [userinfo] WHERE ([profile_id] = @me)">
                            <SelectParameters>
                                <asp:SessionParameter Name="me" SessionField="idsess" Type="String" />
                            </SelectParameters>
                        </asp:sqldatasource>
                    </div>
                </div>
            </div>
        </div>
        <!--PROFILE PANEL-->
        <div class="tab-pane fade" id="profile">
            <div class="panel panel-info col-sm-8 pull-right">
                <div class="panel-heading">
                    Profile Settings:
                </div>
                <asp:Repeater ID="pro_rep" runat="server" DataSourceID="prosetinfopro">
                    <ItemTemplate>
                        <div class="panel-body">
                            <!-- BASIC PROFILE -->
                            <legend>Basic Profile </legend>
                            <!--Current City-->
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="inputEmail" class="control-label">
                                        Current City: <b>
                                            <%# Eval("city") %>
                                        </b>
                                    </label>
                                    <a href="#currcity" class="btn btn-default pull-right btn-xs" data-toggle="tab">Change
                                        <i class="glyphicon-pencil"></i></a>
                                </div>
                                <div class="tab-content">
                                    <div id="currcity" class="col-sm-10 tab-pane fade">
                                        <div class="form-group">
                                            <label for="inputEmail" class="col-sm-2 control-label">
                                                New Current City:</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" runat="server" id="c_txt" value='<%# Eval("city") %>' />
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-sm-10 col-sm-offset-2">
                                            <a href="Settings.aspx" style="text-decoration: none" class="btn btn-default btn-xs">
                                                Cancel </a>
                                            <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success btn-xs" runat="server"
                                                OnClick="save_city_batman">Save</asp:LinkButton>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--sex-->
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="inputEmail" class="control-label">
                                        Sex: <b>
                                            <%# Eval("sex") %>
                                        </b>
                                    </label>
                                    <a href="#sex" class="btn btn-default pull-right btn-xs" data-toggle="tab">Change <i
                                        class="glyphicon-pencil"></i></a>
                                </div>
                                <div class="tab-content">
                                    <div id="sex" class="col-sm-10 tab-pane fade">
                                        <div class="form-group">
                                            <label for="inputEmail" class="col-sm-2 control-label">
                                                Update Sex? Really?:</label>
                                            <div class="col-sm-10">
                                                <div class="col-sm-10">
                                                    <asp:RadioButtonList ID="Radiobuttonlist3" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem id="m" Value="male">&nbsp;&nbsp;Male&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem id="f" Value="female">&nbsp;&nbsp;Female</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-sm-12">
                                                    <a href="Settings.aspx" style="text-decoration: none" class="btn btn-default btn-xs">
                                                        Cancel </a>
                                                    <asp:LinkButton ID="LinkButton4" CssClass="btn btn-success btn-xs" runat="server"
                                                        OnClick="save_sex">Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- R -->
                            <!-- PRIVATE PROFILE -->
                            <legend>Private Profile </legend>
                            <!--DOB-->
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="inputEmail" class="control-label">
                                        Date of Birth: <b>
                                            <%# Eval("dob", "{0:D}") %>
                                        </b>
                                    </label>
                                    <a href="#dob" class="btn btn-default pull-right btn-xs" data-toggle="tab">Update <i
                                        class="glyphicon-pencil"></i></a>
                                </div>
                                <div class="tab-content">
                                    <div id="dob" class="col-sm-10 tab-pane fade">
                                        <div class="form-group col-sm-12">
                                            <div class="input-group date col-sm-8" id="datetimepicker9">
                                                <input id="date_txt" class="form-control" runat="server" data-format="dd/MM/yyyy"
                                                    type="text" />
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <a href="Settings.aspx" style="text-decoration: none" class="btn btn-default btn-xs">
                                                Cancel </a>
                                            <asp:LinkButton ID="LinkButton5" CssClass="btn btn-success btn-xs" runat="server"
                                                OnClick="save_dob">Save</asp:LinkButton>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Phone No-->
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="inputEmail" class="control-label">
                                        Personal Cell: <b>
                                            <%# Eval("cell_no") %>
                                        </b>
                                    </label>
                                    <a href="#cell" class="btn btn-default pull-right btn-xs" data-toggle="tab">Update <i
                                        class="glyphicon-pencil"></i></a>
                                </div>
                                <div class="tab-content">
                                    <div id="cell" class="col-sm-10 tab-pane fade">
                                        <div class="form-group">
                                            <label for="inputEmail" class="col-sm-2 control-label">
                                                New Cell Number:</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" runat="server" id="cell_txt" value='<%# Eval("cell_no") %>' />
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-sm-10 col-sm-offset-2">
                                            <a href="Settings.aspx" style="text-decoration: none" class="btn btn-default btn-xs">
                                                Cancel </a>
                                            <asp:LinkButton ID="LinkButton6" CssClass="btn btn-success btn-xs" runat="server"
                                                OnClick="save_cell">Save</asp:LinkButton>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--Testimonials-->
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="inputEmail" class="control-label">
                                        Testimonials: <b>Delete any testimonials you don't like? </b>
                                    </label>
                                    <a href="#test" class="btn btn-default pull-right btn-xs" data-toggle="tab">See All<i
                                        class="glyphicon-pencil"></i></a>
                                </div>
                                <div class="tab-content">
                                    <div id="test" class="col-sm-10 tab-pane fade">
                                        <div class="form-group">
                                            <asp:Repeater DataSourceID="testfetch" runat="server">
                                                <ItemTemplate>
                                                    <blockquote class="col-sm-6">
                                                        <p>
                                                            <%# Eval("content")%></p>
                                                        <small>Someone
                                                            <%# Eval("mode_1") %>
                                                            named <cite title="Source Title">
                                                                <%# Eval("uname") %></cite>,
                                                            <br />
                                                            rated you
                                                            <%# Eval("rating")%>
                                                            stars. </small>
                                                        <br />
                                                        <asp:LinkButton ID="LinkButton6" CssClass="btn btn-danger btn-xs" runat="server"
                                                            OnCommand="del_test" CommandArgument=' <%# Eval("test_id")%>'>Remove</asp:LinkButton>
                                                    </blockquote>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:sqldatasource ID="testfetch" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                                SelectCommand="SELECT * FROM ([userinfo] LEFT JOIN [tests] ON [userinfo].[profile_id]=[tests].[from]) WHERE ([to] = @me)">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="me" SessionField="idsess" Type="String" />
                                                </SelectParameters>
                                            </asp:sqldatasource>
                                        </div>
                                        <div class="col-sm-10 col-sm-offset-2">
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:sqldatasource ID="prosetinfopro" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                    SelectCommand="SELECT * FROM [userinfo] WHERE ([profile_id] = @me)">
                    <SelectParameters>
                        <asp:SessionParameter Name="me" SessionField="idsess" Type="String" />
                    </SelectParameters>
                </asp:sqldatasource>
            </div>
        </div>
    </div>
    <!--SECURITY PANEL-->
    <div class="tab-pane fade" id="security">
        <div class="panel panel-info col-sm-8 pull-right">
            <div class="panel-heading">
                Security Measures:
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <fieldset>
                        <!-- CHANGE PASSWORD-->
                        <legend>Change Password</legend>
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label for="inputPassword" class="control-label">
                                    Old Password: <b>Change monthly.</b></label>
                                <a href="#changepassword" class="btn btn-default pull-right btn-xs" data-toggle="tab">
                                    Change<i class="glyphicon-pencil"></i></a>
                            </div>
                            <div class="tab-content">
                                <div id="changepassword" class="col-sm-10 tab-pane fade">
                                    <div class="form-group">
                                        <label for="inputEmail" class="col-sm-2 control-label">
                                            Old Password:</label>
                                        <div class="col-sm-10">
                                            <input type="password" runat="server" id="olpasswordhere" class="form-control" placeholder="Enter your old password." />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail" class="col-sm-2 control-label">
                                            New Password:</label>
                                        <div class="col-sm-10">
                                            <input type="password" runat="server" id="newp" class="form-control" placeholder="Enter a new password." />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail" class="col-sm-2 control-label">
                                            Confirm New Password:</label>
                                        <div class="col-sm-10">
                                            <input type="password" runat="server" id="newpc" class="form-control" placeholder="Re-enter the new password." />
                                        </div>
                                    </div>
                                    <div class="col-sm-10 col-sm-offset-2">
                                        <a href="Settings.aspx" style="text-decoration: none" class="btn btn-default btn-xs">
                                            Cancel </a>
                                        <asp:LinkButton ID="LinkButton3" CssClass="btn btn-success btn-xs" runat="server"
                                            OnClick="save_pw">Save</asp:LinkButton>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--LOGIN LOG-->
                        <legend>Login Log</legend>
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label for="inputPassword" class="control-label">
                                    Active Session/s: <b>1</b>.</label>
                                <a href="#loginlog" class="btn btn-default pull-right btn-xs" data-toggle="tab">View<i
                                    class="glyphicon-pencil"></i></a>
                            </div>
                            <div class="tab-content">
                                <div id="loginlog" class="col-sm-10 tab-pane fade">
                                    <table class="table table-striped table-hover ">
                                        <thead>
                                            <tr>
                                                <th>
                                                    #
                                                </th>
                                                <th>
                                                    Browser
                                                </th>
                                                <th>
                                                    IP
                                                </th>
                                                <th>
                                                    Date & Time
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater DataSourceID="acsessdata" runat="server">
                                                <ItemTemplate>
                                                    <tr class="success">
                                                        <td>
                                                            1
                                                        </td>
                                                        <td>
                                                            <%# Eval("brow_name")%>
                                                            <%# Eval("brow_ver")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ip_id")%>
                                                        </td>
                                                        <td>
                                                            Active Now
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:sqldatasource ID="acsessdata" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                                SelectCommand="SELECT TOP 1 * FROM [session_logs] WHERE ([profile_id] = @me) ORDER BY date_time_login DESC">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="me" SessionField="idsess" Type="Int32" />
                                                </SelectParameters>
                                            </asp:sqldatasource>
                                            <asp:Repeater runat="server" DataSourceID="sessdata">
                                                <ItemTemplate>
                                                    <tr class="default">
                                                        <td>
                                                            <%# Container.ItemIndex + 2 %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("brow_name")%>
                                                            <%# Eval("brow_ver")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ip_id")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("date_time_login")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:sqldatasource ID="sessdata" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                                SelectCommand="SELECT TOP 9 * FROM [session_logs] WHERE ([profile_id] = @me) ORDER BY date_time_login DESC">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="me" SessionField="idsess" Type="Int32" />
                                                </SelectParameters>
                                            </asp:sqldatasource>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- DELETE ACCOUNT -->
                        <legend>Delete Account</legend>
                        <label>
                            Are you sure you want to delete your account?</label>
                        <a data-toggle="modal" data-target="#deleteaccountwarn" style="text-decoration: none;
                            color: White;">
                            <button class="btn btn-danger pull-right btn-xs">
                                Delete
                            </button>
                        </a>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <!--PRIVACY PANEL-->
    <div class="tab-pane fade" id="tagging">
        <div class="panel panel-info col-sm-8 pull-right">
            <div class="panel-heading">
                Privacy Settings:
            </div>
            <div class="panel-body">
                <legend>
                    <asp:LinkButton ID="LinkButton1" runat="server" href="MyPhotos.aspx" Style="text-decoration: none"
                        CssClass="fa fa-camera-retro">
                    </asp:LinkButton>&nbsp;&nbsp;Who can see your photos? </legend>
                <div class="form-group">
                    <label class="col-sm-2 control-label text-muted">
                        Only those you select would be able to see your photos:</label>
                    <div class="col-sm-10">
                        <asp:RadioButtonList ID="photo_set_rad" runat="server">
                            <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;Everyone</asp:ListItem>
                            <asp:ListItem Value="2">&nbsp;&nbsp;&nbsp;Your Feelpals Only</asp:ListItem>
                            <asp:ListItem Value="3">&nbsp;&nbsp;&nbsp;None</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <br />
                <legend>
                    <asp:LinkButton ID="LinkButton7" runat="server" href="MyProfile.aspx" Style="text-decoration: none"
                        CssClass="fa fa-book">
                    </asp:LinkButton>&nbsp;&nbsp;Who can see your writes?</legend>
                <div class="form-group">
                    <label class="col-sm-2 control-label text-muted">
                        Only those you select would be able to see your writes:</label>
                    <div class="col-sm-10">
                        <asp:RadioButtonList ID="write_set_rad" runat="server">
                            <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;Everyone</asp:ListItem>
                            <asp:ListItem Value="2">&nbsp;&nbsp;&nbsp;Your Feelpals Only</asp:ListItem>
                            <asp:ListItem Value="3">&nbsp;&nbsp;&nbsp;None</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                 <br />
                <legend>
                    <asp:LinkButton ID="LinkButton10" runat="server" href="Feelpals.aspx" Style="text-decoration: none"
                        CssClass="fa fa-users">
                    </asp:LinkButton>&nbsp;&nbsp;Who can send you messages & follow-request?</legend>
                <div class="form-group">
                    <label class="col-sm-2 control-label text-muted">
                        Only those you select would be able to send you a messages/follow-request:</label>
                    <div class="col-sm-10">
                        <asp:RadioButtonList ID="req_set_rad" runat="server">
                            <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;Everyone</asp:ListItem>
                            <asp:ListItem Value="2">&nbsp;&nbsp;&nbsp;People You Follow Only</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <asp:LinkButton ID="LinkButton12" runat="server" OnClick="save_priv" Style="text-decoration: none"
                CssClass="btn btn-success btn-xs pull-right">Save</asp:LinkButton>
            <br />
        </div>
    </div>
    <!--BLOCKING PANEL-->
    <div class="tab-pane fade" id="blocking">
        <div class="panel panel-info col-sm-8 pull-right">
            <div class="panel-heading">
                Block a profile:
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <label for="inputEmail" class="col-sm-2 control-label">
                        Enter Username:</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control ac" runat="server" id="bl_txt" placeholder="Who'd you want gone?" />
                        <br />
                        <div class="col-sm-1 pull-right">
                            <asp:LinkButton ID="LinkButton9" runat="server" OnClick="block" Style="text-decoration: none"
                                CssClass="btn btn-danger btn-xs">Block</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <legend>Blocked Users</legend>
                <table class="table table-striped table-hover ">
                    <thead>
                        <tr>
                            <th>
                                Name
                            </th>
                            <th>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" DataSourceID="blocked_users">
                            <ItemTemplate>
                                <tr class="default">
                                    <td>
                                        <%# Eval("fname")%>
                                        <%# Eval("lname")%>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="che" runat="server" CssClass="btn-xs btn-primary pull-right col-sm-offset-1"
                                            OnCommand="unblock" CommandArgument='<%# Eval("profile_id")%>'>Unblock</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:sqldatasource ID="blocked_users" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                            SelectCommand="SELECT * FROM ([block_list] LEFT JOIN [userinfo] ON [block_list].[blockee]=[userinfo].[profile_id]) WHERE ([blocker] = @me)">
                            <SelectParameters>
                                <asp:SessionParameter Name="me" SessionField="idsess" Type="Int32" />
                            </SelectParameters>
                        </asp:sqldatasource>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
