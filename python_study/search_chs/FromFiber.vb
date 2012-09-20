'Author :kerry
'date   :2011-5-30
'version:1.001.531.003

Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient

Public Class FromFiber
    'DB Setting
    Public gSQLIP As String = MainFrom.gSQLIP
    Public gSQL_ID As String = MainFrom.gSQL_ID
    Public gSQL_PASSWORD As String = MainFrom.gSQL_PASSWORD
    Public gDatabaseName As String = MainFrom.gDatabaseName

    'input data
    Public in_strFiberId As String = ""
    Public in_strRIndex As String = ""
    Public in_strFiberStatus As String = ""
    Public in_strStartOdfId As String = ""
    Public in_strStartSubOdfId As String = ""
    Public in_strStartPort As String = ""
    Public in_strEndOdfId As String = ""
    Public in_strEndSubOdfId As String = ""
    Public in_strEndPort As String = ""
    Public in_strFiberLength As String = ""
    Public in_strControlRoom_ID_S As String = "" 'James Lee Select Start ODF In Fiber Control Room
    Public in_strControlRoom_ID_E As String = "" 'James Lee Select End ODF In Fiber Control Room
    'Public in_strRIndex As String = ""
    Public gCommandType As Integer = 1    ' 0: Add, 1:Edit

    Public o_nEndDlgResult As Integer = 0   '0:cancel 1:ok  2:delete

    Dim m_strSegmentId As String = ""
    Dim m_strOdfId As String = ""
    Dim m_strSubOdfId As String = ""

    Dim m_bInitUI As Boolean = False
    Dim gSql As New SqlComm
#Const NOT_USED_ODF_ID = 1

    Sub InitListViewStone()

    End Sub

    Function InitComboBox() As Boolean
        Dim bRet As Boolean = True
#If NOT_USED_ODF_ID Then
        Dim vSqlConn As String = ("Server=" + gSQLIP + ";Database=" + gDatabaseName + ";Uid=" + gSQL_ID + ";Pwd=" + gSQL_PASSWORD + ";")
        Dim vSqlStr As String
        Dim iWhile As Integer = 0

        Dim cn As New SqlConnection(vSqlConn)
        Dim vSqlCmd As New SqlCommand
        Dim Dr As SqlDataReader

        Dim listStartOdf As New DataTable()
        listStartOdf.Columns.Add(New DataColumn("Display", System.Type.GetType("System.String")))
        listStartOdf.Columns.Add(New DataColumn("Id", System.Type.GetType("System.String")))

        Dim listEndOdf As New DataTable()
        listEndOdf.Columns.Add(New DataColumn("Display", System.Type.GetType("System.String")))
        listEndOdf.Columns.Add(New DataColumn("Id", System.Type.GetType("System.String")))
        '20110623 James Lee Select Start ODF In Fiber Control Room--------------------------
        Try

            cn.Open()

            vSqlStr = "select ODF_NAME,ODF_ID from ODF where CONTROLROOM_ID ='" + in_strControlRoom_ID_S + "'"
            vSqlCmd = New SqlCommand(vSqlStr, cn)
            Dr = vSqlCmd.ExecuteReader()
            Dim strTmp As String = ""

            While Dr.Read()
                listStartOdf.Rows.Add(listStartOdf.NewRow())

                strTmp = Dr.Item("ODF_NAME").ToString
                listStartOdf.Rows(iWhile)(0) = strTmp

                strTmp = Dr.Item("ODF_ID").ToString
                listStartOdf.Rows(iWhile)(1) = strTmp

                iWhile += 1
            End While

            Dr.Close()
            cn.Close()
        Catch r As Exception
            bRet = False
        End Try
        '20110623 James Lee Select Start ODF In Fiber Control Room--------------------------
        '20110623 James Lee Select End ODF In Fiber Control Room----------------------------
        Try

            cn.Open()

            vSqlStr = "select ODF_NAME,ODF_ID from ODF where CONTROLROOM_ID ='" + in_strControlRoom_ID_E + "'"
            vSqlCmd = New SqlCommand(vSqlStr, cn)
            Dr = vSqlCmd.ExecuteReader()
            Dim strTmp As String = ""
            iWhile = 0
            While Dr.Read()
                listEndOdf.Rows.Add(listEndOdf.NewRow())

                strTmp = Dr.Item("ODF_NAME").ToString
                listEndOdf.Rows(iWhile)(0) = strTmp

                strTmp = Dr.Item("ODF_ID").ToString
                listEndOdf.Rows(iWhile)(1) = strTmp

                iWhile += 1
            End While

            Dr.Close()
            cn.Close()
        Catch r As Exception
            bRet = False
        End Try
        '20110623 James Lee Select End ODF In Fiber Control Room----------------------------
        With ComboBoxStartOdf
            .DataSource = listStartOdf
            .DisplayMember = "Display"
            .ValueMember = "Id"
        End With

        With ComboBoxEndOdf
            .DataSource = listEndOdf
            .DisplayMember = "Display"
            .ValueMember = "Id"
        End With
