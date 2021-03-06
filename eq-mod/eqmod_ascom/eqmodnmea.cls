VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "eqmodnmea"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False

'---------------------------------------------------------------------
' Copyright � 2006 Raymund Sarmiento
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
'
' Written:  07-Oct-06   Raymund Sarmiento
'
' Edits:
'
' When      Who     What
' --------- ---     --------------------------------------------------
' 10-Nov-06 rcs     Initial edit for EQ Mount Driver GPS CLASS
' 01-Dec-06 rcs     Add GPS Function to EQMOD Driver
'---------------------------------------------------------------------
'
'
'  SYNOPSIS:
'
'  This is a demonstration of a EQ6/ATLAS/EQG direct stepper motor control access
'  using the EQCONTRL.DLL driver code.
'
'  File EQCONTROL.bas contains all the function prototypes of all subroutines
'  encoded in the EQCONTRL.dll
'
'  The EQ6CONTRL.DLL simplifies execution of the Mount controller board stepper
'  commands.
'
'  The mount circuitry needs to be modified for this test program to work.
'  Circuit details can be found at http://www.freewebs.com/eq6mod/
'

'  DISCLAIMER:

'  You can use the information on this site COMPLETELY AT YOUR OWN RISK.
'  The modification steps and other information on this site is provided
'  to you "AS IS" and WITHOUT WARRANTY OF ANY KIND, express, statutory,
'  implied or otherwise, including without limitation any warranty of
'  merchantability or fitness for any particular or intended purpose.
'  In no event the author will  be liable for any direct, indirect,
'  punitive, special, incidental or consequential damages or loss of any
'  kind whether or not the author  has been advised of the possibility
'  of such loss.

'  WARNING:

'  Circuit modifications implemented on your setup could invalidate
'  any warranty that you may have with your product. Use this
'  information at your own risk. The modifications involve direct
'  access to the stepper motor controls of your mount. Any "mis-control"
'  or "mis-command"  / "invalid parameter" or "garbage" data sent to the
'  mount could accidentally activate the stepper motors and allow it to
'  rotate "freely" damaging any equipment connected to your mount.
'  It is also possible that any garbage or invalid data sent to the mount
'  could cause its firmware to generate mis-steps pulse sequences to the
'  motors causing it to overheat. Make sure that you perform the
'  modifications and testing while there is no physical "load" or
'  dangling wires on your mount. Be sure to disconnect the power once
'  this event happens or if you notice any unusual sound coming from
'  the motor assembly.
'

Option Explicit

Dim gnmlatitude As String
Dim gnmlongitude As String
Dim gnmsattime As String
Dim gnmsatdate As String
Dim gnmsatnow As String

Dim tokv() As String

Public Event EQgpsposition(ByVal gnmlatitude As String, ByVal gnmlongitude As String, ByVal lathm As String, ByVal lath As String, ByVal latm As String, ByVal lonhm As String, ByVal lonh As String, ByVal lonm As String)
Public Event EQgpstime(ByVal time As String)
Public Event EQgpsdate(ByVal satDate As String)
Public Event EQgpsnow(ByVal Satnow As String, ByVal hh As String, ByVal mm As String, ByVal ss As String, ByVal mn As String, ByVal dd As String, ByVal yy As String)
Public Event EQgpsfixok()
Public Event EQgpsfixnotok()
Public Event EQgpsaltitude(ByVal altitude As String)
Public Event EQgpsunit(ByVal altitudeUnits As String)


Public Function scan(ByVal sbuf As String) As Boolean
    Dim str As String

    If Not IsValid(sbuf) Then
      scan = False
      Exit Function
    End If
    
    str = Right(sbuf, Len(sbuf) - 3)
    str = Left(str, 3)
    
    If Left(sbuf, 2) = "$G" Then
        Select Case str
          Case "GGA"  ' Altitude
              scan = scanGPGGA(sbuf)
              Exit Function
          Case "GLL" 'gnmlatitude / gnmlongitude / Date and Time
              scan = scanGPGLL(sbuf)
              Exit Function
          Case "RMC" ' Satellite Fix
              scan = scanGPRMC(sbuf)
              Exit Function
          Case "GNS" ' GNSS Satellite Fix
              scan = scanGPGNS(sbuf)
              Exit Function
          Case "ZDA" ' time date
              scan = scanGPZDA(sbuf)
              Exit Function
          Case Else
              scan = False
              Exit Function
        End Select
    End If
