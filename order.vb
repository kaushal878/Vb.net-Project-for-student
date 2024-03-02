Public Class order

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lbldate.Text = Date.Today
        lbltime.Text = TimeOfDay

    End Sub
    Private Sub order_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
        GroupBox1.Enabled = False
        txtremarks.Enabled = False
        Timer1.Start()
        Dim jborder, itemcode As String
        Dim da, da1 As New OleDb.OleDbDataAdapter
        Dim con As OleDb.OleDbConnection = jokenconn()
        jborder = "Select appendchar & '-' & autoend + incrementvalue from tblauto where ID = 1"
        itemcode = "Select appendchar & '-' & autoend + incrementvalue from tblauto where ID = 3"
        Try
            con.Open()
            da = New OleDb.OleDbDataAdapter(jborder, con)
            Dim ds As New DataSet
            da.Fill(ds, "jobdb")
            lblorderno.Text = ds.Tables("jobdb").Rows(0).Item(0)

            da1 = New OleDb.OleDbDataAdapter(itemcode, con)
            Dim ds1 As New DataSet
            da1.Fill(ds1, "jobdb")
            lblitemcode.Text = ds1.Tables("jobdb").Rows(0).Item(0)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        con.Close()
        jokenselect("SELECT i.ID,qty as [QTY],description as [Description],serialno as [Serial No],problem as [Problem],actiontaken as [Action Taken],totalamount as [Total Amount] " & _
                   " FROM tblcustomer c, tbliteminfo i, tbljobestimate j where c.custcode =i.custcode " & _
                   " and j.itemcode = i.itemcode and i.custcode ='" & txtcustcode.Text & "' ")
        fillitemtable(DataGridView1)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'If GroupBox1.Enabled = False Then
        'MsgBox("Please Provide Job Estimate")
        'Else
        If txtcustcode.Text = "" Then
            MsgBox("Please provide Customer Information!", MsgBoxStyle.Exclamation)
        Else
            jokeninsert("INSERT INTO tbliteminfo (joborderno,itemcode, custcode, qty, description, serialno, " & _
                    " problem, actiontaken,dateIn, recievedby, tech_incharge,transactiontype) " & _
                    " VALUES('" & lblorderno.Text & "','" & lblitemcode.Text & "','" & txtcustcode.Text & "', " & _
                    " " & Val(txtqty.Text) & ",'" & txtdesc.Text & "','" & txtserial.Text & "', " & _
                    " '" & txtprob.Text & "','" & txtaction.Text & "',#" & lbldate.Text & "#,'" & Main.Guest.Text & "', " & _
                    " '" & txtincharge.Text & "','" & lblchckresult.Text & "')")

            jokeninsert("INSERT INTO tbljobestimate(jobordercode,itemcode,parts,labor,pickup,delivery,downpayment,balanceamount,totalamount,Remarks) " & _
                        " VALUES('" & lblorderno.Text & "','" & lblitemcode.Text & "'," & Val(txtparts.Text) & "," & Val(txtlabor.Text) & ", " & _
                        " " & Val(txtpickup.Text) & "," & Val(txtdeliver.Text) & "," & 0 & "," & Val(txttotalamnt.Text) & "," & Val(txttotalamnt.Text) & ",'" & txtremarks.Text & "')")
            jokenupdate("Update tblauto set autoend = autoend + incrementvalue where id = 3")
            jokenupdate("Update tblauto set autoend = autoend + incrementvalue where id = 1")

            order_Load(sender, e)

            jokenselect("SELECT i.ID,qty as [QTY],description as [Description],serialno as [Serial No],problem as [Problem],actiontaken as [Action Taken] " & _
                      "  FROM tblcustomer c, tbliteminfo i  where c.custcode =i.custcode  and i.custcode ='" & txtcustcode.Text & "' and i.joborderno='" & lblorderno.Text & "'")
            fillitemtable(DataGridView1)


        End If
        'End If
        PrintJB.Show()
    End Sub

    Private Sub btnsavetrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavetrans.Click
        If chckUW.Checked = True Then
            lblchckresult.Text = chckUW.Text
        ElseIf chckOUW.Checked = True Then
            lblchckresult.Text = chckOUW.Text
        ElseIf chckOS.Checked = True Then
            lblchckresult.Text = chckOS.Text
        ElseIf chckR.Checked = True Then
            lblchckresult.Text = chckR.Text
        ElseIf chckO.Checked = True Then
            lblchckresult.Text = chckO.Text
        Else
            lblchckresult.Text = chckR.Text

        End If

        If txtcustcode.Text = "" Then
            MsgBox("Please provide Customer Information!", MsgBoxStyle.Exclamation)
        Else
            jokeninsert("INSERT INTO tbliteminfo (joborderno,itemcode, custcode, qty, description, serialno, " & _
                    " problem, actiontaken,dateIn, recievedby, tech_incharge,transactiontype) " & _
                    " VALUES('" & lblorderno.Text & "','" & lblitemcode.Text & "','" & txtcustcode.Text & "', " & _
                    " " & Val(txtqty.Text) & ",'" & txtdesc.Text & "','" & txtserial.Text & "', " & _
                    " '" & txtprob.Text & "','" & txtaction.Text & "',#" & lbldate.Text & "#,'" & Main.Guest.Text & "', " & _
                    " '" & txtincharge.Text & "','" & lblchckresult.Text & "')")

            'jokeninsert("INSERT INTO tbljobestimate(itemcode,parts,labor,pickup,delivery,totalamount) " & _
            '            " VALUES('" & lblitemcode.Text & "'," & Val(txtparts.Text) & "," & Val(txtlabor.Text) & ", " & _
            '            " " & Val(txtpickup.Text) & "," & Val(txtdeliver.Text) & "," & Val(txttotalamnt.Text) & ")")
            jokenupdate("Update tblauto set autoend = autoend + incrementvalue where id = 3")
            'jokenupdate("Update tblauto set autoend = autoend + incrementvalue where id = 1")

            'Form1_Load(sender, e)

            jokenselect("SELECT i.ID,qty as [QTY],description as [Description],serialno as [Serial No],problem as [Problem],actiontaken as [Action Taken] " & _
                      "  FROM tblcustomer c, tbliteminfo i  where c.custcode =i.custcode  and i.custcode ='" & txtcustcode.Text & "' and i.joborderno='" & lblorderno.Text & "'")
            fillitemtable(DataGridView1)
        End If


    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        cleartext(GroupBox2)
        cleartext(GroupBox1)
    End Sub

    Private Sub jobest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles jobest.Click
        GroupBox1.Enabled = True
        txtremarks.Enabled = True
    End Sub

    Private Sub txtcustcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustcode.TextChanged
        'jokenselect("SELECT i.ID,qty as [QTY],description as [Description],serialno as [Serial No],problem as [Problem],actiontaken as [Action Taken] " & _
        '         "  FROM tblcustomer c, tbliteminfo i  where c.custcode =i.custcode  and i.custcode ='" & txtcustcode.Text & "' and i.joborderno='" & lblorderno.Text & "'")
        'fillitemtable(DataGridView1)
    End Sub

    Private Sub btnopenlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnopenlist.Click
        'Dim f As Customer = New Customer
        With Customer
            .Show()
            .Focus()
            .btnuse.Visible = True
        End With

    End Sub

    Private Sub txtparts_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtparts.TextChanged, txtlabor.TextChanged, txtpickup.TextChanged, txtdeliver.TextChanged
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
            txtdeliver .Text = Nothing

        End If

        compute()
    End Sub

    Public Sub compute()
        txttotalamnt.Text = Val(txtdeliver.Text) + Val(txtparts.Text) + Val(txtlabor.Text) + Val(txtpickup.Text)
        txttotalamnt.Text = FormatNumber(txttotalamnt.Text, 2)
    End Sub

    Private Sub txtqty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtqty.TextChanged
        If Not IsNumeric(txtqty.Text) Then
            txtqty.Text = ""
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        technician.Show()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()

    End Sub

    Private Sub btnsavejoborder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavejoborder.Click
        If GroupBox1.Enabled = False Then
            MsgBox("Please Provide Job Estimate")
        Else
            If txtcustcode.Text = "" Then
                MsgBox("Please provide Customer Information!", MsgBoxStyle.Exclamation)
            Else
                jokeninsert("INSERT INTO tbliteminfo (joborderno,itemcode, custcode, qty, description, serialno, " & _
                        " problem, actiontaken,dateIn, recievedby, tech_incharge,transactiontype) " & _
                        " VALUES('" & lblorderno.Text & "','" & lblitemcode.Text & "','" & txtcustcode.Text & "', " & _
                        " " & Val(txtqty.Text) & ",'" & txtdesc.Text & "','" & txtserial.Text & "', " & _
                        " '" & txtprob.Text & "','" & txtaction.Text & "',#" & lbldate.Text & "#,'" & Main.Guest.Text & "', " & _
                        " '" & txtincharge.Text & "','" & lblchckresult.Text & "')")

                jokeninsert("INSERT INTO tbljobestimate(jobordercode,itemcode,parts,labor,pickup,delivery,downpayment,balanceamount,totalamount,Remarks) " & _
                            " VALUES('" & lblorderno.Text & "','" & lblitemcode.Text & "'," & Val(txtparts.Text) & "," & Val(txtlabor.Text) & ", " & _
                            " " & Val(txtpickup.Text) & "," & Val(txtdeliver.Text) & "," & 0 & "," & Val(txttotalamnt.Text) & "," & Val(txttotalamnt.Text) & ",'" & txtremarks.Text & "')")
                jokenupdate("Update tblauto set autoend = autoend + incrementvalue where id = 3")
                jokenupdate("Update tblauto set autoend = autoend + incrementvalue where id = 1")

                order_Load(sender, e)

                jokenselect("SELECT i.ID,qty as [QTY],description as [Description],serialno as [Serial No],problem as [Problem],actiontaken as [Action Taken] " & _
                          "  FROM tblcustomer c, tbliteminfo i  where c.custcode =i.custcode  and i.custcode ='" & txtcustcode.Text & "' and i.joborderno='" & lblorderno.Text & "'")
                fillitemtable(DataGridView1)


            End If
        End If

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub
End Class