#End If
        'ComboBoxStartOdf.Items.Add("1")
        GetStartSubOdfByStartOdf()
        GetStartPortByStartSubOdf()
        GetEndSubOdfByEndOdf()
        GetEndPortByEndSubOdf()

        With ComboBoxFiberStatus.Items
            .Add("未使用")
            .Add("使用中")
        End With

        Return bRet
    End Function

    Sub InitUiData()
        TextBoxFiberId.Text = in_strFiberId
        TextBoxFiberLength.Text = in_strFiberLength
        TextBoxRIndex.Text = in_strRIndex



        If 0 = gCommandType Then
            ComboBoxFiberStatus.SelectedIndex = 0
            '  ComboBoxStartOdf.Text = in_strStartOdfId
            '  ComboBoxStartSubOdf.Text = in_strStartSubOdfId
            '  ComboBoxStartPort.Text = in_strStartPort
            '  ComboBoxEndOdf.Text = in_strEndOdfId
            '  ComboBoxEndSubOdf.Text = in_strEndSubOdfId
            '  ComboBoxEndPort.Text = in_strEndPort
        End If

        If 1 = gCommandType Then
            ' 1:Edit
            ComboBoxFiberStatus.SelectedItem = in_strFiberStatus

            ComboBoxStartOdf.SelectedValue = in_strStartOdfId

            ComboBoxStartSubOdf.SelectedItem = in_strStartSubOdfId

            ComboBoxStartPort.Items.Add(in_strStartPort)
            ComboBoxStartPort.SelectedItem = in_strStartPort

            ComboBoxEndOdf.SelectedValue = in_strEndOdfId

            ComboBoxEndSubOdf.SelectedItem = in_strEndSubOdfId

            ComboBoxEndPort.Items.Add(in_strEndPort)
            ComboBoxEndPort.SelectedItem = in_strEndPort
        End If

        ComboBoxStartOdf.SelectedValue = in_strStartOdfId
        ComboBoxEndOdf.SelectedValue = in_strEndOdfId

    End Sub

    Sub SetInputData(ByVal strSegmentId As String)
        m_strSegmentId = strSegmentId
    End Sub

    Private Sub FormFiber_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_bInitUI = False


        InitComboBox()
        InitListViewStone()
        m_bInitUI = True
        InitUiData()


        If 1 = gCommandType Then
            ' 1:Edit
            'GetDbDataToListViewStone()
            ButtonDelete.Visible = True
        End If

        If 0 = gCommandType Then
            ' 0:Add
            ButtonDelete.Visible = False
        End If


    End Sub

    Function UpdateDataToSql() As Boolean
        Dim strSql As String = "update FIBER set "
        strSql += "FIBER_LEN =" + TextBoxFiberLength.Text
        strSql += ",FIBER_STATUS =" + CStr(ComboBoxFiberStatus.SelectedIndex)
        strSql += ",R_INDEX =" + TextBoxRIndex.Text
        strSql += ",ODF_ID_S ='" + ComboBoxStartOdf.SelectedValue + "'"
        strSql += ",Sub_ODF_ID_S ='" + ComboBoxStartSubOdf.Text + "'"
        strSql += ",PORT_ID_S ='" + ComboBoxStartPort.Text + "'"
        strSql += ",ODF_ID_E ='" + ComboBoxEndOdf.SelectedValue + "'"
        strSql += ",SUB_ODF_ID_E ='" + ComboBoxEndSubOdf.Text + "'"
        strSql += ",PORT_ID_E ='" + ComboBoxEndPort.Text + "'"
        strSql += "where SEG_ID = '" + m_strSegmentId + "'and FIBER_ID = '" + TextBoxFiberId.Text + "'"

        Return gSql.ExecuteSqlStatementB(strSql)
    End Function

    Function UnlockStartFiberStatus() As Boolean     'change fiber status after change port or sub ODF or ODF
        Dim bIsStartChanged As Boolean = False

        ' Is start change
        If ComboBoxStartOdf.SelectedValue <> in_strStartOdfId Then
            bIsStartChanged = True
        End If

        If ComboBoxStartSubOdf.Text <> in_strStartSubOdfId Then
            bIsStartChanged = True
        End If

        If ComboBoxStartPort.Text <> in_strStartPort Then
            bIsStartChanged = True
        End If

        If bIsStartChanged Then
            Return gSql.ExecuteSqlStatementB("update SUB_ODF_PORT set BACK_USED = 0 where ODF_ID = '" + in_strStartOdfId + "' and SUB_ODF_ID = '" + in_strStartSubOdfId + "' and PORT_ID ='" + in_strStartPort + "'")
        End If

        Return True
    End Function

    Function UnlockEndFiberStatus() As Boolean
        Dim bIsEndChanged As Boolean = False

        ' Is end changed
        If ComboBoxEndOdf.SelectedValue <> in_strEndOdfId Then
            bIsEndChanged = True
        End If

        If ComboBoxEndSubOdf.Text <> in_strEndSubOdfId Then
            bIsEndChanged = True
        End If

        If ComboBoxEndPort.Text = in_strEndPort Then
            bIsEndChanged = True
        End If

        If bIsEndChanged Then
            Return gSql.ExecuteSqlStatementB("update SUB_ODF_PORT set BACK_USED = 0 where ODF_ID = '" + in_strEndOdfId + "' and SUB_ODF_ID = '" + in_strEndSubOdfId + "' and PORT_ID ='" + in_strEndPort + "'")
        End If

        Return True
    End Function

    Function LockStartFiberStatus() As Boolean
        Return gSql.ExecuteSqlStatementB("update SUB_ODF_PORT set BACK_USED = 1 where ODF_ID = '" + ComboBoxStartOdf.SelectedValue + "' and SUB_ODF_ID = '" + ComboBoxStartSubOdf.Text + "' and PORT_ID ='" + ComboBoxStartPort.Text + "'")
    End Function

    Function LockEndFiberStatus() As Boolean
        Return gSql.ExecuteSqlStatementB("update SUB_ODF_PORT set BACK_USED = 1 where ODF_ID = '" + ComboBoxEndOdf.SelectedValue + "' and SUB_ODF_ID = '" + ComboBoxEndSubOdf.Text + "' and PORT_ID ='" + ComboBoxEndPort.Text + "'")
    End Function

    Function InsertDataToSql() As Boolean
        Dim strSql As String = "insert into FIBER(SEG_ID, FIBER_ID, FIBER_LEN, FIBER_STATUS, R_INDEX, ODF_ID_S, Sub_ODF_ID_S, PORT_ID_S, ODF_ID_E, SUB_ODF_ID_E, PORT_ID_E, CREATED_DATE, CREATED_BY, LAST_EDIT_DATE, LAST_EDIT_BY, ACTIVE_STATUS) values ('"
        Dim strDate As String = ""
        strDate = DateTime.Now.ToLocalTime()

        strSql += m_strSegmentId + "','"
        strSql += TextBoxFiberId.Text + "',"
        strSql += TextBoxFiberLength.Text + ","
        strSql += CStr(ComboBoxFiberStatus.SelectedIndex) + ","
        strSql += TextBoxRIndex.Text + ",'"
        strSql += ComboBoxStartOdf.SelectedValue + "','"
        strSql += ComboBoxStartSubOdf.Text + "','"
        strSql += ComboBoxStartPort.Text + "','"
        strSql += ComboBoxEndOdf.SelectedValue + "','"
        strSql += ComboBoxEndSubOdf.Text + "','"
        strSql += ComboBoxEndPort.Text + "','"
        strSql += strDate + "','admin','"
        strSql += strDate + "','001','0')"


        Return gSql.ExecuteSqlStatementB(strSql)
    End Function

    Function IsUIDataEmpty() As Boolean
        If "" = ComboBoxStartOdf.Text Then
            MsgBox("[起始端机架]不能为空！")
            Return False
        End If

        If "" = ComboBoxStartSubOdf.Text Then
            MsgBox("[起始端子架]不能为空！")
            Return False
        End If

        If "" = ComboBoxStartPort.Text Then
            MsgBox("[起始端端口]不能为空！")
            Return False
        End If

        If "" = ComboBoxEndOdf.Text Then
            MsgBox("[结束端机架]不能为空！")
            Return False
        End If

        If "" = ComboBoxEndSubOdf.Text Then
            MsgBox("[结束端子架]不能为空！")
            Return False
        End If

        If "" = ComboBoxEndPort.Text Then
            MsgBox("[结束端端口]不能为空！")
            Return False
        End If

        Return True
    End Function

    Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        If Not IsUIDataEmpty() Then
            Return
        End If

        o_nEndDlgResult = 1 'For flush ListViewFiber

        If 1 = gCommandType Then
            ' 1:Edit
            If Not UpdateDataToSql() Then
                MsgBox("更新光纤失败！")
                Return
            End If

            If Not UnlockStartFiberStatus() Then
                MsgBox("解锁起始端端口状态失败！")
                Return
            End If

            If Not UnlockEndFiberStatus() Then
                MsgBox("解锁结束端端口状态失败！")
                Return
            End If

            MsgBox("更新成功")
        End If

        If 0 = gCommandType Then
            ' 0:Add
            If Not InsertDataToSql() Then
                MsgBox("更新光纤失败")
                Return
            End If

            MsgBox("插入成功")
        End If

        If Not LockStartFiberStatus() Then
            MsgBox("锁住起始端端口状态失败！")
            Return
        End If

        If Not LockEndFiberStatus() Then
            MsgBox("锁住结束端端口状态失败！")
            Return
        End If

        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        o_nEndDlgResult = 0
        Me.Close()
    End Sub

    Function GetStartSubOdfByStartOdf() As Boolean
        Dim vSqlConn As String = ("Server=" + gSQLIP + ";Database=" + gDatabaseName + ";Uid=" + gSQL_ID + ";Pwd=" + gSQL_PASSWORD + ";")
        Dim vSqlStr As String
        Dim bRet As Boolean = True
        Dim iWhile As Integer = 0

        Dim cn As New SqlConnection(vSqlConn)
        Dim vSqlCmd As New SqlCommand
        Dim Dr As SqlDataReader

        ' set ComboBoxStartSubOdf
        ComboBoxStartSubOdf.Items.Clear()   'clear 
        Try

            cn.Open()
