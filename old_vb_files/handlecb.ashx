<%@ WebHandler Language="VB" Class="handlecb" %>

Imports System.Data
Imports System.IO
imports system.data.sqlclient
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Web
Imports System.Web.Script.Serialization

Public Class handlecb
    Implements IHttpHandler, System.Web.SessionState.IRequiresSessionState, System.Web.SessionState.IReadOnlySessionState
    
   
    Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
  
    Dim cmd As sqlcommand
    Dim cmd2 As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader
    Dim result As Integer
    Dim tickhandler As String
    
    
    Public Class ticker
        Public Property tick() As String
            Get
                Return m_Id
            End Get
            Set(ByVal value As String)
                m_Id = Value
            End Set
        End Property
        Private m_Id As String
        
    End Class

    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
        
        Dim employee = Convert.ToString(context.Request("tick"))
        context.Response.ContentType = "text/html"
       
        
        
        If Not context.Session("chatsess") Is Nothing Then
           
            GoTo b
        Else
            
            GoTo a
        End If
b:
        Dim thread As Integer
        thread = Convert.ToInt32(context.Session("chatsess"))


        con.Open() 'checklastupdatedon
        cmd = New sqlcommand("SELECT [last_updated_on] FROM [message_threads] WHERE ([message_threads].[thread_id] = " & thread & ")", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Dim oldstr As String = context.Session("last_updated_on").ToString()
            Dim fetchedstr As String = dr("last_updated_on").ToString()
            Dim fetched As Date = Convert.ToDateTime(fetchedstr)
            Dim old As Date = Convert.ToDateTime(oldstr)

            result = DateTime.Compare(old, fetched)
            con.Close()

            If result < 0 Then 'old fetched is earlier than timer fetched
                context.Response.Write("yes")
                context.Session("last_updated_on") = fetched
            Else
                context.Response.Write("no")
            End If
           
        End If
a:

    
    End Sub
    
    Private Function GetData(ByVal tick As String) As ticker
        
        
        Dim employee = New ticker()
        employee.tick = tick
       
        Return employee
    End Function
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
End Class