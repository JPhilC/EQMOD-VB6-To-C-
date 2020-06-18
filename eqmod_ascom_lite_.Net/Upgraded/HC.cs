using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Project1
{
    internal static class HC
    {


        //''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        const string CURRENT_VERSION = "V2.00i";
        //''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        public static int SiteIdx = 0;
        public static int EncoderReadErrCount = 0;
        public static bool EncoderTimerFlag = false;
        public static int DisplayMode = 0;
        private static Persist _oPersist = null;
        internal static Persist oPersist
        {
            get
            {
                if (_oPersist is null)
                {
                    _oPersist = new Persist();
                }
                return _oPersist;
            }
            set
            {
                _oPersist = value;
            }
        }


        private static object m_scope = null;
        private static int OldHeight = 0;
        private static int OldScaleHeight = 0;
        private static bool flash = false;
        private static int logcount = 0;
        private static int logfileindex = 0;
        private static int lblmode = 0;
        private static bool ScrollFlag = false;
        private static int AutoParkDuration = 0;
        //UPGRADE_ISSUE: (2068) DriverHelper.Util object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
        private static DriverHelper.Util hUtil = null;
        private static bool gIgnorClick = false;
        private static bool JoystickLost = false;

        private static string CommsLogFile = "";
        private static int CommsLogState = 0;


        //UPGRADE_NOTE: (2041) The following line was commented. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2041
        //[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //extern public static int GetTickCount();

		private void EncoderTimer_Timer()
        {

            eqmodvector.Coordt tmpcoord = new eqmodvector.Coordt();
            double tRa = 0;
            double tAlt = 0;
            double tAz = 0;
            object tmpRa = null;
            int tmpDec = 0;

            //Avoid overruns
            if (EncoderTimerFlag)
            {
                EncoderTimerFlag = false;
                //UPGRADE_TODO: (1067) Member CheckRASync is not defined in type HC. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
                //Read true motor positions
                tmpRa = Common.EQGetMotorValues(0);
                tmpDec = Common.EQGetMotorValues(1);

                EQMath.gEmulRA = Convert.ToDouble(tmpRa);
                EQMath.gEmulDEC = tmpDec;
                EQMath.gEmulRA_Init = EQMath.gEmulRA;
                EQMath.gLast_time = EQMath.EQnow_lst_norange();
                EQMath.gEmulOneShot = false;

                if (!Alignment.gThreeStarEnable)
                {
                    Alignment.gSelectStar = 0;
                    EQMath.gRA_Encoder = EQMath.Delta_RA_Map(EQMath.gEmulRA);
                    EQMath.gDec_Encoder = EQMath.Delta_DEC_Map(EQMath.gEmulDEC);
                }
                else
                {

                    switch (Common.gAlignmentMode)
                    {
                        case 2:
                            tmpcoord = EQMath.DeltaSync_Matrix_Map(EQMath.gEmulRA, EQMath.gEmulDEC);
                            EQMath.gRA_Encoder = tmpcoord.x;
                            EQMath.gDec_Encoder = tmpcoord.Y;

                            break;
                        case 1:
                            tmpcoord = EQMath.Delta_Matrix_Reverse_Map(EQMath.gEmulRA, EQMath.gEmulDEC);
                            EQMath.gRA_Encoder = tmpcoord.x;
                            EQMath.gDec_Encoder = tmpcoord.Y;

                            break;
                        default:
                            tmpcoord = EQMath.Delta_Matrix_Reverse_Map(EQMath.gEmulRA, EQMath.gEmulDEC);
                            EQMath.gRA_Encoder = tmpcoord.x;
                            EQMath.gDec_Encoder = tmpcoord.Y;
                            if (tmpcoord.f == 0)
                            {
                                tmpcoord = EQMath.DeltaSync_Matrix_Map(EQMath.gEmulRA, EQMath.gEmulDEC);
                                EQMath.gRA_Encoder = tmpcoord.x;
                                EQMath.gDec_Encoder = tmpcoord.Y;
                            }

                            break;
                    }
                }

                //Convert RA_Encoder to Hours
                if ((EQMath.gRA_Encoder < 0x1000000))
                {
                    EQMath.gRA_Hours = EQMath.Get_EncoderHours(EQMath.gRAEncoder_Zero_pos, EQMath.gRA_Encoder, EQMath.gTot_RA, EQMath.gHemisphere);
                }

                //Convert DEC_Encoder to DEC Degrees
                EQMath.gDec_DegNoAdjust = EQMath.Get_EncoderDegrees(EQMath.gDECEncoder_Zero_pos, EQMath.gDec_Encoder, EQMath.gTot_DEC, EQMath.gHemisphere);
                if (EQMath.gDec_Encoder < 0x1000000)
                {
                    EQMath.gDec_Degrees = EQMath.Range_DEC(EQMath.gDec_DegNoAdjust);
                }

                tRa = EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD) + EQMath.gRA_Hours;
                if (EQMath.gHemisphere == 0)
                {
                    if ((EQMath.gDec_DegNoAdjust > 90) && (EQMath.gDec_DegNoAdjust <= 270))
                    {
                        tRa -= 12;
                    }
                }
                else
                {
                    if ((EQMath.gDec_DegNoAdjust <= 90) || (EQMath.gDec_DegNoAdjust > 270))
                    {
                        tRa += 12;
                    }
                }
                tRa = EQMath.Range24(tRa);

                //assign global RA / Dec
                EQMath.gRA = tRa;
                EQMath.gDec = EQMath.gDec_Degrees;
                EQMath.gha = tRa - EQMath.EQnow_lst(EQMath.gLongitude * EQMath.DEG_RAD);

                //calc alt/ az poition
                ((Array)hadec_aa).GetValue(Convert.ToInt32(EQMath.gLatitude * EQMath.DEG_RAD), Convert.ToInt32(EQMath.gha * EQMath.HRS_RAD), Convert.ToInt32(EQMath.gDec_Degrees * EQMath.DEG_RAD), Convert.ToInt32(tAlt), Convert.ToInt32(tAz));
                //asign global Alt / Az
                EQMath.gAlt = tAlt * EQMath.RAD_DEG; // convert to degrees from Radians
                EQMath.gAz = 360d - (tAz * EQMath.RAD_DEG); //  convert to degrees from Radians

                //Poll the Motor Status while slew is active
                if (EQMath.gSlewStatus)
                {
                    EQMath.gRAStatus = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMotorStatus(0);
                    EQMath.gDECStatus = UpgradeSolution1Support.PInvoke.SafeNative.eqcontrl.EQ_GetMotorStatus(1);
                    if (EQMath.gEQparkstatus == 0)
                    {
                        Goto.ManageGoto();
                    }
                }

                //UPGRADE_TODO: (1067) Member Caption is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
                AlignmentCountLbl.Caption = Alignment.gAlignmentStars_count.ToString();

                do limit management


               Limits.Limits_Execute();

                EncoderTimerFlag = true;

            }

        }
    }
}