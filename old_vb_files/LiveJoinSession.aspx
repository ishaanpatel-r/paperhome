<%@ Page Title="" Language="VB" MasterPageFile="~/Navigation.master" AutoEventWireup="false"
    CodeFile="LiveJoinSession.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
    <h1>
        ScreenBoard <small>(You're attending a LiveJoin session on
            <%Response.Write(Session("ljsub"))%>.)</small>
    </h1>
    <br />
    <div class="col-sm-5">
        <div class="search-form">
            <div class="form-group has-feedback">
                <label for="search" class="sr-only">
                    Search</label>
                <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seahome"
                    runat="server" AutoPostBack="True" />
                <span class="glyphicon glyphicon-search form-control-feedback"></span>
            </div>
        </div>
    </div>
    <hr />
    <br />
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="6000" Enabled="True">
    </asp:Timer>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Repeater runat="server" ID="notes">
                <ItemTemplate>
                    <div class="well-lg container-fluid jumbotron col-sm-7">
                        <pre style="background-color:White; border: none;">
                            <%#Eval ("content") %></pre>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            <asp:AsyncPostBackTrigger ControlID="btn_chat" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
        <div class="col-sm-4 col-sm-offset-1">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-comment"></span>&nbsp;&nbsp;Group Discussion<asp:LinkButton ID="refclick" runat="server" CssClass="pull-right" OnClick="refchat"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                    </div>
                    <div class="panel-body" style="overflow-y: scroll; height: 300px;">
                        <ul class="chat">
                            <asp:Repeater id="gd" runat="server" >
                                <ItemTemplate>
                                    <li class="<%# if (DataBinder.Eval(Container.DataItem, "sender") = Session("idsess"), "right","left") %> clearfix">
                                        <span class="chat-img <%# if (DataBinder.Eval(Container.DataItem, "sender") = Session("idsess"), "pull-right","pull-left") %>  col-sm-3">
                                           <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration:none;"> <img src='<%# Eval("dp_url")%>' alt='<%# Eval("fname")%><%# Eval("lname")%>' class="img-circle col-sm-12" /></a>
                                        </span>
                                        <div class="chat-body clearfix">
                                            <div class="header">
                                                <strong class="<%# if (DataBinder.Eval(Container.DataItem, "sender") = Session("idsess"), "pull-right","") %> primary-font">
                                                    <%# Eval("fname")%>&nbsp;<%# Eval("lname")%></strong> <small class="pull-<%# if (DataBinder.Eval(Container.DataItem, "sender") = Session("idsess"), "left","right") %> text-muted">
                                                        <span class="pull-right text-muted small time-line" data-livestamp="<%# Eval("bit_date_written", "{0:f}")%>">
                                                    </small>
                                            </div>
                                            <%# if (DataBinder.Eval(Container.DataItem, "sender") = Session("idsess"), "<br />","") %>
                                            <p class="<%# if (DataBinder.Eval(Container.DataItem, "sender") = Session("idsess"), "pull-right","pull-left") %>">
                                                <%# Eval("bit")%>
                                            </p>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:AccessDataSource ID="bits_data" runat="server" DataFile="~/paperhome_data.accdb"
                                SelectCommand="SELECT [userinfo].[profile_id], [dp_url], [fname], [lname], [bit], [bit_date_written], [sender] FROM (([bit_data] INNER JOIN [livesessions] ON [bit_data].[session_id] = [livesessions].[session_id]) LEFT JOIN [userinfo] ON [bit_data].[sender] = [userinfo].[profile_id]) WHERE [livesessions].[session_id] = hm ORDER BY [bit_date_written] DESC">
                                <SelectParameters>
                                    <asp:SessionParameter Name="hm" SessionField="ljjsess" Type="Int32" />
                                </SelectParameters>
                            </asp:AccessDataSource>
                        </ul>
                    </div>
                    <div class="panel-footer">
                        <div class="input-group">
                            <input id="bitin" type="text" runat="server" class="form-control" placeholder="Type your doubt here..." />
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btn_chat" OnClick="sendbit" runat="server" CssClass="btn btn-info btn-sm">
                    Send</asp:LinkButton>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            
        </ContentTemplate>
        <Triggers>
              <asp:AsyncPostBackTrigger ControlID="refclick" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btn_chat" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
