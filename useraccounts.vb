Public Class useraccounts
    Public Sub displaymember()
        jokenselect("Select userID,username as [Name], userusername as [Username],usertype as [Type] From tbluseraccounts")
        filltable(DataGridView1)

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txtpass.Text <> txtconfirm.Text Then
            MsgBox("Password Confirmation did not match!", MsgBoxStyle.Information)
        Else
            jokeninsert("INSERT INTO tbluseraccounts ( username, userusername, userpassword, usertype ) " & _
                  " VALUES('" & txtname.Text & "','" & txtuname.Text & "','" & txtpass.Text & "','" & cbtype.SelectedItem & "')")
            displaymember()
        End If
        cleartext(GroupBox1)
    End Sub

    Private Sub btndel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndel.Click
        jokendelete("Delete * from tbluseraccounts where userID= " & lblid.Text & "")
        displaymember()
        cleartext(GroupBox1)
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        If txtpass.Text <> txtconfirm.Text Then
            MsgBox("Password Confirmation did not match!", MsgBoxStyle.Information)
        Else
            jokenupdate("UPDATE tbluseraccounts set username ='" & txtname.Text & "' , userusername = '" & txtuname.Text & "', userpassword = '" & txtpass.Text & "', usertype= '" & cbtype.SelectedItem & "' where userID = " & lblid.Text & "")
            displaymember()
        End If
        cleartext(GroupBox1)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        clearall(GroupBox1, DataGridView1)
    End Sub
    Private Sub itemdatagrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        lblid.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
        txtname.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        txtuname.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        cbtype.SelectedItem = DataGridView1.CurrentRow.Cells(3).Value.ToString
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub useraccounts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        displaymember()
    End Sub
End Class