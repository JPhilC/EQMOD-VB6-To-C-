Attribute VB_Name = "Gamepad"

Option Explicit
Public gdwXpos As Long
Public gdwYpos As Long
Public gdwZpos As Long
Public gdwRpos As Long
Public gdwButtons As Long
Public gdwPov As Long

Public gEQjbuttons As Long

Public gPrevCode As Long

Public BTN_STARTSIDREAL As Long
Public BTN_EMERGENCYSTOP As Long
Public BTN_SPIRAL As Long
Public BTN_RARATEINC As Long
Public BTN_DECRATEINC As Long
Public BTN_RARATEDEC As Long
Public BTN_DECRATEDEC As Long
Public BTN_HOMEPARK As Long
Public BTN_USERPARK As Long
Public BTN_FOCUSIN As Long
Public BTN_FOCUSOUT As Long
Public BTN_ALIGNACCEPT As Long
Public BTN_ALIGNCANCEL As Long
Public BTN_ALIGNEND As Long
Public BTN_UNPARK As Long
Public BTN_EAST As Long
Public BTN_WEST As Long
Public BTN_NORTH As Long
Public BTN_SOUTH As Long
Public BTN_RAREVERSE As Long
Public BTN_DECREVERSE As Long
Public BTN_CUSTOMTRACKSTART As Long
Public BTN_CURRENTPARK As Long
Public BTN_STARTSOLAR As Long
Public BTN_STARTLUNAR As Long
Public BTN_INCRATEPRESET As Long
Public BTN_DECRATEPRESET As Long
Public BTN_RATE1 As Long
Public BTN_RATE2 As Long
Public BTN_RATE3 As Long
Public BTN_RATE4 As Long
Public BTN_PEC As Long
Public BTN_FSPEED1 As Long
Public BTN_FSPEED2 As Long
Public BTN_FSPEED3 As Long
Public BTN_FSPEED4 As Long
Public BTN_INCFSPEEDPRESET As Long
Public BTN_DECFSPEEDPRESET As Long
Public BTN_FOCUSLOCK As Long
Public BTN_FOCUSER1 As Long
Public BTN_FOCUSER2 As Long
Public BTN_FWINC As Long
Public BTN_FWDEC As Long
Public BTN_FW1 As Long
Public BTN_FW2 As Long
Public BTN_FW3 As Long
Public BTN_FW4 As Long
Public BTN_FW5 As Long
Public BTN_FW6 As Long

' default joystick button assignments
Public Const BTN_UNDEFINED = 0
Public Const BTN_JOY1 = 1
Public Const BTN_JOY2 = 2
Public Const BTN_JOY3 = 4
Public Const BTN_JOY4 = 8
Public Const BTN_JOY5 = 16
Public Const BTN_JOY6 = 32
Public Const BTN_JOY7 = 64
Public Const BTN_JOY8 = 128
Public Const BTN_JOY9 = 256
Public Const BTN_JOY10 = 512
Public Const BTN_JOY11 = 1024
Public Const BTN_JOY12 = 2048
Type JOYINFOEX
   dwSize As Long                      ' size of structure
   dwFlags As Long                     ' flags to indicate what to return
   dwXpos As Long                      ' x position
   dwYpos As Long                      ' y position
   dwZpos As Long                      ' z position
   dwRpos As Long                      ' rudder/4th axis position
   dwUpos As Long                      ' 5th axis position
   dwVpos As Long                      ' 6th axis position
   dwButtons As Long                   ' button states
   dwButtonNumber As Long              ' current button number pressed
   dwPOV As Long                       ' point of view state
   dwReserved1 As Long                 ' reserved for communication between winmm driver
   dwReserved2 As Long                 ' reserved for future expansion
End Type

Public Const MAX_JOYSTICKOEMVXDNAME = 260
Public Const MAXPNAMELEN = 32

' The JOYCAPS user-defined type contains information about the joystick capabilities
Type JOYCAPS
    wMid As Integer ' Manufacturer identifier of the device driver for the MIDI output device
    ' For a list of identifiers, see the Manufacturer Indentifier topic in the
    ' Multimedia Reference of the Platform SDK.
    
    wPid As Integer ' Product Identifier Product of the MIDI output device. For a list of
    ' product identifiers, see the Product Identifiers topic in the Multimedia
    ' Reference of the Platform SDK.
    szPname As String * MAXPNAMELEN ' Null-terminated string containing the joystick product name
    wXmin As Long ' Minimum X-coordinate.
    wXmax As Long ' Maximum X-coordinate.
    wYmin As Long ' Minimum Y-coordinate
    wYmax As Long ' Maximum Y-coordinate
    wZmin As Long ' Minimum Z-coordinate
    wZmax As Long ' Maximum Z-coordinate
    wNumButtons As Long ' Number of joystick buttons
    wPeriodMin As Long ' Smallest polling frequency supported when captured by the joySetCapture function.
    wPeriodMax As Long ' Largest polling frequency supported when captured by the joySetCapture function.
    wRmin As Long ' Minimum rudder value. The rudder is a fourth axis of movement.
    wRmax As Long ' Maximum rudder value. The rudder is a fourth axis of movement.
    wUmin As Long ' Minimum u-coordinate (fifth axis) values.
    wUmax As Long ' Maximum u-coordinate (fifth axis) values.
    wVmin As Long ' Minimum v-coordinate (sixth axis) values.
    wVmax As Long ' Maximum v-coordinate (sixth axis) values.
    wCaps As Long ' Joystick capabilities as defined by the following flags
    ' JOYCAPS_HASZ- Joystick has z-coordinate information.
    ' JOYCAPS_HASR- Joystick has rudder (fourth axis) information.
    ' JOYCAPS_HASU- Joystick has u-coordinate (fifth axis) information.
    ' JOYCAPS_HASV- Joystick has v-coordinate (sixth axis) information.
    ' JOYCAPS_HASPOV- Joystick has point-of-view information.
    ' JOYCAPS_POV4DIR- Joystick point-of-view supports discrete values (centered, forward, backward, left, and right).
    ' JOYCAPS_POVCTS Joystick point-of-view supports continuous degree bearings.
    wMaxAxes As Long ' Maximum number of axes supported by the joystick.
    wNumAxes As Long ' Number of axes currently in use by the joystick.
    wMaxButtons As Long ' Maximum number of buttons supported by the joystick.
    szRegKey As String * MAXPNAMELEN ' String containing the registry key for the joystick.
    szOEMVxD As String * MAX_JOYSTICKOEMVXDNAME ' OEM VxD in use
End Type

Type JOYCAPS1
  wMid As Integer
  wPid As Integer
  szPname As String * 32
  wXmin As Long
  wXmax As Long
  wYmin As Long
  wYmax As Long
  wZmin As Long
  wZmax As Long
  wNumButtons As Long
  wPeriodMin As Long
 wPeriodMax As Long
' wRmin As Long
' wRmax As Long
' wUmin As Long
' wUmax As Long
' wVmin As Long
' wVmax As Long
' wMaxAxes As Long
' wNumAxes As Long
' wMaxButtons As Long
' szRegKey As String * 32
' szOEMVxD As String * 240
End Type

