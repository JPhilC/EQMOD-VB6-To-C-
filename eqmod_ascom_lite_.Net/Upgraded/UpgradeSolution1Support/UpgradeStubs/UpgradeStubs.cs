using System.Windows.Forms;

namespace UpgradeStubs
{
	public class AlignmentModes
	{

	} 
	public class DriveRates
	{

	} 
	public class EquatorialCoordinateType
	{

	} 
	public class GuideDirections
	{

	} 
	public class IAxisRates
	{

	} 
	public class IRate
	{

	} 
	public class ITelescope
	{

	} 
	public class ITrackingRates
	{

	} 
	public class PierSide
	{

	} 
	public class stdole
	{

	} 
	public class stdole_IEnumVARIANT
	{

	} 
	public static class System_Windows_Forms_Control
	{

		public static void Print(this Control instance, string Unnamed)
		{
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("VB.Control.Print");
		}
	} 
	public static class System_Windows_Forms_PictureBox
	{

		public static void Cls(this PictureBox instance)
		{
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("VB.PictureBox.Cls");
		}
	} 
	public class TelescopeAxes
	{

	} 
	public class VB
	{

		public static UpgradeStubs.VB_Global getGlobal()
		{
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("VB.Global");
			return default(UpgradeStubs.VB_Global);
		}
	} 
	public class VB_App
	{

		public int getStartMode()
		{
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("VB.App.StartMode");
			return default(int);
		}
	} 
	public class VB_Global
	{

		public static UpgradeStubs.VB_App getApp()
		{
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("VB.Global.App");
			return default(UpgradeStubs.VB_App);
		}
		public void Unload(object object_Renamed)
		{
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("VB.Global.Unload");
		}
	} 
	public class VB_IUnknown
	{

	} 
	public class VBRUN_ApplicationStartConstants
	{

		public static UpgradeStubs.VBRUN_ApplicationStartConstantsEnum getvbSModeStandalone()
		{
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("VBRUN.ApplicationStartConstants.vbSModeStandalone");
			return (UpgradeStubs.VBRUN_ApplicationStartConstantsEnum) VBRUN_ApplicationStartConstantsEnum.vbSModeStandalone;
		}
	} 
	public enum VBRUN_ApplicationStartConstantsEnum
	{
		vbSModeStandalone = 0
	}
}