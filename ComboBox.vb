Public Class ComboBox
    Public Event ErrorMessage(ByVal errDesc As String, ByVal errNo As Integer, ByVal errTrace As String)
    Public Sub New()
    End Sub
    Public Function Load(ByRef cbo As System.Windows.Forms.ComboBox, ByRef ds As DataSet)

        On Error GoTo Err



        Dim lrows As Integer = ds.Tables(0).Rows.Count

        Dim r As Integer
        Dim colTag As String
        If lrows > 0 Then
            cbo.Items.Clear()
            cbo.Items.Add("")
            For Each rows In ds.Tables(0).Rows
                cbo.Items.Add(ds.Tables(0).Rows(r).Item(1).ToString())
                r = r + 1
            Next


        End If

        Exit Function

Err:
        Dim rtn As String = "The error occur within the module " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + Me.ToString() + "."
        RaiseEvent ErrorMessage(Err.Description, Err.Number, rtn)


    End Function
    Public Function Binding(ByRef cbo As System.Windows.Forms.ComboBox, ByRef tbl As DataTable)

        On Error GoTo Err

        cbo.DataSource = tbl
        cbo.DisplayMember = "Name"
        cbo.ValueMember = "ID"
        Exit Function

Err:
        Dim rtn As String = "The error occur within the module " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + Me.ToString() + "."
        RaiseEvent ErrorMessage(Err.Description, Err.Number, rtn)


    End Function
    Public Function ID(ByRef cbo As System.Windows.Forms.ComboBox) As Integer

        On Error GoTo Err
        Dim _id As Integer = 0
        If cbo.Items.Count > 0 Then
            _id = cbo.SelectedValue
        Else
            _id = 0
        End If
        ID = _id
        Return ID
        Exit Function

Err:
        Dim rtn As String = "The error occur within the module " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + Me.ToString() + "."

        RaiseEvent ErrorMessage(Err.Description, Err.Number, rtn)


    End Function
    Public Function ArrayID(ByRef ds As DataSet) As ArrayList

        On Error GoTo Err

        Dim arrData As New ArrayList
        If ds.Tables(0).Rows.Count > 0 Then
            arrData.Add("")
            For Each row As DataRow In ds.Tables(0).Rows
                arrData.Add(CInt(row("ID").ToString))
            Next
        Else
            arrData.Add(0)
        End If
        ArrayID = arrData
        Exit Function

Err:
        Dim rtn As String = "The error occur within the module " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + Me.ToString() + "."
        RaiseEvent ErrorMessage(Err.Description, Err.Number, rtn)


    End Function

    Public Function GetListviewValueFromColumn(lvw As Windows.Forms.ListView, selectIndex As Integer) As ArrayList

        Dim lngID As Integer
        Dim lngCol As Integer = lvw.Columns.Count
        Dim lngRow As Integer = lvw.Items.Count
        Dim strvalue As String
        Dim arr As New ArrayList


        On Error GoTo Err
        For c As Integer = 0 To lngCol - 1
            arr.Add(lvw.Items(selectIndex).SubItems(c).Text)
        Next

        Return arr

        Exit Function

Err:
        Dim rtn As String = "The error occur within the module " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + Me.ToString() + "."
        RaiseEvent ErrorMessage(Err.Description, Err.Number, rtn)

    End Function
    Public Sub SelectListviewToCombo(lvw As Windows.Forms.ListView, cbo As System.Windows.Forms.ComboBox, selectIndex As Integer, col As Integer, FirstTextBox As Windows.Forms.TextBox, FirstCol As Integer, SecondTextBox As Windows.Forms.TextBox, SecondCol As Integer)

        Dim lngID As Integer
        Dim lngCol As Integer = lvw.Columns.Count
        Dim lngRow As Integer = lvw.Items.Count
        Dim strvalue As String
        Dim arr As New ArrayList




        On Error GoTo Err
        ''get the ID from the front col
        lngID = lvw.Items(selectIndex).SubItems(0).Text
        'get the value from the selected col
        strvalue = lvw.Items(selectIndex).SubItems(col).Text

        'set the combo text
        cbo.Text = strvalue
        'set the ID to the combo tag
        cbo.Tag = lngID

        'populate the return textboxes
        FirstTextBox.Text = lvw.Items(selectIndex).SubItems(FirstCol).Text
        SecondTextBox.Text = lvw.Items(selectIndex).SubItems(SecondCol).Text

        Exit Sub

Err:
        Dim rtn As String = "The error occur within the module " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + Me.ToString() + "."
        RaiseEvent ErrorMessage(Err.Description, Err.Number, rtn)

    End Sub
    Public Function DropDown(lvw As Windows.Forms.ListView, LastListViewIndex As Integer, comboTextValue As String) As Integer

        On Error GoTo Err



        lvw.Visible = True
        Return ListviewToComboFind(lvw, comboTextValue, LastListViewIndex)


        Exit Function

Err:
        Dim rtn As String = "The error occur within the module " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + Me.ToString() + "."
        RaiseEvent ErrorMessage(Err.Description, Err.Number, rtn)

    End Function
    Private Function ListviewToComboFind(lvw As Windows.Forms.ListView, strFind As String, LastIndex As Integer) As Integer

        Dim lngRow As Integer = lvw.Items.Count
        On Error GoTo Err
        Dim lngFindIndex As Integer = 0
        '' change the backgroud back of the last selected item
        If LastIndex > 0 Then
            lvw.Items(LastIndex).Selected = False
            lvw.Items(LastIndex).BackColor = System.Drawing.Color.White
            lvw.Items(LastIndex).ForeColor = System.Drawing.Color.Black
        End If

        '' change the backgroud back for the new selected item
        For r As Integer = 0 To lngRow - 1
            If lvw.Items(r).SubItems(1).Text = strFind Then
                lvw.Items(r).BackColor = System.Drawing.Color.Black
                lvw.Items(r).ForeColor = System.Drawing.Color.White
                lngFindIndex = r
                lvw.Select()
                lvw.Focus()
                lvw.EnsureVisible(r)
                Exit For
            End If
        Next
        ''Set the new index 
        ListviewToComboFind = lngFindIndex
        Exit Function

Err:
        Dim rtn As String = "The error occur within the module " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + Me.ToString() + "."
        RaiseEvent ErrorMessage(Err.Description, Err.Number, rtn)

    End Function

End Class
