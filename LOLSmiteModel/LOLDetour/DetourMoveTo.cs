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
	public class DetourMoveTo : IHook
	{
		
		public moveTo MoveToDele;
		
		// void sub_8A60A0(int32_t a1, int32_t a2     , struct s0* a3, char a4,  char a5,  char a6)
		// void sub_8A60A0(int a1<ecx>, double a2<st0>, signed int a3, int a4, int a5, int a6, int a7, char a8)
		
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void moveTo(uint playerBaseAddress, uint moveType, uint vector3, uint p4, uint p5, uint p6);
		
		public DetourMoveTo()
		{
			
			
				
				try {
					
					IntPtr moveToPointer = new IntPtr(Memory.LOLBaseAddress+Offsets.MoveTo);
					MoveToDele = (moveTo)Marshal.GetDelegateForFunctionPointer(moveToPointer, typeof(moveTo));
					
					Memory.GetMagic.Detours.CreateAndApply(MoveToDele,new moveTo(this.MoveToDetour),"moveTo");
					
					
				} catch (Exception ex) {
					
					Frame.Log(ex.ToString());
				}
			
			
		}
		
		// with vec3 player.baseaddress, movetype = 2, vector3, 0, 1, 0
		
		private void MoveToDetour(uint p1, uint p2, uint p3, uint p4, uint p5, uint p6)
		{
			Frame.Log("p1: " + p1.ToString("X") + " p2: " + p2.ToString("X") + " p3: " + p3.ToString("X") + " p4: " + p4.ToString("X") + " p5: " + p5.ToString("X") + " p6: " + p6.ToString("X"));

			Memory.GetMagic.Detours["moveTo"].CallOriginal(p1,p2,p3,p4,p5,p6);
			
		}
		
		public void Dispose() {
			Memory.GetMagic.Detours["moveTo"].Dispose();
		}
	}
}
