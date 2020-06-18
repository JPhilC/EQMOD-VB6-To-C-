Attribute VB_Name = "Sounds"
Option Explicit

Public Type EQMOD_SOUNDS
    PositionBeep As Boolean
    RateClick As Boolean
    GotoClick As Boolean
    GotoStartClick As Boolean
    ParkClick As Boolean
    ParkedClick As Boolean
    Stopclick As Boolean
    Unparkclick As Boolean
    TrackClick As Boolean
    FocusLockClick As Boolean
    FocuserClick As Boolean
    FocuserStepsClick As Boolean
    FMoveClick As Boolean
    BeepWav As String
    RateWav(1 To 10) As String
    FRateWav(1 To 4) As String
    SyncWav As String
    ParkedWav As String
    StopWav As String
    Unparkwav As String
    SiderealWav As String
    LunarWav As String
    SolarWav As String
    CustomWav As String
    F1Wav As String
    F2Wav As String
    FLOnWav As String
    FLOffWav As String
    FInWav As String
    FOutWav As String
    FiltersWav(0 To 9) As String
    FiltersClick As Boolean
End Type

Public EQSounds As EQMOD_SOUNDS
Private Declare Function sndPlaySound Lib "winmm.dll" Alias "sndPlaySoundA" (ByVal lpszSoundName As String, ByVal uFlags As Long) As Long
Private Const SND_ASYNC = &H1         '  play asynchronously
Private Const SND_SYNC = &H0


Public Sub EQ_Beep(BeepType As Integer)
    
    On Error Resume Next
    
    Select Case BeepType
        ' Beep
        Case 0
            If EQSounds.PositionBeep = True Then
                Call sndPlaySound(EQSounds.BeepWav, SND_ASYNC)
            End If
            
        ' Beep - always sounds
        Case 3
            Call sndPlaySound(EQSounds.BeepWav, SND_ASYNC)
            
        ' Stop
        Case 7
            If EQSounds.Stopclick = True Then
                Call sndPlaySound(EQSounds.StopWav, SND_ASYNC)
            End If
        
        ' Parked
        Case 8
            If EQSounds.ParkedClick = True Then
                Call sndPlaySound(EQSounds.ParkedWav, SND_ASYNC)
            End If
            
        ' Unpark
        Case 9
            If EQSounds.Unparkclick = True Then
                Call sndPlaySound(EQSounds.Unparkwav, SND_ASYNC)
            End If
            
        ' Sidereal
        Case 10
            If EQSounds.TrackClick = True Then
                Call sndPlaySound(EQSounds.SiderealWav, SND_ASYNC)
            End If
            
        ' lunar
        Case 11
            If EQSounds.TrackClick = True Then
                Call sndPlaySound(EQSounds.LunarWav, SND_ASYNC)
            End If
            
        ' solar
        Case 12
            If EQSounds.TrackClick = True Then
                Call sndPlaySound(EQSounds.SolarWav, SND_ASYNC)
            End If
            
        ' custom
        Case 13
            If EQSounds.TrackClick = True Then
                Call sndPlaySound(EQSounds.CustomWav, SND_ASYNC)
            End If
            
        ' rate sounds
        Case 101, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110
            If EQSounds.RateClick = True Then
                Call sndPlaySound(EQSounds.RateWav(BeepType - 200), SND_ASYNC)
            End If
        
        ' Frate sounds
        Case 201, 202, 203, 204
            If EQSounds.FocuserStepsClick = True Then
                Call sndPlaySound(EQSounds.FRateWav(BeepType - 200), SND_ASYNC)
            End If
        
        ' Focuser lock on
        Case 210
            If EQSounds.FocusLockClick = True Then
                Call sndPlaySound(EQSounds.FLOnWav, SND_ASYNC)
            End If
        
        ' Focuser lock off
        Case 211
            If EQSounds.FocusLockClick = True Then
                Call sndPlaySound(EQSounds.FLOffWav, SND_ASYNC)
            End If
        
        ' F1 select
        Case 212
            If EQSounds.FocuserClick = True Then
                Call sndPlaySound(EQSounds.F1Wav, SND_ASYNC)
            End If
        
        ' F2 select
        Case 213
            If EQSounds.FocuserClick = True Then
                Call sndPlaySound(EQSounds.F2Wav, SND_ASYNC)
            End If
        
        ' In
        Case 214
            If EQSounds.FMoveClick = True Then
                Call sndPlaySound(EQSounds.FInWav, SND_ASYNC)
            End If
        
        ' Out
        Case 215
            If EQSounds.FMoveClick = True Then
                Call sndPlaySound(EQSounds.FOutWav, SND_ASYNC)
            End If
            
        ' filter selection
        Case 300, 301, 302, 303, 304, 305, 306, 307, 308, 309
            If EQSounds.FiltersClick = True Then
                Call sndPlaySound(EQSounds.FiltersWav(BeepType - 300), SND_ASYNC)
            End If
        
       
        
    End Select
