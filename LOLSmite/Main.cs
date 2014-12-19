/*
 * Created by SharpDevelop.
 * User: dserver
 * Date: 12.12.2013
 * Time: 00:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using D3DDetour;
using System.Threading;


namespace LOLSmite
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	///
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		/// 

        [STAThread]
		private static int EntryPoint(string args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var form = new MainForm();
			Application.Run(form);
			return 0;

		}
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            var form = new MainForm();
			Application.Run(form);

		}
	}
}
