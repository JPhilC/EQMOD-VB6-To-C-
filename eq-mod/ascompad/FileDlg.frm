VERSION 5.00
Begin VB.Form FileDlg 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Load / Save PE File"
   ClientHeight    =   3840
   ClientLeft      =   2760
   ClientTop       =   3630
   ClientWidth     =   6030
   Icon            =   "FileDlg.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3840
   ScaleWidth      =   6030
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox Text1 
      Height          =   375
      Left            =   120
      TabIndex        =   5
      Text            =   "Text1"
      Top             =   2880
      Width           =   5775
   End
   Begin VB.FileListBox File1 
      Height          =   2625
      Left            =   3240
      Pattern         =   "*.txt*"
      TabIndex        =   4
      Top             =   120
      Width           =   2655
   End
   Begin VB.DirListBox Dir1 
      Height          =   2115
      Left            =   120
      TabIndex        =   3
      Top             =   600
      Width           =   2895
   End
   Begin VB.DriveListBox Drive1 
      Height          =   315
      Left            =   120
      TabIndex        =   2
      Top             =   120
      Width           =   2895
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
      Top             =   3360
      Width           =   1215
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
      Left            =   4680
      TabIndex        =   0
      Top             =   3360
      Width           =   1215
   End
End
Attribute VB_Name = "FileDlg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Public filename As String
Public filename2 As String
Public lastdrive As String
Public lastdir As String
Public notfirst As Boolean
Public filter As String

Option Explicit

Private Sub CancelButton_Click()
    filename = ""
    filename2 = ""
    Unload FileDlg
End Sub

Private Sub Dir1_Change()
    File1.Path = Dir1.Path
    Text1.Text = Dir1.Path & "\"
End Sub

Private Sub Dir1_Click()
    Dir1.Path = Dir1.List(Dir1.ListIndex)
End Sub

Private Sub Drive1_Change()
    On Error Resume Next
    Dir1.Path = Drive1.Drive
    Text1.Text = Dir1.Path
End Sub

Private Sub File1_Click()
    Text1.Text = Dir1.Path & "\" & File1.filename
    filename = Text1.Text
End Sub

Private Sub File1_DblClick()
    Text1.Text = Dir1.Path & "\" & File1.filename
    OKButton_Click
End Sub

Private Sub Form_Activate()
    filename = ""
    If filter = "" Then filter = "*.*"
    File1.Pattern = filter
    Call PutWindowOnTop(FileDlg)
    Text1.SetFocus
    Text1.Text = Dir1.Path & "\"
End Sub

Private Sub Form_Load()
    On Error GoTo errhandle:
    If filter = "" Then filter = "*.*"
    FileDlg.Caption = "Load (" & filter & ")"

    If Not notfirst Then
        Dir1.Path = App.Path
        notfirst = True
    Else
        Dir1.Path = lastdir
        Drive1.Drive = lastdrive
    End If
    Exit Sub
errhandle:
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
    lastdrive = Drive1.Drive
    lastdir = Dir1.Path
End Sub

Private Sub OKButton_Click()
    filename = Text1.Text
    filename2 = File1.filename
    Unload FileDlg
End Sub
Private Sub Text1_KeyDown(KeyCode As Integer, Shift As Integer)
    If KeyCode = 13 Then
        OKButton_Click
    End If
End Sub

