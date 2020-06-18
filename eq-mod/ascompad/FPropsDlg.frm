VERSION 5.00
Begin VB.Form FPropsDlg 
   BackColor       =   &H00000000&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Focuser Properties"
   ClientHeight    =   7125
   ClientLeft      =   45
   ClientTop       =   315
   ClientWidth     =   8070
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "FPropsDlg.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7125
   ScaleWidth      =   8070
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.Frame Frame5 
      BackColor       =   &H00000000&
      Caption         =   "Movement Presets"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000080FF&
      Height          =   1455
      Left            =   120
      TabIndex        =   61
      Top             =   5520
      Width           =   4335
      Begin VB.CommandButton Command9 
         BackColor       =   &H0095C1CB&
         Caption         =   "Set"
         Height          =   255
         Left            =   2160
         Style           =   1  'Graphical
         TabIndex        =   65
         Top             =   1080
         Width           =   1935
      End
      Begin VB.ListBox List1 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   900
         Left            =   120
         TabIndex        =   64
         Top             =   240
         Width           =   1935
      End
      Begin VB.HScrollBar VScroll1 
         Height          =   135
         Left            =   2160
         TabIndex        =   63
         Top             =   840
         Width           =   1935
      End
      Begin VB.ComboBox Combo4 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   330
         ItemData        =   "FPropsDlg.frx":0CCA
         Left            =   2160
         List            =   "FPropsDlg.frx":0CD4
         Style           =   2  'Dropdown List
         TabIndex        =   62
         Top             =   240
         Width           =   1935
      End
      Begin VB.Label Label26 
         BackColor       =   &H00000000&
         Caption         =   "Steps to move"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2160
         TabIndex        =   67
         Top             =   600
         Width           =   1215
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000040&
         Caption         =   "0"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   1
         Left            =   3360
         TabIndex        =   66
         ToolTipText     =   "Click to edit"
         Top             =   600
         Width           =   735
      End
   End
   Begin VB.Frame Frame4 
      BackColor       =   &H00000000&
      Caption         =   "Sounds"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000080FF&
      Height          =   6855
      Left            =   4560
      TabIndex        =   25
      Top             =   120
      Width           =   3375
      Begin VB.CommandButton CmdApply 
         BackColor       =   &H0095C1CB&
         Caption         =   "Apply Changes"
         Height          =   255
         Left            =   120
         Style           =   1  'Graphical
         TabIndex        =   60
         Top             =   6360
         Width           =   3015
      End
      Begin VB.CommandButton CmdLoadF1 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Left            =   480
         Picture         =   "FPropsDlg.frx":0CEE
         Style           =   1  'Graphical
         TabIndex        =   39
         Top             =   750
         Width           =   350
      End
      Begin VB.CheckBox ChkFSelect 
         BackColor       =   &H00000000&
         Caption         =   "Focuser Select"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   120
         TabIndex        =   38
         TabStop         =   0   'False
         Top             =   240
         Width           =   2775
      End
      Begin VB.CommandButton CmdLoadFLOn 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Left            =   480
         Picture         =   "FPropsDlg.frx":1270
         Style           =   1  'Graphical
         TabIndex        =   37
         Top             =   2160
         Width           =   350
      End
      Begin VB.CheckBox ChkFocusLock 
         BackColor       =   &H00000000&
         Caption         =   "Focus Lock"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   120
         TabIndex        =   36
         TabStop         =   0   'False
         Top             =   1680
         Width           =   2775
      End
      Begin VB.CommandButton CmdLoadFLOff 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Left            =   480
         Picture         =   "FPropsDlg.frx":17F2
         Style           =   1  'Graphical
         TabIndex        =   35
         Top             =   2760
         Width           =   350
      End
      Begin VB.CheckBox ChkFouserSteps 
         BackColor       =   &H00000000&
         Caption         =   "Focuser Step Presets"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   120
         TabIndex        =   34
         TabStop         =   0   'False
         Top             =   3120
         Width           =   2895
      End
      Begin VB.CommandButton CmdLoadRate 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Index           =   10
         Left            =   480
         Picture         =   "FPropsDlg.frx":1D74
         Style           =   1  'Graphical
         TabIndex        =   33
         Top             =   3360
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadRate 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Index           =   11
         Left            =   480
         Picture         =   "FPropsDlg.frx":22F6
         Style           =   1  'Graphical
         TabIndex        =   32
         Top             =   3720
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadRate 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Index           =   12
         Left            =   480
         Picture         =   "FPropsDlg.frx":2878
         Style           =   1  'Graphical
         TabIndex        =   31
         Top             =   4080
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadF2 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Left            =   480
         Picture         =   "FPropsDlg.frx":2DFA
         Style           =   1  'Graphical
         TabIndex        =   30
         Top             =   1320
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadOut 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Left            =   480
         Picture         =   "FPropsDlg.frx":337C
         Style           =   1  'Graphical
         TabIndex        =   29
         Top             =   5880
         Width           =   350
      End
      Begin VB.CheckBox ChkFMove 
         BackColor       =   &H00000000&
         Caption         =   "Focuser Movement"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   120
         TabIndex        =   28
         TabStop         =   0   'False
         Top             =   4800
         Width           =   2775
      End
      Begin VB.CommandButton CmdLoadIn 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Left            =   480
         Picture         =   "FPropsDlg.frx":38FE
         Style           =   1  'Graphical
         TabIndex        =   27
         Top             =   5280
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadRate 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   250
         Index           =   13
         Left            =   480
         Picture         =   "FPropsDlg.frx":3E80
         Style           =   1  'Graphical
         TabIndex        =   26
         Top             =   4440
         Width           =   350
      End
      Begin VB.Label LabelF1 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   960
         TabIndex        =   59
         Top             =   750
         Width           =   2055
      End
      Begin VB.Label LabelFLOn 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   960
         TabIndex        =   58
         Top             =   2160
         Width           =   2055
      End
      Begin VB.Label Label25 
         BackColor       =   &H00000000&
         Caption         =   "On"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   480
         TabIndex        =   57
         Top             =   1920
         Width           =   2535
      End
      Begin VB.Label LabelFLOff 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   960
         TabIndex        =   56
         Top             =   2760
         Width           =   2055
      End
      Begin VB.Label Label24 
         BackColor       =   &H00000000&
         Caption         =   "Off"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   480
         TabIndex        =   55
         Top             =   2520
         Width           =   2775
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   10
         Left            =   960
         TabIndex        =   54
         Top             =   3360
         Width           =   2055
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   11
         Left            =   960
         TabIndex        =   53
         Top             =   3720
         Width           =   2055
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   12
         Left            =   960
         TabIndex        =   52
         Top             =   4080
         Width           =   2055
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "1"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   10
         Left            =   120
         TabIndex        =   51
         Top             =   3360
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "2"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   11
         Left            =   120
         TabIndex        =   50
         Top             =   3720
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "3"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   12
         Left            =   120
         TabIndex        =   49
         Top             =   4080
         Width           =   285
      End
      Begin VB.Label LabelF2 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   960
         TabIndex        =   48
         Top             =   1320
         Width           =   2055
      End
      Begin VB.Label Label23 
         BackColor       =   &H00000000&
         Caption         =   "Focuser1"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   480
         TabIndex        =   47
         Top             =   480
         Width           =   2535
      End
      Begin VB.Label Label22 
         BackColor       =   &H00000000&
         Caption         =   "Focuser2"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   480
         TabIndex        =   46
         Top             =   1080
         Width           =   2535
      End
      Begin VB.Label Label21 
         BackColor       =   &H00000000&
         Caption         =   "Out"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   480
         TabIndex        =   45
         Top             =   5640
         Width           =   2535
      End
      Begin VB.Label LabelOut 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   960
         TabIndex        =   44
         Top             =   5880
         Width           =   2055
      End
      Begin VB.Label Label20 
         BackColor       =   &H00000000&
         Caption         =   "In"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   480
         TabIndex        =   43
         Top             =   5040
         Width           =   2535
      End
      Begin VB.Label LabelIn 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   960
         TabIndex        =   42
         Top             =   5280
         Width           =   2055
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   13
         Left            =   960
         TabIndex        =   41
         Top             =   4440
         Width           =   2055
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "4"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   13
         Left            =   120
         TabIndex        =   40
         Top             =   4440
         Width           =   285
      End
   End
   Begin VB.Frame Frame3 
      BackColor       =   &H00000000&
      Caption         =   "ASCOM "
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000080FF&
      Height          =   975
      Left            =   120
      TabIndex        =   20
      Top             =   120
      Width           =   4335
      Begin VB.CommandButton Command2 
         BackColor       =   &H0095C1CB&
         Caption         =   "Setup"
         Height          =   255
         Left            =   2760
         Style           =   1  'Graphical
         TabIndex        =   68
         Top             =   600
         Width           =   1095
      End
      Begin VB.CommandButton Command1 
         BackColor       =   &H0095C1CB&
         Caption         =   "Select"
         Height          =   255
         Left            =   120
         Style           =   1  'Graphical
         TabIndex        =   22
         Top             =   600
         Width           =   1095
      End
      Begin VB.CommandButton Command3 
         BackColor       =   &H0095C1CB&
         Caption         =   "Remove"
         Height          =   255
         Left            =   1440
         Style           =   1  'Graphical
         TabIndex        =   21
         Top             =   600
         Width           =   1095
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000000&
         Caption         =   "Driver ID"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   10
         Left            =   120
         TabIndex        =   24
         Top             =   240
         Width           =   975
      End
      Begin VB.Label Label19 
         BackColor       =   &H00000000&
         Caption         =   "Label19"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1200
         TabIndex        =   23
         Top             =   240
         Width           =   3015
      End
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00000000&
      Caption         =   "Operating Parameters"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000080FF&
      Height          =   1815
      Left            =   120
      TabIndex        =   11
      Top             =   1320
      Width           =   4335
      Begin VB.Label Label1 
         BackColor       =   &H00000000&
         Caption         =   "Focuser Type:"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   19
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "Absolute"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2640
         TabIndex        =   18
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "Max Increment:"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   17
         Top             =   720
         Width           =   1215
      End
      Begin VB.Label Label4 
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2640
         TabIndex        =   16
         Top             =   720
         Width           =   1335
      End
      Begin VB.Label Label5 
         BackColor       =   &H00000000&
         Caption         =   "Max Step:"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   120
         TabIndex        =   15
         Top             =   1080
         Width           =   1215
      End
      Begin VB.Label Label6 
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2640
         TabIndex        =   14
         Top             =   1080
         Width           =   1335
      End
      Begin VB.Label Label7 
         BackColor       =   &H00000000&
         Caption         =   "Step Size:"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   120
         TabIndex        =   13
         Top             =   1440
         Width           =   1215
      End
      Begin VB.Label Label8 
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2640
         TabIndex        =   12
         Top             =   1440
         Width           =   1335
      End
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00000000&
      Caption         =   "Capabilities"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000080FF&
      Height          =   2175
      Left            =   120
      TabIndex        =   0
      Top             =   3240
      Width           =   4335
      Begin VB.Label Label9 
         BackColor       =   &H00000000&
         Caption         =   "CanHalt"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   120
         TabIndex        =   10
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label10 
         BackColor       =   &H00000000&
         Caption         =   "No"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2640
         TabIndex        =   9
         Top             =   360
         Width           =   1335
      End
      Begin VB.Label Label11 
         BackColor       =   &H00000000&
         Caption         =   "Temp Compensation Available"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   120
         TabIndex        =   8
         Top             =   720
         Width           =   2175
      End
      Begin VB.Label Label12 
         BackColor       =   &H00000000&
         Caption         =   "No"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2640
         TabIndex        =   7
         Top             =   720
         Width           =   1335
      End
      Begin VB.Label Label13 
         BackColor       =   &H00000000&
         Caption         =   "Can Report Position"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   120
         TabIndex        =   6
         Top             =   1440
         Width           =   2175
      End
      Begin VB.Label Label14 
         BackColor       =   &H00000000&
         Caption         =   "No"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2640
         TabIndex        =   5
         Top             =   1440
         Width           =   1335
      End
      Begin VB.Label Label15 
         BackColor       =   &H00000000&
         Caption         =   "No"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2640
         TabIndex        =   4
         Top             =   1800
         Width           =   1335
      End
      Begin VB.Label Label16 
         BackColor       =   &H00000000&
         Caption         =   "Can Report Step Size"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   120
         TabIndex        =   3
         Top             =   1800
         Width           =   2175
      End
      Begin VB.Label Label17 
         BackColor       =   &H00000000&
         Caption         =   "No"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2640
         TabIndex        =   2
         Top             =   1080
         Width           =   1335
      End
      Begin VB.Label Label18 
         BackColor       =   &H00000000&
         Caption         =   "Can Report Temp"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   120
         TabIndex        =   1
         Top             =   1080
         Width           =   2175
      End
   End