End Function
 
Private Function septok(ByVal sbuf As String) As String()
    If Len(sbuf) > 3 Then sbuf = Left$(sbuf, Len(sbuf) - 3)
    septok = Split(sbuf, ",")
End Function

Private Function scanGPGGA(ByVal sbuf As String) As Boolean
    tokv = septok(sbuf)
    Call GetTime(tokv(1))
    Call GetLatLon(tokv(2), tokv(3), tokv(4), tokv(5))
    Call GetAltitudeVAL(tokv(9))
    Call GetAltitudeUnit(tokv(10))
    scanGPGGA = True
End Function

Private Function scanGPGNS(ByVal sbuf As String) As Boolean
    tokv = septok(sbuf)
    Call GetTime(tokv(1))
    Call GetLatLon(tokv(2), tokv(3), tokv(4), tokv(5))
    Call GetAltitudeVAL(tokv(9))
    Call GetAltitudeUnit("M")
    scanGPGNS = True
End Function

Private Function scanGPGLL(ByVal sbuf As String) As Boolean
    tokv = septok(sbuf)
    Call GetLatLon(tokv(1), tokv(2), tokv(3), tokv(4))
    Call GetTime(tokv(5))
    Call GetStatus(tokv(6))
    scanGPGLL = True
End Function

Private Function scanGPRMC(ByVal sbuf As String) As Boolean
    tokv = septok(sbuf)
    Call GetTime(tokv(1))
    Call GetStatus(tokv(2))
    Call GetLatLon(tokv(3), tokv(4), tokv(5), tokv(6))
    Call GetDate(tokv(9))
    Call GetNow(tokv(9), tokv(1))
    scanGPRMC = True
    Exit Function
End Function

Private Function scanGPZDA(ByVal sbuf As String) As Boolean
    '$GPZDA,hhmmss.ss,dd,mm,yyyy,xx,yy*CC
    Call GetNow2(sbuf)
    scanGPZDA = True
    Exit Function
End Function

 Private Function IsValid(ByVal sbuf As String) As Boolean
  If Right$(sbuf, 2) = GetChecksum(sbuf) Then
    IsValid = True
  Else
    IsValid = False
  End If
End Function
 
Private Function GetChecksum(ByVal sbuf As String) As String ' Calculates the checksum for a sbuf
  
  Dim Character As String
  Dim charCount As Integer
  Dim Checksum As Integer
  
  On Error GoTo endsub
  For charCount = 1 To Len(sbuf)
    Character = Right$(Left$(sbuf, charCount), 1)
    Select Case Character
        Case "$"
        Case "*"
            Exit For
        Case Else
            If Checksum = 0 Then
                Checksum = val(Asc(Character))
            Else
                Checksum = Checksum Xor val(Asc(Character))
            End If
    End Select
  Next charCount
  GetChecksum = Right$("00" & Hex(Checksum), 2)
endsub:
End Function

Private Sub GetNow2(ByVal str As String)
Dim tdate As String
Dim ttime As String
   On Error GoTo endsub
    If str <> "" Then
        tokv = septok(str)
        gnmsattime = Left$(tokv(1), 2) + ":" + mid$(tokv(1), 3, 2) + ":" + mid$(tokv(1), 5, 2)
        gnmsatdate = Left$(tokv(3), 2) + "/" + Left$(tokv(2), 2) + "/" + mid$(tokv(4), 3, 2)
        gnmsatnow = gnmsatdate & " " & gnmsattime
        RaiseEvent EQgpsdate(gnmsatdate)
        RaiseEvent EQgpstime(gnmsattime)
        RaiseEvent EQgpsnow(gnmsatnow, Left$(tokv(1), 2), mid$(tokv(1), 3, 2), mid$(tokv(1), 5, 5), Left(tokv(3), 2), Left$(tokv(2), 2), mid$(tokv(4), 3, 2))
    End If