#If NOT_USED_ODF_ID Then
            vSqlStr = "select SUB_ODF_ID from SUB_ODF where ODF_ID = '" + ComboBoxStartOdf.SelectedValue + "'"
#Else
            vSqlStr = "select SUB_ODF_ID from SUB_ODF where ODF_ID = '" + in_strStartOdfId + "'"
#End If
            vSqlCmd = New SqlCommand(vSqlStr, cn)
            Dr = vSqlCmd.ExecuteReader()

            While Dr.Read()
                ComboBoxStartSubOdf.Items.Add(Dr.Item("SUB_ODF_ID").ToString)
            End While

            Dr.Close()
            cn.Close()
        Catch r As Exception
            bRet = False
        End Try

        Return bRet
    End Function

    Function GetStartPortByStartSubOdf() As Boolean
        Dim vSqlConn As String = ("Server=" + gSQLIP + ";Database=" + gDatabaseName + ";Uid=" + gSQL_ID + ";Pwd=" + gSQL_PASSWORD + ";")
        Dim vSqlStr As String
        Dim bRet As Boolean = True
        Dim iWhile As Integer = 0

        Dim cn As New SqlConnection(vSqlConn)
        Dim vSqlCmd As New SqlCommand
        Dim Dr As SqlDataReader

        ComboBoxStartPort.Items.Clear()
        Try

            cn.Open()
