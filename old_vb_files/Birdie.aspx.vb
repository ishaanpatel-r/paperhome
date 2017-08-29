Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports System.Web.Services

Partial Class _Default
    Inherits System.Web.UI.Page

    Dim con As sqlconnection
    Dim cmd As sqlcommand
    Dim cmd2 As SqlCommand
    Dim cmd3 As SqlCommand
    Dim cmd4 As SqlCommand
    Dim cmd5 As SqlCommand
    Dim cmdsm As SqlCommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader
    Dim dt As DataTable
    Public sea As String 'searchstring

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))

        If Page.IsPostBack = False Then
            con.Open() 'load  req
            cmd = New SqlCommand("SELECT TOP 4 [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE ([accepted]= 0 AND [to]= " & mysess & " )", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                actreqrep.DataSource = cmd.ExecuteReader()
                actreqrep.DataBind()

            End If
            con.Close()

           

            con.Open() 'load  peeps
            cmd = New SqlCommand("SELECT [userinfo].[dp_url], [userinfo].[fname], [userinfo].[lname], [userinfo].[profile_id] FROM [feelpals_sys], [userinfo] WHERE (([group_to_followers] = " & mysess & ") OR ([group_to_following] = " & mysess & "))", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                peeps.DataSource = cmd.ExecuteReader()
                peeps.DataBind()
            End If
            con.Close()




            con.Open() 'load  notif
            cmd = New SqlCommand("SELECT TOP 4 [userinfo].[profile_id],[type], [uname], [date_time_written], [post] FROM (([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) LEFT JOIN [writes] ON [writes].[writes_id]=[activity].[post]) WHERE ((([doee] = " & mysess & ") OR ([writes].[profile_id] = " & mysess & ")) AND [doer] <> " & mysess & ") ORDER BY [date_time_written] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                actnotifrep.DataSource = cmd.ExecuteReader()
                actnotifrep.DataBind()
            End If
            con.Close()
        End If

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
        cmd = New sqlcommand("INSERT INTO feelpals_sys(group_to_following, group_to_followers) VALUES(" & hissess & ", " & mysess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        'activity input'
        con.Open()
        cmd = New sqlcommand("INSERT INTO activity(doer, type, doee, date_time_written) VALUES(" & hissess & ", 'started following', " & mysess & ", '" & intDay & "') ", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        con.Open() 'load  req
        cmd = New SqlCommand("SELECT TOP 4 [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE ([accepted]= 0 AND [to]= " & mysess & " )", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            actreqrep.Visible = True
            actreqrep.DataSource = cmd.ExecuteReader()
            actreqrep.DataBind()
        Else
            actreqrep.Visible = False

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
        cmd = New sqlcommand("DELETE FROM req WHERE ([from] = " & hissess & " AND [to] = " & mysess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        con.Open() 'load  req
        cmd = New SqlCommand("SELECT TOP 4 [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE ([accepted]= 0 AND [to]= " & mysess & " )", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            actreqrep.Visible = True
            actreqrep.DataSource = cmd.ExecuteReader()
            actreqrep.DataBind()
        Else
            actreqrep.Visible = False

        End If
        con.Close()
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


End Class
