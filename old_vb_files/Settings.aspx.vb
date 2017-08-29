Imports System.Data
Imports System.Data.SqlClient

Imports System.Net


Public Class _Default
    Inherits System.Web.UI.Page


    Dim con As sqlConnection
    Dim cmd As sqlCommand
    Dim da As sqlDataAdapter
    Dim ds As New DataSet
    Dim dr As sqlDataReader

    Shared code As String = String.Empty

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If

        If Page.IsPostBack = "false" Then
            'privacy radio fetch
            Dim mysess As Integer
            mysess = Convert.ToInt32(Session("idsess"))
            con.Open()
            cmd = New sqlCommand("SELECT photo_priv, writes_priv, request_priv FROM userinfo WHERE ([profile_id] = " & mysess & ")", con)
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                photo_set_rad.SelectedIndex = dr(0).ToString() - 1
                write_set_rad.SelectedIndex = dr(1).ToString() - 1
                req_set_rad.SelectedIndex = dr(2).ToString() - 1
            End If
            con.Close()


        End If

    End Sub

    Protected Sub save_priv(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))

        Dim a As Integer
        a = write_set_rad.SelectedValue
        con.Open()
        cmd = New sqlCommand("UPDATE userinfo SET [writes_priv] = " & a & " WHERE [profile_id] = " & mysess & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        Dim b As Integer
        b = photo_set_rad.SelectedValue
        con.Open()
        cmd = New sqlCommand("UPDATE userinfo SET [photo_priv] = " & b & " WHERE [profile_id] = " & mysess & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        Dim c As Integer
        c = req_set_rad.SelectedValue
        con.Open()
        cmd = New SqlCommand("UPDATE userinfo SET [request_priv] = " & c & " WHERE [profile_id] = " & mysess & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        Response.Redirect("Settings.aspx")
    End Sub

    Protected Sub save_name(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim ftxtBox As HtmlInputText
        Dim ftxt As String
        For Each rItem As RepeaterItem In gen_rep.Items
            ftxtBox = DirectCast(rItem.FindControl("fn_txt"), HtmlInputText)
            If Not IsNothing(ftxtBox) Then
                If ftxtBox.Value.Length > 0 Then
                    ftxt = ftxtBox.Value.ToString()
                End If
            End If
        Next
        Dim ltxtBox As HtmlInputText
        Dim ltxt As String
        For Each rItem As RepeaterItem In gen_rep.Items
            ltxtBox = DirectCast(rItem.FindControl("ln_txt"), HtmlInputText)
            If Not IsNothing(ltxtBox) Then
                If ltxtBox.Value.Length > 0 Then
                    ltxt = ltxtBox.Value.ToString()
                End If
            End If
        Next
        con.Open()
        cmd = New SqlCommand("UPDATE userinfo SET [fname] =@ftxt, [lname] = @ltxt WHERE ([profile_id] = " & mysess & ")", con)
        cmd.Parameters.Add("@ftxt", SqlDbType.NVarChar)
        cmd.Parameters("@ftxt").Value = ftxt.Replace("'", "''").ToString()
        cmd.Parameters.Add("@ltxt", SqlDbType.NVarChar)
        cmd.Parameters("@ltxt").Value = ltxt.Replace("'", "''").ToString()

        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Settings.aspx")
    End Sub

    Protected Sub save_uname(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim utxtBox As HtmlInputText
        Dim utxt As String
        For Each rItem As RepeaterItem In gen_rep.Items
            utxtBox = DirectCast(rItem.FindControl("un_txt"), HtmlInputText)
            If Not IsNothing(utxtBox) Then
                If utxtBox.Value.Length > 0 Then
                    utxt = utxtBox.Value.ToString()
                End If
            End If
        Next
        con.Open()
        cmd = New sqlCommand("UPDATE userinfo SET [uname] = @utxt WHERE ([profile_id] = " & mysess & ")", con)
        cmd.Parameters.Add("@utxt", SqlDbType.NVarChar)
        cmd.Parameters("@utxt").Value = utxt.Replace("'", "''").ToString()

        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Settings.aspx")
    End Sub

    Protected Sub save_email(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim etxtBox As HtmlInputText
        Dim etxt As String
        For Each rItem As RepeaterItem In gen_rep.Items
            etxtBox = DirectCast(rItem.FindControl("em_txt"), HtmlInputText)
            If Not IsNothing(etxtBox) Then
                If etxtBox.Value.Length > 0 Then
                    etxt = etxtBox.Value.ToString()
                End If
            End If
        Next
        con.Open()
        cmd = New sqlCommand("UPDATE userinfo SET [email] = @etxt WHERE ([profile_id] = " & mysess & ")", con)
        cmd.Parameters.Add("@etxt", SqlDbType.NVarChar)
        cmd.Parameters("@etxt").Value = etxt.Replace("'", "''").ToString()

        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Settings.aspx")
    End Sub

    Protected Sub save_city_batman(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim ctxtBox As HtmlInputText
        Dim ctxt As String
        For Each rItem As RepeaterItem In pro_rep.Items
            ctxtBox = DirectCast(rItem.FindControl("c_txt"), HtmlInputText)
            If Not IsNothing(ctxtBox) Then
                If ctxtBox.Value.Length > 0 Then
                    ctxt = ctxtBox.Value.ToString()
                End If
            End If
        Next
        con.Open()
        cmd = New sqlCommand("UPDATE userinfo SET [city] = @ctxt WHERE ([profile_id] = " & mysess & ")", con)
        cmd.Parameters.Add("@ctxt", SqlDbType.NVarChar)
        cmd.Parameters("@ctxt").Value = ctxt.Replace("'", "''").ToString()
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Settings.aspx")
    End Sub

    Protected Sub save_sex(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))

        For Each rItem As RepeaterItem In pro_rep.Items
            Dim atxtBox As RadioButtonList
            atxtBox = DirectCast(rItem.FindControl("Radiobuttonlist3"), RadioButtonList)
            If Not IsNothing(atxtBox) Then
                If atxtBox.SelectedValue = "male" Then
                    con.Open()
                    cmd = New sqlCommand("UPDATE userinfo SET [sex] = 'male' WHERE ([profile_id] = " & mysess & ")", con)
                    cmd.ExecuteNonQuery()
                    con.Close()
                End If
                If atxtBox.SelectedValue = "female" Then
                    con.Open()
                    cmd = New sqlCommand("UPDATE userinfo SET [sex] = 'female' WHERE ([profile_id] = " & mysess & ")", con)
                    cmd.ExecuteNonQuery()
                    con.Close()
                End If

            End If

        Next
        Response.Redirect("Settings.aspx")


    End Sub

    Protected Sub save_dob(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim atxtBox As HtmlInputText

        For Each rItem As RepeaterItem In pro_rep.Items
            atxtBox = DirectCast(rItem.FindControl("date_txt"), HtmlInputText)
            If Not IsNothing(atxtBox) Then
                If atxtBox.Value.Length > 0 Then
                    con.Open()
                    cmd = New SqlCommand("UPDATE userinfo SET [dob] =@atxt WHERE ([profile_id] = " & mysess & ")", con)
                    cmd.Parameters.Add("@atxt", SqlDbType.DateTime2)
                    cmd.Parameters("@atxt").Value = DateTime.ParseExact(atxtBox.Value, "dd'/'MM'/'yyyy", Nothing)
                    cmd.ExecuteNonQuery()
                    con.Close()
                End If
            End If
        Next
        
        Response.Redirect("Settings.aspx")


    End Sub '

    Protected Sub save_cell(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim cetxtBox As HtmlInputText
        Dim cetxt As String
        For Each rItem As RepeaterItem In pro_rep.Items
            cetxtBox = DirectCast(rItem.FindControl("cell_txt"), HtmlInputText)
            If Not IsNothing(cetxtBox) Then
                If cetxtBox.Value.Length > 0 Then
                    cetxt = cetxtBox.Value.ToString()
                End If
            End If
        Next
        con.Open()
        cmd = New sqlCommand("UPDATE userinfo SET [cell_no] =@cetxt WHERE ([profile_id] = " & mysess & ")", con)
        cmd.Parameters.Add("@cetxt", SqlDbType.NVarChar)
        cmd.Parameters("@cetxt").Value = cetxt.Replace("'", "''").ToString()

        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Settings.aspx")


    End Sub

    Protected Sub del_test(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument.ToString()
        Dim testid As Integer = commandArgsAccept.ToString
        con.Open()
        cmd = New SqlCommand("DELETE FROM tests WHERE ([test_id] = " & testid & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Settings.aspx")
    End Sub

    Protected Sub save_pw(ByVal Sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))

        con.Open()
        cmd = New sqlCommand("SELECT [password] FROM userinfo WHERE ([profile_id]= " & mysess & ")", con)
        dr = cmd.ExecuteReader()
        If (dr.Read) Then
            Dim fetchedpass As String = Convert.ToString(dr("password"))

            If String.Equals(fetchedpass, olpasswordhere.Value) = True Then
                Dim newpget As String = newp.Value.ToString()
                Dim newpcget As String = newpc.Value.ToString()
                If String.Equals(newpget, newpcget) = True Then

                    cmd = New sqlCommand("UPDATE [userinfo] SET [password] =@newpcget WHERE [profile_id] = " & mysess & "", con)
                    cmd.Parameters.Add("@newpcget", SqlDbType.NVarChar)
                    cmd.Parameters("@newpcget").Value = newpcget.Replace("'", "''").ToString()

                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    Response.Redirect("Settings.aspx")
                Else
                    Response.Write("ERROR n-3")
                End If

            Else
                MsgBox("Password you entered is incorrect.")
            End If
        End If

    End Sub

    Protected Sub block(ByVal Sender As Object, ByVal e As EventArgs)
        If bl_txt.Value.Length > 0 Then
            Dim blname As String
            blname = bl_txt.Value
            con.Open()
            cmd = New sqlCommand("SELECT profile_id FROM userinfo WHERE ([uname] = '" & blname & "')", con)
            If cmd.ExecuteScalar() > 0 Then
                dr = cmd.ExecuteReader
                If (dr.Read) Then
                    Dim mysess As Integer
                    mysess = Convert.ToInt32(Session("idsess"))
                    Dim blnum As Integer
                    blnum = dr(0).ToString()
                    cmd = New sqlCommand("INSERT INTO block_list(blocker, blockee) VALUES (" & mysess & ", " & blnum & ")", con)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("DELETE FROM [feelpals_sys] WHERE ([group_to_following] = " & blnum & " AND [group_to_followers] = " & mysess & ")", con)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("DELETE FROM [feelpals_sys] WHERE ([group_to_followers] = " & blnum & " AND [group_to_following] = " & mysess & ")", con)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()

                End If
                con.Close()
                Response.Redirect("Settings.aspx")
            Else
                Response.Write("No such user exists.")
            End If
        Else
            Response.Write("Please enter the username of the person you'd like to block.")

        End If

    End Sub

    Protected Sub unblock(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As Integer = e.CommandArgument.ToString()
        Dim blsess As Integer = commandArgsAccept.ToString
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("DELETE FROM [block_list] WHERE ([blockee] = " & blsess & " AND [blocker] = " & mysess & ")", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        Response.Redirect("Settings.aspx")
    End Sub

End Class
