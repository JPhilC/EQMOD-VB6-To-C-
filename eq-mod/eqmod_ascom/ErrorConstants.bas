Attribute VB_Name = "ErrorConstants"

Option Explicit

Public Const ERR_SOURCE As String = "ASCOM EQASCOM Driver"

Public Const SCODE_NOT_IMPLEMENTED As Long = vbObjectError + &H400
Public Const SCODE_INVALID_VALUE As Long = vbObjectError + &H401
Public Const SCODE_VALUE_NOT_SET As Long = vbObjectError + &H402
Public Const SCODE_NOT_CONNECTED As Long = vbObjectError + &H407
Public Const SCODE_INVALID_WHILST_PARKED As Long = vbObjectError + &H408
Public Const SCODE_INVALID_WHILST_SLAVED As Long = vbObjectError + &H409
Public Const SCODE_SETTINGS_PROVIDER_ERROR As Long = vbObjectError + &H40A
Public Const SCODE_INVALID_OPERATION_EXCEPTION As Long = vbObjectError + &H40B
Public Const SCODE_ACTION_NOT_IMPLEMENTED As Long = vbObjectError + &H40C
Public Const SCODE_UNSEPCIFIED_ERROR As Long = vbObjectError + &H4FF
Public Const SCODE_DRIVER_SPECIFIC_BASE As Long = vbObjectError + &H500
Public Const SCODE_DRIVER_SPECIFIC_MAX As Long = vbObjectError + &HFFF

Public Const MSG_NOT_IMPLEMENTED As String = " is not implemented by this telescope driver object."
Public Const MSG_PROP_RANGE_ERROR As String = " is out of range in this telescope driver object."
Public Const MSG_VAL_OUTOFRANGE As String = "A property value is out of range"
Public Const MSG_PROP_NOT_SET As String = " has not been initialised."
Public Const MSG_SCOPE_PARKED As String = " is not permitted while mount is parked or parking."
Public Const MSG_RADEC_SLEW_ERROR As String = "RaDec slew is not permittted if mount is not Tracking."
Public Const MSG_RADEC_SYNC_ERROR As String = "RaDec sync is not permitted if moumt is not Tracking."
Public Const MSG_RADEC_SYNC_REJECT As String = "RaDec sync was rejected."


