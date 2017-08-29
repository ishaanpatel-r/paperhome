<%@ Page Title="" Language="VB" MasterPageFile="~/Navigation.master" AutoEventWireup="false"
    CodeFile="MyPhotos.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
    <br /><br />
    <h1>
        Photos &nbsp;</h1>
    <br />
    <div class="col-sm-5">
        <div class="search-form">
            <div class="form-group has-feedback">
                <label for="search" class="sr-only">
                    Search</label>
                <asp:TextBox type="text" class="form-control inputsea" ID="seareq" runat="server"
                    AutoPostBack="True" OnTextChanged="txtSearch_KeyUp" onkeyup="SetDelayreq();" />
                <span class="glyphicon glyphicon-search form-control-feedback"></span>
            </div>
        </div>
    </div>
    <hr />
    <br />
    <br />
   
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-sm-2">
                <ul class="list-group">
                    <br />
                    <br />
                    <asp:LinkButton ID="allclick" OnClick="allpic" CssClass="list-group-item" runat="server">All</asp:LinkButton>
                    <asp:LinkButton ID="youclick" OnClick="youpic" CssClass="list-group-item" runat="server">You</asp:LinkButton>
                    <asp:LinkButton ID="tripclick" OnClick="trippic" CssClass="list-group-item" runat="server">Trips</asp:LinkButton>
                    <asp:LinkButton ID="parclick" OnClick="parpic" CssClass="list-group-item" runat="server">Parties</asp:LinkButton>
                    <asp:LinkButton ID="eveclick" OnClick="evepic" CssClass="list-group-item" runat="server">Events</asp:LinkButton>
                    <asp:LinkButton ID="obclick" OnClick="obpic" CssClass="list-group-item" runat="server">Openbook</asp:LinkButton>
                    <asp:LinkButton ID="dpclick" OnClick="dppic" CssClass="list-group-item" runat="server">Profile Photos</asp:LinkButton>
                    
                    <br />
                    
                    <h4>
                        <b><a class="list-group-item col-sm-3" data-toggle="tab" href="#alb_list">
                            <center>
                                +</center>
                        </a></b>
                    </h4>
                    <div class="tab-content">
                        <div class="tab-pane fade in" id="alb_list">
                            <ul class="list-group activity-list col-sm-6">
                                <asp:LinkButton ID="you_click_up" runat="server" CssClass="list-group-item" OnCommand="makealbumsession"
                                    CommandArgument="you">in <br /> You.</asp:LinkButton>
                                <asp:LinkButton ID="trip_click_up" runat="server" CssClass="list-group-item" OnCommand="makealbumsession"
                                    CommandArgument="trip">in <br /> Trips.</asp:LinkButton>
                                <asp:LinkButton ID="par_click_up" runat="server" CssClass="list-group-item" OnCommand="makealbumsession"
                                    CommandArgument="parties">in Parties.</asp:LinkButton>
                                <asp:LinkButton ID="eve_click_up" runat="server" CssClass="list-group-item" OnCommand="makealbumsession"
                                    CommandArgument="events">in Events.</asp:LinkButton>
                            </ul>
                        </div>
                    </div>
                </ul>
            </div>
            <div runat="server" class="col-sm-10 col-sm-offset-0" id="youtabpics">
                <div id="instafetch" runat="server" visible="false" style="min-height: 700px;">
                    <div id="instapola" class="row masonry-container col-sm-offset-1 polaroid" style="min-height: 700px;">
                    </div>
                </div>
                <div class="well-lg" id="empdiv" style="display: none" runat="server">
                    <div class="jumbotron">
                        <h2>
                            &nbsp; &nbsp; No pictures here. :O</h2>
                    </div>
                </div>
                <!-- The container for the list of example images -->
                <div id="normalfetch" runat="server" visible="true">
                    <div id="normpola" class="row masonry-container col-sm-offset-1 polaroid">
                        <asp:Repeater ID="gallery" runat="server">
                            <ItemTemplate>
                                <figure class="item">
                               
                                             <a href="img/<%# Eval("user_path") %>/<%# Eval("folder_type_path") %>/<%# Eval("pic_name") %>"
                                                    title="<%# Eval("caption") %>" data-gallery style="text-decoration: none;">
<img src="img/<%# Eval("user_path") %>/<%# Eval("folder_type_path") %>/<%# Eval("pic_name") %>" alt="<%# Eval("caption") %>" class="img-responsive" /> </a>
<figcaption><asp:LinkButton id="delphoclick" runat="server" commandargument='<%# Eval("photo_id") %>' oncommand="delpho" Cssclass="pull-right" style="text-decoration:none; color:#008cba; padding-top:5px;"><i class="fa fa-trash"></i></asp:LinkButton><asp:LinkButton visible='<%# if (DataBinder.Eval(Container.DataItem, "folder_type_path") = "coverphotos", "True","False") %>' id="LinkButton1" runat="server" commandargument='<%# Eval("photo_id") %>' oncommand="makeobp" Cssclass="pull-left" tooltip="Use this as Openbook background." style="text-decoration:none; color:#008cba; padding-top:5px;"><i class="glyphicon glyphicon-book"></i></asp:LinkButton><asp:LinkButton visible='<%# if (DataBinder.Eval(Container.DataItem, "folder_type_path") = "displaypictures", "True","False") %>' id="LinkButton2" runat="server" commandargument='<%# Eval("photo_id") %>' oncommand="makedp" Cssclass="pull-left" tooltip="Use this as profile photo." style="text-decoration:none; color:#008cba; padding-top:5px;"><i class="glyphicon glyphicon-user"></i></asp:LinkButton></figcaption>
</figure>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <!-- The Bootstrap Image Gallery lightbox, should be a child element of the document body -->
                <div id="blueimp-gallery" class="blueimp-gallery blueimp-gallery-controls" data-use-bootstrap-modal="false">
                    <div class="slides">
                    </div>
                    <a class="prev">‹</a> <a class="next">›</a> <a class="close">×</a> <a class="play-pause">
                    </a>
                    <h3 class="title">
                    </h3>
                    <ol class="indicator">
                    </ol>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="allclick" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="youclick" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="tripclick" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="parclick" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="eveclick" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="obclick" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="dpclick" EventName="click" />
            
            <asp:AsyncPostBackTrigger ControlID="you_click_up" EventName="command" />
            <asp:AsyncPostBackTrigger ControlID="trip_click_up" EventName="command" />
            <asp:AsyncPostBackTrigger ControlID="par_click_up" EventName="command" />
            <asp:AsyncPostBackTrigger ControlID="eve_click_up" EventName="command" />
            <asp:AsyncPostBackTrigger ControlID='seareq' EventName="textchanged" />
        </Triggers>
    </asp:UpdatePanel>
      <!-- <asp:AsyncPostBackTrigger ControlID="instclick" EventName="click" /> -->
    <script type="text/javascript">

        function RefreshUpdatePanelreq() {
            __doPostBack('<%= seareq.ClientID %>', '');
        };

        function SetDelayreq() {
            setTimeout("RefreshUpdatePanelreq()", 100);
        }
    </script>
</asp:Content>