#If NOT_USED_ODF_ID Then
            vSqlStr = "select PORT_ID from SUB_ODF_PORT where ODF_ID = '" + ComboBoxStartOdf.SelectedValue + "' and SUB_ODF_ID = '" + ComboBoxStartSubOdf.Text + "' and BACK_USED = 0"
#Else
            vSqlStr = "select PORT_ID from SUB_ODF_PORT where ODF_ID = '" + in_strStartOdfId + "' and SUB_ODF_ID = '" + ComboBoxStartSubOdf.Text + "' and BACK_USED = 0"
#End If
            vSqlCmd = New SqlCommand(vSqlStr, cn)
            Dr = vSqlCmd.ExecuteReader()

            ComboBoxStartPort.Items.Clear()
            While Dr.Read()
                ComboBoxStartPort.Items.Add(Dr.Item("PORT_ID").ToString)
            End While

            Dr.Close()
            cn.Close()
        Catch r As Exception
            bRet = False
        End Try

        Return bRet
    End Function

    Function GetEndSubOdfByEndOdf() As Boolean
        Dim vSqlConn As String = ("Server=" + gSQLIP + ";Database=" + gDatabaseName + ";Uid=" + gSQL_ID + ";Pwd=" + gSQL_PASSWORD + ";")
        Dim vSqlStr As String
        Dim bRet As Boolean = True
        Dim iWhile As Integer = 0

        Dim cn As New SqlConnection(vSqlConn)
        Dim vSqlCmd As New SqlCommand
        Dim Dr As SqlDataReader

        ' set ComboBoxEndSubOdf
        ComboBoxEndSubOdf.Items.Clear()
        Try

            cn.Open()
