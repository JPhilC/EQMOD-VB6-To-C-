VERSION 5.00
Begin VB.Form JStickConfigForm 
   BackColor       =   &H00000000&
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Gampad Config"
   ClientHeight    =   6795
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   11565
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "JStickConfig.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6795
   ScaleWidth      =   11565
   StartUpPosition =   1  'CenterOwner
   Begin VB.Timer Timer1 
      Enabled         =   0   'False
      Interval        =   100
      Left            =   6840
      Top             =   2640
   End
   Begin VB.Timer Timer2 
      Enabled         =   0   'False
      Interval        =   100
      Left            =   10800
      Top             =   3480
   End
   Begin VB.CommandButton ApplyBtn 
      BackColor       =   &H0095C1CB&
      Caption         =   "Apply Changes"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   6840
      Style           =   1  'Graphical
      TabIndex        =   78
      Top             =   6000
      Width           =   1575
   End
   Begin VB.CommandButton CancelBtn 
      BackColor       =   &H0095C1CB&
      Caption         =   "Cancel"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   5040
      Style           =   1  'Graphical
      TabIndex        =   77
      Top             =   6000
      Width           =   1575
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00000000&
      Caption         =   "Calibration"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000040C0&
      Height          =   6495
      Left            =   8760
      TabIndex        =   60
      Top             =   120
      Width           =   2655
      Begin VB.CommandButton StartBtn 
         BackColor       =   &H0095C1CB&
         Caption         =   "Start"
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
         Left            =   240
         Style           =   1  'Graphical
         TabIndex        =   61
         Top             =   5880
         Width           =   2295
      End
      Begin VB.Label XminLabel 
         BackColor       =   &H00000080&
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
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   960
         TabIndex        =   76
         Top             =   720
         Width           =   615
      End
      Begin VB.Label YminLabel 
         BackColor       =   &H00000080&
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
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   960
         TabIndex        =   75
         Top             =   1200
         Width           =   615
      End
      Begin VB.Label ZminLabel 
         BackColor       =   &H00000080&
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
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   960
         TabIndex        =   74
         Top             =   1680
         Width           =   615
      End
      Begin VB.Label RminLabel 
         BackColor       =   &H00000080&
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
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   960
         TabIndex        =   73
         Top             =   2160
         Width           =   615
      End
      Begin VB.Label XmaxLabel 
         BackColor       =   &H00000080&
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
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   1800
         TabIndex        =   72
         Top             =   720
         Width           =   615
      End
      Begin VB.Label YmaxLabel 
         BackColor       =   &H00000080&
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
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   1800
         TabIndex        =   71
         Top             =   1200
         Width           =   615
      End
      Begin VB.Label ZmaxLabel 
         BackColor       =   &H00000080&
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
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   1800
         TabIndex        =   70
         Top             =   1680
         Width           =   615
      End
      Begin VB.Label RmaxLabel 
         BackColor       =   &H00000080&
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
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   1800
         TabIndex        =   69
         Top             =   2160
         Width           =   615
      End
      Begin VB.Label Label8 
         BackColor       =   &H00000000&
         Caption         =   "Min"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   255
         Left            =   960
         TabIndex        =   68
         Top             =   360
         Width           =   615
      End
      Begin VB.Label Label9 
         BackColor       =   &H00000000&
         Caption         =   "Max"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   255
         Left            =   1800
         TabIndex        =   67
         Top             =   360
         Width           =   615
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "X Axis"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   255
         Left            =   120
         TabIndex        =   66
         Top             =   720
         Width           =   855
      End
      Begin VB.Label Label4 
         BackColor       =   &H00000000&
         Caption         =   "Y Axis"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   255
         Left            =   120
         TabIndex        =   65
         Top             =   1200
         Width           =   855
      End
      Begin VB.Label Label5 
         BackColor       =   &H00000000&
         Caption         =   "Z Axis"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   255
         Left            =   120
         TabIndex        =   64
         Top             =   1680
         Width           =   855
      End
      Begin VB.Label Label6 
         BackColor       =   &H00000000&
         Caption         =   "R Axis"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   255
         Left            =   120
         TabIndex        =   63
         Top             =   2160
         Width           =   855
      End
      Begin VB.Label Label7 
         BackColor       =   &H00000000&
         Caption         =   "Move the joystick paddles to their extreme limits until the numbers above cease changing."
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   975
         Left            =   240
         TabIndex        =   62
         Top             =   2640
         Width           =   2295
      End
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00000000&
      Caption         =   "Buttons"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000040C0&
      Height          =   6495
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   8535
      Begin VB.CheckBox CheckPOV 
         BackColor       =   &H00000000&
         Caption         =   "Enable POV"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   6720
         TabIndex        =   95
         Top             =   2040
         Width           =   1695
      End
      Begin VB.ComboBox Combo2 
         BackColor       =   &H00000080&
         ForeColor       =   &H000000FF&
         Height          =   330
         ItemData        =   "JStickConfig.frx":0CCA
         Left            =   6720
         List            =   "JStickConfig.frx":0CD4
         Style           =   2  'Dropdown List
         TabIndex        =   83
         Top             =   1440
         Width           =   1695
      End
      Begin VB.ComboBox Combo1 
         BackColor       =   &H00000080&
         ForeColor       =   &H000000FF&
         Height          =   330
         ItemData        =   "JStickConfig.frx":0CEF
         Left            =   6720
         List            =   "JStickConfig.frx":0CF9
         Style           =   2  'Dropdown List
         TabIndex        =   81
         Top             =   600
         Width           =   1695
      End
      Begin VB.CommandButton ClearBtn 
         BackColor       =   &H0095C1CB&
         Caption         =   "Clear All"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   240
         Style           =   1  'Graphical
         TabIndex        =   1
         Top             =   5880
         Width           =   1575
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   38
         Left            =   5280
         TabIndex        =   102
         Top             =   2040
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   35
         Left            =   5280
         TabIndex        =   96
         Top             =   2280
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   34
         Left            =   5280
         TabIndex        =   93
         Top             =   1800
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   33
         Left            =   5280
         TabIndex        =   91
         Top             =   1560
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   31
         Left            =   5280
         TabIndex        =   87
         Top             =   1080
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   30
         Left            =   5280
         TabIndex        =   85
         Top             =   840
         Width           =   1215
      End
      Begin VB.Label Label11 
         BackColor       =   &H00000000&
         Caption         =   "Right Joystick"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   6720
         TabIndex        =   84
         Top             =   1200
         Width           =   1695
      End
      Begin VB.Label Label10 
         BackColor       =   &H00000000&
         Caption         =   "Left Joystick"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   6720
         TabIndex        =   82
         Top             =   360
         Width           =   1695
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   0
         Left            =   2040
         TabIndex        =   42
         Top             =   360
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   1
         Left            =   5280
         TabIndex        =   41
         Top             =   3720
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   2
         Left            =   2040
         TabIndex        =   40
         Top             =   840
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   3
         Left            =   2040
         TabIndex        =   39
         Top             =   1080
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   4
         Left            =   2040
         TabIndex        =   38
         Top             =   1440
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   5
         Left            =   2040
         TabIndex        =   37
         Top             =   3600
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   6
         Left            =   2040
         TabIndex        =   36
         Top             =   3840
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   7
         Left            =   5280
         TabIndex        =   35
         Top             =   3240
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   8
         Left            =   5280
         TabIndex        =   34
         Top             =   4440
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   9
         Left            =   5280
         TabIndex        =   33
         Top             =   4680
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   10
         Left            =   5280
         TabIndex        =   32
         Top             =   4920
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   11
         Left            =   5280
         TabIndex        =   31
         Top             =   5400
         Visible         =   0   'False
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   12
         Left            =   5280
         TabIndex        =   30
         Top             =   360
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   13
         Left            =   2040
         TabIndex        =   29
         Top             =   2520
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   14
         Left            =   2040
         TabIndex        =   28
         Top             =   2760
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   15
         Left            =   2040
         TabIndex        =   27
         Top             =   3000
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   16
         Left            =   2040
         TabIndex        =   26
         Top             =   3240
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   17
         Left            =   5280
         TabIndex        =   24
         Top             =   600
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   18
         Left            =   2040
         TabIndex        =   22
         Top             =   2160
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   19
         Left            =   5280
         TabIndex        =   20
         Top             =   3480
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   20
         Left            =   2040
         TabIndex        =   17
         Top             =   1680
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   21
         Left            =   2040
         TabIndex        =   16
         Top             =   1920
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   22
         Left            =   2040
         TabIndex        =   13
         Top             =   4200
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   23
         Left            =   2040
         TabIndex        =   12
         Top             =   4440
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   24
         Left            =   5280
         TabIndex        =   10
         Top             =   4200
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   25
         Left            =   2040
         TabIndex        =   5
         Top             =   4680
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   26
         Left            =   2040
         TabIndex        =   4
         Top             =   4920
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   27
         Left            =   2040
         TabIndex        =   3
         Top             =   5160
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   28
         Left            =   2040
         TabIndex        =   2
         Top             =   5400
         Width           =   1215
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         BorderStyle     =   1  'Fixed Single
         Caption         =   "Emergency Stop"
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
         Height          =   255
         Index           =   0
         Left            =   240
         TabIndex        =   59
         Top             =   360
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Filter1"
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
         Height          =   255
         Index           =   1
         Left            =   3480
         TabIndex        =   58
         Top             =   3720
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Park to User Defined"
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
         Height          =   255
         Index           =   2
         Left            =   240
         TabIndex        =   57
         Top             =   840
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Decrement Filter"
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
         Height          =   255
         Index           =   19
         Left            =   3480
         TabIndex        =   21
         Top             =   3480
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Unpark"
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
         Height          =   255
         Index           =   3
         Left            =   240
         TabIndex        =   56
         Top             =   1080
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Sidereal Tracking"
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
         Height          =   255
         Index           =   4
         Left            =   240
         TabIndex        =   55
         Top             =   1440
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Lunar Tracking"
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
         Height          =   255
         Index           =   20
         Left            =   240
         TabIndex        =   19
         Top             =   1680
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Solar Tracking"
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
         Height          =   255
         Index           =   21
         Left            =   240
         TabIndex        =   18
         Top             =   1920
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Custom Tracking"
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
         Height          =   255
         Index           =   18
         Left            =   240
         TabIndex        =   23
         Top             =   2160
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Increment Filter"
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
         Height          =   255
         Index           =   7
         Left            =   3480
         TabIndex        =   52
         Top             =   3240
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "North"
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
         Height          =   255
         Index           =   13
         Left            =   240
         TabIndex        =   46
         Top             =   2520
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "East"
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
         Height          =   255
         Index           =   14
         Left            =   240
         TabIndex        =   45
         Top             =   2760
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "South"
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
         Height          =   255
         Index           =   15
         Left            =   240
         TabIndex        =   44
         Top             =   3000
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "West"
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
         Height          =   255
         Index           =   16
         Left            =   240
         TabIndex        =   43
         Top             =   3240
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Reverse RA"
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
         Height          =   255
         Index           =   5
         Left            =   240
         TabIndex        =   54
         Top             =   3600
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Reverse DEC"
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
         Height          =   255
         Index           =   6
         Left            =   240
         TabIndex        =   53
         Top             =   3840
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Filter4"
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
         Height          =   255
         Index           =   8
         Left            =   3480
         TabIndex        =   51
         Top             =   4440
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Filter5"
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
         Height          =   255
         Index           =   9
         Left            =   3480
         TabIndex        =   50
         Top             =   4680
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Filter6"
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
         Height          =   255
         Index           =   10
         Left            =   3480
         TabIndex        =   49
         Top             =   4920
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Decrease DEC Rate"
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
         Height          =   255
         Index           =   11
         Left            =   3480
         TabIndex        =   48
         Top             =   5400
         Visible         =   0   'False
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Increment Preset"
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
         Height          =   255
         Index           =   22
         Left            =   240
         TabIndex        =   15
         Top             =   4200
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Decrement Preset"
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
         Height          =   255
         Index           =   23
         Left            =   240
         TabIndex        =   14
         Top             =   4440
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Rate_1"
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
         Height          =   255
         Index           =   25
         Left            =   240
         TabIndex        =   9
         Top             =   4680
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Rate_2"
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
         Height          =   255
         Index           =   26
         Left            =   240
         TabIndex        =   8
         Top             =   4920
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Rate_3"
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
         Height          =   255
         Index           =   27
         Left            =   240
         TabIndex        =   7
         Top             =   5160
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Rate_4"
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
         Height          =   255
         Index           =   28
         Left            =   240
         TabIndex        =   6
         Top             =   5400
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Focuser IN"
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
         Height          =   255
         Index           =   12
         Left            =   3480
         TabIndex        =   47
         Top             =   360
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Focuser OUT"
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
         Height          =   255
         Index           =   17
         Left            =   3480
         TabIndex        =   25
         Top             =   600
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Filter3"
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
         Height          =   255
         Index           =   24
         Left            =   3480
         TabIndex        =   11
         Top             =   4200
         Width           =   2895
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   29
         Left            =   5280
         TabIndex        =   79
         Top             =   3960
         Width           =   1215
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Filter2"
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
         Height          =   255
         Index           =   29
         Left            =   3480
         TabIndex        =   80
         Top             =   3960
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Increment Preset"
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
         Height          =   255
         Index           =   30
         Left            =   3480
         TabIndex        =   86
         Top             =   840
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Decrement Preset"
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
         Height          =   255
         Index           =   31
         Left            =   3480
         TabIndex        =   88
         Top             =   1080
         Width           =   2895
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   32
         Left            =   5280
         TabIndex        =   89
         Top             =   1320
         Width           =   1215
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Speed _1"
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
         Height          =   255
         Index           =   32
         Left            =   3480
         TabIndex        =   90
         Top             =   1320
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Speed _2"
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
         Height          =   255
         Index           =   33
         Left            =   3480
         TabIndex        =   92
         Top             =   1560
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Speed _3"
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
         Height          =   255
         Index           =   34
         Left            =   3480
         TabIndex        =   94
         Top             =   1800
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Toggle Focus Lock"
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
         Height          =   255
         Index           =   35
         Left            =   3480
         TabIndex        =   97
         Top             =   2280
         Width           =   2895
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   36
         Left            =   5280
         TabIndex        =   98
         Top             =   2520
         Width           =   1215
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Select Focuser1"
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
         Height          =   255
         Index           =   36
         Left            =   3480
         TabIndex        =   99
         Top             =   2520
         Width           =   2895
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         BackStyle       =   0  'Transparent
         Caption         =   "---"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   37
         Left            =   5280
         TabIndex        =   100
         Top             =   2760
         Width           =   1215
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Select Focuser2"
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
         Height          =   255
         Index           =   37
         Left            =   3480
         TabIndex        =   101
         Top             =   2760
         Width           =   2895
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000040&
         Caption         =   "Speed _4"
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
         Height          =   255
         Index           =   38
         Left            =   3480
         TabIndex        =   103
         Top             =   2040
         Width           =   2895
      End
   End
