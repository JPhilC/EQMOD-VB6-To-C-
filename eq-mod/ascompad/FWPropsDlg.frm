VERSION 5.00
Begin VB.Form FWPropsDlg 
   BackColor       =   &H00000000&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "FilterWheel Setup"
   ClientHeight    =   5880
   ClientLeft      =   45
   ClientTop       =   315
   ClientWidth     =   4560
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5880
   ScaleWidth      =   4560
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.Frame Frame1 
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
      Height          =   4575
      Left            =   120
      TabIndex        =   5
      Top             =   1200
      Width           =   4335
      Begin VB.CommandButton CmdApply 
         BackColor       =   &H0095C1CB&
         Caption         =   "Apply Changes"
         Height          =   255
         Left            =   120
         Style           =   1  'Graphical
         TabIndex        =   37
         Top             =   4200
         Width           =   2895
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   9
         Left            =   480
         Picture         =   "FWPropsDlg.frx":0000
         Style           =   1  'Graphical
         TabIndex        =   34
         Top             =   3720
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   8
         Left            =   480
         Picture         =   "FWPropsDlg.frx":0582
         Style           =   1  'Graphical
         TabIndex        =   31
         Top             =   3360
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   7
         Left            =   480
         Picture         =   "FWPropsDlg.frx":0B04
         Style           =   1  'Graphical
         TabIndex        =   28
         Top             =   3000
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   6
         Left            =   480
         Picture         =   "FWPropsDlg.frx":1086
         Style           =   1  'Graphical
         TabIndex        =   25
         Top             =   2640
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   5
         Left            =   480
         Picture         =   "FWPropsDlg.frx":1608
         Style           =   1  'Graphical
         TabIndex        =   22
         Top             =   2280
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   4
         Left            =   480
         Picture         =   "FWPropsDlg.frx":1B8A
         Style           =   1  'Graphical
         TabIndex        =   19
         Top             =   1920
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   3
         Left            =   480
         Picture         =   "FWPropsDlg.frx":210C
         Style           =   1  'Graphical
         TabIndex        =   10
         Top             =   1560
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   2
         Left            =   480
         Picture         =   "FWPropsDlg.frx":268E
         Style           =   1  'Graphical
         TabIndex        =   9
         Top             =   1200
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   1
         Left            =   480
         Picture         =   "FWPropsDlg.frx":2C10
         Style           =   1  'Graphical
         TabIndex        =   8
         Top             =   840
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadFilter 
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
         Index           =   0
         Left            =   480
         Picture         =   "FWPropsDlg.frx":3192
         Style           =   1  'Graphical
         TabIndex        =   7
         Top             =   480
         Width           =   350
      End
      Begin VB.CheckBox ChkFWFilters 
         BackColor       =   &H00000000&
         Caption         =   "Filters"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   120
         TabIndex        =   6
         TabStop         =   0   'False
         Top             =   240
         Width           =   2895
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "10"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   5
         Left            =   120
         TabIndex        =   36
         Top             =   3720
         Width           =   285
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   9
         Left            =   960
         TabIndex        =   35
         Top             =   3720
         Width           =   2055
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "9"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   4
         Left            =   120
         TabIndex        =   33
         Top             =   3360
         Width           =   285
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   8
         Left            =   960
         TabIndex        =   32
         Top             =   3360
         Width           =   2055
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "8"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   3
         Left            =   120
         TabIndex        =   30
         Top             =   3000
         Width           =   285
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   7
         Left            =   960
         TabIndex        =   29
         Top             =   3000
         Width           =   2055
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "7"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   2
         Left            =   120
         TabIndex        =   27
         Top             =   2640
         Width           =   285
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   6
         Left            =   960
         TabIndex        =   26
         Top             =   2640
         Width           =   2055
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "6"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   24
         Top             =   2280
         Width           =   285
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   5
         Left            =   960
         TabIndex        =   23
         Top             =   2280
         Width           =   2055
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "5"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   21
         Top             =   1920
         Width           =   285
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   4
         Left            =   960
         TabIndex        =   20
         Top             =   1920
         Width           =   2055
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "4"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   13
         Left            =   120
         TabIndex        =   18
         Top             =   1560
         Width           =   285
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   3
         Left            =   960
         TabIndex        =   17
         Top             =   1560
         Width           =   2055
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "3"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   12
         Left            =   120
         TabIndex        =   16
         Top             =   1200
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "2"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   11
         Left            =   120
         TabIndex        =   15
         Top             =   840
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "1"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   10
         Left            =   120
         TabIndex        =   14
         Top             =   480
         Width           =   285
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   2
         Left            =   960
         TabIndex        =   13
         Top             =   1200
         Width           =   2055
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   1
         Left            =   960
         TabIndex        =   12
         Top             =   840
         Width           =   2055
      End
      Begin VB.Label LabelFilter 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   0
         Left            =   960
         TabIndex        =   11
         Top             =   480
         Width           =   2055
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
      TabIndex        =   0
      Top             =   120
      Width           =   4335
      Begin VB.CommandButton Command3 
         BackColor       =   &H0095C1CB&
         Caption         =   "Remove"
         Height          =   255
         Left            =   1440
         Style           =   1  'Graphical
         TabIndex        =   2
         Top             =   600
         Width           =   1095
      End
      Begin VB.CommandButton Command1 
         BackColor       =   &H0095C1CB&
         Caption         =   "Select"
         Height          =   255
         Left            =   120
         Style           =   1  'Graphical
         TabIndex        =   1
         Top             =   600
         Width           =   1095
      End
      Begin VB.Label Label19 
         BackColor       =   &H00000000&
         Caption         =   "Label19"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   1200
         TabIndex        =   4
         Top             =   240
         Width           =   3015
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000000&
         Caption         =   "Driver ID"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   10
         Left            =   120
         TabIndex        =   3
         Top             =   240
         Width           =   975
      End
   End
End
Attribute VB_Name = "FWPropsDlg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub CmdApply_Click()
    Dim i As Integer
    
    With EQSounds
        For i = 0 To 9
            .FiltersWav(i) = LabelFilter(i).ToolTipText
        Next i
    
        If ChkFWFilters.Value = 1 Then
            .FiltersClick = True
        Else
            .FiltersClick = False
        End If
    End With
    
    ' write to ini
    Call writeBeep

End Sub

Private Sub CmdLoadFilter_Click(Index As Integer)
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelFilter(Index).Caption = FileDlg.filename2
        LabelFilter(Index).ToolTipText = FileDlg.filename
    End If
End Sub

Private Sub Command1_Click()
            
    Call HC.ChooseFilterWheel
    Label19.Caption = HC.oPersist.ReadIniValue("filterwheeldriver")
    Call ShowProperties

End Sub

Private Sub Command3_Click()
    Call HC.oPersist.WriteIniValue("filterwheeldriver", "")
    Label19.Caption = ""
    Call ascomFilterWheelDisconnect
    Call HC.DisableFilterWheelControls
End Sub

Private Sub ShowProperties()
    Dim i As Integer
    
    Call ascomFilterwheelGetProperties
    Label19.Caption = HC.oPersist.ReadIniValue("filterwheeldriver")
    
    For i = 0 To 9
        LabelFilter(i).Caption = StripPath(EQSounds.FiltersWav(i))
        LabelFilter(i).ToolTipText = EQSounds.FiltersWav(i)
    Next i
    
    If EQSounds.FiltersClick Then
        ChkFWFilters.Value = 1
    Else
        ChkFWFilters.Value = 0
    End If

End Sub

Private Sub Form_Load()
    Call ShowProperties
End Sub
