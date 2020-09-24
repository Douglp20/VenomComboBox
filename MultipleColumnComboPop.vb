Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Public Delegate Function AfterRowSelectEventHandler(sender As Object, SelectedRow As DataRow)
Public Class MultipleColumnComboPop


    ' Public Event AfterRowSelectEvent As AfterRowSelectEventHandler()

    Private component As System.ComponentModel.Container
    Private selectedRow As DataRow
    Private lstvMyView As System.Windows.Forms.ListView
    Private inputRows As DataRow()
    Private tbl As DataTable
    Private mCols As Integer = 0
    Private mRows As Integer = 0
    Private columnsToDisplay As String()
    Private data As String()()

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mCols = 4
        mRows = 0
        InitializeGridProperties()
    End Sub

    Public Sub New(drows() As DataRow)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Me.inputRows = drows
        SetDataRows(inputRows)
        Me.tbl = inputRows(0).Table

    End Sub

    Public Sub New(dtable As DataTable, selRow As DataRow, colsToDisplay As String())

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.tbl = dtable
        Me.selectedRow = selRow
        Me.columnsToDisplay = colsToDisplay
        If (columnsToDisplay.Count = 0) Then
            SetDataTable(DataTable, columnsToDisplay)
        Else
            SetDataTable(DataTable);
    End Sub
    Private Sub InitializeGridProperties()
        lvwMultipleColumn.Items.Clear()
        lvwMultipleColumn.Columns.Clear()

        'Add the data Header
        For r As Integer = 0 To mRows - 0
            Dim lvwItem As New ListViewItem("")
            lvwMultipleColumn.Items.Add(lvwItem)
            For c As Integer = 0 To mCols - 1
                lvwItem.SubItems.Add(" ")
            Next
        Next
    End Sub
    Private Sub SetDataRows(drows() As DataRow)


        mCols = drows(0).Table.Columns.Count
        mRows = drows.Length
        InitializeGridProperties()
        For r As Integer = 0 To mRows - 0
            SetFullRow(r, drows(r))
        Next
        SetColumnHeaderNames(GetColumnNames(drows(0)));
    End Sub

    Private Sub SetFullRow(row As Integer, drow As DataRow)

    End Sub

    Private Function IsValidRow(row As Integer) As Boolean
        Dim ret As Boolean
        If row < 0 Then
            MessageBox.Show(Me, "Row cannot be negative", "Grid Error")
        ElseIf row > lvwMultipleColumn.Items.Count Then
            MessageBox.Show(Me, "Row out of range", "Grid Error")
            ret = False
        Else
            ret = True
        End If
        Return IsValidRow = ret
    End Function
    Private Function IsValidColumn(Column As Integer) As Boolean
        Dim ret As Boolean
        If Column < 0 Then
            MessageBox.Show(Me, "Column cannot be negative", "Grid Error")
        ElseIf Column > lvwMultipleColumn.Items.Count Then
            MessageBox.Show(Me, "Column out of range", "Grid Error")
            ret = False
        Else
            ret = True
        End If
        Return IsValidColumn = ret
    End Function


    Private Sub SetColumnHeaderNames(ColumnNames As String())
        For i As Integer = 0 To ColumnNames.Length - 1
            If i > lvwMultipleColumn.Columns.Count Then
                lvwMultipleColumn.Columns.Add(ColumnNames(i), 25, HorizontalAlignment.Center)
                lvwMultipleColumn.Columns(i).Width = ColumnNames(i).Length * CInt(Font.SizeInPoints)
            Else
                lvwMultipleColumn.Columns(i).Text = ColumnNames(i)
                lvwMultipleColumn.Columns(i).Width = ColumnNames(i).Length * CInt(Font.SizeInPoints)
            End If
        Next

    End Sub

    Private Function GetColumnNames(drow As DataRow) As ArrayList()
        Dim strColNames As New ArrayList()
        For i As Integer = 0 To drow.Table.Columns.Count
            strColNames.Add(drow.Table.Columns(i).ColumnName)
        Next

        Return strColNames
    End Function
End Class