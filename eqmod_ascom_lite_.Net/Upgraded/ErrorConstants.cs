using Microsoft.VisualBasic;
using System;

namespace Project1
{
	internal static class ErrorConstants
	{



		public const string ERR_SOURCE = "ASCOM EQASCOM Driver";

		static readonly public int SCODE_NOT_IMPLEMENTED = Constants.vbObjectError + 0x400;
		static readonly public int SCODE_INVALID_VALUE = Constants.vbObjectError + 0x401;
		static readonly public int SCODE_VALUE_NOT_SET = Constants.vbObjectError + 0x402;
		static readonly public int SCODE_NOT_CONNECTED = Constants.vbObjectError + 0x407;
		static readonly public int SCODE_INVALID_WHILST_PARKED = Constants.vbObjectError + 0x408;
		static readonly public int SCODE_INVALID_WHILST_SLAVED = Constants.vbObjectError + 0x409;
		static readonly public int SCODE_SETTINGS_PROVIDER_ERROR = Constants.vbObjectError + 0x40A;
		static readonly public int SCODE_INVALID_OPERATION_EXCEPTION = Constants.vbObjectError + 0x40B;
		static readonly public int SCODE_ACTION_NOT_IMPLEMENTED = Constants.vbObjectError + 0x40C;
		static readonly public int SCODE_UNSEPCIFIED_ERROR = Constants.vbObjectError + 0x4FF;
		static readonly public int SCODE_DRIVER_SPECIFIC_BASE = Constants.vbObjectError + 0x500;
		static readonly public int SCODE_DRIVER_SPECIFIC_MAX = Constants.vbObjectError + 0xFFF;

		public const string MSG_NOT_IMPLEMENTED = " is not implemented by this telescope driver object.";
		public const string MSG_PROP_RANGE_ERROR = " is out of range in this telescope driver object.";
		public const string MSG_VAL_OUTOFRANGE = "A property value is out of range";
		public const string MSG_PROP_NOT_SET = " has not been initialised.";
		public const string MSG_SCOPE_PARKED = " is not permitted while mount is parked or parking.";
		public const string MSG_RADEC_SLEW_ERROR = "RaDec slew is not permittted if mount is not Tracking.";
		public const string MSG_RADEC_SYNC_ERROR = "RaDec sync is not permitted if moumt is not Tracking.";
		public const string MSG_RADEC_SYNC_REJECT = "RaDec sync was rejected.";
	}
}