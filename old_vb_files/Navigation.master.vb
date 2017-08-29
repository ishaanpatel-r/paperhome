Imports System.Data
imports system.data.sqlclient
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

Partial Class Navigation
    Inherits System.Web.UI.MasterPage
    Dim con As sqlconnection
    Dim cmd As sqlcommand
    Dim cmd2 As sqlcommand
    Dim cmd3 As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader
    Dim dt As DataTable
    Public myList As New ArrayList() 'whom you could tag array
    Public myListMessaging As New ArrayList() 'ac for messaging.aspx
    Public str As String 'whom you could tag string
    Public strMessaging As String 'str for messaging ac

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") <> 2) Then
            adminlink.Style("Display") = "none"
        End If

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))

        con.Open() 'whom you could tag
        cmd = New sqlcommand("SELECT uname, fname, lname FROM userinfo WHERE ([profile_id] <> " & mysess & ")", con)
        dr = cmd.ExecuteReader()
        myList.Add("[")
        While dr.Read()
            Dim value1 = dr.GetValue(0)
            Dim value2 = dr.GetValue(1)
            Dim value3 = dr.GetValue(2)
            myList.Add("{ username: """ + value1 + """, fullname: """ + value2 + " " + value3 + """ },")
        End While
        myList.Add("];")
        str = String.Join(" ", myList.ToArray())
        con.Close()

        con.Open() 'whom you could message
        cmd = New sqlcommand("SELECT uname FROM userinfo WHERE ([profile_id] <> " & mysess & ")", con)
        dr = cmd.ExecuteReader()
        myListMessaging.Add("[")
        While dr.Read()
            Dim value1 = dr.GetValue(0)

            myListMessaging.Add("'" + value1 + "',")
        End While
        myListMessaging.Add("],")
        strMessaging = String.Join(" ", myListMessaging.ToArray())
        con.Close()

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

        End If

        If Page.IsPostBack = False Then
            con.Open() 'load  req
            cmd = New sqlcommand("SELECT * from [userinfo] WHERE [profile_id] = " & mysess & "", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                accrep.DataSource = cmd.ExecuteReader()
                accrep.DataBind()

            End If
            con.Close()

        End If

        If Page.IsPostBack = False Then
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


    Protected Sub reload(ByVal Sender As Object, ByVal e As EventArgs)

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

        End If

        If Page.IsPostBack = False Then
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

    Protected Sub logoutsess(ByVal Sender As Object, ByVal e As EventArgs)
        con.Open()
        Dim sess As Integer
        sess = Session("sessid")
        Dim outtime As Date
        outtime = Date.Now
        cmd = New sqlcommand("UPDATE [session_logs] SET [date_time_logout] = '" & outtime & "' WHERE ([session_no] = " & sess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        Session.Clear()
        Response.Redirect("MainPage.aspx")
        con.Close()
    End Sub



End Class