Declare Function joyGetPosEx Lib "winmm.dll" (ByVal uJoyID As Long, pji As JOYINFOEX) As Long
Declare Function joyGetDevCaps Lib "winmm.dll" Alias "joyGetDevCapsA" (ByVal id As Long, lpCaps As JOYCAPS1, ByVal uSize As Long) As Long
Declare Function joyGetDevCaps2 Lib "winmm.dll" Alias "joyGetDevCapsA" (ByVal id As Long, lpCaps As JOYCAPS, ByVal uSize As Long) As Long

 Public Const JOYSTICKID1 = 0
   Public Const JOYSTICKID2 = 1
   Public Const JOY_RETURNBUTTONS = &H80&
   Public Const JOY_RETURNCENTERED = &H400&
   Public Const JOY_RETURNPOV = &H40&
   Public Const JOY_RETURNR = &H8&
   Public Const JOY_RETURNU = &H10
   Public Const JOY_RETURNV = &H20
   Public Const JOY_RETURNX = &H1&
   Public Const JOY_RETURNY = &H2&
   Public Const JOY_RETURNZ = &H4&
   Public Const JOY_RETURNALL = (JOY_RETURNX Or JOY_RETURNY Or JOY_RETURNZ Or JOY_RETURNR Or JOY_RETURNU Or JOY_RETURNV Or JOY_RETURNPOV Or JOY_RETURNBUTTONS)
   Public Const JOYCAPS_HASZ = &H1&
   Public Const JOYCAPS_HASR = &H2&
   Public Const JOYCAPS_HASU = &H4&
   Public Const JOYCAPS_HASV = &H8&
   Public Const JOYCAPS_HASPOV = &H10&
   Public Const JOYCAPS_POV4DIR = &H20&
   Public Const JOYCAPS_POVCTS = &H40&
   Public Const JOYERR_BASE = 160
   Public Const JOYERR_UNPLUGGED = (JOYERR_BASE + 7)

   Public Const JOYERR_NOCANDO = (JOYERR_BASE + 6)   ' Request Not Completed
   Public Const JOYERR_NOERROR = (0)                 ' No Error
   Public Const JOYERR_PARMS = (JOYERR_BASE + 5)     ' Bad Parameters

Type JOYCALIB
    dwMinXpos As Long
    dwMaxXpos As Long
    dwMinYpos As Long
    dwMaxYpos As Long
    dwMinZpos As Long
    dwMaxZpos As Long
    dwMinRpos As Long
    dwMaxRpos As Long
End Type

Public joyinfo As JOYCAPS1  ' receives joystick information
Public joyinfo2 As JOYCAPS  ' receives joystick information
Public JoystickDat As JOYINFOEX  ' receives joystick information
Public JoystickCal As JOYCALIB
Public rates(10) As Double
Public CustomRateRA As Double
Public CustomRateDEC As Double
Public currentrate As Double
Public ReverseRa As Boolean
Public ReverseDEC As Boolean
Public Tracking As Boolean

Public LHJStick As Integer
Public RHJStick As Integer
Public POV_Enable As Integer
Public FocusingEnabled As Integer

Public Function EQ_JoystickPoller() As Boolean
Dim i As Long
Dim dwXpos As Long
Dim dwYpos As Long
Dim dwZpos As Long
Dim dwRpos As Long
Dim dwButtons As Long
Dim dwPOV As Long


        JoystickDat.dwSize = Len(JoystickDat)
        JoystickDat.dwFlags = JOY_RETURNALL

        i = joyGetPosEx(JOYSTICKID1, JoystickDat)
        If i <> JOYERR_NOERROR Then i = joyGetPosEx(JOYSTICKID2, JoystickDat)
            If i <> JOYERR_NOERROR Then
                
                ' Joystick not found disable joystick scan
                
                EQ_JoystickPoller = False
                Exit Function
                
            End If
        
        ' Start Polling for Joytick routines here
        
        If i = JOYERR_NOERROR Then
        
            dwXpos = JoystickDat.dwXpos
            dwYpos = JoystickDat.dwYpos
            dwZpos = JoystickDat.dwZpos
            dwRpos = JoystickDat.dwRpos
            dwButtons = JoystickDat.dwButtons
            dwPOV = JoystickDat.dwPOV
        
            If LHJStick <> 1 Then
                ' Debouncing on X Axis
                If dwXpos <> gdwXpos Then
                
                    'Scan for Joystick Release here
                    If (gdwXpos <= JoystickCal.dwMinXpos) And (dwXpos > JoystickCal.dwMinXpos) Then Call West_Up
                    If (gdwXpos >= JoystickCal.dwMaxXpos) And (dwXpos < JoystickCal.dwMaxXpos) Then Call East_Up
                
                    ' Scan for Joystick Activate Here
                    If (dwXpos <= JoystickCal.dwMinXpos) And (gdwXpos > JoystickCal.dwMinXpos) Then Call West_Down
                    If (dwXpos >= JoystickCal.dwMaxXpos) And (gdwXpos < JoystickCal.dwMaxXpos) Then Call East_Down
                    
                    gdwXpos = dwXpos
            
                End If
            
                ' Debouncing on Y Axis
            
                If dwYpos <> gdwYpos Then
    
                    'Scan for Joystick Release here
                    If (gdwYpos <= JoystickCal.dwMinYpos) And (dwYpos > JoystickCal.dwMinYpos) Then Call North_Up
                    If (gdwYpos >= JoystickCal.dwMaxYpos) And (dwYpos < JoystickCal.dwMaxYpos) Then Call South_Up
                
                    ' Scan for Joystick Activate Here
                    If (dwYpos <= JoystickCal.dwMinYpos) And (gdwYpos > JoystickCal.dwMinYpos) Then Call North_Down
                    If (dwYpos >= JoystickCal.dwMaxYpos) And (gdwYpos < JoystickCal.dwMaxYpos) Then Call South_Down
                    
                    gdwYpos = dwYpos
            
                End If
            End If
            
            If FocusingEnabled = 1 Then
                If RHJStick <> 1 Then
                    ' Debouncing on R Axis
                    If dwRpos <> gdwRpos Then
                    
                        'Scan for Joystick Release here
                        If (gdwRpos <= JoystickCal.dwMinRpos) And (dwRpos > JoystickCal.dwMinRpos) Then Call RL_UP
                        If (gdwRpos >= JoystickCal.dwMaxRpos) And (dwRpos < JoystickCal.dwMaxRpos) Then Call RR_UP
                    
                        ' Scan for Joystick Activate Here
                        If (dwRpos <= JoystickCal.dwMinRpos) And (gdwRpos > JoystickCal.dwMinRpos) Then Call RL_Down
                        If (dwRpos >= JoystickCal.dwMaxRpos) And (gdwRpos < JoystickCal.dwMaxRpos) Then Call RR_Down
                        
                        gdwRpos = dwRpos
                
                    End If
                
                    ' Debouncing on Z Axis
                    If dwZpos <> gdwZpos Then
        
                        'Scan for Joystick Release here
                        If (gdwZpos <= JoystickCal.dwMinZpos) And (dwZpos > JoystickCal.dwMinZpos) Then Call ZU_UP
                        If (gdwZpos >= JoystickCal.dwMaxZpos) And (dwZpos < JoystickCal.dwMaxZpos) Then Call ZD_UP
                    
                        ' Scan for Joystick Activate Here
                        If (dwZpos <= JoystickCal.dwMinZpos) And (gdwZpos > JoystickCal.dwMinZpos) Then Call ZU_Down
                        If (dwZpos >= JoystickCal.dwMaxZpos) And (gdwZpos < JoystickCal.dwMaxZpos) Then Call ZD_Down
                        
                        gdwZpos = dwZpos
                
                    End If
                End If
            End If
            Call Buttonhandler(dwButtons, gdwButtons)
            
            If POV_Enable Then
                ' Debouncing on the POV Pads
                If dwPOV = 65535 Then
                    dwPOV = 0
                Else
                    dwPOV = dwPOV + 65536
                End If
                Call Buttonhandler(dwPOV, gdwPov)
            End If
                
        End If



