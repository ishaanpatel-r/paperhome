<%@ Page Title="" Language="VB" MasterPageFile="~/Main.master" AutoEventWireup="false"
    CodeFile="DelAcc.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Confirm Deletion-->
    <div id="regpage2" class="tab-pane active">
        <br />
        <br />
        <br />
        <br />
        <div class="panel panel-danger col-sm-6 col-sm-offset-3">
            <div class="panel-heading">
                <div class="panel-title">
                    Delete Account</div>
            </div>
            <!-- CONFIRM PASSWORD-->
            <br />
            <br />
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="newpassword" class="col-sm-2 control-label">
                        Password:</label>
                    <div class="col-sm-10">
                        <input type="password" class="form-control" runat="server" id="wrepw" placeholder="Re-enter password to authenticate deletion."
                            />
                        <br />
                    </div>
                </div>
            </div>
            <!--PAGING-->
            <ul class="pagination pagination-lg col-sm-12">
                <li class="pull-left"><a href="Home.aspx">Cancel</a></li>
                <li class="pull-right">
                    <asp:LinkButton runat="server" OnClick="delacc">Delete</asp:LinkButton></li>
            </ul>
        </div>

     
    </div>
</asp:Content>