#If NOT_USED_ODF_ID Then
            vSqlStr = "select SUB_ODF_ID from SUB_ODF where ODF_ID = '" + ComboBoxEndOdf.SelectedValue + "'"
#Else
            vSqlStr = "select SUB_ODF_ID from SUB_ODF where ODF_ID = '" + in_strEndOdfId + "'"
#End If
            vSqlCmd = New SqlCommand(vSqlStr, cn)
            Dr = vSqlCmd.ExecuteReader()
            Dim strTmp As String = ""

            While Dr.Read()
                ComboBoxEndSubOdf.Items.Add(Dr.Item("SUB_ODF_ID").ToString)
            End While

            Dr.Close()
            cn.Close()
        Catch r As Exception
            bRet = False
        End Try

        Return bRet
    End Function

    Function GetEndPortByEndSubOdf() As Boolean
        Dim vSqlConn As String = ("Server=" + gSQLIP + ";Database=" + gDatabaseName + ";Uid=" + gSQL_ID + ";Pwd=" + gSQL_PASSWORD + ";")
        Dim vSqlStr As String
        Dim bRet As Boolean = True
        Dim iWhile As Integer = 0

        Dim cn As New SqlConnection(vSqlConn)
        Dim vSqlCmd As New SqlCommand
        Dim Dr As SqlDataReader

        ComboBoxEndPort.Items.Clear()
        Try

            cn.Open()
#If NOT_USED_ODF_ID Then
            vSqlStr = "select PORT_ID from SUB_ODF_PORT where ODF_ID = '" + ComboBoxEndOdf.SelectedValue + "' and SUB_ODF_ID = '" + ComboBoxEndSubOdf.Text + "' and BACK_USED = 0"
#Else
            vSqlStr = "select PORT_ID from SUB_ODF_PORT where ODF_ID = '" + in_strEndOdfId + "' and SUB_ODF_ID = '" + ComboBoxEndSubOdf.Text + "' and BACK_USED = 0"
#End If
            vSqlCmd = New SqlCommand(vSqlStr, cn)
            Dr = vSqlCmd.ExecuteReader()

            While Dr.Read()
                ComboBoxEndPort.Items.Add(Dr.Item("PORT_ID").ToString)
            End While

            Dr.Close()
            cn.Close()
        Catch r As Exception
            bRet = False
        End Try

        Return bRet
    End Function

    Private Sub ComboBoxStartOdf_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxStartOdf.SelectedIndexChanged
#If NOT_USED_ODF_ID Then
#Else
        Return
