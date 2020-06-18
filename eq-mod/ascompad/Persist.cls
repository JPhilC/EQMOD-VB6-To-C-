VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "Persist"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private iniPath As String
Private INIfilename As String
Private key As String

Private Sub Class_Initialize()
    iniPath = Environ("APPDATA") + "\EQMOD"
    On Error Resume Next
    If GetAttr(iniPath) <> vbDirectory Then
        MkDir (iniPath)
    End If
    INIfilename = iniPath + "\ASCOMPAD.ini"
    key = "[default]"
End Sub

Public Sub Store()
Dim Index As Integer
End Sub

Public Sub Retrieve()

End Sub


' write using the default key and path
Public Sub WriteIniValue(PutVariable As String, PutValue As String)
    Call WriteIniValueEx(PutVariable, PutValue, key, INIfilename)
End Sub

' read using the default key and path
Public Function ReadIniValue(Variable As String) As String
ReadIniValue = ReadIniValueEx(Variable, key, INIfilename)
End Function

' write using any key and path
Public Sub WriteIniValueEx(PutVariable As String, PutValue As String, IniKey As String, filepath As String)
Dim Temp As String
Dim LcaseTemp As String
Dim ReadKey As String
Dim ReadVariable As String
Dim LOKEY As Integer
Dim HIKEY As Integer
Dim KEYLEN As Integer
Dim VAR As Integer
Dim VARENDOFLINE As Integer
Dim NF As Integer
Dim X As Integer

AssignVariables:
    NF = FreeFile
    ReadKey = vbCrLf & IniKey & Chr$(13)
    KEYLEN = Len(ReadKey)
    ReadVariable = Chr$(10) & LCase$(PutVariable) & "="
        
EnsureFileExists:
    Open filepath For Binary As NF
    Close NF
    SetAttr filepath, vbArchive
    
LoadFile:
    Open filepath For Input As NF
    Temp = Input$(LOF(NF), NF)
    Temp = vbCrLf & Temp & "[]"
    Close NF
    LcaseTemp = LCase$(Temp)
    
LogicMenu:
    LOKEY = InStr(LcaseTemp, ReadKey)
    If LOKEY = 0 Then GoTo AddKey:
    HIKEY = InStr(LOKEY + KEYLEN, LcaseTemp, "[")
    VAR = InStr(LOKEY, LcaseTemp, ReadVariable)
    If VAR > HIKEY Or VAR < LOKEY Then GoTo AddVariable:
    GoTo RenewVariable:
    
AddKey:
        Temp = Left$(Temp, Len(Temp) - 2)
        Temp = Temp & vbCrLf & vbCrLf & IniKey & vbCrLf & PutVariable & "=" & PutValue
        GoTo TrimFinalString:
        
AddVariable:
        Temp = Left$(Temp, Len(Temp) - 2)
        Temp = Left$(Temp, LOKEY + KEYLEN) & PutVariable & "=" & PutValue & vbCrLf & Mid$(Temp, LOKEY + KEYLEN + 1)
        GoTo TrimFinalString:
        
RenewVariable:
        Temp = Left$(Temp, Len(Temp) - 2)
        VARENDOFLINE = InStr(VAR, Temp, Chr$(13))
        Temp = Left$(Temp, VAR) & PutVariable & "=" & PutValue & Mid$(Temp, VARENDOFLINE)
        GoTo TrimFinalString:

TrimFinalString:
        Temp = Mid$(Temp, 2)
        Do Until InStr(Temp, vbCrLf & vbCrLf & vbCrLf) = 0
        Temp = Replace(Temp, vbCrLf & vbCrLf & vbCrLf, vbCrLf & vbCrLf)
        Loop
    
        Do Until Right$(Temp, 1) > Chr$(13)
        Temp = Left$(Temp, Len(Temp) - 1)
        Loop
    
        Do Until Left$(Temp, 1) > Chr$(13)
        Temp = Mid$(Temp, 2)
        Loop
    
OutputAmendedINIFile:
        Open filepath For Output As NF
        Print #NF, Temp
        Close NF
    
End Sub

' read using any key and path
Public Function ReadIniValueEx(Variable As String, IniKey As String, filepath As String) As String
Dim NF As Integer
Dim Temp As String
Dim LcaseTemp As String
Dim ReadyToRead As Boolean
    
AssignVariables:
        NF = FreeFile
        ReadIniValueEx = ""
        Variable = LCase$(Variable)
    
EnsureFileExists:
    Open filepath For Binary As NF
    Close NF
    SetAttr filepath, vbArchive
    
LoadFile:
    Open filepath For Input As NF
    While Not EOF(NF)
    Line Input #NF, Temp
    LcaseTemp = LCase$(Temp)
    If InStr(LcaseTemp, "[") <> 0 Then ReadyToRead = False
    If LcaseTemp = IniKey Then ReadyToRead = True
    If InStr(LcaseTemp, "[") = 0 And ReadyToRead = True Then
        If InStr(LcaseTemp, Variable & "=") = 1 Then
            ReadIniValueEx = Mid$(Temp, 1 + Len(Variable & "="))
            Close NF: Exit Function
            End If
        End If
    Wend
    Close NF
End Function

Public Function GetIniPath() As String
    GetIniPath = iniPath
End Function
