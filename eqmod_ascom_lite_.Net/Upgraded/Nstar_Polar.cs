using System;
using System.Drawing;

namespace Project1
{
	internal static class Nstar_Polar
	{

		// Attribute VB_Name = "Nstar_Polar"
		//---------------------------------------------------------------------
		// Copyright © 2007 Raymund Sarmiento
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
		// Nstar_polar.bas - Polar Alignment using the N-star table
		//
		//
		// Written:  07-Oct-06   Raymund Sarmiento
		//
		// Edits:
		//
		// When      Who     What
		// --------- ---     --------------------------------------------------
		// 21-Dec-07 rcs     Initial edit for EQ Mount Driver Function Prototype
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
		//  CREDITS:
		//
		//  Portions of the information on this code should be attributed
		//  to Mr. John Archbold from his initial observations and analysis
		//  of the interface circuits and of the ASCII data stream between
		//  the Hand Controller (HC) and the Go To Controller.
		//



		internal static double EQGet_Polar_Offset(double RA, double DEC, double radius, double raprobe, double pscale)
		{
			object HC = null;


			eqmodvector.Coord tmpcoord1 = new eqmodvector.Coord();
			eqmodvector.Coord tmpcoord2 = new eqmodvector.Coord();



			// Must perform the usual Update Affine here

			// Transform using the Negative RA boundary

			int i = EQ_UpdateAffine_PolarDrift(RA - raprobe, DEC);

			tmpcoord1.x = RA - raprobe;
			tmpcoord1.Y = DEC;


			tmpcoord1 = eqmodvector.EQ_plAffine2(tmpcoord1);

			// Transform using the Positive RA Boundary

			i = EQ_UpdateAffine_PolarDrift(RA + raprobe, DEC);

			tmpcoord2.x = RA + raprobe;
			tmpcoord2.Y = DEC;


			tmpcoord2 = eqmodvector.EQ_plAffine2(tmpcoord2);


			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(tmpcoord1).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpcoord1).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(tmpcoord2).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpcoord2).Y, pscale), Color.Red);


			// Get the drift points

			double dy1 = DEC - tmpcoord1.Y;
			double dy2 = DEC - tmpcoord2.Y;

			// Coompute for the run data for slope computations

			double dx = raprobe * Math.Sin(360 * (radius / EQMath.gTot_step) * EQMath.DEG_RAD) * 2;

			if (dx == 0)
			{
				dx = 0.00000001d;
			}

			// Get the Perpendicular offset error

			return Math.Tan(Math.Atan((dy2 - dy1) / dx)) * Math.Abs(radius);

		}

		// Function to Normalize the Virtual Horizon Measurement data

		internal static eqmodvector.Coord EQNormalize_Polar(double Alt, double Az, double vhoriz)
		{


			// Transform Alt/Az data based on the horiz value

			// 90 degrees from the virtual horizon
			eqmodvector.Coord result = new eqmodvector.Coord();
			eqmodvector.CartesCoord crt = eqmodvector.EQ_Polar2Cartes(vhoriz + (EQMath.gTot_step / 4d), Alt, EQMath.gTot_step, 0, 0);

			// 180 degrees from the virtual horizon
			eqmodvector.CartesCoord crt2 = eqmodvector.EQ_Polar2Cartes(vhoriz + (EQMath.gTot_step / 2d), Az, EQMath.gTot_step, 0, 0);

			// Return the normalized data
			result.x = (crt.x + crt2.x) * -1;
			result.Y = crt.Y + crt2.Y;


			return result;
		}


		//Function to convert polar coordinates to Cartesian using the Coord structure (for Polar Alignment function)

		internal static eqmodvector.Coord EQ_pl2Cs_Polar(eqmodvector.Coord obj, double poffset)
		{


			eqmodvector.Coord result = new eqmodvector.Coord();
			eqmodvector.CartesCoord tmpobj = eqmodvector.EQ_Polar2Cartes(obj.x, obj.Y - poffset, EQMath.gTot_step, EQMath.RAEncoder_Home_pos, EQMath.gDECEncoder_Home_pos);

			result.x = tmpobj.x;
			result.Y = tmpobj.Y;
			result.z = 1;

			return result;
		}

		internal static int EQ_UpdateAffine_PolarDrift(double x, double Y)
		{

			eqmodvector.Coord tmpcoord = new eqmodvector.Coord();

			int i = 0;

			double[] datholder = new double[Alignment.MAX_STARS];
			double[] dotidholder = new double[Alignment.MAX_STARS];

			// Adjust only if there are four alignment stars
			if (Alignment.gAlignmentStars_count < 3)
			{
				return 0;
			}

			tmpcoord.x = x;
			tmpcoord.Y = Y;
			tmpcoord = eqmodvector.EQ_sp2Cs(tmpcoord);

			int tempForEndVar = Alignment.gAlignmentStars_count;
			for (i = 1; i <= tempForEndVar; i++)
			{
				// Compute for total X-Y distance.
				datholder[i - 1] = Math.Abs(Alignment.my_PointsC[i - 1].x - tmpcoord.x) + Math.Abs(Alignment.my_PointsC[i - 1].Y - tmpcoord.Y);
				// Also save the reference star id for this particular reference star
				dotidholder[i - 1] = i;
			}

			eqmodvector.EQ_Quicksort(datholder, dotidholder, 1, Alignment.gAlignmentStars_count);
			// Get the nearest Star (lowest at the head of the sorted list)
			i = Convert.ToInt32(dotidholder[0]);
			int j = Convert.ToInt32(dotidholder[1]);
			int k = Convert.ToInt32(dotidholder[2]);

			return eqmodvector.EQ_AssembleMatrix_Affine(tmpcoord.x, tmpcoord.Y, Alignment.ct_PointsC[i - 1], Alignment.ct_PointsC[j - 1], Alignment.ct_PointsC[k - 1], Alignment.my_PointsC[i - 1], Alignment.my_PointsC[j - 1], Alignment.my_PointsC[k - 1]);

		}


		//Implement an Affine transformation on a Polar coordinate system
		//This is done by converting the Polar Data to Cartesian, Apply affine transformation
		//then return the transformed coordinates

		internal static eqmodvector.Coord EQ_plAffineCartes(eqmodvector.Coord obj)
		{

			eqmodvector.Coord result = new eqmodvector.Coord();
			eqmodvector.Coord tmpobj2 = new eqmodvector.Coord();

			eqmodvector.CartesCoord tmpobj1 = eqmodvector.EQ_Polar2Cartes(obj.x, obj.Y, EQMath.gTot_step, EQMath.RAEncoder_Home_pos, EQMath.gDECEncoder_Home_pos);

			tmpobj2.x = tmpobj1.x;
			tmpobj2.Y = tmpobj1.Y;
			tmpobj2.z = 1;

			eqmodvector.Coord tmpobj3 = eqmodvector.EQ_Transform_Affine(tmpobj2);

			result.x = tmpobj3.x;
			result.Y = tmpobj3.Y;
			result.z = 1;


			return result;
		}


		internal static void PolarAlign_init(int stepcount)
		{
			object HC = null;
			int i = 0;

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.DrawMode = 13;
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Cls();

			if (stepcount > 50)
			{
				//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				object tempForEndVar = HC.polarplot.width;
				int tempForStepVar = stepcount;
				for (i = 0; (tempForStepVar < 0) ? i >= Convert.ToDouble(tempForEndVar) : i <= Convert.ToDouble(tempForEndVar); i += tempForStepVar)
				{

					//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
					HC.polarplot.Circle(Convert.ToDouble(HC.polarplot.width) / 2d, Convert.ToDouble(HC.polarplot.Height) / 2d, i, Color.Blue);

				}
			}

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(0, Convert.ToDouble(HC.polarplot.Height) / 2d, HC.polarplot.width, Convert.ToDouble(HC.polarplot.Height) / 2d, Color.Red);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(Convert.ToDouble(HC.polarplot.width) / 2d, 0, Convert.ToDouble(HC.polarplot.width) / 2d, HC.polarplot.Height, Color.Red);

		}


		internal static void Plot_PolarAlign(double RA, double DEC, double pscale)
		{
			object HC = null;

			// 0.0024 = 0.144 / 60  that is .0024 arcminute / microsteps

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Circle((Convert.ToDouble(HC.polarplot.width) / 2d) + ((RA * 0.0024d) * pscale), (Convert.ToDouble(HC.polarplot.Height) / 2d) - ((DEC * 0.0024d) * pscale), 50, Color.Yellow);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(Convert.ToDouble(HC.polarplot.width) / 2d, Convert.ToDouble(HC.polarplot.Height) / 2d, (Convert.ToDouble(HC.polarplot.width) / 2d) + ((RA * 0.0024d) * pscale), (Convert.ToDouble(HC.polarplot.Height) / 2d) - ((DEC * 0.0024d) * pscale), Color.Yellow);

		}

		internal static void NStar_Polar_plot_init(int stepcount)
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.DrawMode = 13;
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Cls();


			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(EQMath.gXshift, EQMath.gYshift + (Convert.ToDouble(HC.polarplot.Height) * 3 / 4d), EQMath.gXshift + Convert.ToDouble(HC.polarplot.width), EQMath.gYshift + (Convert.ToDouble(HC.polarplot.Height) * 3 / 4d), Color.Red);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(EQMath.gXshift + (Convert.ToDouble(HC.polarplot.width) / 2d), EQMath.gYshift, EQMath.gXshift + (Convert.ToDouble(HC.polarplot.width) / 2d), EQMath.gYshift + Convert.ToDouble(HC.polarplot.Height), Color.Red);


		}

		internal static double pxlate_x(double inpx, double pscale)
		{
			object HC = null;

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			return (Convert.ToDouble(HC.polarplot.width) / 2d) - (inpx * pscale / (EQMath.gTot_step / 2d)) + EQMath.gXshift;

		}
		internal static double pxlate_y(double inpy, double pscale)
		{
			object HC = null;


			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			return (Convert.ToDouble(HC.polarplot.Height) * 3 / 4d) + (inpy * pscale / (EQMath.gTot_step / 2d)) + EQMath.gYshift;


		}



		internal static void NStar_Polar_plot(double RA1, double DEC1, double RA2, double DEC2, double pscale)
		{
			object HC = null;
			double i = 0;


			eqmodvector.Coord tmpobj = new eqmodvector.Coord();
			eqmodvector.Coord tmpobj2 = new eqmodvector.Coord();
			double[] datholder = new double[Alignment.MAX_STARS];
			double[] dotidholder = new double[Alignment.MAX_STARS];

			if (!Alignment.gThreeStarEnable)
			{

				return;

			}

			//UPGRADE_TODO: (1067) Member HScroll4 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			double raprobe = Convert.ToDouble(HC.HScroll4.Value);
			raprobe *= 100;

			tmpobj.x = RA1;
			tmpobj.Y = DEC1;

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Circle(pxlate_x(eqmodvector.EQ_pl2Cs(tmpobj).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpobj).Y, pscale), 30, Color.Yellow);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(0, pscale), pxlate_y(0, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(tmpobj).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpobj).Y, pscale), Color.Yellow);

			tmpobj.x = RA1 - raprobe;
			tmpobj2.x = RA1 + raprobe;
			tmpobj2.Y = DEC1;

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(tmpobj).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpobj).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(tmpobj2).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpobj2).Y, pscale), Color.Blue);


			tmpobj.x = RA2;
			tmpobj.Y = DEC2;

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Circle(pxlate_x(eqmodvector.EQ_pl2Cs(tmpobj).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpobj).Y, pscale), 30, Color.Lime);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(0, pscale), pxlate_y(0, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(tmpobj).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpobj).Y, pscale), Color.Lime);

			tmpobj.x = RA2 - raprobe;
			tmpobj2.x = RA2 + raprobe;
			tmpobj2.Y = DEC2;

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(tmpobj).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpobj).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(tmpobj2).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(tmpobj2).Y, pscale), Color.Blue);

			tmpobj.x = RA1;
			tmpobj.Y = DEC1;

			int tempForEndVar = Alignment.gAlignmentStars_count;
			for (i = 1; i <= tempForEndVar; i++)
			{

				//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
				HC.polarplot.Circle(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).Y, pscale), 30, Color.Cyan);
				// Compute for total X-Y distance.
				datholder[Convert.ToInt32(i) - 1] = Math.Abs(Alignment.my_PointsC[Convert.ToInt32(i) - 1].x - eqmodvector.EQ_sp2Cs(tmpobj).x) + Math.Abs(Alignment.my_PointsC[Convert.ToInt32(i) - 1].Y - eqmodvector.EQ_sp2Cs(tmpobj).Y);
				// Also save the reference star id for this particular reference star
				dotidholder[Convert.ToInt32(i) - 1] = i;

			}

			eqmodvector.EQ_Quicksort(datholder, dotidholder, 1, Alignment.gAlignmentStars_count);
			// Get the nearest Star (lowest at the head of the sorted list)
			i = dotidholder[0];
			double j = dotidholder[1];
			double k = dotidholder[2];

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(i) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(i) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(j) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(j) - 1]).Y, pscale), Color.Yellow);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(j) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(j) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(k) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(k) - 1]).Y, pscale), Color.Yellow);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(i) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(i) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(k) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(k) - 1]).Y, pscale), Color.Yellow);

			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(j) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(j) - 1]).Y, pscale), Color.Blue);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(j) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(j) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(k) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(k) - 1]).Y, pscale), Color.Blue);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(k) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(k) - 1]).Y, pscale), Color.Blue);


			tmpobj.x = RA2;
			tmpobj.Y = DEC2;

			int tempForEndVar2 = Alignment.gAlignmentStars_count;
			for (i = 1; i <= tempForEndVar2; i++)
			{
				// Compute for total X-Y distance.
				datholder[Convert.ToInt32(i) - 1] = Math.Abs(Alignment.my_PointsC[Convert.ToInt32(i) - 1].x - eqmodvector.EQ_sp2Cs(tmpobj).x) + Math.Abs(Alignment.my_PointsC[Convert.ToInt32(i) - 1].Y - eqmodvector.EQ_sp2Cs(tmpobj).Y);

				// Also save the reference star id for this particular reference star
				dotidholder[Convert.ToInt32(i) - 1] = i;
			}
			eqmodvector.EQ_Quicksort(datholder, dotidholder, 1, Alignment.gAlignmentStars_count);
			// Get the nearest Star (lowest at the head of the sorted list)
			i = dotidholder[0];
			j = dotidholder[1];
			k = dotidholder[2];


			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(i) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(i) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(j) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(j) - 1]).Y, pscale), Color.Lime);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(j) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(j) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(k) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(k) - 1]).Y, pscale), Color.Lime);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(i) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(i) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(k) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.my_Points[Convert.ToInt32(k) - 1]).Y, pscale), Color.Lime);


			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(j) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(j) - 1]).Y, pscale), Color.Blue);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(j) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(j) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(k) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(k) - 1]).Y, pscale), Color.Blue);
			//UPGRADE_TODO: (1067) Member polarplot is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.polarplot.Line(pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(i) - 1]).Y, pscale), pxlate_x(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(k) - 1]).x, pscale), pxlate_y(eqmodvector.EQ_pl2Cs(Alignment.ct_Points[Convert.ToInt32(k) - 1]).Y, pscale), Color.Blue);

		}


		internal static eqmodvector.Coord PolarAlignDrift_Map(double RA1, double DEC1, double RA2, double DEC2, double raprobe, double pscale)
		{
			eqmodvector.Coord result = new eqmodvector.Coord();
			object HC = null;


			if ((RA1 >= 0x1000000) || (DEC1 >= 0x1000000) || (!Alignment.gThreeStarEnable))
			{

				result.x = 0;
				result.Y = 0;
				result.z = 0;

				return result;

			}



			// re transform using the 3 nearest stars

			//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.EncoderTimer.Enabled = false;

			double dy1 = EQGet_Polar_Offset(RA1, DEC1, EQMath.gDECEncoder_Home_pos - DEC1, raprobe, pscale);
			double dy2 = EQGet_Polar_Offset(RA2, DEC2, EQMath.gDECEncoder_Home_pos - DEC1, raprobe, pscale);


			//UPGRADE_TODO: (1067) Member EncoderTimer is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			HC.EncoderTimer.Enabled = true;

			eqmodvector.Coord obtmp2 = EQNormalize_Polar(dy1, dy2, EQMath.RAEncoder_Home_pos - RA1);

			result.x = obtmp2.x;
			result.Y = obtmp2.Y;
			result.z = 1;


			return result;
		}

		internal static void Position_polar(double pscale)
		{
			object HC = null;

			if (!Alignment.gThreeStarEnable)
			{

				return;

			}

			//UPGRADE_TODO: (1067) Member HScroll4 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			double raprobe = Convert.ToDouble(HC.HScroll4.Value);
			raprobe *= 100;

			//UPGRADE_TODO: (1067) Member HScroll2 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			double vh = Convert.ToDouble(HC.HScroll2.Value);
			vh = (vh / 360d) * EQMath.gTot_step;

			//UPGRADE_TODO: (1067) Member HScroll3 is not defined in type Variant. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			double vy = 90 + Convert.ToDouble(HC.HScroll3.Value);
			vy = (vy / 360d) * EQMath.gTot_step;

			double RA1 = EQMath.RAEncoder_Home_pos + vh;
			double DEC1 = EQMath.gDECEncoder_Home_pos - vy;
			double RA2 = EQMath.RAEncoder_Home_pos + vh - (EQMath.gTot_step / 4d);
			double DEC2 = EQMath.gDECEncoder_Home_pos + vy;


			NStar_Polar_plot_init(Convert.ToInt32(pscale));
			NStar_Polar_plot(RA1, DEC1, RA2, DEC2, pscale);
			eqmodvector.Coord obtmp = PolarAlignDrift_Map(RA1, DEC1, RA2, DEC2, raprobe, pscale);
			//    HC.Label62.Caption = Format(obtmp.x * 0.0024, "####0.0000000000")   '.144 * 60
			//    HC.Label64.Caption = Format(obtmp.y * 0.0024, "####0.0000000000")


		}
	}
}