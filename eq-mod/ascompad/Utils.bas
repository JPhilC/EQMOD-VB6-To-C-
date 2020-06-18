Attribute VB_Name = "Utils"
Public Declare Function SendMessage Lib "user32" Alias _
        "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As _
        Long, ByVal wParam As Long, lParam As Any) As Long

Public Const LB_SETTABSTOPS = &H192
 
' The number of tabs
Public Const OBorder = 3
 
Public Tabulator(1 To OBorder) As Long


'user defined type required by Shell_NotifyIcon API call
Public Type NOTIFYICONDATA
 cbSize As Long
 hwnd As Long
 uId As Long
 uFlags As Long
 uCallBackMessage As Long
 hIcon As Long
 szTip As String * 64
End Type

'constants required by Shell_NotifyIcon API call:
Public Const NIM_ADD = &H0
Public Const NIM_MODIFY = &H1
Public Const NIM_DELETE = &H2
Public Const NIF_MESSAGE = &H1
Public Const NIF_ICON = &H2
Public Const NIF_TIP = &H4
Public Const WM_MOUSEMOVE = &H200
Public Const WM_LBUTTONDOWN = &H201     'Button down
Public Const WM_LBUTTONUP = &H202       'Button up
Public Const WM_LBUTTONDBLCLK = &H203   'Double-click
Public Const WM_RBUTTONDOWN = &H204     'Button down
Public Const WM_RBUTTONUP = &H205       'Button up
Public Const WM_RBUTTONDBLCLK = &H206   'Double-click

Public Declare Function SetForegroundWindow Lib "user32" _
(ByVal hwnd As Long) As Long
Public Declare Function Shell_NotifyIcon Lib "shell32" _
Alias "Shell_NotifyIconA" _
(ByVal dwMessage As Long, pnid As NOTIFYICONDATA) As Boolean

Public nid As NOTIFYICONDATA


Private Declare Function SetWindowPos Lib "user32" _
         (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, _
          ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long

Const HWND_TOPMOST = -1
Const HWND_NOTTOPMOST = -2
Const SWP_NOMOVE = &H2
Const SWP_NOSIZE = &H1


Public Sub PutWindowOnTop(pFrm As Form)
  Dim lngWindowPosition As Long
  
  lngWindowPosition = SetWindowPos(pFrm.hwnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)

End Sub
Public Sub PutWindowNormal(pFrm As Form)
  Dim lngWindowPosition As Long
  
  lngWindowPosition = SetWindowPos(pFrm.hwnd, HWND_NOTTOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)

End Sub

Public Function StripPath(str As String) As String
Dim i As Integer
    i = InStrRev(str, "\")
    StripPath = Right$(str, Len(str) - i)
End Function


