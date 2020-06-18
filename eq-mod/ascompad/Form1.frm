VERSION 5.00
Begin VB.Form HC 
   AutoRedraw      =   -1  'True
   BackColor       =   &H00000000&
   Caption         =   "ASCOMPad V2.07"
   ClientHeight    =   3255
   ClientLeft      =   165
   ClientTop       =   855
   ClientWidth     =   4170
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   8.25
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "Form1.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   3255
   ScaleWidth      =   4170
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame3 
      BackColor       =   &H00000000&
      Caption         =   "FilterWheel"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000080FF&
      Height          =   1095
      Left            =   120
      TabIndex        =   20
      Top             =   2040
      Width           =   2175
      Begin VB.ComboBox Combo5 
         BackColor       =   &H00000080&
         ForeColor       =   &H000000FF&
         Height          =   330
         Left            =   120
         Style           =   2  'Dropdown List
         TabIndex        =   22
         Top             =   240
         Width           =   1935
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   375
         Left            =   120
         TabIndex        =   21
         Top             =   600
         Width           =   1935
      End
   End
   Begin VB.Timer Timer1 
      Interval        =   100
      Left            =   2520
      Top             =   240
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00000000&
      Caption         =   "Focuser"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000080FF&
      Height          =   2055
      Left            =   120
      TabIndex        =   15
      Top             =   0
      Width           =   2175
      Begin VB.CommandButton Command9 
         BackColor       =   &H0095C1CB&
         Height          =   375
         Left            =   120
         Picture         =   "Form1.frx":0CCA
         Style           =   1  'Graphical
         TabIndex        =   26
         ToolTipText     =   "Gamepad Lock"
         Top             =   1560
         Width           =   375
      End
      Begin VB.CommandButton Command8 
         BackColor       =   &H0095C1CB&
         Height          =   375
         Left            =   1680
         Picture         =   "Form1.frx":124C
         Style           =   1  'Graphical
         TabIndex        =   25
         ToolTipText     =   "Bookmarks"
         Top             =   1560
         Width           =   375
      End
      Begin VB.PictureBox Picture1 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         ForeColor       =   &H80000008&
         Height          =   315
         Index           =   1
         Left            =   1740
         Picture         =   "Form1.frx":17CE
         ScaleHeight     =   285
         ScaleWidth      =   285
         TabIndex        =   24
         ToolTipText     =   "Out"
         Top             =   667
         Width           =   315
      End
      Begin VB.PictureBox Picture1 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         ForeColor       =   &H80000008&
         Height          =   315
         Index           =   0
         Left            =   120
         Picture         =   "Form1.frx":1D50
         ScaleHeight     =   285
         ScaleWidth      =   285
         TabIndex        =   23
         ToolTipText     =   "In"
         Top             =   667
         Width           =   315
      End
      Begin VB.ComboBox Combo2 
         BackColor       =   &H00000080&
         ForeColor       =   &H000000FF&
         Height          =   330
         ItemData        =   "Form1.frx":22D2
         Left            =   120
         List            =   "Form1.frx":22E2
         Style           =   2  'Dropdown List
         TabIndex        =   16
         Top             =   1080
         Width           =   1935
      End
      Begin VB.ComboBox Combo3 
         BackColor       =   &H00000080&
         ForeColor       =   &H000000FF&
         Height          =   330
         ItemData        =   "Form1.frx":22F2
         Left            =   120
         List            =   "Form1.frx":22FC
         Style           =   2  'Dropdown List
         TabIndex        =   19
         Top             =   240
         Width           =   1935
      End
      Begin VB.CheckBox gamepadlock 
         BackColor       =   &H00000000&
         Caption         =   "Gamepad Lock"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   120
         TabIndex        =   18
         Top             =   2160
         Visible         =   0   'False
         Width           =   1935
      End
      Begin VB.Label LabelPos 
         Alignment       =   2  'Center
         BackColor       =   &H00000000&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   375
         Left            =   480
         TabIndex        =   17
         ToolTipText     =   "Double Click for presets."
         Top             =   637
         Width           =   1215
      End
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00000000&
      Caption         =   "Mount"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000080FF&
      Height          =   3135
      Left            =   2400
      TabIndex        =   0
      Top             =   0
      Width           =   1695
      Begin VB.CommandButton Command2 
         BackColor       =   &H0095C1CB&
         Caption         =   "N"
         Height          =   375
         Index           =   0
         Left            =   720
         Style           =   1  'Graphical
         TabIndex        =   14
         Top             =   240
         Width           =   375
      End
      Begin VB.CommandButton Command2 
         BackColor       =   &H0095C1CB&
         Caption         =   "S"
         Height          =   375
         Index           =   1
         Left            =   720
         Style           =   1  'Graphical
         TabIndex        =   13
         Top             =   960
         Width           =   375
      End
      Begin VB.CommandButton Command2 
         BackColor       =   &H0095C1CB&
         Caption         =   "W"
         Height          =   375
         Index           =   2
         Left            =   360
         Style           =   1  'Graphical
         TabIndex        =   12
         Top             =   600
         Width           =   375
      End
      Begin VB.CommandButton Command2 
         BackColor       =   &H0095C1CB&
         Caption         =   "E"
         Height          =   375
         Index           =   3
         Left            =   1080
         Style           =   1  'Graphical
         TabIndex        =   11
         Top             =   600
         Width           =   375
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Reverse DEC"
         BeginProperty Font 
            Name            =   "Arial Narrow"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   330
         Index           =   1
         Left            =   360
         TabIndex        =   10
         Top             =   1320
         Width           =   1215
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Reverse RA"
         BeginProperty Font 
            Name            =   "Arial Narrow"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   330
         Index           =   0
         Left            =   360
         TabIndex        =   9
         Top             =   1560
         Width           =   1215
      End
      Begin VB.CommandButton Command3 
         BackColor       =   &H0095C1CB&
         Caption         =   "Park"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         Style           =   1  'Graphical
         TabIndex        =   8
         Top             =   2280
         Width           =   735
      End
      Begin VB.CommandButton Command4 
         BackColor       =   &H0095C1CB&
         Caption         =   "UnPark"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   840
         Style           =   1  'Graphical
         TabIndex        =   7
         Top             =   2280
         Width           =   735
      End
      Begin VB.CommandButton Command5 
         Height          =   375
         Left            =   720
         Picture         =   "Form1.frx":2316
         Style           =   1  'Graphical
         TabIndex        =   6
         Top             =   600
         Width           =   375
      End
      Begin VB.CommandButton Command1 
         Height          =   375
         Index           =   0
         Left            =   120
         Picture         =   "Form1.frx":2898
         Style           =   1  'Graphical
         TabIndex        =   5
         ToolTipText     =   "Sidreal Tracking"
         Top             =   2640
         Width           =   375
      End
      Begin VB.CommandButton Command1 
         Height          =   375
         Index           =   1
         Left            =   480
         Picture         =   "Form1.frx":2E1A
         Style           =   1  'Graphical
         TabIndex        =   4
         ToolTipText     =   "Lunar Tracking"
         Top             =   2640
         Width           =   375
      End
      Begin VB.CommandButton Command1 
         Height          =   375
         Index           =   2
         Left            =   840
         Picture         =   "Form1.frx":339C
         Style           =   1  'Graphical
         TabIndex        =   3
         ToolTipText     =   "Solar Tracking"
         Top             =   2640
         Width           =   375
      End
      Begin VB.CommandButton Command1 
         Height          =   375
         Index           =   3
         Left            =   1200
         Picture         =   "Form1.frx":391E
         Style           =   1  'Graphical
         TabIndex        =   2
         ToolTipText     =   "Custom Tracking"
         Top             =   2640
         Width           =   375
      End
      Begin VB.ComboBox Combo1 
         BackColor       =   &H00000080&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   330
         ItemData        =   "Form1.frx":3EA0
         Left            =   360
         List            =   "Form1.frx":3EC2
         TabIndex        =   1
         Text            =   "Combo1"
         Top             =   1920
         Width           =   735
      End
   End
   Begin VB.Menu Hide 
      Caption         =   "Hide"
   End
   Begin VB.Menu config 
      Caption         =   "Devices"
      Begin VB.Menu jstick 
         Caption         =   "Gampad"
         Begin VB.Menu assign 
            Caption         =   "Assign Buttons"
         End
      End
      Begin VB.Menu focuser 
         Caption         =   "Focuser1"
         Begin VB.Menu FProps 
            Caption         =   "Setup"
         End
         Begin VB.Menu Focuserdriver 
            Caption         =   "Connect"
         End
         Begin VB.Menu focusdisconnect 
            Caption         =   "Disconnect"
         End
      End
      Begin VB.Menu focuser2 
         Caption         =   "Focuser2"
         Begin VB.Menu Focuser2Setup 
            Caption         =   "Setup"
         End
         Begin VB.Menu Focuser2Connect 
            Caption         =   "Connect"
         End
         Begin VB.Menu Focuser2Disconnect 
            Caption         =   "Disconnect"
         End
      End
      Begin VB.Menu fw 
         Caption         =   "FilterWheel"
         Begin VB.Menu fwsetup 
            Caption         =   "Setup"
         End
         Begin VB.Menu fwconnect 
            Caption         =   "Connect"
         End
         Begin VB.Menu fwdisconnect 
            Caption         =   "Disconnect"
         End
      End
      Begin VB.Menu telescope 
         Caption         =   "Mount"
         Begin VB.Menu SetupTelescope 
            Caption         =   "Setup"
         End
         Begin VB.Menu AscomChoose 
            Caption         =   "Connect"
         End
         Begin VB.Menu MountDisconnect 
            Caption         =   "Disconnect"
         End
      End
   End
   Begin VB.Menu mPopupsys 
      Caption         =   "&SysTray"
      Visible         =   0   'False
      Begin VB.Menu mPopRestore 
         Caption         =   "&Restore"
      End
      Begin VB.Menu popsep2 
         Caption         =   "------------"
      End
      Begin VB.Menu mpopResetW 
         Caption         =   "Reset Window Position"
      End
      Begin VB.Menu popsep 
         Caption         =   "------------"
      End
      Begin VB.Menu mPopExit 
         Caption         =   "&Exit"
      End
   End
