VERSION 5.00
Begin VB.Form ASCOMStatus 
   BackColor       =   &H00000000&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "ASCOM Driver Capabilities"
   ClientHeight    =   5910
   ClientLeft      =   45
   ClientTop       =   315
   ClientWidth     =   6495
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
   ScaleHeight     =   5910
   ScaleWidth      =   6495
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.Frame Frame2 
      BackColor       =   &H00000000&
      Caption         =   "Driver Properties"
      ForeColor       =   &H000080FF&
      Height          =   5655
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   6255
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   20
         Top             =   240
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   19
         Top             =   600
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   2
         Left            =   120
         TabIndex        =   18
         Top             =   960
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   3
         Left            =   120
         TabIndex        =   17
         Top             =   1320
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   4
         Left            =   120
         TabIndex        =   16
         Top             =   1680
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   5
         Left            =   120
         TabIndex        =   15
         Top             =   2040
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   6
         Left            =   120
         TabIndex        =   14
         Top             =   2400
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   7
         Left            =   120
         TabIndex        =   13
         Top             =   2760
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   8
         Left            =   120
         TabIndex        =   12
         Top             =   3120
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   9
         Left            =   3120
         TabIndex        =   11
         Top             =   240
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   10
         Left            =   3120
         TabIndex        =   10
         Top             =   600
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   11
         Left            =   3120
         TabIndex        =   9
         Top             =   960
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   12
         Left            =   3120
         TabIndex        =   8
         Top             =   1320
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   13
         Left            =   3120
         TabIndex        =   7
         Top             =   1680
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   14
         Left            =   3120
         TabIndex        =   6
         Top             =   2040
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   15
         Left            =   3120
         TabIndex        =   5
         Top             =   2400
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   16
         Left            =   3120
         TabIndex        =   4
         Top             =   2760
         Width           =   2895
      End
      Begin VB.CheckBox Check1 
         BackColor       =   &H00000000&
         Caption         =   "Check1"
         CausesValidation=   0   'False
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   17
         Left            =   3120
         TabIndex        =   3
         Top             =   3120
         Width           =   2895
      End
      Begin VB.Frame Frame1 
         BackColor       =   &H00000000&
         Caption         =   "Axis Rates"
         ForeColor       =   &H000080FF&
         Height          =   1935
         Left            =   120
         TabIndex        =   1
         Top             =   3480
         Width           =   2895
         Begin VB.TextBox Text1 
            BackColor       =   &H00000040&
            BorderStyle     =   0  'None
            ForeColor       =   &H000000FF&
            Height          =   1575
            Left            =   120
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   2
            Text            =   "ASCOMStatus.frx":0000
            Top             =   240
            Width           =   2655
         End
      End
   End
End
Attribute VB_Name = "ASCOMStatus"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub Check1_Click(Index As Integer)
    refreshdata
End Sub

Private Sub Form_Load()
    refreshdata
End Sub


Private Sub refreshdata()
Dim i As Integer
    ascomGetFunctions
    
    Check1(0).Caption = "CanFindHome"
    If ascomFunctions.CanFindHome Then
        Check1(0).Value = 1
    Else
        Check1(0).Value = 0
    End If
    
    If ascomFunctions.CanPark Then
        Check1(1).Value = 1
    Else
        Check1(1).Value = 0
    End If
    Check1(1).Caption = "CanPark"
    
    If ascomFunctions.CanPulseGuide Then
        Check1(2).Value = 1
    Else
        Check1(2).Value = 0
    End If
    Check1(2).Caption = "CanPulseGuide"
    
    If ascomFunctions.CanSetDeclinationRate Then
        Check1(3).Value = 1
    Else
        Check1(3).Value = 0
    End If
    Check1(3).Caption = "CanSetDeclinationRate"
    
    If ascomFunctions.CanSetGuideRates Then
        Check1(4).Value = 1
    Else
        Check1(4).Value = 0
    End If
    Check1(4).Caption = "CanSetGuideRates"
    
    If ascomFunctions.CanSetPark Then
        Check1(5).Value = 1
    Else
        Check1(5).Value = 0
    End If
    Check1(5).Caption = "CanSetPark"
    
    If ascomFunctions.CanSetPierSide Then
        Check1(6).Value = 1
    Else
        Check1(6).Value = 0
    End If
    Check1(6).Caption = "CanSetPierSide"
    
    If ascomFunctions.CanSetRightAscensionRate Then
        Check1(7).Value = 1
    Else
        Check1(7).Value = 0
    End If
    Check1(7).Caption = "CanSetRightAscensionRate"
    
    If ascomFunctions.CanSetTracking Then
        Check1(8).Value = 1
    Else
        Check1(8).Value = 0
    End If
    Check1(8).Caption = "CanSetTracking"
    
    If ascomFunctions.CanSlew Then
        Check1(9).Value = 1
    Else
        Check1(9).Value = 0
    End If
    Check1(9).Caption = "CanSlew"
    
    If ascomFunctions.CanSlewAltAz Then
        Check1(10).Value = 1
    Else
        Check1(10).Value = 0
    End If
    Check1(10).Caption = "CanSlewAltAz"
    
    If ascomFunctions.CanSlewAltAzAsync Then
        Check1(11).Value = 1
    Else
        Check1(11).Value = 0
    End If
    Check1(11).Caption = "CanSlewAltAzAsync"
    
    If ascomFunctions.CanSlewAsync Then
        Check1(12).Value = 1
    Else
        Check1(12).Value = 0
    End If
    Check1(12).Caption = "CanSlewAsync"
    
    If ascomFunctions.CanSync Then
        Check1(13).Value = 1
    Else
        Check1(13).Value = 0
    End If
    Check1(13).Caption = "CanSync"
    
    If ascomFunctions.CanSyncAltAz Then
        Check1(14).Value = 1
    Else
        Check1(14).Value = 0
    End If
    Check1(14).Caption = "CanSyncAltAz"
    
    If ascomFunctions.CanUnpark Then
        Check1(15).Value = 1
    Else
        Check1(15).Value = 0
    End If
    Check1(15).Caption = "CanUnpark"
    
    If ascomFunctions.CanMoveRAAxis Then
        Check1(16).Value = 1
    Else
        Check1(16).Value = 0
    End If
    Check1(16).Caption = "CanMoveRAAxis"
    
    If ascomFunctions.CanMoveDecAxis Then
        Check1(17).Value = 1
    Else
        Check1(17).Value = 0
    End If
    Check1(17).Caption = "CanMoveDECAxis"
    
    Text1.Text = "Total RA Rates " & ascomAxisRates.NumRaRates & vbCrLf
    For i = 0 To ascomAxisRates.NumRaRates - 1
        Text1.Text = Text1.Text & i + 1 & ": min=" & FormatNumber(ascomAxisRates.RaRates(i).min, 4) & ", max=" & FormatNumber(ascomAxisRates.RaRates(i).max, 4) & vbCrLf
    Next
    Text1.Text = Text1.Text & vbCrLf
    Text1.Text = Text1.Text & "Total DEC Rates " & ascomAxisRates.NumDecRates & vbCrLf
    For i = 0 To ascomAxisRates.NumDecRates - 1
        Text1.Text = Text1.Text & i + 1 & ": min=" & FormatNumber(ascomAxisRates.DecRates(i).min, 4) & ", max=" & FormatNumber(ascomAxisRates.DecRates(i).max, 4) & vbCrLf
    Next
    

End Sub
