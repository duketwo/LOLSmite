/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 03.12.2014
 * Time: 17:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using D3DDetour;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of Frame.
	/// </summary>
	public class Frame
	{
		
		public delegate void Message(string msg, Color? col);
		public static event Message OnMessage;
		const D3DVersion dxver = D3DVersion.Direct3D9;
		public event EventHandler OnFrame;
		public static LOLClient Client { get; private set; }
		protected static readonly object _frameLock = new object();
		public LOLHookManager hookManager;
		
		
		public Frame()
		{
			
			Pulse.Initialize(D3DVersion.Direct3D9);
			D3DHook.OnFrame += new EventHandler(OnD3DFrame);
			
			
			hookManager = new LOLHookManager();
			//hookManager.AddController(new DetourCastSpell());
			//hookManager.AddController(new DetourViewPort());
			//hookManager.AddController(new DetourFloatingText());
			
		}
		

		

		/// <summary>
		/// This method will be called for every frame captured by D3DDetour
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="lse"></param>
		/// 
		private void OnD3DFrame(object sender,EventArgs e)
		{
				lock (_frameLock)
				{
					using (LOLClient _client = new LOLClient())
					{
						Client = _client;
						if (OnFrame != null)
							OnFrame(this, new EventArgs());
						//Client = null;
						
					}
				}
		}
		public void Dispose()
		{
			if (Client != null) {
				Client.Dispose();
			}
			D3DHook.OnFrame -= new EventHandler(OnD3DFrame);
			Pulse.Shutdown();
			hookManager.Dispose();
			
		}
		/// <summary>
		/// Log
		/// </summary>
		/// <param name="text">Text to log</param>
		public static void Log(string text, Color? color = null)
		{
			try {
				if (OnMessage != null)
				{
					OnMessage(DateTime.UtcNow.ToString() + " " + text.ToString(), color);
				}
			} catch (Exception) {
				return;
			}
			
		}
	}
}
