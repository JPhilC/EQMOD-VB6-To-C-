VERSION 5.00
Begin VB.Form GetNumDlg 
   BackColor       =   &H00000000&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Enter Number"
   ClientHeight    =   420
   ClientLeft      =   2760
   ClientTop       =   3630
   ClientWidth     =   2025
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   420
   ScaleWidth      =   2025
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton OKButton 
      BackColor       =   &H0095C1CB&
      Caption         =   "OK"
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
      Left            =   1560
      TabIndex        =   1
      Top             =   75
      Width           =   375
   End
   Begin VB.TextBox Text1 
      BackColor       =   &H00000000&
      ForeColor       =   &H000000FF&
      Height          =   285
      Left            =   120
      TabIndex        =   0
      Top             =   60
      Width           =   1335
   End
End
Attribute VB_Name = "GetNumDlg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit


Public val As Double
Public min As Double
Public max As Double

Private Sub Form_Activate()
    Text1.SetFocus
    Text1.SelStart = 0
    Text1.SelLength = Len(Text1.Text)
End Sub

Private Sub Form_Load()
    Text1.Text = CStr(val)
End Sub

Private Sub OKButton_Click()
Dim num As Double
    num = CDbl(Text1.Text)
    If num >= min And num <= max Then
        val = num
    End If

    Unload Me
End Sub

Private Sub Text1_KeyDown(KeyCode As Integer, Shift As Integer)
    If KeyCode = 13 Then
        OKButton_Click
    End If
End Sub

