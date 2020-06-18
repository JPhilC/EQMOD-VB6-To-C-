using System;

namespace Project1
{
	//UPGRADE_NOTE: (1043) Class instancing was changed to public. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1043 
	public class Rate
	: UpgradeStubs.IRate
	{

		// -----------------------------------------------------------------------------'
		// ==================
		//   Rate.cls
		// ==================
		//
		// Implementation of the ASCOM Rate Class
		//
		// Written: Chris Rowland
		//
		// Edits:
		//
		// When      Who     What
		// --------- ---     --------------------------------------------------
		// ??-??-??  cr      Initial edit
		// 21-May-07 rbd     Add new ASCOM master interface, for early binding
		// -----------------------------------------------------------------------------'


		//===============
		//===============

		//local variables to hold property values
		private double m_Maximum = 0;
		private double m_Minimum = 0;

		//========================
		// IMPLEMEMTATION OF IRate
		// =======================


		//UPGRADE_NOTE: (7001) The following declaration (get Maximum) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double Maximum()
		//{
			//return m_Maximum;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let Maximum) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void Maximum(double value)
		//{
			//m_Maximum = value;
		//}


		//UPGRADE_NOTE: (7001) The following declaration (get Minimum) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private double Minimum()
		//{
			//return m_Minimum;
		//}
		//UPGRADE_NOTE: (7001) The following declaration (let Minimum) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private void Minimum(double value)
		//{
			//m_Minimum = value;
		//}

		// ================
		// PUBLIC INTERFACE
		// ================


		public double Maximum
		{
			get
			{
				return m_Maximum;
			}
			set
			{
				m_Maximum = value;
			}
		}



		public double Minimum
		{
			get
			{
				return m_Minimum;
			}
			set
			{
				m_Minimum = value;
			}
		}

		internal Rate()
		{
		}
	}
}