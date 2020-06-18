Attribute VB_Name = "ASCOM"
Option Explicit

Public Const SID_RATE As Double = 15.041067
Public Const SOL_RATE As Double = 15
Public Const LUN_RATE As Double = 14.511415


Type ASCOM_CAN_DOS
    CanFindHome As Boolean
    CanPark As Boolean
    CanPulseGuide As Boolean
    CanSetDeclinationRate As Boolean
    CanSetGuideRates As Boolean
    CanSetPark As Boolean
    CanSetPierSide As Boolean
    CanSetRightAscensionRate As Boolean
    CanSetTracking As Boolean
    CanSlew As Boolean
    CanSlewAltAz As Boolean
    CanSlewAltAzAsync As Boolean
    CanSlewAsync As Boolean
    CanSync As Boolean
    CanSyncAltAz As Boolean
    CanUnpark As Boolean
    CanMoveRAAxis As Boolean
    CanMoveDecAxis As Boolean
    RAMin As Double
    RAMax As Double
End Type

Type Speed_def
    deltaStep As Long
    type As Integer
End Type

Type Bookmark_def
    Position As Long
    enabled As Boolean
    Name As String
End Type

Type ASCOM_FILTERWHEEL
    FilterNames() As String
    FilterCount As Integer
    Description As String
    DriverInfo As String
    DriverVersion As String
    FocusOffsets() As Integer
    InterfaceVersion As Integer
    Name As String
    Position As Integer
End Type


Type ASCOM_FOCUSER
    Absolute As Boolean
    IsMoving As Boolean
    Link As Boolean
    MaxIncrement As Long
    MaxStep As Long
    Position As Long
    StepSize As Double
    TempComp As Boolean
    TempCompAvailable As Boolean
    Temperatrue As Double
    MoveIn As Boolean
    MoveOut As Boolean
    Speeds(4) As Speed_def
    CanHalt As Boolean
    CanReportPosition As Boolean
    CanReportStepSize As Boolean
    InterfaceVersion As Integer
    Temperature As Double
    CanReportTemp As Boolean
    Bookmarks(10) As Bookmark_def
    ActiveDelta As Long
    ActiveType As Integer
End Type


Type ASCOM_RATE
    min As Double
    max As Double
End Type

Type ASCOM_AXISRATES
    NumDecRates As Integer
    DecRates() As ASCOM_RATE
    NumRaRates As Integer
    RaRates() As ASCOM_RATE
End Type

Public ascomFunctions As ASCOM_CAN_DOS
Public m_scope As Object
Public m_focuser As Object
Public m_focuser2 As Object
Public m_filterwheel As Object
Public FocuserProps As ASCOM_FOCUSER
Public FilterWheelProps As ASCOM_FILTERWHEEL
Public ascomAxisRates As ASCOM_AXISRATES
Public ActiveFocuser As Integer
Dim TimerCount As Integer
Dim FailCount As Integer