End Sub

Public Sub writeBeep()
Dim key As String
Dim i As Integer

    With EQSounds
        HC.oPersist.WriteIniValue "SND_WAV_BEEP", .BeepWav
        HC.oPersist.WriteIniValue "SND_WAV_PARKED", .ParkedWav
        HC.oPersist.WriteIniValue "SND_WAV_STOP", .StopWav
        HC.oPersist.WriteIniValue "SND_WAV_UNPARK", .Unparkwav
        HC.oPersist.WriteIniValue "SND_WAV_SIDEREAL", .SiderealWav
        HC.oPersist.WriteIniValue "SND_WAV_LUNAR", .LunarWav
        HC.oPersist.WriteIniValue "SND_WAV_SOLAR", .SolarWav
        HC.oPersist.WriteIniValue "SND_WAV_CUSTOM", .CustomWav
        HC.oPersist.WriteIniValue "SND_WAV_F1", .F2Wav
        HC.oPersist.WriteIniValue "SND_WAV_F2", .F1Wav
        HC.oPersist.WriteIniValue "SND_WAV_FLON", .FLOnWav
        HC.oPersist.WriteIniValue "SND_WAV_FLOFF", .FLOffWav
        HC.oPersist.WriteIniValue "SND_WAV_FIN", .FInWav
        HC.oPersist.WriteIniValue "SND_WAV_FOUT", .FOutWav

        For i = 1 To 10
            key = "SND_WAV_RATE" & CStr(i)
            HC.oPersist.WriteIniValue key, .RateWav(i)
        Next i
        For i = 1 To 4
            key = "SND_WAV_FRATE" & CStr(i)
            HC.oPersist.WriteIniValue key, .FRateWav(i)
        Next i
        
        For i = 0 To 9
            key = "SND_WAV_FILTER" & CStr(i)
            HC.oPersist.WriteIniValue key, .FiltersWav(i)
        Next i
        
        
        If .PositionBeep Then
            HC.oPersist.WriteIniValue "SND_ENABLE_BEEP", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_BEEP", "0"
        End If
        
        If .RateClick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_RATE", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_RATE", "0"
        End If
        If .ParkedClick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_PARKED", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_PARKED", "0"
        End If
        If .Stopclick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_STOP", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_STOP", "0"
        End If
        If .Unparkclick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_UNPARK", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_UNPARK", "0"
        End If
        If .TrackClick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_TRACKING", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_TRACKING", "0"
        End If
        If .FocuserClick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_FSELECT", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_FSELECT", "0"
        End If
        If .FocusLockClick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_FLOCK", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_FLOCK", "0"
        End If
        If .FocuserStepsClick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_FRATES", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_FRATES", "0"
        End If
        If .FMoveClick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_FMOVE", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_FMOVE", "0"
        End If
        
        If .FiltersClick Then
            HC.oPersist.WriteIniValue "SND_ENABLE_FILTERS", "1"
        Else
            HC.oPersist.WriteIniValue "SND_ENABLE_FILTERS", "0"
        End If
    
    
    End With
End Sub
Public Sub readBeep()

