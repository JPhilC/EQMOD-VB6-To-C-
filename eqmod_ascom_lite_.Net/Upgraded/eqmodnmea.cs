using Microsoft.VisualBasic;
using System;

namespace Project1
{
	internal class eqmodnmea
	{


		//---------------------------------------------------------------------
		// Copyright © 2006 Raymund Sarmiento
		//
		// Permission is hereby granted to use this Software for any purpose
		// including combining with commercial products, creating derivative
		// works, and redistribution of source or binary code, without
		// limitation or consideration. Any redistributed copies of this
		// Software must include the above Copyright Notice.
		//
		// THIS SOFTWARE IS PROVIDED "AS IS". THE AUTHOR OF THIS CODE MAKES NO
		// WARRANTIES REGARDING THIS SOFTWARE, EXPRESS OR IMPLIED, AS TO ITS
		// SUITABILITY OR FITNESS FOR A PARTICULAR PURPOSE.
		//---------------------------------------------------------------------
		//
		//
		// Written:  07-Oct-06   Raymund Sarmiento
		//
		// Edits:
		//
		// When      Who     What
		// --------- ---     --------------------------------------------------
		// 10-Nov-06 rcs     Initial edit for EQ Mount Driver GPS CLASS
		// 01-Dec-06 rcs     Add GPS Function to EQMOD Driver
		//---------------------------------------------------------------------
		//
		//
		//  SYNOPSIS:
		//
		//  This is a demonstration of a EQ6/ATLAS/EQG direct stepper motor control access
		//  using the EQCONTRL.DLL driver code.
		//
		//  File EQCONTROL.bas contains all the function prototypes of all subroutines
		//  encoded in the EQCONTRL.dll
		//
		//  The EQ6CONTRL.DLL simplifies execution of the Mount controller board stepper
		//  commands.
		//
		//  The mount circuitry needs to be modified for this test program to work.
		//  Circuit details can be found at http://www.freewebs.com/eq6mod/
		//

		//  DISCLAIMER:

		//  You can use the information on this site COMPLETELY AT YOUR OWN RISK.
		//  The modification steps and other information on this site is provided
		//  to you "AS IS" and WITHOUT WARRANTY OF ANY KIND, express, statutory,
		//  implied or otherwise, including without limitation any warranty of
		//  merchantability or fitness for any particular or intended purpose.
		//  In no event the author will  be liable for any direct, indirect,
		//  punitive, special, incidental or consequential damages or loss of any
		//  kind whether or not the author  has been advised of the possibility
		//  of such loss.

		//  WARNING:

		//  Circuit modifications implemented on your setup could invalidate
		//  any warranty that you may have with your product. Use this
		//  information at your own risk. The modifications involve direct
		//  access to the stepper motor controls of your mount. Any "mis-control"
		//  or "mis-command"  / "invalid parameter" or "garbage" data sent to the
		//  mount could accidentally activate the stepper motors and allow it to
		//  rotate "freely" damaging any equipment connected to your mount.
		//  It is also possible that any garbage or invalid data sent to the mount
		//  could cause its firmware to generate mis-steps pulse sequences to the
		//  motors causing it to overheat. Make sure that you perform the
		//  modifications and testing while there is no physical "load" or
		//  dangling wires on your mount. Be sure to disconnect the power once
		//  this event happens or if you notice any unusual sound coming from
		//  the motor assembly.
		//


		string gnmlatitude = "";
		string gnmlongitude = "";
		string gnmsattime = "";
		string gnmsatdate = "";
		string gnmsatnow = "";

		string[] tokv = null;

		[field: System.NonSerialized]
		public event EQgpspositionHandler EQgpsposition;
		public delegate void EQgpspositionHandler(string gnmlatitude, string gnmlongitude, string lathm, string lath, string latm, string lonhm, string lonh, string lonm);
		[field: System.NonSerialized]
		public event EQgpstimeHandler EQgpstime;
		public delegate void EQgpstimeHandler(string time);
		[field: System.NonSerialized]
		public event EQgpsdateHandler EQgpsdate;
		public delegate void EQgpsdateHandler(string satDate);
		[field: System.NonSerialized]
		public event EQgpsnowHandler EQgpsnow;
		public delegate void EQgpsnowHandler(string Satnow, string hh, string mm, string ss, string mn, string dd, string yy);
		[field: System.NonSerialized]
		public event EQgpsfixokHandler EQgpsfixok;
		public delegate void EQgpsfixokHandler();
		[field: System.NonSerialized]
		public event EQgpsfixnotokHandler EQgpsfixnotok;
		public delegate void EQgpsfixnotokHandler();
		[field: System.NonSerialized]
		public event EQgpsaltitudeHandler EQgpsaltitude;
		public delegate void EQgpsaltitudeHandler(string altitude);
		[field: System.NonSerialized]
		public event EQgpsunitHandler EQgpsunit;
		public delegate void EQgpsunitHandler(string altitudeUnits);