Public Sub ascomGetFunctions()
Dim i As Integer
Dim rate As Object
Dim oRates As Object
   
    On Error Resume Next
    
    ascomFunctions.CanFindHome = False
    ascomFunctions.CanPark = False
    ascomFunctions.CanPulseGuide = False
    ascomFunctions.CanSetDeclinationRate = False
    ascomFunctions.CanSetGuideRates = False
    ascomFunctions.CanSetPark = False
    ascomFunctions.CanSetPierSide = False
    ascomFunctions.CanSetRightAscensionRate = False
    ascomFunctions.CanSetTracking = False
    ascomFunctions.CanSlew = False
    ascomFunctions.CanSlewAltAz = False
    ascomFunctions.CanSlewAltAzAsync = False
    ascomFunctions.CanSlewAsync = False
    ascomFunctions.CanSync = False
    ascomFunctions.CanSyncAltAz = False
    ascomFunctions.CanUnpark = False
    ascomFunctions.CanMoveRAAxis = False
    ascomFunctions.CanMoveDecAxis = False
    
    Open "ascom_mount_debug.txt" For Output As #1
    
    If Not m_scope Is Nothing Then
        If m_scope.Connected Then
        
            If Err <> 0 Then
                Print #1, "Connected: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
        
            ascomFunctions.CanFindHome = m_scope.CanFindHome
            If Err <> 0 Then
                Print #1, "CanFindHome: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If

            ascomFunctions.CanPark = m_scope.CanPark
            If Err <> 0 Then
                Print #1, "CanPark: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            
            ascomFunctions.CanPulseGuide = m_scope.CanPulseGuide
            If Err <> 0 Then
                Print #1, "CanPulseGuide: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSetDeclinationRate = m_scope.CanSetDeclinationRate
            If Err <> 0 Then
                Print #1, "CanSetDeclinationRate: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSetGuideRates = m_scope.CanSetGuideRates
            If Err <> 0 Then
                Print #1, "CanSetGuideRates: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSetPark = m_scope.CanSetPark
            If Err <> 0 Then
                Print #1, "CanSetPark: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSetPierSide = m_scope.CanSetPierSide
            If Err <> 0 Then
                Print #1, "CanSetPierSide: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSetRightAscensionRate = m_scope.CanSetRightAscensionRate
            If Err <> 0 Then
                Print #1, "CanSetRightAscensionRate: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSetTracking = m_scope.CanSetTracking
            If Err <> 0 Then
                Print #1, "CanSetTracking: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSlew = m_scope.CanSlew
            If Err <> 0 Then
                Print #1, "CanSlew: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSlewAltAz = m_scope.CanSlewAltAz
            If Err <> 0 Then
                Print #1, "CanSlewAltAz: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSlewAltAzAsync = m_scope.CanSlewAltAzAsync
            If Err <> 0 Then
                Print #1, "CanSlewAltAzAsync: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSlewAsync = m_scope.CanSlewAsync
            If Err <> 0 Then
                Print #1, "CanSlewAsync: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSync = m_scope.CanSync
            If Err <> 0 Then
                Print #1, "CanSync: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanSyncAltAz = m_scope.CanSyncAltAz
            If Err <> 0 Then
                Print #1, "CanSyncAltAz: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanUnpark = m_scope.CanUnpark
            If Err <> 0 Then
                Print #1, "CanUnpark: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanMoveRAAxis = m_scope.CanMoveAxis(0)
            If Err <> 0 Then
                Print #1, "CanMoveAxis(0): ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            ascomFunctions.CanMoveDecAxis = m_scope.CanMoveAxis(1)
            If Err <> 0 Then
                Print #1, "CanMoveAxis(0): ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
    
            On Error GoTo RARATE_ERR
            Set oRates = m_scope.AxisRates(0)    ' this is for the primary (Az) axis, use (1) for the secondary (Alt) axis
            ascomAxisRates.NumRaRates = oRates.Count
            ReDim ascomAxisRates.RaRates(0 To ascomAxisRates.NumRaRates - 1)
            i = 0
            For Each rate In oRates
                ascomAxisRates.RaRates(i).min = rate.Minimum
                ascomAxisRates.RaRates(i).max = rate.Maximum
                i = i + 1
            Next
            Set oRates = Nothing
            GoTo RARATE_CONT:
            
RARATE_ERR: ascomAxisRates.NumRaRates = 0
            Print #1, "AxisRates(0): ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            Err = 0
RARATE_CONT:
            On Error GoTo DECRATE_ERR
            Set oRates = m_scope.AxisRates(1)
            ascomAxisRates.NumDecRates = oRates.Count
            ReDim ascomAxisRates.DecRates(0 To ascomAxisRates.NumDecRates - 1)
            i = 0
            For Each rate In oRates
                ascomAxisRates.DecRates(i).min = rate.Minimum
                ascomAxisRates.DecRates(i).max = rate.Maximum
                i = i + 1
            Next
            Set oRates = Nothing
            GoTo DECRATE_CONT:

DECRATE_ERR:
            ascomAxisRates.NumDecRates = 0
            Print #1, "AxisRates(1): ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            Err = 0

DECRATE_CONT:
        Else
            Print #1, "Connected: Returned false!"
        End If
        
                    
    End If

    Close #1
End Sub