#End If
        IsPortSame()
        If "" = ComboBoxStartOdf.Text Then
            Return
        End If

        If Not m_bInitUI Then
            Return
        End If

        GetStartSubOdfByStartOdf()

        Dim nItemsCount As Integer = ComboBoxStartSubOdf.Items.Count
        If 0 = nItemsCount Then
            ' MsgBox("没有可用子架")
            Return
        End If

        ComboBoxStartSubOdf.SelectedIndex = 0
    End Sub

    Function IsPortSame() As Boolean
        Return True

        If Not m_bInitUI Then
            Return False
        End If

        If ComboBoxStartOdf.Text <> ComboBoxEndOdf.Text Then
            Return False
        End If

        If ComboBoxStartSubOdf.Text <> ComboBoxEndSubOdf.Text Then
            Return False
        End If

        If ComboBoxStartPort.Text <> ComboBoxEndPort.Text Then
            Return False
        End If

        MsgBox("不可用同样端口！")
        Return True
    End Function

    Private Sub ComboBoxStartSubOdf_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxStartSubOdf.SelectedIndexChanged
#If NOT_USED_ODF_ID Then
        If "" = ComboBoxStartSubOdf.Text Then
            Return
        End If
#Else
#End If
        IsPortSame()

        If Not m_bInitUI Then
            Return
        End If

        GetStartPortByStartSubOdf()

        Dim nItemsCount As Integer = ComboBoxStartPort.Items.Count
        If 0 = nItemsCount Then
            ' MsgBox("没有可用端口")
            Return
        End If

        ComboBoxStartPort.SelectedIndex = 0
    End Sub

    Private Sub ComboBoxEndOdf_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxEndOdf.SelectedIndexChanged
#If NOT_USED_ODF_ID Then
#Else
        Return
#End If
        IsPortSame()

        If "" = ComboBoxEndOdf.Text Then
            Return
        End If

        If Not m_bInitUI Then
            Return
        End If

        GetEndSubOdfByEndOdf()

        Dim nItemsCount As Integer = ComboBoxEndSubOdf.Items.Count
        If 0 = nItemsCount Then
            '  MsgBox("没有可用子架")
            Return
        End If

        ComboBoxEndSubOdf.SelectedIndex = 0
    End Sub

    Private Sub ComboBoxEndSubOdf_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxEndSubOdf.SelectedIndexChanged
#If NOT_USED_ODF_ID Then
        If "" = ComboBoxEndSubOdf.Text Then
            Return
        End If
#Else
#End If
        IsPortSame()

        If Not m_bInitUI Then
            Return
        End If

        GetEndPortByEndSubOdf()

        Dim nItemsCount As Integer = ComboBoxEndPort.Items.Count
        If 0 = nItemsCount Then
            '  MsgBox("没有可用端口")
            Return
        End If

        ComboBoxEndPort.SelectedIndex = 0
    End Sub

    Sub DeleteFiber()
        Dim bRet As Boolean = False

        Dim strActiveStatus As String = gSql.ExecuteSqlStatementS("select ACTIVE_STATUS from FIBER where FIBER_ID = '" + in_strFiberId + "'", "ACTIVE_STATUS")
        If "0" <> strActiveStatus Then
            MsgBox("光纤正在使用中，无法删除！")
            Return
        End If

        'Unlock SubOdfPort
        bRet = gSql.ExecuteSqlStatementB("update SUB_ODF_PORT set BACK_USED = 0 where ODF_ID = '" + in_strStartOdfId + "' and SUB_ODF_ID = '" + in_strStartSubOdfId + "' and PORT_ID ='" + in_strStartPort + "'")
        If Not bRet Then
            MsgBox("解锁前端失败！")
            Return
        End If
        bRet = gSql.ExecuteSqlStatementB("update SUB_ODF_PORT set BACK_USED = 0 where ODF_ID = '" + in_strEndOdfId + "' and SUB_ODF_ID = '" + in_strEndSubOdfId + "' and PORT_ID ='" + in_strEndPort + "'")
        If Not bRet Then
            MsgBox("解锁后端失败！")
            Return
        End If

        bRet = gSql.ExecuteSqlStatementB("delete from FIBER where FIBER_ID = '" + in_strFiberId + "'")
        If Not bRet Then
            MsgBox("删除光纤失败！")
            Return
        End If

        MsgBox("删除光纤成功。")
        o_nEndDlgResult = 2
        Me.Close()
    End Sub

    Private Sub ButtonDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click
        If MsgBox("是否要删除光纤？", MessageBoxButtons.YesNo + vbExclamation, "警告") = DialogResult.Yes Then
            DeleteFiber()
        End If
    End Sub
End Class