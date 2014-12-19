/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 11.12.2014
 * Time: 03:33
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
	/// Description of Detour.
	/// </summary>
	public class DetourPrintChat : IHook
	{
		
		// detour test
	
		public PrintChat printChatDelegate;
		
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void PrintChat(uint objPointer, uint charPointer, uint type);
		
		public DetourPrintChat()
		{
				try {
					
					IntPtr printChatPointer = new IntPtr(Memory.LOLBaseAddress+Offsets.PrintChat);
					printChatDelegate = (PrintChat)Marshal.GetDelegateForFunctionPointer(printChatPointer, typeof(PrintChat));
				
					Memory.GetMagic.Detours.CreateAndApply(printChatDelegate,new PrintChat(this.PrintChatDetour),"printChat");
				
				} catch (Exception ex) {
					
					Frame.Log(ex.ToString());
				}
			
			
		}
		
		private unsafe void PrintChatDetour(uint p1 ,uint p2, uint p3)
		{
			uint font = *(uint*)(Memory.LOLBaseAddress+0x300267C);
			
			char* stringPointer = (char*) Marshal.StringToHGlobalAnsi("hello managed world.").ToPointer();
			
			Frame.Log("p1: " + p1.ToString("X") + " p2: " + p2.ToString("X") + " p3: "  + p3.ToString("X"));
			
			Memory.GetMagic.Detours["printChat"].CallOriginal(p1,(uint)stringPointer,4);
			
		}
		
		public void Dispose(){
			Memory.GetMagic.Detours["printChat"].Dispose();
		}
		
	}
}