End
Attribute VB_Name = "HC"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public RRATE As Long
Public DRATE As Long

Const HWND_TOPMOST = -1
Const HWND_NOTTOPMOST = -2
Const SWP_NOMOVE = &H2
Const SWP_NOSIZE = &H1

Private Const SC_CLOSE As Long = &HF060&
Private Const MIIM_STATE As Long = &H1&
Private Const MIIM_ID As Long = &H2&
Private Const MFS_GRAYED As Long = &H3&
Private Const WM_NCACTIVATE As Long = &H86

Public oPersist As New Persist
Private Declare Function SetWindowPos Lib "user32" _
         (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, _
          ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long

 

Private m_Util As DriverHelper.Util

Private Sub AscomChoose_Click()
    Call ChooseTelescope
End Sub



Private Sub assign_Click()
    JStickConfigForm.Show (1)
End Sub

Private Sub Check1_Click(Index As Integer)
    ReverseRa = Check1(0).Value
    ReverseDEC = Check1(0).Value
End Sub

Private Sub Combo1_Change()
Dim idx As Integer
    idx = val(Combo1.List(Combo1.ListIndex)) - 1
    
    currentrate = rates(idx)
End Sub

Private Sub Combo1_Click()
    Combo1_Change
End Sub


Private Sub Combo2_Click()
    FocuserProps.ActiveDelta = FocuserProps.Speeds(Combo2.ListIndex).deltaStep
    FocuserProps.ActiveType = FocuserProps.Speeds(Combo2.ListIndex).type
End Sub

Private Sub Combo3_Click()
    ActiveFocuser = Combo3.ListIndex
    Call LoadFSpeeds
    Call LoadFBookmarks
    Call ascomFocusGetProperties(ActiveFocuser)
    ' set lowest speed.
    Combo2.ListIndex = 0
End Sub

Private Sub Combo5_Click()
    If Combo5.ListIndex <> FilterWheelProps.Position Then
        ascomFilterWheelSelect (Combo5.ListIndex)
    End If
End Sub

Private Sub Command1_Click(Index As Integer)
    Select Case Index
        Case 0
            Start_sidereal
        Case 1
            Start_Lunar
        Case 2
            Start_Solar
        Case 3
           Start_Custom
    End Select
    
End Sub



Private Sub Command2_MouseDown(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
Dim rate As Double
    If Not m_scope Is Nothing Then
        If m_scope.Connected Then
            Select Case Index
                Case 0
                    ' RA positive rate
                    North_Down
                Case 1
                    ' RA negative rate
                    South_Down
                Case 2
                    ' DEC positive rate
                    East_Down
                Case 3
                    ' DEC negative rate
                    West_Down
                
            End Select
        End If
    End If
End Sub

Private Sub Command2_MouseUp(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Not m_scope Is Nothing Then
        If m_scope.Connected Then
            Select Case Index
                Case 0
                    ' RA positive rate
                    North_Up
                Case 1
                    ' RA negative rate
                    South_Up
                Case 2
                    ' DEC positive rate
                    East_Up
                Case 3
                    ' DEC negative rate
                    West_Up
            End Select
        End If
    End If
End Sub

Private Sub Command3_Click()
    Park
End Sub

Private Sub Command4_Click()
    UnPark
End Sub

Private Sub Command5_Click()
    emergency_stop
End Sub

Private Sub Command6_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    ascomFocuserIn
End Sub

Private Sub Command6_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
    ascomFocuserStop
End Sub


Private Sub Command7_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    ascomFocuserOut
End Sub

Private Sub Command7_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
    ascomFocuserStop
End Sub

Private Sub Command8_Click()
    Call LabelPos_DblClick
End Sub

Private Sub Command9_Click()
    If gamepadlock.Value = 0 Then
        gamepadlock.Value = 1
    Else
        gamepadlock.Value = 0
    End If
End Sub

Private Sub focusdisconnect_Click()
    Call ascomFocusDisconnect
    Call DisableFocuserControls
End Sub

Private Sub Focuser2Connect_Click()
    Call ChooseFocuser2
End Sub

Private Sub Focuser2Disconnect_Click()
    Call ascomFocus2Disconnect
    Call DisableFocuserControls
End Sub

Private Sub Focuser2Setup_Click()
    FPropsDlg.fid = 1
    FPropsDlg.Show (1)
End Sub

Private Sub Focuserdriver_Click()
    Call ChooseFocuser
End Sub

Private Sub fwconnect_Click()
    Call ChooseFilterWheel
End Sub

Private Sub fwdisconnect_Click()
    Call ascomFilterWheelDisconnect
    Call DisableFilterWheelControls
End Sub

Private Sub Form_Load()
Dim tmptxt As String
    Set m_Util = New DriverHelper.Util
    
    ' Individual tab values
    Tabulator(1) = 10
    Tabulator(2) = 40
    Tabulator(3) = 120
    SendMessage Combo2.hwnd, LB_SETTABSTOPS, OBorder, Tabulator(1)

    Me.Show
    Me.Refresh
    With nid
        .cbSize = Len(nid)
        .hwnd = Me.hwnd
        .uId = vbNull
        .uFlags = NIF_ICON Or NIF_TIP Or NIF_MESSAGE
        .uCallBackMessage = WM_MOUSEMOVE
        .hIcon = Me.Icon
        .szTip = "ASCOMPAD" & vbNullChar
    End With
    Shell_NotifyIcon NIM_ADD, nid
    
    Call readBeep
    
    
    tmptxt = HC.oPersist.ReadIniValue("form_width")
    If tmptxt = "" Then
        Call HC.oPersist.WriteIniValue("form_width", Me.Width)
    Else
        Me.Width = val(tmptxt)
    End If
    
    tmptxt = HC.oPersist.ReadIniValue("form_top")
    If tmptxt = "" Then
        Call HC.oPersist.WriteIniValue("form_top", Me.Top)
    Else
        Me.Top = val(tmptxt)
    End If
    
    tmptxt = HC.oPersist.ReadIniValue("form_left")
    If tmptxt = "" Then
        Call HC.oPersist.WriteIniValue("form_left", Me.Left)
    Else
        Me.Left = val(tmptxt)
    End If
    
    FocusingEnabled = 1

    LoadJoystickBtns
    LoadJoystickCalib
    LoadRates
    LoadFSpeeds
    Call LoadFBookmarks

    Combo1.ListIndex = 0
    
    If ascomConnect(oPersist.ReadIniValue("driver")) = True Then
        Call EnableMountControls
    Else
        Call DisableMountControls
    End If
    
    If ascomFocusConnect(oPersist.ReadIniValue("focuserdriver")) = True Then
        Call EnableFocuserControls
    Else
        Call DisableFocuserControls
    End If

    If ascomFilterWheelConnect(oPersist.ReadIniValue("filterwheeldriver")) = True Then
        Call EnableFilterWheelControls
    Else
        Call DisableFilterWheelControls
    End If


    If ascomFocus2Connect(oPersist.ReadIniValue("focuser2driver")) = True Then
        Combo3.enabled = True
    Else
        Combo3.enabled = False
    End If

    ' select focuser 1
    Combo3.ListIndex = 0

    If FocuserProps.CanReportPosition Then
        LabelPos.Visible = True
        Command8.Visible = True
    Else
        LabelPos.Visible = False
        Command8.Visible = False
    End If
    Combo2.ListIndex = 2
    Timer1.enabled = True
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Shell_NotifyIcon NIM_DELETE, nid
    
    On Error Resume Next
    
    If Not m_scope Is Nothing Then
        m_scope.Connected = False
    End If
    
    If Not m_focuser Is Nothing Then
        m_focuser.Link = False
        Set m_focuser = Nothing
    End If
    
    If Not m_focuser2 Is Nothing Then
        m_focuser2.Link = False
        Set m_focuser2 = Nothing
    End If
    
    
    If Not m_filterwheel Is Nothing Then
        m_filterwheel.Connected = False
    End If
    Set m_filterwheel = Nothing
    
    Call HC.oPersist.WriteIniValue("form_width", Me.Width)
    Call HC.oPersist.WriteIniValue("form_top", Me.Top)
    Call HC.oPersist.WriteIniValue("form_left", Me.Left)
    
    Unload ASCOMStatus
    Unload BookmarkForm
    Unload FileDlg
    Unload FPropsDlg
    Unload FWPropsDlg
    Unload JStickConfigForm
    Unload RatesFrm
    Unload GetNumDlg

End Sub

Private Sub Form_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
'this procedure receives the callbacks from the System Tray icon.
Dim Result As Long
Dim msg As Long
    'the value of X will vary depending upon the scalemode setting
    If Me.ScaleMode = vbPixels Then
        msg = X
    Else
        msg = X / Screen.TwipsPerPixelX
    End If
    Select Case msg
        Case WM_LBUTTONUP        '514 restore form window
            Me.WindowState = vbNormal
            Result = SetForegroundWindow(Me.hwnd)
            Me.Show
        Case WM_LBUTTONDBLCLK    '515 restore form window
            Me.WindowState = vbNormal
            Result = SetForegroundWindow(Me.hwnd)
            Me.Show
        Case WM_RBUTTONUP        '517 display popup menu
            Result = SetForegroundWindow(Me.hwnd)
            Me.PopupMenu Me.mPopupsys
    End Select
End Sub

Private Sub Form_Resize()
    'this is necessary to assure that the minimized window is hidden
    If Me.WindowState = vbMinimized Then Me.Hide
End Sub



Private Sub fwsetup_Click()
        FWPropsDlg.Show (1)
End Sub

Private Sub Hide_Click()
    Me.Hide
End Sub

Private Sub LabelPos_DblClick()
    If LabelPos.Visible Then
        BookmarkForm.Show (1)
        If BookmarkForm.Destination <> FocuserProps.Position Then
            Call ascomFocuserGoto(BookmarkForm.Destination)
        End If
    End If
End Sub

Private Sub mPopExit_Click()
    'called when user clicks the popup menu Exit command
    Unload Me
End Sub

Private Sub mpopResetW_Click()
    Dim Result As Long
    On Error Resume Next
    Me.Top = 0
    Me.Left = 0
    Me.WindowState = vbNormal
    Result = SetForegroundWindow(Me.hwnd)
    Me.Show
End Sub

Private Sub mPopRestore_Click()
    'called when the user clicks the popup menu Restore command
    Dim Result As Long
    Me.WindowState = vbNormal
    Result = SetForegroundWindow(Me.hwnd)
    Me.Show
End Sub


Private Sub FProps_Click()
    FPropsDlg.fid = 0
    FPropsDlg.Show (1)
    Call EnableFocuserControls
End Sub

Private Sub gamepadlock_Click()
    If gamepadlock.Value = 0 Then
        Command9.Picture = LoadResPicture(107, vbResBitmap)
        FocusingEnabled = 1
    Else
        FocusingEnabled = 0
        Command9.Picture = LoadResPicture(108, vbResBitmap)
    End If
End Sub


Private Sub MountDisconnect_Click()
    Call ascomDisconnect
End Sub


Private Sub Picture1_MouseDown(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
    
    Select Case Index
    
        Case 0
            Picture1(0).Picture = LoadResPicture(102, vbResBitmap)
            ascomFocuserIn

        Case 1
            Picture1(1).Picture = LoadResPicture(105, vbResBitmap)
            ascomFocuserOut
    
    End Select

End Sub

Private Sub Picture1_MouseUp(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)
    
    Select Case Index
    
        Case 0
            Picture1(0).Picture = LoadResPicture(103, vbResBitmap)
            ascomFocuserStop


        Case 1
            Picture1(1).Picture = LoadResPicture(106, vbResBitmap)
            ascomFocuserStop
    
    End Select

End Sub

Private Sub SetupTelescope_Click()
        RatesFrm.Show (1)
End Sub

Private Sub Timer1_Timer()
On Error Resume Next
    
    If Not m_filterwheel Is Nothing Then
        AscomFilterWheelTimer
    End If
    
    
    If Not m_focuser Is Nothing Then
        EQ_JoystickPoller
        AscomFocuserTimer
    Else
        If Not m_focuser2 Is Nothing Then
            EQ_JoystickPoller
            AscomFocuserTimer
        Else
            If Not m_scope Is Nothing Then
                If m_scope.Connected Then
                    EQ_JoystickPoller
                End If
            End If
        End If
    End If
    
End Sub



Public Sub EnableFocuserControls()
    Picture1(0).Picture = LoadResPicture(103, vbResBitmap)
    Picture1(1).Picture = LoadResPicture(106, vbResBitmap)
    Combo2.enabled = True
    Command9.enabled = True
    If FocuserProps.Absolute Then
        LabelPos.Visible = True
        Command8.enabled = True
    Else
        LabelPos.Visible = False
        Command8.enabled = False
    End If

End Sub
Public Sub DisableFocuserControls()
    If m_focuser Is Nothing And m_focuser2 Is Nothing Then
        Picture1(0).Picture = LoadResPicture(101, vbResBitmap)
        Picture1(1).Picture = LoadResPicture(104, vbResBitmap)
        Combo2.enabled = False
        Command8.enabled = False
        Command9.enabled = False
        LabelPos.Visible = False
    End If
End Sub

Public Sub EnableMountControls()
    Command1(0).enabled = True
    Command1(1).enabled = True
    Command1(2).enabled = True
    Command1(3).enabled = True
    Command2(0).enabled = True
    Command2(1).enabled = True
    Command2(2).enabled = True
    Command2(3).enabled = True
    Command3.enabled = True
    Command4.enabled = True
    Command5.enabled = True
    Combo1.enabled = True
    Check1(0).enabled = True
    Check1(1).enabled = True
End Sub
Public Sub DisableMountControls()
    Command1(0).enabled = False
    Command1(1).enabled = False
    Command1(2).enabled = False
    Command1(3).enabled = False
    Command2(0).enabled = False
    Command2(1).enabled = False
    Command2(2).enabled = False
    Command2(3).enabled = False
    Command3.enabled = False
    Command4.enabled = False
    Command5.enabled = False
    Combo1.enabled = False
    Check1(0).enabled = False
    Check1(1).enabled = False
End Sub
Public Sub ChooseFocuser2()
    Dim id As String
    Dim Chsr As DriverHelper.Chooser
    
    id = oPersist.ReadIniValue("focuserdriver")
    
    Set Chsr = New DriverHelper.Chooser
    Chsr.DeviceType = "Focuser"
    '  you can remember the scope ID and set it to give you the last one used
    id = Chsr.Choose(id)
    If id <> "" Then
        Call oPersist.WriteIniValue("focuser2driver", id)
        If ascomFocus2Connect(id) = True Then
            Call EnableFocuserControls
        Else
            Call DisableFocuserControls
        End If
    End If

End Sub
Public Sub ChooseFocuser()
    Dim id As String
    Dim Chsr As DriverHelper.Chooser
    
    id = oPersist.ReadIniValue("focuserdriver")
    
    Set Chsr = New DriverHelper.Chooser
    Chsr.DeviceType = "Focuser"
    '  you can remember the scope ID and set it to give you the last one used
    id = Chsr.Choose(id)
    If id <> "" Then
        Call oPersist.WriteIniValue("focuserdriver", id)
        If ascomFocusConnect(id) = True Then
            Call EnableFocuserControls
        Else
            Call DisableFocuserControls
        End If
    End If

End Sub
Public Sub ChooseFilterWheel()
    Dim id As String
    Dim Chsr As DriverHelper.Chooser
    
    id = oPersist.ReadIniValue("filterwheeldriver")
    
    Set Chsr = New DriverHelper.Chooser
    Chsr.DeviceType = "FilterWheel"
    '  you can remember the scope ID and set it to give you the last one used
    id = Chsr.Choose(id)
    If id <> "" Then
        Call oPersist.WriteIniValue("filterwheeldriver", id)
        If ascomFilterWheelConnect(id) = True Then
            Call EnableFilterWheelControls
        Else
            Call DisableFilterWheelControls
        End If
    End If

End Sub

Public Sub ChooseTelescope()
    Dim id As String
    Dim Chsr As DriverHelper.Chooser
    
    id = oPersist.ReadIniValue("driver")
    
    Set Chsr = New DriverHelper.Chooser
    Chsr.DeviceType = "Telescope"
    '  you can remember the scope ID and set it to give you the last one used
    id = Chsr.Choose(id)
    If id <> "" Then
        If ascomConnect(id) = True Then
            Call EnableMountControls
            Call oPersist.WriteIniValue("driver", id)
        Else
            Call DisableMountControls
        End If
    End If

End Sub

Public Sub EnableFilterWheelControls()
End Sub

Public Sub DisableFilterWheelControls()
    If m_filterwheel Is Nothing Then
        Combo5.enabled = False
    End If
End Sub
