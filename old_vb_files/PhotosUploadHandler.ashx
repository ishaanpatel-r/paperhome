<%@ WebHandler Language="VB" Class="FileUploadHandler" %>

Imports System.Data
Imports System.IO
imports system.data.sqlclient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web

Public Class FileUploadHandler
    Implements IHttpHandler, System.Web.SessionState.IRequiresSessionState

    
    Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
  
    Dim cmd As sqlcommand
    Dim cmd2 As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
       
        Dim files As HttpFileCollection = context.Request.Files
       
        
        If context.Request.Files.Count > 0 Then
            For i As Integer = 0 To files.Count - 1
                Dim file As HttpPostedFile = files(i)
                Dim mysess As Integer
                mysess = Convert.ToInt32(context.Session("idsess"))
                Dim albumname As String
                albumname = Convert.ToString(context.Session("alnum"))
                Dim intDay As Date
                intDay = Date.Now()
                
                If Not Directory.Exists(context.Server.MapPath("~/img/" & mysess & "/" & albumname)) Then
                    Directory.CreateDirectory(context.Server.MapPath("~/img/" & mysess & "/" & albumname))
                End If
                
                'Dim newfilename As String =  intDay & "-" & mysess & "-" & file.FileName
                context.Session("alpath") = context.Server.MapPath("~/img/" & mysess & "/" & albumname & "/" & File.FileName)
                
                Dim fname As String
                fname = context.Session("alpath").ToString()
                File.SaveAs(fname)
                con.Open()
                cmd = New sqlcommand("INSERT INTO picture_library(user_path, folder_type_path, pic_name, add_date) VALUES(" & mysess & ", '" & albumname & "', '" & File.FileName & "', '" & intDay & "')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                con.Open()
                cmd = New SqlCommand("INSERT INTO activity(doer, type, date_time_written) VALUES('" & mysess & "','uploaded new', '" & intDay & "')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Next
            context.Response.ContentType = "text/plain"
            context.Response.Write("File(s) Uploaded Successfully!")
        End If
        
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class