End
Attribute VB_Name = "JStickConfigForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'---------------------------------------------------------------------
' EQMOD project Copyright © 2006 Raymund Sarmiento
'
' Permission is hereby granted to use this Software for any purpose
' including combining with commercial products, creating derivative
' works, and redistribution of source or binary code, without
' limitation or consideration. Any redistributed copies of this
' Software must include the above Copyright Notice.
'
' THIS SOFTWARE IS PROVIDED "AS IS". THE AUTHOR OF THIS CODE MAKES NO
' WARRANTIES REGARDING THIS SOFTWARE, EXPRESS OR IMPLIED, AS TO ITS
' SUITABILITY OR FITNESS FOR A PARTICULAR PURPOSE.
'---------------------------------------------------------------------
'
' JStickConfig.frm - ASCOM EQMOD Joystick assignemnt form
'
'
'
' Written:  27-Jul-07   Chris Shillito
'
' Edits:
'
' When      Who     What
' --------- ---     --------------------------------------------------
' 03-Apr-08 cs      Initial edit


Option Explicit


    Public LastIdx As Integer
    Public NoofJbtns As Integer
    
Private Sub ApplyBtn_Click()
    BTN_EMERGENCYSTOP = GetBtnData(Label2(0).Caption)
    BTN_USERPARK = GetBtnData(Label2(2).Caption)
    BTN_UNPARK = GetBtnData(Label2(3).Caption)
    BTN_STARTSIDREAL = GetBtnData(Label2(4).Caption)
    BTN_RAREVERSE = GetBtnData(Label2(5).Caption)
    BTN_DECREVERSE = GetBtnData(Label2(6).Caption)
    BTN_FOCUSIN = GetBtnData(Label2(12).Caption)
    BTN_NORTH = GetBtnData(Label2(13).Caption)
    BTN_EAST = GetBtnData(Label2(14).Caption)
    BTN_SOUTH = GetBtnData(Label2(15).Caption)
    BTN_WEST = GetBtnData(Label2(16).Caption)
    BTN_FOCUSOUT = GetBtnData(Label2(17).Caption)
    BTN_CUSTOMTRACKSTART = GetBtnData(Label2(18).Caption)
    BTN_STARTLUNAR = GetBtnData(Label2(20).Caption)
    BTN_STARTSOLAR = GetBtnData(Label2(21).Caption)
    BTN_INCRATEPRESET = GetBtnData(Label2(22).Caption)
    BTN_DECRATEPRESET = GetBtnData(Label2(23).Caption)
    BTN_RATE1 = GetBtnData(Label2(25).Caption)
    BTN_RATE2 = GetBtnData(Label2(26).Caption)
    BTN_RATE3 = GetBtnData(Label2(27).Caption)
    BTN_RATE4 = GetBtnData(Label2(28).Caption)
    
    BTN_INCFSPEEDPRESET = GetBtnData(Label2(30).Caption)
    BTN_DECFSPEEDPRESET = GetBtnData(Label2(31).Caption)
    BTN_FSPEED1 = GetBtnData(Label2(32).Caption)
    BTN_FSPEED2 = GetBtnData(Label2(33).Caption)
    BTN_FSPEED3 = GetBtnData(Label2(34).Caption)
    BTN_FSPEED4 = GetBtnData(Label2(38).Caption)
    BTN_FOCUSLOCK = GetBtnData(Label2(35).Caption)
    BTN_FOCUSER1 = GetBtnData(Label2(36).Caption)
    BTN_FOCUSER2 = GetBtnData(Label2(37).Caption)
    
    BTN_FWINC = GetBtnData(Label2(7).Caption)
    BTN_FWDEC = GetBtnData(Label2(19).Caption)
    BTN_FW1 = GetBtnData(Label2(1).Caption)
    BTN_FW2 = GetBtnData(Label2(29).Caption)
    BTN_FW3 = GetBtnData(Label2(24).Caption)
    BTN_FW4 = GetBtnData(Label2(8).Caption)
    BTN_FW5 = GetBtnData(Label2(9).Caption)
    BTN_FW6 = GetBtnData(Label2(10).Caption)
    
    BTN_DECRATEDEC = GetBtnData(Label2(11).Caption)
    
    LHJStick = Combo1.ListIndex
    RHJStick = Combo2.ListIndex
    If CheckPOV.Value = 1 Then
        POV_Enable = 1
    Else
        POV_Enable = 0
    End If
    
    Call SaveJoystickBtns
    Call SaveJoystickCalib

    Unload JStickConfigForm
    
