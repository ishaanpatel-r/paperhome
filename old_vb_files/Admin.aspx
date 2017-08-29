<%@ Page Title="" Language="VB" MasterPageFile="~/Navigation.master" AutoEventWireup="false"
    EnableSessionState="True" CodeFile="Admin.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Main -->
    <div class="container-fluid">
        <!-- /col-3 -->
        <div class="col-sm-12">
            <div class="row">
                <!-- center left-->
                <div class="col-md-6">
                    <!--tabs-->
                    <div class="panel">
                        <h1>
                            Recent</h1>
                        <br />
                        <ul class="nav nav-tabs" id="myTab">
                            <li class="active"><a href="#profile" data-toggle="tab">Activity</a></li>
                            
                                    <li><a href="#messages" data-toggle="tab">Members</a></li></ItemTemplate>
                            
                            <li><a href="#settings" data-toggle="tab">Sessions</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active well" id="profile" style="max-height: 500px; overflow-y: scroll;">
                                <asp:Repeater ID="Repeater2" DataSourceID="notif_data_sf" runat="server">
                                    <ItemTemplate>
                                        <ul class="list-group activity-list">
                                            <li class="list-group-item"><i class="glyphicon glyphicon-user icon-activity"></i>
                                                <asp:LinkButton ID="HyperLink1" OnCommand="red_act_sf" runat="server" CommandArgument='<%# Eval("profile_id") %>'> &nbsp; <%# Eval("uname")%></asp:LinkButton>
                                                started follwing <a href="#">
                                                    <%# Eval("uname") %>. </a>
                                                <br />
                                                <br />
                                                <span class="pull-right text-muted small time-line"><span data-livestamp="<%# Eval("date_time_written", "{0:f}")%>">
                                                </span><span class="glyphicon glyphicon-time timestamp" data-toggle="tooltip" data-
                                                    placement="bottom" title="*time*"></span></span>
                                                <br />
                                            </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:sqldatasource ID="notif_data_sf" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                    SelectCommand="SELECT [profile_id],[type], [uname], [date_time_written] FROM ([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) WHERE ([type] = ?) ORDER BY [date_time_written] DESC">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="started following" Name="type" Type="String" />
                                     
                                    </SelectParameters>
                                </asp:sqldatasource>
                                <!-- Someone started following someone-->
                                <asp:Repeater ID="Repeater3" DataSourceID="notif_data_wn" runat="server">
                                    <ItemTemplate>
                                        <ul class="list-group activity-list">
                                            <li class="list-group-item"><i class="glyphicon glyphicon-bookmark"></i>
                                                <asp:LinkButton ID="HyperLink1" OnCommand="red_act_wn" runat="server" CommandArgument='<%# Eval("profile_id") %>'> &nbsp; <%# Eval("uname")%></asp:LinkButton>
                                                wrote a new <a href="#">post.</a>
                                                <br />
                                                <br />
                                                <span class="pull-right text-muted small time-line"><span data-livestamp="<%# Eval("date_time_written", "{0:f}")%>">
                                                </span><span class="glyphicon glyphicon-time timestamp" data-toggle="tooltip" data-
                                                    placement="bottom" title="*time*"></span></span>
                                                <br />
                                            </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:sqldatasource ID="notif_data_wn" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                    SelectCommand="SELECT [profile_id],[type], [uname], [date_time_written] FROM ([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) WHERE ([type] = ?) ORDER BY [date_time_written] DESC">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="wrote a new" Name="type" Type="String" />
                                        <asp:SessionParameter Name="me" SessionField="idsess" Type="Int32" />
                                    </SelectParameters>
                                </asp:sqldatasource>
                                <!-- Wrote a new post -->
                                <asp:Repeater ID="Repeater4" DataSourceID="notif_data_co" runat="server">
                                    <ItemTemplate>
                                        <ul class="list-group activity-list">
                                            <li class="list-group-item"><i class="glyphicon glyphicon-bookmark"></i>
                                                <asp:LinkButton ID="HyperLink1" OnCommand="red_act_cy" runat="server" CommandArgument='<%# Eval("profile_id") %>'> &nbsp; <%# Eval("uname")%></asp:LinkButton>
                                                commented on your <a href="#">post.</a>
                                                <br />
                                                <br />
                                                <span class="pull-right text-muted small time-line"><span data-livestamp="<%# Eval("date_time_written", "{0:f}")%>">
                                                </span><span class="glyphicon glyphicon-time timestamp" data-toggle="tooltip" data-
                                                    placement="bottom" title="*time*"></span></span>
                                                <br />
                                            </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:sqldatasource ID="notif_data_co" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                    SelectCommand="SELECT [profile_id],[type], [uname], [date_time_written] FROM ([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) WHERE ([type] = ?) ORDER BY [date_time_written] DESC">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="commented on a" Name="type" Type="String" />
                                        <asp:SessionParameter Name="me" SessionField="idsess" Type="Int32" />
                                    </SelectParameters>
                                </asp:sqldatasource>
                                <!-- Commented on your post -->
                                <!-- NEED MORE REFERENCING-->
                                <asp:Repeater ID="Repeater5" DataSourceID="notif_data_co_d" runat="server">
                                    <ItemTemplate>
                                        <ul class="list-group activity-list">
                                            <li class="list-group-item"><i class="glyphicon glyphicon-bookmark"></i>
                                                <asp:LinkButton ID="HyperLink1" OnCommand="red_act_ca" runat="server" CommandArgument='<%# Eval("profile_id") %>'> &nbsp; <%# Eval("uname")%></asp:LinkButton>
                                                commented on a <a href="#">post.</a>
                                                <br />
                                                <br />
                                                <span class="pull-right text-muted small time-line"><span data-livestamp="<%# Eval("date_time_written", "{0:f}")%>">
                                                </span><span class="glyphicon glyphicon-time timestamp" data-toggle="tooltip" data-
                                                    placement="bottom" title="*time*"></span></span>
                                                <br />
                                            </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:sqldatasource ID="notif_data_co_d" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                    SelectCommand="SELECT [profile_id],[type], [uname], [date_time_written] FROM ([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) WHERE ([type] = ?) ORDER BY [date_time_written] DESC">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="commented on a" Name="type" Type="String" />
                                        <asp:SessionParameter Name="me" SessionField="idsess" Type="Int32" />
                                    </SelectParameters>
                                </asp:sqldatasource>
                                <!-- Commented on someone's post -->
                                <asp:Repeater ID="Repeater6" DataSourceID="notif_data_l" runat="server">
                                    <ItemTemplate>
                                        <ul class="list-group activity-list">
                                            <li class="list-group-item"><i class="glyphicon glyphicon-heart icon-activity"></i>
                                                <asp:LinkButton ID="HyperLink1" OnCommand="red_act_ly" runat="server" CommandArgument='<%# Eval("profile_id") %>'> &nbsp; <%# Eval("uname")%></asp:LinkButton>
                                                liked your <a href="#">post.</a>
                                                <br />
                                                <br />
                                                <span class="pull-right text-muted small time-line"><span data-livestamp="<%# Eval("date_time_written", "{0:f}")%>">
                                                </span><span class="glyphicon glyphicon-time timestamp" data-toggle="tooltip" data-
                                                    placement="bottom" title="*time*"></span></span>
                                                <br />
                                            </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:sqldatasource ID="notif_data_l" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                    SelectCommand="SELECT [profile_id],[type], [uname], [date_time_written] FROM ([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) WHERE ([type] = ?) ORDER BY [date_time_written] DESC">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="liked a" Name="type" Type="String" />
                                        <asp:SessionParameter Name="me" SessionField="idsess" Type="Int32" />
                                    </SelectParameters>
                                </asp:sqldatasource>
                                <!-- Liked your post-->
                                <!-- NEED MORE REFERENCING-->
                                <asp:Repeater ID="Repeater1" DataSourceID="notif_data_l_d" runat="server">
                                    <ItemTemplate>
                                        <ul class="list-group activity-list">
                                            <li class="list-group-item"><i class="glyphicon glyphicon-heart icon-activity"></i>
                                                <asp:LinkButton ID="HyperLink1" OnCommand="red_act_la" runat="server" CommandArgument='<%# Eval("profile_id") %>'>  &nbsp; <%# Eval("uname")%></asp:LinkButton>
                                                liked
                                                <%# Eval("uname") %>'s<a href="#"> post.</a>
                                                <br />
                                                <br />
                                                <span class="pull-right text-muted small time-line"><span data-livestamp="<%# Eval("date_time_written", "{0:f}")%>">
                                                </span><span class="glyphicon glyphicon-time timestamp" data-toggle="tooltip" data-
                                                    placement="bottom" title="*time*"></span></span>
                                                <br />
                                            </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:sqldatasource ID="notif_data_l_d" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                    SelectCommand="SELECT [profile_id],[type], [uname], [date_time_written] FROM ([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) WHERE ([type] = ?) ORDER BY [date_time_written] DESC">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="liked a" Name="type" Type="String" />
                                        <asp:SessionParameter Name="me" SessionField="idsess" Type="Int32" />
                                    </SelectParameters>
                                </asp:sqldatasource>
                                <!-- Liked someone's post-->
                            </div>
                            <div class="tab-pane well" id="messages" style="max-height: 500px; overflow-y: scroll;">
                                <asp:Repeater ID="Repeater7" runat="server" DataSourceID="xfollowing">
                                    <ItemTemplate>
                                        <li class="list-group-item">
                                            <div class="list-group-item" style="min-height: 160px">
                                                <div class="list-group-item-heading col-sm-5">
                                                    <h3>
                                                        <%# Eval("fname")%>
                                                        <br />
                                                        <%# Eval("lname")%></h3>
                                                </div>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:sqldatasource ID="xfollowing" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                    SelectCommand="SELECT * FROM userinfo"></asp:sqldatasource>
                            </div>
                            <div class="tab-pane well" id="settings" style="max-height: 500px; overflow-y: scroll;">
                                <div class="table-responsive">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th>
                                                     Date & Time
                                                </th>
                                                <th>
                                                    IP
                                                </th>
                                                <th>
                                                    Browser
                                                </th>
                                                 <th>
                                                    Username
                                                </th>
                                                 
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater DataSourceID="log" ID="Repeater8" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("date_time_login") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ip_id")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("brow_name")%>
                                                            <%# Eval("brow_ver")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("uname")%>
                                                           
                                                        </td>
                                                        
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:sqldatasource ID="log" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM ([session_logs] INNER JOIN [userinfo] on [session_logs].[profile_id] = [userinfo].[profile_id])">
                                            </asp:sqldatasource>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--/tabs-->
                </div>
                <!--/col-->
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4>
                                Generalize Fields</h4>
                        </div>
                        <div class="panel-body" style="min-height: 300px">
                            <ul class="nav nav-tabs" id="Ul1">
                                <li class="active"><a href="#music" data-toggle="tab">Music</a></li>
                                <li><a href="#movies" data-toggle="tab">Movies</a></li>
                                <li><a href="#books" data-toggle="tab">Books</a></li>
                                <li><a href="#food" data-toggle="tab">Food</a></li>
                                <li><a href="#tv" data-toggle="tab">TV Shows</a></li>
                                <li><a href="#te" data-toggle="tab">Teams</a></li>
                                <li><a href="#places" data-toggle="tab">Places</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="well tab-pane active" id="music" style="max-height: 200px; overflow-y: scroll;">
                                    <div class="list-group">
                                        <asp:Repeater ID="murep" runat="server" DataSourceID="fields">
                                            <ItemTemplate>
                                                <li class="list-group-item">
                                                    <br />
                                                    <input type="text" runat="server" class="form-control" id="fn_txt" value='<%# Eval("m_name") %>' />
                                                    <br />
                                                    <asp:LinkButton ID="che" runat="server" CssClass="btn-xs btn-primary pull-right col-sm-offset-1"
                                                        OnCommand="setcheck" CommandArgument='<%# Eval("music_d_id")%>'>Check</asp:LinkButton>
                                                    <asp:LinkButton ID="rep" runat="server" CssClass="btn-xs btn-info pull-right col-sm-offset-1"
                                                        OnCommand="setdel" CommandArgument='<%# Eval("music_d_id")%>'>Delete&nbsp;</asp:LinkButton>
                                                    <asp:LinkButton ID="edi" runat="server" CssClass="btn-xs btn-danger pull-right col-sm-offset-1"
                                                        OnCommand="setedit" CommandArgument='<%# Eval("music_d_id")%>'>Edit & Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:sqldatasource ID="fields" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                            SelectCommand="SELECT * FROM [music_data] WHERE ([checked] = False)"></asp:sqldatasource>
                                    </div>
                                </div>
                                <div class="tab-pane well" id="movies" style="max-height: 200px; overflow-y: scroll;">
                                    <div class="list-group">
                                        <asp:Repeater ID="morep" runat="server" DataSourceID="mov">
                                            <ItemTemplate>
                                                <li class="list-group-item">
                                                    <br />
                                                    <input type="text" runat="server" class="form-control" id="fn_txt" value='<%# Eval("d_name") %>' />
                                                    <br />
                                                    <asp:LinkButton ID="che1" runat="server" CssClass="btn-xs btn-primary pull-right col-sm-offset-1"
                                                        OnCommand="set1check" CommandArgument='<%# Eval("movies_d_id")%>'>Check</asp:LinkButton>
                                                    <asp:LinkButton ID="rep1" runat="server" CssClass="btn-xs btn-info pull-right col-sm-offset-1"
                                                        OnCommand="set1del" CommandArgument='<%# Eval("movies_d_id")%>'>Delete&nbsp;</asp:LinkButton>
                                                    <asp:LinkButton ID="edi1" runat="server" CssClass="btn-xs btn-danger pull-right col-sm-offset-1"
                                                        OnCommand="set1edit" CommandArgument='<%# Eval("movies_d_id")%>'>Edit & Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:sqldatasource ID="mov" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM [movies_data] WHERE ([checked] = False)">
                                        </asp:sqldatasource>
                                    </div>
                                </div>
                                <div class="tab-pane well" id="books" style="max-height: 200px; overflow-y: scroll;">
                                    <div class="list-group">
                                        <asp:Repeater ID="borep" runat="server" DataSourceID="boo">
                                            <ItemTemplate>
                                                <li class="list-group-item">
                                                    <br />
                                                    <input type="text" runat="server" class="form-control" id="fn_txt" value='<%# Eval("d_name") %>' />
                                                    <br />
                                                    <asp:LinkButton ID="che2" runat="server" CssClass="btn-xs btn-primary pull-right col-sm-offset-1"
                                                        OnCommand="set2check" CommandArgument='<%# Eval("books_d_id")%>'>Check</asp:LinkButton>
                                                    <asp:LinkButton ID="rep2" runat="server" CssClass="btn-xs btn-info pull-right col-sm-offset-1"
                                                        OnCommand="set2del" CommandArgument='<%# Eval("books_d_id")%>'>Delete&nbsp;</asp:LinkButton>
                                                    <asp:LinkButton ID="edi2" runat="server" CssClass="btn-xs btn-danger pull-right col-sm-offset-1"
                                                        OnCommand="set2edit" CommandArgument='<%# Eval("books_d_id")%>'>Edit & Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:sqldatasource ID="boo" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM [books_data] WHERE ([checked] = False)">
                                        </asp:sqldatasource>
                                    </div>
                                </div>
                                <div class="tab-pane well" id="food" style="max-height: 200px; overflow-y: scroll;">
                                    <div class="list-group">
                                        <asp:Repeater ID="forep" runat="server" DataSourceID="foo">
                                            <ItemTemplate>
                                                <li class="list-group-item">
                                                    <br />
                                                    <input type="text" runat="server" class="form-control" id="fn_txt" value='<%# Eval("d_name") %>' />
                                                    <br />
                                                    <asp:LinkButton ID="che3" runat="server" CssClass="btn-xs btn-primary pull-right col-sm-offset-1"
                                                        OnCommand="set3check" CommandArgument='<%# Eval("food_d_id")%>'>Check</asp:LinkButton>
                                                    <asp:LinkButton ID="rep3" runat="server" CssClass="btn-xs btn-info pull-right col-sm-offset-1"
                                                        OnCommand="set3del" CommandArgument='<%# Eval("food_d_id")%>'>Delete&nbsp;</asp:LinkButton>
                                                    <asp:LinkButton ID="edi3" runat="server" CssClass="btn-xs btn-danger pull-right col-sm-offset-1"
                                                        OnCommand="set3edit" CommandArgument='<%# Eval("food_d_id")%>'>Edit & Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:sqldatasource ID="foo" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM [food_data] WHERE ([checked] = False)">
                                        </asp:sqldatasource>
                                    </div>
                                </div>
                                <div class="tab-pane well" id="tv" style="max-height: 200px; overflow-y: scroll;">
                                    <div class="list-group">
                                        <asp:Repeater ID="tvrep" runat="server" DataSourceID="t">
                                            <ItemTemplate>
                                                <li class="list-group-item">
                                                    <br />
                                                    <input type="text" runat="server" class="form-control" id="fn_txt" value='<%# Eval("tv_d") %>' />
                                                    <br />
                                                    <asp:LinkButton ID="che4" runat="server" CssClass="btn-xs btn-primary pull-right col-sm-offset-1"
                                                        OnCommand="set4check" CommandArgument='<%# Eval("tv_d_id")%>'>Check</asp:LinkButton>
                                                    <asp:LinkButton ID="rep4" runat="server" CssClass="btn-xs btn-info pull-right col-sm-offset-1"
                                                        OnCommand="set4del" CommandArgument='<%# Eval("tv_d_id")%>'>Delete&nbsp;</asp:LinkButton>
                                                    <asp:LinkButton ID="edi4" runat="server" CssClass="btn-xs btn-danger pull-right col-sm-offset-1"
                                                        OnCommand="set4edit" CommandArgument='<%# Eval("tv_d_id")%>'>Edit & Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:sqldatasource ID="t" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM [tv_data] WHERE ([checked] = False)">
                                        </asp:sqldatasource>
                                    </div>
                                </div>
                                <div class="tab-pane well" id="te" style="max-height: 200px; overflow-y: scroll;">
                                    <div class="list-group">
                                        <asp:Repeater ID="terep" runat="server" DataSourceID="e">
                                            <ItemTemplate>
                                                <li class="list-group-item">
                                                    <br />
                                                    <input type="text" runat="server" class="form-control" id="fn_txt" value='<%# Eval("team_d") %>' />
                                                    <br />
                                                    <asp:LinkButton ID="che5" runat="server" CssClass="btn-xs btn-primary pull-right col-sm-offset-1"
                                                        OnCommand="set5check" CommandArgument='<%# Eval("team_d_id")%>'>Check</asp:LinkButton>
                                                    <asp:LinkButton ID="rep5" runat="server" CssClass="btn-xs btn-info pull-right col-sm-offset-1"
                                                        OnCommand="set5del" CommandArgument='<%# Eval("team_d_id")%>'>Delete&nbsp;</asp:LinkButton>
                                                    <asp:LinkButton ID="edi5" runat="server" CssClass="btn-xs btn-danger pull-right col-sm-offset-1"
                                                        OnCommand="set5edit" CommandArgument='<%# Eval("team_d_id")%>'>Edit & Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:sqldatasource ID="e" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM [team_data] WHERE ([checked] = False)">
                                        </asp:sqldatasource>
                                    </div>
                                </div>
                                <div class="tab-pane well" id="places" style="max-height: 200px; overflow-y: scroll;">
                                    <div class="list-group">
                                        <asp:Repeater ID="plrep" runat="server" DataSourceID="pl">
                                            <ItemTemplate>
                                                <li class="list-group-item">
                                                    <br />
                                                    <input type="text" runat="server" class="form-control" id="fn_txt" value='<%# Eval("places_d") %>' />
                                                    <br />
                                                    <asp:LinkButton ID="che6" runat="server" CssClass="btn-xs btn-primary pull-right col-sm-offset-1"
                                                        OnCommand="set6check" CommandArgument='<%# Eval("places_d_id")%>'>Check</asp:LinkButton>
                                                    <asp:LinkButton ID="rep6" runat="server" CssClass="btn-xs btn-info pull-right col-sm-offset-1"
                                                        OnCommand="set6del" CommandArgument='<%# Eval("places_d_id")%>'>Delete&nbsp;</asp:LinkButton>
                                                    <asp:LinkButton ID="edi6" runat="server" CssClass="btn-xs btn-danger pull-right col-sm-offset-1"
                                                        OnCommand="set6edit" CommandArgument='<%# Eval("places_d_id")%>'>Edit & Save</asp:LinkButton>
                                                    <br />
                                                    <br />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:sqldatasource ID="pl" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM [places_data] WHERE ([checked] = False)">
                                        </asp:sqldatasource>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4>
                                Reported Profiles</h4>
                        </div>
                        <div class="panel-body" style="max-height: 165px; overflow-y: scroll;">
                            <div class="list-group">
                               <asp:Repeater ID="Repeater9" runat="server" DataSourceID="reppro">
                                    <ItemTemplate>
                                        <li class="list-group-item">
                                            <div class="list-group-item" style="min-height: 160px">
                                                <div class="list-group-item-heading col-sm-5">
                                                    <h3>
                                                        <%# Eval("fname")%>
                                                        <br />
                                                        <%# Eval("lname")%></h3>
                                                       <asp:LinkButton CssClass="btn btn-success fa fa-user" ID="LinkButton1" runat="server" OnCommand="unpro"
                                                        CommandArgument='<%# Eval("profile_id") %>'>
                                                        Unreport</asp:LinkButton>
                                                   &nbsp; <as class="btn btn-danger fa fa-user-times" data-toggle="modal" data-target="#<%# Eval("fname")%>unfmodal">
                                                   Delete </as>
                                                
                                                </div>
                                            </div>
                                        </li>
                                        <div class="modal fade" id="<%# Eval("fname")%>unfmodal">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                        ×</button>
                                                    <br />
                                                </div>
                                                <div class="modal-body">
                                                    <p>
                                                        Delete
                                                        <%# Eval("fname")%>'s account
                                                        ?</p>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                                        Cancel</button>
                                                    <asp:LinkButton CssClass="btn btn-primary" ID="LinkButton2" runat="server" OnCommand="delpro"
                                                        CommandArgument='<%# Eval("profile_id") %>'>
                                                        Yes</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Unfollow Modal-->
                                  
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:sqldatasource ID="reppro" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                                    SelectCommand="SELECT * FROM userinfo WHERE ([is_reported] = True)"></asp:sqldatasource>
                            </div>
                        </div>
                    </div>
                </div>
                <!--/col-span-6-->
            </div>
            <!--/row-->
        </div>
    <!--/col-span-9-->
    </div>
    <!-- /Main -->
</asp:Content>
