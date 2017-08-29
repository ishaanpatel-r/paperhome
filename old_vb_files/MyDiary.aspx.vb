Imports System.Data
Imports System.Data.SqlClient

Public Class Default3
    Inherits System.Web.UI.Page


    Dim con As sqlconnection
    Dim cmd As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader
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
            con.Open() 'load  feed
            cmd = New SqlCommand(" SELECT * FROM [writes] WHERE ([profile_id] = " & mysess & ") ORDER BY [date_written] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                diary.DataSource = cmd.ExecuteReader()
                diary.DataBind()
            End If
            con.Close()
        End If


    End Sub

    Protected Sub happy_go(ByVal Sender As Object, ByVal e As EventArgs)


        If Radiobuttonlist3.SelectedValue = "1" Then
            If hapfromrm.Value = "" Then
                Dim m As String
                m = "Please enter a from date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo A
            End If
            If haptorm.Value = "" Then
                Dim m As String
                m = "Please enter a to date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo A
            End If
            If happy_words.Value = "" Then
                Dim m As String
                m = "Please enter the word to be included in your search!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo A
            End If

            Dim rmfrom As Date
            rmfrom = DateTime.ParseExact(hapfromrm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim rmto As Date
            rmto = DateTime.ParseExact(haptorm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim mywrites As String
            mywrites = Session("idsess")
            Dim may_word As String
            may_word = happy_words.Value
            con.Open()
            cmd = New SqlCommand("SELECT * FROM writes WHERE (([date_written] BETWEEN @rmfrom AND @rmto) AND ([profile_id] = " & mywrites & ") AND ([content] Like '%' + @mayword + '%') AND ([happy] = 1))", con)
            cmd.Parameters.Add("@rmfrom", SqlDbType.DateTime2)
            cmd.Parameters.Add("@rmto", SqlDbType.DateTime2)
            cmd.Parameters.Add("@mayword", SqlDbType.NVarChar)
            cmd.Parameters("@rmfrom").Value = rmfrom.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@rmto").Value = rmto.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@mayword").Value = may_word.ToString()
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                overview.Style("Display") = "block"
                con.Close()
                con.Open()
                diary.DataSource = cmd.ExecuteReader()
                diary.DataBind()
            Else
                Dim m As String
                m = "Whoops! No such posts were found."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            End If
            con.Close()
        ElseIf Radiobuttonlist3.SelectedValue = "2" Then
            If hapfromrm.Value = "" Then
                Dim m As String
                m = "Please enter a from date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo A
            End If
            If haptorm.Value = "" Then
                Dim m As String
                m = "Please enter a to date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo A
            End If
            If happy_words.Value = "" Then
                Dim m As String
                m = "Please enter the word to be excluded from your search!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo A
            End If

            Dim rmfrom As Date
            rmfrom = DateTime.ParseExact(hapfromrm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim rmto As Date
            rmto = DateTime.ParseExact(haptorm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim mywrites As String
            mywrites = Session("idsess")
            Dim may_not_word As String
            may_not_word = happy_words.Value
            con.Open()
            'cmd = New sqlcommand("SELECT * FROM writes WHERE (([date_written] BETWEEN #" & rmfrom.ToString("MM'/'dd'/'yyyy") & "# AND #" & rmto.ToString("MM'/'dd'/'yyyy") & "#) AND ([profile_id] = " & mywrites & ") AND ([content] NOT Like '%" & may_not_word.ToString() & "%') AND ([happy] = True))", con)
            cmd = New SqlCommand("SELECT * FROM writes WHERE (([date_written] BETWEEN @rmfrom AND @rmto) AND ([profile_id] = " & mywrites & ") AND ([content] NOT Like '%' + @maynotword + '%') AND ([happy] = 1))", con)
            cmd.Parameters.Add("@rmfrom", SqlDbType.DateTime2)
            cmd.Parameters.Add("@rmto", SqlDbType.DateTime2)
            cmd.Parameters.Add("@maynotword", SqlDbType.NVarChar)
            cmd.Parameters("@rmfrom").Value = rmfrom.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@rmto").Value = rmto.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@maynotword").Value = may_not_word.ToString()
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                overview.Style("Display") = "block"
                con.Close()
                con.Open()
                diary.DataSource = cmd.ExecuteReader()
                diary.DataBind()
            Else
                Dim m As String
                m = "Whoops! No such posts were found."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            End If
            con.Close()
        Else
            If hapfromrm.Value = "" Then
                Dim m As String
                m = "Please enter a from date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo A
            End If
            If haptorm.Value = "" Then
                Dim m As String
                m = "Please enter a to date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo A
            End If


            Dim rmfrom As Date
            rmfrom = DateTime.ParseExact(hapfromrm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim rmto As Date
            rmto = DateTime.ParseExact(haptorm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim mywrites As String
            mywrites = Session("idsess")
            con.Open()
            'cmd = New sqlcommand("SELECT * FROM writes WHERE (([date_written] BETWEEN #" & rmfrom.ToString("MM'/'dd'/'yyyy") & "# AND #" & rmto.ToString("MM'/'dd'/'yyyy") & "#) AND ([profile_id] = " & mywrites & ") AND ([happy] = TRUE))", con)
            cmd = New SqlCommand("SELECT * FROM writes WHERE (([date_written] BETWEEN @rmfrom AND @rmto) AND ([profile_id] = " & mywrites & ") AND ([happy] = 1))", con)
            cmd.Parameters.Add("@rmfrom", SqlDbType.DateTime2)
            cmd.Parameters.Add("@rmto", SqlDbType.DateTime2)
            cmd.Parameters("@rmfrom").Value = rmfrom.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@rmto").Value = rmto.ToString("MM'/'dd'/'yyyy")
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                overview.Style("Display") = "block"
                con.Close()
                con.Open()
                diary.DataSource = cmd.ExecuteReader()
                diary.DataBind()
            Else
                Dim m As String
                m = "Whoops! No such posts were found."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            End If
            con.Close()
        End If

A:
    End Sub

    Protected Sub sad_go(ByVal Sender As Object, ByVal e As EventArgs)
        If Radiobuttonlist1.SelectedValue = "1" Then
            If sadfromrm.Value = "" Then
                Dim m As String
                m = "Please enter a from date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo B
            End If
            If sadtorm.Value = "" Then
                Dim m As String
                m = "Please enter a to date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo B
            End If
            If sad_words.Value = "" Then
                Dim m As String
                m = "Please enter the word to be included in your search!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo B
            End If

            Dim rmfrom As Date
            rmfrom = DateTime.ParseExact(sadfromrm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim rmto As Date
            rmto = DateTime.ParseExact(sadtorm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim mywrites As String
            mywrites = Session("idsess")
            Dim may_word As String
            may_word = sad_words.Value
            con.Open()
            'cmd = New sqlcommand("SELECT * FROM writes WHERE (([date_written] BETWEEN #" & rmfrom.ToString("MM'/'dd'/'yyyy") & "# AND #" & rmto.ToString("MM'/'dd'/'yyyy") & "#) AND ([profile_id] = " & mywrites & ") AND ([content] Like '%" & may_word.ToString() & "%') AND ([happy] = False))", con)
            cmd = New SqlCommand("SELECT * FROM writes WHERE (([date_written] BETWEEN @rmfrom AND @rmto) AND ([profile_id] = " & mywrites & ") AND ([content] Like '%' + @mayword + '%') AND ([happy] = 0))", con)
            cmd.Parameters.Add("@rmfrom", SqlDbType.DateTime2)
            cmd.Parameters.Add("@rmto", SqlDbType.DateTime2)
            cmd.Parameters.Add("@mayword", SqlDbType.NVarChar)
            cmd.Parameters("@rmfrom").Value = rmfrom.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@rmto").Value = rmto.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@mayword").Value = may_word.ToString()
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                overview.Style("Display") = "block"
                con.Close()
                con.Open()
                diary.DataSource = cmd.ExecuteReader()
                diary.DataBind()
            Else
                Dim m As String
                m = "Whoops! No such posts were found."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            End If
            con.Close()
        ElseIf Radiobuttonlist1.SelectedValue = "2" Then
            If sadfromrm.Value = "" Then
                Dim m As String
                m = "Please enter a from date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo B
            End If
            If sadtorm.Value = "" Then
                Dim m As String
                m = "Please enter a to date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo B
            End If
            If sad_words.Value = "" Then
                Dim m As String
                m = "Please enter the word to be excluded from your search!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo B
            End If

            Dim rmfrom As Date
            rmfrom = DateTime.ParseExact(sadfromrm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim rmto As Date
            rmto = DateTime.ParseExact(sadtorm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim mywrites As String
            mywrites = Session("idsess")
            Dim may_not_word As String
            may_not_word = sad_words.Value
            con.Open()
            'cmd = New sqlcommand("SELECT * FROM writes WHERE (([date_written] BETWEEN #" & rmfrom.ToString("MM'/'dd'/'yyyy") & "# AND #" & rmto.ToString("MM'/'dd'/'yyyy") & "#) AND ([profile_id] = " & mywrites & ") AND ([content] NOT Like '%" & may_not_word.ToString() & "%') AND ([happy] = False))", con)
            cmd = New SqlCommand("SELECT * FROM writes WHERE (([date_written] BETWEEN @rmfrom AND @rmto) AND ([profile_id] = " & mywrites & ") AND ([content] NOT Like '%' + @maynotword + '%') AND ([happy] = 0))", con)
            cmd.Parameters.Add("@rmfrom", SqlDbType.DateTime2)
            cmd.Parameters.Add("@rmto", SqlDbType.DateTime2)
            cmd.Parameters.Add("@maynotword", SqlDbType.NVarChar)
            cmd.Parameters("@rmfrom").Value = rmfrom.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@rmto").Value = rmto.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@maynotword").Value = may_not_word.ToString()
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                overview.Style("Display") = "block"
                con.Close()
                con.Open()
                diary.DataSource = cmd.ExecuteReader()
                diary.DataBind()
            Else
                Dim m As String
                m = "Whoops! No such posts were found."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            End If
            con.Close()
        Else
            If sadfromrm.Value = "" Then
                Dim m As String
                m = "Please enter a from date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo B
            End If
            If sadtorm.Value = "" Then
                Dim m As String
                m = "Please enter a to date!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                GoTo B
            End If


            Dim rmfrom As Date
            rmfrom = DateTime.ParseExact(sadfromrm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim rmto As Date
            rmto = DateTime.ParseExact(sadtorm.Value, "dd'/'MM'/'yyyy", Nothing)
            Dim mywrites As String
            mywrites = Session("idsess")
            con.Open()
            ' cmd = New sqlcommand("SELECT * FROM writes WHERE (([date_written] BETWEEN #" & rmfrom.ToString("MM'/'dd'/'yyyy") & "# AND #" & rmto.ToString("MM'/'dd'/'yyyy") & "#) AND ([profile_id] = " & mywrites & ")  AND ([happy] = False))", con)
            cmd = New SqlCommand("SELECT * FROM writes WHERE (([date_written] BETWEEN @rmfrom AND @rmto) AND ([profile_id] = " & mywrites & ")  AND ([happy] = 0))", con)
            cmd.Parameters.Add("@rmfrom", SqlDbType.DateTime2)
            cmd.Parameters.Add("@rmto", SqlDbType.DateTime2)
            cmd.Parameters("@rmfrom").Value = rmfrom.ToString("MM'/'dd'/'yyyy")
            cmd.Parameters("@rmto").Value = rmto.ToString("MM'/'dd'/'yyyy")
            cmd.Connection = con
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                overview.Style("Display") = "block"
                con.Close()
                con.Open()
                diary.DataSource = cmd.ExecuteReader()
                diary.DataBind()
            Else
                Dim m As String
                m = "Whoops! No such posts were found."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            End If
            con.Close()
        End If
B:
    End Sub

    Protected Sub burnwrite(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim writeid As String = commandArgsAccept.ToString
        Session("tempsess") = writeid
        Dim wrisess As String
        wrisess = Convert.ToInt32(Session("tempsess"))
        con.Open()
        cmd = New SqlCommand("DELETE FROM activity WHERE [post] = " & wrisess & "", con) 'conflicts with refrence in dbo caus of relationship
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New SqlCommand("DELETE FROM comments WHERE [writes_id_c] = " & wrisess & "", con) 'conflicts with refrence in dbo caus of relationship
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New SqlCommand("DELETE FROM writes WHERE [writes_id] = " & wrisess & "", con) 'conflicts with refrence in dbo caus of relationship
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("MyDiary.aspx")
    End Sub

    Protected Sub txtSearch_KeyUp(ByVal Sender As Object, ByVal e As EventArgs)

        sea = seahome.Text
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()

        'cmd = New sqlcommand("SELECT * FROM [writes] WHERE ([profile_id] = " & mysess & " AND  (([content] LIKE '%" & sea & "%') OR ([hashes] LIKE '%" & sea & "%') OR ([feeling_type] LIKE '%" & sea & "%'))) ORDER BY [date_written] DESC", con)
        cmd = New SqlCommand("SELECT * FROM [writes] WHERE ([profile_id] = " & mysess & " AND  (([content] LIKE '%' + @sea + '%') OR ([hashes] LIKE '%' + @sea + '%') OR ([feeling_type] LIKE '%' + @sea + '%'))) ORDER BY [date_written] DESC", con)
        cmd.Parameters.Add("@sea", SqlDbType.NVarChar)
        cmd.Parameters("@sea").Value = seahome.Text
        cmd.Connection = con
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()

            diary.Visible = True
            diary.DataSource = cmd.ExecuteReader()
            diary.DataBind()
        Else
            diary.Visible = False

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
        For Each rItem As RepeaterItem In diary.Items
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
        For Each rItem As RepeaterItem In diary.Items
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

        For Each rItem As RepeaterItem In diary.Items
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

    Sub pubunpub(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim wrisess As Integer = e.CommandArgument
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))

        con.Open()
        cmd = New SqlCommand("SELECT * FROM [writes] WHERE [writes_id] = " & wrisess & "", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Dim stat As Integer = dr("published")
            dr.Close()
            If stat = 0 Then
                Dim m As String
                m = "Published on Openbook."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                con.Close()
                con.Open()
                cmd = New SqlCommand("UPDATE [writes] SET [published] = 1 WHERE [writes_id] = " & wrisess & "", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                'activity input'
                Dim intDay As Date
                intDay = Date.Now
                con.Open()
                cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'published a', '" & intDay & "', '" & wrisess & "') ", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                Dim m As String
                m = "Unpublished from Openbook."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
                con.Close()
                con.Open()
                cmd = New SqlCommand("UPDATE [writes] SET [published] = 0 WHERE [writes_id] = " & wrisess & "", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()

            End If
        End If

        con.Open() 'load  feed
        cmd = New SqlCommand(" SELECT * FROM [writes] WHERE ([profile_id] = " & mysess & ") ORDER BY [date_written] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            diary.DataSource = cmd.ExecuteReader()
            diary.DataBind()
        End If
        con.Close()
    End Sub

    Protected Sub diary_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles diary.ItemDataBound
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
