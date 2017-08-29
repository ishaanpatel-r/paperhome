<%@ WebHandler Language="VB" Class="handlenotifs" %>

Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web
Imports System.Web.Script.Serialization

Public Class handlenotifs
    Implements IHttpHandler, System.Web.SessionState.IRequiresSessionState, System.Web.SessionState.IReadOnlySessionState
    
   
    Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
  
    Dim cmd As sqlcommand
    Dim cmd2 As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader
    
 
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
        
        'Dim employee = Convert.ToString(context.Request("tick"))
        context.Response.ContentType = "text/html"
        Dim notifcount As Integer
        Dim reqcount As Integer
        If context.Session("rfn") Is Nothing Then
           GoTo a
        Else
            GoTo b
        End If
        
        b:
        Dim mysess As Integer
        mysess = Convert.ToInt32(context.Session("idsess"))
        Dim rightnow As Date = Convert.ToDateTime(context.Session("rfn"))
        con.Open() 'checkrequests
        cmd = New SqlCommand("SELECT * FROM ([userinfo] LEFT JOIN [req] ON [userinfo].[profile_id]=[req].[from]) WHERE (([accepted]= 0 AND [to]= " & mysess & " ) AND ([date_time] > '" & rightnow & "'))", con)
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            reqcount = Convert.ToInt16(cmd.ExecuteScalar())
            context.Response.Write("yes")
            dr.Close()
            con.Close()
        Else
            dr.Close()
            con.Close()
          
        End If
        
        con.Open() 'checknotifs
        cmd = New SqlCommand("SELECT * FROM (([activity] LEFT JOIN [userinfo] ON [userinfo].[profile_id]=[activity].[doer]) LEFT JOIN [writes] ON [writes].[writes_id]=[activity].[post]) WHERE (((([doee] = " & mysess & ") OR ([writes].[profile_id] = " & mysess & ")) AND [doer] <> " & mysess & ")  AND ([date_time_written] > '" & rightnow & "')) ORDER BY [date_time_written] DESC", con)
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            con.Close()
            con.Open()
            notifcount = Convert.ToInt16(cmd.ExecuteScalar())
            context.Response.Write("yes")
            MsgBox(notifcount)
            dr.Close()
            con.Close()
        Else
            dr.Close()
            con.Close()
           
        End If
a:
     
    context.Session("rfn") = Date.Now
    End Sub
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
End Class