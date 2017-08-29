Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web

Public Class Default3
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
    Public seai As String 'searchstring following
    Public seae As String 'searchstring followers
    Dim txtName As TextBox 'rep2 messtxt2

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))

        'If (Session("fpra_sess") <> "") Then
        Session("fpra_sess") = ""
        fp_ra.Visible = True
        'End If

        If Page.IsPostBack = False Then
            con.Open() 'load  following list
            cmd = New SqlCommand("SELECT * FROM ([userinfo] LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([group_to_following] = " & mysess & ")", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                Repeater1.DataSource = cmd.ExecuteReader()
                Repeater1.DataBind()
            End If
            con.Close()

            con.Open() 'load  followers list
            cmd = New SqlCommand("SELECT * FROM ([userinfo] LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_following]) WHERE ([group_to_followers] = " & mysess & ")", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                Repeater2.DataSource = cmd.ExecuteReader()
                Repeater2.DataBind()
            End If
            con.Close()


        End If


    End Sub

    Protected Sub txtSearch_KeyUp_ing(ByVal Sender As Object, ByVal e As EventArgs)

        seai = seaing.Text
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("SELECT * FROM ([userinfo] LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([group_to_following] = " & mysess & " AND (([fname] LIKE '%' + @seai + '%') OR ([lname] LIKE '%' + @seai + '%') OR ([uname] LIKE '%' + @seai + '%')))", con)
        cmd.Parameters.Add("@seai", SqlDbType.NVarChar)
        cmd.Parameters("@seai").Value = seaing.Text
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()

            Repeater1.Visible = True
            Repeater1.DataSource = cmd.ExecuteReader()
            Repeater1.DataBind()
        Else
            Repeater1.Visible = False

        End If
        con.Close()
    End Sub

    Protected Sub txtSearch_KeyUp_ers(ByVal Sender As Object, ByVal e As EventArgs)

        seae = seaers.Text
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("SELECT * FROM ([userinfo] LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_following]) WHERE ([group_to_followers] = " & mysess & " AND (([fname] LIKE '%' + @seae + '%') OR ([lname] LIKE '%' + @seae + '%') OR ([uname] LIKE '%' + @seae + '%')))", con)
        cmd.Parameters.Add("@seae", SqlDbType.NVarChar)
        cmd.Parameters("@seae").Value = seaers.Text
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()

            Repeater2.Visible = True
            Repeater2.DataSource = cmd.ExecuteReader()
            Repeater2.DataBind()
        Else
            Repeater2.Visible = False

        End If
        con.Close()
    End Sub

    Protected Sub sendmess(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Session.Remove("tempsess")
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
        cmd = New SqlCommand("SELECT [thread_id] FROM message_threads WHERE (([from] = " & prosess & " AND [to] = " & mysess & ") OR ([to] = " & prosess & " AND [from] = " & mysess & "))", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Dim thread As Integer
            Dim txtBox As HtmlInputText
            Dim messtxta As String
            thread = Convert.ToInt32(dr(0).ToString())
            For Each rItem As RepeaterItem In Repeater1.Items
                txtBox = DirectCast(rItem.FindControl("messtxt"), HtmlInputText)
                If Not IsNothing(txtBox) Then
                    If txtBox.Value.Length > 0 Then
                        messtxta = txtBox.Value
                    End If
                End If
            Next
            cmd2 = New SqlCommand("INSERT INTO messages(content, sender, thread_id, date_written) VALUES('" & messtxta.Replace("'", "''") & "', '" & mysess & "', '" & thread & "', '" & intDay & "')", con)
            cmd2.Connection = con
            cmd2.ExecuteNonQuery()
            cmd = New SqlCommand("UPDATE message_threads SET [last_updated_on] = '" & intDay & "' WHERE ([thread_id] = " & thread & ")", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            Response.Redirect("Feelpals.aspx")
        Else
            con.Close()
            con.Open()
            cmd3 = New SqlCommand("INSERT INTO message_threads([from], [to]) VALUES('" & mysess & "', '" & prosess & "')", con)
            cmd3.Connection = con
            cmd3.ExecuteNonQuery()
            cmd4 = New SqlCommand("SELECT [thread_id] FROM message_threads WHERE (([from] = " & prosess & " AND [to] = " & mysess & ") OR ([to] = " & prosess & " AND [from] = " & mysess & "))", con)
            dr = cmd4.ExecuteReader
            If (dr.Read) Then
                Dim newthread As Integer
                newthread = Convert.ToInt32(dr(0).ToString())
                Dim txtBox As HtmlInputText
                Dim messtxta As String
                For Each rItem As RepeaterItem In Repeater1.Items
                    txtBox = DirectCast(rItem.FindControl("messtxt"), HtmlInputText)
                    If Not IsNothing(txtBox) Then
                        If txtBox.Value.Length > 0 Then
                            messtxta = txtBox.Value
                        End If
                    End If
                Next
                cmd5 = New SqlCommand("INSERT INTO messages(content, sender, thread_id, date_written) VALUES('" & messtxta.Replace("'", "''") & "', '" & mysess & "', '" & newthread & "', '" & intDay & "')", con)
                cmd5.Connection = con
                cmd5.ExecuteNonQuery()
                cmd = New SqlCommand("UPDATE message_threads SET [last_updated_on] = '" & intDay & "' WHERE ([thread_id] = " & newthread & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                Response.Redirect("Feelpals.aspx")
            End If
        End If
        con.Close()
    End Sub

    Protected Sub sendmess2(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Session.Remove("tempsess")
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
        cmd = New SqlCommand("SELECT [thread_id] FROM message_threads WHERE (([from] = " & prosess & " AND [to] = " & mysess & ") OR ([to] = " & prosess & " AND [from] = " & mysess & "))", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Dim thread As Integer
            Dim txtBox As HtmlInputText
            Dim messtxta As String
            thread = Convert.ToInt32(dr(0).ToString())
            For Each rItem As RepeaterItem In Repeater2.Items
                txtBox = DirectCast(rItem.FindControl("messtxt2"), HtmlInputText)
                If Not IsNothing(txtBox) Then
                    If txtBox.Value.Length > 0 Then
                        messtxta = txtBox.Value
                    End If
                End If
            Next
            cmd2 = New SqlCommand("INSERT INTO messages(content, sender, thread_id, date_written) VALUES('" & messtxta.Replace("'", "''") & "', '" & mysess & "', '" & thread & "', '" & intDay & "')", con)
            cmd2.Connection = con
            cmd2.ExecuteNonQuery()
            cmd = New SqlCommand("UPDATE message_threads SET [last_updated_on] = '" & intDay & "' WHERE ([thread_id] = " & thread & ")", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            Response.Redirect("Feelpals.aspx")
        Else
            con.Close()
            con.Open()
            cmd3 = New SqlCommand("INSERT INTO message_threads([from], [to]) VALUES('" & mysess & "', '" & prosess & "')", con)
            cmd3.Connection = con
            cmd3.ExecuteNonQuery()
            cmd4 = New SqlCommand("SELECT [thread_id] FROM message_threads WHERE (([from] = " & prosess & " AND [to] = " & mysess & ") OR ([to] = " & prosess & " AND [from] = " & mysess & "))", con)
            dr = cmd4.ExecuteReader
            If (dr.Read) Then
                Dim newthread As Integer
                newthread = Convert.ToInt32(dr(0).ToString())
                Dim txtBox As HtmlInputText
                Dim messtxta As String
                For Each rItem As RepeaterItem In Repeater2.Items
                    txtBox = DirectCast(rItem.FindControl("messtxt2"), HtmlInputText)
                    If Not IsNothing(txtBox) Then
                        If txtBox.Value.Length > 0 Then
                            messtxta = txtBox.Value
                        End If
                    End If
                Next
                cmd5 = New SqlCommand("INSERT INTO messages(content, sender, thread_id, date_written) VALUES('" & messtxta.Replace("'", "''") & "', '" & mysess & "', '" & newthread & "', '" & intDay & "')", con)
                cmd5.Connection = con
                cmd5.ExecuteNonQuery()
                cmd = New SqlCommand("UPDATE message_threads SET [last_updated_on] = '" & intDay & "' WHERE ([thread_id] = " & newthread & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                Response.Redirect("Feelpals.aspx")
            End If
        End If
        con.Close()
    End Sub

    Sub fpra(ByVal Sender As Object, ByVal e As CommandEventArgs)

        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("fpra_sess") = value1


        con.Open() 'load  notif
        Dim fpsess As Integer
        fpsess = Session("fpra_sess")
        cmd = New SqlCommand("SELECT TOP 5 [profile_id],[type], [uname], [date_time_written], [post], [fname], [doer], [doee] FROM [activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer] WHERE ([profile_id] = " & fpsess & ") ORDER BY [date_time_written] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If dr.Read Then
            Dim m As String
            Dim fname As String = dr("fname")
            m = "Showing Recent Activity of " & fname & "."
            Dim m2 As String
            m2 = m.ToString()
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m2 & "')", True)

            notifdisp.Visible = True
            con.Close()
            con.Open()
            notifdisp.DataSource = cmd.ExecuteReader()
            notifdisp.DataBind()
            hehe.Update()
        Else
            notifdisp.Visible = False
            Dim n As String
            n = "No recent activity. :O"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & n & "')", True)
            hehe.Update()
        End If
        con.Close()


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
        Response.Redirect("Feelpals.aspx")
    End Sub

    Sub stopf(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim proid As Integer = commandArgsAccept
        Session("stopf") = proid
        Dim prosess As Integer
        prosess = Convert.ToInt32(Session("stopf"))
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("DELETE FROM feelpals_sys WHERE ([group_to_followers] = " & mysess & " AND [group_to_following] = " & prosess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Feelpals.aspx")
    End Sub

    Protected Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        Dim lb As LinkButton = TryCast(e.Item.FindControl("dude1"), LinkButton)
        ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(lb)
    End Sub

    Protected Sub Repeater2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemDataBound
        Dim lb2 As LinkButton = TryCast(e.Item.FindControl("dude2"), LinkButton)
        ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(lb2)
    End Sub
End Class
