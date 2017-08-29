Imports System.Data
Imports System.Data.SqlClient
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
Imports System.IO
Imports Microsoft.Office.Interop



Partial Class Default3
    Inherits System.Web.UI.Page

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim cmd2 As SqlCommand
    Dim cmd3 As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Public sea As String 'searchstring


    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim a As New Word.Application
        'Dim b As Word.Document

        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If

        Dim thread As Integer
        thread = Convert.ToInt32(Context.Session("chatsess"))


        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New sqlCommand("SELECT TOP 1 [writes].[feeling_type] FROM [writes]  WHERE [profile_id] = " & mysess & " GROUP BY [writes].[feeling_type] ORDER BY Count([writes].[feeling_type]) DESC", con)
        dr = cmd.ExecuteReader()
        If dr.Read Then
            Session("1stmodefeel") = dr(0).ToString()
            dr.Close()
        End If
        con.Close()
        con.Open()
        cmd2 = New sqlCommand("SELECT [writes].[feeling_type] FROM [writes]  WHERE (([profile_id] = " & mysess & ") AND ([writes].[feeling_type] = (SELECT MAX([writes].[feeling_type]) FROM [writes] WHERE (([writes].[feeling_type] <> (SELECT MAX([writes].[feeling_type]) FROM [writes]) AND ([profile_id] = " & mysess & ")))))) GROUP BY [writes].[feeling_type] ORDER BY Count([writes].[feeling_type]) DESC", con)
        dr = cmd2.ExecuteReader()
        If dr.Read Then
            Session("2ndmodefeel") = dr(0).ToString()
            dr.Close()
        End If

        Dim stmodefeel As String
        stmodefeel = Convert.ToString(Session("1stmodefeel"))
        Dim ndmodefeel As String
        ndmodefeel = Convert.ToString(Session("2ndmodefeel"))
        con.Close()
        con.Open()
        cmd3 = New SqlCommand("UPDATE [userinfo] SET [mode_1] = '" & stmodefeel & "', [mode_2] = '" & ndmodefeel & "' WHERE [userinfo].[profile_id] = " & mysess & "", con)
        cmd3.Connection = con
        cmd3.ExecuteNonQuery()


        If Page.IsPostBack = False Then
            Dim loopfet As Integer = 2

            Dim posno As Integer
            posno = loopfet * 10
            'load  feed
            cmd = New SqlCommand("SELECT TOP " & posno & " [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [feelpals_sys] on [writes].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([published] = 1 AND [group_to_following] = " & mysess & ") ORDER BY [date_written] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                Repeater1.DataSource = cmd.ExecuteReader()
                dr.Close()
                Repeater1.DataBind()
            Else
                dr.Close()
                Session("nonexist") = "1"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "nonexist();", True)


                con.Close()
            End If




        End If
        con.Close()
        con.Open()
        'load  notif
        cmd = New SqlCommand("SELECT TOP 10 [profile_id],[type], [uname], [date_time_written], [post], [doer], [doee] FROM (([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([group_to_following] = " & mysess & ") ORDER BY [date_time_written] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader


        If (dr.Read) Then

            con.Close()
            con.Open()

            notifrepeater.DataSource = cmd.ExecuteReader()
            dr.Close()
            notifrepeater.DataBind()

        End If
        con.Close()
    End Sub


    '<WebMethod> _
    'Public Shared Function GetData() As String
    'con.Open()
    'cmd = New SqlCommand("SELECT TOP " & posno & " [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [feelpals_sys] on [writes].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([published] = 1 AND [group_to_following] = " & mysess & ") ORDER BY [date_written] DESC", con)
    'cmd.Connection = con
    'dr = cmd.ExecuteReader
    'If (dr.Read) Then
    '    con.Close()
    '    con.Open()
    '    Repeater1.DataSource = cmd.ExecuteReader()
    '    dr.Close()
    '    Repeater1.DataBind()
    'Else
    '    dr.Close()
    '    Session("nonexist") = "1"
    '    ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "nonexist();", True)


    '    con.Close()
    'End If
    'RecordCount = RecordCount + 10
    'Dim Sql As String = "SELECT Title, DateCreated, Slug FROM be_Posts ORDER BY Title OFFSET " + FirstCount + " ROWS FETCH NEXT 10 ROWS ONLY"
    'FirstCount = RecordCount
    'Dim sb As New StringBuilder()
    'dt = New DataTable()
    'da = New SqlCeDataAdapter(Sql, con)
    'con.Open()
    'da.Fill(dt)

    'Dim dv As DataView = dt.DefaultView

    'For Each row As DataRowView In dv
    '    sb.AppendFormat("<p>Post Title" + " <strong>" + row("Title") + "</strong>")
    '    sb.AppendFormat("<p>Post Date" + " <strong>" + row("DateCreated") + "</strong>")
    '    sb.AppendFormat("<p>Slug" + " <strong>" + row("Slug") + "</strong>")
    '    sb.AppendFormat("<hr/>")
    'Next

    'sb.AppendFormat("<divstyle='height:15px;'></div>")
    'con.Close()
    'Return sb.ToString()
    'End Function

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================


    Protected Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        Dim subRepeter As Repeater = DirectCast(e.Item.FindControl("Repeater2"), Repeater)
        Dim writesid As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "writes_id"))
        Dim con As SqlConnection
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader
        con.Open() 'load  notif
        cmd = New SqlCommand("SELECT [userinfo].[uname], [comments].[content_c],[comments].[comment_id],[comments].[date_written_c],[userinfo].[dp_url], [userinfo].[profile_id] FROM ([comments] LEFT JOIN [userinfo] ON [comments].[poster] = [userinfo].[profile_id]) WHERE [writes_id_c]= " & writesid & " ORDER BY [date_written_c] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            dr.Close()
            con.Close()
            con.Open()
            subRepeter.DataSource = cmd.ExecuteReader()
            subRepeter.DataBind()
        Else
            dr.Close()
        End If
        con.Close()
    End Sub

    Protected Sub postwrite(ByVal Sender As Object, ByVal e As EventArgs)

        If date_txt.Value = "" Then
            Dim err As String
            err = "Please enter a date you\'re writing this memory for!"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & err & "')", True)
            GoTo A
        End If
        If ftxt.Value = "" Then
            Dim err As String
            err = "You need to assign a feeling to this memory."
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & err & "')", True)
            GoTo A
        End If
        If hashtxt.Value = "" Then
            Dim err As String
            err = "Need #Hashtags. :3"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & err & "')", True)
            GoTo A
        End If
        If posttxt.Text.Length = 0 Then
            Dim err As String
            err = "You gotta write something, right?"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & err & "')", True)
            GoTo A
        End If

        Dim mysess As String
        mysess = Convert.ToString(Session("idsess"))
        Dim posdate As Date
        posdate = DateTime.ParseExact(date_txt.Value, "dd'/'MM'/'yyyy", Nothing)
        Dim poscon As String
        poscon = posttxt.Text.ToString().Replace("'", "''")
        Dim posfeel As String
        posfeel = ftxt.Value.ToString()
        Dim poshash As String
        poshash = hashtxt.Value.ToString()
        Dim intDay As Date
        intDay = Date.Now



        con.Open()
        If publiccheck.Checked Then
            If FileUpload1.HasFile Then
                If happycheck.Checked Then 'published, hasfile & happy
                    If Not Directory.Exists(Server.MapPath("img\" + mysess + "\" + "writes_pictures")) Then
                        Directory.CreateDirectory(Server.MapPath("img\" + mysess + "\" + "writes_pictures"))
                    End If

                    FileUpload1.SaveAs(Server.MapPath("img\" + mysess + "\" + "writes_pictures\" + FileUpload1.FileName))

                    cmd = New SqlCommand("INSERT INTO writes(profile_id, date_written, content, feeling_type, hashes, published, img_att, happy) VALUES('" & mysess & "',@posdate,@poscon,@posfeel,@poshash,1, '" & "img\" + mysess + "\" + "writes_pictures\" + FileUpload1.FileName & "', 1)", con)
                    cmd.Parameters.Add("@posdate", SqlDbType.DateTime2)
                    cmd.Parameters("@posdate").Value = DateTime.ParseExact(date_txt.Value, "dd'/'MM'/'yyyy", Nothing)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@posfeel", SqlDbType.NVarChar)
                    cmd.Parameters("@posfeel").Value = posfeel.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@poshash", SqlDbType.NVarChar)
                    cmd.Parameters("@poshash").Value = poshash.Replace("'", "''").ToString()


                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    'activity input'
                    con.Open()
                    cmd = New SqlCommand("SELECT [writes_id] FROM writes WHERE ([content] = @poscon AND [profile_id] = " & mysess & ")", con)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()

                    cmd.Connection = con
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Session("id") = dr("writes_id")
                        dr.Close()
                        con.Close()
                        con.Open()
                        Dim postid As Integer
                        postid = Convert.ToInt32(Session("id"))
                        cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'wrote a new', '" & intDay & "', " & postid & ") ", con)
                        cmd.Connection = con
                        cmd.ExecuteNonQuery()
                    Else
                        dr.Close()
                        Response.Write("Broke.")
                    End If
                Else 'published & hasfile
                    If Not Directory.Exists(Server.MapPath("img\" + mysess + "\" + "writes_pictures")) Then
                        Directory.CreateDirectory(Server.MapPath("img\" + mysess + "\" + "writes_pictures"))
                    End If

                    FileUpload1.SaveAs(Server.MapPath("img\" + mysess + "\" + "writes_pictures\" + FileUpload1.FileName))

                    cmd = New SqlCommand("INSERT INTO writes(profile_id, date_written, content, feeling_type, hashes, published, img_att) VALUES('" & mysess & "',@posdate,@poscon,@posfeel,@poshash, 1, '" & "img\" + mysess + "\" + "writes_pictures\" + FileUpload1.FileName & "')", con)
                    cmd.Parameters.Add("@posdate", SqlDbType.DateTime2)
                    cmd.Parameters("@posdate").Value = DateTime.ParseExact(date_txt.Value, "dd'/'MM'/'yyyy", Nothing)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@posfeel", SqlDbType.NVarChar)
                    cmd.Parameters("@posfeel").Value = posfeel.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@poshash", SqlDbType.NVarChar)
                    cmd.Parameters("@poshash").Value = poshash.Replace("'", "''").ToString()


                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    'activity input'
                    con.Open()
                    cmd = New SqlCommand("SELECT [writes_id] FROM writes WHERE ([content] =@poscon AND [profile_id] = " & mysess & ")", con)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()


                    cmd.Connection = con
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Session("id") = dr("writes_id")
                        dr.Close()
                        con.Close()
                        con.Open()
                        Dim postid As Integer
                        postid = Convert.ToInt32(Session("id"))
                        cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'wrote a new', '" & intDay & "', " & postid & ") ", con)
                        cmd.Connection = con
                        cmd.ExecuteNonQuery()
                    Else
                        dr.Close()
                        Response.Write("Broke.")
                    End If
                End If
            Else
                If happycheck.Checked Then 'published & happy
                    cmd = New SqlCommand("INSERT INTO writes(profile_id, date_written, content, feeling_type, hashes, published, happy) VALUES('" & mysess & "',@posdate,@poscon,@posfeel,@poshash,1,1)", con)
                    cmd.Parameters.Add("@posdate", SqlDbType.DateTime2)
                    cmd.Parameters("@posdate").Value = DateTime.ParseExact(date_txt.Value, "dd'/'MM'/'yyyy", Nothing)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@posfeel", SqlDbType.NVarChar)
                    cmd.Parameters("@posfeel").Value = posfeel.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@poshash", SqlDbType.NVarChar)
                    cmd.Parameters("@poshash").Value = poshash.Replace("'", "''").ToString()


                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    'activity input'
                    con.Open()
                    cmd = New SqlCommand("SELECT [writes_id] FROM writes WHERE ([content] =@poscon AND [profile_id] = " & mysess & ")", con)

                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()


                    cmd.Connection = con
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Session("id") = dr("writes_id")
                        dr.Close()
                        con.Close()
                        con.Open()
                        Dim postid As Integer
                        postid = Convert.ToInt32(Session("id"))
                        cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'wrote a new', '" & intDay & "', " & postid & ") ", con)
                        cmd.Connection = con
                        cmd.ExecuteNonQuery()
                    Else
                        dr.Close()
                        Response.Write("Broke.")
                    End If
                Else 'published only
                    cmd = New SqlCommand("INSERT INTO writes(profile_id, date_written, content, feeling_type, hashes, published) VALUES('" & mysess & "',@posdate,@poscon,@posfeel,@poshash,1)", con)
                    cmd.Parameters.Add("@posdate", SqlDbType.DateTime2)
                    cmd.Parameters("@posdate").Value = DateTime.ParseExact(date_txt.Value, "dd'/'MM'/'yyyy", Nothing)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@posfeel", SqlDbType.NVarChar)
                    cmd.Parameters("@posfeel").Value = posfeel.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@poshash", SqlDbType.NVarChar)
                    cmd.Parameters("@poshash").Value = poshash.Replace("'", "''").ToString()


                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    'activity input'
                    con.Open()

                    cmd = New SqlCommand("SELECT [writes_id] FROM writes WHERE ([content] =@poscon AND [profile_id] = " & mysess & ")", con)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Connection = con
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Session("id") = dr("writes_id")
                        dr.Close()
                        con.Close()
                        con.Open()
                        Dim postid As Integer
                        postid = Convert.ToInt32(Session("id"))
                        cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'wrote a new', '" & intDay & "', " & postid & ") ", con)
                        cmd.Connection = con
                        cmd.ExecuteNonQuery()
                    Else
                        dr.Close()
                        Response.Write("Broke.")
                    End If
                End If
            End If
        Else
            If FileUpload1.HasFile Then
                If happycheck.Checked Then 'not published, hasfile & happy
                    If Not Directory.Exists(Server.MapPath("img\" + mysess + "\" + "writes_pictures")) Then
                        Directory.CreateDirectory(Server.MapPath("img\" + mysess + "\" + "writes_pictures"))
                    End If

                    FileUpload1.SaveAs(Server.MapPath("img\" + mysess + "\" + "writes_pictures\" + FileUpload1.FileName))

                    cmd = New SqlCommand("INSERT INTO writes(profile_id, date_written, content, feeling_type, hashes, img_att, happy) VALUES('" & mysess & "',@posdate,@poscon,@posfeel,@poshash, '" & "img\" + mysess + "\" + "writes_pictures\" + FileUpload1.FileName & "',1)", con)
                    cmd.Parameters.Add("@posdate", SqlDbType.DateTime2)
                    cmd.Parameters("@posdate").Value = DateTime.ParseExact(date_txt.Value, "dd'/'MM'/'yyyy", Nothing)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@posfeel", SqlDbType.NVarChar)
                    cmd.Parameters("@posfeel").Value = posfeel.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@poshash", SqlDbType.NVarChar)
                    cmd.Parameters("@poshash").Value = poshash.Replace("'", "''").ToString()

                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    'activity input'
                    con.Open()
                    cmd = New SqlCommand("SELECT [writes_id] FROM writes WHERE ([content] = @poscon AND [profile_id] = " & mysess & ")", con)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()

                    cmd.Connection = con
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Session("id") = dr("writes_id")
                        dr.Close()
                        con.Close()
                        con.Open()
                        Dim postid As Integer
                        postid = Convert.ToInt32(Session("id"))
                        cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'wrote a new', '" & intDay & "', " & postid & ") ", con)
                        cmd.Connection = con
                        cmd.ExecuteNonQuery()
                    Else
                        dr.Close()
                        Response.Write("Broke.")
                    End If
                Else 'not published & hasfile
                    If Not Directory.Exists(Server.MapPath("img\" + mysess + "\" + "writes_pictures")) Then
                        Directory.CreateDirectory(Server.MapPath("img\" + mysess + "\" + "writes_pictures"))
                    End If

                    FileUpload1.SaveAs(Server.MapPath("img\" + mysess + "\" + "writes_pictures\" + FileUpload1.FileName))

                    cmd = New SqlCommand("INSERT INTO writes(profile_id, date_written, content, feeling_type, hashes, img_att) VALUES('" & mysess & "',@posdate,@poscon,@posfeel,@poshash, '" & "img\" + mysess + "\" + "writes_pictures\" + FileUpload1.FileName & "')", con)
                    cmd.Parameters.Add("@posdate", SqlDbType.DateTime2)
                    cmd.Parameters("@posdate").Value = DateTime.ParseExact(date_txt.Value, "dd'/'MM'/'yyyy", Nothing)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@posfeel", SqlDbType.NVarChar)
                    cmd.Parameters("@posfeel").Value = posfeel.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@poshash", SqlDbType.NVarChar)
                    cmd.Parameters("@poshash").Value = poshash.Replace("'", "''").ToString()


                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    'activity input'
                    con.Open()
                    cmd = New SqlCommand("SELECT [writes_id] FROM writes WHERE ([content] =@poscon AND [profile_id] = " & mysess & ")", con)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Connection = con
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Session("id") = dr("writes_id")
                        dr.Close()
                        con.Close()
                        con.Open()
                        Dim postid As Integer
                        postid = Convert.ToInt32(Session("id"))
                        cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES(" & mysess & ", 'wrote a new', '" & intDay & "', " & postid & ") ", con)
                        cmd.Connection = con
                        cmd.ExecuteNonQuery()
                    Else
                        dr.Close()
                        Response.Write("Broke.")
                    End If
                End If
            Else
                If happycheck.Checked Then 'not published, nofile & happy
                    cmd = New SqlCommand("INSERT INTO writes(profile_id, date_written, content, feeling_type, hashes, happy) VALUES(" & mysess & ",@posdate,@poscon,@posfeel,@poshash,1)", con)
                    cmd.Parameters.Add("@posdate", SqlDbType.DateTime2)
                    cmd.Parameters("@posdate").Value = DateTime.ParseExact(date_txt.Value, "dd'/'MM'/'yyyy", Nothing)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@posfeel", SqlDbType.NVarChar)
                    cmd.Parameters("@posfeel").Value = posfeel.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@poshash", SqlDbType.NVarChar)
                    cmd.Parameters("@poshash").Value = poshash.Replace("'", "''").ToString()


                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    'activity input'
                    con.Open()
                    cmd = New SqlCommand("SELECT [writes_id] FROM writes WHERE ([content] = @poscon AND [profile_id] = " & mysess & ")", con)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()



                    cmd.Connection = con
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Session("id") = dr("writes_id")
                        dr.Close()
                        con.Close()
                        con.Open()
                        Dim postid As Integer
                        postid = Convert.ToInt32(Session("id"))
                        cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'wrote a new', '" & intDay & "', " & postid & ") ", con)
                        cmd.Connection = con
                        cmd.ExecuteNonQuery()
                    Else
                        dr.Close()
                        Response.Write("Broke.")
                    End If
                Else 'not publsihed & not happy, no file either!
                    cmd = New SqlCommand("INSERT INTO writes(profile_id, date_written, content, feeling_type, hashes) VALUES('" & mysess & "',@posdate,@poscon,@posfeel,@poshash)", con)
                    cmd.Parameters.Add("@posdate", SqlDbType.DateTime2)
                    cmd.Parameters("@posdate").Value = DateTime.ParseExact(date_txt.Value, "dd'/'MM'/'yyyy", Nothing)
                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@posfeel", SqlDbType.NVarChar)
                    cmd.Parameters("@posfeel").Value = posfeel.Replace("'", "''").ToString()
                    cmd.Parameters.Add("@poshash", SqlDbType.NVarChar)
                    cmd.Parameters("@poshash").Value = poshash.Replace("'", "''").ToString()


                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    'activity input'
                    con.Open()
                    cmd = New SqlCommand("SELECT [writes_id] FROM writes WHERE ([content] = '" & poscon & "' AND [profile_id] = " & mysess & ")", con)

                    cmd.Parameters.Add("@poscon", SqlDbType.NVarChar)
                    cmd.Parameters("@poscon").Value = poscon.Replace("'", "''").ToString()



                    cmd.Connection = con
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Session("id") = dr("writes_id")
                        dr.Close()
                        con.Close()
                        con.Open()
                        Dim postid As Integer
                        postid = Convert.ToInt32(Session("id"))
                        cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'wrote a new', '" & intDay & "', " & postid & ") ", con)
                        cmd.Connection = con
                        cmd.ExecuteNonQuery()
                    Else
                        dr.Close()
                        Response.Write("Broke.")
                    End If
                End If
            End If
        End If
        con.Close()

        Dim m As String
        m = "Duly noted! :3"
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)

A:
    End Sub

    Protected Sub delcom(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim m As String
        m = "Comment was deleted."
        ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)

        Dim wrisess As Integer = 0
        Dim commandArgsAccept As Integer = Convert.ToInt32(e.CommandArgument.ToString())

        con.Open() 'reload  comments
        cmd = New SqlCommand("SELECT [writes_id_c] FROM [comments] WHERE [comment_id] = " & commandArgsAccept & "", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            wrisess = dr("writes_id_c")
            dr.Close()
        End If

        con.Close()
        con.Open()
        cmd = New SqlCommand("DELETE FROM [comments] WHERE [comment_id] = " & commandArgsAccept & "", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()

        For Each rItem As RepeaterItem In Repeater1.Items
            Dim subRepeter As Repeater = DirectCast(rItem.FindControl("Repeater2"), Repeater)
            con.Open() 'reload  comments
            cmd = New SqlCommand("SELECT [userinfo].[uname], [comments].[content_c],[comments].[date_written_c],[comments].[comment_id],[userinfo].[dp_url], [userinfo].[profile_id] FROM ([comments] LEFT JOIN [userinfo] ON [comments].[poster] = [userinfo].[profile_id]) WHERE [writes_id_c]= " & wrisess & " ORDER BY [date_written_c] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                dr.Close()
                con.Close()
                con.Open()
                subRepeter.DataSource = cmd.ExecuteReader()
                subRepeter.DataBind()
            Else
                dr.Close()
            End If
            con.Close()
        Next
    End Sub

    Protected Sub like_post(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim writeid As String = commandArgsAccept.ToString
        Session("tempsess") = writeid
        Dim wrisess As String
        wrisess = Convert.ToInt32(Session("tempsess"))
        Dim mysess As String
        mysess = Convert.ToString(Session("idsess"))
        Dim intDay As Date
        intDay = Date.Now

        con.Open()
        cmd = New SqlCommand("SELECT * FROM activity WHERE ([doer]=" & mysess & " AND [type]='liked a' AND [post]=" & wrisess & ")", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Dim m As String
            m = "You\'ve already liked this post."
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & m & "')", True)
            dr.Close()
        Else
            dr.Close()
            cmd = New SqlCommand("INSERT INTO activity(doer, type, post, date_time_written) VALUES('" & mysess & "','liked a', " & wrisess & ", '" & intDay & "')", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            Dim m As String
            m = "Liked! <3"
            ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & m & "')", True)
            con.Close()
        End If
    End Sub

    Protected Sub comment_post(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim wrisess As Integer
        wrisess = e.CommandArgument
        Dim tagexists As Integer = 0
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        Dim intDay As Date
        intDay = Date.Now
        con.Open()
        Dim txtBox As HtmlInputText
        Dim messtxta As String = " "
        For Each rItem As RepeaterItem In Repeater1.Items
            txtBox = DirectCast(rItem.FindControl("Text1"), HtmlInputText)
            If Not IsNothing(txtBox) Then
                If txtBox.Value.Length > 0 Then
                    messtxta = txtBox.Value
                    For Each m As Match In Regex.Matches(messtxta, "@\w+")
                        cmd = New SqlCommand("SELECT [profile_id] FROM [userinfo] WHERE [uname] = @unametag", con)
                        cmd.Parameters.Add("@unametag", SqlDbType.NVarChar)
                        cmd.Parameters("@unametag").Value = m.Value.Trim("@").Replace("'", "''")
                        cmd.Connection = con
                        dr = cmd.ExecuteReader
                        If (dr.Read) Then
                            tagexists = 1
                            Dim idc As Integer = dr("profile_id")
                            con.Close()
                            con.Open()
                            'insert activity with tag for each tag
                            cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, doee, post) VALUES('" & mysess & "', 'tagged', '" & intDay & "', " & idc & ", " & wrisess & ") ", con)
                            cmd.Connection = con
                            cmd.ExecuteNonQuery()
                        End If

                    Next
                    If tagexists = 1 Then
                        tagexists = 0
                        GoTo tagcominput
                    Else
                        GoTo cominput
                    End If
cominput:           'insert comment & activity if tag not found
                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written, post) VALUES('" & mysess & "', 'commented on a', '" & intDay & "', '" & wrisess & "') ", con)
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    con.Close()
                    con.Open()
                    cmd = New SqlCommand("INSERT INTO comments(poster, content_c, date_written_c, writes_id_c) VALUES('" & mysess & "',@comcon, '" & intDay & "', '" & wrisess & "')", con)
                    cmd.Parameters.Add("@comcon", SqlDbType.NVarChar)
                    cmd.Parameters("@comcon").Value = messtxta.Replace("'", "''")
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()
                    txtBox.Value = ""
                    con.Close()

                    GoTo a
                End If
            End If

        Next
tagcominput:  'inserts comment if tag found
        cmd = New SqlCommand("INSERT INTO comments(poster, content_c, date_written_c, Writes_id_c) VALUES('" & mysess & "',@comcon, '" & intDay & "', '" & wrisess & "')", con)
        cmd.Parameters.Add("@comcon", SqlDbType.NVarChar)
        cmd.Parameters("@comcon").Value = messtxta.Replace("'", "''")
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        txtBox.Value = ""
a:      'reloads the comments
        For Each rItem As RepeaterItem In Repeater1.Items
            Dim subRepeter As Repeater = DirectCast(rItem.FindControl("Repeater2"), Repeater)
            con.Open()
            cmd = New SqlCommand("SELECT [comments].[comment_id],[userinfo].[uname], [comments].[content_c],[comments].[date_written_c],[userinfo].[dp_url], [userinfo].[profile_id] FROM ([comments] LEFT JOIN [userinfo] ON [comments].[poster] = [userinfo].[profile_id]) WHERE [writes_id_c]= " & wrisess & " ORDER BY [date_written_c] DESC", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                dr.Close()
                con.Close()
                con.Open()
                subRepeter.DataSource = cmd.ExecuteReader()
                subRepeter.DataBind()
            Else
                dr.Close()
            End If
            con.Close()
        Next

    End Sub

    Protected Sub txtSearch_KeyUp(ByVal Sender As Object, ByVal e As EventArgs)


        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New SqlCommand("SELECT [userinfo].[profile_id], [userinfo].[uname], [writes].[date_written], [writes].[content], [writes].[writes_id], [writes].[hashes], [writes].[feeling_type], [writes].[img_att] FROM (([writes] INNER JOIN [userinfo] ON [writes].[profile_id]=[userinfo].[profile_id]) LEFT JOIN [feelpals_sys] on [writes].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([published] = 1 AND [group_to_following] = " & mysess & " AND (([uname] LIKE '%' + @sea + '%') OR ([fname] LIKE '%' + @sea + '%')  OR ([content] LIKE '%' + @sea + '%') OR ([lname] LIKE '%' + @sea + '%') OR ([hashes] LIKE '%' + @sea + '%') OR ([feeling_type] LIKE '%' + @sea + '%'))) ORDER BY [date_written] DESC", con)

        cmd.Parameters.Add("@sea", SqlDbType.NVarChar)
        cmd.Parameters("@sea").Value = seahome.Text.Replace("'", "''").ToString()

        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()

            Repeater1.Visible = True
            Repeater1.DataSource = cmd.ExecuteReader()
            dr.Close()
            Repeater1.DataBind()
        Else
            Repeater1.Visible = False
            dr.Close()
        End If
        con.Close()
    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open() 'load  notif
        cmd = New SqlCommand("SELECT TOP 10 [profile_id],[type], [uname], [date_time_written], [post], [doer], [doee] FROM (([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) LEFT JOIN [feelpals_sys] ON [userinfo].[profile_id]=[feelpals_sys].[group_to_followers]) WHERE ([group_to_following] = " & mysess & ") ORDER BY [date_time_written] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader


        If (dr.Read) Then

            con.Close()
            con.Open()

            notifrepeater.DataSource = cmd.ExecuteReader()
            notifrepeater.DataBind()
            dr.Close()
        End If
        con.Close()
    End Sub


End Class
