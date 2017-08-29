Imports System.Data
Imports System.Data.OleDb
Imports System.Web.Services
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic



Public Class _Default
    Inherits System.Web.UI.Page



    Dim con As OleDbConnection
    Dim cmd As OleDbCommand
    Dim cmd2 As OleDbCommand
    Dim cmd3 As OleDbCommand
    Dim da As OleDbDataAdapter
    Dim ds As New DataSet
    Dim dr As OleDbDataReader
    Dim dt As DataTable
    Public sea As String 'searchstring


    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ishaan Patel\Desktop\Paperhome\paperhome_data.accdb")
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If
        Dim ljs As Integer
        ljs = Convert.ToInt32(Session("ljjsession"))
        con.Open()
        cmd = New OleDbCommand("SELECT sub_name from livesessions WHERE [session_id] = " & ljs & " ", con)
        dr = cmd.ExecuteReader()
        If dr.Read Then

            Session("ljsub") = dr("sub_name")
        End If
        con.Close()
        Dim sessin As Integer = Session("ljjsession")

        If Page.IsPostBack = False Then
            con.Open()
            cmd = New OleDbCommand("SELECT [userinfo].[profile_id], [dp_url], [fname], [lname], [bit], [bit_date_written], [sender] FROM (([bit_data] INNER JOIN [livesessions] ON [bit_data].[session_id] = [livesessions].[session_id]) LEFT JOIN [userinfo] ON [bit_data].[sender] = [userinfo].[profile_id]) WHERE [livesessions].[session_id] = " & sessin & " ORDER BY [bit_date_written] ASC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                gd.DataSource = cmd.ExecuteReader()
                gd.DataBind()

            End If
            con.Close()

        End If
    End Sub


    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Dim ljs As Integer
        ljs = Convert.ToInt32(Session("ljjsession"))
         con.Open()
        cmd = New OleDbCommand("SELECT content from livesessions WHERE [session_id] = " & ljs & " ", con)
        dr = cmd.ExecuteReader()
        If dr.Read Then
            con.Close()
            con.Open()
            notes.DataSource = cmd.ExecuteReader()
            notes.DataBind()
        End If
        con.Close()
    End Sub

    Protected Sub sendbit(ByVal Sender As Object, ByVal e As EventArgs)

        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim sessin As Integer = Session("ljjsession")

        Dim messtxta As String = bitin.Value()
        Dim intDay As Date
        intDay = Date.Now
        con.Open()
        cmd = New OleDbCommand("INSERT INTO bit_data([bit], [sender], [session_id], bit_date_written) VALUES('" & messtxta.Replace("'", "''") & "'," & mysess & ", " & sessin & ", '" & intDay & "')", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        If Page.IsPostBack = True Then
            con.Open()
            cmd = New OleDbCommand("SELECT [dp_url], [fname], [lname], [bit], [bit_date_written], [sender] FROM (([bit_data] INNER JOIN [livesessions] ON [bit_data].[session_id] = [livesessions].[session_id]) LEFT JOIN [userinfo] ON [bit_data].[sender] = [userinfo].[profile_id]) WHERE [livesessions].[session_id] = " & sessin & " ORDER BY [bit_date_written] ASC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                gd.DataSource = cmd.ExecuteReader()
                gd.DataBind()
            Else
                Response.Write("damn nigga")
            End If
            con.Close()
            bitin.Value = ""
        End If
    End Sub

    Protected Sub refchat(ByVal Sender As Object, ByVal e As EventArgs)
        Dim sessin As Integer = Session("ljjsession")
        If Page.IsPostBack = True Then
            con.Open()
            cmd = New OleDbCommand("SELECT [dp_url], [fname], [lname], [bit], [bit_date_written], [sender] FROM (([bit_data] INNER JOIN [livesessions] ON [bit_data].[session_id] = [livesessions].[session_id]) LEFT JOIN [userinfo] ON [bit_data].[sender] = [userinfo].[profile_id]) WHERE [livesessions].[session_id] = " & sessin & " ORDER BY [bit_date_written] ASC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                gd.DataSource = cmd.ExecuteReader()
                gd.DataBind()
            Else
                Response.Write("damn nigga")
            End If
            con.Close()
            bitin.Value = ""
        End If

    End Sub
End Class