Public Function ascomConnect(id As String) As Boolean
    On Error Resume Next
    ascomConnect = False
    If id = "" Then
    Else
        Set m_scope = CreateObject(id)
        If Not m_scope Is Nothing Then
            m_scope.Connected = False
            m_scope.Connected = True
            If m_scope.Connected = True Then
                ascomGetFunctions
                ascomConnect = True
            Else
                m_scope = Nothing
            End If
        End If
    End If
End Function
Public Function ascomDisconnect()
    If Not m_scope Is Nothing Then
        m_scope.Connected = False
        Set m_scope = Nothing
    End If
End Function
Public Function ascomFocusConnect(id As String) As Boolean
    On Error Resume Next
    ascomFocusConnect = False
    Call ascomFocusClearProperties
    If id <> "" Then
        Set m_focuser = CreateObject(id)
        If Not m_focuser Is Nothing Then
        
            FocuserProps.InterfaceVersion = CInt(m_focuser.InterfaceVerison)
            If Err <> 0 Then
                FocuserProps.InterfaceVersion = 1
                Err = 0
            End If
        
            If FocuserProps.InterfaceVersion > 1 Then
                'lets try a connect - its not supported by V1 ASCOM spec but is from V2
                If m_focuser.Connect = False Then
                    m_focuser.Connect = True
                End If
            End If
        
            If m_focuser.Link = False Then
                m_focuser.Link = True
            End If
            Call ascomFocusGetProperties(0)
            
            ascomFocusConnect = True
            If FocuserProps.Absolute Then
                ' for a absolute focuser we really need to be able
                ' to read the current position!
                If Not FocuserProps.CanReportPosition Then
                    MsgBox ("Sorry Focuser is incompatible: Absolute requires position read!")
                End If
            Else
            End If
        End If
    End If
End Function
Public Function ascomFocus2Connect(id As String) As Boolean
    On Error Resume Next
    ascomFocus2Connect = False
    Call ascomFocusClearProperties
    If id <> "" Then
        Set m_focuser2 = CreateObject(id)
        If Not m_focuser2 Is Nothing Then
            
            FocuserProps.InterfaceVersion = CInt(m_focuser.InterfaceVerison)
            If Err <> 0 Then
                FocuserProps.InterfaceVersion = 1
                Err = 0
            End If
        
            If FocuserProps.InterfaceVersion > 1 Then
                'lets try a connect - its not supported by V1 ASCOM spec but is from V2
                If m_focuser.Connect = False Then
                    m_focuser.Connect = True
                End If
            End If
            
            If m_focuser2.Link = False Then
                m_focuser2.Link = True
            End If
            Call ascomFocusGetProperties(1)
            
            ascomFocus2Connect = True
            If FocuserProps.Absolute Then
                ' for a absolute focuser we really need to be able
                ' to read the current position!
                If Not FocuserProps.CanReportPosition Then
                    MsgBox ("Sorry Focuser is incompatible: Absolute requires position read!")
                End If
            Else
            End If
        End If
    End If
End Function
Public Sub ascomFocus2Disconnect()
    On Error Resume Next
    
    Call ascomFocusClearProperties
            
    If FocuserProps.InterfaceVersion > 1 Then
        'lets try a connect - its not supported by V1 ASCOM spec but is from V2
        If m_focuser.Connect = True Then
            m_focuser.Connect = False
        End If
    End If

    If m_focuser2.Link = True Then
        m_focuser2.Link = False
    End If

    If Not m_focuser2 Is Nothing Then
        Set m_focuser2 = Nothing
    End If
End Sub

Public Sub ascomFocusDisconnect()
    On Error Resume Next
    
    Call ascomFocusClearProperties
    
    If FocuserProps.InterfaceVersion > 1 Then
        'lets try a connect - its not supported by V1 ASCOM spec but is from V2
        If m_focuser.Connect = True Then
            m_focuser.Connect = False
        End If
    End If
    
    If m_focuser2.Link = True Then
        m_focuser2.Link = False
    End If
    
    If Not m_focuser Is Nothing Then
        Set m_focuser = Nothing
    End If
End Sub

