using System;
using Leap;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapTools
{
	public class LeapMotion
	{
		public event LeapFrameEventHandler HandsMoved;
		protected virtual void OnHandsMoved(object sender, LeapFrameEventArgs ea)
		{
			HandsMoved?.Invoke(sender, ea);
		}

		FrameAnalyzer listener;
		Controller controller;
		public LeapMotion()
		{
			listener = new FrameAnalyzer();
			controller = new Controller();
			controller.AddListener(listener);
			listener.HandsMoved += Listener_HandsMoved;
		}

		private void Listener_HandsMoved(object sender, LeapFrameEventArgs ea)
		{
			OnHandsMoved(this, ea);
		}

		~LeapMotion()
		{
			controller.RemoveListener(listener);
			controller.Dispose();
		}
	}
}