End Sub

Private Sub CancelBtn_Click()
    Call LoadJoystickCalib
    Unload JStickConfigForm
End Sub

Private Sub ClearBtn_Click()
Dim Index As Integer

    For Index = 0 To Label2.count - 1
       Label2(Index).ForeColor = &HFF&
       Label2(Index).Caption = "---"
    Next Index

End Sub



Private Sub DefaultBtn_Click()
    ClearBtn_Click
End Sub

Private Sub Form_Load()
Dim i As Integer
Dim btnval As Long
Dim tmptxt As String

'    Call SetText
    StartBtn.Caption = "Start Calibration"
'    If HC.HCOnTop.Value = 1 Then Call PutWindowOnTop(JStickConfigForm)
    
    NoofJbtns = 0
    
    JoystickDat.dwSize = Len(JoystickDat)
    JoystickDat.dwFlags = JOY_RETURNALL

'    i = joyGetDevCaps(JOYSTICKID1, JoystickInfo, Len(JoystickInfo))
    i = joyGetPosEx(JOYSTICKID1, JoystickDat)

    If i <> JOYERR_NOERROR Then
'        i = joyGetDevCaps(JOYSTICKID2, JoystickInfo, Len(JoystickInfo))
         i = joyGetPosEx(JOYSTICKID2, JoystickDat)
    End If
        
    If i <> JOYERR_NOERROR Then
        ' no joystick don't let user trash any existing data
        ApplyBtn.enabled = False
        ClearBtn.enabled = False
    Else
        ' stop Joystick control of ths scope
        HC.Timer1.enabled = False
        ' start scanning
        XminLabel.Caption = CStr(JoystickCal.dwMinXpos)
        XmaxLabel.Caption = CStr(JoystickCal.dwMaxXpos)
        YminLabel.Caption = CStr(JoystickCal.dwMinYpos)
        YmaxLabel.Caption = CStr(JoystickCal.dwMaxYpos)
        ZminLabel.Caption = CStr(JoystickCal.dwMinZpos)
        ZmaxLabel.Caption = CStr(JoystickCal.dwMaxZpos)
        RminLabel.Caption = CStr(JoystickCal.dwMinRpos)
        RmaxLabel.Caption = CStr(JoystickCal.dwMaxRpos)
        Timer1.enabled = True
        Call BuildBtns
    End If
    
    Combo1.ListIndex = LHJStick
    Combo2.ListIndex = RHJStick
    
    If POV_Enable Then
        CheckPOV.Value = 1
    Else
        CheckPOV.Value = 0
    End If
    
    
    LastIdx = 0
        
