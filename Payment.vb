Public Class Payment
    Dim payments As Decimal


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        txtdate.Text = Date.Today

    End Sub

    Private Sub Payment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Timer1.Start()
        Dim dt, dt1 As New DataTable
        Dim da, da1 As New OleDb.OleDbDataAdapter
        Dim con As OleDb.OleDbConnection = jokenconn()
        Dim orno As String = "Select autoend + incrementvalue from tblauto where ID = 4"

        Try
        
            da1 = New OleDb.OleDbDataAdapter(orno, con)
            da1.Fill(dt1)
            lblor.Text = dt1.Rows(0).Item(0)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        con.Close()
        jokenselect("Select ID,jobordercode as [Job Order],Orno as [Or No],curdate as [Date],Cashier,Amount from tblpayments")
        filltotaltable (DataGridView2)
        Try
            Me.DataGridView2.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).DefaultCellStyle.Font = New Font(DataGridView2.Font, FontStyle.Bold)

            DataGridView2.Columns(5).DefaultCellStyle.Format = "n"

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
     
        btnsaveonly.Enabled = False
        btnprint.Enabled = False
    End Sub

    Private Sub btnopenlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Customer.Show()
        Customer.btnuse.Visible = False

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            lblid.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString

            txttotal.Text = FormatNumber(DataGridView1.CurrentRow.Cells(6).Value.ToString, 2)
            txtbal.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString
            Txtdownpayment.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString

            ' txtremainingbal.Text = FormatNumber(DataGridView1.CurrentRow.Cells(8).Value.ToString, 2)
            txtcustcode.Text = DataGridView1.CurrentRow.Cells(11).Value.ToString
            txtaddress.Text = DataGridView1.CurrentRow.Cells(13).Value.ToString
            txttell.Text = DataGridView1.CurrentRow.Cells(14).Value.ToString
            txtjborder.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
            btnsaveonly.Enabled = True
            btnprint.Enabled = True


            txtchange.Text = "0.00"
            txtrecieve.Text = Nothing
            'txttotal.Text = FormatNumber(txttotal.Text, 2)
            txtnetbal.Text = txtbal.Text
            txtnetbal.Text = FormatNumber(txtnetbal.Text, 2)
        Catch ex As Exception
            MsgBox("Column Header is no allowed", MsgBoxStyle.Information)
        End Try

    End Sub
    Private Sub txtrecieve_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrecieve.TextChanged
        Dim str, str1 As String
        txtremainingbal.Text = Val(txtbal.Text) - Val(txtrecieve.Text)
        txtrpayments.Text = Val(txtbal.Text) - Val(txtremainingbal.Text)
        txtgrandtotal.Text = Val(Txtdownpayment.Text) + Val(txtrpayments.Text)
        txtchange.Text = Val(txtrecieve.Text) - Val(txtbal.Text)
        txtchange.Text = FormatNumber(txtchange.Text, 2)
        txtremainingbal.Text = FormatNumber(txtremainingbal.Text, 2)
        txtgrandtotal.Text = FormatNumber(txtgrandtotal.Text, 2)
        txtrpayments.Text = FormatNumber(txtrpayments.Text, 2)


        If Val(txtrecieve.Text) > Val(txtbal.Text) Then
            txtgrandtotal.Text = txttotal.Text
            txtrpayments.Text = Val(txtbal.Text)
            txtgrandtotal.Text = FormatNumber(txtgrandtotal.Text, 2)
            txtrpayments.Text = FormatNumber(txtrpayments.Text, 2)

        End If
        If txtrecieve.Text = "" Then

            txtrecieve.Text = Nothing
            txtchange.Text = Nothing
            txtchange.Text = "0.00"
            txtgrandtotal.Text = Txtdownpayment.Text
            txtremainingbal.Text = txtbal.Text
            txtrpayments.Text = txtrecieve.Text

        End If

        If Not IsNumeric(txtrecieve.Text) Then
            txtrecieve.Text = Nothing
            txtchange.Text = Nothing
            txtchange.Text = "0.00"
            txtgrandtotal.Text = Txtdownpayment.Text
            txtremainingbal.Text = txtbal.Text
            txtrpayments.Text = "0.00"
        End If

        str = Val(txtchange.Text)

        If str.Contains("-") = True Then
            txtchange.Text = "0.00"
        End If
        str1 = Val(txtremainingbal.Text)
        If str1.Contains("-") = True Then
            txtremainingbal.Text = "0.00"
        End If

    End Sub
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        savereciept()
        cleartext(GroupBox1)
        Payment_Load(sender, e)
        receipt.Show()
    End Sub


    Private Sub btnsaveonly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsaveonly.Click
        savereciept()
        cleartext(GroupBox1)
        Payment_Load(sender, e)
    End Sub

    Private Sub txtcustname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustname.TextChanged
        Dim sql As String = "SELECT j.ID,j.jobordercode as [JobOrder],Parts, Labor,pickup as [Pick up],Delivery,Totalamount as [Total],downpayment as [Payment], " & _
                            " balanceamount as [Balance],Settled,c.custcode,custname,custcontact, Address,custtel " & _
                            " FROM  tbljobestimate AS j,tblcustomer AS c, tbliteminfo AS i " & _
                            " WHERE j.itemcode=i.itemcode And c.custcode=i.custcode And c.custname like'%" & txtcustname.Text & "%'"
        jokenselect(sql)
        fillpaymentdata(DataGridView1)
        With DataGridView1
            .Columns(2).DefaultCellStyle.Format = "n"
            .Columns(3).DefaultCellStyle.Format = "n"
            .Columns(4).DefaultCellStyle.Format = "n"
            .Columns(5).DefaultCellStyle.Format = "n"
            .Columns(6).DefaultCellStyle.Format = "n"
            .Columns(7).DefaultCellStyle.Format = "n"
            .Columns(8).DefaultCellStyle.Format = "n"
        End With

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, txtrecieve.TextChanged
        'If balance.Text = "0.00" Then
        '    txtamount.Text = txttotal.Text
        '    txtrpayments.Text = txtbal.Text
        '    txtrpayments.Text = FormatNumber(txtrpayments.Text, 2)

        '    'ElseIf balance.Text = "0" Then
        '    '    balance.Text = "0.00"
        'Else

        '    txtrpayments.Text = Val(txtbal.Text) - Val(balance.Text)
        '    ' txtrpayments.Text = FormatNumber(txtrpayments.Text, 2)
        '    txtamount.Text = Val(Txtpayments.Text) + Val(txtrpayments.Text)
        '    ' txtamount.Text = FormatNumber(txtamount.Text, 2)


        'End If
    End Sub
    Public Sub savereciept()
        If txtremainingbal.Text = "0.00" Then

            jokenupdate("Update tbljobestimate set downpayment = '" & txttotal.Text & "',balanceamount = 0,settled = 1 where ID = " & Val(lblid.Text) & "")
            jokeninsert("Insert INTO tblpayments (jobordercode,curdate,amount,cashier,orno,custcode)VALUES('" & txtjborder.Text & "',#" & txtdate.Text & "#,'" & txtrpayments.Text & "','" & Main.Guest.Text & "'," & lblor.Text & ",'" & txtcustcode.Text & "')")
            jokenupdate("Update tblauto set autoend = autoend + incrementvalue where id = 4")
            jokenupdate("Update tbliteminfo set datereleased = #" & txtdate.Text & "# where joborderno = '" & txtjborder.Text & "'")

        Else
            'MsgBox(txttotal.Text)
            'MsgBox(txtremainingbal.Text)

            jokenupdate("Update tbljobestimate set downpayment = '" & txtgrandtotal.Text & "',balanceamount = '" & txtremainingbal.Text & "',settled = 0 where ID = " & Val(lblid.Text) & "")
            jokeninsert("Insert INTO tblpayments (jobordercode,curdate,amount,cashier,orno,custcode)VALUES('" & txtjborder.Text & "',#" & txtdate.Text & "#,'" & txtrpayments.Text & "','" & Main.Guest.Text & "'," & lblor.Text & ",'" & txtcustcode.Text & "')")
            jokenupdate("Update tblauto set autoend = autoend + incrementvalue where id = 4")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MsgBox(txtgrandtotal.Text)
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        payables.Label2.Text = txtjborder.Text
        payables.Show()

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class