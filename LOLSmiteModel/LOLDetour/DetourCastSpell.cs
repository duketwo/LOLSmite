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
using WhiteMagic;


namespace LOLSmiteModel
{
	/// <summary>
	/// Description of Detour.
	/// </summary>
	public class DetourCastSpell : IHook
	{
		public uint CastSpellPointer;
		public CastSpell CastSpellDelegate;
		
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void CastSpell(uint param1, uint param2, uint param3, uint param4, uint param5);
		
		public DetourCastSpell()
		{
			
			
			
			try {
				
				CastSpellPointer = Memory.LOLBaseAddress+Offsets.CastSpell;
				
				CastSpellDelegate = (CastSpell)Marshal.GetDelegateForFunctionPointer(new IntPtr(CastSpellPointer), typeof(CastSpell));
				Memory.GetMagic.Detours.CreateAndApply(CastSpellDelegate,new CastSpell(this.castSpellDetour),"castSpell");

				
			} catch (Exception ex) {
				
				Frame.Log(ex.ToString());
			}
			
			
		}
		
		private unsafe void castSpellDetour(uint param1, uint param2, uint param3, uint param4, uint param5)
		{
			try
			{
				param1 = *(uint*) (*(uint*)(Memory.LOLBaseAddress+Offsets.LocalPlayer) + Offsets.Champion_SCI);
				uint spellSlot = *(uint*)(param1+0xC);
				
				Vector3 targetPos = Memory.ReadStruct<Vector3>(*(uint*)(Memory.LOLBaseAddress+Offsets.LocalPlayer)+Offsets.Position);

				Frame.Log("p1: " + param1.ToString("X") + " p2: " + param2.ToString("X") + " p3: " + param3.ToString("X") + " p4: " + param4.ToString("X") + " p5: " + param5.ToString("X") + " spellSlot: " + spellSlot);
				
				using (var s = new StructWrapper<Vector3>(targetPos)){
					Memory.GetMagic.Detours["castSpell"].CallOriginal(param1,s.Ptr,s.Ptr,param4,param5);
				}
				
			}
			catch (Exception ex)
			{
				
				Frame.Log(ex.ToString());
			}
			
			
		}
		
		public void Dispose()
		{
			Memory.GetMagic.Detours["castSpell"].Dispose();
		}
		
	}
}