End Sub

Private Sub Form_Unload(Cancel As Integer)

    HC.Timer1.enabled = True

End Sub

Private Sub Label1_Click(Index As Integer)
    
    ' highlight selcted function
    Label1(LastIdx).BorderStyle = 0
    Label1(Index).BorderStyle = 1
    LastIdx = Index
    
End Sub


Private Sub Label2_Click(Index As Integer)
    Label1_Click (Index)
End Sub

Private Sub BuildBtns()

    Label2(0).Caption = GetBtnStr(BTN_EMERGENCYSTOP)
    Label2(2).Caption = GetBtnStr(BTN_USERPARK)
    Label2(3).Caption = GetBtnStr(BTN_UNPARK)
    Label2(4).Caption = GetBtnStr(BTN_STARTSIDREAL)
    Label2(5).Caption = GetBtnStr(BTN_RAREVERSE)
    Label2(6).Caption = GetBtnStr(BTN_DECREVERSE)
    Label2(12).Caption = GetBtnStr(BTN_FOCUSIN)
    Label2(13).Caption = GetBtnStr(BTN_NORTH)
    Label2(14).Caption = GetBtnStr(BTN_EAST)
    Label2(15).Caption = GetBtnStr(BTN_SOUTH)
    Label2(16).Caption = GetBtnStr(BTN_WEST)
    Label2(17).Caption = GetBtnStr(BTN_FOCUSOUT)
    Label2(18).Caption = GetBtnStr(BTN_CUSTOMTRACKSTART)
    Label2(20).Caption = GetBtnStr(BTN_STARTLUNAR)
    Label2(21).Caption = GetBtnStr(BTN_STARTSOLAR)
    Label2(22).Caption = GetBtnStr(BTN_INCRATEPRESET)
    Label2(23).Caption = GetBtnStr(BTN_DECRATEPRESET)
    Label2(25).Caption = GetBtnStr(BTN_RATE1)
    Label2(26).Caption = GetBtnStr(BTN_RATE2)
    Label2(27).Caption = GetBtnStr(BTN_RATE3)
    Label2(28).Caption = GetBtnStr(BTN_RATE4)
    Label2(30).Caption = GetBtnStr(BTN_INCFSPEEDPRESET)
    Label2(31).Caption = GetBtnStr(BTN_DECFSPEEDPRESET)
    Label2(32).Caption = GetBtnStr(BTN_FSPEED1)
    Label2(33).Caption = GetBtnStr(BTN_FSPEED2)
    Label2(34).Caption = GetBtnStr(BTN_FSPEED3)
    Label2(35).Caption = GetBtnStr(BTN_FOCUSLOCK)
    Label2(36).Caption = GetBtnStr(BTN_FOCUSER1)
    Label2(37).Caption = GetBtnStr(BTN_FOCUSER2)
    Label2(38).Caption = GetBtnStr(BTN_FSPEED4)

    Label2(7).Caption = GetBtnStr(BTN_FWINC)
    Label2(19).Caption = GetBtnStr(BTN_FWDEC)
    Label2(1).Caption = GetBtnStr(BTN_FW1)
    Label2(29).Caption = GetBtnStr(BTN_FW2)
    Label2(24).Caption = GetBtnStr(BTN_FW3)
    Label2(8).Caption = GetBtnStr(BTN_FW4)
    Label2(9).Caption = GetBtnStr(BTN_FW5)
    Label2(10).Caption = GetBtnStr(BTN_FW6)
    
    Label2(11).Caption = GetBtnStr(BTN_DECRATEDEC)


