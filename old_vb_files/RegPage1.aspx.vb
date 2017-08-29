Imports System.Data
imports system.data.sqlclient

Public Class _Default
    Inherits System.Web.UI.Page


    Dim con As sqlconnection
    Dim cmd As sqlcommand
    Dim cmd2 As sqlcommand
    Dim cmdsm As sqlcommand
    Dim da As sqldataadapter
    Dim ds As New DataSet
    Dim dr As sqldatareader

    Protected Sub Page_Load(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("con").ConnectionString)
        If (Session("idsess") <> 0) Then
           
            Dim mysess As Integer
            mysess = Convert.ToInt32(Session("idsess"))
            con.Open()
            cmd = New sqlcommand("SELECT uname FROM [userinfo] WHERE [profile_id] = " & mysess & "", con)
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If (dr.Read) Then
                Response.Redirect("Home.aspx")
            End If
        End If
    End Sub

    Protected Sub sub1(ByVal Sender As Object, ByVal e As EventArgs)
        Dim ftxt As String
        ftxt = fname.Value.ToString()
        Session("fname") = ftxt
        Dim ltxt As String
        ltxt = lname.Value.ToString()
        Dim dobtext As Date
        dobtext = DateTime.ParseExact(dobtxt.Value, "dd'/'MM'/'yyyy", Nothing)
        Dim emtxt As String
        emtxt = emailtxt.Value.ToString()
        Session("emailid") = emtxt
        Dim gen As String
        gen = Radiobuttonlist3.SelectedValue.ToString()
        Dim cityt As String
        cityt = citxt.Value.ToString()
        Dim cotyt As String
        cotyt = cotxt.Value.ToString()
        con.Open()
        cmd = New SqlCommand("INSERT INTO userinfo(fname, lname, dob, city, country, sex, email) Values(@fname,@lname,@dob,@city,@country,@sex,@email)")
        cmd.Parameters.Add("@fname", SqlDbType.NVarChar)
        cmd.Parameters("@fname").Value = ftxt.Replace("'", "''").ToString()
        cmd.Parameters.Add("@lname", SqlDbType.NVarChar)
        cmd.Parameters("@lname").Value = ltxt.Replace("'", "''").ToString()
        cmd.Parameters.Add("@dob", SqlDbType.DateTime2)
        cmd.Parameters("@dob").Value = DateTime.ParseExact(dobtxt.Value, "dd'/'MM'/'yyyy", Nothing)
        cmd.Parameters.Add("@city", SqlDbType.NVarChar)
        cmd.Parameters("@city").Value = cityt.Replace("'", "''").ToString()
        cmd.Parameters.Add("@country", SqlDbType.NVarChar)
        cmd.Parameters("@country").Value = cotyt.Replace("'", "''").ToString()
        cmd.Parameters.Add("@sex", SqlDbType.NVarChar)
        cmd.Parameters("@sex").Value = gen.Replace("'", "''").ToString()
        cmd.Parameters.Add("@email", SqlDbType.NVarChar)
        cmd.Parameters("@email").Value = emtxt.Replace("'", "''").ToString()
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        con.Open()
        cmd = New sqlcommand("SELECT [profile_id] FROM userinfo WHERE ([email] = '" & emtxt & "')", con)
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            Session("nidsess") = dr("profile_id")
            con.Close()
            For i = 1 To 3
                'music
                con.Open()
                cmd = New sqlcommand("INSERT INTO music_junc(profile_id, music_d_id) Values(" & Convert.ToInt32(Session("nidsess")) & ", '85')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()

                'movies
                con.Open()
                cmd = New sqlcommand("INSERT INTO movies_junc(profile_id, movies_d_id) Values(" & Convert.ToInt32(Session("nidsess")) & ", '8')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()

                'books
                con.Open()
                cmd = New sqlcommand("INSERT INTO books_junc(profile_id, books_d_id) Values(" & Convert.ToInt32(Session("nidsess")) & ", '9')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()

                'food
                con.Open()
                cmd = New sqlcommand("INSERT INTO food_junc(profile_id, food_d_id) Values(" & Convert.ToInt32(Session("nidsess")) & ", '11')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()

                'tv
                con.Open()
                cmd = New sqlcommand("INSERT INTO tv_junc(profile_id, tv_d_id) Values(" & Convert.ToInt32(Session("nidsess")) & ", '6')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()

                'team
                con.Open()
                cmd = New sqlcommand("INSERT INTO team_junc(profile_id, team_d_id) Values(" & Convert.ToInt32(Session("nidsess")) & ", '7')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()

                'places
                con.Open()
                cmd = New sqlcommand("INSERT INTO places_junc(profile_id, places_d_id) Values(" & Convert.ToInt32(Session("nidsess")) & ", '9')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()

                'hobbies
                con.Open()
                cmd = New sqlcommand("INSERT INTO int_fav_junc(profile_id, int_fav_id) Values(" & Convert.ToInt32(Session("nidsess")) & ", '8')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()

                'interests
                con.Open()
                cmd = New sqlcommand("INSERT INTO int_junc(profile_id, int_id) Values(" & Convert.ToInt32(Session("nidsess")) & ", '9')", con)
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
            Next
           
            Response.Redirect("RegPage2.aspx")
        End If
        con.Close()

        Response.Redirect("RegPage1.aspx")


    End Sub
   

End Class