Public Sub ascomFocusGetProperties(fid As Integer)
Dim id As String
    On Error Resume Next
    
    HC.Timer1.enabled = False
    
    If Not m_focuser Is Nothing Then
    
        Open "ascom_focuser_debug.txt" For Output As #1
        
        Select Case fid
            Case 1
                FocuserProps.Absolute = m_focuser2.Absolute
            Case Else
                FocuserProps.Absolute = m_focuser.Absolute
        End Select
        If Err <> 0 Then
            Print #1, "Read Absolute: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.Absolute = False
            Err = 0
        End If
        
        Select Case fid
            Case 1
                FocuserProps.IsMoving = m_focuser2.IsMoving
            Case Else
                FocuserProps.IsMoving = m_focuser.IsMoving
        End Select
        If Err <> 0 Then
            Print #1, "Read IsMoving: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.IsMoving = False
            Err = 0
        End If
        
        Select Case fid
            Case 1
                FocuserProps.Link = m_focuser2.Link
                If Err <> 0 Then
                    Print #1, "Read Link: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                    m_focuser2.Link = False
                    Err = 0
                End If
            Case Else
                FocuserProps.Link = m_focuser.Link
                If Err <> 0 Then
                    Print #1, "Read Link: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                    m_focuser.Link = False
                    Err = 0
                End If
        End Select
        
        Select Case fid
            Case 1
                FocuserProps.MaxIncrement = m_focuser2.MaxIncrement
            Case Else
                FocuserProps.MaxIncrement = m_focuser.MaxIncrement
        End Select
        If Err <> 0 Then
            Print #1, "Read MaxIncrement: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.MaxIncrement = 0
            Err = 0
        End If
        If FocuserProps.MaxIncrement <= 32767 Then
            FPropsDlg.VScroll1.max = FocuserProps.MaxIncrement
        Else
            FPropsDlg.VScroll1.max = 32767
        End If
        
        Select Case fid
            Case 1
                FocuserProps.MaxStep = m_focuser2.MaxStep
            Case Else
                FocuserProps.MaxStep = m_focuser.MaxStep
        End Select
        If Err <> 0 Then
            Print #1, "Read MaxStep: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.MaxStep = 0
            Err = 0
        End If
        
        Select Case fid
            Case 1
                FocuserProps.TempComp = m_focuser2.TempComp
            Case Else
                FocuserProps.TempComp = m_focuser.TempComp
        End Select
        If Err <> 0 Then
            Print #1, "Read TempComp: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.TempComp = False
            Err = 0
        End If
        
        Select Case fid
            Case 1
                FocuserProps.TempCompAvailable = m_focuser2.TempCompAvailable
            Case Else
                FocuserProps.TempCompAvailable = m_focuser.TempCompAvailable
        End Select
        If Err <> 0 Then
            Print #1, "Read TempCompAvailable: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.TempCompAvailable = False
            Err = 0
        End If
        
        FocuserProps.CanHalt = True
        Select Case fid
            Case 1
                m_focuser2.Halt
            Case Else
                m_focuser.Halt
        End Select
        If Err <> 0 Then
            Print #1, "Halt: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.CanHalt = False
            Err = 0
        End If
        
        FocuserProps.CanReportPosition = True
        HC.LabelPos.Visible = True
        Select Case fid
            Case 1
                FocuserProps.Position = m_focuser2.Position
            Case Else
                FocuserProps.Position = m_focuser.Position
        End Select
        If Err <> 0 Then
            Print #1, "Read Position: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.CanReportPosition = False
            FocuserProps.Position = 0
            HC.LabelPos.Visible = False
            Err = 0
        End If
        
        FocuserProps.CanReportStepSize = True
        Select Case fid
            Case 1
                FocuserProps.StepSize = m_focuser2.StepSize
            Case Else
                FocuserProps.StepSize = m_focuser.StepSize
        End Select
        If Err <> 0 Then
            Print #1, "Read StepSize: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.CanReportStepSize = False
            FocuserProps.StepSize = 0
            Err = 0
        End If
        
        FocuserProps.CanReportTemp = True
        Select Case fid
            Case 1
                FocuserProps.Temperature = m_focuser2.Temperature
            Case Else
                FocuserProps.Temperature = m_focuser.Temperature
        End Select
        If Err <> 0 Then
            Print #1, "Read Temperature: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            FocuserProps.CanReportTemp = False
            FocuserProps.Temperature = 0
            Err = 0
        End If
    
        Close #1
        
        HC.Timer1.enabled = True

    End If