End Sub
            
Private Function GetBtnStr(BtnData As Long) As String
Dim i As Integer
Dim mask As Long

    If BtnData >= 65536 Then
        ' its a point of view button
        BtnData = BtnData - 65536
        Select Case BtnData
            Case 9000
                 GetBtnStr = "POV_E"
            Case 27000
                 GetBtnStr = "POV_W"
            Case 0
                 GetBtnStr = "POV_N"
            Case 18000
                 GetBtnStr = "POV_S"
            Case 31500
                 GetBtnStr = "POV_NW"
            Case 4500
                 GetBtnStr = "POV_NE"
            Case 22500
                 GetBtnStr = "POV_SE"
            Case 13500
                 GetBtnStr = "POV_SW"
            Case Else
                GetBtnStr = "---"
        End Select
    Else
    '    For i = 1 To JoystickInfo.wNumButtons
        For i = 1 To 31
            If i = 1 Then
                mask = 1
            Else
                If i = 32 Then
                    ' can't shift a signed long any further so assign number directly
                    mask = &H80000000
                Else
                    mask = mask * 2
                End If
            End If
            If mask = BtnData Then
                GetBtnStr = "BUTTON_" & CStr(i)
                Exit Function
            End If
        Next i
        ' no natch found
        GetBtnStr = "---"
    End If
    
