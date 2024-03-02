Public Class AllReport

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
    Private Sub AllReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        radreport()


    End Sub
    Public Sub radreport()
        If Reports.RadioButton1.Checked = True Then
            report("Select * from tbljobestimate", "jobestimates")
        ElseIf Reports.RadioButton2.Checked = True Then
            report("Select * from tblpayments", "paymentlist")
        ElseIf Reports.RadioButton3.Checked = True Then
            report("Select * from tbljobestimate where settled = yes", "jobestimates")
        ElseIf Reports.RadioButton4.Checked = True Then
            report("Select * from tbljobestimate where settled = no", "jobestimates")
        ElseIf Reports.RadioButton5.Checked = True Then
            report("Select * from tbliteminfo", "endorseditem")
        Else
            report("SELECT tblcustomer.ID,custname,custcontact,Address,custtel , " & _
                        " jobordercode as [Job Order],Orno as [Or No],curdate as [Date],Cashier, " & _
                        " Amount FROM tblcustomer LEFT JOIN tblpayments ON tblcustomer.custcode =tblpayments.custcode where tblcustomer.custname like'%" & Reports.TextBox1.Text & "%'", "custpayment")
        End If

    End Sub
End Class