End
Attribute VB_Name = "FPropsDlg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Public fid As Integer


Private Sub CmdApply_Click()
    Dim i As Integer
    
    With EQSounds
        .F1Wav = LabelF1.ToolTipText
        .F2Wav = LabelF2.ToolTipText
        .FLOnWav = LabelFLOn.ToolTipText
        .FLOffWav = LabelFLOff.ToolTipText
        .FInWav = LabelIn.ToolTipText
        .FOutWav = LabelOut.ToolTipText
        For i = 11 To 14
            .FRateWav(i - 10) = LabelRate(i - 1).ToolTipText
        Next i
        .FocuserClick = ChkFSelect.Value
        .FocusLockClick = ChkFocusLock.Value
        .FocuserStepsClick = ChkFouserSteps.Value
        .FMoveClick = ChkFMove.Value
        
    End With
    ' write to ini
    Call writeBeep

End Sub

Private Sub Command1_Click()
    Select Case fid
        Case 1
            Call HC.ChooseFocuser2
            Label19.Caption = HC.oPersist.ReadIniValue("focuser2driver")
            Call ShowProperties
        Case Else
            Call HC.ChooseFocuser
            Label19.Caption = HC.oPersist.ReadIniValue("focuserdriver")
            Call ShowProperties
    End Select