End Function
Private Sub Buttonhandler(ByRef current As Long, ByRef last As Long)

    If current <> last Then
        If BTN_STARTSIDREAL <> BTN_UNDEFINED Then
            If (current) = BTN_STARTSIDREAL Then Call Start_sidereal
        End If
        If BTN_STARTLUNAR <> BTN_UNDEFINED Then
            If (current) = BTN_STARTLUNAR Then Call Start_Lunar
        End If
        If BTN_STARTSOLAR <> BTN_UNDEFINED Then
            If (current) = BTN_STARTSOLAR Then Call Start_Solar
        End If
        If BTN_HOMEPARK <> BTN_UNDEFINED Then
            If (current) = BTN_HOMEPARK Then Call Park
        End If
        If BTN_UNPARK <> BTN_UNDEFINED Then
            If (current) = BTN_UNPARK Then Call UnPark
        End If
        If BTN_RAREVERSE <> BTN_UNDEFINED Then
            If (current) = BTN_RAREVERSE Then Call RAReverse
        End If
        If BTN_DECREVERSE <> BTN_UNDEFINED Then
            If (current) = BTN_DECREVERSE Then Call DecReverse
        End If
        If BTN_RATE1 <> BTN_UNDEFINED Then
            If (current) = BTN_RATE1 Then Call SetRate(1)
        End If
        If BTN_RATE2 <> BTN_UNDEFINED Then
            If (current) = BTN_RATE2 Then Call SetRate(2)
        End If
        If BTN_RATE3 <> BTN_UNDEFINED Then
            If (current) = BTN_RATE3 Then Call SetRate(3)
        End If
        If BTN_RATE4 <> BTN_UNDEFINED Then
            If (current) = BTN_RATE4 Then Call SetRate(4)
        End If
        If BTN_INCRATEPRESET <> BTN_UNDEFINED Then
            If (current) = BTN_INCRATEPRESET Then Call ChangeRatePreset(1)
        End If
        If BTN_DECRATEPRESET <> BTN_UNDEFINED Then
            If (current) = BTN_DECRATEPRESET Then Call ChangeRatePreset(-1)
        End If
        
        If BTN_FWINC <> BTN_UNDEFINED Then
            If (current) = BTN_FWINC Then Call ChangeFilter(1)
        End If
        If BTN_FWDEC <> BTN_UNDEFINED Then
            If (current) = BTN_FWDEC Then Call ChangeFilter(-1)
        End If
        If BTN_FW1 <> BTN_UNDEFINED Then
            If (current) = BTN_FW1 Then Call SelectFilter(0)
        End If
        If BTN_FW2 <> BTN_UNDEFINED Then
            If (current) = BTN_FW2 Then Call SelectFilter(1)
        End If
        If BTN_FW3 <> BTN_UNDEFINED Then
            If (current) = BTN_FW3 Then Call SelectFilter(2)
        End If
        If BTN_FW4 <> BTN_UNDEFINED Then
            If (current) = BTN_FW4 Then Call SelectFilter(3)
        End If
        If BTN_FW5 <> BTN_UNDEFINED Then
            If (current) = BTN_FW5 Then Call SelectFilter(4)
        End If
        If BTN_FW6 <> BTN_UNDEFINED Then
            If (current) = BTN_FW6 Then Call SelectFilter(5)
        End If

        
        
        If (BTN_SOUTH <> BTN_UNDEFINED) Then
            If (last = BTN_SOUTH) Then Call South_Up
            If (current = BTN_SOUTH) Then Call South_Down
        End If
        If (BTN_EAST <> BTN_UNDEFINED) Then
            If (last = BTN_EAST) Then Call East_Up
            If (current = BTN_EAST) Then Call East_Down
        End If
        If (BTN_WEST <> BTN_UNDEFINED) Then
            If (last = BTN_WEST) Then Call West_Up
            If (current = BTN_WEST) Then Call West_Down
        End If
        If (BTN_NORTH <> BTN_UNDEFINED) Then
            If (last = BTN_NORTH) Then Call North_Up
            If (current = BTN_NORTH) Then Call North_Down
        End If
        
        If FocusingEnabled = 1 Then
            If (BTN_FOCUSIN <> BTN_UNDEFINED) Then
                If (last = BTN_FOCUSIN) Then Call ascomFocuserStop
                If (current = BTN_FOCUSIN) Then
                    Call ascomFocuserIn
                    EQ_Beep (214)
                End If
            End If
            If (BTN_FOCUSOUT <> BTN_UNDEFINED) Then
                If (last = BTN_FOCUSOUT) Then Call ascomFocuserStop
                If (current = BTN_FOCUSOUT) Then
                    Call ascomFocuserOut
                    EQ_Beep (215)
                End If
            End If
            If BTN_FSPEED1 <> BTN_UNDEFINED Then
                If (current) = BTN_FSPEED1 Then Call SetFSpeed(1)
            End If
            If BTN_FSPEED2 <> BTN_UNDEFINED Then
                If (current) = BTN_FSPEED2 Then Call SetFSpeed(2)
            End If
            If BTN_FSPEED3 <> BTN_UNDEFINED Then
                If (current) = BTN_FSPEED3 Then Call SetFSpeed(3)
            End If
            If BTN_FSPEED4 <> BTN_UNDEFINED Then
                If (current) = BTN_FSPEED4 Then Call SetFSpeed(4)
            End If
            If BTN_INCFSPEEDPRESET <> BTN_UNDEFINED Then
                If (current) = BTN_INCFSPEEDPRESET Then Call ChangeFSpeedPreset(1)
            End If
            If BTN_DECFSPEEDPRESET <> BTN_UNDEFINED Then
                If (current) = BTN_DECFSPEEDPRESET Then Call ChangeFSpeedPreset(-1)
            End If
        End If
    
        If BTN_FOCUSLOCK <> BTN_UNDEFINED Then
            If (current) = BTN_FOCUSLOCK Then Call ToggleFocusLock
        End If
        
        If BTN_FOCUSER1 <> BTN_UNDEFINED Then
            If (current) = BTN_FOCUSER1 Then Call SelectFocuser(0)
        End If
        
        If BTN_FOCUSER2 <> BTN_UNDEFINED Then
            If (current) = BTN_FOCUSER2 Then Call SelectFocuser(1)
        End If
    
        ' keep this last so it has overall priority!
        If BTN_EMERGENCYSTOP <> BTN_UNDEFINED Then
            If (current) = BTN_EMERGENCYSTOP Then Call emergency_stop
        End If
    
    End If
    last = current
End Sub

Public Sub SelectFocuser(val As Integer)
    HC.Combo3.ListIndex = val
    If val > 0 Then
        EQ_Beep (213)
    Else
        EQ_Beep (212)
    End If
End Sub
Public Sub ToggleFocusLock()
    If HC.gamepadlock.Value = 0 Then
        HC.gamepadlock.Value = 1
        HC.Command9.Picture = LoadResPicture(108, vbResBitmap)
        EQ_Beep (210)
    Else
        HC.gamepadlock.Value = 0
        HC.Command9.Picture = LoadResPicture(107, vbResBitmap)
        EQ_Beep (211)
    End If
End Sub

Public Sub RL_Down()
    ChangeFSpeedPreset (-1)
