<%@ Page Title="" Language="VB" MasterPageFile="~/UI.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body_cont" Runat="Server">
      <div class="col-xs-offset-2"> <!-- WRITE NEW POST MODULE-->
    
    <h1>Around You
    </h1>
    <br />
    <div class="tab-pane active fade in col-sm-12" id="innsbut">
        <a class="fa fa-pencil btn btn-info btn-circle" data-toggle="collapse" data-target="#inns"></a><span class="text-muted">&nbsp;&nbsp;Write about today!</span>
        <br />
        <br />
    </div>
    <div class="collapse col-sm-12" id="inns">
     
                <div class="container col-sm-8 col-sm-offset-2">
                    <div class="form-group col-sm-8">
                        <br />
                        <!-- DATE SELECTION -->
                        <div class="form-group has-feedback col-sm-12">
                            <div class="form-group col-sm-12">
                                <div class="input-group date" id="datetimepicker3">
                                    <input id="date_txt" class="form-control" runat="server" data-format="dd/MM/yyyy"
                                        type="text" minlength="6" placeholder="Writing a page for the date..." pattern="(?:(?:0[1-9]|1[0-2])[\/\\-. ]?(?:0[1-9]|[12][0-9])|(?:(?:0[13-9]|1[0-2])[\/\\-. ]?30)|(?:(?:0[13578]|1[02])[\/\\-. ]?31))[\/\\-. ]?(?:19|20)[0-9]{2}"
                                        required />
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group col-sm-12">
                            <asp:TextBox ID="posttxt" CssClass="form-control col-sm-12" placeholder="Hi! This is the infamous *What's on your mind?* box. ;)"
                                TextMode="MultiLine" Columns="500" Rows="10" runat="server" />
                        </div>
                    </div>
                    <!-- FEELING WHAT?, HASHES & PUBLISH POST?-->
                    <div class="container well-lg col-sm-4">
                        <asp:FileUpload ID="FileUpload1" CssClass="col-sm-12 btn-primary" runat="server" />
                        <%--<ajaxToolkit:AsyncFileUpload runat="server"
                            ID="FileUpload1" />--%>
                        <br />
                        <br />
                        <div class="form-group col-sm-12 pull-right">
                            <div class="input-group" style="width: 100%">
                                <input type="text" id="ftxt" runat="server" class="form-control acf" placeholder="Any particular feelings?" />
                                <span class="input-group-addon">
                                    <input type="checkbox" id="happycheck" runat="server" />
                                    Happy?</span>
                                <div class="help-block with-errors">
                                </div>
                            </div>
                            <br />
                            <input type="text" id="hashtxt" runat="server" class="token-input ui-autocomplete-input form-control"
                                placeholder="#hashes" />
                            <br />
                            <div class="form-group col-sm-12 pull-right">
                                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="postwrite" Style="text-decoration: none" ToolTip="Save Diary Page"
                                    CssClass="btn-lg col-sm-6 btn-success pull-right"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Write</asp:LinkButton>

                                <label>
                                    <br />
                                    <input type="checkbox" id="publiccheck" runat="server" />
                                    Publish to Public?
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

        <br />
        <br />
    </div>
   <div class="col-sm-5">
        <div class="search-form">
            <div class="form-group has-feedback">
                <label for="search" class="sr-only">
                    Search</label>
                <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seahome"
                    runat="server" AutoPostBack="True" OnTextChanged="txtSearch_KeyUp" onkeyup="SetDelay();" />
                <span class="glyphicon glyphicon-search form-control-feedback"></span>
            </div>
        </div>
    </div> <br /><br /> <br />
    <hr />
    <br /><br /><br /></div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="False" UpdateMode="Conditional">
        <ContentTemplate>
            <!--FEED-->
            <div id="mainfeed" class="container main-container col-sm-8 col-sm-offset-2">
                <!--prev: 110px and sm-8-->
                <div role="tabpanel" class="tab-pane active" id="panel-1">
                    <div class="row masonry-container">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div class="col-sm-6 item">

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
                                        <div class="panel-body hide" style="padding: 0px;">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
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
                                                                            <p class="meta"><a href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>" style="text-decoration: none;"><%# Eval("uname")%></a><asp:LinkButton Visible='<%# if (DataBinder.Eval(Container.DataItem, "profile_id") = Session("idsess"), "True","False") %>' ID="LinkButton2" runat="server" CommandArgument='<%# Eval("comment_id") %>' OnCommand="delcom" CssClass="pull-right" ToolTip="Delete comment." Style="text-decoration: none; color: #008cba; padding-top: 5px;">&nbsp;<i class="fa fa-trash"></i>&nbsp;</asp:LinkButton>&nbsp;<span class="pull-right" data-livestamp="<%# Eval("date_written_c", "{0:f}")%>"></span></p>
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
                    </div>
                    <!--/.masonry-container  -->
                </div>
                <!--/.tab-panel -->
            </div>
            <!--/.masonry-container  -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID='seahome' EventName="textchanged" />
        </Triggers>
    </asp:UpdatePanel>
    <!--SELF_RA_GRID -->
    <div class="panel panel-warning col-sm-3 pull-right" runat="server" visible="false">
        <div class="panel-heading">
            <h3 class="panel-title">Recent Activity</h3>
        </div>
        <div class="panel-body" style="max-height: 350px; min-height: 50px; overflow-y: scroll;">
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="6000" Enabled="True">
            </asp:Timer>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="notifrepeater" runat="server">
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">


        function InfiniteScroll() {
            //toastr.options = {
            //    "closeButton": false,
            //    "debug": false,
            //    "newestOnTop": false,
            //    "progressBar": false,
            //    "positionClass": "toast-bottom-right",
            //    "preventDuplicates": true,
            //    "onclick": null,
            //    "showDuration": "300",
            //    "hideDuration": "300",
            //    "timeOut": "4300",
            //    "extendedTimeOut": "3000",
            //    "showEasing": "swing",
            //    "hideEasing": "linear",
            //    "showMethod": "fadeIn",
            //    "hideMethod": "fadeOut"
            //}
            //toastr.warning("END OF PAGE");
            //$('#divPostsLoader').html('<img src="images/loader.gif">');

            ////send a query to server side to present new content
            //$.ajax({
            //    type: "POST",
            //    url: "Home.aspx/newposts",
            //    data: "{}",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {
            //        if (data != "") {
            //            $('.divLoadData:last').after(data.d);
            //        }
            //        $('#divPostsLoader').empty();
            //    }
            //})
        };

        function RefreshUpdatePanel() {
            __doPostBack('<%= seahome.ClientID %>', '');
        };

        function SetDelay() {
            setTimeout("RefreshUpdatePanel()", 100);
        }

        function nonexist() {
            setTimeout(function () {
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-bottom-right",
                    "preventDuplicates": true,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "300",
                    "timeOut": "4300",
                    "extendedTimeOut": "3000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                toastr.warning("Hi there! Seems like your feed is empty. :O");
                setTimeout(function () {
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-bottom-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "300",
                        "timeOut": "4300",
                        "extendedTimeOut": "3000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }
                    toastr.info("Lemme suggest you some people you should follow! :D");
                    setTimeout(function () {
                        toastr.options = {
                            "closeButton": false,
                            "debug": false,
                            "newestOnTop": false,
                            "progressBar": false,
                            "positionClass": "toast-bottom-right",
                            "preventDuplicates": true,
                            "onclick": null,
                            "showDuration": "300",
                            "hideDuration": "300",
                            "timeOut": "4300",
                            "extendedTimeOut": "3000",
                            "showEasing": "swing",
                            "hideEasing": "linear",
                            "showMethod": "fadeIn",
                            "hideMethod": "fadeOut"
                        }
                        toastr.info("Okayyyyyyyy! Lemme see. :3");
                        setTimeout(function () {
                            window.location = "Explore.aspx";
                        }, 2000)
                    }, 5000)
                }, 3000)
            }, 3000)

        }
    </script>

</asp:Content>

