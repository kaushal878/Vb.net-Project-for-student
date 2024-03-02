Public Class technician

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Enabled = True
        Button1.Enabled = True

    End Sub

    Private Sub technician_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button1.Enabled = False
        TextBox1.Enabled = False
        jokenselect("SELECT  ID,techname as [Technician Name] FROM tbltch")
        fillitemtable(DataGridView1)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        jokeninsert("INSERT INTO tbltch(techname)VALUES('" & TextBox1.Text & "')")
        jokenselect("SELECT ID,techname as [Technician Name] FROM tbltch")
        fillitemtable(DataGridView1)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            With order
                .txtincharge.Text = DataGridView1.CurrentRow.Cells(1).Value
            End With
            Me.Close()
        Catch ex As Exception
            MsgBox("Please select a customer!", MsgBoxStyle.Exclamation)
        End Try
    End Sub
End Class