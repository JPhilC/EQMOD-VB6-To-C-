VERSION 5.00
Begin VB.Form RatesFrm 
   BackColor       =   &H00000000&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Mount Control Setup"
   ClientHeight    =   6165
   ClientLeft      =   45
   ClientTop       =   315
   ClientWidth     =   11520
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
   ScaleHeight     =   6165
   ScaleWidth      =   11520
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
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
      Height          =   5895
      Left            =   5280
      TabIndex        =   42
      Top             =   120
      Width           =   6135
      Begin VB.CommandButton CmdLoadBeep 
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
         Left            =   600
         Picture         =   "RatesFrm.frx":0000
         Style           =   1  'Graphical
         TabIndex        =   67
         Top             =   480
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
         Index           =   0
         Left            =   3600
         Picture         =   "RatesFrm.frx":0582
         Style           =   1  'Graphical
         TabIndex        =   66
         Top             =   480
         Width           =   350
      End
      Begin VB.CheckBox ChkRate 
         BackColor       =   &H00000000&
         Caption         =   "Rate Presets"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   3240
         TabIndex        =   65
         TabStop         =   0   'False
         Top             =   240
         Width           =   2775
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
         Index           =   1
         Left            =   3600
         Picture         =   "RatesFrm.frx":0B04
         Style           =   1  'Graphical
         TabIndex        =   64
         Top             =   840
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
         Index           =   2
         Left            =   3600
         Picture         =   "RatesFrm.frx":1086
         Style           =   1  'Graphical
         TabIndex        =   63
         Top             =   1200
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
         Index           =   3
         Left            =   3600
         Picture         =   "RatesFrm.frx":1608
         Style           =   1  'Graphical
         TabIndex        =   62
         Top             =   1560
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
         Index           =   4
         Left            =   3600
         Picture         =   "RatesFrm.frx":1B8A
         Style           =   1  'Graphical
         TabIndex        =   61
         Top             =   1920
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
         Index           =   5
         Left            =   3600
         Picture         =   "RatesFrm.frx":210C
         Style           =   1  'Graphical
         TabIndex        =   60
         Top             =   2280
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
         Index           =   6
         Left            =   3600
         Picture         =   "RatesFrm.frx":268E
         Style           =   1  'Graphical
         TabIndex        =   59
         Top             =   2640
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
         Index           =   7
         Left            =   3600
         Picture         =   "RatesFrm.frx":2C10
         Style           =   1  'Graphical
         TabIndex        =   58
         Top             =   3000
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
         Index           =   8
         Left            =   3600
         Picture         =   "RatesFrm.frx":3192
         Style           =   1  'Graphical
         TabIndex        =   57
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
         Index           =   9
         Left            =   3600
         Picture         =   "RatesFrm.frx":3714
         Style           =   1  'Graphical
         TabIndex        =   56
         Top             =   3720
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadStop 
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
         Left            =   600
         Picture         =   "RatesFrm.frx":3C96
         Style           =   1  'Graphical
         TabIndex        =   55
         Top             =   2520
         Width           =   350
      End
      Begin VB.CheckBox ChkStop 
         BackColor       =   &H00000000&
         Caption         =   "Stop"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   240
         TabIndex        =   54
         TabStop         =   0   'False
         Top             =   2280
         Width           =   2775
      End
      Begin VB.CheckBox ChkParked 
         BackColor       =   &H00000000&
         Caption         =   "Park"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   240
         TabIndex        =   53
         TabStop         =   0   'False
         Top             =   1080
         Width           =   2775
      End
      Begin VB.CommandButton CmdLoadParked 
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
         Left            =   600
         Picture         =   "RatesFrm.frx":4218
         Style           =   1  'Graphical
         TabIndex        =   52
         Top             =   1320
         Width           =   350
      End
      Begin VB.CommandButton CmdApply 
         BackColor       =   &H0095C1CB&
         Caption         =   "Apply Changes"
         Height          =   255
         Left            =   3480
         Style           =   1  'Graphical
         TabIndex        =   51
         Top             =   5520
         Width           =   2535
      End
      Begin VB.CheckBox ChkUnpark 
         BackColor       =   &H00000000&
         Caption         =   "Unpark"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   240
         TabIndex        =   50
         TabStop         =   0   'False
         Top             =   1680
         Width           =   2775
      End
      Begin VB.CommandButton CmdLoadUnpark 
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
         Left            =   600
         Picture         =   "RatesFrm.frx":479A
         Style           =   1  'Graphical
         TabIndex        =   49
         Top             =   1935
         Width           =   350
      End
      Begin VB.CheckBox ChkTracking 
         BackColor       =   &H00000000&
         Caption         =   "Tracking"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   240
         TabIndex        =   48
         TabStop         =   0   'False
         Top             =   3120
         Width           =   2775
      End
      Begin VB.CommandButton CmdLoadSidereal 
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
         Left            =   600
         Picture         =   "RatesFrm.frx":4D1C
         Style           =   1  'Graphical
         TabIndex        =   47
         Top             =   3630
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadSolar 
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
         Left            =   600
         Picture         =   "RatesFrm.frx":529E
         Style           =   1  'Graphical
         TabIndex        =   46
         Top             =   4230
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadLunar 
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
         Left            =   600
         Picture         =   "RatesFrm.frx":5820
         Style           =   1  'Graphical
         TabIndex        =   45
         Top             =   4830
         Width           =   350
      End
      Begin VB.CommandButton CmdLoadCustom 
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
         Left            =   600
         Picture         =   "RatesFrm.frx":5DA2
         Style           =   1  'Graphical
         TabIndex        =   44
         Top             =   5430
         Width           =   350
      End
      Begin VB.CheckBox ChkBeep 
         BackColor       =   &H00000000&
         Caption         =   "Beep"
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   240
         TabIndex        =   43
         TabStop         =   0   'False
         Top             =   240
         Width           =   2775
      End
      Begin VB.Label LabelBeep 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1080
         TabIndex        =   99
         Top             =   480
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   0
         Left            =   4080
         TabIndex        =   98
         Top             =   480
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   1
         Left            =   4080
         TabIndex        =   97
         Top             =   840
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   2
         Left            =   4080
         TabIndex        =   96
         Top             =   1200
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   3
         Left            =   4080
         TabIndex        =   95
         Top             =   1560
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   4
         Left            =   4080
         TabIndex        =   94
         Top             =   1920
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   5
         Left            =   4080
         TabIndex        =   93
         Top             =   2280
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   6
         Left            =   4080
         TabIndex        =   92
         Top             =   2640
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   7
         Left            =   4080
         TabIndex        =   91
         Top             =   3000
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   8
         Left            =   4080
         TabIndex        =   90
         Top             =   3360
         Width           =   1935
      End
      Begin VB.Label LabelRate 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   9
         Left            =   4080
         TabIndex        =   89
         Top             =   3720
         Width           =   1935
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "1"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   10
         Left            =   3240
         TabIndex        =   88
         Top             =   480
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "2"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   1
         Left            =   3240
         TabIndex        =   87
         Top             =   840
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "3"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   2
         Left            =   3240
         TabIndex        =   86
         Top             =   1200
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "4"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   3
         Left            =   3240
         TabIndex        =   85
         Top             =   1560
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "5"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   4
         Left            =   3240
         TabIndex        =   84
         Top             =   1920
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "6"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   5
         Left            =   3240
         TabIndex        =   83
         Top             =   2280
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "7"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   6
         Left            =   3240
         TabIndex        =   82
         Top             =   2640
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "8"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   7
         Left            =   3240
         TabIndex        =   81
         Top             =   3000
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "9"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   8
         Left            =   3240
         TabIndex        =   80
         Top             =   3360
         Width           =   285
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "10"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   9
         Left            =   3240
         TabIndex        =   79
         Top             =   3720
         Width           =   285
      End
      Begin VB.Label LabelStop 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1080
         TabIndex        =   78
         Top             =   2520
         Width           =   1935
      End
      Begin VB.Label LabelParked 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   240
         Left            =   1080
         TabIndex        =   77
         Top             =   1335
         Width           =   1935
      End
      Begin VB.Label LabelUnpark 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1080
         TabIndex        =   76
         Top             =   1920
         Width           =   1935
      End
      Begin VB.Label LabelSidereal 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1080
         TabIndex        =   75
         Top             =   3600
         Width           =   1935
      End
      Begin VB.Label LabelSolar 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1080
         TabIndex        =   74
         Top             =   4230
         Width           =   1935
      End
      Begin VB.Label LabelLunar 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1080
         TabIndex        =   73
         Top             =   4830
         Width           =   1935
      End
      Begin VB.Label LabelCustom 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   1080
         TabIndex        =   72
         Top             =   5430
         Width           =   1935
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000000&
         Caption         =   "Solar"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   11
         Left            =   600
         TabIndex        =   71
         Top             =   3975
         Width           =   1095
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "Lunar"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   10
         Left            =   600
         TabIndex        =   70
         Top             =   4575
         Width           =   2775
      End
      Begin VB.Label Label7 
         BackColor       =   &H00000000&
         Caption         =   "Custom"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   600
         TabIndex        =   69
         Top             =   5175
         Width           =   2775
      End
      Begin VB.Label Label6 
         BackColor       =   &H00000000&
         Caption         =   "Sidereal"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   600
         TabIndex        =   68
         Top             =   3360
         Width           =   2415
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
      TabIndex        =   36
      Top             =   120
      Width           =   5055
      Begin VB.CommandButton Command3 
         BackColor       =   &H0095C1CB&
         Caption         =   "Remove"
         Height          =   255
         Left            =   1440
         Style           =   1  'Graphical
         TabIndex        =   40
         Top             =   600
         Width           =   1095
      End
      Begin VB.CommandButton Command2 
         BackColor       =   &H0095C1CB&
         Caption         =   "Driver Capabiities"
         Height          =   255
         Left            =   2760
         Style           =   1  'Graphical
         TabIndex        =   39
         Top             =   600
         Width           =   1455
      End
      Begin VB.CommandButton Command1 
         BackColor       =   &H0095C1CB&
         Caption         =   "Select"
         Height          =   255
         Left            =   120
         Style           =   1  'Graphical
         TabIndex        =   38
         Top             =   600
         Width           =   1095
      End
      Begin VB.Label Label5 
         BackColor       =   &H00000000&
         Caption         =   "Label5"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   840
         TabIndex        =   41
         Top             =   240
         Width           =   3975
      End
      Begin VB.Label Label1 
         BackColor       =   &H00000000&
         Caption         =   "Driver ID"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   10
         Left            =   120
         TabIndex        =   37
         Top             =   240
         Width           =   1335
      End
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00000000&
      Caption         =   "Custom Track Rate"
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
      Height          =   855
      Left            =   120
      TabIndex        =   31
      Top             =   5160
      Width           =   5055
      Begin VB.TextBox Text2 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   285
         Left            =   2520
         TabIndex        =   35
         Text            =   "0"
         Top             =   360
         Width           =   1095
      End
      Begin VB.TextBox Text1 
         BackColor       =   &H00000040&
         ForeColor       =   &H000000FF&
         Height          =   285
         Left            =   600
         TabIndex        =   34
         Text            =   "0"
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label4 
         BackColor       =   &H00000000&
         Caption         =   "DEC"
         ForeColor       =   &H000080FF&
         Height          =   255
         Left            =   2040
         TabIndex        =   33
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label3 
         BackColor       =   &H00000000&
         Caption         =   "RA"
         ForeColor       =   &H000080FF&
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   32
         Top             =   360
         Width           =   495
      End
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00000000&
      Caption         =   "Preset Slew Rates (xSidereal)"
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
      Height          =   3495
      Left            =   120
      TabIndex        =   0
      Top             =   1320
      Width           =   5055
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   1
         Left            =   480
         Max             =   800
         TabIndex        =   10
         Top             =   1200
         Width           =   1815
      End
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   2
         Left            =   480
         Max             =   800
         TabIndex        =   9
         Top             =   1800
         Width           =   1815
      End
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   3
         Left            =   480
         Max             =   800
         TabIndex        =   8
         Top             =   2400
         Width           =   1815
      End
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   4
         Left            =   480
         Max             =   800
         TabIndex        =   7
         Top             =   3000
         Width           =   1815
      End
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   5
         Left            =   2880
         Max             =   800
         TabIndex        =   6
         Top             =   600
         Width           =   1815
      End
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   6
         Left            =   2880
         Max             =   800
         TabIndex        =   5
         Top             =   1200
         Width           =   1815
      End
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   7
         Left            =   2880
         Max             =   800
         TabIndex        =   4
         Top             =   1800
         Width           =   1815
      End
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   8
         Left            =   2880
         Max             =   800
         TabIndex        =   3
         Top             =   2400
         Width           =   1815
      End
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   9
         Left            =   2880
         Max             =   800
         TabIndex        =   2
         Top             =   3000
         Width           =   1815
      End
      Begin VB.HScrollBar HScroll1 
         Height          =   135
         Index           =   0
         Left            =   480
         Max             =   800
         TabIndex        =   1
         Top             =   600
         Width           =   1815
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   0
         Left            =   1440
         TabIndex        =   30
         Top             =   360
         Width           =   855
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   1
         Left            =   1440
         TabIndex        =   29
         Top             =   960
         Width           =   855
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   2
         Left            =   1440
         TabIndex        =   28
         Top             =   1560
         Width           =   855
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   3
         Left            =   1440
         TabIndex        =   27
         Top             =   2160
         Width           =   855
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   4
         Left            =   1440
         TabIndex        =   26
         Top             =   2760
         Width           =   855
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   5
         Left            =   3840
         TabIndex        =   25
         Top             =   360
         Width           =   855
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   6
         Left            =   3840
         TabIndex        =   24
         Top             =   960
         Width           =   855
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   7
         Left            =   3840
         TabIndex        =   23
         Top             =   1560
         Width           =   855
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   8
         Left            =   3840
         TabIndex        =   22
         Top             =   2160
         Width           =   855
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00000000&
         Caption         =   "0"
         ForeColor       =   &H000000FF&
         Height          =   255
         Index           =   9
         Left            =   3840
         TabIndex        =   21
         Top             =   2760
         Width           =   855
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   1
         Left            =   120
         TabIndex        =   20
         Top             =   1140
         Width           =   375
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   2
         Left            =   120
         TabIndex        =   19
         Top             =   1740
         Width           =   375
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   3
         Left            =   120
         TabIndex        =   18
         Top             =   2340
         Width           =   375
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   4
         Left            =   120
         TabIndex        =   17
         Top             =   2940
         Width           =   375
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   5
         Left            =   2520
         TabIndex        =   16
         Top             =   540
         Width           =   375
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   6
         Left            =   2520
         TabIndex        =   15
         Top             =   1140
         Width           =   375
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   7
         Left            =   2520
         TabIndex        =   14
         Top             =   1740
         Width           =   375
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   8
         Left            =   2520
         TabIndex        =   13
         Top             =   2340
         Width           =   375
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   9
         Left            =   2520
         TabIndex        =   12
         Top             =   2940
         Width           =   375
      End
      Begin VB.Label Label2 
         BackColor       =   &H00000000&
         Caption         =   "1"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000080FF&
         Height          =   375
         Index           =   0
         Left            =   120
         TabIndex        =   11
         Top             =   540
         Width           =   375
      End
   End
