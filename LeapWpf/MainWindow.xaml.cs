using LeapTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeapWpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		LeapMotion leapMotion = new LeapMotion();
		public MainWindow()
		{
			InitializeComponent();
			leapMotion.HandsMoved += LeapMotion_HandsMoved;
			hands = new Ellipse[] { hand1, hand2 };
		}

		bool handsAreHidden;

		void HideHands()
		{
			hand1.Visibility = Visibility.Hidden;
			hand2.Visibility = Visibility.Hidden;
			handsAreHidden = true;
		}
		void ShowHands()
		{
			hand1.Visibility = Visibility.Visible;
			hand2.Visibility = Visibility.Visible;
			handsAreHidden = false;
		}

		void SafeHideHands()
		{
			Dispatcher.Invoke(() =>
			{
				HideHands();
			});
		}

		void SafeShowHands()
		{
			Dispatcher.Invoke(() =>
			{
				ShowHands();
			});
		}

		float topMostY = float.MinValue;
		float forwardMostZ = float.MaxValue;
		float backMostZ = float.MinValue;
		float leftMostX = float.MaxValue;
		float rightMostX = float.MinValue;
		float bottomMostY = float.MaxValue;

		void AnalyzeBounds(Leap.Vector position)
		{
			if (position.x < leftMostX)
				leftMostX = position.x;
			if (position.y > topMostY)
				topMostY = position.y;
			if (position.x > rightMostX)
				rightMostX = position.x;
			if (position.y < bottomMostY)
				bottomMostY = position.y;
			if (position.z < forwardMostZ)
				forwardMostZ = position.z;
			if (position.z > backMostZ)
				backMostZ = position.z;
		}

		void AnalyzeMotionBounds(Leap.Frame frame)
		{
			foreach (Leap.Hand hand in frame.Hands)
			{
				AnalyzeBounds(hand.PalmPosition);
				foreach (Leap.Finger finger in hand.Fingers)
				{
					AnalyzeBounds(finger.TipPosition);
				}
			}
		}
		Point GetCanvasPosition(Leap.Vector position)
		{
			float percentageX = (position.x - leftMostX) / (rightMostX - leftMostX);
			float percentageY = (position.y - topMostY) / (bottomMostY - topMostY);
			float newX = (float)(percentageX * myCanvas.ActualWidth);
			float newY = (float)(percentageY * myCanvas.ActualHeight);
			return new Point(newX, newY);
		}

		Ellipse[] hands;
		void MoveHands(Leap.Frame frame)
		{
			for (int i = 0; i < frame.Hands.Count; i++)
			{
				Leap.Hand hand = frame.Hands[i];
				if (i < hands.Length)
				{
					Point position = GetCanvasPosition(hand.PalmPosition);
					Canvas.SetLeft(hands[i], position.X);
					Canvas.SetTop(hands[i], position.Y);
					float scale = Math.Abs((backMostZ - hand.PalmPosition.z) / (backMostZ - forwardMostZ));
					float newDiameter = 100 * scale + 40;
					hands[i].Width = newDiameter;
					hands[i].Height = newDiameter;
				}
				//foreach (Leap.Finger finger in hand.Fingers)
				//{
				//	AnalyzeBounds(finger.TipPosition);
				//}
			}
		}
		void SafeMoveHands(Leap.Frame frame)
		{
			Dispatcher.Invoke(() =>
			{
				MoveHands(frame);
			});
		}
		private void LeapMotion_HandsMoved(object sender, LeapFrameEventArgs ea)
		{
			if (ea.Frame.Hands.IsEmpty)
			{
				SafeHideHands();
				return;
			}
			if (handsAreHidden)
				SafeShowHands();
			AnalyzeMotionBounds(ea.Frame);

			SafeMoveHands(ea.Frame);
		}
	}
}
