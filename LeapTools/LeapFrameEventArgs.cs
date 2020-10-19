using Leap;
using System;
using System.Linq;
using System.Windows;
using System.Drawing;

namespace LeapTools
{
	//public class LeapData2d
	//{
	//	public Point LeftPalm { get; set; }
	//	public Point RightPalm { get; set; }
	//	public LeapData2d()
	//	{
	//		
	//	}
	//}
	public class LeapFrameEventArgs : EventArgs
	{

		public LeapFrameEventArgs()
		{

		}
		public void Set(Frame frame)
		{
			Frame = frame;
		}
		public Frame Frame { get; set; }
	}
}
