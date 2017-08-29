Imports System.Data
Imports System.IO
imports system.data.sqlclient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web


Partial Class Profile
    Inherits System.Web.UI.MasterPage


    Dim con As sqlconnection
    Dim cmd As sqlcommand
    Dim cmd2 As sqlcommand
    Dim cmd3 As sqlcommand
    Dim cmd4 As sqlcommand
    Dim cmd5 As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader
    Public myList As New ArrayList() 'whom you could tag array
    Public str As String 'whom you could tag string


    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)

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
        Str = String.Join(" ", myList.ToArray())
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
            cmd = New sqlcommand("SELECT TOP 4 [userinfo].[profile_id],[type], [uname], [date_time_written], [post] FROM (([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) LEFT JOIN [writes] ON [writes].[writes_id]=[activity].[post]) WHERE ((([doee] = " & mysess & ") OR ([writes].[profile_id] = " & mysess & ")) AND [doer] <> " & mysess & ") ORDER BY [date_time_written] DESC", con)
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
        cmd = New sqlcommand("UPDATE req SET [accepted]= True WHERE ([from] = " & hissess & " AND [to] = " & mysess & ")", con)
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
        cmd = New sqlcommand("SELECT TOP 4 [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE ([accepted]= False AND [to]= " & mysess & " )", con)
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
        cmd = New sqlcommand("SELECT TOP 4 [lname], [fname], [dp_url], [profile_id] FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE ([accepted]= False AND [to]= " & mysess & " )", con)
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


    Protected Sub logoutsess(ByVal Sender As Object, ByVal e As EventArgs)
        Session.Clear()
        Response.Redirect("MainPage.aspx")
    End Sub

    Sub f(ByVal Sender As Object, ByVal e As CommandEventArgs)
       
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim proid As Integer = commandArgsAccept
        Session("f") = proid
        Dim prosess As Integer
        prosess = Convert.ToInt32(Session("f"))
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim intDay As Date
        intDay = Date.Now
        con.Open()
        cmd = New sqlcommand("INSERT INTO req([from], [to], date_time)  VALUES(" & mysess & "," & prosess & ",'" & intDay & "')", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        Response.Redirect(String.Format("XsProfile.aspx?profileid={0}", Session("prosess")))
    End Sub

    Sub unf(ByVal Sender As Object, ByVal e As CommandEventArgs)
        
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim proid As Integer = commandArgsAccept
        Session("unf") = proid
        Dim prosess As Integer
        prosess = Convert.ToInt32(Session("unf"))
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("DELETE FROM feelpals_sys WHERE ([group_to_following] = " & mysess & " AND [group_to_followers] = " & prosess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect(String.Format("XsProfile.aspx?profileid={0}", Session("prosess")))
    End Sub

    Sub wrtest(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim proid As Integer = commandArgsAccept
        Session("unf") = proid
        Dim prosess As Integer
        prosess = Convert.ToInt32(Session("unf"))
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim intDay As Date
        intDay = Date.Now
        con.Open()
        Dim txtBox As HtmlInputText
        Dim messtxtaw As String = " "
        For Each rItem As RepeaterItem In Repeater12.Items
            txtBox = DirectCast(rItem.FindControl("Text1"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxtaw = txtBox.Value
                End If
            End If
        Next
        Dim rated As Integer = 1
        For Each rItem As RepeaterItem In Repeater12.Items
            txtBox = DirectCast(rItem.FindControl("rate"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    rated = txtBox.Value
                End If
            End If
        Next
        cmd = New SqlCommand("INSERT INTO tests([to], [from], content, rating, date_written) VALUES(" & prosess & ", " & mysess & ", @testcon, '" & rated & "', '" & intDay & "')", con)
        cmd.Parameters.Add("@testcon", SqlDbType.NVarChar)
        cmd.Parameters("@testcon").Value = messtxtaw.Replace("'", "''")
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect(String.Format("XsProfile.aspx?profileid={0}", Session("prosess")))
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
            For Each rItem As RepeaterItem In Repeater12.Items
                txtBox = DirectCast(rItem.FindControl("messxtxt"), HtmlInputText)
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
            End If
        End If
        con.Close()
    End Sub

    Sub repprosub(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim prosess As Integer
        prosess = Convert.ToInt32(e.CommandArgument)
        con.Open()
        cmd = New SqlCommand("UPDATE userinfo SET [is_reported] = 1 WHERE ([profile_id] = " & prosess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect(String.Format("XsProfile.aspx?profileid={0}", Session("prosess")))
    End Sub

End Class

