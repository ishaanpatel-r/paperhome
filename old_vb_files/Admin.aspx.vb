Imports System.Data
Imports System.IO
imports system.data.sqlclient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web

Public Class _Default
    Inherits System.Web.UI.Page


    Dim con As sqlconnection
    Dim cmd As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
        Else
            If (Session("idsess") <> 2) Then
                Response.Redirect("Home.aspx")
            End If
        End If
    End Sub

    Sub setdel(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("DELETE FROM music_data WHERE [music_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New sqlcommand("DELETE FROM music_junc WHERE [music_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub setcheck(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("UPDATE music_data SET [checked] = True WHERE [music_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub setedit(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        Dim txtBox As HtmlInputText
        Dim messtxta As String
        For Each rItem As RepeaterItem In murep.Items
            txtBox = DirectCast(rItem.FindControl("fn_txt"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxta = txtBox.Value
                End If
            End If
        Next
        con.Open()
        cmd = New sqlcommand("UPDATE music_data SET [checked] = True, [m_name] = '" & messtxta.Replace("'", "''") & "' WHERE [music_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub



    Sub set1del(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("DELETE FROM movies_data WHERE [movies_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New sqlcommand("DELETE FROM movies_junc WHERE [movies_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set1check(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("UPDATE movies_data SET [checked] = True WHERE [movies_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set1edit(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        Dim txtBox As HtmlInputText
        Dim messtxta As String
        For Each rItem As RepeaterItem In morep.Items
            txtBox = DirectCast(rItem.FindControl("fn_txt"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxta = txtBox.Value
                End If
            End If
        Next
        con.Open()
        cmd = New sqlcommand("UPDATE movies_data SET [checked] = True, [d_name] = '" & messtxta.Replace("'", "''") & "' WHERE [movies_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub




    Sub set2del(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("DELETE FROM books_data WHERE [books_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New sqlcommand("DELETE FROM books_junc WHERE [books_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set2check(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("UPDATE books_data SET [checked] = True WHERE [books_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set2edit(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        Dim txtBox As HtmlInputText
        Dim messtxta As String
        For Each rItem As RepeaterItem In borep.Items
            txtBox = DirectCast(rItem.FindControl("fn_txt"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxta = txtBox.Value
                End If
            End If
        Next
        con.Open()
        cmd = New sqlcommand("UPDATE books_data SET [checked] = True, [d_name] = '" & messtxta.Replace("'", "''") & "' WHERE [books_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set3del(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("DELETE FROM food_data WHERE [food_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New sqlcommand("DELETE FROM food_junc WHERE [food_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set3check(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("UPDATE food_data SET [checked] = True WHERE [food_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set3edit(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        Dim txtBox As HtmlInputText
        Dim messtxta As String
        For Each rItem As RepeaterItem In forep.Items
            txtBox = DirectCast(rItem.FindControl("fn_txt"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxta = txtBox.Value
                End If
            End If
        Next
        con.Open()
        cmd = New sqlcommand("UPDATE food_data SET [checked] = True, [d_name] = '" & messtxta.Replace("'", "''") & "' WHERE [food_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub


    Sub set4del(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("DELETE FROM tv_data WHERE [tv_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New sqlcommand("DELETE FROM tv_junc WHERE [tv_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set4check(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("UPDATE tv_data SET [checked] = True WHERE [tv_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set4edit(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        Dim txtBox As HtmlInputText
        Dim messtxta As String
        For Each rItem As RepeaterItem In tvrep.Items
            txtBox = DirectCast(rItem.FindControl("fn_txt"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxta = txtBox.Value
                End If
            End If
        Next
        con.Open()
        cmd = New sqlcommand("UPDATE tv_data SET [checked] = True, [tv_d] = '" & messtxta.Replace("'", "''") & "' WHERE [tv_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub


    Sub set5del(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("DELETE FROM team_data WHERE [team_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New sqlcommand("DELETE FROM team_junc WHERE [team_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set5check(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("UPDATE team_data SET [checked] = True WHERE [team_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set5edit(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        Dim txtBox As HtmlInputText
        Dim messtxta As String
        For Each rItem As RepeaterItem In terep.Items
            txtBox = DirectCast(rItem.FindControl("fn_txt"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxta = txtBox.Value
                End If
            End If
        Next
        con.Open()
        cmd = New sqlcommand("UPDATE team_data SET [checked] = True, [team_d] = '" & messtxta.Replace("'", "''") & "' WHERE [team_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub


    Sub set6del(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("DELETE FROM places_data WHERE [places_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New sqlcommand("DELETE FROM places_junc WHERE [places_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set6check(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        con.Open()
        cmd = New sqlcommand("UPDATE places_data SET [checked] = True WHERE [places_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Sub set6edit(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument
        Dim datacheck As Integer = commandArgsAccept
        Dim txtBox As HtmlInputText
        Dim messtxta As String
        For Each rItem As RepeaterItem In plrep.Items
            txtBox = DirectCast(rItem.FindControl("fn_txt"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxta = txtBox.Value
                End If
            End If
        Next
        con.Open()
        cmd = New sqlcommand("UPDATE places_data SET [checked] = True, [places_d] = '" & messtxta.Replace("'", "''") & "' WHERE [places_d_id] = " & datacheck & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub




    Protected Sub delpro(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim prosess As Integer = Convert.ToInt32(commandArgsAccept.ToString())
        con.Open()
        cmd = New sqlcommand("DELETE FROM userinfo WHERE (profile_id=" & prosess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub

    Protected Sub unpro(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim prosess As Integer = Convert.ToInt32(commandArgsAccept.ToString())
        con.Open()
        cmd = New sqlcommand("UPDATE userinfo SET [is_reported] = False WHERE (profile_id=" & prosess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Admin.aspx")
    End Sub




    Sub red_writes(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Sub red_act_sf(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Sub red_act_wn(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Sub red_act_cy(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Sub red_act_ca(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Sub red_act_ly(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Sub red_act_la(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Sub red_reqs(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub

    Sub red_fav_writes(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("prosess") = value1
        Response.Redirect("XsProfile.aspx?id=" & value1)
    End Sub
End Class
