Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web

Public Class Default3
    Inherits System.Web.UI.Page


    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Public sea As String 'searchstring

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If



        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        If Page.IsPostBack = False Then
            con.Open() 'load  liked
            cmd = New SqlCommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id]=[activity].[post]) WHERE ([doer]= " & mysess & " AND [type]= 'liked a') ORDER BY [date_time_written] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                liked_writes.DataSource = cmd.ExecuteReader()
                liked_writes.DataBind()
            End If
            con.Close()
        End If

        If Page.IsPostBack = False Then
            con.Open() 'load  req
            cmd = New SqlCommand("SELECT [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE ([accepted]= 0 AND [to]= " & mysess & " )", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                repreq.DataSource = cmd.ExecuteReader()
                repreq.DataBind()

            End If
            con.Close()

        End If

        If Page.IsPostBack = False Then
            con.Open() 'load  notif
            cmd = New SqlCommand("SELECT [userinfo].[profile_id],[type], [uname], [date_time_written], [post], [doer], [doee] FROM (([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) LEFT JOIN [writes] ON [writes].[writes_id]=[activity].[post]) WHERE (([doee] = " & mysess & " OR ([doee] IS NULL AND [writes].[profile_id] = " & mysess & ")) AND [doer] <> " & mysess & ") ORDER BY [date_time_written] DESC", con) 'this query is wrong (THE OR IS USED INCORRECTLY)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                notifrepeater.DataSource = cmd.ExecuteReader()
                notifrepeater.DataBind()
            End If
            con.Close()
        End If


    End Sub

    Protected Sub txtSearch_KeyUp_log(ByVal Sender As Object, ByVal e As EventArgs)

        sea = sealog.Text
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        'cmd = New sqlcommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id]=[activity].[post]) WHERE ([doer]= " & mysess & " AND [type]= 'liked a' AND (([uname] LIKE '%" & sea & "%') OR ([fname] LIKE '%" & sea & "%')  OR ([content] LIKE '%" & sea & "%') OR ([lname] LIKE '%" & sea & "%') OR ([hashes] LIKE '%" & sea & "%') OR ([feeling_type] LIKE '%" & sea & "%'))) ORDER BY [date_time_written] DESC", con)
        cmd = New SqlCommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id]=[activity].[post]) WHERE ([doer]= " & mysess & " AND [type]= 'liked a' AND (([uname] LIKE '%' + @sea + '%') OR ([fname] LIKE '%' + @sea + '%')  OR ([content] LIKE '%' + @sea + '%') OR ([lname] LIKE '%' + @sea + '%') OR ([hashes] LIKE '%' + @sea + '%') OR ([feeling_type] LIKE '%' + @sea + '%'))) ORDER BY [date_time_written] DESC", con)
        cmd.Parameters.Add("@sea", SqlDbType.NVarChar)
        cmd.Parameters("@sea").Value = sealog.Text
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()

            liked_writes.Visible = True
            liked_writes.DataSource = cmd.ExecuteReader()
            liked_writes.DataBind()
        Else
            liked_writes.Visible = False

        End If
        con.Close()
    End Sub

    Protected Sub txtSearch_KeyUp_req(ByVal Sender As Object, ByVal e As EventArgs)
        Dim sear As String
        sear = seareq.Text
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        'cmd = New sqlcommand("SELECT [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE (([accepted]= False) AND ([to]= " & mysess & ") AND (([fname] LIKE '%" & sear & "%') OR ([lname] LIKE '%" & sear & "%') OR ([uname] LIKE '%" & sear & "%'))) ORDER BY [date_time] DESC", con)
        cmd = New SqlCommand("SELECT [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE (([accepted]= 0) AND ([to]= " & mysess & ") AND (([fname] LIKE '%' + @sear + '%') OR ([lname] LIKE '%' + @sear + '%') OR ([uname] LIKE '%' + @sear + '%'))) ORDER BY [date_time] DESC", con)
        cmd.Parameters.Add("@sear", SqlDbType.NVarChar)
        cmd.Parameters("@sear").Value = seareq.Text
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()

            repreq.Visible = True
            repreq.DataSource = cmd.ExecuteReader()
            repreq.DataBind()
        Else
            repreq.Visible = False

        End If
        con.Close()
    End Sub

    Protected Sub txtSearch_KeyUp_not(ByVal Sender As Object, ByVal e As EventArgs)
        Dim sean As String
        sean = seanot.Text
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open() 'load  notif

        'cmd = New sqlcommand("SELECT TOP 10 [userinfo].[profile_id],[type], [uname], [date_time_written], [post] FROM (([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) LEFT JOIN [writes] ON [writes].[writes_id]=[activity].[post]) WHERE ((([doee] = " & mysess & ") OR ([writes].[profile_id] = " & mysess & ")) AND [doer] <> " & mysess & " AND (([fname] LIKE '%" & sean & "%') OR ([lname] LIKE '%" & sean & "%') OR ([uname] LIKE '%" & sean & "%'))) ORDER BY [date_time_written] DESC", con)
        cmd = New SqlCommand("SELECT TOP 10 [userinfo].[profile_id],[type], [uname], [date_time_written], [post], [doer], [doee] FROM (([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) LEFT JOIN [writes] ON [writes].[writes_id]=[activity].[post]) WHERE ((([doee] = " & mysess & ") OR ([writes].[profile_id] = " & mysess & ")) AND [doer] <> " & mysess & " AND (([fname] LIKE '%' + @sean + '%') OR ([lname] LIKE '%' + @sean + '%') OR ([uname] LIKE '%' + @sean + '%'))) ORDER BY [date_time_written] DESC", con)
        cmd.Parameters.Add("@sean", SqlDbType.NVarChar)
        cmd.Parameters("@sean").Value = seanot.Text
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            notifrepeater.Visible = True
            notifrepeater.DataSource = cmd.ExecuteReader()
            notifrepeater.DataBind()
        Else
            notifrepeater.Visible = False
        End If
        con.Close()
    End Sub

    Protected Sub unlike_post(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim m As String
        m = "The post was removed from your favourites."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim writeid As Integer = commandArgsAccept
        Session("tempsess") = writeid
        Dim wrisess As Integer
        wrisess = Convert.ToInt32(Session("tempsess"))
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("DELETE FROM activity WHERE [doer] = " & mysess & " AND [type] = 'liked a' AND [post] = " & wrisess & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()


        con.Open() 'load  liked
        cmd = New SqlCommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id]=[activity].[post]) WHERE ([doer]= " & mysess & " AND [type]= 'liked a') ORDER BY [date_time_written] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            liked_writes.Visible = True
            liked_writes.DataSource = cmd.ExecuteReader()
            liked_writes.DataBind()
        Else
            liked_writes.Visible = False
        End If
        con.Close()

    End Sub

    Protected Sub accept(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim m As String
        m = "Added to Feelpals."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim hisid As Integer = commandArgsAccept
        Session("tempsess") = hisid
        Dim hissess As Integer
        hissess = Convert.ToInt32(Session("tempsess"))
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim intDay As Date
        intDay = Date.Now
        con.Open()
        cmd = New SqlCommand("UPDATE req SET [accepted]= 1 WHERE ([from] = " & hissess & " AND [to] = " & mysess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        cmd = New SqlCommand("INSERT INTO feelpals_sys(group_to_following, group_to_followers) VALUES(" & hissess & ", " & mysess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        'activity input'
        con.Open()
        cmd = New SqlCommand("INSERT INTO activity(doer, type, doee, date_time_written) VALUES(" & hissess & ", 'started following', " & mysess & ", '" & intDay & "') ", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        con.Open() 'load  req
        cmd = New SqlCommand("SELECT [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE ([accepted]= 0 AND [to]= " & mysess & " )", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            repreq.Visible = True
            repreq.DataSource = cmd.ExecuteReader()
            repreq.DataBind()
        Else
            repreq.Visible = False

        End If
        con.Close()
    End Sub

    Protected Sub decline(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim m As String
        m = "Request declined."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim hisid As Integer = commandArgsAccept
        Session("tempsess") = hisid
        Dim hissess As Integer
        hissess = Convert.ToInt32(Session("tempsess"))
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("DELETE FROM req WHERE ([from] = " & hissess & " AND [to] = " & mysess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        con.Open() 'load  req
        cmd = New SqlCommand("SELECT [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE ([accepted]= False AND [to]= " & mysess & " )", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            repreq.Visible = True
            repreq.DataSource = cmd.ExecuteReader()
            repreq.DataBind()
        Else
            repreq.Visible = False

        End If
        con.Close()
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
        For Each rItem As RepeaterItem In liked_writes.Items()
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
        For Each rItem As RepeaterItem In liked_writes.Items()
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

        For Each rItem As RepeaterItem In liked_writes.Items
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

    Protected Sub showc(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim writeid As String = commandArgsAccept.ToString
        Session("tempsess2") = writeid
    End Sub

    Sub red(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Protected Sub liked_writes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles liked_writes.ItemDataBound
        Dim subRepeter As Repeater = DirectCast(e.Item.FindControl("Repeater2"), Repeater)
        Dim writesid As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "writes_id"))
        Dim con As SqlConnection
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader
        con.Open() 'load  notif
        cmd = New SqlCommand("SELECT [comments].[comment_id],[userinfo].[uname], [comments].[content_c],[comments].[date_written_c],[userinfo].[dp_url], [userinfo].[profile_id] FROM ([comments] LEFT JOIN [userinfo] ON [comments].[poster] = [userinfo].[profile_id]) WHERE [writes_id_c]= " & writesid & " ORDER BY [date_written_c] DESC", con)
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
End Class
