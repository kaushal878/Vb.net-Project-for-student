Public Class login

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Dim sql As String

        If txtuser.Text = "" And txtpass.Text = "" Then
            MsgBox("Password or Username Incorrect!")

        Else
            sql = "select * from tbluseraccounts where userusername ='" & txtuser.Text & "' and userpassword = '" & txtpass.Text & "'"
            jokenfindthis(sql)
            checkresult()
        End If
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()

    End Sub
End Class