<%@ Page Title="" Language="VB" MasterPageFile="~/UI.master" AutoEventWireup="false" CodeFile="MyDiary.aspx.vb" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body_cont" Runat="Server">
      <div class="col-sm-offset-2">
    <h1>My Diary
    </h1>
    <br />
    
    <div class="tab-pane active fade in col-sm-12" id="innsbut">
        <a class="fa fa-eye btn btn-info btn-circle" data-toggle="collapse" data-target="#inns"></a><span class="text-muted">&nbsp;&nbsp;Relive Moments.</span>
        <br />
        <br />
    </div>
    <div class="collapse col-sm-8 col-sm-offset-2" id="inns">
       <div class="panel-body">
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#happy" data-toggle="tab" aria-expanded="true">Happy</a></li>
                    <li class=""><a href="#sad" data-toggle="tab" aria-expanded="false">Sad</a></li>
                </ul>
                <div id="myTabContent" class="tab-content">
                    <div class="tab-pane fade active in" id="happy">
                        <h3 class="col-sm-12">Show me happy memories,
                        </h3>
                        <div class="col-sm-6">
                            <br />
                            <label>
                                From:</label>
                            <div class="input-group date col-sm-8" id="datetimepicker3">
                                <input id="hapfromrm" class="form-control" runat="server" data-format="dd/MM/yyyy"
                                    type="text" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            </div>
                            <br />
                            <label>
                                To:</label>
                            <div class="input-group date col-sm-8" id="datetimepicker4">
                                <input id="haptorm" class="form-control" runat="server" data-format="dd/MM/yyyy"
                                    type="text" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <br />
                            <label class="col-sm-12 control-label">
                                That:
                            </label>
                            <div class="col-sm-12">
                                <asp:RadioButtonList ID="Radiobuttonlist3" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">&nbsp;&nbsp;May&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2">&nbsp;&nbsp;May Not</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <label class="col-sm-12 control-label">
                                <br />
                                contain:
                            </label>
                            <div class="col-sm-12">
                                <input type="text" id="happy_words" runat="server" class="form-control" placeholder="certain words." />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <asp:LinkButton OnClick="happy_go" ID="hapclick" runat="server" CssClass="btn btn-success pull-right">Go!</asp:LinkButton>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="sad">
                        <h3 class="col-sm-12">Show me sad memories,
                        </h3>
                        <div class="col-sm-6">
                            <br />
                            <label>
                                From:</label>
                            <div class="input-group date col-sm-8" id="datetimepicker5">
                                <input id="sadfromrm" class="form-control" runat="server" data-format="dd/MM/yyyy"
                                    type="text" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            </div>
                            <br />
                            <label>
                                To:</label>
                            <div class="input-group date col-sm-8" id="datetimepicker6">
                                <input id="sadtorm" class="form-control" runat="server" data-format="dd/MM/yyyy"
                                    type="text" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <br />
                            <label class="col-sm-12 control-label">
                                That:
                            </label>
                            <div class="col-sm-12">
                                <asp:RadioButtonList ID="Radiobuttonlist1" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">&nbsp;&nbsp;May&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2">&nbsp;&nbsp;May Not</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <label class="col-sm-12 control-label">
                                <br />
                                contain:
                            </label>
                            <div class="col-sm-12">
                                <input type="text" id="sad_words" runat="server" class="form-control" placeholder="certain words." />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <asp:LinkButton OnClick="sad_go" ID="sadclick" runat="server" CssClass="btn btn-success pull-right">Go!</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        <br />
        <br />
    </div>
           <hr /> <div class="col-sm-5">
        <div class="search-form">
            <div class="form-group has-feedback">
                <label for="search" class="sr-only">
                    Search</label>
                <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seahome"
                    runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp" onkeyup="SetDelay();" />
                <span class="glyphicon glyphicon-search form-control-feedback"></span>
            </div>
        </div>
    </div> <br /> <br /> <br />
    <hr />
    <br /><br /><br /></div>
    <div class="tab-content">
        <div id="overview" class="tab-pane fade active in" runat="server">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="True" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="container main-container col-sm-10 col-sm-offset-1">
                        <div role="tabpanel" class="tab-pane active" id="panel-1">
                            <br />
                            <br />
                            <div class="row masonry-container">
                                <asp:Repeater ID="diary" runat="server">
                                    <ItemTemplate>
                                        <div class="col-sm-4 item">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" style="background-color: #f9fcfe;">
                                                    <span><%# Eval("hashes")%></span>
                                                </div>
                                                <div class="panel-image">
                                                    <center style="background-color: black;">
                                                        <img class="img-responsive enlargewrite" src='<%# Eval("img_att") %>'></center>
                                                    <div style="padding-left: 10px;">
                                                        <h2><a style="text-decoration: none">
                                                            <%# Eval("date_written", "{0:MMMM d, yyyy}")%></a>&nbsp;<%# If(DataBinder.Eval(Container.DataItem, "published") = 0, "<small>(private)</small>", "<small class='text-success'>(published)</small>")%></h2>
                                                        <h5 class="text-muted">&nbsp;&nbsp;&nbsp;feeling
                                            <%# Eval("feeling_type")%></h5>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnCommand="pubunpub"
                                                            CommandArgument='<%# Eval("writes_id") %>'>&nbsp;&nbsp;&nbsp;<%# If(DataBinder.Eval(Container.DataItem, "published") = 0, "Show on Openbook", "Hide from Openbook")%>
                                                        </asp:LinkButton>
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
                                                    <a data-toggle="modal" data-target="#<%# Eval("writes_id")%>burnwrite" class="btn btn-warning btn-xs pull-right">
                                                        <i class="fa fa-trash"></i></a>

                                                    <span class="toggler fa fa-chevron-down pull-left" onclick="var tog = $(this); var secondDiv = tog.parent().prev();var firstDiv = secondDiv.prev();var $container = $('.masonry-container');firstDiv.children('p').toggleClass('hide');secondDiv.toggleClass('hide');tog.toggleClass('fa fa-chevron-up fa fa-chevron-down'); $container.masonry({columnWidth: '.item',itemSelector: '.item'});return false;" style="color: #008cba">&nbsp;Comments</span>
                                                </div>
                                            </div>
                                        </div>

                                        <!--My Diary Post Viewing Modal-->
                                        <div class="modal fade" id='<%# Eval("writes_id")%>burnwrite'>
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            ×</button>
                                                        <br />
                                                    </div>
                                                    <div class="modal-body">
                                                        <p>
                                                            Are you sure you want to delete this write?
                                                        </p>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-default" data-dismiss="modal">
                                                            Cancel</button>
                                                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server" OnCommand="burnwrite"
                                                            CommandArgument='<%# Eval("writes_id")%>'> Yes </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--Burn page Modal-->

                                    </ItemTemplate>
                                </asp:Repeater>
                                <!--/.item  -->
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID='seahome' EventName="textchanged" />
                    <asp:AsyncPostBackTrigger ControlID='hapclick' EventName="click" />
                    <asp:AsyncPostBackTrigger ControlID='sadclick' EventName="click" />

                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!--/.masonry-container  -->
    </div>
    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= seahome.ClientID %>', '');
        };

        function SetDelay() {
            setTimeout("RefreshUpdatePanel()", 100);
        }
    </script>
</asp:Content>

