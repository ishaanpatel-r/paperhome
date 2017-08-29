Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class _Default
    Inherits System.Web.UI.Page



    Dim con As sqlConnection
    Dim cmd As sqlCommand
    Dim da As sqlDataAdapter
    Dim ds As New DataSet
    Dim dr As sqlDataReader

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") <> 0) Then

            Dim mysess As Integer
            mysess = Convert.ToInt32(Session("idsess"))
            con.Open()
            cmd = New sqlCommand("SELECT uname FROM [userinfo] WHERE [profile_id] = " & mysess & "", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                Response.Redirect("Home.aspx")
            End If
        End If

    End Sub

    Protected Sub verifyacc(ByVal Sender As Object, ByVal e As EventArgs)
        Dim notmysessyet As Integer = Convert.ToInt32(Session("nidsess"))
        Dim txtinscode As String = wrepw.Value.ToString()
        Dim emailsencode As String = Convert.ToString(Session("authcode"))
        If String.Equals(txtinscode, emailsencode) = True Then
            con.Open()
            cmd = New sqlCommand("UPDATE [userinfo] SET [is_verified] = True WHERE [profile_id] = " & notmysessyet & "", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            con.Close()
            Session("idsess") = Session("nidsess")
            Response.Redirect("MyProfile.aspx")
        Else
            Dim errmess As String = "The code you entered does not match the code sent to you. If the code has expired, request a new code."
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & errmess & "')", True)
        End If
    End Sub

    Protected Sub resendcode(ByVal Sender As Object, ByVal e As EventArgs)
        Dim notmysessyet As Integer = Convert.ToInt32(Session("nidsess"))
        con.Open()
        cmd = New sqlCommand("SELECT * FROM [userinfo] WHERE (profile_id = " & notmysessyet & ")", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Dim fname As String = dr("fname")
            Dim emailtargetadd As String = dr("email")
            con.Close()

            Dim randnum As String = Convert.ToString(CInt(Int(9999 * Rnd()) + 1111))
            Session("authcode") = randnum

            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("helpdesk.paperhome@gmail.com", "discovermemories*5")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("helpdesk.paperhome@gmail.com")
            e_mail.To.Add(emailtargetadd)
            e_mail.Subject = "Paperhome Verification Code"
            e_mail.IsBodyHtml = False
            Dim fnameadd As String = Convert.ToString(Session("fname"))
            e_mail.Body = "Hello " & fname & ", your verification code is: " & randnum & ". Please use it to verify your account."
            Smtp_Server.Send(e_mail)
        End If


    End Sub


End Class
