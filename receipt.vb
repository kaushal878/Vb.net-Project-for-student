Public Class receipt



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

    Private Sub receipt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ornonum As String
        Dim da As New OleDb.OleDbDataAdapter
        Dim con As OleDb.OleDbConnection = jokenconn()
        ornonum = "Select autoend from tblauto where ID = 4"
        Try
            con.Open()
            da = New OleDb.OleDbDataAdapter(ornonum, con)
            Dim ds As New DataSet
            da.Fill(ds, "jobdb")
            Me.Text = ds.Tables("jobdb").Rows(0).Item(0)
        Catch ex As Exception

        End Try

        report("SELECT * FROM tbljobestimate AS j, tblpayments AS p, tblcustomer as c " & _
               " WHERE j.jobordercode=p.jobordercode and c.custcode = p.custcode and p.orno = " & Me.Text & "", "receipt")
    End Sub
End Class