End Sub



Public Sub ascomFocusClearProperties()
    FocuserProps.TempCompAvailable = False
    FocuserProps.StepSize = 0
    FocuserProps.Absolute = False
    FocuserProps.IsMoving = False
    FocuserProps.Link = False
    FocuserProps.MaxIncrement = 0
    FocuserProps.MaxStep = 0
    FocuserProps.TempComp = 0
    FocuserProps.TempCompAvailable = False
    FocuserProps.CanHalt = False
    FocuserProps.CanReportPosition = False
    FocuserProps.Position = 0
    FocuserProps.CanReportPosition = False
    FocuserProps.StepSize = 0
    FocuserProps.CanReportStepSize = False
    FocuserProps.CanReportTemp = False
End Sub

Public Sub AscomFocuserSetup(fid As Integer)
    On Error Resume Next
    
    Select Case fid
        Case 1
            If Not m_focuser2 Is Nothing Then
                m_focuser2.SetupDialog
            End If
        
        Case 0
            If Not m_focuser Is Nothing Then
                m_focuser.SetupDialog
            End If
    End Select
    
    ' Check properties in case anything ASCOM releated has changed
    Call ascomFocusGetProperties(fid)

End Sub


Public Sub AscomFocuserTimer()
Dim pos As Long
Dim Position As Long
On Error Resume Next
    
    If FocuserProps.Absolute <> m_focuser.Absolute Then
        ' focuser has change mode - best re-read all properties!
        ascomFocusGetProperties (ActiveFocuser)
    End If
    
    Err = 0
    If FocuserProps.CanReportPosition Then
        If TimerCount > 5 Then
            Position = m_focuser.Position
            If Err <> 0 Then
                Err = 0
                If FailCount >= 5 Then
                    ' Position read has failed 5 times in succession!
                    FocuserProps.CanReportPosition = False
                    HC.LabelPos.Visible = False
                Else
                    FailCount = FailCount + 1
                End If
            Else
                FailCount = 0
                FocuserProps.Position = Position
            End If
            HC.LabelPos.Caption = CStr(Position)
            HC.LabelPos.Visible = True
            TimerCount = 0
        Else
            TimerCount = TimerCount + 1
        End If
    End If

    If FocuserProps.MoveIn Then
        If Not m_focuser.IsMoving Then
            Select Case ActiveFocuser
                Case 1
                    If FocuserProps.Absolute Then
                        Position = m_focuser2.Position
                        If Position > 0 Then
                            pos = Position - FocuserProps.ActiveDelta
                            If pos < 0 Then pos = 0
                            m_focuser2.Move pos
                        End If
                    Else
                        pos = -1 * FocuserProps.ActiveDelta
                        m_focuser2.Move pos
                    End If
                Case Else
                    If FocuserProps.Absolute Then
                        Position = m_focuser.Position
                        If Position > 0 Then
                            pos = Position - FocuserProps.ActiveDelta
                            If pos < 0 Then pos = 0
                            m_focuser.Move pos
                        End If
                    Else
                        pos = -1 * FocuserProps.ActiveDelta
                        m_focuser.Move pos
                    End If
            End Select
        End If
    Else
        If FocuserProps.MoveOut Then
            Select Case ActiveFocuser
                Case 1
                    If Not m_focuser2.IsMoving Then
                        If FocuserProps.Absolute Then
                            Position = m_focuser2.Position
                            If Position < FocuserProps.MaxStep Then
                                pos = Position + FocuserProps.ActiveDelta
                                If pos > FocuserProps.MaxStep Then
                                    pos = FocuserProps.MaxStep
                                End If
                                m_focuser2.Move pos
                            End If
                        Else
                            pos = FocuserProps.ActiveDelta
                            m_focuser2.Move pos
                        End If
                    End If
                Case Else
                    If Not m_focuser.IsMoving Then
                        If FocuserProps.Absolute Then
                            Position = m_focuser.Position
                            If Position < FocuserProps.MaxStep Then
                                pos = Position + FocuserProps.ActiveDelta
                                If pos > FocuserProps.MaxStep Then
                                    pos = FocuserProps.MaxStep
                                End If
                                m_focuser.Move pos
                            End If
                        Else
                            pos = FocuserProps.ActiveDelta
                            m_focuser.Move pos
                        End If
                    End If
            End Select
        End If
    End If

