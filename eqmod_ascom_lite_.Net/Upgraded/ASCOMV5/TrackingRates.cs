using System;
using System.Collections;
using System.Collections.Specialized;

namespace Project1
{
	//UPGRADE_NOTE: (1043) Class instancing was changed to public. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1043 
	public class TrackingRates
	: UpgradeStubs.ITrackingRates, IEnumerable
	{



		// local variable to hold collection
		private OrderedDictionary mCol = null;

		// Creates the collection when this class is created

		internal TrackingRates()
		{

			mCol = new OrderedDictionary(System.StringComparer.OrdinalIgnoreCase);

		}

		// Destroys collection when this class is terminated

		~TrackingRates()
		{

			mCol = null;

		}

		// Get the number of elements in the collection.
		// Example: Debug.Print x.Count

		public int Count
		{
			get
			{

				return mCol.Count;

			}
		}


		// Get an element in the collection
		// Example: Set foo = x.Item(5)

		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public UpgradeStubs.DriveRates GetItem(int Index)
		{

			//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
			return (UpgradeStubs.DriveRates) mCol[Index - 1];

		}

		// this property allows you to enumerate
		// this collection with the For...Each syntax

		//UPGRADE_ISSUE: (2068) stdole.IEnumVARIANT object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_ISSUE: (2068) stdole.IEnumVARIANT object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		public UpgradeStubs.stdole_IEnumVARIANT NewEnum
		{
			get
			{

				//UPGRADE_ISSUE: (2068) stdole.IEnumVARIANT object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
				return (UpgradeStubs.stdole_IEnumVARIANT) mCol.GetEnumerator();

			}
		}



		// IMPLEMENTATION OF ITrackingRates

		// Get the number of elements in the collection.
		// Example: Debug.Print x.Count

		//UPGRADE_NOTE: (7001) The following declaration (get Count) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private int Count()
		//{
			//return mCol.Count;
		//}

		// Get an element in the collection
		// Example: Set foo = x.Item(5)

		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (get Item) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private UpgradeStubs.DriveRates Item(int Index)
		//{
			////UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
			//return (UpgradeStubs.DriveRates) mCol[Index - 1];
		//}
		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068

		// this property allows you to enumerate
		// this collection with the For...Each syntax


		public IEnumerator GetEnumerator()
		{
			return mCol.Values.GetEnumerator();
		}

		// Add an element to the collection

		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_ISSUE: (2068) DriveRates object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		internal UpgradeStubs.DriveRates Add(UpgradeStubs.DriveRates newrate)
		{

			mCol.Add(Guid.NewGuid().ToString(), newrate);

			return newrate;

		}

		// Remove an element from the collection
		// Example: x.Remove(5)

		internal void Remove(int Index)
		{

			mCol.RemoveAt(Index - 1);

		}
	}
}