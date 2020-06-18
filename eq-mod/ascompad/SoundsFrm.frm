VERSION 5.00
Begin VB.Form SoundsFrm 
   BackColor       =   &H00000000&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Set Sounds"
   ClientHeight    =   6570
   ClientLeft      =   2760
   ClientTop       =   3630
   ClientWidth     =   7185
   Icon            =   "SoundsFrm.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6570
   ScaleWidth      =   7185
   ShowInTaskbar   =   0   'False
   Begin VB.CheckBox ChkBeep 
      BackColor       =   &H00000000&
      Caption         =   "Beep"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   120
      TabIndex        =   58
      TabStop         =   0   'False
      Top             =   120
      Width           =   2775
   End
   Begin VB.CommandButton CmdLoadCustom 
      Height          =   250
      Left            =   480
      Picture         =   "SoundsFrm.frx":0CCA
      Style           =   1  'Graphical
      TabIndex        =   52
      Top             =   5310
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadLunar 
      Height          =   250
      Left            =   480
      Picture         =   "SoundsFrm.frx":124C
      Style           =   1  'Graphical
      TabIndex        =   50
      Top             =   4710
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadSolar 
      Height          =   250
      Left            =   480
      Picture         =   "SoundsFrm.frx":17CE
      Style           =   1  'Graphical
      TabIndex        =   48
      Top             =   4110
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadSidereal 
      Height          =   250
      Left            =   480
      Picture         =   "SoundsFrm.frx":1D50
      Style           =   1  'Graphical
      TabIndex        =   46
      Top             =   3510
      Width           =   350
   End
   Begin VB.CheckBox ChkTracking 
      BackColor       =   &H00000000&
      Caption         =   "Tracking"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   120
      TabIndex        =   45
      TabStop         =   0   'False
      Top             =   3000
      Width           =   3015
   End
   Begin VB.CommandButton CmdLoadUnpark 
      Height          =   250
      Left            =   480
      Picture         =   "SoundsFrm.frx":22D2
      Style           =   1  'Graphical
      TabIndex        =   43
      Top             =   1815
      Width           =   350
   End
   Begin VB.CheckBox ChkUnpark 
      BackColor       =   &H00000000&
      Caption         =   "Unpark"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   120
      TabIndex        =   42
      TabStop         =   0   'False
      Top             =   1560
      Width           =   2775
   End
   Begin VB.CommandButton CmdApply 
      Caption         =   "Apply Changes"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   3480
      TabIndex        =   41
      Top             =   6000
      Width           =   1695
   End
   Begin VB.CommandButton CmdLoadParked 
      Height          =   250
      Left            =   480
      Picture         =   "SoundsFrm.frx":2854
      Style           =   1  'Graphical
      TabIndex        =   39
      Top             =   1200
      Width           =   350
   End
   Begin VB.CheckBox ChkParked 
      BackColor       =   &H00000000&
      Caption         =   "Park"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   120
      TabIndex        =   38
      TabStop         =   0   'False
      Top             =   960
      Width           =   2775
   End
   Begin VB.CheckBox ChkStop 
      BackColor       =   &H00000000&
      Caption         =   "Stop"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   120
      TabIndex        =   36
      TabStop         =   0   'False
      Top             =   2160
      Width           =   2775
   End
   Begin VB.CommandButton CmdLoadStop 
      Height          =   250
      Left            =   480
      Picture         =   "SoundsFrm.frx":2DD6
      Style           =   1  'Graphical
      TabIndex        =   35
      Top             =   2400
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   9
      Left            =   3960
      Picture         =   "SoundsFrm.frx":3358
      Style           =   1  'Graphical
      TabIndex        =   23
      Top             =   3600
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   8
      Left            =   3960
      Picture         =   "SoundsFrm.frx":38DA
      Style           =   1  'Graphical
      TabIndex        =   21
      Top             =   3240
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   7
      Left            =   3960
      Picture         =   "SoundsFrm.frx":3E5C
      Style           =   1  'Graphical
      TabIndex        =   19
      Top             =   2880
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   6
      Left            =   3960
      Picture         =   "SoundsFrm.frx":43DE
      Style           =   1  'Graphical
      TabIndex        =   17
      Top             =   2520
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   5
      Left            =   3960
      Picture         =   "SoundsFrm.frx":4960
      Style           =   1  'Graphical
      TabIndex        =   15
      Top             =   2160
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   4
      Left            =   3960
      Picture         =   "SoundsFrm.frx":4EE2
      Style           =   1  'Graphical
      TabIndex        =   13
      Top             =   1800
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   3
      Left            =   3960
      Picture         =   "SoundsFrm.frx":5464
      Style           =   1  'Graphical
      TabIndex        =   11
      Top             =   1440
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   2
      Left            =   3960
      Picture         =   "SoundsFrm.frx":59E6
      Style           =   1  'Graphical
      TabIndex        =   9
      Top             =   1080
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   1
      Left            =   3960
      Picture         =   "SoundsFrm.frx":5F68
      Style           =   1  'Graphical
      TabIndex        =   7
      Top             =   720
      Width           =   350
   End
   Begin VB.CheckBox ChkRate 
      BackColor       =   &H00000000&
      Caption         =   "Rate Presets"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   3600
      TabIndex        =   5
      TabStop         =   0   'False
      Top             =   120
      Width           =   3135
   End
   Begin VB.CommandButton CmdLoadRate 
      Height          =   250
      Index           =   0
      Left            =   3960
      Picture         =   "SoundsFrm.frx":64EA
      Style           =   1  'Graphical
      TabIndex        =   4
      Top             =   360
      Width           =   350
   End
   Begin VB.CommandButton CmdLoadBeep 
      Height          =   250
      Left            =   480
      Picture         =   "SoundsFrm.frx":6A6C
      Style           =   1  'Graphical
      TabIndex        =   3
      Top             =   360
      Width           =   350
   End
   Begin VB.CommandButton CancelButton 
      Caption         =   "Cancel"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   6120
      Width           =   1695
   End
   Begin VB.CommandButton OKButton 
      Caption         =   "OK"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   5280
      TabIndex        =   0
      Top             =   6000
      Width           =   1695
   End
   Begin VB.Label Label5 
      BackColor       =   &H00000000&
      Caption         =   "Sidereal"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   480
      TabIndex        =   57
      Top             =   3240
      Width           =   2775
   End
   Begin VB.Label Label4 
      BackColor       =   &H00000000&
      Caption         =   "Custom"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   480
      TabIndex        =   56
      Top             =   5055
      Width           =   2775
   End
   Begin VB.Label Label2 
      BackColor       =   &H00000000&
      Caption         =   "Lunar"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   480
      TabIndex        =   55
      Top             =   4455
      Width           =   2775
   End
   Begin VB.Label Label1 
      BackColor       =   &H00000000&
      Caption         =   "Solar"
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   480
      TabIndex        =   54
      Top             =   3855
      Width           =   1095
   End
   Begin VB.Label LabelCustom 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   960
      TabIndex        =   53
      Top             =   5310
      Width           =   2295
   End
   Begin VB.Label LabelLunar 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   960
      TabIndex        =   51
      Top             =   4710
      Width           =   2295
   End
   Begin VB.Label LabelSolar 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   960
      TabIndex        =   49
      Top             =   4110
      Width           =   2295
   End
   Begin VB.Label LabelSidereal 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   960
      TabIndex        =   47
      Top             =   3480
      Width           =   2295
   End
   Begin VB.Label LabelUnpark 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   960
      TabIndex        =   44
      Top             =   1815
      Width           =   2295
   End
   Begin VB.Label LabelParked 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   240
      Left            =   960
      TabIndex        =   40
      Top             =   1215
      Width           =   2295
   End
   Begin VB.Label LabelStop 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   960
      TabIndex        =   37
      Top             =   2400
      Width           =   2295
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "10"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   9
      Left            =   3600
      TabIndex        =   34
      Top             =   3600
      Width           =   285
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "9"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   8
      Left            =   3600
      TabIndex        =   33
      Top             =   3240
      Width           =   285
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "8"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   7
      Left            =   3600
      TabIndex        =   32
      Top             =   2880
      Width           =   285
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "7"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   6
      Left            =   3600
      TabIndex        =   31
      Top             =   2520
      Width           =   285
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "6"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   5
      Left            =   3600
      TabIndex        =   30
      Top             =   2160
      Width           =   285
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "5"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   4
      Left            =   3600
      TabIndex        =   29
      Top             =   1800
      Width           =   285
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "4"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   3
      Left            =   3600
      TabIndex        =   28
      Top             =   1440
      Width           =   285
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "3"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   2
      Left            =   3600
      TabIndex        =   27
      Top             =   1080
      Width           =   285
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "2"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   1
      Left            =   3600
      TabIndex        =   26
      Top             =   720
      Width           =   285
   End
   Begin VB.Label Label3 
      BackColor       =   &H00000000&
      Caption         =   "1"
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   0
      Left            =   3600
      TabIndex        =   25
      Top             =   360
      Width           =   285
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   9
      Left            =   4440
      TabIndex        =   24
      Top             =   3600
      Width           =   2295
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   8
      Left            =   4440
      TabIndex        =   22
      Top             =   3240
      Width           =   2295
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   7
      Left            =   4440
      TabIndex        =   20
      Top             =   2880
      Width           =   2295
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   6
      Left            =   4440
      TabIndex        =   18
      Top             =   2520
      Width           =   2295
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   5
      Left            =   4440
      TabIndex        =   16
      Top             =   2160
      Width           =   2295
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   4
      Left            =   4440
      TabIndex        =   14
      Top             =   1800
      Width           =   2295
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   3
      Left            =   4440
      TabIndex        =   12
      Top             =   1440
      Width           =   2295
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   2
      Left            =   4440
      TabIndex        =   10
      Top             =   1080
      Width           =   2295
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   1
      Left            =   4440
      TabIndex        =   8
      Top             =   720
      Width           =   2295
   End
   Begin VB.Label LabelRate 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Index           =   0
      Left            =   4440
      TabIndex        =   6
      Top             =   360
      Width           =   2295
   End
   Begin VB.Label LabelBeep 
      BackColor       =   &H00000040&
      ForeColor       =   &H000080FF&
      Height          =   255
      Left            =   960
      TabIndex        =   2
      Top             =   360
      Width           =   2295
   End