End Sub
Public Sub RL_UP()

End Sub
Public Sub RR_Down()
    ChangeFSpeedPreset (1)
End Sub
Public Sub RR_UP()

End Sub

Public Sub ZU_Down()
    ascomFocuserIn
    EQ_Beep (214)
End Sub
Public Sub ZU_UP()
    ascomFocuserStop
End Sub
Public Sub ZD_Down()
    ascomFocuserOut
    EQ_Beep (215)
End Sub
Public Sub ZD_UP()
    ascomFocuserStop
End Sub

Public Sub South_Down()
    On Error Resume Next
    
    Tracking = m_scope.Tracking
    If ascomFunctions.CanMoveDecAxis Then
        If ReverseDEC Then
            m_scope.MoveAxis 1, currentrate
        Else
            m_scope.MoveAxis 1, -1 * currentrate
        End If
    End If
End Sub

Public Sub South_Up()
    On Error Resume Next
    If ascomFunctions.CanMoveDecAxis Then
        m_scope.MoveAxis 1, 0
        m_scope.Tracking = Tracking
    End If
End Sub

Public Sub North_Down()
    On Error Resume Next
    Tracking = m_scope.Tracking
    If ascomFunctions.CanMoveDecAxis Then
        If ReverseDEC Then
            m_scope.MoveAxis 1, -1 * currentrate
        Else
            m_scope.MoveAxis 1, currentrate
        End If
    End If
End Sub

Public Sub North_Up()
    On Error Resume Next
    If ascomFunctions.CanMoveDecAxis Then
        m_scope.MoveAxis 1, 0
        m_scope.Tracking = Tracking
    End If
End Sub

Public Sub East_Down()
    On Error Resume Next
    Tracking = m_scope.Tracking
    If ascomFunctions.CanMoveRAAxis Then
        If ReverseRa Then
            m_scope.MoveAxis 0, -1 * currentrate
        Else
            m_scope.MoveAxis 0, currentrate
        End If
    End If
End Sub

Public Sub East_Up()
    On Error Resume Next
    If ascomFunctions.CanMoveRAAxis Then
        m_scope.MoveAxis 0, 0
        m_scope.Tracking = Tracking
    End If
End Sub

Public Sub West_Down()
    On Error Resume Next
    Tracking = m_scope.Tracking
    If ascomFunctions.CanMoveRAAxis Then
        If ReverseRa Then
            m_scope.MoveAxis 0, currentrate
        Else
            m_scope.MoveAxis 0, -1 * currentrate
        End If
    End If
End Sub

Public Sub West_Up()
    On Error Resume Next
    If ascomFunctions.CanMoveRAAxis Then
        m_scope.MoveAxis 0, 0
        m_scope.Tracking = Tracking
    End If
End Sub

Public Sub NorthEast_Down()
    North_Down
    East_Down
End Sub

Public Sub NorthWest_Down()
    North_Down
    West_Down
End Sub

Public Sub SouthEast_Down()
    South_Down
    East_Down
End Sub

Public Sub SouthWest_Down()
    South_Down
    West_Down
End Sub

Public Sub NorthEast_Up()
    North_Up
    East_Up
End Sub

Public Sub NorthWest_Up()
    North_Up
    West_Up
End Sub

Public Sub SouthEast_Up()
    South_Up
    East_Up
End Sub

Public Sub SouthWest_Up()
    South_Up
    West_Up
End Sub

Public Sub emergency_stop()
    On Error Resume Next
    m_scope.AbortSlew
    If ascomFunctions.CanSetTracking Then
        m_scope.Tracking = False
        EQ_Beep (7)
    End If
End Sub

Public Sub Start_sidereal()
    On Error Resume Next
    If ascomFunctions.CanSetTracking Then
        m_scope.Tracking = False
        If ascomFunctions.CanSetDeclinationRate And ascomFunctions.CanSetRightAscensionRate Then
            m_scope.RightAscensionRate = 0
            m_scope.DeclinationRate = 0
        End If
        m_scope.TrackingRate = 0
        If Err.Number <> 0 Then Err.Clear
        m_scope.Tracking = True
        EQ_Beep (10)
    End If
End Sub

Public Sub Start_Lunar()
    
    On Error Resume Next
    If ascomFunctions.CanSetTracking Then
        m_scope.Tracking = False
        m_scope.TrackingRate = 1
        If Err.Number = 0 Then
            m_scope.Tracking = True
        Else
            ' Try using a custom tracking rate instead
            Err.Clear
            If ascomFunctions.CanSetDeclinationRate And ascomFunctions.CanSetRightAscensionRate Then
                m_scope.RightAscensionRate = (LUN_RATE - SID_RATE)
                m_scope.DeclinationRate = (0)
                m_scope.Tracking = True
                EQ_Beep (11)
            End If
        End If
    End If

End Sub

Public Sub Start_Solar()
    
    On Error Resume Next
    If ascomFunctions.CanSetTracking Then
        m_scope.Tracking = False
        m_scope.TrackingRate = 2
        If Err.Number = 0 Then
            m_scope.Tracking = True
        Else
            Err.Clear
            ' Try using a custom tracking instead
            If ascomFunctions.CanSetDeclinationRate And ascomFunctions.CanSetRightAscensionRate Then
                m_scope.RightAscensionRate = (SOL_RATE - SID_RATE)
                m_scope.DeclinationRate = (0)
                m_scope.Tracking = True
                EQ_Beep (12)
            End If
        End If
    End If
End Sub
Public Sub Start_Custom()
    On Error GoTo endsub
    If ascomFunctions.CanSetTracking Then
        If ascomFunctions.CanSetDeclinationRate And ascomFunctions.CanSetRightAscensionRate Then
            m_scope.Tracking = False
            m_scope.RightAscensionRate = (CustomRateRA - SID_RATE)
            m_scope.DeclinationRate = CustomRateDEC
            m_scope.Tracking = True
            EQ_Beep (13)
        End If
    End If
endsub:
End Sub

Public Sub Adjust_rate(axis As Integer, direction As Integer)
Dim i As Integer
Dim j As Integer

End Sub


Public Sub Park()
    On Error Resume Next
    If ascomFunctions.CanPark Then
        m_scope.Park
        EQ_Beep (8)
    End If
End Sub

Public Sub UnPark()
    On Error Resume Next
    If ascomFunctions.CanUnpark Then
        m_scope.UnPark
        EQ_Beep (9)
    End If
End Sub

Public Sub RAReverse()
    If ReverseRa Then
        HC.Check1(0).Value = 0
    Else
        HC.Check1(0).Value = 1
    End If
End Sub

Public Sub DecReverse()
    If ReverseDEC Then
        HC.Check1(1).Value = 0
    Else
        HC.Check1(1).Value = 1
    End If
End Sub

Public Function SetRate(rate As Integer)
    HC.Combo1.ListIndex = rate - 1
    EQ_Beep (100 + rate)
End Function
Public Function SetFSpeed(rate As Integer)
    HC.Combo2.ListIndex = rate - 1
    EQ_Beep (200 + rate)
End Function

Public Function SelectFilter(idx As Integer)
    HC.Combo5.ListIndex = idx
End Function

Public Function ChangeFilter(Shift As Integer)
Dim pos As Integer
    
    pos = HC.Combo5.ListIndex + Shift

    If pos > HC.Combo5.ListCount - 1 Then
        ' no more preset in this direction
        pos = 0
    Else
        If pos < 0 Then
            ' no more preset in this direction
            pos = HC.Combo5.ListCount - 1
        End If
        
    End If
   
    HC.Combo5.ListIndex = pos
