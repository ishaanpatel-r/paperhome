<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRIAL UI.aspx.vb" Inherits="TRIAL_UI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/simple-sidebar.css" rel="stylesheet" type="text/css" />
    <link href="external/google-code-prettify/prettify.css" rel="stylesheet" type="text/css" />
    <link href="css/star-rating.min.css" rel="stylesheet" type="text/css" />
    <link href="css/blueimp-gallery.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-tokenfield.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-tokenfield.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-suggest.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.fd.css" rel="stylesheet" type="text/css" />
    <link href="css/tokenfield-typeahead.css" rel="stylesheet" type="text/css" />
    <link href="css/tokenfield-typeahead.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="css/toastr.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br /><br />
    <!--THE NEW NAVBAR-->
        <div class="navbar navbar-default navbar-fixed-top">
                    <!-- Brand and toggle get grouped for better mobile display -->
                    <div class="navbar-header fixed-brand">
                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" id="menu-toggle">
                            <span class="glyphicon glyphicon-th-large" aria-hidden="true"></span>
                        </button>
                        <a class="navbar-brand" href="Home.aspx"><i class="fa fa-sticky-note fa-4"></i>&nbsp;
                    paperhome </a>
                    </div>
                    <!-- navbar-header-->
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                        <!-- This is Activity & Account- pulled right. -->
                        <ul class="nav navbar-nav pull-right">
                            <%--<li>
                        <div class="navbar-form" role="search">
                            <div class="input-group">
                                <input type="text" class="form-control" />
                               <button class="input-group-addon btn btn-default btn-sm" type="submit">
                                    
                                        <i class="glyphicon glyphicon-search"></i>
                                    
                               </button>
                            </div>
                        </div>
                    </li>--%>
                            <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i
                                class="glyphicon glyphicon-bell"></i></a>
                                <ul class="dropdown-menu" role="menu" style="left: -220px;">
                                    <li>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <ul class="nav nav-tabs">
                                                    <li class="active"><a href="#actnotifications" data-toggle="tab">Notifications</a></li>
                                                    <li><a href="#actrequests" data-toggle="tab">Requests</a></li>
                                                </ul>
                                                <div id="myTabContent" class="tab-content">
                                                    <div class="tab-pane fade active in" id="actnotifications">
                                                        <%--<asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true">
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
                                                        </asp:UpdatePanel>--%>
                                                    </div>
                                                    <div class="tab-pane fade" id="actrequests">
                                                        <ul class="list-group" id="contact-list">
                                                            <%--<asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="true" UpdateMode="Always">
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
                                                            </asp:UpdatePanel>--%>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </li>
                                    
                                    <li>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <a href="Activity.aspx" style="text-decoration: none;">
                                                    <center>
                                                        <p>
                                                            <strong>See All</strong>
                                                        </p>
                                                    </center>
                                                </a>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                            <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button"
                                aria-expanded="true">Account <span class="caret"></span></a>
                                <ul class="dropdown-menu" role="menu" style="left: -220px;">
                                    <li>
                                        <div class="navbar-login">
                                            <%--<asp:Repeater ID="accrep" runat="server">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <a href="MyProfile.aspx">
                                                                <img class="img-responsive" src='<%# Eval("dp_url") %>' /></a>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <p class="text-left">
                                                                <a href="MyProfile.aspx" style="text-decoration: none;"><strong>
                                                                    <%# Eval("fname")%>
                                                                    <%# Eval("lname") %></strong></a>
                                                            </p>
                                                            <p class="text-left small">
                                                                <%# Eval("email") %>
                                                            </p>
                                                            <p class="text-left">
                                                                <a href="Settings.aspx" class="btn btn-primary btn-block btn-sm">Settings</a>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>--%>
                                        </div>
                                    </li>
                                    <li class="divider"></li>
                                    <li>
                                        <div class="navbar-login navbar-login-session">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <p>
                                                       <%--  <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-danger btn-block"
                                                                                                OnCommand="logoutsess">Logout</asp:LinkButton>--%>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </li>

                        </ul>
                      
                    </div>
                </div>
        
        <div class="col-sm-6 col-sm-offset-3">
         
            <div class="col-sm-2 grow">
                <h1><a href="Photos.aspx" style="color:#4c4c4c"><i class="fa fa-camera-retro fa-stack-1x "></i></a></h1>
            </div>
            <div class="col-sm-2 grow">
                <h1><a href="Feelpals.aspx" style="color:#4c4c4c"><i class="fa fa-users fa-stack-1x "></i></a></h1>
            </div>
            <div class="col-sm-2 grow">
                <h1><a href="Home.aspx" style="color:#4c4c4c"><i class="fa fa-home fa-stack-1x "></i></a></h1>
            </div>
            <div class="col-sm-2 grow">
                <h1><a href="MyDiary.aspx" style="color:#4c4c4c"><i class="fa fa-book fa-stack-1x "></i></a></h1>
            </div>
            <div class="col-sm-2 grow">
                <h1><a href="Messages.aspx" style="color:#4c4c4c"><i class="fa fa-comments fa-stack-1x "></i></a></h1>
            </div>
            <div class="col-sm-2 grow">
                <h1><a href="Explore.aspx" style="color:#4c4c4c"><i class="fa fa-search fa-stack-1x "></i></a></h1>

            </div>
                </div>
         <br /><br /><br />
   
    </div>
          <center> <!-- WRITE NEW POST MODULE-->
    <br /><br />
    <h1>Around You
    </h1>
    <br />
    <div class="tab-pane active fade in col-sm-12" id="innsbut">
        <a class="fa fa-pencil btn btn-info btn-circle" data-toggle="collapse" data-target="#inns"></a><span class="text-muted">&nbsp;&nbsp;Write about today!</span>
        <br />
        <br />
    </div>
    <div class="collapse col-sm-12" id="inns">
        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
            <ContentTemplate>

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
                      
                        <ajaxToolkit:AsyncFileUpload runat="server"
                            ID="FileUpload1" />
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

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LinkButton5" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>--%>
        <br />
        <br />
    </div>
    <hr /> <div class="col-sm-2 col-sm-offset-5">
        <div class="search-form">
            <div class="form-group has-feedback">
                <label for="search" class="sr-only">
                    Search</label>
                <asp:TextBox type="text" class="form-control inputsea" name="search" ID="seahome"
                    runat="server" AutoPostBack="True" />
                <span class="glyphicon glyphicon-search form-control-feedback"></span>
            </div>
        </div>
    </div> <br /> <br /> <br />
    <hr />
    <br /><br /><br /></center>
    </form>
    
    <script src="js/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="js/sidebar_menu.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="external/google-code-prettify/prettify.js" type="text/javascript"></script>
    <script src="//rawgithub.com/stidges/jquery-searchable/master/dist/jquery.searchable-1.0.0.min.js"
        type="text/javascript"></script>
    <script src="js/star-rating.js" type="text/javascript"></script>
    <script src="js/masonry-docs.min.js" type="text/javascript"></script>
    <script src="js/masonry.js" type="text/javascript"></script>
    <script src="js/masonry.pkgd.js" type="text/javascript"></script>
    <script src="js/masonry.pkgd.min.js" type="text/javascript"></script>
    <script src="js/moment.min.js" type="text/javascript"></script>
    <script src="js/livestamp.js" type="text/javascript"></script>
    <script src="js/bootstrap3-typeahead.min.js" type="text/javascript"></script>
    <script src="js/bootstrap-tokenfield.js" type="text/javascript"></script>
    <script src="js/bootstrap-tokenfield.min.js" type="text/javascript"></script>
    <script src="js/bootstrap-suggest.js" type="text/javascript"></script>
    <script src="js/jquery.blueimp-gallery.min.js" type="text/javascript"></script>
    <script src="js/formValidation.min.js" type="text/javascript"></script>
    <script src="js/formValidation.js" type="text/javascript"></script>
    <script src="js/bootstrap.fd.js" type="text/javascript"></script>
    <script src="js/toastr.js" type="text/javascript"></script>
    <script src="js/jquery.autocorrect.js" type="text/javascript"></script>
    <script src="assets/js/lib/turn.min.js" type="text/javascript"></script>
    <script src="js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.resizeOnApproach.1.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">    $(function () {
        // Remove Search if user Resets Form or hits Escape!
        $('body, .navbar-collapse form[role="search"] button[type="reset"]').on('click keyup', function (event) {
            console.log(event.currentTarget);
            if (event.which == 27 && $('.navbar-collapse form[role="search"]').hasClass('active') ||
				$(event.currentTarget).attr('type') == 'reset') {
                closeSearch();
            }
        });

        function closeSearch() {
            var $form = $('.navbar-collapse form[role="search"].active')
            $form.find('input').val('');
            $form.removeClass('active');
        }

        // Show Search if form is not active // event.preventDefault() is important, this prevents the form from submitting
        $(document).on('click', '.navbar-collapse form[role="search"]:not(.active) button[type="submit"]', function (event) {
            event.preventDefault();
            var $form = $(this).closest('form'),
				$input = $form.find('input');
            $form.addClass('active');
            $input.focus();
        });

        // Autofocus for collapsed mode
        $(document).on('click', '.navbar-header button.navbar-toggle:last-of-type', function (event) {
            var $form = $('.navbar-collapse form[role="search"]').find('input').focus();
        });

        // ONLY FOR DEMO // Please use $('form').submit(function(event)) to track from submission
        // if your form is ajax remember to call `closeSearch()` to close the search container
        $(document).on('click', '.navbar-collapse form[role="search"].active button[type="submit"]', function (event) {
            event.preventDefault();
            var $form = $(this).closest('form'),
				$input = $form.find('input');
            $('#showSearchTerm').text($input.val());
            closeSearch()
        });
    });</script>
</body>
</html>
