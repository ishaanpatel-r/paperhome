Imports System.Data
Imports System.Data.SqlClient

Public Class _Default
    Inherits System.Web.UI.Page


    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If
    End Sub

    Protected Sub delacc(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New sqlCommand("SELECT ([password]) FROM userinfo WHERE ([profile_id]= " & mysess & ")", con)
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Session("pass") = dr(0).ToString()
            If (Session("pass") = wrepw.Value) Then
                con.Close()
                con.Open()
                cmd = New sqlCommand("DELETE FROM userinfo WHERE (profile_id=" & mysess & ")", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                Response.Redirect("MainPage.aspx")
            Else
                Response.Write("Enter correct password.")
            End If
        End If


    End Sub

End Class