		public bool scan(string sbuf)
		{

			if (!IsValid(sbuf))
			{
				return false;
			}

			string str = sbuf.Substring(Math.Max(sbuf.Length - (Strings.Len(sbuf) - 3), 0));
			str = str.Substring(0, Math.Min(3, str.Length));

			if (sbuf.StartsWith("$G"))
			{
				switch(str)
				{
					case "GGA" :  // Altitude 
						return scanGPGGA(sbuf);
					case "GLL" :  //gnmlatitude / gnmlongitude / Date and Time 
						return scanGPGLL(sbuf);
					case "RMC" :  // Satellite Fix 
						return scanGPRMC(sbuf);
					case "GNS" :  // GNSS Satellite Fix 
						return scanGPGNS(sbuf);
					case "ZDA" :  // time date 
						return scanGPZDA(sbuf);
					default:
						return false;
				}
			}
			return false;
		}

		private string[] septok(string sbuf)
		{
			if (Strings.Len(sbuf) > 3)
			{
				sbuf = sbuf.Substring(0, Math.Min(Strings.Len(sbuf) - 3, sbuf.Length));
			}
			return (string[]) sbuf.Split(',');
		}

		private bool scanGPGGA(string sbuf)
		{
			tokv = (string[]) septok(sbuf);
			GetTime(tokv[1]);
			GetLatLon(tokv[2], tokv[3], tokv[4], tokv[5]);
			GetAltitudeVAL(tokv[9]);
			GetAltitudeUnit(tokv[10]);
			return true;
		}

		private bool scanGPGNS(string sbuf)
		{
			tokv = (string[]) septok(sbuf);
			GetTime(tokv[1]);
			GetLatLon(tokv[2], tokv[3], tokv[4], tokv[5]);
			GetAltitudeVAL(tokv[9]);
			GetAltitudeUnit("M");
			return true;
		}

		private bool scanGPGLL(string sbuf)
		{
			tokv = (string[]) septok(sbuf);
			GetLatLon(tokv[1], tokv[2], tokv[3], tokv[4]);
			GetTime(tokv[5]);
			GetStatus(tokv[6]);
			return true;
		}

		private bool scanGPRMC(string sbuf)
		{
			tokv = (string[]) septok(sbuf);
			GetTime(tokv[1]);
			GetStatus(tokv[2]);
			GetLatLon(tokv[3], tokv[4], tokv[5], tokv[6]);
			GetDate(tokv[9]);
			GetNow(tokv[9], tokv[1]);
			return true;
		}

		private bool scanGPZDA(string sbuf)
		{
			//$GPZDA,hhmmss.ss,dd,mm,yyyy,xx,yy*CC
			GetNow2(sbuf);
			return true;
		}

		private bool IsValid(string sbuf)
		{
			return sbuf.Substring(Math.Max(sbuf.Length - 2, 0)) == GetChecksum(sbuf);
		}

		private string GetChecksum(string sbuf)
		{ // Calculates the checksum for a sbuf

			string result = "";
			string Character = "";
			int charCount = 0;
			int Checksum = 0;

			try
			{
				int tempForEndVar = Strings.Len(sbuf);
				for (charCount = 1; charCount <= tempForEndVar; charCount++)
				{
					Character = sbuf.Substring(0, Math.Min(charCount, sbuf.Length)).Substring(Math.Max(sbuf.Substring(0, Math.Min(charCount, sbuf.Length)).Length - 1, 0));
					switch(Character)
					{
						case "$" : 
							break;
						case "*" : 
							goto exit_for;
						default:
							if (Checksum == 0)
							{
								Checksum = Convert.ToInt32(Conversion.Val(Strings.Asc(Character[0]).ToString()));
							}
							else
							{
								Checksum = Checksum ^ Convert.ToInt32(Conversion.Val(Strings.Asc(Character[0]).ToString()));
							} 
							break;
					}
				}
				exit_for:
				result = ("00" + Checksum.ToString("X")).Substring(Math.Max(("00" + Checksum.ToString("X")).Length - 2, 0));
			}
			catch
			{
			}

			return result;
		}

		private void GetNow2(string str)
		{
			try
			{
				if (str != "")
				{
					tokv = (string[]) septok(str);
					gnmsattime = tokv[1].Substring(0, Math.Min(2, tokv[1].Length)) + ":" + tokv[1].Substring(2, Math.Min(2, tokv[1].Length - 2)) + ":" + tokv[1].Substring(4, Math.Min(2, tokv[1].Length - 4));
					gnmsatdate = tokv[3].Substring(0, Math.Min(2, tokv[3].Length)) + "/" + tokv[2].Substring(0, Math.Min(2, tokv[2].Length)) + "/" + tokv[4].Substring(2, Math.Min(2, tokv[4].Length - 2));
					gnmsatnow = gnmsatdate + " " + gnmsattime;
					if (EQgpsdate != null)
					{
						EQgpsdate(gnmsatdate);
					}
					if (EQgpstime != null)
					{
						EQgpstime(gnmsattime);
					}
					if (EQgpsnow != null)
					{
						EQgpsnow(gnmsatnow, tokv[1].Substring(0, Math.Min(2, tokv[1].Length)), tokv[1].Substring(2, Math.Min(2, tokv[1].Length - 2)), tokv[1].Substring(4, Math.Min(5, tokv[1].Length - 4)), tokv[3].Substring(0, Math.Min(2, tokv[3].Length)), tokv[2].Substring(0, Math.Min(2, tokv[2].Length)), tokv[4].Substring(2, Math.Min(2, tokv[4].Length - 2)));
					}
				}
			}
			catch
			{
			}

		}


