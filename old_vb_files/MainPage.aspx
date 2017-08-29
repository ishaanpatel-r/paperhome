<%@ Page Title="" Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false"
    CodeFile="MainPage.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <!-- LOGIN PANEL-->
    <div class="col-sm-3 pull-right">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div id="loginbox" style="margin-top: 0px;" class="mainbox col-sm-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">
                        Sign In</div>
                    <div style="float: right; font-size: 80%; position: relative; top: -10px">
                        <a data-toggle="modal" data-target="#forpass" style="cursor: pointer;">Forgot password?</a></div>
                </div>
                <asp:Panel runat="server" DefaultButton="loginbut">
                    <div style="padding-top: 30px" class="panel-body">
                        <div style="display: none" id="login-alert" class="alert alert-danger col-sm-12">
                        </div>
                        <form id="loginform" class="form-horizontal" role="form">
                        <div style="margin-bottom: 25px" class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <input id="uname_log" runat="server" type="text" class="form-control" name="username"
                                value="" placeholder="username or email" />
                        </div>
                        <div style="margin-bottom: 25px" class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                            <input id="pass_log" runat="server" type="password" class="form-control" name="password"
                                placeholder="password" />
                        </div>
                        <div style="margin-top: 10px" class="form-group">
                            <!-- Button -->
                            <div class="col-sm-12 controls" align="center">
                                <asp:LinkButton ID="loginbut" CssClass="btn btn-success" OnClick="loginsub" runat="server">Login </asp:LinkButton>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12 control">
                                <br />
                                Don't have an account! <a href="RegPage1.aspx">Sign Up Here </a>
                            </div>
                        </div>
                        </form>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="loginbut" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="myTabContent" class="tab-content col-sm-9" style="padding-top:50px;">
       <%-- <br />
        <br />
        <div id="myCarousel" class="carousel slide" data-ride="carousel" style="color: #003040;
            min-height: 500px; margin-left: 30px;">
            <!-- Carousel indicators -->
            <ol class="carousel-indicators" style="background-color: #d9edf7">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
                <li data-target="#myCarousel" data-slide-to="2"></li>
                <li data-target="#myCarousel" data-slide-to="3"></li>
            </ol>
            <!-- Wrapper for carousel items -->
            <div class="carousel-inner">
                <div class="item active">
                    <div class="panel-default" align="center">
                        <div class="jumbotron" style="background-color: #d9edf7" align="center">
                            <div class="col-sm-12">
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <h1 align="center">
                                    <i class="fa fa-sticky-note fa-1x"></i>&nbsp;paperhome
                                </h1>
                                <p class="text-success">
                                    <br />
                                    Meet your new online diary keeper!
                                    <br />
                                    Because, memories are infinite.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="panel-default" align="center">
                        <div class="jumbotron" style="background-color: #d9edf7" align="center">
                            <div class="col-sm-12">
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <h1 align="center">
                                    Socialize like never before!
                                </h1>
                                <p class="text-success">
                                    <br />
                                    Finally an internet hangout that makes life easier.
                                    <br />
                                    Pleasure to find and meet new people.</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="jumbotron" style="background-color: #d9edf7" align="center">
                        <div class="col-sm-12">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <h1 align="center">
                            </h1>
                            <p class="text-success">
                                <br />
                                Friends, co-workers or simply a frolic chat!
                                <br />
                                Sort your endeavours with feelings and know what everyone is upto.</p>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="panel-default" align="center">
                        <div class="jumbotron" style="background-color: #d9edf7" align="center">
                            <div class="col-sm-12">
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <p class="text-success">
                                    Write away your precious moments as you discover them in your day-to-day lives.
                                </p>
                                <p class="text-success">
                                    It's about making memories and keeping 'em safe.
                                </p>
                                <p class="text-success">
                                    Just so you could come back whenever you wish!</p>
                                <h2 align="center">
                                    'Cause it's your home!</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Carousel controls -->
            <a class="carousel-control left" href="#myCarousel" data-slide="prev"></a><a class="carousel-control right"
                href="#myCarousel" data-slide="next"></a>
            <div class="modal active fade" id="forpass" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                            <br />
                        </div>
                        <div class="modal-body col-sm-12">
                            <p class="text-muted">
                                Currently we do not support recovering your account directly due to privacy issues.
                            </p>
                            <p>
                                However, if you like, you can contact the admin with the form displayed below and
                                he'll revert back to you ASAP!
                                <br />
                                Thanks!</p>
                            <hr />
                            <legend>Contact Admin</legend>
                            <div class="form-group col-sm-12">
                                <label for="inputEmail" class="col-sm-2 control-label">
                                    Your Email or Username:</label>
                                <div class="col-sm-8">
                                    <input type="text" class="form-control" id="inputEmail" placeholder="Email">
                                </div>
                            </div>
                            <div class="form-group col-sm-12">
                                <label for="inputPassword" class="col-sm-2 control-label">
                                    Last Remembered Password:</label>
                                <div class="col-sm-8">
                                    <input type="password" class="form-control" id="inputPassword" placeholder="Password">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox">
                                            Exactly this?
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-sm-12">
                                <label for="textArea" class="col-sm-2 control-label">
                                    Additional Message?</label>
                                <div class="col-sm-8">
                                    <textarea class="form-control" rows="3" id="textArea"></textarea>
                                    <span class="help-block">In case you've changed number after you last logged in, please
                                        include your most recent and functioning phone number.</span>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Cancel</button>
                            <button type="button" class="btn btn-primary">
                                Send</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <div style="overflow-y:scroll; max-height:1000px;">
    <!-- Full Width Image Header -->
    <header class="header-image">
        <div class="headline">
            <div class="container">
                  <h1>paperhome</h1>
                <h2>Welcome to the Beta!</h2>
            </div>
        </div>
    </header>

    <!-- Page Content -->
    <div class="container">

        <hr class="featurette-divider">

        <!-- First Featurette -->
        <div class="featurette" id="about">
            <img class="featurette-image img-circle img-responsive pull-right" src="http://placehold.it/500x500">
            <h2 class="featurette-heading">It's your Diary!
                <span class="text-muted">Live your moments forever.</span>
            </h2>
            <p class="lead">Donec ullamcorper nulla non metus auctor fringilla. Vestibulum id ligula porta felis euismod semper. Praesent commodo cursus magna, vel scelerisque nisl consectetur. Fusce dapibus, tellus ac cursus commodo.</p>
        </div>

        <hr class="featurette-divider">

        <!-- Second Featurette -->
        <div class="featurette" id="services">
            <img class="featurette-image img-circle img-responsive pull-left" src="http://placehold.it/500x500">
            <h2 class="featurette-heading">There's a nifty Slambook.
                <span class="text-muted">Ask the whole world & be under your own spotlight.</span>
            </h2>
            <p class="lead">Donec ullamcorper nulla non metus auctor fringilla. Vestibulum id ligula porta felis euismod semper. Praesent commodo cursus magna, vel scelerisque nisl consectetur. Fusce dapibus, tellus ac cursus commodo.</p>
        </div>

        <hr class="featurette-divider">

        <!-- Third Featurette -->
        <div class="featurette" id="contact">
            <img class="featurette-image img-circle img-responsive pull-right" src="http://placehold.it/500x500">
            <h2 class="featurette-heading">And everything else-
                <span class="text-muted">That a social network should be!</span>
            </h2>
            <p class="lead">Donec ullamcorper nulla non metus auctor fringilla. Vestibulum id ligula porta felis euismod semper. Praesent commodo cursus magna, vel scelerisque nisl consectetur. Fusce dapibus, tellus ac cursus commodo.</p>
        </div>

        <hr class="featurette-divider">

        <!-- Footer -->
        <footer>
            <div class="row">
                <div class="col-lg-12">
                    <p>Copyright &copy; Your Website 2014</p>
                </div>
            </div>
        </footer>

    </div>
    <!-- /.container -->
            </div>
    </div>
    <!--CAROUSEL-->
</asp:Content>