End Function

Private Function GetBtnData(BtnStr As String) As Long
Dim tmptxt As String
Dim i As Integer
Dim BtnNo As Integer

    If BtnStr = "---" Then
        GetBtnData = 0
    Else
        If Left$(BtnStr, 1) = "P" Then
            Select Case BtnStr
                Case "POV_N"
                    GetBtnData = 65536 + 0
                Case "POV_E"
                    GetBtnData = 65536 + 9000
                Case "POV_S"
                    GetBtnData = 65536 + 18000
                Case "POV_W"
                    GetBtnData = 65536 + 27000
                Case "POV_NW"
                    GetBtnData = 65536 + 31500
                Case "POV_NE"
                    GetBtnData = 65536 + 4500
                Case "POV_SW"
                    GetBtnData = 65536 + 13500
                Case "POV_SE"
                    GetBtnData = 65536 + 22500
                Case Else
                    GetBtnData = 0
            End Select
        Else
           tmptxt = Right$(BtnStr, Len(BtnStr) - 7)
           BtnNo = val(tmptxt)
    '       If BtnNo > JoystickInfo.wNumButtons Or BtnNo < 1 Then
           If BtnNo > 32 Or BtnNo < 1 Then
               GetBtnData = 0
           Else
               If BtnNo = 32 Then
                   ' special case when top bit is set in a signed long
                   GetBtnData = &H80000000
               Else
                   ' get button data by shifting by the buton number
                   GetBtnData = 1
                   For i = 2 To BtnNo
                       GetBtnData = GetBtnData * 2
                   Next i
               End If
           End If
        End If
    End If
    
