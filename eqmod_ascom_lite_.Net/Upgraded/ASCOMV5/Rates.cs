using System;
using System.Collections;
using System.Collections.Specialized;

namespace Project1
{
	//UPGRADE_NOTE: (1043) Class instancing was changed to public. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1043 
	public class Rates
	: UpgradeStubs.IAxisRates, IEnumerable
	{

		// -----------------------------------------------------------------------------'
		// ==================
		//   Rates.CLS
		// ==================
		//
		// Implementation of the ASCOM Rates Class
		//
		// Written: Chris Rowland
		//
		// Edits:
		//
		// When      Who     What
		// --------- ---     --------------------------------------------------
		// ???       cr      Initial edit
		// 10-Sep-03 jab     cut out "keys", changed varient to long, minor cleanup
		// 04-Nov-03 cdr     Change name to Rates and use Rate instead of Range for V2
		// -----------------------------------------------------------------------------'


		//local variable to hold collection
		private OrderedDictionary mCol = null;

		internal Rate Add(double Maximum, double Minimum)
		{

			//create a new object
			Rate objNewMember = new Rate();
			//set the properties passed into the method
			objNewMember.Maximum = Maximum;
			objNewMember.Minimum = Minimum;
			mCol.Add(Guid.NewGuid().ToString(), objNewMember);

			//return the object created
			return objNewMember;

		}

		public Rate this[int Index]
		{
			get
			{

				//used when referencing an element in the collection
				//Index contains either the Index to the collection
				//Syntax: Set foo = x.Item(5)
				Rate result = null;
				if (Index <= mCol.Count)
				{
					result = (Rate) mCol[0];
				}

				return result;
			}
		}


		public int Count
		{
			get
			{

				//used when retrieving the number of elements in the
				//collection. Syntax: Debug.Print x.Count
				return mCol.Count;

			}
		}


		//UPGRADE_ISSUE: (2068) VB.IUnknown object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068

		public IEnumerator GetEnumerator()
		{

			//this property allows you to enumerate
			//this collection with the For...Each syntax
			return mCol.Values.GetEnumerator();

		}

		// ============================
		// IMPLEMENTATION OF IAxisRates
		// ============================

		// Get the number of elements in the collection.
		// Example: Debug.Print x.Count

		//UPGRADE_NOTE: (7001) The following declaration (get Count) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private int Count()
		//{
			//return mCol.Count;
		//}

		// Get an element in the collection
		// Example: Set foo = x.Item(5)

		//UPGRADE_ISSUE: (2068) IRate object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
		//UPGRADE_NOTE: (7001) The following declaration (get Item) seems to be dead code More Information: https://www.mobilize.net/vbtonet/ewis/ewi7001
		//private UpgradeStubs.IRate Item(int Index)
		//{
			////UPGRADE_ISSUE: (2068) IRate object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068
			//return (UpgradeStubs.IRate) mCol[Index - 1];
		//}
		//UPGRADE_ISSUE: (2068) IRate object was not upgraded. More Information: https://www.mobilize.net/vbtonet/ewis/ewi2068

		// this property allows you to enumerate
		// this collection with the For...Each syntax


		public IEnumerator GetEnumerator()
		{
			return mCol.Values.GetEnumerator();
		}

		internal void Remove(int Index)
		{

			//used when removing an element from the collection
			//Index contains the Index
			//Syntax: x.Remove(5)
			mCol.RemoveAt(Index - 1);

		}

		internal Rates()
		{

			//creates the collection when this class is created
			mCol = new OrderedDictionary(System.StringComparer.OrdinalIgnoreCase);

		}

		~Rates()
		{

			//destroys collection when this class is terminated
			mCol = null;

		}
	}
}