End Sub

Private Sub Command2_Click()
    Call AscomFocuserSetup(fid)
    Call ShowProperties
End Sub

Private Sub Command3_Click()
    Select Case fid
        Case 1
            Call HC.oPersist.WriteIniValue("focuser2driver", "")
            Label19.Caption = ""
            Call ascomFocus2Disconnect
            Call HC.DisableFocuserControls
        Case Else
            Call HC.oPersist.WriteIniValue("focuserdriver", "")
            Label19.Caption = ""
            Call ascomFocusDisconnect
            Call HC.DisableFocuserControls
    End Select
End Sub

Private Sub CmdLoadF1_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelF1.Caption = FileDlg.filename2
        LabelF1.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadF2_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelF2.Caption = FileDlg.filename2
        LabelF2.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadFLOFf_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelFLOff.Caption = FileDlg.filename2
        LabelFLOff.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadFLOn_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelFLOn.Caption = FileDlg.filename2
        LabelFLOn.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadIn_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelIn.Caption = FileDlg.filename2
        LabelIn.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadOut_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelOut.Caption = FileDlg.filename2
        LabelOut.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadRate_Click(Index As Integer)
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelRate(Index).Caption = FileDlg.filename2
        LabelRate(Index).ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub Command9_Click()
    If List1.ListIndex > 0 Then
        FocuserProps.Speeds(List1.ListIndex - 1).deltaStep = VScroll1.Value
        FocuserProps.Speeds(List1.ListIndex - 1).type = Combo4.ListIndex
        SaveFSpeeds
        LoadFSpeeds
    End If