End Function

Private Sub Label2_MouseDown(Index As Integer, Button As Integer, Shift As Integer, x As Single, Y As Single)
    If Button = 2 Then
       ' right click = clear assignment
        Label2(Index).ForeColor = &HFF&
        Label2(Index).Caption = "---"
    End If
End Sub

Private Sub Label1_MouseDown(Index As Integer, Button As Integer, Shift As Integer, x As Single, Y As Single)
    If Button = 2 Then
       ' right click = clear assignment
        Label2(Index).ForeColor = &HFF&
        Label2(Index).Caption = "---"
    End If
End Sub

Private Sub StartBtn_Click()

    If StartBtn.Caption = "Start Calibration" Then
        JoystickCal.dwMinXpos = 32767
        JoystickCal.dwMaxXpos = 32767
        JoystickCal.dwMinYpos = 32767
        JoystickCal.dwMaxYpos = 32767
        JoystickCal.dwMinZpos = 32767
        JoystickCal.dwMaxZpos = 32767
        JoystickCal.dwMinRpos = 32767
        JoystickCal.dwMaxRpos = 32767
        Timer2.enabled = True
        Timer1.enabled = False
        StartBtn.Caption = "Done"
    Else
        Timer2.enabled = False
        Timer1.enabled = True
        StartBtn.Caption = "Start Calibration"
        SaveJoystickCalib
    End If
    
End Sub

Private Sub Timer1_Timer()
Dim i As Long
Dim BtnNo As Long
Dim Index As Integer
Dim tmptxt As String
Dim mask As Long
    
    ' read first joystick
    i = joyGetPosEx(JOYSTICKID1, JoystickDat)
    If i <> JOYERR_NOERROR Then
        ' try second joystick
        i = joyGetPosEx(JOYSTICKID2, JoystickDat)
    End If
        
    If i = JOYERR_NOERROR Then
