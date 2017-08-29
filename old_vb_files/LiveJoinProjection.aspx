<%@ Page Title="" Language="VB" MasterPageFile="~/Navigation.master" AutoEventWireup="false"
    CodeFile="LiveJoinProjection.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
 <h1>
        Live Projection
       
    </h1>
    <br />
    <div class="col-sm-5">
        <div class="search-form">
            <div class="form-group has-feedback">
                <label for="search" class="sr-only">
                    Search</label>
                <asp:TextBox type="text" class="form-control inputsea" name="search" ID="TextBox1" runat="server"
                    AutoPostBack="True" OnTextChanged="txtSearch_KeyUp" onkeyup="SetDelay();" />
                <span class="glyphicon glyphicon-search form-control-feedback"></span>
            </div>
        </div>
    </div>
    
    <hr />
    <br />


    
<div class="col-sm-7">
 <asp:TextBox type="text" class="form-control" name="search" ID="seahome"  TextMode="MultiLine"
           Columns="50"
           Rows="20"
        runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp" onkeyup="SetDelay();" />
</div>
   
  
   <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="up">
        <ContentTemplate>
        <div class="col-sm-4 col-sm-offset-1">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-comment"></span>&nbsp;&nbsp;Group Discussion
                    </div>
                    <div class="panel-body" style="overflow-y: scroll; height: 300px;">
                        <ul class="chat">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <li class="<%# if (DataBinder.Eval(Container.DataItem, "sender") = Session("idsess"), "right","left") %> clearfix">
                                        <span class="chat-img <%# if (DataBinder.Eval(Container.DataItem, "sender") = Session("idsess"), "pull-right","pull-left") %>  col-sm-3">
                                            <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration:none;"><img src='<%# Eval("dp_url")%>' alt='<%# Eval("fname")%><%# Eval("lname")%>' class="img-circle col-sm-12" /></a>
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
                                    <asp:SessionParameter Name="hm" SessionField="ljpsess" Type="Int32" />
                                </SelectParameters>
                            </asp:AccessDataSource>
                        </ul>
                    </div>
                    <div class="panel-footer">
                        <div class="input-group">
                            <input id="bitin" type="text" runat="server" class="form-control" placeholder="Type here..." />
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
            <asp:AsyncPostBackTrigger ControlID='seahome' EventName="textchanged" />
            <asp:AsyncPostBackTrigger ControlID='btn_chat' EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
   
    
    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= seahome.ClientID %>', '');
        };

        function SetDelay() {
            setTimeout("RefreshUpdatePanel()", 0);
        }
    </script>
</asp:Content>
