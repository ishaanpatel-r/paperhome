<%@ Page Title="" Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false" CodeFile="VerifyAcc.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!--Confirm Deletion-->
    <div id="regpage2" class="tab-pane active">
        <br />
        <br />
        <br />
        <br />
        <div class="panel panel-info col-sm-6 col-sm-offset-3">
            <div class="panel-heading">
                <div class="panel-title">
                    <i class="fa fa-exclamation"></i>&nbsp;&nbsp;Verify Account</div>
            </div>
            <!-- CONFIRM PASSWORD-->
            <br />
            <br />
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="newpassword" class="col-sm-2 control-label">
                        Verification Code<br />(4-digit numeric):</label>
                    <div class="col-sm-10">
                    <br />
                        <input type="text" class="form-control" runat="server" id="wrepw" placeholder="Enter Code Here."
                            />
                        <br />
                    </div>
                </div>
            </div>
            <!--PAGING-->
            <ul class="pagination pagination-lg col-sm-12">
                <li class="pull-left"><asp:LinkButton runat="server" OnClick="resendcode">Resend Code</asp:LinkButton></li>
                <li class="pull-right">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="verifyacc">Verify</asp:LinkButton></li>
            </ul>
        </div>

     
    </div>
</asp:Content>