		private void GetTime(string word)
		{
			try
			{
				if (word != "")
				{
					gnmsattime = word.Substring(0, Math.Min(2, word.Length)) + ":" + word.Substring(2, Math.Min(2, word.Length - 2)) + ":" + word.Substring(4, Math.Min(2, word.Length - 4));
					//  If Len(word) > 7 Then gnmsattime = gnmsattime + Mid$(word, 7, Len(word) - 6)
					if (EQgpstime != null)
					{
						EQgpstime(gnmsattime);
					}
				}
			}
			catch
			{
			}

		}

		private void GetDate(string word)
		{
			try
			{
				if (word != "")
				{
					gnmsatdate = word.Substring(2, Math.Min(2, word.Length - 2)) + "/" + word.Substring(0, Math.Min(2, word.Length)) + "/" + word.Substring(4, Math.Min(2, word.Length - 4));
					if (EQgpsdate != null)
					{
						EQgpsdate(gnmsatdate);
					}
				}
			}
			catch
			{
			}

		}

		private void GetNow(string word1, string word2)
		{

			string tdate = "";
			string ttime = "";
			try
			{

				if (word1 != "" && word2 != "")
				{
					ttime = word2.Substring(0, Math.Min(2, word2.Length)) + ":" + word2.Substring(2, Math.Min(2, word2.Length - 2)) + ":" + word2.Substring(4, Math.Min(2, word2.Length - 4));
					//If Len(word2) > 7 Then ttime = ttime + Mid$(word2, 7, Len(word2) - 6)
					tdate = word1.Substring(2, Math.Min(2, word1.Length - 2)) + "/" + word1.Substring(0, Math.Min(2, word1.Length)) + "/" + word1.Substring(4, Math.Min(2, word1.Length - 4));
					gnmsatnow = tdate + " " + ttime;

					// concat, mm, dd, yy, hh, mm ,ss
					if (EQgpsnow != null)
					{
						EQgpsnow(gnmsatnow, word2.Substring(0, Math.Min(2, word2.Length)), word2.Substring(2, Math.Min(2, word2.Length - 2)), word2.Substring(4, Math.Min(2, word2.Length - 4)), word1.Substring(2, Math.Min(2, word1.Length - 2)), word1.Substring(0, Math.Min(2, word1.Length)), word1.Substring(4, Math.Min(2, word1.Length - 4)));
					}
				}
			}
			catch
			{
			}

		}


		private void GetLatLon(string latWord, string latHemi, string lonWord, string lonHemi)
		{
			try
			{

				if (latWord != "" && latHemi != "" && lonWord != "" && lonHemi != "")
				{
					gnmlatitude = latWord.Substring(0, Math.Min(2, latWord.Length)) + "° "; //hours/degrees
					gnmlatitude = gnmlatitude + latWord.Substring(Math.Max(latWord.Length - (Strings.Len(latWord) - 2), 0)) + "'"; // Append minutes
					gnmlatitude = latHemi + " " + gnmlatitude; // start with the hemisphere
					gnmlongitude = lonWord.Substring(0, Math.Min(3, lonWord.Length)) + "° "; //hours/degrees
					gnmlongitude = gnmlongitude + lonWord.Substring(Math.Max(lonWord.Length - (Strings.Len(lonWord) - 3), 0)) + "'"; // Append minutes
					gnmlongitude = lonHemi + " " + gnmlongitude; // start with the hemisphere

					// lathemi, latdeg, latmin  , lonhemi, londeg, lonmin

					if (EQgpsposition != null)
					{
						EQgpsposition(gnmlatitude, gnmlongitude, latHemi, latWord.Substring(0, Math.Min(2, latWord.Length)), latWord.Substring(Math.Max(latWord.Length - (Strings.Len(latWord) - 2), 0)), lonHemi, lonWord.Substring(0, Math.Min(3, lonWord.Length)), lonWord.Substring(Math.Max(lonWord.Length - (Strings.Len(lonWord) - 3), 0)));
					}
				}
			}
			catch
			{
			}

		}

		private void GetAltitudeVAL(string word)
		{
			if (word != "")
			{
				if (EQgpsaltitude != null)
				{
					EQgpsaltitude(word);
				}
			}
		}

		private void GetAltitudeUnit(string word)
		{
			if (word != "")
			{
				if (EQgpsunit != null)
				{
					EQgpsunit(word);
				}
			}
		}

		private void GetStatus(string word)
		{
			if (word != "")
			{
				switch(word)
				{
					case "A" : 
						if (EQgpsfixok != null)
						{
							EQgpsfixok();
						} 
						break;
					case "V" : 
						if (EQgpsfixnotok != null)
						{
							EQgpsfixnotok();
						} 
						break;
				}
			}
		}
	}
}