End
Attribute VB_Name = "SoundsFrm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit

Private Sub CancelButton_Click()
        Unload SoundsFrm
End Sub

Private Sub CmdApply_Click()
    Dim i As Integer
    
    With EQSounds
        .BeepWav = LabelBeep.ToolTipText
        .ParkedWav = LabelParked.ToolTipText
        .StopWav = LabelStop.ToolTipText
        .Unparkwav = LabelUnpark.ToolTipText
        .SiderealWav = LabelSidereal.ToolTipText
        .SolarWav = LabelSolar.ToolTipText
        .LunarWav = LabelLunar.ToolTipText
        .CustomWav = LabelCustom.ToolTipText
        For i = 1 To 10
            .RateWav(i) = LabelRate(i - 1).ToolTipText
        Next i
        .PositionBeep = ChkBeep.Value
        .RateClick = ChkRate.Value
        .ParkedClick = ChkParked.Value
        .Stopclick = ChkStop.Value
        .Unparkclick = ChkUnpark.Value
        .TrackClick = ChkTracking.Value
        
    End With
    ' write to ini
    Call writeBeep

End Sub

Private Sub CmdLoadBeep_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelBeep.Caption = FileDlg.filename2
        LabelBeep.ToolTipText = FileDlg.filename
    End If
End Sub

