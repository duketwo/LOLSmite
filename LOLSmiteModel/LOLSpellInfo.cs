/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 11.12.2014
 * Time: 05:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Text;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of LOLSpellInfo.
	/// </summary>
	public class LOLSpellInfo
	{
		public uint BaseAddress { get; set; }
		private uint SpellSlot { get; set; }
		public unsafe LOLSpellInfo(uint baseAddress, uint spellSlot, bool rebased = false)
		{
			if (!rebased)
			{
				this.BaseAddress = *(uint*)(baseAddress);
			}
			else
			{
				this.BaseAddress = baseAddress;
			}
			this.SpellSlot = spellSlot;
		}
		
		public unsafe string Name {
			get {
				//return Memory.ReadString( *(uint*)(BaseAddress+Offsets.SpellName)+0x18,Encoding.UTF8,5);
				return Encoding.UTF8.GetString(Memory.GetMagic.ReadBytes(*(uint*)(BaseAddress+Offsets.SpellName)+0x18,35));
			
			}
		}
		
		public unsafe int Level
		{
			get { return *(int*)(BaseAddress+Offsets.SpellLevel); }
		}
		
		public unsafe float TimeStamp
		{
			get { return *(float*)(BaseAddress+Offsets.TimeStamp); }
		}
		
		public bool IsReady {
			get {
				return (this.TimeStamp-Frame.Client.GameClockTime) <= 0;
			}
		}
		
		public float TimeUntilReady {
			get {
				
				return this.TimeStamp-Frame.Client.GameClockTime;
			}
		}
		
		
		public unsafe void Cast(Vector3 vec)
		{
			var castSpell = (DetourCastSpell.CastSpell)Marshal.GetDelegateForFunctionPointer(new IntPtr(Memory.LOLBaseAddress+Offsets.CastSpell), typeof(DetourCastSpell.CastSpell));
			*(uint*)(Frame.Client.Me.Champion_SCIPointer + Offsets.SpellSlot) = this.SpellSlot;
			castSpell(Frame.Client.Me.Champion_SCIPointer, (uint)&vec, (uint)&vec, 0x0, 0x0);
		}
		
		public unsafe void Cast(LOLObject obj){
			
			var castSpell = (DetourCastSpell.CastSpell)Marshal.GetDelegateForFunctionPointer(new IntPtr(Memory.LOLBaseAddress+Offsets.CastSpell), typeof(DetourCastSpell.CastSpell));
			*(uint*)(Frame.Client.Me.Champion_SCIPointer + Offsets.SpellSlot) = this.SpellSlot;
			castSpell(Frame.Client.Me.Champion_SCIPointer, obj.BaseAddress+Offsets.Position, obj.BaseAddress+Offsets.Position, (uint)obj.NetworkId, 0x0);
		}
		
		
	}
}
