Imports System.Data
imports system.data.sqlclient
Imports System.Net.Mail

Public Class _Default
    Inherits System.Web.UI.Page


    Dim con As sqlconnection
    Dim cmd As sqlcommand
    Dim cmd2 As sqlcommand
    Dim cmdsm As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("nidsess") = 0) Then
            Response.Redirect("RegPage1.aspx")
        Else
            Dim mysess As Integer
            mysess = Convert.ToInt32(Session("idsess"))
            con.Open()
            cmd = New sqlcommand("SELECT uname FROM [userinfo] WHERE [profile_id] = " & mysess & "", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                If (dr.Read <> Nothing) Then
                    Response.Redirect("Home.aspx")
                End If
            End If
        End If
        If (Session("idsess") <> 0) Then

            Dim mysess As Integer
            mysess = Convert.ToInt32(Session("idsess"))
            con.Open()
            cmd = New sqlcommand("SELECT uname FROM [userinfo] WHERE [profile_id] = " & mysess & "", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                Response.Redirect("Home.aspx")
            End If
        End If
    End Sub

    Protected Sub submit_but(ByVal Sender As Object, ByVal e As EventArgs)
        If ((Not inputTwitter.Value = "") Or (Not inputPassword.Value = "") Or (Not inputPasswordConfirm.Value = "") Or (Not cellnum.Value = "")) Then
            Dim untxt As String
            untxt = inputTwitter.Value.ToString()
            Dim passctxt As String
            passctxt = inputPasswordConfirm.Value.ToString()
            Dim passtxt As String
            passtxt = inputPassword.Value.ToString()
            Dim cellno As String
            cellno = cellnum.Value.ToString()
            Dim intDay As Date
            intDay = Date.Now
            Dim thisacc As Integer
            thisacc = Convert.ToInt32(Session("nidsess"))
            If (passctxt = passtxt) Then
                Try
                    Dim Smtp_Server As New SmtpClient
                    Dim e_mail As New MailMessage()
                    Smtp_Server.UseDefaultCredentials = False
                    Smtp_Server.Credentials = New Net.NetworkCredential("helpdesk.paperhome@gmail.com", "discovermemories*5")
                    Smtp_Server.Port = 587
                    Smtp_Server.EnableSsl = True
                    Smtp_Server.Host = "smtp.gmail.com"

                    e_mail = New MailMessage()
                    e_mail.From = New MailAddress("helpdesk.paperhome@gmail.com")
                    Dim emailtargetadd As String = Convert.ToString(Session("emailid"))
                    e_mail.To.Add(emailtargetadd)
                    e_mail.Subject = "Registered For Paperhome! Yay!"
                    e_mail.IsBodyHtml = False
                    Dim fnameadd As String = Convert.ToString(Session("fname"))
                    Dim randnum As String = Convert.ToString(CInt(Int(9999 * Rnd()) + 1111))
                    Session("authcode") = randnum
                    e_mail.Body = "This is to notify you that your profile has successfully been created at Paperhome. Here\'s your access code:" & randnum & ". Please use it to verify your e-mail. Thank you for joining us, " & fnameadd & "."
                    Smtp_Server.Send(e_mail)

                    con.Open()
                    cmd = New SqlCommand("UPDATE userinfo SET [uname] = '" & untxt & "', [password] = '" & passctxt & "', [join_date] = '" & intDay & "', [cell_no] = '+91-" & cellno & "' WHERE ([profile_id] = " & thisacc & ")", con)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    Response.Redirect("VerifyAcc.aspx")

                Catch ex As Exception
                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("UPDATE userinfo SET [is_verified] = True WHERE ([profile_id] = " & thisacc & ")", con)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    Session("idsess") = Session("nidsess")

                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("UPDATE userinfo SET [uname] = '" & untxt & "', [password] = '" & passctxt & "', [join_date] = '" & intDay & "', [cell_no] = '+91-" & cellno & "' WHERE ([profile_id] = " & thisacc & ")", con)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()


                    Response.Redirect("Home.aspx")
                End Try

            End If
        Else
            MsgBox("You Left Something Empty")
        End If
    End Sub

End Class
