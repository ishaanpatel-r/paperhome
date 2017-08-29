Imports System.Data
Imports System.IO
Imports System.Data.OleDb
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Threading
Imports System.Threading.Tasks



Public Class _Default
    Inherits System.Web.UI.Page


    Dim con As OleDbConnection
    Dim cmd As OleDbCommand
    Dim cmd2 As OleDbCommand
    Dim da As OleDbDataAdapter
    Dim ds As New DataSet
    Dim dr As OleDbDataReader
    Public sea As String 'searchstring
    Public urlbuild_array As New ArrayList() 'url string array

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ishaan Patel\Desktop\Paperhome\paperhome_data.accdb")
        If (Session("idsess") Is Nothing) Then
            Response.Redirect("MainPage.aspx")
            Response.Write("Please Log In")
        End If

        'School
        If Page.IsPostBack = True Then
            'If main.SelectedValue = 1 Then
            '    urlbuild_array.Clear()
            '    urlbuild_array.Add("books/school/")

            '    schooldiv.Visible = True
            '    boardsel.Visible = True
            '    stdsel.Visible = False
            '    subsel2.Visible = False

            '    If boardsel.SelectedValue <> 0 Then
            '        urlbuild_array.Add(boardsel.SelectedItem)
            '        urlbuild_array.Add("/")

            '        stdsel.Visible = True

            '        If stdsel.SelectedValue <> 0 Then

            '            urlbuild_array.Add(stdsel.SelectedItem)
            '            urlbuild_array.Add("/")

            '            subsel2.Visible = True
            '            If subsel2.Visible = True Then
            '                MsgBox(subsel2.Text)

            '            Else
            '                MsgBox("d")
            '            End If

            '            'code to search within directory 'For Schools
            '            Dim inputstr As String = subsel2.Text


            '            Dim str3 As String = String.Join("", urlbuild_array.ToArray())
            '            Dim str As String = str3.Trim("")


            '            Session("searchurl") = str
            '        Else
            '            subsel2.Visible = True
            '        End If
            '    Else
            '        stdsel.Visible = False
            '    End If
            'Else
            '    schooldiv.Visible = False
            '    boardsel.Visible = False
            '    stdsel.Visible = False
            '    subsel2.Visible = False
            'End If

            'College

            If main.SelectedValue = 2 Then
                coldiv.Visible = True
                degree.Visible = True
                selyear.Visible = False
                bscfield.Visible = False
                mscfield.Visible = False
                subsel2.Visible = False
                If degree.SelectedValue = 1 Then
                    urlbuild_array.Clear()
                    urlbuild_array.Add("books/college/")
                    urlbuild_array.Add(degree.SelectedItem)
                    urlbuild_array.Add("/")

                    bscfield.Visible = True
                    If bscfield.SelectedValue <> 0 Then
                        urlbuild_array.Add(bscfield.SelectedItem)
                        urlbuild_array.Add("/")

                        selyear.Visible = True
                        If selyear.SelectedValue <> 0 Then
                            urlbuild_array.Add(selyear.SelectedItem)
                            urlbuild_array.Add("/")

                            subsel2.Visible = True


                            Dim str1 As String = String.Join("", urlbuild_array.ToArray())
                            Dim str2 As String = str1.Trim(" ")

                            Session("searchurl") = str2
                        Else
                            subsel2.Visible = False
                        End If
                    Else
                        selyear.Visible = False
                    End If
                ElseIf degree.SelectedValue = 2 Then
                    urlbuild_array.Clear()
                    urlbuild_array.Add("books/college/")
                    urlbuild_array.Add(degree.SelectedItem)
                    urlbuild_array.Add("/")

                    mscfield.Visible = True
                    If mscfield.SelectedValue <> 0 Then
                        urlbuild_array.Add(mscfield.SelectedItem)
                        urlbuild_array.Add("/")

                        selyear.Visible = True
                        If selyear.SelectedValue <> 0 Then
                            urlbuild_array.Add(selyear.SelectedItem)
                            urlbuild_array.Add("/")

                            subsel2.Visible = True

                            'code to search within directory 'For college
                            If subsel2.Visible = True Then

                                Dim str12 As String = String.Join("", urlbuild_array.ToArray())
                                Dim str22 As String = str12.Trim(" ")

                                Session("searchurl") = str22
                                Dim inputstr As String = subsel2.Text


                            End If
                        Else
                            subsel2.Visible = False
                        End If
                    Else
                        selyear.Visible = False
                    End If
                Else
                    selyear.Visible = False
                    bscfield.Visible = False
                    mscfield.Visible = False
                End If
            Else
                coldiv.Visible = False
                degree.Visible = False
                selyear.Visible = False
                bscfield.Visible = False
                mscfield.Visible = False
                subsel2.Visible = False
            End If
        End If

        If Page.IsPostBack = False Then
            con.Open() 'load  following list
            cmd = New OleDbCommand("SELECT * FROM library_data", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                con.Close()
                con.Open()
                bookdisp.DataSource = cmd.ExecuteReader()
                bookdisp.DataBind()
            End If
            con.Close()
        End If
        con.Open()
        Dim rn As Date
        rn = Date.Now
        cmd = New OleDbCommand("SELECT * FROM [livesessions] LEFT JOIN [userinfo] ON [userinfo].[profile_id] = [livesessions].[host_id] WHERE [end_time] > #" & rn & "#", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If dr.Read Then
            con.Close()
            con.Open()
            livefill.DataSource = cmd.ExecuteReader()
            livefill.DataBind()

        End If
        con.Close()
    End Sub

    Protected Sub txtSearch_KeyUp(ByVal Sender As Object, ByVal e As EventArgs)

        sea = seahome.Text

        con.Open()
        cmd = New OleDbCommand("SELECT DISTINCT * FROM [library_data] WHERE [book_name] LIKE '%" & sea & "%' ", con)
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            gsearched.Visible = False
            mainprimary.Visible = True
            bookdisp.Visible = True
            bookdisp.DataSource = cmd.ExecuteReader()
            bookdisp.DataBind()
        Else
            bookdisp.Visible = False

        End If

        con.Close()
    End Sub

    Protected Sub createnew(ByVal Sender As Object, ByVal e As EventArgs)
        Dim subname As String
        subname = sub_txt.Value
        Dim intDay As Date
        intDay = Date.Now
        Dim endDay As Date
        endDay = Date.Now.AddDays(1)
        Dim mysess As Integer
        mysess = Convert.ToInt32(Session("idsess"))
        con.Open()
        cmd = New OleDbCommand("INSERT INTO livesessions(sub_name, host_id, start_time, end_time) VALUES('" & subname & "', " & mysess & ", '" & intDay & "', '" & endDay & "')", con)
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New OleDbCommand("SELECT TOP 1 session_id FROM livesessions WHERE (([host_id] = " & mysess & ") AND ([end_time] = #" & endDay & "#)) ORDER BY [session_id] DESC", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Session("ljpsession") = dr("session_id")
            Response.Redirect("LiveJoinProjection.aspx")
        Else
            Response.Write("damn nigga")
        End If
        con.Close()
    End Sub

    Protected Sub joinsession(ByVal Sender As Object, ByVal e As CommandEventArgs)
        Dim commandArgsAccept As String = e.CommandArgument.ToString()
        Dim value1 As String = commandArgsAccept.ToString
        Session("ljjsession") = value1
        Response.Redirect("LiveJoinSession.aspx?id=" & value1)
    End Sub

    Protected Sub refbookdata(ByVal Sender As Object, ByVal e As EventArgs)
        Dim searchdir As String = "books"

        For Each filestr As String In Directory.EnumerateFiles(Server.MapPath(searchdir), "*.pdf")
            Dim a As Integer = Server.MapPath(searchdir).Length
            Dim filename = filestr.Substring(a)
            MsgBox(filestr)

            'con.Open()
            'cmd = New OleDbCommand("INSERT INTO [library_data](book_name, file_path) VALUES('" & filename & "','" & searchdir & "')", con)
            'cmd.Connection = con
            'cmd.ExecuteNonQuery()
            'con.Close()
        Next
    End Sub

    Protected Sub gsearch(ByVal Sender As Object, ByVal e As EventArgs)
        If subsel2.Text.Length = 0 Then
            gsearched.Visible = False
            mainprimary.Visible = True

            Dim searchdirinit As String = Convert.ToString(Session("searchurl"))
            If Not Directory.Exists(Server.MapPath(searchdirinit)) Then
                Directory.CreateDirectory(Server.MapPath(searchdirinit))
            End If

            con.Open()
            Dim rn As Date
            rn = Date.Now
            cmd = New OleDbCommand("SELECT * FROM [library_data] WHERE [file_path] LIKE '%" & searchdirinit & "%'", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If dr.Read Then
                con.Close()
                con.Open()
                bookdisp.DataSource = cmd.ExecuteReader()
                bookdisp.DataBind()
            Else
                Dim str As String = "No books exist there!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & str & "')", True)
            End If
            con.Close()
        Else
            gsearched.Visible = True
            mainprimary.Visible = False

            Dim inputstr As String = subsel2.Text
            Dim seastring = "(?<![\w\d])" & inputstr & "(?![\w\d])" 'wildcards help pick the whole word and not parts of words
            Dim r As New Regex(seastring, RegexOptions.IgnoreCase)
            Dim seanum As Integer = 0
            Dim maxcount As Integer = 0
            Dim i As Integer = 0
            Dim searchdir As String = Convert.ToString(Session("searchurl"))

            If Not Directory.Exists(Server.MapPath(searchdir)) Then
                Directory.CreateDirectory(Server.MapPath(searchdir)) 'creating directory if it doesnt exist
                File.Create(Server.MapPath(searchdir) & "blah.txt").Close() 'creating and closing a dummy file to avoid null reference while in search for-loop ("No files found inside the folder!") '**
            End If

            For Each filestr As String In Directory.EnumerateFiles(Server.MapPath(searchdir), "*.pdf") '**
                Dim textsea As New StringBuilder()
                'IF-ELSE used to differentiate if the user wants to search (1)precisely or (2)quickly
                If advsea.Checked Then
                    'seeks only the first three files with highest word counts
                    If i > 2 Then
                        GoTo endsearch
                    End If

                    'this is a parsing method- perfectly accurate- but slower!
                    Dim pdfReader As New PdfReader(filestr)
                    For page As Integer = 1 To pdfReader.NumberOfPages
                        Dim strategy As ITextExtractionStrategy = New SimpleTextExtractionStrategy()
                        Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
                        textsea.Append(currentText)
                    Next
                    pdfReader.Close()
                Else
                    'this is a normal reader which doesn't parse- very inaccurate- very fast though!
                    Using streamReader As New StreamReader(filestr, Encoding.UTF8)
                        textsea.Append(streamReader.ReadToEnd())
                    End Using
                End If

                Dim matches As MatchCollection = r.Matches(textsea.ToString()) 'finds matches filewise for each filelocation
                Dim count As Integer = matches.Count 'gets count of matches for each filelocation
                seanum = seanum + 1 'increments number of books searched

                If count > 0 Then
                    'increments number of searchresults
                    i = i + 1

                    'creates book name and link '**
                    Dim link As New HyperLink
                    Dim a As Integer = Server.MapPath(searchdir).Length
                    link.Text = filestr.Substring(a)
                    link.NavigateUrl = filestr


                    'this creates a css-book item
                    Dim newitem As New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
                    newitem.Attributes("class") = "col-sm-3 item"
                    Dim innerthumb As New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
                    innerthumb.Attributes("class") = "col-sm-12 thumbnail panel-info"
                    newitem.Controls.Add(innerthumb)
                    Dim diaryholder As New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
                    diaryholder.Attributes("class") = "diary-holder col-sm-offset-3"
                    innerthumb.Controls.Add(diaryholder)
                    Dim diarycont As New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
                    diarycont.Attributes("class") = "diary-container"
                    diaryholder.Controls.Add(diarycont)
                    diarycont.InnerHtml = "<a href='" & searchdir & filestr.Substring(a) & "' target='_blank'><div class='diary'><div class='diary-cover'><div class='cover'></div></div><div class='diary-spine'><h1><marquee>'" & inputstr & "' word count:" & count & "</marquee></h1></div></div></a>"
                    Dim breakline As New System.Web.UI.HtmlControls.HtmlGenericControl("br")
                    innerthumb.Controls.Add(breakline)
                    Dim mainthumbname As New System.Web.UI.HtmlControls.HtmlGenericControl("h5")
                    mainthumbname.Style("overflow") = "hidden"
                    mainthumbname.Style("white-space") = "nowrap"
                    mainthumbname.Style("text-overflow") = "clip"
                    mainthumbname.InnerHtml = "&nbsp;&nbsp;" & filestr.Substring(a) & "<br />"
                    innerthumb.Controls.Add(mainthumbname)

                    'this takes care of sorting the list from max word count to least
                    If maxcount < count Then
                        gsearched.Controls.AddAt(1, newitem)
                        maxcount = count
                    Else
                        gsearched.Controls.Add(newitem)
                    End If
                End If
                Thread.Sleep(1000)
                bup.Update()
            Next
endsearch:
            'shows end-results with alerts
            If i > 0 Then
                Dim n As String
                n = "Books searched: " & seanum & "<br />Books found: " & i & " ! Yay!"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrsuccess('" & n & "')", True)
            Else
                Dim o As String
                o = "Books searched: " & seanum & "<br />No books found! :c"
                ScriptManager.RegisterStartupScript(Me, [GetType](), "text", "showtoastrinfo('" & o & "')", True)
            End If
        End If

    End Sub

End Class