Dim tmptxt As String
Dim key As String
Dim i As Integer

    With EQSounds
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_BEEP")
        If tmptxt <> "" Then
            .BeepWav = tmptxt
        Else
            .BeepWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_BEEP", .BeepWav
        End If
         
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_UNPARK")
        If tmptxt <> "" Then
            .Unparkwav = tmptxt
        Else
            .Unparkwav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_UNPARK", .Unparkwav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_PARKED")
        If tmptxt <> "" Then
            .ParkedWav = tmptxt
        Else
            .ParkedWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_PARKED", .ParkedWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_STOP")
        If tmptxt <> "" Then
            .StopWav = tmptxt
        Else
            .StopWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_STOP", .StopWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_SIDEREAL")
        If tmptxt <> "" Then
            .SiderealWav = tmptxt
        Else
            .SiderealWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_SIDEREAL", .SiderealWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_LUNAR")
        If tmptxt <> "" Then
            .LunarWav = tmptxt
        Else
            .LunarWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_LUNAR", .LunarWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_SOLAR")
        If tmptxt <> "" Then
            .SolarWav = tmptxt
        Else
            .SolarWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_SOLAR", .SolarWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_CUSTOM")
        If tmptxt <> "" Then
            .CustomWav = tmptxt
        Else
            .CustomWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_CUSTOM", .CustomWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_CUSTOM")
        If tmptxt <> "" Then
            .CustomWav = tmptxt
        Else
            .CustomWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_CUSTOM", .CustomWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_F1")
        If tmptxt <> "" Then
            .F1Wav = tmptxt
        Else
            .F1Wav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_F1", .F1Wav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_F2")
        If tmptxt <> "" Then
            .F2Wav = tmptxt
        Else
            .F2Wav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_F2", .F2Wav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_FLON")
        If tmptxt <> "" Then
            .FLOnWav = tmptxt
        Else
            .FLOnWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_FLON", .FLOnWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_FLOFF")
        If tmptxt <> "" Then
            .FLOffWav = tmptxt
        Else
            .FLOffWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_FLOFF", .FLOffWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_FIN")
        If tmptxt <> "" Then
            .FInWav = tmptxt
        Else
            .FInWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_FIN", .FInWav
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_WAV_FOUT")
        If tmptxt <> "" Then
            .FOutWav = tmptxt
        Else
            .FOutWav = "EQMOD_beep.wav"
            HC.oPersist.WriteIniValue "SND_WAV_FOUT", .FOutWav
        End If
        
        
        For i = 1 To 10
            key = "SND_WAV_RATE" & CStr(i)
            tmptxt = HC.oPersist.ReadIniValue(key)
            If tmptxt <> "" Then
                .RateWav(i) = tmptxt
            Else
                .RateWav(i) = "EQMOD_beep.wav"
                HC.oPersist.WriteIniValue key, .RateWav(i)
            End If
        Next i
        
        For i = 1 To 4
            key = "SND_WAV_FRATE" & CStr(i)
            tmptxt = HC.oPersist.ReadIniValue(key)
            If tmptxt <> "" Then
                .FRateWav(i) = tmptxt
            Else
                .FRateWav(i) = "EQMOD_beep.wav"
                HC.oPersist.WriteIniValue key, .FRateWav(i)
            End If
        Next i
        
        For i = 0 To 9
            key = "SND_WAV_FILTER" & CStr(i)
            tmptxt = HC.oPersist.ReadIniValue(key)
            If tmptxt <> "" Then
                .FiltersWav(i) = tmptxt
            Else
                .FiltersWav(i) = "EQMOD_beep.wav"
                HC.oPersist.WriteIniValue key, .FiltersWav(i)
            End If
        Next i
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_BEEP")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .PositionBeep = True
            Else
                .PositionBeep = False
            End If
        Else
            .PositionBeep = False
            HC.oPersist.WriteIniValue "SND_ENABLE_BEEP", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_RATE")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .RateClick = True
            Else
                .RateClick = False
            End If
        Else
            .RateClick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_RATE", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_UNPARK")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .Unparkclick = True
            Else
                .Unparkclick = False
            End If
        Else
            .Unparkclick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_UNPARK", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_PARKED")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .ParkedClick = True
            Else
                .ParkedClick = False
            End If
        Else
            .ParkedClick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_PARKED", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_STOP")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .Stopclick = True
            Else
                .Stopclick = False
            End If
        Else
            .Stopclick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_STOP", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_TRACKING")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .TrackClick = True
            Else
                .TrackClick = False
            End If
        Else
            .TrackClick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_TRACKING", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_FSELECT")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .FocuserClick = True
            Else
                .FocuserClick = False
            End If
        Else
            .FocuserClick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_FOCUSER", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_FLOCK")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .FocusLockClick = True
            Else
                .FocusLockClick = False
            End If
        Else
            .FocusLockClick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_FLOCK", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_FRATES")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .FocuserStepsClick = True
            Else
                .FocuserStepsClick = False
            End If
        Else
            .FocuserStepsClick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_FRATES", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_FMOVE")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .FMoveClick = True
            Else
                .FMoveClick = False
            End If
        Else
            .FMoveClick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_FMOVE", "0"
        End If
        
        tmptxt = HC.oPersist.ReadIniValue("SND_ENABLE_FILTERS")
        If tmptxt <> "" Then
            If tmptxt = "1" Then
                .FiltersClick = True
            Else
                .FiltersClick = False
            End If
        Else
            .FiltersClick = False
            HC.oPersist.WriteIniValue "SND_ENABLE_FILTERS", "0"
        End If
        
        
    End With
End Sub

