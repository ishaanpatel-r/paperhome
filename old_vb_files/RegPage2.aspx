<%@ Page Title="" Language="VB" MasterPageFile="~/Reg.master" AutoEventWireup="false"
    CodeFile="RegPage2.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <form data-toggle="validator" runat="server" role="form">
    <div class="panel panel-info col-sm-6 col-sm-offset-3">
        <div class="panel-heading">
            <div class="panel-title">
                New Identity! Yay!</div>
        </div>
        <!-- USERNAME, PASSWORD, CONFIRM PASSWORD-->
        <div class="form-group has-feedback">
            <br />
            <br />
            <label for="inputTwitter" class="control-label col-sm-2">
                <br />
                Username:</label>
            <div class="input-group">
                <span class="input-group-addon">@</span>
                <input type="text" pattern="^[_A-z0-9]{1,}$" runat="server" maxlength="15" minlength="6"
                    class="form-control" id="inputTwitter" placeholder="1000hz" required="true" />
            </div>
            <span class="glyphicon form-control-feedback" aria-hidden="true"></span><span class="help-block with-errors  col-sm-offset-2">
                Wait for the green!</span>
        </div>
        <div class="form-group">
            <br />
            <label class=" col-sm-2 control-label">
                Password:</label>
            <div class="form-group col-sm-10">
                <input type="password" runat="server" data-minlength="6" class="form-control" id="inputPassword"
                    placeholder="Minimum of 6 characters" required="true" />
            </div>
            <label class=" col-sm-2 control-label">
                Confirm Password:</label>
            <div class="form-group col-sm-10">
                <input type="password" runat="server" class="form-control" id="inputPasswordConfirm"
                    data-match="#ctl00_ContentPlaceHolder1_inputPassword" data-match-error="Whoops, these don't match." placeholder="Re-Enter Password" required="true" />
                <div class="help-block with-errors">
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
        <div class="form-group has-feedback">
            <label for="inputTwitter" class="control-label col-sm-2">
                <br />
                Cell Phone:</label>
            <div class="input-group">
                <span class="input-group-addon">+91</span>
                <input type="text" pattern="(7|8|9)\d{9}" runat="server" maxlength="10" minlength="10"
                    class="form-control" id="cellnum" placeholder="Your 10-digit number here." required="true" />
            </div>
            <span class="glyphicon form-control-feedback" aria-hidden="true"></span><span class="help-block with-errors  col-sm-offset-2">
                Enter your correct number!
                <br />
                This is for your safety.</span>
        </div>
        <!--PAGING-->
        <ul class="pagination pagination-sm col-sm-12">
            <li class="pull-left"><a data-toggle="modal" data-target="#cancelsignup">Cancel</a></li>
            </li>
            <li class="pull-right">
                <asp:LinkButton ID="A2" OnClick="submit_but" runat="server">Submit »</asp:LinkButton></li>
        </ul>
        <div class="progress progress-striped active">
            <div class="progress-bar" style="width: 67%">
            </div>
        </div>
    </div>
    </form>
</asp:Content>
