VERSION 5.00
Begin VB.Form BookmarkForm 
   BackColor       =   &H00000000&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Bookmarks"
   ClientHeight    =   2445
   ClientLeft      =   45
   ClientTop       =   315
   ClientWidth     =   5055
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
   ScaleHeight     =   2445
   ScaleWidth      =   5055
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.TextBox Text2 
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
      Height          =   375
      Left            =   3240
      TabIndex        =   5
      Text            =   "Text1"
      Top             =   1080
      Width           =   1575
   End
   Begin VB.TextBox Text1 
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
      Height          =   375
      Left            =   3240
      TabIndex        =   4
      Text            =   "Text1"
      Top             =   360
      Width           =   1095
   End
   Begin VB.CommandButton Command3 
      BackColor       =   &H0095C1CB&
      Caption         =   "Get"
      Height          =   375
      Left            =   4440
      Style           =   1  'Graphical
      TabIndex        =   3
      Top             =   360
      Width           =   375
   End
   Begin VB.CommandButton Command2 
      BackColor       =   &H0095C1CB&
      Height          =   375
      Left            =   4440
      Picture         =   "BookmarkForm.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   2
      ToolTipText     =   "Clear"
      Top             =   1920
      Width           =   375
   End
   Begin VB.CommandButton Command1 
      BackColor       =   &H0095C1CB&
      Height          =   375
      Left            =   3240
      Picture         =   "BookmarkForm.frx":0582
      Style           =   1  'Graphical
      TabIndex        =   1
      ToolTipText     =   "Add"
      Top             =   1920
      Width           =   375
   End
   Begin VB.ListBox List1 
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
      ForeColor       =   &H000080FF&
      Height          =   2160
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   3015
   End
   Begin VB.Label Label2 
      BackColor       =   &H00000000&
      Caption         =   "Name"
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
      Left            =   3240
      TabIndex        =   7
      Top             =   840
      Width           =   1335
   End
   Begin VB.Label Label1 
      BackColor       =   &H00000000&
      Caption         =   "Position"
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
      Left            =   3240
      TabIndex        =   6
      Top             =   120
      Width           =   1335
   End
End
Attribute VB_Name = "BookmarkForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Public Destination As Long

Private Sub Command1_Click()
    On Error GoTo exitsub

    If List1.ListIndex <> -1 Then
        FocuserProps.Bookmarks(List1.ListIndex).Position = val(Text1)
        FocuserProps.Bookmarks(List1.ListIndex).Name = Text2
        FocuserProps.Bookmarks(List1.ListIndex).enabled = True
        Call FillList
        Call SaveFBookmarks
    End If
exitsub:
End Sub

Private Sub Command2_Click()
    Dim i As Integer
    
    List1.Clear
    For i = 0 To 9
        FocuserProps.Bookmarks(i).enabled = False
    Next i
End Sub

Private Sub Command3_Click()
    Text1 = CStr(FocuserProps.Position)
End Sub

Private Sub Form_Load()
    PutWindowOnTop BookmarkForm
    Text1 = ""
    Text2 = ""
    Destination = FocuserProps.Position
    Call FillList
End Sub

Private Sub List1_Click()
    If FocuserProps.Bookmarks(List1.ListIndex).enabled Then
        Text1 = CStr(FocuserProps.Bookmarks(List1.ListIndex).Position)
        Text2 = FocuserProps.Bookmarks(List1.ListIndex).Name
    Else
        Text1 = ""
        Text2 = ""
    End If
End Sub

Private Sub FillList()
    Dim i As Integer
    Dim strlist As String
    
    List1.Clear
    For i = 0 To 9
        strlist = CStr(i + 1) & ": "
        If FocuserProps.Bookmarks(i).enabled Then
            strlist = strlist & CStr(FocuserProps.Bookmarks(i).Position) & " " & FocuserProps.Bookmarks(i).Name
        End If
        List1.AddItem (strlist)
    Next i

End Sub

Private Sub List1_DblClick()
    If FocuserProps.Bookmarks(List1.ListIndex).enabled Then
        Destination = FocuserProps.Bookmarks(List1.ListIndex).Position
    End If
    Me.Hide
End Sub
