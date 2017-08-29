Imports System.Data
Imports System.Data.SqlClient

Partial Class Default2
    Inherits System.Web.UI.Page

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        Session("postid") = Request.QueryString("id")
        Dim poid As Integer

        Try 'try catch to catch any unregistered profileids in url
            poid = Convert.ToInt32(Session("postid"))
            If poid = 0 Then
                Response.Redirect("Home.aspx")
            Else
                Dim psessinit As Integer = Convert.ToInt32(Session("postid"))
                con.Open()
                cmd = New SqlCommand("SELECT * FROM writes WHERE [writes_id]= " & psessinit & "", con)
                cmd.Connection = con
                dr = cmd.ExecuteReader
                If (dr.Read) Then
                    dr.Close()
                    poid = psessinit
                Else
                    dr.Close()
                    Response.Redirect("Home.aspx")
                End If
                con.Close()
            End If
        Catch
            Response.Redirect("Home.aspx")
        End Try

        If Page.IsPostBack = False Then
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [feelpals_sys] on [writes].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([writes_id] = " & poid & ")", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                Repeater1.DataSource = cmd.ExecuteReader()
                dr.Close()
                Repeater1.DataBind()
            Else
                Response.Redirect("Home.aspx")
                con.Close()
            End If
        End If
    End Sub

    Protected Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        Dim subRepeter As Repeater = DirectCast(e.Item.FindControl("Repeater2"), Repeater)
        Dim writesid As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "writes_id"))
        Dim con As sqlConnection
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        Dim cmd As sqlCommand
        Dim dr As sqlDataReader
        con.Open() 'load  notif
        cmd = New SqlCommand("SELECT [userinfo].[uname], [comments].[content_c],[comments].[comment_id],[comments].[date_written_c],[userinfo].[dp_url], [userinfo].[profile_id] FROM ([comments] LEFT JOIN [userinfo] ON [comments].[poster] = [userinfo].[profile_id]) WHERE [writes_id_c]= " & writesid & " ORDER BY [date_written_c] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            dr.Close()
            con.Close()
            con.Open()
            subRepeter.DataSource = cmd.ExecuteReader()
            subRepeter.DataBind()
        Else
            dr.Close()
        End If
        con.Close()
    End Sub

    Protected Sub delcom(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim m As String
        m = "Comment was deleted."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

        Dim wrisess As Integer = 0
        Dim commandArgsAccept As Integer = Convert.ToInt32(e.CommandArgument.ToString())

        con.Open() 'reload  comments
        cmd = New SqlCommand("SELECT [writes_id_c] FROM [comments] WHERE [comment_id] = " & commandArgsAccept & "", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            wrisess = dr("writes_id_c")
            dr.Close()
        End If

        con.Close()
        con.Open()
        cmd = New SqlCommand("DELETE FROM [comments] WHERE [comment_id] = " & commandArgsAccept & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        For Each rItem As RepeaterItem In Repeater1.Items
            Dim subRepeter As Repeater = DirectCast(rItem.FindControl("Repeater2"), Repeater)
            con.Open() 'reload  comments
            cmd = New SqlCommand("SELECT [userinfo].[uname], [comments].[content_c],[comments].[date_written_c],[comments].[comment_id],[userinfo].[dp_url], [userinfo].[profile_id] FROM ([comments] LEFT JOIN [userinfo] ON [comments].[poster] = [userinfo].[profile_id]) WHERE [writes_id_c]= " & wrisess & " ORDER BY [date_written_c] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                dr.Close()
                con.Close()
                con.Open()
                subRepeter.DataSource = cmd.ExecuteReader()
                subRepeter.DataBind()
            Else
                dr.Close()
            End If
            con.Close()
        Next
    End Sub

    Protected Sub like_post(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim writeid As String = commandArgsAccept.ToString
        Session("tempsess") = writeid
        Dim wrisess As String
        wrisess = Convert.ToInt32(Session("tempsess"))
        Dim mysess As String
        mysess = Convert.ToString(Session("idsess"))
        Dim intDay As Date
        intDay = Date.Now

        con.Open()
        cmd = New sqlCommand("SELECT * FROM activity WHERE ([doer]=" & mysess & " AND [type]='liked a' AND [post]=" & wrisess & ")", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Dim m As String
            m = "You\'ve already liked this post."
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            dr.Close()
        Else
            dr.Close()
            cmd = New SqlCommand("INSERT INTO activity(doer, type, post, date_time_written) VALUES('" & mysess & "','liked a', " & wrisess & ", '" & intDay & "')", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            Dim m As String
            m = "Liked! <3"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
            con.Close()
        End If
    End Sub

    Protected Sub comment_post(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim wrisess As Integer
        wrisess = e.CommandArgument
        Dim tagexists As Integer = 0
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim intDay As Date
        intDay = Date.Now
        con.Open()
        Dim txtBox As HtmlInputText
        Dim messtxta As String = " "
        For Each rItem As RepeaterItem In Repeater1.Items
            txtBox = DirectCast(rItem.FindControl("Text1"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxta = txtBox.Value
                    For Each m As Match In Regex.Matches(messtxta, "@\w+")
                        cmd = New SqlCommand("SELECT [profile_id] FROM [userinfo] WHERE [uname] = @unametag", con)
                        cmd.Parameters.Add("@unametag", SqlDbType.NVarChar)
                        cmd.Parameters("@unametag").Value = m.Value.Trim("@").Replace("'", "''")
                        cmd.Connection = con
                        dr = cmd.ExecuteReader
                        If (dr.Read) Then
                            tagexists = 1
                            Dim idc As Integer = dr("profile_id")
                            con.Close()
                            con.Open()
                            'insert activity with tag for each tag
                            cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, doee, post) VALUES('" & mysess & "', 'tagged', '" & intDay & "', " & idc & ", " & wrisess & ") ", con)
                            cmd.Connection = con
                            cmd.ExecuteNonQuery()
                        End If

                    Next
                    If tagexists = 1 Then
                        tagexists = 0
                        GoTo tagcominput
                    Else
                        GoTo cominput
                    End If
cominput:           'insert comment & activity if tag not found
                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'commented on a', '" & intDay & "', '" & wrisess & "') ", con)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("INSERT INTO comments(poster, content_c, date_written_c, writes_id_c) VALUES('" & mysess & "',@comcon, '" & intDay & "', '" & wrisess & "')", con)
                    cmd.Parameters.Add("@comcon", SqlDbType.NVarChar)
                    cmd.Parameters("@comcon").Value = messtxta.Replace("'", "''")
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    txtBox.Value = ""
                    con.Close()

                    GoTo a
                End If
            End If

        Next
tagcominput:  'inserts comment if tag found
        cmd = New SqlCommand("INSERT INTO comments(poster, content_c, date_written_c, Writes_id_c) VALUES('" & mysess & "',@comcon, '" & intDay & "', '" & wrisess & "')", con)
        cmd.Parameters.Add("@comcon", SqlDbType.NVarChar)
        cmd.Parameters("@comcon").Value = messtxta.Replace("'", "''")
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        txtBox.Value = ""
a:      'reloads the comments
        For Each rItem As RepeaterItem In Repeater1.Items
            Dim subRepeter As Repeater = DirectCast(rItem.FindControl("Repeater2"), Repeater)
            con.Open()
            cmd = New SqlCommand("SELECT [comments].[comment_id],[userinfo].[uname], [comments].[content_c],[comments].[date_written_c],[userinfo].[dp_url], [userinfo].[profile_id] FROM ([comments] LEFT JOIN [userinfo] ON [comments].[poster] = [userinfo].[profile_id]) WHERE [writes_id_c]= " & wrisess & " ORDER BY [date_written_c] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                dr.Close()
                con.Close()
                con.Open()
                subRepeter.DataSource = cmd.ExecuteReader()
                subRepeter.DataBind()
            Else
                dr.Close()
            End If
            con.Close()
        Next

    End Sub

End Class
