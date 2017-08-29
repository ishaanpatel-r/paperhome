Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web
Imports System.Net

Public Class Default3
    Inherits System.Web.UI.Page


    Dim con As sqlconnection
    Dim cmd As sqlcommand
    Dim cmd2 As sqlcommand
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


        If Page.IsPostBack = False Then
            con.Open()
            Dim weeoff As Date = Date.Now.AddDays(-7)
            Dim todaywee As Date = Date.Now
            cmd = New SqlCommand("SELECT TOP 20 [writes].[img_att], [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type] FROM (([writes] INNER JOIN [userinfo] ON  [writes].[profile_id] = [userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id] = [activity].[post]) WHERE ([activity].[type] = 'liked a' AND ([activity].[date_time_written] BETWEEN '" & weeoff & "' AND '" & todaywee & "')) GROUP BY [activity].[post], [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] ORDER BY COUNT(post) DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()
                pyl_fill.Visible = True
                pyl_fill.DataSource = cmd.ExecuteReader()
                dr.Close()
                pyl_fill.DataBind()
            Else
                con.Close()
                con.Open()
                cmd = New SqlCommand("SELECT TOP 20 [writes].[img_att], [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type] FROM (([writes] INNER JOIN [userinfo] ON  [writes].[profile_id] = [userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id] = [activity].[post]) WHERE ([activity].[type] = 'liked a') GROUP BY [activity].[post], [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] ORDER BY COUNT(post) DESC", con)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    con.Close()
                    con.Open()
                    pyl_fill.Visible = True
                    pyl_fill.DataSource = cmd.ExecuteReader()
                    dr.Close()
                    pyl_fill.DataBind()
                End If
            End If
            con.Close()

            'algo to get posts that have similar hashes x2 or similar feels x2 or mode feels x1
            Dim hash1 As String = "" 'fetched from activity posts fill
            Dim hash2 As String = "" 'fetched from activity posts fill
            Dim feel1 As String = "" 'fetched from activity posts fill
            Dim feel2 As String = "" 'fetched from activity posts fill
            Dim modefeel1 As String = "" 'fetched from activity posts fill's user modes
            Dim modefeel2 As String = "" ' ""

            Dim mysess As Integer = Convert.ToInt32(Session("idsess"))

            con.Open()
            cmd = New SqlCommand("SELECT TOP 1 [writes].[feeling_type] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id]=[activity].[post]) WHERE ([doer]= " & mysess & " AND [type]= 'liked a') GROUP BY [writes].[feeling_type] ORDER BY Count([writes].[feeling_type]) DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                feel1 = dr("feeling_type")

                dr.Close()
            End If
            con.Close()

            con.Open()
            cmd = New SqlCommand("SELECT TOP 1 [writes].[feeling_type] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id]=[activity].[post]) WHERE ([doer]= " & mysess & " AND [type]= 'liked a' AND [feeling_type] <> '" & feel1 & "') GROUP BY [writes].[feeling_type] ORDER BY Count([writes].[feeling_type]) DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                feel2 = dr("feeling_type")

                dr.Close()
            End If
            con.Close()

            con.Open()
            cmd = New SqlCommand("SELECT TOP 1 [writes].[hashes] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id]=[activity].[post]) WHERE ([doer]= " & mysess & " AND [type]= 'liked a') GROUP BY [writes].[hashes] ORDER BY Count([writes].[hashes]) DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                hash1 = dr("hashes")

                dr.Close()
            End If
            con.Close()

            con.Open()
            cmd = New SqlCommand("SELECT TOP 1 [writes].[hashes] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id]=[activity].[post]) WHERE ([doer]= " & mysess & " AND [type]= 'liked a' AND [hashes] <> '" & hash1 & "') GROUP BY [writes].[hashes] ORDER BY Count([writes].[hashes]) DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                hash2 = dr("hashes")

                dr.Close()
            End If
            con.Close()

            con.Open()
            cmd = New SqlCommand("SELECT TOP 20 [writes].[img_att], [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type] FROM (([writes] INNER JOIN [userinfo] ON  [writes].[profile_id] = [userinfo].[profile_id]) LEFT JOIN [activity] ON [writes].[writes_id] = [activity].[post]) WHERE ([activity].[type] = 'liked a' AND [doee] <> " & mysess & " AND (([feeling_type] = '" & feel1 & "' OR [feeling_type] = '" & feel2 & "') OR ([hashes] = '" & hash1 & "' OR [hashes] = '" & hash2 & "'))) GROUP BY [activity].[post], [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] ORDER BY COUNT(post) DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()
                s_post_fill.Visible = True
                s_post_fill.DataSource = cmd.ExecuteReader()
                dr.Close()
                s_post_fill.DataBind()
            End If
            con.Close()
        End If


        If Session("nonexist") = "1" Then
            Dim mysess As Integer = Convert.ToInt32(Session("idsess"))
            Dim citysess As String
            con.Open()
            cmd = New SqlCommand("SELECT [city] FROM [userinfo] WHERE [profile_id] = " & mysess & "", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                citysess = dr("city")
            End If
            con.Close()
            Dim dobfet As Date
            con.Open()
            cmd = New SqlCommand("SELECT [dob] FROM [userinfo] WHERE [profile_id] = " & mysess & "", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                dobfet = dr("dob")
            End If
            con.Close()
            Dim twoyooff As Date = dobfet.AddYears(-2)
            Dim twoyoon As Date = dobfet.AddYears(2)

            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM [userinfo] WHERE (([userinfo].[city] = '" & citysess & "') AND ([dob] BETWEEN '" & twoyooff & "' AND '" & twoyoon & "') AND ([profile_id] <> " & mysess & "))", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "necompstep1();", True)
                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                dr.Close()
                s_cows_fill.DataBind()
                con.Close()
                Session("nonexist") = 0
            Else
                con.Close()
                con.Open()
                cmd = New SqlCommand("SELECT DISTINCT TOP 8 [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM [userinfo] WHERE (([userinfo].[city] = '" & citysess & "') AND ([profile_id] <> " & mysess & "))", con)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "necompstep2();", True)
                    con.Close()
                    con.Open()
                    s_post_fill.Visible = False
                    s_cows_fill.Visible = True
                    s_cows_fill.DataSource = cmd.ExecuteReader()
                    dr.Close()
                    s_cows_fill.DataBind()
                    con.Close()
                    Session("nonexist") = 0
                Else
                    ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "necompfail();", True)
                    s_cows.Style("Display") = "none"
                    con.Close()
                End If
            End If
        End If

    End Sub

    Protected Sub search_posts(ByVal Sender As Object, ByVal e As EventArgs)
        Dim hash_words As String
        hash_words = post_hash_txt.Value
        Dim feel As String
        feel = post_feel_txt.Value
        If (post_feel_txt.Value.Length = 0 And post_hash_txt.Value.Length = 0) Then
            Dim m As String
            m = "Please enter at least one parameter!"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            GoTo A
        End If
        If (post_feel_txt.Value.Length > 0 And post_hash_txt.Value.Length > 0) Then
            con.Open()
            cmd = New SqlCommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att]  FROM ([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) WHERE (([hashes] Like '%' + @hasht + '%') AND ([feeling_type] =   @feelt ) AND ([published] = 1))", con)
            cmd.Parameters.Add("@hasht", SqlDbType.NVarChar)
            cmd.Parameters("@hasht").Value = hash_words.Replace("'", "''").ToString()
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Here are the posts you searched for! :)"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                con.Close()
                con.Open()

                s_posts.Style("Display") = "block"
                empdiv.Style("Display") = "none"
                s_cows_fill.Visible = False
                s_post_fill.Visible = True
                s_post_fill.DataSource = cmd.ExecuteReader()
                s_post_fill.DataBind()
                con.Close()
            Else
                Dim m As String
                m = "Nada. Your requests are much too esoteric. :o"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

            End If
        End If
        If (post_feel_txt.Value.Length > 0 And post_hash_txt.Value.Length = 0) Then
            con.Open()

            cmd = New SqlCommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM ([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) WHERE (([feeling_type] LIKE '%' + @feelt + '%') AND ([published] = 1))", con)
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Here are the posts you searched for! :)"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                con.Close()
                con.Open()

                s_posts.Style("Display") = "block"
                empdiv.Style("Display") = "none"
                s_cows_fill.Visible = False
                s_post_fill.Visible = True
                s_post_fill.DataSource = cmd.ExecuteReader()
                s_post_fill.DataBind()
                con.Close()
            Else
                Dim m As String
                m = "Nada. Your requests are much too esoteric. Try some more accurate feeling perhaps? :o"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

            End If
        End If
        If (post_feel_txt.Value.Length = 0 And post_hash_txt.Value.Length > 0) Then
            con.Open()
            cmd = New SqlCommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM ([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) WHERE (([hashes] Like '%' + @hasht+ '%') AND ([published] = 1))", con)
            cmd.Parameters.Add("@hasht", SqlDbType.NVarChar)
            cmd.Parameters("@hasht").Value = hash_words.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Here are the posts you searched for! :)"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
                con.Close()
                con.Open()

                s_posts.Style("Display") = "block"
                empdiv.Style("Display") = "none"
                s_cows_fill.Visible = False
                s_post_fill.Visible = True
                s_post_fill.DataSource = cmd.ExecuteReader()
                s_post_fill.DataBind()
                con.Close()
            Else
                Dim m As String
                m = "Nada. Your requests are much too esoteric. Try a different hash-tag perhaps? :o"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

            End If
        End If
A:
    End Sub

    Protected Sub txtSearch_KeyUp(ByVal Sender As Object, ByVal e As EventArgs)

        sea = seahome.Text
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM [userinfo] WHERE ([uname] LIKE '%' + @sea + '%') OR ([fname] LIKE '%' + @sea + '%') OR ([lname] LIKE '%' + @sea + '%')", con)
        cmd.Parameters.Add("@sea", SqlDbType.NVarChar)
        cmd.Parameters("@sea").Value = sea.Replace("'", "''").ToString()
        dr = cmd.ExecuteReader
        If sea.Length > 0 Then
            If dr.Read Then
                con.Close()
                con.Open()


                s_cows.Style("Display") = "block"
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
            Else
                s_cows.Style("Display") = "none"
                s_post_fill.Visible = True
            End If
        Else
            s_cows.Style("Display") = "none"
            s_post_fill.Visible = True

        End If

        con.Close()
    End Sub

    Protected Sub search_friends(ByVal Sender As Object, ByVal e As EventArgs)
        If feel_pals_txt.Value = "" And sim_fav_pals_check.Checked = False And sim_int_pals_check.Checked = False Then
            Dim m As String
            m = "Please enter at least one parameter!"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            GoTo C
        End If
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))


        con.Open()
        cmd = New SqlCommand("SELECT [music_junc].[music_d_id] FROM ([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id] = [music_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mu_1") = dr(0).ToString
        End If
        Dim mu_id1 As Integer
        mu_id1 = Session("mu_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [music_junc].[music_d_id] FROM ([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id] = [music_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([music_d_id] <> " & mu_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mu_2") = dr(0).ToString
        End If
        Dim mu_id2 As Integer
        mu_id2 = Session("mu_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [music_junc].[music_d_id] FROM ([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id] = [music_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([music_d_id] <> " & mu_id1 & ") AND ([music_d_id] <> " & mu_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mu_3") = dr(0).ToString
        End If
        Dim mu_id3 As Integer
        mu_id3 = Session("mu_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [movies_junc].[movies_d_id] FROM ([userinfo] INNER JOIN [movies_junc] ON [userinfo].[profile_id] = [movies_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mo_1") = dr(0).ToString
        End If
        Dim mo_id1 As Integer
        mo_id1 = Session("mo_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [movies_junc].[movies_d_id] FROM ([userinfo] INNER JOIN [movies_junc] ON [userinfo].[profile_id] = [movies_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([movies_d_id] <> " & mo_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mo_2") = dr(0).ToString
        End If
        Dim mo_id2 As Integer
        mo_id2 = Session("mo_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [movies_junc].[movies_d_id] FROM ([userinfo] INNER JOIN [movies_junc] ON [userinfo].[profile_id] = [movies_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([movies_d_id] <> " & mo_id1 & ") AND ([movies_d_id] <> " & mo_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mo_3") = dr(0).ToString
        End If
        Dim mo_id3 As Integer
        mo_id3 = Session("mo_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [books_junc].[books_d_id] FROM ([userinfo] INNER JOIN [books_junc] ON [userinfo].[profile_id] = [books_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("bo_1") = dr(0).ToString
        End If
        Dim bo_id1 As Integer
        bo_id1 = Session("bo_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [books_junc].[books_d_id] FROM ([userinfo] INNER JOIN [books_junc] ON [userinfo].[profile_id] = [books_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([books_d_id] <> " & bo_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("bo_2") = dr(0).ToString
        End If
        Dim bo_id2 As Integer
        bo_id2 = Session("bo_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [books_junc].[books_d_id] FROM ([userinfo] INNER JOIN [books_junc] ON [userinfo].[profile_id] = [books_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([books_d_id] <> " & bo_id1 & ") AND ([books_d_id] <> " & bo_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("bo_3") = dr(0).ToString
        End If
        Dim bo_id3 As Integer
        bo_id3 = Session("bo_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [food_junc].[food_d_id] FROM ([userinfo] INNER JOIN [food_junc] ON [userinfo].[profile_id] = [food_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("fo_1") = dr(0).ToString
        End If
        Dim fo_id1 As Integer
        fo_id1 = Session("fo_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [food_junc].[food_d_id] FROM ([userinfo] INNER JOIN [food_junc] ON [userinfo].[profile_id] = [food_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([food_d_id] <> " & fo_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("fo_2") = dr(0).ToString
        End If
        Dim fo_id2 As Integer
        fo_id2 = Session("fo_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [food_junc].[food_d_id] FROM ([userinfo] INNER JOIN [food_junc] ON [userinfo].[profile_id] = [food_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([food_d_id] <> " & fo_id1 & ") AND ([food_d_id] <> " & fo_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("fo_3") = dr(0).ToString
        End If
        Dim fo_id3 As Integer
        fo_id3 = Session("fo_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [tv_junc].[tv_d_id] FROM ([userinfo] INNER JOIN [tv_junc] ON [userinfo].[profile_id] = [tv_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("tv_1") = dr(0).ToString
        End If
        Dim tv_id1 As Integer
        tv_id1 = Session("tv_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [tv_junc].[tv_d_id] FROM ([userinfo] INNER JOIN [tv_junc] ON [userinfo].[profile_id] = [tv_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([tv_d_id] <> " & tv_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("tv_2") = dr(0).ToString
        End If
        Dim tv_id2 As Integer
        tv_id2 = Session("tv_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [tv_junc].[tv_d_id] FROM ([userinfo] INNER JOIN [tv_junc] ON [userinfo].[profile_id] = [tv_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([tv_d_id] <> " & tv_id1 & ") AND ([tv_d_id] <> " & tv_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("tv_3") = dr(0).ToString
        End If
        Dim tv_id3 As Integer
        tv_id3 = Session("tv_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [team_junc].[team_d_id] FROM ([userinfo] INNER JOIN [team_junc] ON [userinfo].[profile_id] = [team_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("te_1") = dr(0).ToString
        End If
        Dim te_id1 As Integer
        te_id1 = Session("te_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [team_junc].[team_d_id] FROM ([userinfo] INNER JOIN [team_junc] ON [userinfo].[profile_id] = [team_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([team_d_id] <> " & te_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("te_2") = dr(0).ToString
        End If
        Dim te_id2 As Integer
        te_id2 = Session("te_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [team_junc].[team_d_id] FROM ([userinfo] INNER JOIN [team_junc] ON [userinfo].[profile_id] = [team_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([team_d_id] <> " & te_id1 & ") AND ([team_d_id] <> " & te_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("te_3") = dr(0).ToString
        End If
        Dim te_id3 As Integer
        te_id3 = Session("te_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [places_junc].[places_d_id] FROM ([userinfo] INNER JOIN [places_junc] ON [userinfo].[profile_id] = [places_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("pl_1") = dr(0).ToString
        End If
        Dim pl_id1 As Integer
        pl_id1 = Session("pl_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [places_junc].[places_d_id] FROM ([userinfo] INNER JOIN [places_junc] ON [userinfo].[profile_id] = [places_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([places_d_id] <> " & pl_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("pl_2") = dr(0).ToString
        End If
        Dim pl_id2 As Integer
        pl_id2 = Session("pl_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [places_junc].[places_d_id] FROM ([userinfo] INNER JOIN [places_junc] ON [userinfo].[profile_id] = [places_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([places_d_id] <> " & pl_id1 & ") AND ([places_d_id] <> " & pl_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("pl_3") = dr(0).ToString
        End If
        Dim pl_id3 As Integer
        pl_id3 = Session("pl_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [int_id] FROM [int_junc] WHERE [profile_id] = " & mysess & "", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("int1") = dr(0).ToString
        End If
        con.Close()
        con.Open()
        Dim intid1 As Integer
        intid1 = Session("int1")
        cmd = New SqlCommand("SELECT [int_id] FROM [int_junc] WHERE (([profile_id] = " & mysess & ") AND ([int_id] <> " & intid1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("int2") = dr(0).ToString
        End If
        con.Close()
        con.Open()
        Dim intid2 As Integer
        intid2 = Session("int2")
        cmd = New SqlCommand("SELECT [int_id] FROM [int_junc] WHERE (([profile_id] = " & mysess & ") AND ([int_id] <> " & intid1 & ") AND ([int_id] <> " & intid2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("int3") = dr(0).ToString
        End If
        Dim intid3 As Integer
        intid3 = Session("int3")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [int_fav_id] FROM [int_fav_junc] WHERE [profile_id] = " & mysess & "", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("intfav1") = dr(0).ToString()
        End If
        con.Close()
        con.Open()
        Dim intfavid1 As Integer
        intfavid1 = Session("intfav1")
        cmd = New SqlCommand("SELECT [int_fav_id] FROM [int_fav_junc] WHERE (([profile_id] = " & mysess & ") AND ([int_fav_id] <> " & intfavid1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("intfav2") = dr(0).ToString()
        End If
        con.Close()
        con.Open()
        Dim intfavid2 As Integer
        intfavid2 = Session("intfav2")
        cmd = New SqlCommand("SELECT [int_fav_id] FROM [int_fav_junc] WHERE (([profile_id] = " & mysess & ") AND ([int_fav_id] <> " & intfavid1 & ") AND ([int_fav_id] <> " & intfavid2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("intfav3") = dr(0).ToString()
        End If
        Dim intfavid3 As Integer
        intfavid3 = Session("intfav3")
        con.Close()



        If (sim_int_pals_check.Checked And sim_fav_pals_check.Checked And feel_pals_txt.Value.Length > 0) Then 'done

            Dim feel As String
            feel = feel_pals_txt.Value
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM ((((((((([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id]=[music_junc].[profile_id]) INNER JOIN [movies_junc] ON [userinfo].[profile_id]=[movies_junc].[profile_id]) INNER JOIN [books_junc] ON [userinfo].[profile_id]=[books_junc].[profile_id]) INNER JOIN [food_junc] ON [userinfo].[profile_id]=[food_junc].[profile_id]) INNER JOIN [tv_junc] ON [userinfo].[profile_id]=[tv_junc].[profile_id]) INNER JOIN [team_junc] ON [userinfo].[profile_id]= [team_junc].[profile_id]) INNER JOIN [places_junc] ON [userinfo].[profile_id]=[places_junc].[profile_id]) LEFT JOIN [int_junc] ON [userinfo].[profile_id] = [int_junc].[profile_id]) LEFT JOIN [int_fav_junc] ON [userinfo].[profile_id] = [int_fav_junc].[profile_id]) WHERE ((([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id2 & ") OR ([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id3 & ") OR ([music_junc].[music_d_id] = " & mu_id2 & " AND [music_junc].[music_d_id] =" & mu_id3 & ")) AND (([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id2 & ") OR ([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") OR ([movies_junc].[movies_d_id] = " & mo_id2 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ")) AND ([books_junc].[books_d_id] = " & bo_id1 & " OR [books_junc].[books_d_id] =" & bo_id2 & " OR [books_junc].[books_d_id] =" & bo_id3 & ") AND (([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id2 & ") OR ([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id3 & ") OR ([food_junc].[food_d_id] = " & fo_id2 & " AND [food_junc].[food_d_id] =" & fo_id3 & ")) AND ([tv_junc].[tv_d_id] = " & tv_id1 & " OR [tv_junc].[tv_d_id] =" & tv_id2 & " OR [tv_junc].[tv_d_id] =" & tv_id3 & ") AND (([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id2 & ") OR ([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id3 & ") OR ([team_junc].[team_d_id] = " & te_id2 & " AND [team_junc].[team_d_id] =" & te_id3 & ")) AND ([places_junc].[places_d_id] = " & pl_id1 & " OR [places_junc].[places_d_id] =" & pl_id2 & " OR [places_junc].[places_d_id] =" & pl_id3 & ") AND (([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid2 & ") OR ([int_junc].[int_id] = " & intid2 & " AND [int_junc].[int_id] =" & intid3 & ") OR ([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid3 & ")) AND ([int_fav_junc].[int_fav_id] = " & intfavid1 & " OR [int_fav_junc].[int_fav_id] =" & intfavid2 & " OR [int_fav_junc].[int_fav_id] =" & intfavid3 & ")   AND ([userinfo].[profile_id] <> " & mysess & ") AND (([userinfo].[mode_1] LIKE @feelt + '%') OR ([userinfo].[mode_2] LIKE @feelt + '%')))", con)
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Phew! That was a tough search. Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo C
            Else
                Dim m As String
                m = "No reults for that. Too ambitious a search? Try again with less specifics."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo C
            End If
        End If
        If (sim_int_pals_check.Checked And sim_fav_pals_check.Checked) Then 'done
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url],[userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM ((((((((([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id]=[music_junc].[profile_id]) INNER JOIN [movies_junc] ON [userinfo].[profile_id]=[movies_junc].[profile_id]) INNER JOIN [books_junc] ON [userinfo].[profile_id]=[books_junc].[profile_id]) INNER JOIN [food_junc] ON [userinfo].[profile_id]=[food_junc].[profile_id]) INNER JOIN [tv_junc] ON [userinfo].[profile_id]=[tv_junc].[profile_id]) INNER JOIN [team_junc] ON [userinfo].[profile_id]= [team_junc].[profile_id]) INNER JOIN [places_junc] ON [userinfo].[profile_id]=[places_junc].[profile_id]) LEFT JOIN [int_junc] ON [userinfo].[profile_id] = [int_junc].[profile_id]) LEFT JOIN [int_fav_junc] ON [userinfo].[profile_id] = [int_fav_junc].[profile_id]) WHERE ((([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id2 & ") OR ([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id3 & ") OR ([music_junc].[music_d_id] = " & mu_id2 & " AND [music_junc].[music_d_id] =" & mu_id3 & ")) AND (([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id2 & ") OR ([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") OR ([movies_junc].[movies_d_id] = " & mo_id2 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ")) AND ([books_junc].[books_d_id] = " & bo_id1 & " OR [books_junc].[books_d_id] =" & bo_id2 & " OR [books_junc].[books_d_id] =" & bo_id3 & ") AND (([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id2 & ") OR ([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id3 & ") OR ([food_junc].[food_d_id] = " & fo_id2 & " AND [food_junc].[food_d_id] =" & fo_id3 & ")) AND ([tv_junc].[tv_d_id] = " & tv_id1 & " OR [tv_junc].[tv_d_id] =" & tv_id2 & " OR [tv_junc].[tv_d_id] =" & tv_id3 & ") AND (([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id2 & ") OR ([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id3 & ") OR ([team_junc].[team_d_id] = " & te_id2 & " AND [team_junc].[team_d_id] =" & te_id3 & ")) AND ([places_junc].[places_d_id] = " & pl_id1 & " OR [places_junc].[places_d_id] =" & pl_id2 & " OR [places_junc].[places_d_id] =" & pl_id3 & ") AND (([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid2 & ") OR ([int_junc].[int_id] = " & intid2 & " AND [int_junc].[int_id] =" & intid3 & ") OR ([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid3 & ")) AND ([int_fav_junc].[int_fav_id] = " & intfavid1 & " OR [int_fav_junc].[int_fav_id] =" & intfavid2 & " OR [int_fav_junc].[int_fav_id] =" & intfavid3 & ")   AND ([userinfo].[profile_id] <> " & mysess & "))", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Lucky for you! That took thinking. Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo C
            Else
                Dim m As String
                m = "Nope. Nada. Nothing. Zilch."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo C
            End If
        End If

        If (sim_fav_pals_check.Checked And feel_pals_txt.Value.Length > 0) Then 'done
            Dim feel As String
            feel = feel_pals_txt.Value
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM ((((((([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id]=[music_junc].[profile_id]) INNER JOIN [movies_junc] ON [userinfo].[profile_id]=[movies_junc].[profile_id]) INNER JOIN [books_junc] ON [userinfo].[profile_id]=[books_junc].[profile_id]) INNER JOIN [food_junc] ON [userinfo].[profile_id]=[food_junc].[profile_id]) INNER JOIN [tv_junc] ON [userinfo].[profile_id]=[tv_junc].[profile_id]) INNER JOIN [team_junc] ON [userinfo].[profile_id]= [team_junc].[profile_id]) INNER JOIN [places_junc] ON [userinfo].[profile_id]=[places_junc].[profile_id]) WHERE ((([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id2 & ") OR ([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id3 & ") OR ([music_junc].[music_d_id] = " & mu_id2 & " AND [music_junc].[music_d_id] =" & mu_id3 & ")) AND (([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id2 & ") OR ([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") OR ([movies_junc].[movies_d_id] = " & mo_id2 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ")) AND ([books_junc].[books_d_id] = " & bo_id1 & " OR [books_junc].[books_d_id] =" & bo_id2 & " OR [books_junc].[books_d_id] =" & bo_id3 & ") AND (([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id2 & ") OR ([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id3 & ") OR ([food_junc].[food_d_id] = " & fo_id2 & " AND [food_junc].[food_d_id] =" & fo_id3 & ")) AND ([tv_junc].[tv_d_id] = " & tv_id1 & " OR [tv_junc].[tv_d_id] =" & tv_id2 & " OR [tv_junc].[tv_d_id] =" & tv_id3 & ") AND (([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id2 & ") OR ([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id3 & ") OR ([team_junc].[team_d_id] = " & te_id2 & " AND [team_junc].[team_d_id] =" & te_id3 & ")) AND ([places_junc].[places_d_id] = " & pl_id1 & " OR [places_junc].[places_d_id] =" & pl_id2 & " OR [places_junc].[places_d_id] =" & pl_id3 & ") AND ([userinfo].[profile_id] <> " & mysess & ")  AND (([userinfo].[mode_1] LIKE @feelt + '%') OR ([userinfo].[mode_2] LIKE @feelt + '%')))", con)
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Tough combo. Here's all I could find."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo C
            Else
                Dim m As String
                m = "Have to yet meet someone like that. Maybe try less specific?"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo C
            End If
        End If
        If (sim_int_pals_check.Checked And feel_pals_txt.Value.Length > 0) Then 'done
            Dim feel As String
            feel = feel_pals_txt.Value
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM (([userinfo] LEFT JOIN [int_junc] ON [userinfo].[profile_id] = [int_junc].[profile_id]) LEFT JOIN [int_fav_junc] ON [userinfo].[profile_id] = [int_fav_junc].[profile_id]) WHERE ((([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid2 & ") OR ([int_junc].[int_id] = " & intid2 & " AND [int_junc].[int_id] =" & intid3 & ") OR ([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid3 & ")) AND ([int_fav_junc].[int_fav_id] = " & intfavid1 & " OR [int_fav_junc].[int_fav_id] =" & intfavid2 & " OR [int_fav_junc].[int_fav_id] =" & intfavid3 & ") AND ([userinfo].[profile_id] <> " & mysess & ")  AND (([userinfo].[mode_1] LIKE @feelt + '%')  OR ([userinfo].[mode_2] LIKE @feelt + '%')))", con)
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Hah. Easy. Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo C
            Else
                Dim m As String
                m = "Kill me. Twas a futile search."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo C
            End If
        End If
        If (sim_int_pals_check.Checked) Then 'done
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM (([userinfo] LEFT JOIN [int_junc] ON [userinfo].[profile_id] = [int_junc].[profile_id]) LEFT JOIN [int_fav_junc] ON [userinfo].[profile_id] = [int_fav_junc].[profile_id]) WHERE ((([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid2 & ") OR ([int_junc].[int_id] = " & intid2 & " AND [int_junc].[int_id] =" & intid3 & ") OR ([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid3 & ")) AND ([int_fav_junc].[int_fav_id] = " & intfavid1 & " OR [int_fav_junc].[int_fav_id] =" & intfavid2 & " OR [int_fav_junc].[int_fav_id] =" & intfavid3 & ") AND ([userinfo].[profile_id] <> " & mysess & "))", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Laddie. There you go!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo C
            Else
                Dim m As String
                m = "Much esoteric? No results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo C
            End If
        End If
        If (sim_fav_pals_check.Checked) Then 'done
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM ((((((([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id]=[music_junc].[profile_id]) INNER JOIN [movies_junc] ON [userinfo].[profile_id]=[movies_junc].[profile_id]) INNER JOIN [books_junc] ON [userinfo].[profile_id]=[books_junc].[profile_id]) INNER JOIN [food_junc] ON [userinfo].[profile_id]=[food_junc].[profile_id]) INNER JOIN [tv_junc] ON [userinfo].[profile_id]=[tv_junc].[profile_id]) INNER JOIN [team_junc] ON [userinfo].[profile_id]= [team_junc].[profile_id]) INNER JOIN [places_junc] ON [userinfo].[profile_id]=[places_junc].[profile_id]) WHERE ((([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id2 & ") OR ([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id3 & ") OR ([music_junc].[music_d_id] = " & mu_id2 & " AND [music_junc].[music_d_id] =" & mu_id3 & ")) AND (([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id2 & ") OR ([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") OR ([movies_junc].[movies_d_id] = " & mo_id2 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ")) AND ([books_junc].[books_d_id] = " & bo_id1 & " OR [books_junc].[books_d_id] =" & bo_id2 & " OR [books_junc].[books_d_id] =" & bo_id3 & ") AND (([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id2 & ") OR ([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id3 & ") OR ([food_junc].[food_d_id] = " & fo_id2 & " AND [food_junc].[food_d_id] =" & fo_id3 & ")) AND ([tv_junc].[tv_d_id] = " & tv_id1 & " OR [tv_junc].[tv_d_id] =" & tv_id2 & " OR [tv_junc].[tv_d_id] =" & tv_id3 & ") AND (([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id2 & ") OR ([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id3 & ") OR ([team_junc].[team_d_id] = " & te_id2 & " AND [team_junc].[team_d_id] =" & te_id3 & ")) AND ([places_junc].[places_d_id] = " & pl_id1 & " OR [places_junc].[places_d_id] =" & pl_id2 & " OR [places_junc].[places_d_id] =" & pl_id3 & ") AND ([userinfo].[profile_id] <> " & mysess & "))", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Almost yous. Good results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo C
            Else
                Dim m As String
                m = "You. Be proud. No results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo C
            End If
        End If
        If (feel_pals_txt.Value.Length > 0) Then 'done

            Dim feel As String
            feel = feel_pals_txt.Value
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM [userinfo] WHERE (([userinfo].[profile_id] <> " & mysess & ") AND (([userinfo].[mode_1] LIKE  @feelt+ '%') OR ([userinfo].[mode_2] LIKE  @feelt + '%')))", con)
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Always good to have someone who feels the same. Here are your results. :3"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo C
            Else
                Dim m As String
                m = "Got nothing. Are you sure that\'s a feeling? :P"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo C
            End If
        End If

C:
    End Sub

    Protected Sub search_friends_nearme(ByVal Sender As Object, ByVal e As EventArgs)
        If feel_pals_txt.Value = "" And sim_fav_pals_check.Checked = False And sim_int_pals_check.Checked = False Then
            Dim m As String
            m = "Please enter at least one parameter!"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            GoTo D
        End If
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))

        con.Open()
        cmd = New SqlCommand("SELECT [city] FROM [userinfo] WHERE [profile_id] = " & mysess & "", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("citysess") = dr(0).ToString()
        End If
        Dim citysess As String
        citysess = Session("citysess")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [music_junc].[music_d_id] FROM ([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id] = [music_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mu_1") = dr(0).ToString
        End If
        Dim mu_id1 As Integer
        mu_id1 = Session("mu_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [music_junc].[music_d_id] FROM ([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id] = [music_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([music_d_id] <> " & mu_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mu_2") = dr(0).ToString
        End If
        Dim mu_id2 As Integer
        mu_id2 = Session("mu_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [music_junc].[music_d_id] FROM ([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id] = [music_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([music_d_id] <> " & mu_id1 & ") AND ([music_d_id] <> " & mu_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mu_3") = dr(0).ToString
        End If
        Dim mu_id3 As Integer
        mu_id3 = Session("mu_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [movies_junc].[movies_d_id] FROM ([userinfo] INNER JOIN [movies_junc] ON [userinfo].[profile_id] = [movies_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mo_1") = dr(0).ToString
        End If
        Dim mo_id1 As Integer
        mo_id1 = Session("mo_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [movies_junc].[movies_d_id] FROM ([userinfo] INNER JOIN [movies_junc] ON [userinfo].[profile_id] = [movies_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([movies_d_id] <> " & mo_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mo_2") = dr(0).ToString
        End If
        Dim mo_id2 As Integer
        mo_id2 = Session("mo_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [movies_junc].[movies_d_id] FROM ([userinfo] INNER JOIN [movies_junc] ON [userinfo].[profile_id] = [movies_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([movies_d_id] <> " & mo_id1 & ") AND ([movies_d_id] <> " & mo_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("mo_3") = dr(0).ToString
        End If
        Dim mo_id3 As Integer
        mo_id3 = Session("mo_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [books_junc].[books_d_id] FROM ([userinfo] INNER JOIN [books_junc] ON [userinfo].[profile_id] = [books_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("bo_1") = dr(0).ToString
        End If
        Dim bo_id1 As Integer
        bo_id1 = Session("bo_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [books_junc].[books_d_id] FROM ([userinfo] INNER JOIN [books_junc] ON [userinfo].[profile_id] = [books_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([books_d_id] <> " & bo_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("bo_2") = dr(0).ToString
        End If
        Dim bo_id2 As Integer
        bo_id2 = Session("bo_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [books_junc].[books_d_id] FROM ([userinfo] INNER JOIN [books_junc] ON [userinfo].[profile_id] = [books_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([books_d_id] <> " & bo_id1 & ") AND ([books_d_id] <> " & bo_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("bo_3") = dr(0).ToString
        End If
        Dim bo_id3 As Integer
        bo_id3 = Session("bo_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [food_junc].[food_d_id] FROM ([userinfo] INNER JOIN [food_junc] ON [userinfo].[profile_id] = [food_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("fo_1") = dr(0).ToString
        End If
        Dim fo_id1 As Integer
        fo_id1 = Session("fo_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [food_junc].[food_d_id] FROM ([userinfo] INNER JOIN [food_junc] ON [userinfo].[profile_id] = [food_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([food_d_id] <> " & fo_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("fo_2") = dr(0).ToString
        End If
        Dim fo_id2 As Integer
        fo_id2 = Session("fo_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [food_junc].[food_d_id] FROM ([userinfo] INNER JOIN [food_junc] ON [userinfo].[profile_id] = [food_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([food_d_id] <> " & fo_id1 & ") AND ([food_d_id] <> " & fo_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("fo_3") = dr(0).ToString
        End If
        Dim fo_id3 As Integer
        fo_id3 = Session("fo_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [tv_junc].[tv_d_id] FROM ([userinfo] INNER JOIN [tv_junc] ON [userinfo].[profile_id] = [tv_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("tv_1") = dr(0).ToString
        End If
        Dim tv_id1 As Integer
        tv_id1 = Session("tv_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [tv_junc].[tv_d_id] FROM ([userinfo] INNER JOIN [tv_junc] ON [userinfo].[profile_id] = [tv_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([tv_d_id] <> " & tv_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("tv_2") = dr(0).ToString
        End If
        Dim tv_id2 As Integer
        tv_id2 = Session("tv_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [tv_junc].[tv_d_id] FROM ([userinfo] INNER JOIN [tv_junc] ON [userinfo].[profile_id] = [tv_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([tv_d_id] <> " & tv_id1 & ") AND ([tv_d_id] <> " & tv_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("tv_3") = dr(0).ToString
        End If
        Dim tv_id3 As Integer
        tv_id3 = Session("tv_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [team_junc].[team_d_id] FROM ([userinfo] INNER JOIN [team_junc] ON [userinfo].[profile_id] = [team_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("te_1") = dr(0).ToString
        End If
        Dim te_id1 As Integer
        te_id1 = Session("te_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [team_junc].[team_d_id] FROM ([userinfo] INNER JOIN [team_junc] ON [userinfo].[profile_id] = [team_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([team_d_id] <> " & te_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("te_2") = dr(0).ToString
        End If
        Dim te_id2 As Integer
        te_id2 = Session("te_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [team_junc].[team_d_id] FROM ([userinfo] INNER JOIN [team_junc] ON [userinfo].[profile_id] = [team_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([team_d_id] <> " & te_id1 & ") AND ([team_d_id] <> " & te_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("te_3") = dr(0).ToString
        End If
        Dim te_id3 As Integer
        te_id3 = Session("te_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [places_junc].[places_d_id] FROM ([userinfo] INNER JOIN [places_junc] ON [userinfo].[profile_id] = [places_junc].[profile_id]) WHERE [userinfo].[profile_id] = " & mysess & " ", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("pl_1") = dr(0).ToString
        End If
        Dim pl_id1 As Integer
        pl_id1 = Session("pl_1")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [places_junc].[places_d_id] FROM ([userinfo] INNER JOIN [places_junc] ON [userinfo].[profile_id] = [places_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([places_d_id] <> " & pl_id1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("pl_2") = dr(0).ToString
        End If
        Dim pl_id2 As Integer
        pl_id2 = Session("pl_2")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [places_junc].[places_d_id] FROM ([userinfo] INNER JOIN [places_junc] ON [userinfo].[profile_id] = [places_junc].[profile_id]) WHERE (([userinfo].[profile_id] = " & mysess & ") AND ([places_d_id] <> " & pl_id1 & ") AND ([places_d_id] <> " & pl_id2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("pl_3") = dr(0).ToString
        End If
        Dim pl_id3 As Integer
        pl_id3 = Session("pl_3")
        con.Close()

        con.Open()
        cmd = New SqlCommand("SELECT [int_id] FROM [int_junc] WHERE [profile_id] = " & mysess & "", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("int1") = dr(0).ToString
        End If
        con.Close()
        con.Open()
        Dim intid1 As Integer
        intid1 = Session("int1")
        cmd = New SqlCommand("SELECT [int_id] FROM [int_junc] WHERE (([profile_id] = " & mysess & ") AND ([int_id] <> " & intid1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("int2") = dr(0).ToString
        End If
        con.Close()
        con.Open()
        Dim intid2 As Integer
        intid2 = Session("int2")
        cmd = New SqlCommand("SELECT [int_id] FROM [int_junc] WHERE (([profile_id] = " & mysess & ") AND ([int_id] <> " & intid1 & ") AND ([int_id] <> " & intid2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("int3") = dr(0).ToString
        End If
        Dim intid3 As Integer
        intid3 = Session("int3")
        con.Close()
        con.Open()
        cmd = New SqlCommand("SELECT [int_fav_id] FROM [int_fav_junc] WHERE [profile_id] = " & mysess & "", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("intfav1") = dr(0).ToString()
        End If
        con.Close()
        con.Open()
        Dim intfavid1 As Integer
        intfavid1 = Session("intfav1")
        cmd = New SqlCommand("SELECT [int_fav_id] FROM [int_fav_junc] WHERE (([profile_id] = " & mysess & ") AND ([int_fav_id] <> " & intfavid1 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("intfav2") = dr(0).ToString()
        End If
        con.Close()
        con.Open()
        Dim intfavid2 As Integer
        intfavid2 = Session("intfav2")
        cmd = New SqlCommand("SELECT [int_fav_id] FROM [int_fav_junc] WHERE (([profile_id] = " & mysess & ") AND ([int_fav_id] <> " & intfavid1 & ") AND ([int_fav_id] <> " & intfavid2 & "))", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Session("intfav3") = dr(0).ToString()
        End If
        Dim intfavid3 As Integer
        intfavid3 = Session("intfav3")
        con.Close()

        If (sim_int_pals_check.Checked And sim_fav_pals_check.Checked And feel_pals_txt.Value.Length > 0) Then 'done

            Dim feel As String
            feel = feel_pals_txt.Value
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM ((((((((([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id]=[music_junc].[profile_id]) INNER JOIN [movies_junc] ON [userinfo].[profile_id]=[movies_junc].[profile_id]) INNER JOIN [books_junc] ON [userinfo].[profile_id]=[books_junc].[profile_id]) INNER JOIN [food_junc] ON [userinfo].[profile_id]=[food_junc].[profile_id]) INNER JOIN [tv_junc] ON [userinfo].[profile_id]=[tv_junc].[profile_id]) INNER JOIN [team_junc] ON [userinfo].[profile_id]= [team_junc].[profile_id]) INNER JOIN [places_junc] ON [userinfo].[profile_id]=[places_junc].[profile_id]) LEFT JOIN [int_junc] ON [userinfo].[profile_id] = [int_junc].[profile_id]) LEFT JOIN [int_fav_junc] ON [userinfo].[profile_id] = [int_fav_junc].[profile_id]) WHERE ((([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id2 & ") OR ([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id3 & ") OR ([music_junc].[music_d_id] = " & mu_id2 & " AND [music_junc].[music_d_id] =" & mu_id3 & ")) AND (([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id2 & ") OR ([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") OR ([movies_junc].[movies_d_id] = " & mo_id2 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") AND ([books_junc].[books_d_id] = " & bo_id1 & " OR [books_junc].[books_d_id] =" & bo_id2 & " OR [books_junc].[books_d_id] =" & bo_id3 & ") AND (([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id2 & ") OR ([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id3 & ") OR ([food_junc].[food_d_id] = " & fo_id2 & " AND [food_junc].[food_d_id] =" & fo_id3 & ")) AND ([tv_junc].[tv_d_id] = " & tv_id1 & " OR [tv_junc].[tv_d_id] =" & tv_id2 & " OR [tv_junc].[tv_d_id] =" & tv_id3 & ") AND (([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id2 & ") OR ([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id3 & ") OR ([team_junc].[team_d_id] = " & te_id2 & " AND [team_junc].[team_d_id] =" & te_id3 & ")) AND ([places_junc].[places_d_id] = " & pl_id1 & " OR [places_junc].[places_d_id] =" & pl_id2 & " OR [places_junc].[places_d_id] =" & pl_id3 & ") AND ([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid2 & ") OR ([int_junc].[int_id] = " & intid2 & " AND [int_junc].[int_id] =" & intid3 & ") OR ([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid3 & ")) AND ([int_fav_junc].[int_fav_id] = " & intfavid1 & " OR [int_fav_junc].[int_fav_id] =" & intfavid2 & " OR [int_fav_junc].[int_fav_id] =" & intfavid3 & ")   AND ([userinfo].[profile_id] <> " & mysess & ") AND (([userinfo].[mode_1] LIKE @feelt + '%') OR ([userinfo].[mode_2] LIKE @feelt + '%')) AND ([userinfo].[city] = '" & citysess & "'))", con)
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Phew! That was a tough search. Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo D
            Else
                Dim m As String
                m = "No reults for that. Too ambitious a search? Try again with less specifics."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo D
            End If
        End If
        If (sim_int_pals_check.Checked And sim_fav_pals_check.Checked) Then 'done
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM ((((((((([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id]=[music_junc].[profile_id]) INNER JOIN [movies_junc] ON [userinfo].[profile_id]=[movies_junc].[profile_id]) INNER JOIN [books_junc] ON [userinfo].[profile_id]=[books_junc].[profile_id]) INNER JOIN [food_junc] ON [userinfo].[profile_id]=[food_junc].[profile_id]) INNER JOIN [tv_junc] ON [userinfo].[profile_id]=[tv_junc].[profile_id]) INNER JOIN [team_junc] ON [userinfo].[profile_id]= [team_junc].[profile_id]) INNER JOIN [places_junc] ON [userinfo].[profile_id]=[places_junc].[profile_id]) LEFT JOIN [int_junc] ON [userinfo].[profile_id] = [int_junc].[profile_id]) LEFT JOIN [int_fav_junc] ON [userinfo].[profile_id] = [int_fav_junc].[profile_id]) WHERE ((([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id2 & ") OR ([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id3 & ") OR ([music_junc].[music_d_id] = " & mu_id2 & " AND [music_junc].[music_d_id] =" & mu_id3 & ")) AND (([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id2 & ") OR ([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") OR ([movies_junc].[movies_d_id] = " & mo_id2 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") AND ([books_junc].[books_d_id] = " & bo_id1 & " OR [books_junc].[books_d_id] =" & bo_id2 & " OR [books_junc].[books_d_id] =" & bo_id3 & ") AND (([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id2 & ") OR ([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id3 & ") OR ([food_junc].[food_d_id] = " & fo_id2 & " AND [food_junc].[food_d_id] =" & fo_id3 & ")) AND ([tv_junc].[tv_d_id] = " & tv_id1 & " OR [tv_junc].[tv_d_id] = " & tv_id2 & " OR [tv_junc].[tv_d_id] = " & tv_id3 & ") AND (([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id2 & ") OR ([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id3 & ") OR ([team_junc].[team_d_id] = " & te_id2 & " AND [team_junc].[team_d_id] =" & te_id3 & ")) AND ([places_junc].[places_d_id] = " & pl_id1 & " OR [places_junc].[places_d_id] =" & pl_id2 & " OR [places_junc].[places_d_id] =" & pl_id3 & ") AND (([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid2 & ") OR ([int_junc].[int_id] = " & intid2 & " AND [int_junc].[int_id] =" & intid3 & ") OR ([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid3 & ")) AND ([int_fav_junc].[int_fav_id] = " & intfavid1 & " OR [int_fav_junc].[int_fav_id] =" & intfavid2 & " OR [int_fav_junc].[int_fav_id] =" & intfavid3 & "))   AND ([userinfo].[profile_id] <> " & mysess & ") AND ([userinfo].[city] = '" & citysess & "'))", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Lucky for you! That took thinking. Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo D
            Else
                Dim m As String
                m = "Nope. Nada. Nothing. Zilch."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo D
            End If
        End If

        If (sim_fav_pals_check.Checked And feel_pals_txt.Value.Length > 0) Then 'done
            Dim feel As String
            feel = feel_pals_txt.Value
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM ((((((([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id]=[music_junc].[profile_id]) INNER JOIN [movies_junc] ON [userinfo].[profile_id]=[movies_junc].[profile_id]) INNER JOIN [books_junc] ON [userinfo].[profile_id]=[books_junc].[profile_id]) INNER JOIN [food_junc] ON [userinfo].[profile_id]=[food_junc].[profile_id]) INNER JOIN [tv_junc] ON [userinfo].[profile_id]=[tv_junc].[profile_id]) INNER JOIN [team_junc] ON [userinfo].[profile_id]= [team_junc].[profile_id]) INNER JOIN [places_junc] ON [userinfo].[profile_id]=[places_junc].[profile_id]) WHERE ((([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id2 & ") OR ([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id3 & ") OR ([music_junc].[music_d_id] = " & mu_id2 & " AND [music_junc].[music_d_id] =" & mu_id3 & ")) AND (([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id2 & ") OR ([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") OR ([movies_junc].[movies_d_id] = " & mo_id2 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ")) AND ([books_junc].[books_d_id] = " & bo_id1 & " OR [books_junc].[books_d_id] =" & bo_id2 & " OR [books_junc].[books_d_id] =" & bo_id3 & ") AND (([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id2 & ") OR ([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id3 & ") OR ([food_junc].[food_d_id] = " & fo_id2 & " AND [food_junc].[food_d_id] =" & fo_id3 & ")) AND ([tv_junc].[tv_d_id] = " & tv_id1 & " OR [tv_junc].[tv_d_id] =" & tv_id2 & " OR [tv_junc].[tv_d_id] =" & tv_id3 & ") AND (([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id2 & ") OR ([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id3 & ") OR ([team_junc].[team_d_id] = " & te_id2 & " AND [team_junc].[team_d_id] =" & te_id3 & ")) AND ([places_junc].[places_d_id] = " & pl_id1 & " OR [places_junc].[places_d_id] =" & pl_id2 & " OR [places_junc].[places_d_id] =" & pl_id3 & ") AND ([userinfo].[profile_id] <> " & mysess & ")  AND (([userinfo].[mode_1] LIKE @feelt + '%') OR ([userinfo].[mode_2] LIKE @feelt + '%')) AND ([userinfo].[city] = '" & citysess & "'))", con)
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Tough combo. Here's all I could find."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo D
            Else
                Dim m As String
                m = "Have to yet meet someone like that. Maybe try less specific?"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo D
            End If
        End If
        If (sim_int_pals_check.Checked And feel_pals_txt.Value.Length > 0) Then 'done
            Dim feel As String
            feel = feel_pals_txt.Value
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM (([userinfo] LEFT JOIN [int_junc] ON [userinfo].[profile_id] = [int_junc].[profile_id]) LEFT JOIN [int_fav_junc] ON [userinfo].[profile_id] = [int_fav_junc].[profile_id]) WHERE ((([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid2 & ") OR ([int_junc].[int_id] = " & intid2 & " AND [int_junc].[int_id] =" & intid3 & ") OR ([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid3 & ")) AND ([int_fav_junc].[int_fav_id] = " & intfavid1 & " OR [int_fav_junc].[int_fav_id] =" & intfavid2 & " OR [int_fav_junc].[int_fav_id] =" & intfavid3 & ") AND ([userinfo].[profile_id] <> " & mysess & ")  AND (([userinfo].[mode_1] LIKE @feelt + '%') OR ([userinfo].[mode_2] LIKE @feelt + '%')) AND ([userinfo].[city] = '" & citysess & "'))", con)
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Hah. Easy. Here are your results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo D
            Else
                Dim m As String
                m = "Kill me. Twas a futile search."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo D
            End If
        End If
        If (sim_int_pals_check.Checked) Then 'done
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM (([userinfo] LEFT JOIN [int_junc] ON [userinfo].[profile_id] = [int_junc].[profile_id]) LEFT JOIN [int_fav_junc] ON [userinfo].[profile_id] = [int_fav_junc].[profile_id]) WHERE ((([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid2 & ") OR ([int_junc].[int_id] = " & intid2 & " AND [int_junc].[int_id] =" & intid3 & ") OR ([int_junc].[int_id] = " & intid1 & " AND [int_junc].[int_id] =" & intid3 & ")) AND ([int_fav_junc].[int_fav_id] = " & intfavid1 & " OR [int_fav_junc].[int_fav_id] =" & intfavid2 & " OR [int_fav_junc].[int_fav_id] =" & intfavid3 & ") AND ([userinfo].[profile_id] <> " & mysess & ") AND ([userinfo].[city] = '" & citysess & "'))", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Laddie. There you go!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo D
            Else
                Dim m As String
                m = "Much esoteric? No results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo D
            End If
        End If
        If (sim_fav_pals_check.Checked) Then 'done
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM ((((((([userinfo] INNER JOIN [music_junc] ON [userinfo].[profile_id]=[music_junc].[profile_id]) INNER JOIN [movies_junc] ON [userinfo].[profile_id]=[movies_junc].[profile_id]) INNER JOIN [books_junc] ON [userinfo].[profile_id]=[books_junc].[profile_id]) INNER JOIN [food_junc] ON [userinfo].[profile_id]=[food_junc].[profile_id]) INNER JOIN [tv_junc] ON [userinfo].[profile_id]=[tv_junc].[profile_id]) INNER JOIN [team_junc] ON [userinfo].[profile_id]= [team_junc].[profile_id]) INNER JOIN [places_junc] ON [userinfo].[profile_id]=[places_junc].[profile_id]) WHERE ((([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id2 & ") OR ([music_junc].[music_d_id] = " & mu_id1 & " AND [music_junc].[music_d_id] =" & mu_id3 & ") OR ([music_junc].[music_d_id] = " & mu_id2 & " AND [music_junc].[music_d_id] =" & mu_id3 & ")) AND (([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id2 & ") OR ([movies_junc].[movies_d_id] = " & mo_id1 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ") OR ([movies_junc].[movies_d_id] = " & mo_id2 & " AND [movies_junc].[movies_d_id] =" & mo_id3 & ")) AND ([books_junc].[books_d_id] = " & bo_id1 & " OR [books_junc].[books_d_id] =" & bo_id2 & " OR [books_junc].[books_d_id] =" & bo_id3 & ") AND (([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id2 & ") OR ([food_junc].[food_d_id] = " & fo_id1 & " AND [food_junc].[food_d_id] =" & fo_id3 & ") OR ([food_junc].[food_d_id] = " & fo_id2 & " AND [food_junc].[food_d_id] =" & fo_id3 & ")) AND ([tv_junc].[tv_d_id] = " & tv_id1 & " OR [tv_junc].[tv_d_id] =" & tv_id2 & " OR [tv_junc].[tv_d_id] =" & tv_id3 & ") AND (([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id2 & ") OR ([team_junc].[team_d_id] = " & te_id1 & " AND [team_junc].[team_d_id] =" & te_id3 & ") OR ([team_junc].[team_d_id] = " & te_id2 & " AND [team_junc].[team_d_id] =" & te_id3 & ")) AND ([places_junc].[places_d_id] = " & pl_id1 & " OR [places_junc].[places_d_id] =" & pl_id2 & " OR [places_junc].[places_d_id] =" & pl_id3 & ") AND ([userinfo].[profile_id] <> " & mysess & ") AND ([userinfo].[city] = '" & citysess & "'))", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Almost yous. Good results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo D
            Else
                Dim m As String
                m = "You. Be proud. No results."
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo D
            End If
        End If
        If (feel_pals_txt.Value.Length > 0) Then 'done
            Dim feel As String
            feel = feel_pals_txt.Value
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[dp_url], [userinfo].[profile_id], [userinfo].[uname], [userinfo].[fname], [userinfo].[lname], [userinfo].[city], [userinfo].[about_me] FROM [userinfo] WHERE (([userinfo].[profile_id] <> " & mysess & ") AND (([userinfo].[mode_1] LIKE @feelt + '%') OR ([userinfo].[mode_2] LIKE @feelt + '%')) AND ([userinfo].[city] = '" & citysess & "'))", con)
            cmd.Parameters.Add("@feelt", SqlDbType.NVarChar)
            cmd.Parameters("@feelt").Value = feel.Replace("'", "''").ToString()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim m As String
                m = "Always good to have someone who feels the same. Here are your results. :3"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

                con.Close()
                con.Open()
                s_post_fill.Visible = False
                s_cows_fill.Visible = True
                s_cows_fill.DataSource = cmd.ExecuteReader()
                s_cows_fill.DataBind()
                con.Close()
                GoTo D
            Else
                Dim m As String
                m = "Got nothing. Are you sure that's a feeling? :P"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

                s_post_fill.Visible = True
                s_cows_fill.Visible = False
                con.Close()
                GoTo D
            End If
        End If

D:
    End Sub

    Protected Sub comment_post_pyl(ByVal Sender As Object, ByVal e As CommandEventArgs)
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
        For Each rItem As RepeaterItem In pyl_fill.Items
            txtBox = DirectCast(rItem.FindControl("Text1_pyl"), HtmlInputText)
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
        For Each rItem As RepeaterItem In pyl_fill.Items()
            Dim subRepeter As Repeater = DirectCast(rItem.FindControl("Repeater2_pyl"), Repeater)
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
        cmd = New SqlCommand("SELECT * FROM activity WHERE ([doer]=" & mysess & " AND [type]='liked a' AND [post]=" & wrisess & ")", con)
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
        For Each rItem As RepeaterItem In s_post_fill.Items
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
        For Each rItem As RepeaterItem In s_post_fill.Items()
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

        For Each rItem As RepeaterItem In s_post_fill.Items
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

    Protected Sub delcom_pyl(ByVal Sender As Object, ByVal e As CommandEventArgs)
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

        For Each rItem As RepeaterItem In pyl_fill.Items
            Dim subRepeter As Repeater = DirectCast(rItem.FindControl("Repeater2_pyl"), Repeater)
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

    Protected Sub s_post_fill_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles s_post_fill.ItemDataBound
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

    Protected Sub pyl_fill_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles pyl_fill.ItemDataBound
        Dim subRepeter As Repeater = DirectCast(e.Item.FindControl("Repeater2_pyl"), Repeater)
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



