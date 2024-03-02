Public Class Main



    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        txttime.Text = My.Computer.Clock.LocalTime.Date.ToLongDateString

    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
        JobOrdertool.Visible = False
        paymentstool.Visible = False
        customertool.Visible = False
        ReportsToolStripMenuItem.Visible = False
        ManageUserToolStripMenuItem.Visible = False
        techToolStrip.Visible = False

    End Sub

    Private Sub LogInToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles customertool.Click
        Customer.Show()
        Payment.Close()
        order.Close()

    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()

    End Sub

    Private Sub GuestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles logintool.Click
        
        If logintool.Text = "Log in" Then
            login.Show()
            login.BringToFront()
        Else
            jokenupdate("UPDATE logs SET lastlogout = #" & DateValue(DateTimePicker1.Value) & "#, userid = '" & userid.Text & "'  where ID = (SELECT last(ID) FROM logs)")
            Main_Load(sender, e)
            logintool.Text = "Log in"
            Guest.Text = "Guest"
        End If

    End Sub

    Private Sub ManageUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManageUserToolStripMenuItem.Click
        useraccounts.Show()
        useraccounts.BringToFront()
    End Sub

    Private Sub ReportsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportsToolStripMenuItem.Click
        Reports.Show()
    End Sub

    
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles techToolStrip.Click
        technician.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If logintool.Text = "Log in" Then
            Me.Close()
        Else
            MsgBox("Please Log out first before Exit program!")
        End If

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim f As Customer = New Customer
        'f.ShowDialog()
        showform(f)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim f As technician = New technician
        'f.ShowDialog()
        showform(f)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim f As Reports = New Reports
        'f.ShowDialog()
        showform(f)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f As order = New order
        showform(f)
        'f.ShowDialog()
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        Dim f As Payment = New Payment
        showform(f)
        'f.ShowDialog()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim f As useraccounts = New useraccounts
        showform(f)
        'f.ShowDialog()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Me.Close()
        login.Show()
    End Sub
End Class