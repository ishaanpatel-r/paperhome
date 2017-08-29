<%@ Page Title="" Language="VB" MasterPageFile="~/Navigation.master" AutoEventWireup="false" CodeFile="editpost.aspx.vb" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:Repeater runat="server" ID="Repeater1">
        <ItemTemplate>
            <div class="item">
                <div class="panel panel-default col-sm-12">
                    <div class="panel-heading" style="background-color: #f9fcfe;">
                        <h3><a id="HyperLink1" href="XsProfile.aspx?profileid=<%# Eval("profile_id") %>"
                            style="text-decoration: none;">
                            <%# Eval("uname")%></a></h3>

                    </div>
                    <div class="panel-image col-sm-12">
                        <div class="col-sm-6">
                            <center style="background-color: black;">
                                <img class="img-responsive enlargewrite col-sm-12" src='<%# Eval("img_att") %>'></center>
                        </div>
                        <div class="col-sm-6">
                            <h2 class="col-sm-12"><a style="text-decoration: none">
                                <%# Eval("date_written", "{0:MMMM d, yyyy}")%></a></h2>
                            <div class="col-sm-12">
                                <br />
                                <div class="form-group col-sm-12">
                                    <label for="select" class="col-sm-3 control-label pull-left">
                                        Mostly Feeling:</label>
                                    <br />
                                    <br />
                                    <input type="text" class="col-sm-2 form-control pull-right acf" id="feel_txt" runat="server" placeholder="You are feeling?" value='<%# Eval("feeling_type")%>' />
                                </div>
                                 <br />
                                <div class="form-group col-sm-12">
                                    <label for="select" class="col-sm-3 control-label pull-left">
                                        Page:</label>
                                    <br />
                                    <br />
                                    <asp:TextBox ID="content_txt" runat="server" CssClass="col-sm-12 form-control" TextMode="MultiLine" Columns="500" Rows="20" Text='<%# Eval("content")%>'></asp:TextBox>
                                </div>
                                 <br />
                                <div class="form-group col-sm-12">
                                    <label for="select" class="col-sm-3 control-label pull-left">
                                        Hashtags:</label>
                                    <br />
                                    <br />
                                   <asp:TextBox ID="hash_txt" runat="server" CssClass="form-control" Text='<%# Eval("hashes")%>'></asp:TextBox>
                                </div>
                            </div>
                           
                    <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="saveeditedpost" CssClass="btn btn-info pull-right col-sm-4"
                        CommandArgument='<%# Eval("writes_id") %>'>Save Edits&nbsp;&nbsp;<i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                    <div class="col-sm-12">
                    <br /><br />
                    </div>
                        </div>
                    </div>
                    <br />
                    <br />

                    <br /><br />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

