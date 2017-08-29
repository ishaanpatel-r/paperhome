Imports System.Data
Imports System.Data.SqlClient

Public Class _Default
    Inherits System.Web.UI.Page
    Dim con As sqlConnection
    Dim cmd As sqlCommand
    Dim cmd2 As sqlCommand
    Dim cmd3 As sqlCommand
    Dim cmd4 As sqlCommand
    Dim cmd5 As sqlCommand
    Dim cmdsm As sqlCommand
    Dim da As sqlDataAdapter
    Dim ds As New DataSet
    Dim dr As sqlDataReader
    Public sea As String 'searchstring

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If

        Dim thread As Integer
        thread = Convert.ToInt32(Session("chatsess"))
        con.Open() 'laodmess
        cmd = New sqlCommand("SELECT * FROM (([messages] INNER JOIN [message_threads] ON [messages].[thread_id]=[message_threads].[thread_id]) LEFT JOIN [userinfo] on [userinfo].[profile_id] = [messages].[sender])  WHERE ([message_threads].[thread_id] = " & thread & ") ORDER BY [date_written] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            personmess.DataSource = cmd.ExecuteReader()
            personmess.DataBind()
        End If
        con.Close()

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        If Page.IsPostBack = False Then
            con.Open() 'load  peeps
            cmd = New SqlCommand("SELECT [userinfo].[dp_url], [message_threads].[thread_id], [userinfo].[fname], [userinfo].[lname], [userinfo].[profile_id] FROM [message_threads] LEFT JOIN [userinfo] ON [userinfo].[profile_id] = (CASE WHEN ([message_threads].[from] <> '" & mysess & "') THEN ([message_threads].[from]) ELSE ([message_threads].[to]) END) WHERE '" & mysess & "' = (CASE WHEN ([message_threads].[from] <> '" & mysess & "') THEN ([to]) ELSE ([from]) END) ORDER BY [last_updated_on] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                peeps.DataSource = cmd.ExecuteReader()
                peeps.DataBind()
            End If
            con.Close()



        End If
    End Sub

    Protected Sub txtSearch_KeyUp(ByVal Sender As Object, ByVal e As EventArgs)

        sea = seanot.Text
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("SELECT [userinfo].[dp_url], [message_threads].[thread_id], [userinfo].[fname], [userinfo].[lname], [userinfo].[profile_id] FROM [message_threads] LEFT JOIN [userinfo] ON [userinfo].[profile_id] = (CASE WHEN ([message_threads].[from] <> '" & mysess & "') THEN ([message_threads].[from]) ELSE ([message_threads].[to]) END) WHERE ('" & mysess & "' = (CASE WHEN ([message_threads].[from] <> '" & mysess & "') THEN ([to]) ELSE ([from]) END) AND  (([userinfo].[uname] LIKE '%' + @sea+ '%') OR ([userinfo].[fname] LIKE '%' + @sea+ '%') OR ([userinfo].[lname] LIKE '%' + @sea+ '%'))) ORDER BY [last_updated_on] DESC", con)
        cmd.Parameters.Add("@sea", SqlDbType.NVarChar)
        cmd.Parameters("@sea").Value = sea.Replace("'", "''").ToString()
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()

            peeps.Visible = True
            peeps.DataSource = cmd.ExecuteReader()
            peeps.DataBind()
        Else
            peeps.Visible = False

        End If
        con.Close()
    End Sub

    Sub red(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Protected Sub sendmess(ByVal Sender As Object, ByVal e As EventArgs)

        con.Open()
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim thread As Integer
        thread = Convert.ToInt32(Session("chatsess"))
        Dim messtxta As String = sendmess_txt.Value()
        Dim intDay As Date
        intDay = Date.Now
        cmdsm = New SqlCommand("INSERT INTO messages(content, thread_id, sender, date_written) VALUES(@content,'" & thread & "', '" & mysess & "', '" & intDay & "')", con)
        cmdsm.Parameters.Add("@content", SqlDbType.NVarChar)
        cmdsm.Parameters("@content").Value = messtxta.Replace("'", "''").ToString()
        cmdsm.Connection = con
        cmdsm.ExecuteNonQuery()
        cmd = New sqlCommand("UPDATE message_threads SET [last_updated_on] = '" & intDay & "' WHERE ([thread_id] = " & thread & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        con.Open()
        cmd = New sqlCommand("SELECT * FROM (([messages] INNER JOIN [message_threads] ON [messages].[thread_id]=[message_threads].[thread_id]) LEFT JOIN [userinfo] on [userinfo].[profile_id] = [messages].[sender])  WHERE ([message_threads].[thread_id] = " & thread & ") ORDER BY [date_written] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            personmess.DataSource = cmd.ExecuteReader()
            personmess.DataBind()
            sendmess_txt.Value = ""
        End If
        con.Close()
    End Sub

    Protected Sub sendmessnew(ByVal Sender As Object, ByVal e As EventArgs)
        con.Open()
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))

        Dim messtxta As String = sendmess_txt.Value()
        Dim intDay As Date
        intDay = Date.Now




        cmd = New sqlCommand("SELECT [profile_id] FROM [userinfo] WHERE ([uname] = '" & wow.Value & "')", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Dim prosess As Integer
            prosess = dr("profile_id")
            cmd = New sqlCommand("SELECT [thread_id] FROM message_threads WHERE (([from] = " & prosess & " AND [to] = " & mysess & ") OR ([to] = " & prosess & " AND [from] = " & mysess & "))", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                Dim thread As Integer

                Dim messcontent As String
                thread = Convert.ToInt32(dr(0).ToString())
                messcontent = cont.Text
                cmd2 = New SqlCommand("INSERT INTO messages(content, sender, thread_id, date_written) VALUES(@content, " & mysess & ", '" & thread & "', '" & intDay & "')", con)
                cmd.Parameters.Add("@content", SqlDbType.NVarChar)
                cmd.Parameters("@content").Value = messtxta.Replace("'", "''").ToString()
                cmd2.Connection = con
                cmd2.ExecuteNonQuery()
                cmd = New sqlCommand("UPDATE message_threads SET [last_updated_on] = '" & intDay & "' WHERE ([thread_id] = " & thread & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                Response.Redirect("Messages.aspx")
            Else
                con.Close()
                con.Open()
                cmd3 = New sqlCommand("INSERT INTO message_threads([from], [to]) VALUES(" & mysess & ", " & prosess & ")", con)
                cmd3.Connection = con
                cmd3.ExecuteNonQuery()
                cmd4 = New sqlCommand("SELECT [thread_id] FROM message_threads WHERE (([from] = " & prosess & " AND [to] = " & mysess & ") OR ([to] = " & prosess & " AND [from] = " & mysess & "))", con)
                dr = cmd4.ExecuteReader
                If (dr.Read) Then
                    Dim newthread As Integer
                    newthread = Convert.ToInt32(dr(0).ToString())


                    Dim messcontent As String

                    messcontent = cont.Text
                    cmd2 = New SqlCommand("INSERT INTO messages(content, sender, thread_id, date_written) VALUES(@content, " & mysess & ", '" & newthread & "', '" & intDay & "')", con)
                    cmd.Parameters.Add("@content", SqlDbType.NVarChar)
                    cmd.Parameters("@content").Value = messtxta.Replace("'", "''").ToString()
                    cmd2.Connection = con
                    cmd2.ExecuteNonQuery()
                    cmd = New sqlCommand("UPDATE message_threads SET [last_updated_on] = '" & intDay & "' WHERE ([thread_id] = " & newthread & ")", con)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    Response.Redirect("Messages.aspx")
                End If
            End If
        End If
        con.Close()


    End Sub

    Sub thread_sel(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim psesstemp As Integer
        psesstemp = commandArgsAccept.ToString
        Session("psess") = psesstemp

        Dim mysess As Integer = Session("idsess")
        Dim himsess As Integer = Session("psess")
        con.Open() 'laodmess
        cmd = New sqlCommand("SELECT [thread_id], [last_updated_on] FROM [message_threads] WHERE ((([from] = " & mysess & " ) AND ([to] = " & himsess & " )) OR (([from] = " & himsess & " ) AND ([to] = " & mysess & " )))", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Dim thread As Integer = dr("thread_id")
            Session("chatsess") = thread

            Session("last_updated_on") = dr("last_updated_on")

            con.Close()
            con.Open() 'laodmess
            cmd = New sqlCommand("SELECT * FROM (([messages] INNER JOIN [message_threads] ON [messages].[thread_id]=[message_threads].[thread_id]) LEFT JOIN [userinfo] on [userinfo].[profile_id] = [messages].[sender])  WHERE ([message_threads].[thread_id] = " & thread & ") ORDER BY [date_written] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                personmess.DataSource = cmd.ExecuteReader()
                personmess.DataBind()
            End If
            con.Close()
        Else
            Response.Write("nm")
        End If
        con.Close()

    End Sub

    Sub del_con(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim thread As Integer
        thread = Convert.ToInt32(Session("chatsess"))
        con.Open()
        cmd = New sqlCommand("SELECT * FROM [message_threads] WHERE [thread_id] = " & thread & "", con)
        dr = cmd.ExecuteReader
        If dr.Read Then
            con.Close()
            con.Open()
            cmd = New SqlCommand("DELETE FROM [messages] WHERE [thread_id] = " & thread & "", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            con.Close()
            con.Open()
            cmd = New SqlCommand("DELETE FROM [message_threads] WHERE [thread_id] = " & thread & "", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            con.Close()
        End If
        Response.Redirect("Messages.aspx")
    End Sub


End Class
