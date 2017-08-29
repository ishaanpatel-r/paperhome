Imports System.Data
imports system.data.sqlclient

Public Class _Default
    Inherits System.Web.UI.Page


    Dim con As sqlconnection
    Dim cmd As sqlcommand
    Dim cmd2 As sqlcommand
    Dim cmd3 As sqlcommand
    Dim cmd4 As sqlcommand
    Dim cmd5 As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader
    Public sea As String 'searchstring
    Public seai As String 'follsearch
    Public seap As String 'phosearch



    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        Session("prosess") = Request.QueryString("profileid")
        Dim psess As Integer

        Try 'try catch to catch any unregistered profileids in url
            psess = Convert.ToInt32(Session("prosess"))
            If psess = 0 Then
                Response.Redirect("Home.aspx")
            Else
                Dim psessinit As Integer = Convert.ToInt32(Session("prosess"))
                con.Open()
                cmd = New SqlCommand("SELECT * FROM userinfo where [profile_id]= " & psessinit & "", con)
                cmd.Connection = con
                dr = cmd.ExecuteReader
                If (dr.Read) Then
                    dr.Close()
                    psess = psessinit
                Else
                    dr.Close()
                    Response.Redirect("Home.aspx")
                End If
                con.Close()
            End If
        Catch
            Response.Redirect("Home.aspx")
        End Try

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        If String.Equals(psess.ToString(), mysess.ToString()) = True Then
            Response.Redirect("MyProfile.aspx")
        End If

        con.Open()
        'block check
        cmd = New SqlCommand("SELECT * FROM [block_list] WHERE (([blocker] = " & psess & " AND [blockee] = " & mysess & ") OR ([blockee] = " & psess & " AND [blocker] = " & mysess & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            dr.Close()
            Response.Redirect("Home.aspx")

        End If



        If Page.IsPostBack = False Then
            Dim pros As Integer
            pros = Convert.ToInt32(Session("prosess"))



            'load foll
            con.Close()
            con.Open()
            cmd = New SqlCommand("SELECT * FROM ([userinfo] LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([group_to_following] =  " & pros & ")", con)

            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()

                Repeater11.Visible = True
                Repeater11.DataSource = cmd.ExecuteReader()
                dr.Close()
                Repeater11.DataBind()
            Else
                dr.Close()
                Repeater11.Visible = False

            End If

            'loadphotos
            con.Close()
            con.Open()
            cmd = New SqlCommand("SELECT photo_priv FROM [userinfo] WHERE ([profile_id] = " & psess & ")", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim ppriv As Integer = Convert.ToInt32(dr("photo_priv"))
                con.Close()
                con.Open()
                If (ppriv = 1) Then
                    allclick.CssClass = "list-group-item active"
                    youclick.CssClass = "list-group-item"
                    tripclick.CssClass = "list-group-item"
                    parclick.CssClass = "list-group-item"
                    eveclick.CssClass = "list-group-item"
                    cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & ") ORDER BY [add_date] DESC", con)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        con.Close()
                        con.Open()

                        gallery.DataSource = cmd.ExecuteReader()
                        gallery.DataBind()
                        gallery.Visible = True

                    Else
                        con.Close()
                        con.Open()

                        empdiv.Style("Display") = "block"
                        gallery.Visible = False
                    End If
                ElseIf (ppriv = 2) Then
                    cmd = New SqlCommand("SELECT * FROM [feelpals_sys] WHERE (([group_to_following] = " & psess & " AND [group_to_followers] = " & mysess & ") OR ([group_to_followers] = " & psess & " AND [group_to_following] = " & mysess & "))", con) 'check if feelpals
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        con.Close()
                        con.Open()
                        allclick.CssClass = "list-group-item active"
                        youclick.CssClass = "list-group-item"
                        tripclick.CssClass = "list-group-item"
                        parclick.CssClass = "list-group-item"
                        eveclick.CssClass = "list-group-item"
                        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & ") ORDER BY [add_date] DESC", con)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            con.Close()
                            con.Open()

                            gallery.DataSource = cmd.ExecuteReader()
                            gallery.DataBind()
                            gallery.Visible = True

                        Else
                            con.Close()
                            con.Open()
                            empdiv.Style("Display") = "block"
                            gallery.Visible = False
                        End If
                        con.Close()
                    Else
                        Session("sharestatus") = "people who aren't related"
                        Div3.Style("Display") = "block"
                    End If
                ElseIf (ppriv = 3) Then
                    gallery.Visible = False
                    Session("sharestatus") = "anyone"
                    Div3.Style("Display") = "block"

                End If
            End If
        End If

        'modefetch

        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT TOP 1 [writes].[feeling_type] FROM [writes]  WHERE [profile_id] = " & psess & " GROUP BY [writes].[feeling_type] ORDER BY Count([writes].[feeling_type]) DESC", con)
        dr = cmd.ExecuteReader()
        If dr.Read Then
            Session("1stmodefeel") = dr(0).ToString()
        End If
        con.Close()
        con.Open()
        cmd2 = New SqlCommand("SELECT [writes].[feeling_type] FROM [writes]  WHERE (([profile_id] = " & psess & ") AND ([writes].[feeling_type] = (SELECT MAX([writes].[feeling_type]) FROM [writes] WHERE [writes].[feeling_type] <> (SELECT MAX([writes].[feeling_type]) FROM [writes] )))) GROUP BY [writes].[feeling_type] ORDER BY Count([writes].[feeling_type]) DESC", con)
        dr = cmd2.ExecuteReader()
        If dr.Read Then
            Session("2ndmodefeel") = dr(0).ToString()
        End If

        con.Close()
        con.Open()
        cmd2 = New SqlCommand("SELECT [sex] from userinfo where [profile_id]=" & psess & "", con)
        dr = cmd2.ExecuteReader()
        If dr.Read Then
            Session("sex") = dr("sex")
            dr.Close()
            If Session("sex") = "male" Then
                Session("sexdisp") = "His"
                Session("sexdisp2") = "he"
                Session("sexnum") = "him"
                pho_disp_sex.Text = "Him"
            Else
                Session("sexdisp") = "Her"
                Session("sexdisp2") = "she"
                Session("sexnum") = "her"
                pho_disp_sex.Text = "Her"
            End If
        End If

        'request & messaging privacy check
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [feelpals_sys] WHERE (([group_to_following] = " & mysess & ") AND ([group_to_followers] = " & psess & "))", con)
        dr = cmd.ExecuteReader()
        If dr.Read Then
            follbutts.Visible = True
            nfollbutts.Visible = False
        Else
            dr.Close()
            cmd = New SqlCommand("SELECT request_priv FROM [userinfo] WHERE [profile_id] = " & psess & "", con)
            dr = cmd.ExecuteReader()
            If dr.Read Then
                If (dr(0).ToString() = 1) Then
                    dr.Close()
                    follbutts.Visible = False
                    nfollbutts.Visible = True
                ElseIf (dr(0).ToString() = 2) Then
                    dr.Close()
                    cmd = New SqlCommand("SELECT * FROM [feelpals_sys] WHERE (([group_to_following] = " & psess & " AND [group_to_followers] = " & mysess & ") OR ([group_to_followers] = " & psess & " AND [group_to_following] = " & mysess & "))", con) 'check if feelpals
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        dr.Close()
                        follbutts.Visible = False
                        nfollbutts.Visible = True
                    Else
                        dr.Close()
                        follbutts.Visible = False
                        nfollbutts.Visible = False
                    End If
                End If
            End If
        End If

        'load  notif
        con.Close()
        con.Open()
        Dim selpro As Integer
        selpro = Session("prosess")
        cmd = New SqlCommand("SELECT TOP 5 [profile_id],[type], [uname], [date_time_written], [post], [doer], [doee] FROM ([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) WHERE [doer] = " & selpro & " ORDER BY [date_time_written] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader


        If (dr.Read) Then

            con.Close()
            con.Open()

            notifrepeater.DataSource = cmd.ExecuteReader()
            notifrepeater.DataBind()

        End If


        'writes privacy check
        con.Close()
        con.Open()

        cmd = New SqlCommand("SELECT writes_priv FROM [userinfo] WHERE ([profile_id] = " & psess & ")", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Dim wpriv As Integer = Convert.ToInt32(dr("writes_priv"))
            con.Close()
            con.Open()

            If (wpriv = 1) Then

                res_div.Style("Display") = "none"


            End If
            If (wpriv = 2) Then
                con.Close()
                con.Open()
                cmd = New SqlCommand("SELECT * FROM [feelpals_sys] WHERE (([group_to_following] = " & psess & " AND [group_to_followers] = " & mysess & ") OR ([group_to_followers] = " & psess & " AND [group_to_following] = " & mysess & "))", con) 'check if feelpals
                dr = cmd.ExecuteReader
                If dr.Read Then
                    con.Close()
                    con.Open()
                    res_div.Style("Display") = "none"
                    feeddiv.Visible = True
                Else
                    con.Close()
                    con.Open()
                    Session("sharestatus") = "people who aren't related"
                    res_div.Style("Display") = "block"
                    feeddiv.Visible = False
                End If
            End If
            If (wpriv = 3) Then
                con.Close()
                con.Open()
                Session("sharestatus") = "anyone"
                res_div.Style("Display") = "block"
                feeddiv.Visible = False

            End If
        End If

        If Page.IsPostBack = False Then 'feed fill

            con.Close()
            con.Open()
            cmd = New SqlCommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM ([writes] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[writes].[profile_id]) WHERE ([published] = 1 AND [userinfo].[profile_id]= " & psess & ") ORDER BY [date_written] DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()

                Repeater8.DataSource = cmd.ExecuteReader()
                Repeater8.DataBind()
                Repeater8.Visible = True

            End If
            con.Close()

            Dim pros As Integer = Session("prosess")
            con.Open() 'restrict duplicate requests
            cmd = New SqlCommand("SELECT * FROM [req] WHERE ([to]= " & pros & " AND [from]=" & mysess & " AND [accepted]= 0)", con)

            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                dr.Close()
                nfollbutts.Visible = False

            Else
                dr.Close()

            End If
        End If

        con.Close()
    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
        con.Open() 'load  notif
        Dim selpro As Integer
        selpro = Session("prosess")
        cmd = New sqlcommand("SELECT TOP 5 [profile_id],[type], [uname], [date_time_written], [post] FROM ([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) WHERE [doer] = " & selpro & " ORDER BY [date_time_written] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader


        If (dr.Read) Then

            con.Close()
            con.Open()

            notifrepeater.DataSource = cmd.ExecuteReader()
            notifrepeater.DataBind()

        End If
        con.Close()
    End Sub

    Protected Sub txtSearch_KeyUp(ByVal Sender As Object, ByVal e As EventArgs)

        sea = seareq.Text
        Dim psess As Integer
        psess = Convert.ToInt32(Session("prosess"))
        con.Open()
        cmd = New SqlCommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM ([writes] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[writes].[profile_id]) WHERE ([published] = 1 AND [userinfo].[profile_id]= " & psess & " AND (([content] LIKE '%' + @sea + '%') OR ([hashes] LIKE '%' + @sea + '%') OR ([feeling_type] LIKE '%' + @sea + '%'))) ORDER BY [date_written] DESC", con)
        cmd.Parameters.Add("@sea", SqlDbType.NVarChar)
        cmd.Parameters("@sea").Value = seareq.Text.Replace("'", "''")

        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()

            Repeater8.Visible = True
            Repeater8.DataSource = cmd.ExecuteReader()
            Repeater8.DataBind()
        Else
            Repeater8.Visible = False

        End If
        con.Close()


    End Sub

    Protected Sub txtSearch_KeyUp_foll(ByVal Sender As Object, ByVal e As EventArgs)

        seai = seafoll.Text
        Dim pros As Integer
        pros = Convert.ToInt32(Session("prosess"))
        con.Open()
        cmd = New SqlCommand("SELECT * FROM ([userinfo] LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([group_to_following] =  " & pros & ") AND (([fname] LIKE '%' + @seai + '%') OR ([lname] LIKE '%' + @seai + '%') OR ([uname] LIKE '%' + @seai + '%'))", con)
        cmd.Parameters.Add("@seai", SqlDbType.NVarChar)
        cmd.Parameters("@seai").Value = seafoll.Text.Replace("'", "''")

        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()

            Repeater11.Visible = True
            Repeater11.DataSource = cmd.ExecuteReader()
            Repeater11.DataBind()
        Else
            Repeater11.Visible = False

        End If
        con.Close()
    End Sub

    Protected Sub txtSearch_KeyUp_pho(ByVal Sender As Object, ByVal e As EventArgs)

        seap = seapho.Text
        Dim psess As Integer
        psess = Convert.ToInt32(Session("prosess"))
        If seap.Length > 0 Then
            con.Open()
            cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & " AND [caption] LIKE '%' + @seap + '%') ORDER BY [add_date] DESC", con)
            cmd.Parameters.Add("@seap", SqlDbType.NVarChar)
            cmd.Parameters("@seap").Value = seapho.Text.Replace("'", "''")

            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()

                gallery.Visible = True
                gallery.DataSource = cmd.ExecuteReader()
                gallery.DataBind()
            Else
                gallery.Visible = False

            End If
            con.Close()
        Else

            allclick.CssClass = "list-group-item active"
            youclick.CssClass = "list-group-item"
            tripclick.CssClass = "list-group-item"
            parclick.CssClass = "list-group-item"
            eveclick.CssClass = "list-group-item"

            con.Open()
            cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & ") ORDER BY [add_date] DESC", con)
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
        For Each rItem As RepeaterItem In Repeater8.Items
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
        For Each rItem As RepeaterItem In Repeater8.Items
            Dim subRepeter As Repeater = DirectCast(rItem.FindControl("Repeater2x"), Repeater)
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

        For Each rItem As RepeaterItem In Repeater8.Items
            Dim subRepeter As Repeater = DirectCast(rItem.FindControl("Repeater2x"), Repeater)
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

    Protected Sub sendmess(ByVal Sender As Object, ByVal e As CommandEventArgs)

        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim proid As Integer = commandArgsAccept
        Session("tempsess") = proid
        Dim prosess As Integer
        prosess = Convert.ToInt32(Session("tempsess"))
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim intDay As Date
        intDay = Date.Now
        con.Open()
        cmd = New sqlcommand("SELECT [thread_id] FROM message_threads WHERE (([from] = " & prosess & " AND [to] = " & mysess & ") OR ([to] = " & prosess & " AND [from] = " & mysess & "))", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Dim thread As Integer
            Dim txtBox As HtmlInputText
            Dim messtxta2 As String = " "
            thread = Convert.ToInt32(dr(0).ToString())
            dr.Close()
            For Each rItem As RepeaterItem In Repeater11.Items
                txtBox = DirectCast(rItem.FindControl("messtxt"), HtmlInputText)
                If Not IsNothing(txtBox) Then
                    If txtBox.Value.Length > 0 Then
                        messtxta2 = txtBox.Value
                    End If
                End If
            Next
            cmd2 = New SqlCommand("INSERT INTO messages(content, sender, thread_id, date_written) VALUES(@msgcon, " & mysess & ", " & thread & ", '" & intDay & "')", con)
            cmd2.Parameters.Add("@msgcon", SqlDbType.NVarChar)
            cmd2.Parameters("@msgcon").Value = messtxta2.Replace("'", "''")

            cmd2.Connection = con
            cmd2.ExecuteNonQuery()
            cmd = New sqlcommand("UPDATE message_threads SET [last_updated_on] = '" & intDay & "' WHERE ([thread_id] = " & thread & ")", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            Response.Redirect(String.Format("XsProfile.aspx?profileid={0}", Session("prosess")))
        Else
            dr.Close()
            con.Close()
            con.Open()
            cmd3 = New SqlCommand("INSERT INTO message_threads([from], [to]) VALUES('" & mysess & "', '" & prosess & "')", con)
            cmd3.Connection = con
            cmd3.ExecuteNonQuery()
            con.Close()
            con.Open()
            cmd4 = New SqlCommand("SELECT [thread_id] FROM message_threads WHERE (([from] = " & prosess & " AND [to] = " & mysess & ") OR ([to] = " & prosess & " AND [from] = " & mysess & "))", con)
            dr = cmd4.ExecuteReader
            If (dr.Read) Then
                Dim newthread As Integer
                newthread = Convert.ToInt32(dr(0).ToString())
                dr.Close()
                Dim txtBox As HtmlInputText
                Dim messtxta As String = " "
                For Each rItem As RepeaterItem In Repeater12.Items
                    txtBox = DirectCast(rItem.FindControl("messxtxt"), HtmlInputText)
                    If Not IsNothing(txtBox) Then
                        If txtBox.Value.Length > 0 Then
                            messtxta = txtBox.Value
                        End If
                    End If
                Next
                cmd5 = New SqlCommand("INSERT INTO messages(content, sender, thread_id, date_written) VALUES(@newthreadmsgcon, '" & mysess & "', '" & newthread & "', '" & intDay & "')", con)
                cmd5.Parameters.Add("@newthreadmsgcon", SqlDbType.NVarChar)
                cmd5.Parameters("@newthreadmsgcon").Value = messtxta.Replace("'", "''")

                cmd5.Connection = con
                cmd5.ExecuteNonQuery()
                con.Close()
                con.Open()
                cmd = New SqlCommand("UPDATE message_threads SET [last_updated_on] = '" & intDay & "' WHERE ([thread_id] = " & newthread & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                Response.Redirect(String.Format("XsProfile.aspx?profileid={0}", Session("prosess")))
            End If
        End If
        con.Close()
    End Sub

    Sub f(ByVal Sender As Object, ByVal e As CommandEventArgs)

        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim proid As Integer = commandArgsAccept
        Session("f") = proid
        Dim prosess As Integer
        prosess = Convert.ToInt32(Session("f"))
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        If String.Equals(prosess.ToString(), mysess.ToString()) = True Then
            Response.Redirect("MyProfile.aspx")
        Else
            Dim intDay As Date
            intDay = Date.Now
            con.Open()
            cmd = New sqlcommand("INSERT INTO req([from], [to], date_time)  VALUES(" & mysess & "," & prosess & ",'" & intDay & "')", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            con.Close()
            Response.Redirect(String.Format("XsProfile.aspx?profileid={0}", Session("prosess")))
        End If
       
    End Sub

    Protected Sub allpic(ByVal Sender As Object, ByVal e As EventArgs)
        allclick.CssClass = "list-group-item active"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim psess As Integer
        psess = Convert.ToInt32(Session("prosess"))
        con.Open()
        cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & ") ORDER BY [add_date] DESC", con)
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
      
        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item active"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim psess As Integer
        psess = Convert.ToInt32(Session("prosess"))
        con.Open()
        cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & " AND [folder_type_path]='you') ORDER BY [add_date] DESC", con)
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
        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item active"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim psess As Integer
        psess = Convert.ToInt32(Session("prosess"))
        con.Open()
        cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & " AND [folder_type_path]='trip') ORDER BY [add_date] DESC", con)
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
        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item  active"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim psess As Integer
        psess = Convert.ToInt32(Session("prosess"))
        con.Open()
        cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & " AND [folder_type_path]='parties') ORDER BY [add_date] DESC", con)
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
        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item active"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item"

        Dim psess As Integer
        psess = Convert.ToInt32(Session("prosess"))
        con.Open()
        cmd = New sqlcommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & " AND [folder_type_path]='events') ORDER BY [add_date] DESC", con)
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
        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item active"
        dpclick.CssClass = "list-group-item"

        Dim psess As Integer
        psess = Convert.ToInt32(Session("prosess"))
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & " AND [folder_type_path]='coverphotos') ORDER BY [add_date] DESC", con)
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
        allclick.CssClass = "list-group-item"
        youclick.CssClass = "list-group-item"
        tripclick.CssClass = "list-group-item"
        parclick.CssClass = "list-group-item"
        eveclick.CssClass = "list-group-item"
        obclick.CssClass = "list-group-item"
        dpclick.CssClass = "list-group-item active"

        Dim psess As Integer
        psess = Convert.ToInt32(Session("prosess"))
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [picture_library] WHERE ([user_path] = " & psess & " AND [folder_type_path]='displaypictures') ORDER BY [add_date] DESC", con)
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

    Protected Sub Repeater8_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles Repeater8.ItemDataBound
        Dim subRepeter As Repeater = DirectCast(e.Item.FindControl("Repeater2x"), Repeater)
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