End
Attribute VB_Name = "RatesFrm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit




Private Sub CmdLoadCustom_Click()
    FileDlg.filter = "*.wav*"
    FileDlg.Show (1)
    If FileDlg.filename <> "" Then
        LabelCustom.Caption = FileDlg.filename2
        LabelCustom.ToolTipText = FileDlg.filename
    End If

End Sub

Private Sub Command1_Click()

    Call HC.ChooseTelescope
    Label5.Caption = HC.oPersist.ReadIniValue("driver")

End Sub

Private Sub Command2_Click()
    ASCOMStatus.Show (1)
End Sub

Private Sub Command3_Click()
    Call HC.oPersist.WriteIniValue("driver", "")
    Label5.Caption = ""
    Call ascomDisconnect
    Call HC.DisableMountControls
End Sub

Private Sub Form_Load()
Dim i As Integer

On Error Resume Next

    For i = 0 To 9
        Label2(i).Caption = CStr(i + 1)
        If rates(i) = 0 Then
            HScroll1(i).Value = 0
            Label1(i).Caption = "Undefined"
        Else
            HScroll1(i).Value = CInt(rates(i) * 3600 / 15)
        End If
    Next i
    
    Text1.Text = FormatNumber(CustomRateRA, 5)
    Text2.Text = FormatNumber(CustomRateDEC, 5)
    
    Label5.Caption = HC.oPersist.ReadIniValue("driver")
    
    Call RefreshControls
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
    CustomRateRA = val(Text1.Text)
    CustomRateDEC = val(Text2.Text)
    SaveRates
End Sub

Private Sub HScroll1_Change(Index As Integer)
Dim tmp As Double
    tmp = 15 * HScroll1(Index).Value
    rates(Index) = tmp / 3600
    Label1(Index).Caption = CStr(HScroll1(Index).Value)
End Sub

Private Sub HScroll1_Scroll(Index As Integer)
    HScroll1_Change Index
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