End Sub


Private Sub Label1_Click(Index As Integer)
    On Error Resume Next
    GetNumDlg.val = VScroll1.Value
    GetNumDlg.max = VScroll1.max
    GetNumDlg.min = VScroll1.min
    GetNumDlg.Show (1)
    VScroll1.Value = GetNumDlg.val
End Sub

Private Sub VScroll1_Change()
    Label1(1).Caption = CStr(VScroll1.Value)
End Sub

Private Sub VScroll1_Scroll()
    VScroll1_Change
End Sub

Private Sub List1_Click()
    On Error Resume Next
    If List1.ListIndex > 0 Then
        VScroll1.Value = CInt(FocuserProps.Speeds(List1.ListIndex - 1).deltaStep)
        Combo4.ListIndex = CInt(FocuserProps.Speeds(List1.ListIndex - 1).type)
    End If
End Sub

Private Sub Form_Load()
                
    ' Individual tab values
    Tabulator(1) = 10
    Tabulator(2) = 40
    Tabulator(3) = 120
    SendMessage List1.hwnd, LB_SETTABSTOPS, OBorder, Tabulator(1)
                
    Call ShowProperties
    Call RefreshControls
    
    If FocuserProps.MaxIncrement <= 32767 Then
        VScroll1.max = FocuserProps.MaxIncrement
    Else
        VScroll1.max = 32767
    End If
                