Private Sub CmdLoadLunar_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelLunar.Caption = FileDlg.filename2
        LabelLunar.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadSidereal_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelSidereal.Caption = FileDlg.filename2
        LabelSidereal.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadSolar_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelSolar.Caption = FileDlg.filename2
        LabelSolar.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadParked_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelParked.Caption = FileDlg.filename2
        LabelParked.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadStop_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelStop.Caption = FileDlg.filename2
        LabelStop.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub CmdLoadUnpark_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelUnpark.Caption = FileDlg.filename2
        LabelUnpark.ToolTipText = FileDlg.filename
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

Private Sub Form_Load()
    Call RefreshControls
End Sub

Private Sub RefreshControls()
    Dim i As Integer
    
    LabelBeep.ToolTipText = EQSounds.BeepWav
    LabelBeep.Caption = StripPath(EQSounds.BeepWav)
    LabelParked.Caption = StripPath(EQSounds.ParkedWav)
    LabelParked.ToolTipText = EQSounds.ParkedWav
    LabelStop.Caption = StripPath(EQSounds.StopWav)
    LabelStop.ToolTipText = EQSounds.StopWav
    LabelUnpark.Caption = StripPath(EQSounds.Unparkwav)
    LabelUnpark.ToolTipText = EQSounds.Unparkwav
    LabelSidereal.Caption = StripPath(EQSounds.SiderealWav)
    LabelSidereal.ToolTipText = EQSounds.SiderealWav
    LabelSolar.Caption = StripPath(EQSounds.SolarWav)
    LabelSolar.ToolTipText = EQSounds.SolarWav
    LabelLunar.Caption = StripPath(EQSounds.LunarWav)
    LabelLunar.ToolTipText = EQSounds.LunarWav
    LabelCustom.Caption = StripPath(EQSounds.CustomWav)
    LabelCustom.ToolTipText = EQSounds.CustomWav
    For i = 1 To 10
        LabelRate(i - 1).Caption = StripPath(EQSounds.RateWav(i))
        LabelRate(i - 1).ToolTipText = EQSounds.RateWav(i)
    Next i
    
    If EQSounds.PositionBeep Then
        ChkBeep.Value = 1
    Else
        ChkBeep.Value = 0
    End If
    If EQSounds.ParkedClick Then
        ChkParked.Value = 1
    Else
        ChkParked.Value = 0
    End If
    If EQSounds.Stopclick Then
        ChkStop.Value = 1
    Else
        ChkStop.Value = 0
    End If
    If EQSounds.RateClick Then
        ChkRate.Value = 1
    Else
        ChkRate.Value = 0
    End If
    If EQSounds.Unparkclick Then
        ChkUnpark.Value = 1
    Else
        ChkUnpark.Value = 0
    End If
    If EQSounds.TrackClick Then
        ChkTracking.Value = 1
    Else
        ChkTracking.Value = 0
    End If
End Sub


Private Sub OKButton_Click()
    CmdApply_Click
    Unload SoundsFrm
End Sub

Private Function StripPath(str As String) As String
Dim i As Integer
    i = InStrRev(str, "\")
    StripPath = Right$(str, Len(str) - i)
End Function