End Sub


Public Sub ascomFocuserOut()
Dim pos As Long
On Error Resume Next
    FocuserProps.MoveOut = False
    Select Case ActiveFocuser
        Case 1
            If Not m_focuser2 Is Nothing Then
                
                If FocuserProps.ActiveType <> 0 Then
                    FocuserProps.MoveOut = True
                End If
                
                If FocuserProps.Absolute Then
                    pos = m_focuser2.Position
                    If pos < FocuserProps.MaxStep Then
                        pos = pos + FocuserProps.ActiveDelta
                        If pos > FocuserProps.MaxStep Then
                            pos = FocuserProps.MaxStep
                        End If
                        m_focuser2.Move pos
                    End If
                Else
                    pos = FocuserProps.ActiveDelta
                    m_focuser2.Move pos
                End If
            End If
        Case 0
            If Not m_focuser Is Nothing Then
                
                If FocuserProps.ActiveType <> 0 Then
                    FocuserProps.MoveOut = True
                End If
                
                If FocuserProps.Absolute Then
                    pos = m_focuser.Position
                    If pos < FocuserProps.MaxStep Then
                        pos = pos + FocuserProps.ActiveDelta
                        If pos > FocuserProps.MaxStep Then
                            pos = FocuserProps.MaxStep
                        End If
                        m_focuser.Move pos
                    End If
                Else
                    pos = FocuserProps.ActiveDelta
                    m_focuser.Move pos
                End If
            End If
    End Select
End Sub
Public Sub ascomFocuserStop()
On Error Resume Next
    Select Case ActiveFocuser
        Case 1
            If Not m_focuser2 Is Nothing Then
                If FocuserProps.CanHalt Then
                    m_focuser2.Halt
                End If
                FocuserProps.MoveIn = False
                FocuserProps.MoveOut = False
            End If
        Case 0
            If Not m_focuser Is Nothing Then
                If FocuserProps.CanHalt Then
                    m_focuser.Halt
                End If
                FocuserProps.MoveIn = False
                FocuserProps.MoveOut = False
            End If
    End Select
End Sub
Public Sub ascomFocuserIn()
Dim pos As Long
On Error Resume Next
    FocuserProps.MoveIn = False
    
    Select Case ActiveFocuser
    
        Case 1
            If Not m_focuser2 Is Nothing Then
                
                If FocuserProps.ActiveType <> 0 Then
                    FocuserProps.MoveIn = True
                End If
                
                If FocuserProps.Absolute Then
                    pos = m_focuser2.Position
                    If pos > 0 Then
                        pos = pos - FocuserProps.ActiveDelta
                        If pos < 0 Then pos = 0
                        m_focuser2.Move pos
                    End If
                Else
                    pos = -1 * FocuserProps.ActiveDelta
                    m_focuser2.Move pos
                End If
            End If
        
        Case 0
            If Not m_focuser Is Nothing Then
                
                If FocuserProps.ActiveType <> 0 Then
                    FocuserProps.MoveIn = True
                End If
                
                If FocuserProps.Absolute Then
                    pos = m_focuser.Position
                    If pos > 0 Then
                        pos = pos - FocuserProps.ActiveDelta
                        If pos < 0 Then pos = 0
                        m_focuser.Move pos
                    End If
                Else
                    pos = -1 * FocuserProps.ActiveDelta
                    m_focuser.Move pos
                End If
            End If
    End Select
End Sub
Public Sub ascomFocuserGoto(pos As Long)
On Error Resume Next
    Select Case ActiveFocuser
        Case 1
            If Not m_focuser2 Is Nothing Then
                If FocuserProps.Absolute Then
                    m_focuser2.Move pos
                End If
            End If
        Case 0
            If Not m_focuser Is Nothing Then
                If FocuserProps.Absolute Then
                    m_focuser.Move pos
                End If
            End If
    End Select
End Sub

