<%@ Page Title="" Language="VB" MasterPageFile="~/Navigation.master" AutoEventWireup="false"
    CodeFile="Messages.aspx.vb" Inherits="_Default" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
    <h1>
        Messages
    </h1>
    <br />
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="tab-content">
                <div class="tab-pane fade in active" id="inbox">
                    <div class="panel panel-default col-sm-3">
                        <br />
                        <!-- COMPOSE NEW BUTTON-->
                        <div>
                            <a data-toggle="modal" data-target="#messmodalmess" class="btn btn-primary btn-sm btn-block"
                                role="button">Compose New</a>
                            <hr />
                        </div>
                        <!--FEELPALS-->
                        <div class="list-group" style="max-height: 350px; min-height: 50px; overflow-y: scroll;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:Repeater ID="peeps" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="HyperLink1" OnCommand="thread_sel" runat="server" CommandArgument='<%# Eval("profile_id") %>'>
                              <div class="panel panel-default panel-body">
                                     <div class="list-group-item-heading col-sm-5">
                                        <h3>
                                           <%# Eval("fname") %>
                                            <br />
                                          <%# Eval("lname") %></h3>
                                        
                                    </div>
                                    <div class="list-group-item-text col-sm-5 pull-right">
                                        <img src='<%# Eval("dp_url") %>' alt='<%# Eval("fname")%> acquired invisibility' class="img-responsive img-circle" />
                                    </div>
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
            <!--peepbox-->
            <div class="message-wrap col-lg-7 col-lg-offset-1">
                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="chatname">
                    <ItemTemplate>
                        <div class="alert alert-info msg-date">
                            <a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;"
                                style="text-decoration: none; color: Gray;"><strong>
                                    <%# Eval("fname")%>&nbsp;<%# Eval("lname")%>
                                </strong></a><a data-toggle="modal" data-target="#<%# Eval("profile_id")%>burnwrite"
                                    class="btn btn-danger btn-xs pull-right"><i class='fa fa-trash'></i>
                                </a>
                            <div class="modal fade" id="<%# Eval("profile_id")%>burnwrite">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-body">
                                            <p>
                                                Are you sure you want to delete this conversation with
                                                <%# Eval("fname")%>
                                                <%# Eval("lname")%>?<br />
                                                Note: This will delete the conversation from both sides and is un-recoverable.</p>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                Cancel</button>
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server" OnCommand="del_con"
                                                CommandArgument='<%# Eval("profile_id")%>'> Yes </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--Remove Follower Modal-->
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <!--chattop-->
                <asp:sqldatasource ID="chatname" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"
                    SelectCommand="SELECT * FROM [userinfo] WHERE [profile_id] = @herm">
                    <SelectParameters>
                        <asp:SessionParameter Name="herm" SessionField="psess" Type="Int32" />
                    </SelectParameters>
                </asp:sqldatasource>
                <!--chattop-->
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
            <!--chatbox-->
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="messmodalmess">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>s
                    <br />
                    <div class="modal-title">
                        New Message</div>
                </div>
                <div class="modal-body">
                    <label>
                        To:</label><input runat="server" id="wow" class="form-control ac" placeholder="Search..."
                            data-provide="typeahead" />
                    <hr />
                    <asp:TextBox ID="cont" type="text" TextMode="MultiLine" Columns="500" Rows="10" CssClass="form-control"
                        runat="server" AutoPostBack="True" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cancel</button>
                    <asp:LinkButton ID="LinkButton3" OnClick="sendmessnew" runat="server" CssClass="btn btn-primary">
                    <i class="fa fa-plus"></i> &nbsp;Send Message</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <!--Compose Message Modal-->
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
