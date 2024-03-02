Public Class PrintJB


    Dim acscmd As New OleDb.OleDbCommand
    Dim acsda As New OleDb.OleDbDataAdapter
    Dim acscon As OleDb.OleDbConnection = jokenconn()
    Dim acsds As New DataSet
    Dim strsql As String
    Dim strreportname As String

    Public Sub report(ByVal sql As String, ByVal rptname As String)
        Try
            acsds = New DataSet
            strsql = sql
            acscmd.CommandText = strsql
            acscmd.Connection = acscon
            acsda.SelectCommand = acscmd
            acsda.Fill(acsds)

            strreportname = rptname
            Dim strreportpath As String = Application.StartupPath & "\reports\" & strreportname & ".rpt"
            '  Dim strreportpath As String = "C:\Users\DELL\Documents\Visual Studio 2008\Projects\mytest\mytest\bin\reports\" & strreportname & ".rpt"
            If Not IO.File.Exists(strreportpath) Then
                MsgBox("Unable to locate file:" & vbCrLf & strreportpath)

            End If
            Dim reportdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            reportdoc.Load(strreportpath)
            reportdoc.SetDataSource(acsds.Tables(0))

            CrystalReportViewer1.ShowRefreshButton = False
            CrystalReportViewer1.ShowCloseButton = False
            CrystalReportViewer1.ShowGroupTreeButton = False
            CrystalReportViewer1.ReportSource = reportdoc

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
           End Sub

    Private Sub PrintJB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim jborder As String
        Dim da As New OleDb.OleDbDataAdapter
        Dim con As OleDb.OleDbConnection = jokenconn()
        jborder = "Select appendchar & '-' & autoend from tblauto where ID = 1"
        Try
            con.Open()
            da = New OleDb.OleDbDataAdapter(jborder, con)
            Dim ds As New DataSet
            da.Fill(ds, "jobdb")
            Me.Text = ds.Tables("jobdb").Rows(0).Item(0)
        Catch ex As Exception

        End Try
        '  jborder = "Select appendchar & '-' & autoend + incrementvalue from tblauto where ID = 1"
        'report("SELECT Last(custname) AS Name, Last(custcontact) AS Contact, Last(Address) " & _
        '       " as [Home Address], Last(custtel) AS [Telephone No], Last(Qty) as [Quantity], " & _
        '       " Last(Description) as [Item Description], Last(serialno) AS [Serial No], Last(Problem), " & _
        '       " Last(actiontaken) AS [Action Taken], Last(datein) AS [Date In], Last(recievedby) AS [Recieved By], " & _
        '       " Last(tech_incharge) AS [Incharge], Last(transactiontype) AS [Transaction Type], Last(parts) AS [Acquaired Parts], " & _
        '       " Last(labor) AS [Service Fee], Last(pickup) AS [Pick up], Last(delivery) AS Delivered, Last(totalamount) AS [Total Amount], " & _
        '       " Last(i.joborderno) AS [Job Order No], Last(remarks) AS [Job order Remarks] " & _
        '       " FROM tblcustomer AS c, tbliteminfo AS i, tbljobestimate AS j WHERE c.custcode = i.custcode And i.joborderno=j.jobordercode And j.jobordercode='" & Me.Text & "'", "jobreport2")
        report("SELECT custname, custcontact, Address, custtel, qty, description, serialno, problem, actiontaken, datein, recievedby, tech_incharge, transactiontype, parts, labor, pickup, delivery, totalamount, i.joborderno, remarks " & _
               " FROM tblcustomer AS c, tbliteminfo AS i, tbljobestimate AS j WHERE c.custcode=i.custcode And i.joborderno=j.jobordercode And j.jobordercode='" & Me.Text & "'", "jobreport1")

    End Sub
End Class