End Function


Public Function ChangeRatePreset(Shift As Integer)
    
Dim currentPreset As Integer
    
    currentPreset = HC.Combo1.ListIndex + Shift

    If currentPreset > HC.Combo1.ListCount - 1 Then
        ' no more preset in this direction
        currentPreset = HC.Combo1.ListCount
    Else
        If currentPreset < 0 Then
            ' no more preset in this direction
            currentPreset = 0
        End If
    End If
    
    EQ_Beep (101 + currentPreset)
    
    If rates(currentPreset) <> 0 Then
        HC.Combo1.ListIndex = currentPreset
    End If

End Function
Public Function ChangeFSpeedPreset(Shift As Integer)
    
Dim currentPreset As Integer
    
    currentPreset = HC.Combo2.ListIndex + Shift

    If currentPreset > HC.Combo2.ListCount - 1 Then
    Else
        If currentPreset < 0 Then
            ' no more preset in this direction
            HC.Combo2.ListIndex = 0
        Else
            HC.Combo2.ListIndex = currentPreset
        End If
    End If
    
    EQ_Beep (201 + HC.Combo2.ListIndex)

End Function


Public Sub LoadJoystickBtns()

Dim tmptxt As String
Dim VarStr As String
Dim key As String
Dim Ini As String

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"

    key = "[buttondefs]"

    tmptxt = HC.oPersist.ReadIniValueEx("StartSidreal", key, Ini)
    If tmptxt <> "" Then
        BTN_STARTSIDREAL = val(tmptxt)
    Else
        tmptxt = CStr(BTN_JOY10)
        Call HC.oPersist.WriteIniValueEx("StartSidreal", tmptxt, key, Ini)
        BTN_STARTSIDREAL = BTN_JOY10
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("StartPEC", key, Ini)
    If tmptxt <> "" Then
        BTN_PEC = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("StartPEC", tmptxt, key, Ini)
        BTN_STARTSIDREAL = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("StartCustom", key, Ini)
    If tmptxt <> "" Then
        BTN_CUSTOMTRACKSTART = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("StartCustom", tmptxt, key, Ini)
        BTN_CUSTOMTRACKSTART = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("StartLunar", key, Ini)
    If tmptxt <> "" Then
        BTN_STARTLUNAR = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("StartLunar", tmptxt, key, Ini)
        BTN_STARTLUNAR = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("StartSolar", key, Ini)
    If tmptxt <> "" Then
        BTN_STARTSOLAR = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("StartSolar", tmptxt, key, Ini)
        BTN_STARTSOLAR = BTN_UNDEFINED
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("SpiralSearch", key, Ini)
    If tmptxt <> "" Then
        BTN_SPIRAL = val(tmptxt)
    Else
        tmptxt = CStr(BTN_JOY1)
        Call HC.oPersist.WriteIniValueEx("SpiralSearch", tmptxt, key, Ini)
        BTN_SPIRAL = BTN_JOY1
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("EmergencyStop", key, Ini)
    If tmptxt <> "" Then
        BTN_EMERGENCYSTOP = val(tmptxt)
    Else
        tmptxt = CStr(BTN_JOY11)
        Call HC.oPersist.WriteIniValueEx("EmergencyStop", tmptxt, key, Ini)
        BTN_EMERGENCYSTOP = BTN_JOY11
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("RARateInc", key, Ini)
    If tmptxt <> "" Then
        BTN_RARATEINC = val(tmptxt)
    Else
        tmptxt = CStr(BTN_JOY5)
        Call HC.oPersist.WriteIniValueEx("RARateInc", tmptxt, key, Ini)
        BTN_RARATEINC = BTN_JOY5
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("RARateDec", key, Ini)
    If tmptxt <> "" Then
        BTN_RARATEDEC = val(tmptxt)
    Else
        tmptxt = CStr(BTN_JOY7)
        Call HC.oPersist.WriteIniValueEx("RARateDec", tmptxt, key, Ini)
        BTN_RARATEDEC = BTN_JOY7
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("DecRateInc", key, Ini)
    If tmptxt <> "" Then
        BTN_DECRATEINC = val(tmptxt)
    Else
        tmptxt = CStr(BTN_JOY6)
        Call HC.oPersist.WriteIniValueEx("DecRateInc", tmptxt, key, Ini)
        BTN_DECRATEINC = BTN_JOY6
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("DecRateDec", key, Ini)
    If tmptxt <> "" Then
        BTN_DECRATEDEC = val(tmptxt)
    Else
        tmptxt = CStr(BTN_JOY8)
        Call HC.oPersist.WriteIniValueEx("DecRateDec", tmptxt, key, Ini)
        BTN_DECRATEDEC = BTN_JOY8
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("ParkHome", key, Ini)
    If tmptxt <> "" Then
        BTN_HOMEPARK = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("ParkHome", tmptxt, key, Ini)
        BTN_HOMEPARK = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("ParkUser", key, Ini)
    If tmptxt <> "" Then
        BTN_USERPARK = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("ParkUser", tmptxt, key, Ini)
        BTN_USERPARK = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("UnPark", key, Ini)
    If tmptxt <> "" Then
        BTN_UNPARK = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("UnPark", tmptxt, key, Ini)
        BTN_UNPARK = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("FocusIn", key, Ini)
    If tmptxt <> "" Then
        BTN_FOCUSIN = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("FocusIn", tmptxt, key, Ini)
        BTN_FOCUSIN = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("FocusOut", key, Ini)
    If tmptxt <> "" Then
        BTN_FOCUSOUT = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("FocusOut", tmptxt, key, Ini)
        BTN_FOCUSIN = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("AlignEnd", key, Ini)
    If tmptxt <> "" Then
        BTN_ALIGNEND = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("AlignEnd", tmptxt, key, Ini)
        BTN_ALIGNEND = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("East", key, Ini)
    If tmptxt <> "" Then
        BTN_EAST = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("East", tmptxt, key, Ini)
        BTN_EAST = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("West", key, Ini)
    If tmptxt <> "" Then
        BTN_WEST = val(tmptxt)
    Else
        tmptxt = CStr(BTN_WEST)
        Call HC.oPersist.WriteIniValueEx("West", tmptxt, key, Ini)
        BTN_WEST = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("North", key, Ini)
    If tmptxt <> "" Then
        BTN_NORTH = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("North", tmptxt, key, Ini)
        BTN_NORTH = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("South", key, Ini)
    If tmptxt <> "" Then
        BTN_SOUTH = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("South", tmptxt, key, Ini)
        BTN_SOUTH = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("ReverseRA", key, Ini)
    If tmptxt <> "" Then
        BTN_RAREVERSE = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("ReverseRA", tmptxt, key, Ini)
        BTN_RAREVERSE = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("ReverseDec", key, Ini)
    If tmptxt <> "" Then
        BTN_DECREVERSE = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("ReverseDec", tmptxt, key, Ini)
        BTN_DECREVERSE = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("IncRatePreset", key, Ini)
    If tmptxt <> "" Then
        BTN_INCRATEPRESET = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("IncRatePreset", tmptxt, key, Ini)
        BTN_INCRATEPRESET = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("DecRatePreset", key, Ini)
    If tmptxt <> "" Then
        BTN_DECRATEPRESET = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("DecRatePreset", tmptxt, key, Ini)
        BTN_DECRATEPRESET = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("Rate1", key, Ini)
    If tmptxt <> "" Then
        BTN_RATE1 = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("Rate1", CStr(BTN_UNDEFINED), key, Ini)
        BTN_RATE1 = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("Rate2", key, Ini)
    If tmptxt <> "" Then
        BTN_RATE2 = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("Rate2", CStr(BTN_UNDEFINED), key, Ini)
        BTN_RATE2 = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("Rate3", key, Ini)
    If tmptxt <> "" Then
        BTN_RATE3 = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("Rate3", CStr(BTN_UNDEFINED), key, Ini)
        BTN_RATE3 = BTN_UNDEFINED
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("Rate4", key, Ini)
    If tmptxt <> "" Then
        BTN_RATE4 = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("Rate4", CStr(BTN_UNDEFINED), key, Ini)
        BTN_RATE4 = BTN_UNDEFINED
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("FSpeed1", key, Ini)
    If tmptxt <> "" Then
        BTN_FSPEED1 = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("FSpeed1", CStr(BTN_UNDEFINED), key, Ini)
        BTN_FSPEED1 = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("FSpeed2", key, Ini)
    If tmptxt <> "" Then
        BTN_FSPEED2 = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("FSpeed2", CStr(BTN_UNDEFINED), key, Ini)
        BTN_FSPEED2 = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("FSpeed3", key, Ini)
    If tmptxt <> "" Then
        BTN_FSPEED3 = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("FSpeed3", CStr(BTN_UNDEFINED), key, Ini)
        BTN_FSPEED3 = BTN_UNDEFINED
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("FSpeed4", key, Ini)
    If tmptxt <> "" Then
        BTN_FSPEED4 = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("FSpeed4", CStr(BTN_UNDEFINED), key, Ini)
        BTN_FSPEED4 = BTN_UNDEFINED
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("IncFSpeedPreset", key, Ini)
    If tmptxt <> "" Then
        BTN_INCFSPEEDPRESET = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("IncFSpeedPreset", tmptxt, key, Ini)
        BTN_INCFSPEEDPRESET = BTN_UNDEFINED
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("DecFSpeedPreset", key, Ini)
    If tmptxt <> "" Then
        BTN_DECFSPEEDPRESET = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("DecFSpeedPreset", tmptxt, key, Ini)
        BTN_DECFSPEEDPRESET = BTN_UNDEFINED
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("FocusLock", key, Ini)
    If tmptxt <> "" Then
        BTN_FOCUSLOCK = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("FocusLock", tmptxt, key, Ini)
        BTN_FOCUSLOCK = BTN_UNDEFINED
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("Focuser1", key, Ini)
    If tmptxt <> "" Then
        BTN_FOCUSER1 = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("Focuser1", tmptxt, key, Ini)
        BTN_FOCUSER1 = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("Focuser2", key, Ini)
    If tmptxt <> "" Then
        BTN_FOCUSER2 = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("Focuser2", tmptxt, key, Ini)
        BTN_FOCUSER2 = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("FilterInc", key, Ini)
    If tmptxt <> "" Then
        BTN_FWINC = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("FilterInc", tmptxt, key, Ini)
        BTN_FWINC = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("FilterDec", key, Ini)
    If tmptxt <> "" Then
        BTN_FWDEC = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("FilterDec", tmptxt, key, Ini)
        BTN_FWDEC = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("Filter1", key, Ini)
    If tmptxt <> "" Then
        BTN_FW1 = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("Filter1", tmptxt, key, Ini)
        BTN_FW1 = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("Filter2", key, Ini)
    If tmptxt <> "" Then
        BTN_FW2 = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("Filter2", tmptxt, key, Ini)
        BTN_FW2 = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("Filter3", key, Ini)
    If tmptxt <> "" Then
        BTN_FW3 = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("Filter3", tmptxt, key, Ini)
        BTN_FW3 = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("Filter4", key, Ini)
    If tmptxt <> "" Then
        BTN_FW4 = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("Filter4", tmptxt, key, Ini)
        BTN_FW4 = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("Filter5", key, Ini)
    If tmptxt <> "" Then
        BTN_FW5 = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("Filter5", tmptxt, key, Ini)
        BTN_FW5 = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("Filter6", key, Ini)
    If tmptxt <> "" Then
        BTN_FW6 = val(tmptxt)
    Else
        tmptxt = CStr(BTN_UNDEFINED)
        Call HC.oPersist.WriteIniValueEx("Filter6", tmptxt, key, Ini)
        BTN_FW6 = BTN_UNDEFINED
    End If
   
    tmptxt = HC.oPersist.ReadIniValueEx("JStickL", key, Ini)
    If tmptxt <> "" Then
        LHJStick = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("JStickL", "0", key, Ini)
        LHJStick = 0
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("JStickR", key, Ini)
    If tmptxt <> "" Then
        RHJStick = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("JStickR", "1", key, Ini)
        RHJStick = 1
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("POV_Enable", key, Ini)
    If tmptxt <> "" Then
        POV_Enable = val(tmptxt)
    Else
        Call HC.oPersist.WriteIniValueEx("POV_Enable", "1", key, Ini)
        POV_Enable = 1
    End If