'        For BtnNo = 1 To JoystickInfo.wNumButtons
        For BtnNo = 1 To 32
            If BtnNo = 1 Then
                mask = 1
            Else
                If BtnNo = 32 Then
                    ' can't shift a signed long any further so assign number directly
                    mask = &H80000000
                Else
                    ' shift mask to next button
                    mask = mask * 2
                End If
            End If
            ' check if button has been pressed
            If JoystickDat.dwButtons And mask Then
                 ' construct a label
                 tmptxt = "BUTTON_" & CStr(BtnNo)
                 Label2(LastIdx).Caption = tmptxt
                 ' find any duplicates and highlight them
                 For Index = 0 To Label2.count - 1
                    Label2(Index).ForeColor = &HFF&
                    If Index <> LastIdx Then
                        If Label2(Index).Caption = tmptxt Then
                            Label2(Index).ForeColor = &H80FFFF
                        End If
                    End If
                 Next Index
                GoTo endtimer
            End If
        Next BtnNo
        
        If JoystickDat.dwPOV <> 65535 Then
            Select Case JoystickDat.dwPOV
                Case 9000
                     tmptxt = "POV_E"
                Case 27000
                     tmptxt = "POV_W"
                Case 0
                     tmptxt = "POV_N"
                Case 18000
                     tmptxt = "POV_S"
                Case 31500
                     tmptxt = "POV_NW"
                Case 4500
                     tmptxt = "POV_NE"
                Case 22500
                     tmptxt = "POV_SE"
                Case 13500
                     tmptxt = "POV_SW"
            End Select
            
            Label2(LastIdx).Caption = tmptxt
            ' find any duplicates and highlight them
            For Index = 0 To Label2.count - 1
               Label2(Index).ForeColor = &HFF&
               If Index <> LastIdx Then
                   If Label2(Index).Caption = tmptxt Then
                       Label2(Index).ForeColor = &H80FFFF
                   End If
               End If
            Next Index
        End If
        
        
    End If
endtimer:

End Sub



Private Sub Timer2_Timer()
Dim i As Long
Dim dwXpos As Long
Dim dwYpos As Long
Dim dwZpos As Long
Dim dwRpos As Long
        
    JoystickDat.dwSize = Len(JoystickDat)
    JoystickDat.dwFlags = JOY_RETURNALL

    i = joyGetPosEx(JOYSTICKID1, JoystickDat)
    If i <> JOYERR_NOERROR Then
        i = joyGetPosEx(JOYSTICKID2, JoystickDat)
    End If
    If i <> JOYERR_NOERROR Then
        
        ' Joystick not found disable joystick scan
        
    End If
    
    dwXpos = JoystickDat.dwXpos
    dwYpos = JoystickDat.dwYpos
    dwZpos = JoystickDat.dwZpos
    dwRpos = JoystickDat.dwRpos
    
    If dwXpos > JoystickCal.dwMaxXpos Then JoystickCal.dwMaxXpos = dwXpos
    If dwYpos > JoystickCal.dwMaxYpos Then JoystickCal.dwMaxYpos = dwYpos
    If dwZpos > JoystickCal.dwMaxZpos Then JoystickCal.dwMaxZpos = dwZpos
    If dwRpos > JoystickCal.dwMaxRpos Then JoystickCal.dwMaxRpos = dwRpos
    If dwXpos < JoystickCal.dwMinXpos Then JoystickCal.dwMinXpos = dwXpos
    If dwYpos < JoystickCal.dwMinYpos Then JoystickCal.dwMinYpos = dwYpos
    If dwZpos < JoystickCal.dwMinZpos Then JoystickCal.dwMinZpos = dwZpos
    If dwRpos < JoystickCal.dwMinRpos Then JoystickCal.dwMinRpos = dwRpos
    
    XminLabel.Caption = CStr(JoystickCal.dwMinXpos)
    XmaxLabel.Caption = CStr(JoystickCal.dwMaxXpos)
    YminLabel.Caption = CStr(JoystickCal.dwMinYpos)
    YmaxLabel.Caption = CStr(JoystickCal.dwMaxYpos)
    ZminLabel.Caption = CStr(JoystickCal.dwMinZpos)
    ZmaxLabel.Caption = CStr(JoystickCal.dwMaxZpos)
    RminLabel.Caption = CStr(JoystickCal.dwMinRpos)
    RmaxLabel.Caption = CStr(JoystickCal.dwMaxRpos)
End Sub