Public Function ascomFilterWheelConnect(id As String) As Boolean
    On Error Resume Next
    ascomFilterWheelConnect = False
    Call ascomFilterwheelClearProperties
    If id <> "" Then
        Set m_filterwheel = CreateObject(id)
        If Not m_filterwheel Is Nothing Then
            If m_filterwheel.Connected = False Then
                m_filterwheel.Connected = True
            End If
            Call ascomFilterwheelGetProperties
            ' set an ivalid poaition to force update via timer
            FilterWheelProps.Position = -2
            ascomFilterWheelConnect = True
        End If
    End If
End Function

Public Sub ascomFilterWheelDisconnect()
    On Error Resume Next
    Call ascomFilterwheelClearProperties
    If Not m_filterwheel Is Nothing Then
        m_filterwheel.Connected = False
        m_filterwheel.Dispose
        Set m_filterwheel = Nothing
    End If
End Sub

Public Sub ascomFilterwheelGetProperties()
Dim id As String
Dim i As Integer
    On Error Resume Next
    If Not m_filterwheel Is Nothing Then
    
        Open "ascom_filterwheel_debug.txt" For Output As #1
        

        FilterWheelProps.InterfaceVersion = CInt(m_filterwheel.InterfaceVersion)
        If Err <> 0 Then
            m_filterwheel.InterfaceVersion = 1
            Err = 0
        End If
        
        If m_filterwheel.InterfaceVersion > 1 Then
            FilterWheelProps.Name = m_filterwheel.Name
            If Err <> 0 Then
                Print #1, "Read Description: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
                   
            FilterWheelProps.Description = m_filterwheel.Description
            If Err <> 0 Then
                Print #1, "Read Description: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
                   
            FilterWheelProps.DriverInfo = m_filterwheel.DriverInfo
            If Err <> 0 Then
                Print #1, "Read DriverInfo: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
            
            FilterWheelProps.DriverVersion = m_filterwheel.DriverVersion
            If Err <> 0 Then
                Print #1, "Read DriverVersion: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
                Err = 0
            End If
        End If
        
        FilterWheelProps.FilterCount = UBound(m_filterwheel.Names) + 1
        If Err <> 0 Then
            Print #1, "Read Names: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            Err = 0
        End If
        
        ReDim FilterWheelProps.FilterNames(0 To FilterWheelProps.FilterCount)
        ReDim FilterWheelProps.FocusOffsets(0 To FilterWheelProps.FilterCount)
                
        HC.Combo5.Clear
        FilterWheelProps.FilterNames = m_filterwheel.Names()
        If Err <> 0 Then
            Print #1, "Read Names: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            Err = 0
        Else
            For i = 0 To FilterWheelProps.FilterCount - 1
                HC.Combo5.AddItem (FilterWheelProps.FilterNames(i))
            Next i
        End If
                
        FilterWheelProps.FocusOffsets = m_filterwheel.FocusOffsets()
        If Err <> 0 Then
            Print #1, "Read FocusOffsets: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            Err = 0
        End If
        
        FilterWheelProps.Position = m_filterwheel.Position
        If Err <> 0 Then
            Print #1, "Read Posiiton: ErrorNo=" & CStr(Err.Number) & " " & Err.Description
            Err = 0
        End If
    
        Close #1
    End If
End Sub

Public Sub ascomFilterwheelClearProperties()
Dim id As String
    On Error Resume Next
    If Not m_filterwheel Is Nothing Then
    
'        Open "ascom_filterwheel_debug.txt" For Output As #1
'        Close #1
    End If
End Sub

Public Sub AscomFilterWheelTimer()
    Dim pos As Integer
    
    On Error Resume Next

    pos = m_filterwheel.Position
    If pos <> FilterWheelProps.Position Then
    FilterWheelProps.Position = pos
        If FilterWheelProps.Position = -1 Then
            HC.Label3 = "Moving"
        Else
            HC.Label3 = FilterWheelProps.FilterNames(pos)
            HC.Combo5.ListIndex = pos
            EQ_Beep 300 + pos
        End If
    End If
End Sub

Public Sub ascomFilterWheelSelect(pos As Integer)
    On Error Resume Next
    If Not m_filterwheel Is Nothing Then
        m_filterwheel.Position = pos
    End If
End Sub
