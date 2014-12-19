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
	public class DetourFloatingText : IHook
	{
		
		
		
		public FloatingText FloatingTextDelegate;
		
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void FloatingText(uint p1, uint p2, uint p3, uint p4, uint p5, uint p6);
		
		
		// void __thiscall sub_C83F00(void *this, int a2, unsigned int a3, int a4, float a5, void *a6)
		
		public DetourFloatingText()
		{
			
			
			
			try {
				
				IntPtr floatingTextPointer = new IntPtr(Memory.LOLBaseAddress+Offsets.FloatingText);
				FloatingTextDelegate = (FloatingText)Marshal.GetDelegateForFunctionPointer(floatingTextPointer, typeof(FloatingText));
				
				Memory.GetMagic.Detours.CreateAndApply(FloatingTextDelegate,new FloatingText(this.PrintChatDetour),"floatingText");

				
				
			} catch (Exception ex) {
				
				Frame.Log(ex.ToString());
			}
			
			
		}
		
		private unsafe void PrintChatDetour(uint p1, uint p2, uint p3, uint p4, uint p5, uint p6)
		{
//			uint font = *(uint*)(0x21D4D2C);
			// draw(FONT,Frame.Client.Me.BaseAddress,10(type?) ,0, ( VALUE ) ,0);
			// FloatingTextType:
			// 1 = + YELLOW TEXT
			// 2 = + GREEN TEXT ()
			// 3 = blue text
			// 4 = blue text
			// ...
			// A =  + GOLD
			// 
			uint font = *(uint*)(0x2404D2C);
			Frame.Log("Font ptr: " + font.ToString("X"));
			
			Frame.Log("p1: " + p1.ToString("X") + " p2: " + p2.ToString("X") + " p3: " + p3.ToString("X") + " p4: " + p4.ToString("X") + " p5: " +
			          p5.ToString("X") + " p6: " + p6.ToString());
			
			Memory.GetMagic.Detours["floatingText"].CallOriginal(p1,p2,p3,p4,p5,p6);
			
		}
		
		public void Dispose(){
			Memory.GetMagic.Detours["floatingText"].Dispose();
		}
		
	}
}
