Imports System.Data
Imports System.IO
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.Configuration

Public Class _Default
    Inherits System.Web.UI.Page


    'Dim con As sqlconnection
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim cmdip As SqlCommand
    Dim cmdiprem As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader




    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        Session.Clear()

        con.Open()


        Dim outtime As Date
        outtime = Date.Now
        Dim exp_sess_time As Date
        exp_sess_time = Date.Now.AddHours(-3)
        cmd = New SqlCommand("UPDATE [session_logs] SET [date_time_logout] = '" & outtime & "' WHERE (([date_time_logout] IS NULL) AND ([date_time_login] < '" & exp_sess_time & "'))", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()

        con.Close()
    End Sub

    Protected Sub loginsub(ByVal Sender As Object, ByVal e As EventArgs)
        con.Open()
        cmd = New sqlcommand("SELECT ([password]) FROM userinfo WHERE ([uname]= '" & uname_log.Value & "')", con)
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            If (dr(0).ToString = pass_log.Value) Then

                dr.Close()
                cmd = New SqlCommand("SELECT [profile_id], [is_verified] FROM userinfo WHERE ([uname]= '" & uname_log.Value & "')", con)
                dr = cmd.ExecuteReader
                If (dr.Read) Then

                    Dim verstate As String = dr("is_verified")
                    Dim usersess As Integer = Convert.ToInt32(dr("profile_id"))
                    If verstate = "True" Then
                        dr.Close()
                        Session("idsess") = usersess
                    Else
                        dr.Close()
                        Session("nidsess") = usersess
                        Response.Redirect("VerifyAcc.aspx")
                    End If

                End If

                Dim IPAdd As String = String.Empty
                IPAdd = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                Dim mysess As String
                mysess = Convert.ToString(Session("idsess"))
                Dim intDay As Date
                intDay = Date.Now
                Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                Dim browname As String
                browname = objBrwInfo.Browser
                Dim browver As String
                browver = objBrwInfo.Version
                If String.IsNullOrEmpty(IPAdd) Then
                    IPAdd = Request.ServerVariables("REMOTE_ADDR")
                    cmdiprem = New SqlCommand("INSERT INTO session_logs(profile_id, ip_id, date_time_login, brow_name, brow_ver) VALUES('" & mysess & "', '" & IPAdd & "', '" & intDay & "', '" & browname & "', '" & browver & "')", con)
                    cmdiprem.Connection = con
                    cmdiprem.ExecuteNonQuery()
                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("SELECT TOP 1 [session_no] FROM [session_logs] WHERE ([profile_id] = " & mysess & ") ORDER BY [session_no] DESC")
                    cmd.Connection = con
                    dr = cmd.ExecuteReader
                    If (dr.Read) Then

                        Session("sessid") = dr("session_no")
                        dr.Close()
                        con.Close()
                        con.Open()
                    End If

                Else
                    dr.Close()
                    cmdip = New SqlCommand("INSERT INTO session_logs(profile_id, ip_id, brow_name, brow_ver) VALUES('" & mysess & "', '" & IPAdd & "', '" & intDay & "', '" & browname & "', '" & browver & "')", con)
                    cmdip.Connection = con
                    cmdip.ExecuteNonQuery()
                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("SELECT TOP 1 [session_no] FROM [session_logs] WHERE ([profile_id] = " & mysess & ") ORDER BY [session_no] DESC")
                    cmd.Connection = con
                    dr = cmd.ExecuteReader()
                    If (dr.Read) Then
                        dr.Close()
                        Session("sessid") = dr("session_no")
                        con.Close()
                        con.Open()
                    End If
                End If
                Response.Redirect("Home.aspx")
            Else
                Dim m As String
                m = "<center>Incorrect Password.</center>"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrwarn('" & m & "')", True)
            End If
        Else
            Dim m As String
            m = "Signup instead? :)"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
        End If
        con.Close()
    End Sub

End Class
