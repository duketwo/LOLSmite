/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 01.12.2014
 * Time: 18:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using LOLSmiteModel;
using System.Linq;





namespace LOLSmite
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		Frame frame;
		
		
		public MainForm()
		{
			InitializeComponent();
			
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			frame = new Frame();
			frame.OnFrame += OnLOLFrame;
			Frame.OnMessage += AddLog;
			this.checkBox1.Checked = true;
		}
		
		
		
		
		private static bool hook = false;
		private static uint counter = 0;
		public void OnLOLFrame(object sender,EventArgs e)
		{
			
			
			if(!hook) {
				hook = true;
				AddLog("Hooked D3D Endscene.");
				
		
				
			}
			
			
			if(button1clicked) {
				
				button1clicked = false;
				counter++;
			}
			
			if(button2clicked) {
				button2clicked = false;
				Frame.Client.DevTools.LogObjects();
				
			}
			
			if(button3clicked) {
				button3clicked = false;
				Frame.Client.DevTools.LogInfo();
			}
			
			if(checkBox1Value)
			{
				Frame.Client.Me.SmiteBuffs();
			}
			
			

		}
		

		void AddLog(string strMessage, Color? col = null){
			
			try {
				
				if (logbox.Items.Count >= 10000)
				{
					logbox.Items.Clear();
				}
				logbox.Items.Add(strMessage);

				if(logbox.Items.Count>1)
					logbox.SelectedIndex = logbox.Items.Count - 1;

			} catch (Exception) {
				
			}
		}
		
		private static bool button1clicked = false;
		void Button1Click(object sender, EventArgs e)
		{
			button1clicked = true;
		}
		
		private static bool button2clicked = false;
		void Button2Click(object sender, EventArgs e)
		{
			button2clicked = true;
			
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			Frame.OnMessage -= AddLog;
			frame.OnFrame -= OnLOLFrame;
			frame.Dispose();
		}
		
		private static bool button3clicked = false;
		void Button3Click(object sender, EventArgs e)
		{
			button3clicked = true;
		}
		
		private static bool checkBox1Value = false;
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			checkBox1Value = ((CheckBox)sender).Checked;
		}
	}



	

}
