Imports System.Data
Imports System.Data.SqlClient

Partial Class Default2
    Inherits System.Web.UI.Page


    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        Session("postid") = Request.QueryString("id")
        Dim poid As Integer

        Try 'try catch to catch any unregistered profileids in url
            poid = Convert.ToInt32(Session("postid"))
            If poid = 0 Then
                Response.Redirect("Home.aspx")
            Else
                Dim mysess As Integer = Session("idsess")
                Dim psessinit As Integer = Convert.ToInt32(Session("postid"))
                con.Open()
                cmd = New SqlCommand("SELECT * FROM writes WHERE ([writes_id]= " & psessinit & " AND [profile_id] = " & mysess & ")", con)
                cmd.Connection = con
                dr = cmd.ExecuteReader
                If (dr.Read) Then
                    dr.Close()
                    poid = psessinit
                Else
                    dr.Close()
                    Response.Redirect("Home.aspx")
                End If
                con.Close()
            End If
        Catch
            Response.Redirect("Home.aspx")
        End Try

        If Page.IsPostBack = False Then
            con.Open()
            cmd = New SqlCommand("SELECT DISTINCT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [feelpals_sys] on [writes].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([writes_id] = " & poid & ")", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                Repeater1.DataSource = cmd.ExecuteReader()
                dr.Close()
                Repeater1.DataBind()
            Else
                Response.Redirect("Home.aspx")
                con.Close()
            End If
        End If
    End Sub

    Protected Sub saveeditedpost(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim poid As Integer = Request.QueryString("id")
        Dim feeltxtbox As HtmlInputText
        Dim contenttxtbox As TextBox
        Dim hashtxtbox As TextBox
        For Each rItem As RepeaterItem In Repeater1.Items
            feeltxtbox = DirectCast(rItem.FindControl("feel_txt"), HtmlInputText)
            contenttxtbox = DirectCast(rItem.FindControl("content_txt"), TextBox)
            hashtxtbox = DirectCast(rItem.FindControl("hash_txt"), TextBox)
          
        Next
        con.Open()
        cmd = New SqlCommand("UPDATE [writes] SET [feeling_type] = @feel WHERE ([writes_id] = " & poid & ")", con)
        cmd.Parameters.Add("@feel", SqlDbType.NVarChar)
        cmd.Parameters("@feel").Value = feeltxtbox.Value.ToString().Replace("'", "''")
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New SqlCommand("UPDATE [writes] SET [hashes] = @hashes WHERE ([writes_id] = " & poid & ")", con)
        cmd.Parameters.Add("@hashes", SqlDbType.NVarChar)
        cmd.Parameters("@hashes").Value = hashtxtbox.Text.ToString().Replace("'", "''")
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New SqlCommand("UPDATE [writes] SET [content] = @cont WHERE ([writes_id] = " & poid & ")", con)
        cmd.Parameters.Add("@cont", SqlDbType.NVarChar)
        cmd.Parameters("@cont").Value = contenttxtbox.Text.ToString().Replace("'", "''")
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub

End Class
