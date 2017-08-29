Imports System.Data
Imports System.IO
imports system.data.sqlclient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web

Public Class _Default
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

        con.Open()
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        cmd2 = New sqlcommand("SELECT [sex] from userinfo where [profile_id]=" & mysess & "", con)
        dr = cmd2.ExecuteReader()
        If dr.Read Then
            Session("sex") = dr("sex")
            If Session("sex") = "male" Then

                Session("sexnum") = "him"
            Else

                Session("sexnum") = "her"
            End If
        End If
        con.Close()

       

        If Page.IsPostBack = False Then
        
            con.Open()
            cmd = New SqlCommand("SELECT * FROM [writes] WHERE ([published] = 1 AND [profile_id] = " & mysess & ") ORDER BY [date_written] DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()

                Repeater9.DataSource = cmd.ExecuteReader()
                Repeater9.DataBind()
                Repeater9.Visible = True

            End If
            con.Close()

            con.Open()

            cmd = New sqlcommand(" SELECT * FROM [userinfo] WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()

                Repeater10.DataSource = cmd.ExecuteReader()
                Repeater10.DataBind()
                Repeater10.Visible = True

            End If
            con.Close()

            con.Open()
            cmd = New sqlcommand("SELECT * FROM ([music_data] INNER JOIN [music_junc] ON [music_data].[music_d_id]=[music_junc].[music_d_id]) WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                music_rep.DataSource = cmd.ExecuteReader()
                music_rep.DataBind()
            End If
            con.Close()

            con.Open()
            cmd = New sqlcommand("SELECT * FROM ([movies_data] INNER JOIN [movies_junc] ON [movies_data].[movies_d_id]=[movies_junc].[movies_d_id]) WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                movies_rep.DataSource = cmd.ExecuteReader()
                movies_rep.DataBind()
            End If
            con.Close()

            con.Open()
            cmd = New sqlcommand("SELECT * FROM ([books_data] INNER JOIN [books_junc] ON [books_data].[books_d_id]=[books_junc].[books_d_id]) WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                books_rep.DataSource = cmd.ExecuteReader()
                books_rep.DataBind()
            End If
            con.Close()

            con.Open()
            cmd = New sqlcommand("SELECT * FROM ([food_data] INNER JOIN [food_junc] ON [food_data].[food_d_id]=[food_junc].[food_d_id]) WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                food_rep.DataSource = cmd.ExecuteReader()
                food_rep.DataBind()
            End If
            con.Close()

            con.Open()
            cmd = New sqlcommand("SELECT * FROM ([tv_data] INNER JOIN [tv_junc] ON [tv_data].[tv_d_id]=[tv_junc].[tv_d_id]) WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                tv_rep.DataSource = cmd.ExecuteReader()
                tv_rep.DataBind()
            End If
            con.Close()

            con.Open()
            cmd = New sqlcommand("SELECT * FROM ([team_data] INNER JOIN [team_junc] ON [team_data].[team_d_id]=[team_junc].[team_d_id]) WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                teams_rep.DataSource = cmd.ExecuteReader()
                teams_rep.DataBind()
            End If
            con.Close()

            con.Open()
            cmd = New sqlcommand("SELECT * FROM ([places_data] INNER JOIN [places_junc] ON [places_data].[places_d_id]=[places_junc].[places_d_id]) WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                places_rep.DataSource = cmd.ExecuteReader()
                places_rep.DataBind()
            End If
            con.Close()

            con.Open()
            cmd = New sqlcommand("SELECT * FROM ([int_fav_junc] INNER JOIN [int_fav] ON [int_fav_junc].[int_fav_id]=[int_fav].[int_fav_id]) WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                hobb_rep.DataSource = cmd.ExecuteReader()
                hobb_rep.DataBind()
            End If
            con.Close()

            con.Open()
            cmd = New sqlcommand("SELECT * FROM ([int_junc] INNER JOIN [int] ON [int_junc].[int_id]=[int].[int_id]) WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                int_rep.DataSource = cmd.ExecuteReader()
                int_rep.DataBind()
            End If
            con.Close()



        End If
    End Sub

    Protected Sub changemu(ByVal Sender As Object, ByVal e As EventArgs)
        Dim m As String
        m = "Your Music favourites have been updated."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In music_rep.Items
            Dim ftxtBox As HtmlInputText = DirectCast(rItem.FindControl("Text7"), HtmlInputText)
            Dim mupriid As Label = DirectCast(rItem.FindControl("label_music"), Label)
            con.Open()
            cmd = New SqlCommand("SELECT [music_d_id] FROM music_data WHERE ([m_name] = @munewinp)", con)
            cmd.Parameters.Add("@munewinp", SqlDbType.NVarChar)
            cmd.Parameters("@munewinp").Value = ftxtBox.Value.Replace("'", "''")

            dr = cmd.ExecuteReader()
            If (dr.Read) Then
                Dim m_id As Integer = Convert.ToInt32(dr("music_d_id"))
                dr.Close()
                con.Close()
                con.Open()
                cmd = New sqlcommand("UPDATE [music_junc] SET [music_d_id]=" & m_id & " WHERE ([m_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                dr.Close()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO music_data(m_name) VALUES(@crmunewinp)", con)
                cmd.Parameters.Add("@crmunewinp", SqlDbType.NVarChar)
                cmd.Parameters("@crmunewinp").Value = ftxtBox.Value.Replace("'", "''")

                'new non-existing value now inserted'
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Dim new_m_id As Integer
                con.Open()
                'take id of new value inserted previously'
                cmd = New SqlCommand("SELECT [music_d_id] FROM music_data WHERE ([m_name] = @fetcrmunewinp)", con)
                cmd.Parameters.Add("@fetcrmunewinp", SqlDbType.NVarChar)
                cmd.Parameters("@fetcrmunewinp").Value = ftxtBox.Value.Replace("'", "''")
                dr = cmd.ExecuteReader()
                If (dr.Read) Then
                    Session("newmuid") = dr(0).ToString()
                    dr.Close()
                    new_m_id = Convert.ToInt32(Session("newmuid"))
                    'taken'
                    con.Close()
                End If
                'now update the new ids in the junction
                con.Open()
                cmd = New SqlCommand("UPDATE [music_junc] SET [music_d_id]=" & new_m_id & " WHERE ([m_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            End If
        Next
    End Sub

    Protected Sub changemo(ByVal Sender As Object, ByVal e As EventArgs)
        Dim m As String
        m = "Your Movies favourites have been updated."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In movies_rep.Items
            Dim ftxtBox As HtmlInputText = DirectCast(rItem.FindControl("Text7"), HtmlInputText)
            Dim mupriid As Label = DirectCast(rItem.FindControl("label_movies"), Label)

            con.Open()
            cmd = New SqlCommand("SELECT [movies_d_id] FROM movies_data WHERE ([d_name] = @monewinp)", con)
            cmd.Parameters.Add("@monewinp", SqlDbType.NVarChar)
            cmd.Parameters("@monewinp").Value = ftxtBox.Value.Replace("'", "''")

            dr = cmd.ExecuteReader()
            If (dr.Read) Then
                Dim m_id As Integer = Convert.ToInt32(dr("movies_d_id"))
                dr.Close()
                con.Close()
                con.Open()
                cmd = New sqlcommand("UPDATE [movies_junc] SET [movies_d_id]=" & m_id & " WHERE ([mo_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                dr.Close()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO movies_data(d_name) VALUES(@crmonewinp)", con)
                cmd.Parameters.Add("@crmonewinp", SqlDbType.NVarChar)
                cmd.Parameters("@crmonewinp").Value = ftxtBox.Value.Replace("'", "''")
                'new non-existing value now inserted'
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Dim new_m_id As Integer
                con.Open()
                'take id of new value inserted previously'
                cmd = New SqlCommand("SELECT [movies_d_id] FROM movies_data WHERE ([d_name] = @fetcrmonewinp)", con)
                cmd.Parameters.Add("@fetcrmonewinp", SqlDbType.NVarChar)
                cmd.Parameters("@fetcrmonewinp").Value = ftxtBox.Value.Replace("'", "''")
                dr = cmd.ExecuteReader()
                If (dr.Read) Then
                    Session("newmuid") = dr(0).ToString()
                    dr.Close()
                    new_m_id = Convert.ToInt32(Session("newmuid"))
                    'taken'
                    con.Close()
                End If
                'now update the new ids in the junction
                con.Open()
                cmd = New SqlCommand("UPDATE [movies_junc] SET [movies_d_id]=" & new_m_id & " WHERE ([mo_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            End If
        Next
    End Sub

    Protected Sub changebo(ByVal Sender As Object, ByVal e As EventArgs)
        Dim m As String
        m = "Your Books favourites have been updated."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In books_rep.Items
            Dim ftxtBox As HtmlInputText = DirectCast(rItem.FindControl("Text7"), HtmlInputText)
            Dim mupriid As Label = DirectCast(rItem.FindControl("label_books"), Label)

            con.Open()
            cmd = New SqlCommand("SELECT [books_d_id] FROM books_data WHERE ([d_name] = @bonewinp)", con)
            cmd.Parameters.Add("@bonewinp", SqlDbType.NVarChar)
            cmd.Parameters("@bonewinp").Value = ftxtBox.Value.Replace("'", "''")

            dr = cmd.ExecuteReader()
            If (dr.Read) Then
                Dim m_id As Integer = Convert.ToInt32(dr("books_d_id"))
                dr.Close()
                con.Close()
                con.Open()
                cmd = New sqlcommand("UPDATE [books_junc] SET [books_d_id]=" & m_id & " WHERE ([bo_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                dr.Close()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO books_data(d_name) VALUES(@crbonewinp)", con)
                cmd.Parameters.Add("@crbonewinp", SqlDbType.NVarChar)
                cmd.Parameters("@crbonewinp").Value = ftxtBox.Value.Replace("'", "''")

                'new non-existing value now inserted'
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Dim new_m_id As Integer
                con.Open()
                'take id of new value inserted previously'
                cmd = New SqlCommand("SELECT [books_d_id] FROM books_data WHERE ([d_name] = @fetcrbonewinp)", con)
                cmd.Parameters.Add("@fetcrbonewinp", SqlDbType.NVarChar)
                cmd.Parameters("@fetcrbonewinp").Value = ftxtBox.Value.Replace("'", "''")
                dr = cmd.ExecuteReader()
                If (dr.Read) Then
                    Session("newmuid") = dr(0).ToString()
                    dr.Close()
                    new_m_id = Convert.ToInt32(Session("newmuid"))
                    'taken'
                    con.Close()
                End If
                'now update the new ids in the junction
                con.Open()
                cmd = New SqlCommand("UPDATE [books_junc] SET [books_d_id]=" & new_m_id & " WHERE ([bo_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            End If
        Next
    End Sub

    Protected Sub changefo(ByVal Sender As Object, ByVal e As EventArgs)
        Dim m As String
        m = "Your Food favourites have been updated."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In food_rep.Items
            Dim ftxtBox As HtmlInputText = DirectCast(rItem.FindControl("Text7"), HtmlInputText)
            Dim mupriid As Label = DirectCast(rItem.FindControl("label_food"), Label)

            con.Open()
            cmd = New SqlCommand("SELECT [food_d_id] FROM food_data WHERE ([d_name] = @fonewinp)", con)
            cmd.Parameters.Add("@fonewinp", SqlDbType.NVarChar)
            cmd.Parameters("@fonewinp").Value = ftxtBox.Value.Replace("'", "''")

            dr = cmd.ExecuteReader()
            If (dr.Read) Then
                Dim m_id As Integer = Convert.ToInt32(dr("food_d_id"))
                dr.Close()
                con.Close()
                con.Open()
                cmd = New sqlcommand("UPDATE [food_junc] SET [food_d_id]=" & m_id & " WHERE ([fo_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                dr.Close()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO food_data(d_name) VALUES(@crfonewinp)", con)
                cmd.Parameters.Add("@crfonewinp", SqlDbType.NVarChar)
                cmd.Parameters("@crfonewinp").Value = ftxtBox.Value.Replace("'", "''")

                'new non-existing value now inserted'
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Dim new_m_id As Integer
                con.Open()
                'take id of new value inserted previously'
                cmd = New SqlCommand("SELECT [food_d_id] FROM food_data WHERE ([d_name] = @fetcrfonewinp)", con)
                cmd.Parameters.Add("@fetcrfonewinp", SqlDbType.NVarChar)
                cmd.Parameters("@fetcrfonewinp").Value = ftxtBox.Value.Replace("'", "''")

                dr = cmd.ExecuteReader()
                If (dr.Read) Then
                    Session("newmuid") = dr(0).ToString()
                    dr.Close()
                    new_m_id = Convert.ToInt32(Session("newmuid"))
                    'taken'
                    con.Close()
                End If
                'now update the new ids in the junction
                con.Open()
                cmd = New SqlCommand("UPDATE [food_junc] SET [food_d_id]=" & new_m_id & " WHERE ([fo_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            End If
        Next
    End Sub

    Protected Sub changetv(ByVal Sender As Object, ByVal e As EventArgs)
        Dim m As String
        m = "Your TV favourites have been updated."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In tv_rep.Items
            Dim ftxtBox As HtmlInputText = DirectCast(rItem.FindControl("Text7"), HtmlInputText)
            Dim mupriid As Label = DirectCast(rItem.FindControl("label_tv"), Label)

            con.Open()
            cmd = New SqlCommand("SELECT [tv_d_id] FROM tv_data WHERE ([tv_d] = @tvnewinp)", con)
            cmd.Parameters.Add("@tvnewinp", SqlDbType.NVarChar)
            cmd.Parameters("@tvnewinp").Value = ftxtBox.Value.Replace("'", "''")

            dr = cmd.ExecuteReader()
            If (dr.Read) Then
                Dim m_id As Integer = Convert.ToInt32(dr("tv_d_id"))
                dr.Close()
                con.Close()
                con.Open()
                cmd = New sqlcommand("UPDATE [tv_junc] SET [tv_d_id]=" & m_id & " WHERE ([tv_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                dr.Close()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO tv_data(tv_d) VALUES(@crtvnewinp)", con)
                cmd.Parameters.Add("@crtvnewinp", SqlDbType.NVarChar)
                cmd.Parameters("@crtvnewinp").Value = ftxtBox.Value.Replace("'", "''")

                'new non-existing value now inserted'
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Dim new_m_id As Integer
                con.Open()
                'take id of new value inserted previously'
                cmd = New SqlCommand("SELECT [tv_d_id] FROM tv_data WHERE ([tv_d] = @fetcrtvnewinp)", con)
                cmd.Parameters.Add("@fetcrtvnewinp", SqlDbType.NVarChar)
                cmd.Parameters("@fetcrtvnewinp").Value = ftxtBox.Value.Replace("'", "''")

                dr = cmd.ExecuteReader()
                If (dr.Read) Then
                    Session("newmuid") = dr(0).ToString()
                    dr.Close()
                    new_m_id = Convert.ToInt32(Session("newmuid"))
                    'taken'
                    con.Close()
                End If
                'now update the new ids in the junction
                con.Open()
                cmd = New SqlCommand("UPDATE [tv_junc] SET [tv_d_id]=" & new_m_id & " WHERE ([tv_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            End If
        Next
    End Sub

    Protected Sub changeteams(ByVal Sender As Object, ByVal e As EventArgs)
        Dim m As String
        m = "Your Team favourites have been updated."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In teams_rep.Items
            Dim ftxtBox As HtmlInputText = DirectCast(rItem.FindControl("Text7"), HtmlInputText)
            Dim mupriid As Label = DirectCast(rItem.FindControl("label_te"), Label)

            con.Open()
            cmd = New SqlCommand("SELECT [team_d_id] FROM team_data WHERE ([team_d] = @tenewinp)", con)
            cmd.Parameters.Add("@tenewinp", SqlDbType.NVarChar)
            cmd.Parameters("@tenewinp").Value = ftxtBox.Value.Replace("'", "''")

            dr = cmd.ExecuteReader()
            If (dr.Read) Then
                Dim m_id As Integer = Convert.ToInt32(dr("team_d_id"))
                dr.Close()
                con.Close()
                con.Open()
                cmd = New sqlcommand("UPDATE [team_junc] SET [team_d_id]=" & m_id & " WHERE ([te_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                dr.Close()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO team_data(team_d) VALUES(@crtenewinp)", con)
                cmd.Parameters.Add("@crtenewinp", SqlDbType.NVarChar)
                cmd.Parameters("@crtenewinp").Value = ftxtBox.Value.Replace("'", "''")

                'new non-existing value now inserted'
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Dim new_m_id As Integer
                con.Open()
                'take id of new value inserted previously'
                cmd = New SqlCommand("SELECT [team_d_id] FROM team_data WHERE ([team_d] = @fetcrtenewinp)", con)
                cmd.Parameters.Add("@fetcrtenewinp", SqlDbType.NVarChar)
                cmd.Parameters("@fetcrtenewinp").Value = ftxtBox.Value.Replace("'", "''")

                dr = cmd.ExecuteReader()
                If (dr.Read) Then
                    Session("newmuid") = dr(0).ToString()
                    dr.Close()
                    new_m_id = Convert.ToInt32(Session("newmuid"))
                    'taken'
                    con.Close()
                End If
                'now update the new ids in the junction
                con.Open()
                cmd = New SqlCommand("UPDATE [team_junc] SET [team_d_id]=" & new_m_id & " WHERE ([te_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            End If
        Next
    End Sub

    Protected Sub changeplaces(ByVal Sender As Object, ByVal e As EventArgs)
        Dim m As String
        m = "Your Places favourites have been updated."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In places_rep.Items
            Dim ftxtBox As HtmlInputText = DirectCast(rItem.FindControl("Text7"), HtmlInputText)
            Dim mupriid As Label = DirectCast(rItem.FindControl("label_pl"), Label)

            con.Open()
            cmd = New SqlCommand("SELECT [places_d_id] FROM places_data WHERE ([places_d] = @plnewinp)", con)
            cmd.Parameters.Add("@plnewinp", SqlDbType.NVarChar)
            cmd.Parameters("@plnewinp").Value = ftxtBox.Value.Replace("'", "''")

            dr = cmd.ExecuteReader()
            If (dr.Read) Then
                Dim m_id As Integer = Convert.ToInt32(dr("places_d_id"))
                dr.Close()
                con.Close()
                con.Open()
                cmd = New sqlcommand("UPDATE [places_junc] SET [places_d_id]=" & m_id & " WHERE ([pl_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                dr.Close()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO places_data(places_d) VALUES(@crplnewinp)", con)
                cmd.Parameters.Add("@crplnewinp", SqlDbType.NVarChar)
                cmd.Parameters("@crplnewinp").Value = ftxtBox.Value.Replace("'", "''")
                'new non-existing value now inserted'
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Dim new_m_id As Integer
                con.Open()
                'take id of new value inserted previously'
                cmd = New SqlCommand("SELECT [places_d_id] FROM places_data WHERE ([places_d] = @fetcrplnewinp)", con)
                cmd.Parameters.Add("@fetcrplnewinp", SqlDbType.NVarChar)
                cmd.Parameters("@fetcrplnewinp").Value = ftxtBox.Value.Replace("'", "''")
                dr = cmd.ExecuteReader()
                If (dr.Read) Then
                    Session("newmuid") = dr(0).ToString()
                    dr.Close()
                    new_m_id = Convert.ToInt32(Session("newmuid"))
                    'taken'
                    con.Close()
                End If
                'now update the new ids in the junction
                con.Open()
                cmd = New SqlCommand("UPDATE [places_junc] SET [places_d_id]=" & new_m_id & " WHERE ([pl_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            End If
        Next
    End Sub

    Protected Sub changehobbs(ByVal Sender As Object, ByVal e As EventArgs)
        Dim m As String
        m = "Your Hobbies have been updated."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In hobb_rep.Items
            Dim ftxtBox As HtmlInputText = DirectCast(rItem.FindControl("Text7"), HtmlInputText)
            Dim mupriid As Label = DirectCast(rItem.FindControl("label_hobbies"), Label)

            con.Open()
            cmd = New SqlCommand("SELECT [int_fav_id] FROM int_fav WHERE ([int_fav] = @hobnewinp)", con)
            cmd.Parameters.Add("@hobnewinp", SqlDbType.NVarChar)
            cmd.Parameters("@hobnewinp").Value = ftxtBox.Value.Replace("'", "''")
            dr = cmd.ExecuteReader()
            If (dr.Read) Then
                Dim m_id As Integer = Convert.ToInt32(dr("int_fav_id"))
                dr.Close()
                con.Close()
                con.Open()
                cmd = New sqlcommand("UPDATE [int_fav_junc] SET [int_fav_id]=" & m_id & " WHERE ([fav_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                dr.Close()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO int_fav(int_fav) VALUES(@crhobnewinp)", con)
                cmd.Parameters.Add("@crhobnewinp", SqlDbType.NVarChar)
                cmd.Parameters("@crhobnewinp").Value = ftxtBox.Value.Replace("'", "''")
                'new non-existing value now inserted'
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Dim new_m_id As Integer
                con.Open()
                'take id of new value inserted previously'
                cmd = New SqlCommand("SELECT [int_fav_id] FROM int_fav WHERE ([int_fav] = @fetcrhobnewinp)", con)
                cmd.Parameters.Add("@fetcrhobnewinp", SqlDbType.NVarChar)
                cmd.Parameters("@fetcrhobnewinp").Value = ftxtBox.Value.Replace("'", "''")

                dr = cmd.ExecuteReader()
                If (dr.Read) Then
                    Session("newmuid") = dr(0).ToString()
                    dr.Close()
                    new_m_id = Convert.ToInt32(Session("newmuid"))
                    'taken'
                    con.Close()
                End If
                'now update the new ids in the junction
                con.Open()
                cmd = New SqlCommand("UPDATE [int_fav_junc] SET [int_fav_id]=" & new_m_id & " WHERE ([fav_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            End If
        Next
    End Sub

    Protected Sub changeint(ByVal Sender As Object, ByVal e As EventArgs)
        Dim m As String
        m = "Your Interests have been updated."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In int_rep.Items
            Dim ftxtBox As HtmlInputText = DirectCast(rItem.FindControl("Text7"), HtmlInputText)
            Dim mupriid As Label = DirectCast(rItem.FindControl("label_int"), Label)

            con.Open()
            cmd = New SqlCommand("SELECT [int_id] FROM [int] WHERE ([int_name] = @innewinp)", con)
            cmd.Parameters.Add("@innewinp", SqlDbType.NVarChar)
            cmd.Parameters("@innewinp").Value = ftxtBox.Value.Replace("'", "''")

            dr = cmd.ExecuteReader()
            If (dr.Read) Then
                Dim m_id As Integer = Convert.ToInt32(dr("int_id"))
                dr.Close()
                con.Close()
                con.Open()
                cmd = New sqlcommand("UPDATE [int_junc] SET [int_id]=" & m_id & " WHERE ([in_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                dr.Close()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO [int](int_name) VALUES(@crinnewinp)", con)
                cmd.Parameters.Add("@crinnewinp", SqlDbType.NVarChar)
                cmd.Parameters("@crinnewinp").Value = ftxtBox.Value.Replace("'", "''")
                'new non-existing value now inserted'
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Dim new_m_id As Integer
                con.Open()
                'take id of new value inserted previously'
                cmd = New SqlCommand("SELECT [int_id] FROM [int] WHERE ([int_name] = @fetcrinnewinp)", con)
                cmd.Parameters.Add("@fetcrinnewinp", SqlDbType.NVarChar)
                cmd.Parameters("@fetcrinnewinp").Value = ftxtBox.Value.Replace("'", "''")
                dr = cmd.ExecuteReader()
                If (dr.Read) Then
                    Session("newmuid") = dr(0).ToString()
                    dr.Close()
                    new_m_id = Convert.ToInt32(Session("newmuid"))
                    'taken'
                    con.Close()
                End If
                'now update the new ids in the junction
                con.Open()
                cmd = New SqlCommand("UPDATE [int_junc] SET [int_id]=" & new_m_id & " WHERE ([in_primary] = " & Convert.ToInt32(mupriid.Text) & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            End If
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
        cmd = New sqlcommand("SELECT * FROM activity WHERE ([doer]=" & mysess & " AND [type]='liked a' AND [post]=" & wrisess & ")", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Response.Write("You've already liked this post.")
        Else
            cmd = New sqlcommand("INSERT INTO activity(doer, type, post, date_time_written) VALUES('" & mysess & "','liked a', '" & wrisess & "', '" & intDay & "')", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            con.Close()
        End If
    End Sub

    Protected Sub changestatus(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim txtBox As HtmlInputText
        Dim stattxta As String = ""
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        For Each rItem As RepeaterItem In Repeater10.Items
            txtBox = DirectCast(rItem.FindControl("stattext"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    stattxta = txtBox.Value
                    con.Open()
                    cmd = New SqlCommand("UPDATE userinfo SET [about_me] = @stat WHERE ([profile_id] = " & mysess & ")", con)
                    cmd.Parameters.Add("@stat", SqlDbType.NVarChar)
                    cmd.Parameters("@stat").Value = stattxta.Replace("'", "''").ToString()
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                End If

            End If
        Next
        If Page.IsPostBack = True Then

            con.Open()
            cmd = New sqlcommand("SELECT * FROM [userinfo] WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()
                Repeater10.DataSource = cmd.ExecuteReader()
                Repeater10.DataBind()
                Repeater10.Visible = True
            End If
            con.Close()
        End If
    End Sub

    Protected Sub txtSearch_KeyUp(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("SELECT * FROM [writes] WHERE ([published] = 1 AND [profile_id] = " & mysess & " AND (([content] LIKE '%' + @searchstr + '%') OR ([hashes] LIKE '%' + @searchstr + '%') OR ([feeling_type] LIKE '%' + @searchstr + '%'))) ORDER BY [date_written] DESC", con)
        cmd.Parameters.Add("@searchstr", SqlDbType.NVarChar)
        cmd.Parameters("@searchstr").Value = seareq.Text.Replace("'", "''").ToString()
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            Repeater9.Visible = True
            Repeater9.DataSource = cmd.ExecuteReader()
            Repeater9.DataBind()
        Else
            Repeater9.Visible = False
        End If
        con.Close()
    End Sub

    Protected Sub btn_upload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
       
        If FileUpload1.HasFile Then
            FileUpload1.SaveAs(Server.MapPath("img\" + FileUpload1.FileName))
            Dim mysess As Integer
            mysess = Convert.ToInt32(Session("idsess"))
            con.Open()
            cmd = New sqlcommand("UPDATE userinfo SET [dp_url] = '" & "img\" + FileUpload1.FileName & "' WHERE ([profile_id] = " & mysess & ")", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            con.Close()
            Response.Redirect("MyProfile.aspx")
        Else
            Response.Write("Well, you could select a file first, perhaps?")
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
        For Each rItem As RepeaterItem In Repeater9.Items()
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
        For Each rItem As RepeaterItem In Repeater9.Items()
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

        For Each rItem As RepeaterItem In Repeater9.Items
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

    Sub unpub(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim m As String
        m = "Unpublished from Openbook."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("possess") = value1
        Dim poswri As Integer
        poswri = Convert.ToInt32(Session("possess"))
        con.Open()
        cmd = New SqlCommand("UPDATE [writes] SET [published] = 0 WHERE [writes_id] = " & poswri & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        If Page.IsPostBack = True Then
            Dim mysess As Integer
            mysess = Convert.ToInt32(Session("idsess"))
            con.Open()
            cmd = New SqlCommand("SELECT * FROM [writes] WHERE ([published] = 1 AND [profile_id] = " & mysess & ") ORDER BY [date_written] DESC", con)
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()

                Repeater9.DataSource = cmd.ExecuteReader()
                Repeater9.DataBind()
                Repeater9.Visible = True

            End If
            con.Close()
        End If
    End Sub

    Protected Sub Repeater9_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater9.ItemDataBound
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
