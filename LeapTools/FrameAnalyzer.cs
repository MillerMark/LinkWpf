using Leap;
using System;
using System.Linq;

namespace LeapTools
{
	public delegate void LeapFrameEventHandler(object sender, LeapFrameEventArgs ea);
	class FrameAnalyzer : Listener
	{
		public event LeapFrameEventHandler HandsMoved;
		protected virtual void OnHandsMoved(object sender, LeapFrameEventArgs ea)
		{
			HandsMoved?.Invoke(sender, ea);
		}
		public override void OnInit(Controller controller)
		{
		}

		public override void OnConnect(Controller controller)
		{
		}

		public override void OnDisconnect(Controller controller)
		{
		}

		public override void OnExit(Controller controller)
		{
		}

		string lastMsg;
		LeapFrameEventArgs leapFrameEventArgs = new LeapFrameEventArgs();

		public override void OnFrame(Controller controller)
		{
			// Get the most recent frame and report some basic information
			Frame frame = controller.Frame();
			leapFrameEventArgs.Set(frame);
			OnHandsMoved(this, leapFrameEventArgs);

			////SafeWriteLine($"frame.Hands.Count: {frame.Hands.Count}");
			//string msg = "hands: " + frame.Hands.Count
			//							+ ", fingers: " + frame.Fingers.Count
			//							+ ", tools: " + frame.Tools.Count
			//							+ ", gestures: " + frame.Gestures().Count;
			//if (msg != lastMsg)
			//{
			//	lastMsg = msg;
			//}

			//foreach (Hand hand in frame.Hands)
			//{
			//	SafeWriteLine("  Hand id: " + hand.Id
			//				+ ", palm position: " + hand.PalmPosition);
			//	// Get the hand's normal vector and direction
			//	Vector normal = hand.PalmNormal;
			//	Vector direction = hand.Direction;

			//	// Calculate the hand's pitch, roll, and yaw angles
			//	SafeWriteLine("  Hand pitch: " + direction.Pitch * 180.0f / (float)Math.PI + " degrees, "
			//										+ "roll: " + normal.Roll * 180.0f / (float)Math.PI + " degrees, "
			//										+ "yaw: " + direction.Yaw * 180.0f / (float)Math.PI + " degrees");

			//	// Get the Arm bone
			//	Arm arm = hand.Arm;
			//	SafeWriteLine("  Arm direction: " + arm.Direction
			//										+ ", wrist position: " + arm.WristPosition
			//										+ ", elbow position: " + arm.ElbowPosition);

			//	// Get fingers
			//	foreach (Finger finger in hand.Fingers)
			//	{
			//		SafeWriteLine("    Finger id: " + finger.Id
			//							+ ", " + finger.Type.ToString()
			//							+ ", length: " + finger.Length
			//							+ "mm, width: " + finger.Width + "mm");

			//		// Get finger bones
			//		Bone bone;
			//		foreach (Bone.BoneType boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType)))
			//		{
			//			bone = finger.Bone(boneType);
			//			SafeWriteLine("      Bone: " + boneType
			//								+ ", start: " + bone.PrevJoint
			//								+ ", end: " + bone.NextJoint
			//								+ ", direction: " + bone.Direction);
			//		}
			//	}

			//}

			//// Get tools
			//foreach (Tool tool in frame.Tools)
			//{
			//	SafeWriteLine("  Tool id: " + tool.Id
			//						+ ", position: " + tool.TipPosition
			//						+ ", direction " + tool.Direction);
			//}

		}
	}
}
