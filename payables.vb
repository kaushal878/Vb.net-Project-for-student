Public Class payables

    Private Sub txtparts_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtparts.TextChanged
        
        compute()

    End Sub
    Public Sub compute()
        If Not IsNumeric(txtparts.Text) Then
            txtparts.Text = Nothing

        End If
        If Not IsNumeric(txtlabor.Text) Then
            txtlabor.Text = Nothing

        End If
        If Not IsNumeric(txtpickup.Text) Then
            txtpickup.Text = Nothing

        End If
        If Not IsNumeric(txtdeliver.Text) Then
            txtdeliver.Text = Nothing

        End If

        txttotalamnt.Text = Val(txtdeliver.Text) + Val(txtparts.Text) + Val(txtlabor.Text) + Val(txtpickup.Text)
        txttotalamnt.Text = FormatNumber(txttotalamnt.Text, 2)

    End Sub

    Private Sub txtlabor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlabor.TextChanged
        compute()

    End Sub

    Private Sub txtpickup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpickup.TextChanged
        compute()

    End Sub

    Private Sub txtdeliver_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdeliver.TextChanged

        compute()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        jokenupdate("UPDATE tbljobestimate set parts = '" & txtparts.Text & "',labor = '" & txtlabor.Text & "', " & _
                    " pickup= '" & txtpickup.Text & "',delivery = '" & txtdeliver.Text & "',balanceamount='" & txttotalamnt.Text & "', " & _
                    " totalamount='" & txttotalamnt.Text & "' where jobordercode= '" & Label2.Text & "'")
        Me.Close()
        Payment.Show()


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()

    End Sub
End Class