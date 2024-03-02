Public Class Customer
    Private Sub Customer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Timer1.Start()
        GroupBox1.Enabled = False
        Dim custcode As String
        Dim da As New OleDb.OleDbDataAdapter
        Dim con As OleDb.OleDbConnection = jokenconn()

        custcode = "Select appendchar & '-' & autoend + incrementvalue from tblauto where ID = 2"

        Try
            con.Open()
            da = New OleDb.OleDbDataAdapter(custcode, con)
            Dim ds As New DataSet
            da.Fill(ds, "jobdb")
            txtcustcode.Text = ds.Tables("jobdb").Rows(0).Item(0)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        con.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lbldatetime.Text = Date.Today & " " & TimeOfDay
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncreate.Click
        jokeninsert("Insert Into tblcustomer(custcode,custname,custcontact,Address,custtel) " & _
                    " Values('" & txtcustcode.Text & "','" & txtcustname.Text & "', " & _
                    " '" & txtcontact.Text & "','" & txtaddress.Text & "','" & txttell.Text & "')")
        jokenupdate("Update tblauto set autoend = autoend + incrementvalue where id = 2")
        Customer_Load(sender, e)
        jokenselect("Select ID, custcode as [Customer Code],custname As [Customer],custcontact as [Contact Person],Address,custtel as [Telephone No] from tblcustomer")
        fillitemtable(DataGridView1)

    End Sub

    Private Sub btnloadall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloadall.Click
        jokenselect("Select ID, custcode as [Customer Code],custname As [Customer],custcontact as [Contact Person],Address,custtel as [Telephone No] from tblcustomer")
        fillitemtable(DataGridView1)
      
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustsearch.TextChanged
        jokenselect("Select ID, custcode as [Customer Code],custname As [Customer], " & _
                    " custcontact as [Contact Person],Address,custtel as [Telephone No] " & _
                    " from tblcustomer where custcode like'%" & txtcustsearch.Text & "%' or custname like '%" & txtcustsearch.Text & "%'")
        fillitemtable(DataGridView1)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GroupBox1.Enabled = True

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnuse.Click
        Try
            With order
                .txtcustcode.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
                .txtcustname.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
                .txtcontact.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
                .txtaddress.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString
                .txtcontact.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString
            End With
            showform(order)
            Me.Close()
        Catch ex As Exception
            MsgBox("Please select a customer!", MsgBoxStyle.Exclamation)
        End Try


    End Sub

   
End Class