'    tmptxt = HC.oPersist.ReadIniValueEx("Focusing_Enable", key, Ini)
'    If tmptxt <> "" Then
'        FocusingEnabled = val(tmptxt)
'        If FocusingEnabled <> 0 Then
'            FocusingEnabled = 1
'       End If
'    Else
'        Call HC.oPersist.WriteIniValueEx("Focusing_Enable", "1", key, Ini)
'        FocusingEnabled = 1
'    End If


End Sub

Public Sub SaveJoystickBtns()
Dim key As String
Dim Ini As String

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"

    key = "[buttondefs]"

    Call HC.oPersist.WriteIniValueEx("StartSidreal", CStr(BTN_STARTSIDREAL), key, Ini)
    Call HC.oPersist.WriteIniValueEx("StartPEC", CStr(BTN_PEC), key, Ini)
    Call HC.oPersist.WriteIniValueEx("SpiralSearch", CStr(BTN_SPIRAL), key, Ini)
    Call HC.oPersist.WriteIniValueEx("EmergencyStop", CStr(BTN_EMERGENCYSTOP), key, Ini)
    Call HC.oPersist.WriteIniValueEx("RARateInc", CStr(BTN_RARATEINC), key, Ini)
    Call HC.oPersist.WriteIniValueEx("RARateDec", CStr(BTN_RARATEDEC), key, Ini)
    Call HC.oPersist.WriteIniValueEx("DecRateInc", CStr(BTN_DECRATEINC), key, Ini)
    Call HC.oPersist.WriteIniValueEx("DecRateDec", CStr(BTN_DECRATEDEC), key, Ini)
    Call HC.oPersist.WriteIniValueEx("ParkHome", CStr(BTN_HOMEPARK), key, Ini)
    Call HC.oPersist.WriteIniValueEx("ParkUser", CStr(BTN_USERPARK), key, Ini)
    Call HC.oPersist.WriteIniValueEx("ParkCurrent", CStr(BTN_CURRENTPARK), key, Ini)
    Call HC.oPersist.WriteIniValueEx("UnPark", CStr(BTN_UNPARK), key, Ini)
    Call HC.oPersist.WriteIniValueEx("FocusIn", CStr(BTN_FOCUSIN), key, Ini)
    Call HC.oPersist.WriteIniValueEx("FocusOut", CStr(BTN_FOCUSOUT), key, Ini)
    Call HC.oPersist.WriteIniValueEx("AlignEnd", CStr(BTN_ALIGNEND), key, Ini)
    Call HC.oPersist.WriteIniValueEx("East", CStr(BTN_EAST), key, Ini)
    Call HC.oPersist.WriteIniValueEx("West", CStr(BTN_WEST), key, Ini)
    Call HC.oPersist.WriteIniValueEx("North", CStr(BTN_NORTH), key, Ini)
    Call HC.oPersist.WriteIniValueEx("South", CStr(BTN_SOUTH), key, Ini)
    Call HC.oPersist.WriteIniValueEx("ReverseRA", CStr(BTN_RAREVERSE), key, Ini)
    Call HC.oPersist.WriteIniValueEx("ReverseDec", CStr(BTN_DECREVERSE), key, Ini)
    Call HC.oPersist.WriteIniValueEx("StartCustom", CStr(BTN_CUSTOMTRACKSTART), key, Ini)
    Call HC.oPersist.WriteIniValueEx("StartLunar", CStr(BTN_STARTLUNAR), key, Ini)
    Call HC.oPersist.WriteIniValueEx("StartSolar", CStr(BTN_STARTSOLAR), key, Ini)
    Call HC.oPersist.WriteIniValueEx("IncRatePreset", CStr(BTN_INCRATEPRESET), key, Ini)
    Call HC.oPersist.WriteIniValueEx("DecRatePreset", CStr(BTN_DECRATEPRESET), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Rate1", CStr(BTN_RATE1), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Rate2", CStr(BTN_RATE2), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Rate3", CStr(BTN_RATE3), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Rate4", CStr(BTN_RATE4), key, Ini)

    Call HC.oPersist.WriteIniValueEx("FSpeed1", CStr(BTN_FSPEED1), key, Ini)
    Call HC.oPersist.WriteIniValueEx("FSpeed2", CStr(BTN_FSPEED2), key, Ini)
    Call HC.oPersist.WriteIniValueEx("FSpeed3", CStr(BTN_FSPEED3), key, Ini)
    Call HC.oPersist.WriteIniValueEx("FSpeed4", CStr(BTN_FSPEED4), key, Ini)
    Call HC.oPersist.WriteIniValueEx("IncFSpeedPreset", CStr(BTN_INCFSPEEDPRESET), key, Ini)
    Call HC.oPersist.WriteIniValueEx("DecFSpeedPreset", CStr(BTN_DECFSPEEDPRESET), key, Ini)
    Call HC.oPersist.WriteIniValueEx("FocusLock", CStr(BTN_FOCUSLOCK), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Focuser1", CStr(BTN_FOCUSER1), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Focuser2", CStr(BTN_FOCUSER2), key, Ini)

    Call HC.oPersist.WriteIniValueEx("FilterInc", CStr(BTN_FWINC), key, Ini)
    Call HC.oPersist.WriteIniValueEx("FilterDec", CStr(BTN_FWDEC), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Filter1", CStr(BTN_FW1), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Filter2", CStr(BTN_FW2), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Filter3", CStr(BTN_FW3), key, Ini)
    Call HC.oPersist.WriteIniValueEx("Filter4", CStr(BTN_FW4), key, Ini)

    Call HC.oPersist.WriteIniValueEx("JStickL", CStr(LHJStick), key, Ini)
    Call HC.oPersist.WriteIniValueEx("JStickR", CStr(RHJStick), key, Ini)
    Call HC.oPersist.WriteIniValueEx("POV_Enable", CStr(POV_Enable), key, Ini)
'    Call HC.oPersist.WriteIniValueEx("Focusing_Enable", CStr(FocusingEnabled), key, Ini)

End Sub

Public Sub LoadJoystickCalib()
Dim tmptxt As String
Dim VarStr As String
Dim key As String
Dim Ini As String

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"

    key = "[calibration]"

    tmptxt = HC.oPersist.ReadIniValueEx("MinX", key, Ini)
    If tmptxt <> "" Then
        JoystickCal.dwMinXpos = val(tmptxt)
    Else
        tmptxt = "0"
        Call HC.oPersist.WriteIniValueEx("MinX", tmptxt, key, Ini)
        JoystickCal.dwMinXpos = 0
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("MaxX", key, Ini)
    If tmptxt <> "" Then
        JoystickCal.dwMaxXpos = val(tmptxt)
    Else
        tmptxt = "65535"
        Call HC.oPersist.WriteIniValueEx("MaxX", tmptxt, key, Ini)
        JoystickCal.dwMaxXpos = 65535
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("MinY", key, Ini)
    If tmptxt <> "" Then
        JoystickCal.dwMinYpos = val(tmptxt)
    Else
        tmptxt = "0"
        Call HC.oPersist.WriteIniValueEx("MinY", tmptxt, key, Ini)
        JoystickCal.dwMinYpos = 0
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("MaxY", key, Ini)
    If tmptxt <> "" Then
        JoystickCal.dwMaxYpos = val(tmptxt)
    Else
        tmptxt = "65535"
        Call HC.oPersist.WriteIniValueEx("MaxY", tmptxt, key, Ini)
        JoystickCal.dwMaxYpos = 65535
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("MinZ", key, Ini)
    If tmptxt <> "" Then
        JoystickCal.dwMinZpos = val(tmptxt)
    Else
        tmptxt = "0"
        Call HC.oPersist.WriteIniValueEx("MinZ", tmptxt, key, Ini)
        JoystickCal.dwMinZpos = 0
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("MaxZ", key, Ini)
    If tmptxt <> "" Then
        JoystickCal.dwMaxZpos = val(tmptxt)
    Else
        tmptxt = "65535"
        Call HC.oPersist.WriteIniValueEx("MaxZ", tmptxt, key, Ini)
        JoystickCal.dwMaxZpos = 65535
    End If

    tmptxt = HC.oPersist.ReadIniValueEx("MinR", key, Ini)
    If tmptxt <> "" Then
        JoystickCal.dwMinRpos = val(tmptxt)
    Else
        tmptxt = "0"
        Call HC.oPersist.WriteIniValueEx("MinR", tmptxt, key, Ini)
        JoystickCal.dwMinRpos = 0
    End If
    
    tmptxt = HC.oPersist.ReadIniValueEx("MaxR", key, Ini)
    If tmptxt <> "" Then
        JoystickCal.dwMaxRpos = val(tmptxt)
    Else
        tmptxt = "65535"
        Call HC.oPersist.WriteIniValueEx("MaxR", tmptxt, key, Ini)
        JoystickCal.dwMaxRpos = 65535
    End If

End Sub

Public Sub SaveJoystickCalib()

Dim tmptxt As String
Dim VarStr As String
Dim key As String
Dim Ini As String

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"

    key = "[calibration]"

    tmptxt = CStr(JoystickCal.dwMinXpos)
    Call HC.oPersist.WriteIniValueEx("MinX", tmptxt, key, Ini)

    tmptxt = CStr(JoystickCal.dwMaxXpos)
    Call HC.oPersist.WriteIniValueEx("MaxX", tmptxt, key, Ini)

    tmptxt = CStr(JoystickCal.dwMinYpos)
    Call HC.oPersist.WriteIniValueEx("MinY", tmptxt, key, Ini)

    tmptxt = CStr(JoystickCal.dwMaxYpos)
    Call HC.oPersist.WriteIniValueEx("MaxY", tmptxt, key, Ini)

    tmptxt = CStr(JoystickCal.dwMinZpos)
    Call HC.oPersist.WriteIniValueEx("MinZ", tmptxt, key, Ini)

    tmptxt = CStr(JoystickCal.dwMaxZpos)
    Call HC.oPersist.WriteIniValueEx("MaxZ", tmptxt, key, Ini)

    tmptxt = CStr(JoystickCal.dwMinRpos)
    Call HC.oPersist.WriteIniValueEx("MinR", tmptxt, key, Ini)

    tmptxt = CStr(JoystickCal.dwMaxRpos)
    Call HC.oPersist.WriteIniValueEx("MaxR", tmptxt, key, Ini)

End Sub

Public Sub SaveRates()
Dim i As Integer
Dim key As String
Dim Ini As String

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"

    key = "[rates]"

    For i = 0 To 9
        Call HC.oPersist.WriteIniValueEx("rate_" & CStr(i), CStr(rates(i)), key, Ini)
    Next i
    
    Call HC.oPersist.WriteIniValue("custom_rate_ra", FormatNumber(CustomRateRA, 5))
    Call HC.oPersist.WriteIniValue("custom_rate_dec", FormatNumber(CustomRateDEC, 5))
    
End Sub

Public Sub LoadRates()

Dim tmptxt As String
Dim VarStr As String
Dim key As String
Dim Ini As String
Dim i As Integer

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"

    key = "[rates]"

    For i = 0 To 9
        tmptxt = HC.oPersist.ReadIniValueEx("rate_" & CStr(i), key, Ini)
        If tmptxt <> "" Then
            rates(i) = val(tmptxt)
        Else
            rates(i) = 15 / 3600
            Call HC.oPersist.WriteIniValueEx("rate_" & CStr(i), CStr(rates(i)), key, Ini)
        End If
    Next i

    tmptxt = HC.oPersist.ReadIniValue("custom_rate_ra")
    If tmptxt <> "" Then
        CustomRateRA = val(tmptxt)
    Else
        CustomRateRA = SID_RATE
        Call HC.oPersist.WriteIniValue("custom_rate_ra", FormatNumber(CustomRateRA, 5))
    End If

    tmptxt = HC.oPersist.ReadIniValue("custom_rate_dec")
    If tmptxt <> "" Then
        CustomRateDEC = val(tmptxt)
    Else
        CustomRateDEC = 0
        Call HC.oPersist.WriteIniValue("custom_rate_dec", FormatNumber(CustomRateDEC, 5))
    End If

End Sub
Public Sub LoadFSpeeds()
Dim tmptxt As String
Dim VarStr As String
Dim key As String
Dim Ini As String
Dim i As Integer
Dim lSpeed As Long

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"
    
    Select Case ActiveFocuser
        Case 1
            ' focuser2
            key = "[f2speeds]"
        Case Else
            ' focuser1
            key = "[fspeeds]"
    End Select
    
    FPropsDlg.List1.Clear
    FPropsDlg.List1.AddItem vbTab & "Mode" & vbTab & "Steps"

    HC.Combo2.Clear
    For i = 1 To 4
        tmptxt = HC.oPersist.ReadIniValueEx("speed_" & CStr(i), key, Ini)
        If tmptxt <> "" Then
            lSpeed = val(tmptxt)
            FocuserProps.Speeds(i - 1).deltaStep = val(tmptxt)
        Else
            lSpeed = 0
            Call HC.oPersist.WriteIniValueEx("speed_" & CStr(i), "0", key, Ini)
        End If
        FocuserProps.Speeds(i - 1).deltaStep = lSpeed
        
        tmptxt = HC.oPersist.ReadIniValueEx("mode_" & CStr(i), key, Ini)
        Select Case tmptxt
            Case "0"
                FocuserProps.Speeds(i - 1).type = 0
            Case "1"
                FocuserProps.Speeds(i - 1).type = 1
            Case Else
                If i = 1 Then
                    FocuserProps.Speeds(i - 1).type = 0
                    Call HC.oPersist.WriteIniValueEx("mode_" & CStr(i), "0", key, Ini)
                Else
                    FocuserProps.Speeds(i - 1).type = 1
                    Call HC.oPersist.WriteIniValueEx("mode_" & CStr(i), "1", key, Ini)
                End If
        End Select
        If FocuserProps.Speeds(i - 1).type = 0 Then
            FPropsDlg.List1.AddItem CStr(i) & vbTab & "1Shot" & vbTab & CStr(lSpeed)
            HC.Combo2.AddItem "1Shot: " & CStr(lSpeed)
        Else
            FPropsDlg.List1.AddItem CStr(i) & vbTab & "Cont." & vbTab & CStr(lSpeed)
            HC.Combo2.AddItem "Cont.: " & CStr(lSpeed)
        End If
        
        
    Next i

End Sub

Public Sub SaveFSpeeds()
Dim i As Integer
Dim key As String
Dim Ini As String

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"

    Select Case ActiveFocuser
        Case 1
            ' focuser2
            key = "[f2speeds]"
        Case Else
            ' focuser1
            key = "[fspeeds]"
    End Select
    
    For i = 1 To 4
        Call HC.oPersist.WriteIniValueEx("speed_" & CStr(i), CStr(FocuserProps.Speeds(i - 1).deltaStep), key, Ini)
        Call HC.oPersist.WriteIniValueEx("mode_" & CStr(i), CStr(FocuserProps.Speeds(i - 1).type), key, Ini)
    Next i
   
End Sub

Public Sub LoadFBookmarks()
Dim tmptxt As String
Dim key As String
Dim Ini As String
Dim i As Integer

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"
    
    Select Case ActiveFocuser
        Case 1
            ' focuser2
            key = "[f2bookmarks]"
        Case Else
            ' focuser1
            key = "[f1bookmarks]"
    End Select

    For i = 0 To 9
        tmptxt = HC.oPersist.ReadIniValueEx("bookmark_" & CStr(i) & "_Pos", key, Ini)
        If tmptxt <> "" Then
            FocuserProps.Bookmarks(i).Position = val(tmptxt)
            FocuserProps.Bookmarks(i).enabled = True
        Else
            FocuserProps.Bookmarks(i).enabled = False
        End If
        
        tmptxt = HC.oPersist.ReadIniValueEx("bookmark_" & CStr(i) & "_name", key, Ini)
        If tmptxt <> "" Then
            FocuserProps.Bookmarks(i).Name = tmptxt
        Else
            FocuserProps.Bookmarks(i).enabled = False
        End If
    Next i

End Sub

Public Sub SaveFBookmarks()
Dim i As Integer
Dim key As String
Dim Ini As String

    ' set up a file path for the align.ini file
    Ini = HC.oPersist.GetIniPath & "\ASCOMPAD.ini"

    Select Case ActiveFocuser
        Case 1
            ' focuser2
            key = "[f2bookmarks]"
        Case Else
            ' focuser1
            key = "[f1bookmarks]"
    End Select
    
    For i = 0 To 9
        If FocuserProps.Bookmarks(i).enabled Then
            Call HC.oPersist.WriteIniValueEx("bookmark_" & CStr(i) & "_Pos", CStr(FocuserProps.Bookmarks(i).Position), key, Ini)
            Call HC.oPersist.WriteIniValueEx("bookmark_" & CStr(i) & "_name", CStr(FocuserProps.Bookmarks(i).Name), key, Ini)
        Else
            Call HC.oPersist.WriteIniValueEx("bookmark_" & CStr(i) & "_Pos", "", key, Ini)
            Call HC.oPersist.WriteIniValueEx("bookmark_" & CStr(i) & "_name", "", key, Ini)
        End If
    Next i
   
End Sub

