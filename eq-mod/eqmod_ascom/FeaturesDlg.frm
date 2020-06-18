VERSION 5.00
Begin VB.Form FeaturesDlg 
   BackColor       =   &H00000000&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Feature Set"
   ClientHeight    =   3960
   ClientLeft      =   45
   ClientTop       =   315
   ClientWidth     =   2970
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3960
   ScaleWidth      =   2970
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.ListBox List1 
      BackColor       =   &H00000040&
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
      Height          =   4050
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   3015
   End
End
Attribute VB_Name = "FeaturesDlg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Load()

    Call PutWindowOnTop(FeaturesDlg)
    
    List1.Clear
    
    If gMount_Features And &H1 Then
        List1.AddItem "Snap Port 1"
    End If
    If gMount_Features And &H2 Then
        List1.AddItem "Snap Port 2"
    End If
    If gMount_Features And &H4 Then
        List1.AddItem "RA PPEC"
    End If
    If gMount_Features And &H8 Then
        List1.AddItem "DEC PPEC"
    End If
    If gMount_Features And &H10 Then
        List1.AddItem "RA Hardware Encoder"
    End If
    If gMount_Features And &H20 Then
        List1.AddItem "DEC Hardware Encoder"
    End If
    If gMount_Features And &H40 Then
        List1.AddItem "RA Half Current"
    End If
    If gMount_Features And &H80 Then
        List1.AddItem "DEC Half Current"
    End If
    If gMount_Features And &H10000 Then
        List1.AddItem "Polar Scope Brightness Adjust"
    End If
    If gMount_Features And &H20000 Then
        List1.AddItem "Home Position Sensor"
    End If

End Sub
