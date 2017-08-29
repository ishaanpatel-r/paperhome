<%@ Page Title="" Language="VB" MasterPageFile="~/Reg.master" AutoEventWireup="false"
    CodeFile="RegPage1.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <form data-toggle="validator" runat="server" role="form">
    <div id="regpage1" class="tab-pane fade active in">
        <div class="panel panel-info col-sm-6 col-sm-offset-3">
            <div class="panel-heading">
                <div class="panel-title">
                    Some Basic Info?</div>
            </div>
            <!--FIRST NAME & LAST NAME-->
            <div class="form-group col-sm-12">
                <br>
                <label for="inputName" class="control-label col-sm-2">
                    First Name</label>
                <input type="text" pattern="[a-zA-Z]+" runat="server" class="form-control col-sm-10"
                    data-error="Please enter a valid first name." maxlength="15" minlength="2" id="fname"
                    placeholder="John" required />
                <div class="help-block with-errors">
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label for="inputName" class="control-label col-sm-2">
                    Last Name</label>
                <input type="text" pattern="[a-zA-Z]+" runat="server" class="form-control col-sm-10"
                    data-error="Please enter a valid first name." id="lname" maxlength="15" minlength="2"
                    placeholder="Smith" required />
                <div class="help-block with-errors">
                </div>
            </div>
            <!--DOB-->
            <div class="form-group col-sm-12">
                <label class="col-sm-12 control-label">
                    Date of Birth</label>
                <div class="input-group date" id="datetimepicker1">
                    <input id="dobtxt" class="form-control" runat="server" data-format="dd/MM/yyyy" type="text" />
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>
            <!--EMAIL-->
            <div class="form-group col-sm-12">
                <label for="inputEmail" class="control-label col-sm-2">
                    Email</label>
                <input type="text" runat="server" class="form-control" id="emailtxt" pattern="^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$"
                    placeholder="Email" data-error="Bruh, that email address is invalid" required />
                <div class="help-block with-errors">
                </div>
            </div>
            <!--SEX-->
            <div class="form-group col-sm-12">
                <label class="col-sm-2 control-label">
                    Sex:</label>
                <div class="col-sm-10">
                    <asp:RadioButtonList ID="Radiobuttonlist3" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="male">&nbsp;&nbsp;Male&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="female">&nbsp;&nbsp;Female</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <!-- PLACE-->
            <div class="form-group col-sm-12">
                <label class="col-sm-2 control-label ">
                    City & Country:</label>
                <div class="col-sm-10">
                    <div class="form-group col-sm-5">
                        <label class="sr-only" for="city">
                            City</label>
                        <input type="text" runat="server" class="form-control col-sm-10" pattern="[a-zA-Z]+"
                            data-error="Enter a valid city." id="citxt" maxlength="15" minlength="2" placeholder="Mumbai"
                            required />
                        <div class="help-block with-errors">
                        </div>
                        &nbsp
                    </div>
                    <div class="form-group col-sm-5">
                        <label class="sr-only" for="country">
                            Country</label>
                        <input type="text" runat="server" class="form-control col-sm-10" pattern="[a-zA-Z]+"
                            data-error="Enter a valid country." id="cotxt" maxlength="15" minlength="2" placeholder="India"
                            required />
                        <div class="help-block with-errors">
                        </div>
                    </div>
                </div>
            </div>
            <!--PAGING-->
            <div class="form-group col-sm-12">
                <ul class="pagination pagination-sm col-sm-12">
                    <li class="pull-left"><a data-toggle="modal" data-target="#cancelsignup">Cancel</a></li>
                    <li class="pull-right">
                        <asp:LinkButton runat="server" OnClick="sub1">Step 2 »</asp:LinkButton></li>
                </ul>
                <div class="progress progress-striped active">
                    <div class="progress-bar" style="width: 33%">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="cancelsignup">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                        ×</button>
                                    <br />
                                </div>
                                <div class="modal-body">
                                    <p>
                                        It'll only take a moment.
                                        <br />
                                        Sure you want to cancel the sign up?</p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Cancel</button>
                                    <a href="MainPage.aspx" type="button" class="btn btn-primary">Yes</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    
    </form>
</asp:Content>