endsub:
End Sub


Private Sub GetTime(ByVal word As String)
   On Error GoTo endsub
    If word <> "" Then
        gnmsattime = Left$(word, 2) + ":" + mid$(word, 3, 2) + ":" + mid$(word, 5, 2)
      '  If Len(word) > 7 Then gnmsattime = gnmsattime + Mid$(word, 7, Len(word) - 6)
        RaiseEvent EQgpstime(gnmsattime)
    End If
endsub:
End Sub

Private Sub GetDate(ByVal word As String)
   On Error GoTo endsub
    If word <> "" Then
        gnmsatdate = mid$(word, 3, 2) + "/" + Left$(word, 2) + "/" + mid$(word, 5, 2)
        RaiseEvent EQgpsdate(gnmsatdate)
    End If
endsub:
End Sub

Private Sub GetNow(ByVal word1 As String, ByVal word2 As String)

Dim tdate As String
Dim ttime As String
   On Error GoTo endsub

    If word1 <> "" And word2 <> "" Then
        ttime = Left$(word2, 2) + ":" + mid$(word2, 3, 2) + ":" + mid$(word2, 5, 2)
        'If Len(word2) > 7 Then ttime = ttime + Mid$(word2, 7, Len(word2) - 6)
        tdate = mid$(word1, 3, 2) + "/" + Left$(word1, 2) + "/" + mid$(word1, 5, 2)
        gnmsatnow = tdate & " " & ttime
        
        ' concat, mm, dd, yy, hh, mm ,ss
        RaiseEvent EQgpsnow(gnmsatnow, Left$(word2, 2), mid$(word2, 3, 2), mid$(word2, 5, 2), mid$(word1, 3, 2), Left$(word1, 2), mid$(word1, 5, 2))
    End If
endsub:
End Sub


Private Sub GetLatLon(ByVal latWord As String, ByVal latHemi As String, ByVal lonWord As String, ByVal lonHemi As String)
   On Error GoTo endsub
   
   If latWord <> "" And latHemi <> "" And lonWord <> "" And lonHemi <> "" Then
        gnmlatitude = Left$(latWord, 2) & "� " 'hours/degrees
        gnmlatitude = gnmlatitude & Right$(latWord, Len(latWord) - 2) & "'"      ' Append minutes
        gnmlatitude = latHemi & " " & gnmlatitude    ' start with the hemisphere
        gnmlongitude = Left$(lonWord, 3) & "� " 'hours/degrees
        gnmlongitude = gnmlongitude & Right$(lonWord, Len(lonWord) - 3) & "'"     ' Append minutes
        gnmlongitude = lonHemi & " " & gnmlongitude    ' start with the hemisphere
        
        ' lathemi, latdeg, latmin  , lonhemi, londeg, lonmin
        
        RaiseEvent EQgpsposition(gnmlatitude, gnmlongitude, latHemi, Left$(latWord, 2), Right$(latWord, Len(latWord) - 2), lonHemi, Left$(lonWord, 3), Right$(lonWord, Len(lonWord) - 3))
   End If
endsub:
End Sub

Private Sub GetAltitudeVAL(ByVal word As String)
    If word <> "" Then
        RaiseEvent EQgpsaltitude(word)
    End If
End Sub

Private Sub GetAltitudeUnit(ByVal word As String)
    If word <> "" Then
        RaiseEvent EQgpsunit(word)
    End If
End Sub

Private Sub GetStatus(ByVal word As String)
    If word <> "" Then
        Select Case word
            Case "A"
                RaiseEvent EQgpsfixok
            Case "V"
                RaiseEvent EQgpsfixnotok
        End Select
    End If
End Sub


