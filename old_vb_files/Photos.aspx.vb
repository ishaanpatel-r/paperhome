Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web
Imports System.Net

Partial Class Default3
    Inherits System.Web.UI.Page

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim cmd2 As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Public sea As String 'searchstring
    Shared code As String = String.Empty


    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If
        Session("whichone") = 0
        'ScriptManager.RegisterStartupScript(Me, [GetType](), "text", " GetInstagramPhotos();", True)


        'Dim flag As Integer = Convert.ToInt32(Session("check"))
        'If flag = 1 Then
        'Session("check") = 0
        'Dim client_id = ConfigurationManager.AppSettings("instagram.clientid").ToString()
        'Dim redirect_uri = ConfigurationManager.AppSettings("instagram.redirecturi").ToString()
        'Response.Redirect("https://api.instagram.com/oauth/authorize/?client_id=" + client_id + "&redirect_uri=" + redirect_uri + "&response_type=code")

        'End If
        'If Not [String].IsNullOrEmpty(Request("code")) AndAlso Not Page.IsPostBack Then
        'code = Request("code").ToString()

        'GetDataInstagramToken()
        'End If

        If Page.IsPostBack = False Then

            Session("whichone") = 0
            allclick.CssClass = "list-group-item active"
            youclick.CssClass = "list-group-item"
            tripclick.CssClass = "list-group-item"
            parclick.CssClass = "list-group-item"
            eveclick.CssClass = "list-group-item"
            obclick.CssClass = "list-group-item"
            dpclick.CssClass = "list-group-item"
            Dim mysess As Integer
            mysess = Session("idsess")
            con.Open()
            cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & ") ORDER BY [add_date] DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()

                gallery.DataSource = cmd.ExecuteReader()
                gallery.DataBind()
                gallery.Visible = True
                con.Close()
            Else
                empdiv.Style("Display") = "block"
                gallery.Visible = False
            End If
            con.Close()
        End If




        If Page.IsPostBack = False Then
            Session("whichone") = 0
            allclick.CssClass = "list-group-item active"
            youclick.CssClass = "list-group-item"
            tripclick.CssClass = "list-group-item"
            parclick.CssClass = "list-group-item"
            eveclick.CssClass = "list-group-item"
            obclick.CssClass = "list-group-item"
            dpclick.CssClass = "list-group-item"
            Dim mysess As Integer
            mysess = Session("idsess")
            con.Open()
            cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & ") ORDER BY [add_date] DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()

                gallery.DataSource = cmd.ExecuteReader()
                gallery.DataBind()
                gallery.Visible = True
                con.Close()
            Else
                empdiv.Style("Display") = "block"
                gallery.Visible = False
            End If
            con.Close()
        End If


    End Sub

    'Function used to get instagram user id and access token  

    'Public Sub GetDataInstagramToken()
    '    Dim json = ""
    '    Dim id As String
    '    Dim accessToken As String
    '    Try
    '        Dim parameters As New NameValueCollection()
    '        parameters.Add("client_id", ConfigurationManager.AppSettings("instagram.clientid").ToString())
    '        parameters.Add("client_secret", ConfigurationManager.AppSettings("instagram.clientsecret").ToString())
    '        parameters.Add("grant_type", "authorization_code")
    '        parameters.Add("redirect_uri", ConfigurationManager.AppSettings("instagram.redirecturi").ToString())
    '        parameters.Add("code", code)

    '        Dim client As New WebClient()
    '        Dim result = client.UploadValues("https://api.instagram.com/oauth/access_token", "POST", parameters)
    '        Dim response = System.Text.Encoding.[Default].GetString(result)

    '        ' deserializing nested JSON string to object  
    '        Dim jsResult = DirectCast(JsonConvert.DeserializeObject(response), JObject)
    '        accessToken = DirectCast(jsResult("access_token").ToString(), String)
    '        id = CStr(jsResult("user")("id"))

    '        'This code register id and access token to get on client side  
    '        Page.ClientScript.RegisterStartupScript(Me.[GetType](), "GetToken", "<script>var instagramaccessid=""" + "" + id + "" + """; var instagramaccesstoken=""" + "" + accessToken + "" + """;</script>")
    '    Catch ex As Exception

    '        Throw
    '    End Try

    'End Sub

    Sub makealbumsession(ByVal Sender As Object, ByVal e As CommandEventArgs)

        Session("whichone") = 0
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("alnum") = value1
        Dim m As String
        m = "You are uploading photos in " & Session("alnum") & "."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m.ToString() & "'); openup();", True)

    End Sub

    Sub delpho(ByVal Sender As Object, ByVal e As CommandEventArgs)

        Dim m As String
        m = "Photo deleted."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)


        Dim phodelid As Integer = Convert.ToInt32(e.CommandArgument)
        con.Open()
        cmd = New sqlcommand("DELETE FROM [picture_library] WHERE [photo_id] = " & phodelid & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        If Page.IsPostBack = True Then
            Session("whichone") = 0
            allclick.CssClass = "list-group-item active"
            youclick.CssClass = "list-group-item"
            tripclick.CssClass = "list-group-item"
            parclick.CssClass = "list-group-item"
            eveclick.CssClass = "list-group-item"
            obclick.CssClass = "list-group-item"
            dpclick.CssClass = "list-group-item"
            Dim mysess As Integer
            mysess = Session("idsess")
            con.Open()
            cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & ") ORDER BY [add_date] DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()

                gallery.DataSource = cmd.ExecuteReader()
                gallery.DataBind()
                gallery.Visible = True
                con.Close()
            Else
                empdiv.Style("Display") = "block"
                gallery.Visible = False
            End If
            con.Close()
        End If

    End Sub

    Protected Sub makeobp(ByVal Sender As Object, ByVal e As CommandEventArgs)

        Dim m As String
        m = "Openbook cover changed."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

        Dim mysess As Integer = Convert.ToInt32(Session("idsess"))
        Dim phoobpid As Integer = Convert.ToInt32(e.CommandArgument)
        Dim newobname As String = " "

        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([photo_id] = " & phoobpid & ")", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            newobname = dr("pic_name")
            dr.Close()
        End If

        Dim newobpurl As String = "img/" & mysess & "/coverphotos/" & newobname

        con.Close()
        con.Open()
        cmd = New SqlCommand("UPDATE [userinfo] SET [cover_url] = '" & newobpurl & "' WHERE [profile_id] = " & mysess & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

    End Sub

    Protected Sub makedp(ByVal Sender As Object, ByVal e As CommandEventArgs)

        Dim m As String
        m = "Display picture changed."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

        Dim mysess As Integer = Convert.ToInt32(Session("idsess"))
        Dim phodppid As Integer = Convert.ToInt32(e.CommandArgument)
        Dim newobname As String = " "

        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([photo_id] = " & phodppid & ")", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            newobname = dr("pic_name")
            dr.Close()
        End If

        Dim newdpurl As String = "img/" & mysess & "/displaypictures/" & newobname

        con.Close()
        con.Open()
        cmd = New SqlCommand("UPDATE [userinfo] SET [dp_url] = '" & newdpurl & "' WHERE [profile_id] = " & mysess & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

    End Sub


    'Protected Sub txtSearch_KeyUp(ByVal Sender As Object, ByVal e As EventArgs)

    '    sea = seareq.Text
    '    Dim mysess As Integer
    '    mysess = Convert.ToInt32(Session("idsess"))
    '    If sea.Length > 0 Then
    '        con.Open()
    '        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & " AND [caption] LIKE '%' + @seat + '%') ORDER BY [add_date] DESC", con)
    '        cmd.Parameters.Add("@seat", SqlDbType.NVarChar)
    '        cmd.Parameters("@seat").Value = seareq.Text
    '        cmd.Connection = con
    '        dr = cmd.ExecuteReader
    '        If (dr.Read) Then
    '            con.Close()
    '            con.Open()

    '            gallery.Visible = True
    '            gallery.DataSource = cmd.ExecuteReader()
    '            gallery.DataBind()
    '        Else
    '            gallery.Visible = False

    '        End If
    '        con.Close()
    '    Else

    '        allclick.CssClass = "list-group-item active"
    '        youclick.CssClass = "list-group-item"
    '        tripclick.CssClass = "list-group-item"
    '        parclick.CssClass = "list-group-item"
    '        eveclick.CssClass = "list-group-item"
    '        obclick.CssClass = "list-group-item"
    '        dpclick.CssClass = "list-group-item"
    '        con.Open()
    '        cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & ") ORDER BY [add_date] DESC", con)
    '        dr = cmd.ExecuteReader
    '        If dr.Read Then
    '            con.Close()
    '            con.Open()

    '            gallery.DataSource = cmd.ExecuteReader()
    '            gallery.DataBind()
    '            gallery.Visible = True
    '            con.Close()
    '        Else
    '            empdiv.Style("Display") = "block"
    '            gallery.Visible = False
    '        End If
    '        con.Close()

    '    End If

    'End Sub

    Protected Sub allpic(ByVal Sender As Object, ByVal e As EventArgs)
        Session("whichone") = 0

        normalfetch.Visible = True
        instafetch.Visible = False
        allclick.CssClass = "list-group-item active"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim mysess As Integer
        mysess = Session("idsess")
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & ") ORDER BY [add_date] DESC", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            con.Close()
            con.Open()
            gallery.DataSource = cmd.ExecuteReader()
            gallery.DataBind()
            empdiv.Style("Display") = "none"
            gallery.Visible = True
            con.Close()
        Else
            empdiv.Style("Display") = "block"
            gallery.Visible = False
        End If


    End Sub

    Protected Sub youpic(ByVal Sender As Object, ByVal e As EventArgs)
        Session("whichone") = 0
        normalfetch.Visible = True
        instafetch.Visible = False
        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item active"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim mysess As Integer
        mysess = Session("idsess")
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & " AND [folder_type_path]='you') ORDER BY [add_date] DESC", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            con.Close()
            con.Open()
            gallery.DataSource = cmd.ExecuteReader()
            gallery.DataBind()
            empdiv.Style("Display") = "none"
            gallery.Visible = True
            con.Close()
        Else
            empdiv.Style("Display") = "block"
            gallery.Visible = False
        End If
    End Sub

    Protected Sub trippic(ByVal Sender As Object, ByVal e As EventArgs)
        Session("whichone") = 0
        normalfetch.Visible = True
        instafetch.Visible = False
        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item active"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim mysess As Integer
        mysess = Session("idsess")
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & " AND [folder_type_path]='trip') ORDER BY [add_date] DESC", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            con.Close()
            con.Open()
            gallery.DataSource = cmd.ExecuteReader()
            gallery.DataBind()
            empdiv.Style("Display") = "none"
            gallery.Visible = True
            con.Close()
        Else
            empdiv.Style("Display") = "block"
            gallery.Visible = False
        End If
    End Sub

    Protected Sub parpic(ByVal Sender As Object, ByVal e As EventArgs)
        Session("whichone") = 0
        normalfetch.Visible = True
        instafetch.Visible = False
        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item  active"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim mysess As Integer
        mysess = Session("idsess")
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & " AND [folder_type_path]='parties') ORDER BY [add_date] DESC", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            con.Close()
            con.Open()
            gallery.DataSource = cmd.ExecuteReader()
            gallery.DataBind()
            empdiv.Style("Display") = "none"
            gallery.Visible = True
            con.Close()
        Else
            empdiv.Style("Display") = "block"
            gallery.Visible = False
        End If
    End Sub

    Protected Sub evepic(ByVal Sender As Object, ByVal e As EventArgs)
        Session("whichone") = 0
        normalfetch.Visible = True
        instafetch.Visible = False

        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item active"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim mysess As Integer
        mysess = Session("idsess")
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & " AND [folder_type_path]='events') ORDER BY [add_date] DESC", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            con.Close()
            con.Open()
            gallery.DataSource = cmd.ExecuteReader()
            gallery.DataBind()
            empdiv.Style("Display") = "none"
            gallery.Visible = True
            con.Close()
        Else
            empdiv.Style("Display") = "block"
            gallery.Visible = False
        End If
    End Sub

    Protected Sub obpic(ByVal Sender As Object, ByVal e As EventArgs)
        Session("whichone") = 1

        normalfetch.Visible = True
        instafetch.Visible = False

        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item active"
        dpclick.CssClass = "list-group-item"

        Dim mysess As Integer
        mysess = Session("idsess")
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & " AND [folder_type_path]='coverphotos') ORDER BY [add_date] DESC", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            con.Close()
            con.Open()
            gallery.DataSource = cmd.ExecuteReader()
            gallery.DataBind()
            empdiv.Style("Display") = "none"
            gallery.Visible = True
            con.Close()
        Else
            empdiv.Style("Display") = "block"
            gallery.Visible = False
        End If
    End Sub

    Protected Sub dppic(ByVal Sender As Object, ByVal e As EventArgs)
        Session("whichone") = 2
        normalfetch.Visible = True
        instafetch.Visible = False

        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item active"

        Dim mysess As Integer
        mysess = Session("idsess")
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & mysess & " AND [folder_type_path]='displaypictures') ORDER BY [add_date] DESC", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            con.Close()
            con.Open()
            gallery.DataSource = cmd.ExecuteReader()
            gallery.DataBind()
            empdiv.Style("Display") = "none"
            gallery.Visible = True
            con.Close()
        Else
            empdiv.Style("Display") = "block"
            gallery.Visible = False
        End If
    End Sub

    'Protected Sub instpic(ByVal Sender As Object, ByVal e As EventArgs)
    '    allclick.CssClass = "list-group-item"
    '    youclick.CssClass = "list-group-item"
    '    tripclick.CssClass = "list-group-item"
    '    parclick.CssClass = "list-group-item"
    '    eveclick.CssClass = "list-group-item"
    '    instclick.CssClass = "list-group-item active"
    '    normalfetch.Visible = False
    '    instafetch.Visible = True

    'End Sub


    Protected Sub gallery_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles gallery.ItemDataBound
        Dim lb2 As LinkButton = TryCast(e.Item.FindControl("delphoclick"), LinkButton)
        ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(lb2)
    End Sub
End Class
