Public Class Reports

   
    Private Sub btnshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshow.Click
        DataGridView2.DataSource = Nothing
        If RadioButton1.Checked = True Then
            jokenselect("SELECT ID,jobordercode as [Job Order],itemcode as [Item Code],Parts,Labor, " & _
                        " pickup as [Pick up],Delivery,totalamount as [Total Amount],Downpayment, " & _
                        " balanceamount as [Balance],Settled,Remarks FROM tbljobestimate")
            fillitemtable(DataGridView1)
            With DataGridView1
                .Columns(3).DefaultCellStyle.Format = "n"
                .Columns(4).DefaultCellStyle.Format = "n"
                .Columns(5).DefaultCellStyle.Format = "n"
                .Columns(6).DefaultCellStyle.Format = "n"
                .Columns(7).DefaultCellStyle.Format = "n"
                .Columns(8).DefaultCellStyle.Format = "n"
                .Columns(9).DefaultCellStyle.Format = "n"

            End With
        ElseIf RadioButton2.Checked = True Then
            jokenselect("SELECT ID,jobordercode as [Job Order],Orno as [Or No],curdate as [Date],Cashier,Amount FROM tblpayments")
            'jokenselect("SELECT ID,jobordercode as [Job Order],curdate as [Date],Cashier,Orno as [Or No],Amount, " & _
            '            " custcode as [Customercode] FROM tblpayments")
            filltotaltable(DataGridView1)
            With DataGridView1
                .Columns(5).DefaultCellStyle.Format = "n"

            End With
        ElseIf RadioButton3.Checked = True Then
            jokenselect("SELECT ID,jobordercode as [Job Order],itemcode as [Item Code],Parts,Labor,pickup as [Pick up], " & _
                        " Delivery,totalamount as [Total Amount],Downpayment,balanceamount as [Balance],Settled, " & _
                        " Remarks FROM tbljobestimate where settled = yes")
            fillitemtable(DataGridView1)
            With DataGridView1
                .Columns(3).DefaultCellStyle.Format = "n"
                .Columns(4).DefaultCellStyle.Format = "n"
                .Columns(5).DefaultCellStyle.Format = "n"
                .Columns(6).DefaultCellStyle.Format = "n"
                .Columns(7).DefaultCellStyle.Format = "n"
                .Columns(8).DefaultCellStyle.Format = "n"
                .Columns(9).DefaultCellStyle.Format = "n"

            End With

        ElseIf RadioButton4.Checked = True Then
            jokenselect("SELECT ID,jobordercode as [Job Order],itemcode as [Item Code],Parts,Labor,pickup as [Pick up], " & _
                        " Delivery,totalamount as [Total Amount],Downpayment,balanceamount as [Balance],Settled, " & _
                        " Remarks FROM tbljobestimate where settled = no")
            fillitemtable(DataGridView1)
            With DataGridView1
                .Columns(3).DefaultCellStyle.Format = "n"
                .Columns(4).DefaultCellStyle.Format = "n"
                .Columns(5).DefaultCellStyle.Format = "n"
                .Columns(6).DefaultCellStyle.Format = "n"
                .Columns(7).DefaultCellStyle.Format = "n"
                .Columns(8).DefaultCellStyle.Format = "n"
                .Columns(9).DefaultCellStyle.Format = "n"

            End With


        ElseIf RadioButton5.Checked = True Then
            jokenselect("SELECT ID, joborderno as [Job Order],itemcode as [Item Code],custcode as [Customer Code],Qty, " & _
                        " Description,serialno as [Serial No],Problem,actiontaken as [Action Taken],dateIn as [Date In],datereleased as [Date Out], " & _
                        " recievedby as [Recieved By],tech_incharge as [Incharge],transactiontype as [Transaction Type]FROM tbliteminfo")
            fillitemtable(DataGridView1)

        End If

    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        AllReport.Show()


    End Sub

    Private Sub Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadioButton1.Checked = True

    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        'jokenselect("SELECT ID,jobordercode as [Job Order],curdate as [Date],Amount,Cashier,Orno as [Or No], " & _
        '                " custcode as [Customercode] FROM tblpayments where curdate between #" & DateValue(DateTimePicker1.Value) & "# and #" & DateValue(DateTimePicker2.Value) & "#")
        jokenselect("SELECT ID,jobordercode as [Job Order],Orno as [Or No],curdate as [Date],Cashier,Amount FROM tblpayments where curdate between #" & DateValue(DateTimePicker1.Value) & "# and #" & DateValue(DateTimePicker2.Value) & "#")
        filltotaltable(DataGridView1)
        With DataGridView1
            .Columns(5).DefaultCellStyle.Format = "n"

        End With
        'kjjndnnjsjfs

    End Sub
    Private masterBindingSource As New BindingSource()
    Private detailsBindingSource As New BindingSource()

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Radiocustpay.Checked = True Then
            DataGridView1.DataSource = masterBindingSource
            DataGridView2.DataSource = detailsBindingSource

            Try
                'Connection obj to database
                Dim conn As OleDb.OleDbConnection = jokenconn()
                conn.Open()
                Dim da1, da2 As New OleDb.OleDbDataAdapter
                Dim sql1 As String = "Select custcode,custname,custcontact,Address,custtel from tblcustomer"
                Dim sql2 As String = "Select jobordercode as [Job Order],custcode as [Customer-Code],Orno as [Or No],curdate as [Date],Cashier,Amount from tblpayments"


                Dim data As New DataSet()
                data.Locale = System.Globalization.CultureInfo.InvariantCulture

                da1 = New OleDb.OleDbDataAdapter(sql1, conn)
                da1.Fill(data, "tblcustomer")

                da2 = New OleDb.OleDbDataAdapter(sql2, conn)
                da2.Fill(data, "tblpayments")

                Dim relation As New DataRelation("ustorders", _
                  data.Tables("tblcustomer").Columns(0), _
                  data.Tables("tblpayments").Columns(1))
                data.Relations.Add(relation)

                masterBindingSource.DataSource = data
                masterBindingSource.DataMember = "tblcustomer"
                detailsBindingSource.DataSource = masterBindingSource
                detailsBindingSource.DataMember = "ustorders"
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf Radiojobest.Checked = True Then
            Try
                'Connection obj to database
                Dim conn As OleDb.OleDbConnection = jokenconn()
                conn.Open()
                Dim da1, da2 As New OleDb.OleDbDataAdapter
                Dim sql1 As String = " SELECT jobordercode as [Job Order],itemcode as [Item Code],Parts,Labor,pickup as [Pick up], " & _
                                     " Delivery,totalamount as [Total Amount],Downpayment,balanceamount as [Balance],Settled, " & _
                                     " Remarks FROM tbljobestimate"
                Dim sql2 As String = "SELECT joborderno AS [Job Order], itemcode AS [Item Code], custcode AS [Customer Code], " & _
                                    " Qty, Description, serialno AS [Serial No], Problem, actiontaken AS [Action Taken], dateIn AS [Date In], " & _
                                    " datereleased AS [Date Out], recievedby AS [Recieved By], tech_incharge AS Incharge, " & _
                                    " transactiontype AS [Transaction Type]FROM tbliteminfo"



                Dim data As New DataSet()
                data.Locale = System.Globalization.CultureInfo.InvariantCulture

                da1 = New OleDb.OleDbDataAdapter(sql1, conn)
                da1.Fill(data, "tbljobestimate")

                da2 = New OleDb.OleDbDataAdapter(sql2, conn)
                da2.Fill(data, "tbliteminfo")

                Dim relation As New DataRelation("ustorders", _
                  data.Tables("tbljobestimate").Columns(0), _
                  data.Tables("tbliteminfo").Columns(0))
                data.Relations.Add(relation)

                masterBindingSource.DataSource = data
                masterBindingSource.DataMember = "tbljobestimate"
                detailsBindingSource.DataSource = masterBindingSource
                detailsBindingSource.DataMember = "ustorders"
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try

            jokenselect("SELECT tblcustomer.ID,custname,custcontact,Address,custtel , " & _
                        " jobordercode as [Job Order],Orno as [Or No],curdate as [Date],Cashier, " & _
                        " Amount FROM tblcustomer LEFT JOIN tblpayments ON tblcustomer.custcode =tblpayments.custcode where tblcustomer.custname like'%" & TextBox1.Text & "%'")
            'jokenselect("SELECT ID,jobordercode as [Job Order],curdate as [Date],Cashier,Orno as [Or No],Amount, " & _
            '            " custcode as [Customercode] FROM tblpayments")
            filltotalcustmerpayments(DataGridView1)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If TextBox1.Text.Length = 0 Then

            MsgBox("Please provide customer name before Clicking preview!", MsgBoxStyle.Information)


        Else

            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton4.Checked = False
            RadioButton5.Checked = False
            AllReport.Show()



        End If

    End Sub
End Class