End Sub


Private Sub ShowProperties()

    Select Case fid
        Case 1
            Call ascomFocusGetProperties(1)
            Label19.Caption = HC.oPersist.ReadIniValue("focuser2driver")
        Case Else
            Call ascomFocusGetProperties(0)
            Label19.Caption = HC.oPersist.ReadIniValue("focuserdriver")
    End Select
            
    If FocuserProps.Absolute Then
        Label2.Caption = "Absolute"
    Else
        Label2.Caption = "Relative"
    End If
    
    Label4.Caption = CStr(FocuserProps.MaxIncrement)
    Label6.Caption = CStr(FocuserProps.MaxStep)
    Label8.Caption = FormatNumber(FocuserProps.StepSize, 4)
        
    If FocuserProps.CanHalt Then
        Label10.Caption = "Yes"
    Else
        Label10.Caption = "No"
    End If
    
    If FocuserProps.TempCompAvailable Then
        Label12.Caption = "Yes"
    Else
        Label12.Caption = "No"
    End If
    
    If FocuserProps.CanReportPosition Then
        Label14.Caption = "Yes"
    Else
        Label14.Caption = "No"
    End If

    If FocuserProps.CanReportStepSize Then
        Label15.Caption = "Yes"
    Else
        Label15.Caption = "No"
    End If

    If FocuserProps.CanReportTemp Then
        Label17.Caption = "Yes"
    Else
        Label17.Caption = "No"
    End If


End Sub

Private Sub RefreshControls()
    Dim i As Integer
    
    LabelF1.Caption = StripPath(EQSounds.F1Wav)
    LabelF1.ToolTipText = EQSounds.F1Wav
    LabelF2.Caption = StripPath(EQSounds.F2Wav)
    LabelF2.ToolTipText = EQSounds.F2Wav
    LabelFLOn.Caption = StripPath(EQSounds.FLOnWav)
    LabelFLOn.ToolTipText = EQSounds.FLOnWav
    LabelFLOff.Caption = StripPath(EQSounds.FLOffWav)
    LabelFLOff.ToolTipText = EQSounds.FLOffWav
    LabelIn.Caption = StripPath(EQSounds.FInWav)
    LabelIn.ToolTipText = EQSounds.FInWav
    LabelOut.Caption = StripPath(EQSounds.FOutWav)
    LabelOut.ToolTipText = EQSounds.FOutWav
    
    For i = 11 To 14
        LabelRate(i - 1).Caption = StripPath(EQSounds.FRateWav(i - 10))
        LabelRate(i - 1).ToolTipText = EQSounds.FRateWav(i - 10)
    Next i

    
    If EQSounds.FocuserClick Then
        ChkFSelect.Value = 1
    Else
        ChkFSelect.Value = 0
    End If
    If EQSounds.FocusLockClick Then
        ChkFocusLock.Value = 1
    Else
        ChkFocusLock.Value = 0
    End If
    If EQSounds.FocuserStepsClick Then
        ChkFouserSteps.Value = 1
    Else
        ChkFouserSteps.Value = 0
    End If
    If EQSounds.FMoveClick Then
        ChkFMove.Value = 1
    Else
        ChkFMove.Value = 0
    End If

    Call LoadFSpeeds

End Sub

