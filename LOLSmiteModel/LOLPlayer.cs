/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 03.12.2014
 * Time: 18:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of LOLPlayer.
	/// </summary>
	public class LOLPlayer : LOLObject
	{
		
		
		
		public LOLPlayer(uint baseAddress, bool rebased = false) : base(baseAddress, rebased)
		{
		}
		
		public unsafe uint Champion_SCIPointer
		{
			get { return *(uint*)(BaseAddress+Offsets.Champion_SCI); }
		}
		
		public LOLSpellInfo SpellQ
		{
			get { return new LOLSpellInfo(Champion_SCIPointer+Offsets.SpellQ, 0); }
		}
		
		public LOLSpellInfo SpellW
		{
			get { return new LOLSpellInfo(Champion_SCIPointer+Offsets.SpellW, 1); }
		}
		
		public LOLSpellInfo SpellE
		{
			get { return new LOLSpellInfo(Champion_SCIPointer+Offsets.SpellE, 2); }
		}
		
		public LOLSpellInfo SpellR
		{
			get { return new LOLSpellInfo(Champion_SCIPointer+Offsets.SpellR, 3); }
		}
		
		public LOLSpellInfo Summoner1
		{
			get { return new LOLSpellInfo(Champion_SCIPointer+Offsets.Summoner1, 4); }
		}
		
		public LOLSpellInfo Summoner2
		{
			get { return new LOLSpellInfo(Champion_SCIPointer+Offsets.Summoner2, 5); }
		}
		
		
		public double SmiteDamage
		{
			get
			{
				int level = Frame.Client.Me.Level;
				int[] d =
				{
					20*level + 370,
					30*level + 330,
					40*level + 240,
					50*level + 100
				};
				
				return d.Max();
			}
		}
		
		public bool HasSmiteSummoner {
			get
			{
				return Summoner1.Name.ToLower().Contains("smite") || Summoner1.Name.ToLower().Equals("smite");
			}
		}
		
		public LOLSpellInfo SmiteSummoner {
			get {
				return Summoner1.Name.ToLower().Contains("smite") ? Summoner1 : Summoner2;
			}
		}
		
		
		public void SmiteBuffs(){
			
			double smiteDamage = Frame.Client.Me.SmiteDamage;
			
			LOLObject buff = null;
			buff = Frame.Client.GetNearestLOLObjects(3,ObjectType.Minion).FirstOrDefault(s => (s.Name.Equals(StaticEnums.JungleCreeps.Team.Blue.RedBuff) ||
			                                                                                   s.Name.Equals(StaticEnums.JungleCreeps.Team.Blue.BlueBuff) ||
			                                                                                   s.Name.Equals(StaticEnums.JungleCreeps.Team.Red.RedBuff) ||
			                                                                                   s.Name.Equals(StaticEnums.JungleCreeps.Team.Red.BlueBuff) ||
			                                                                                   s.Name.Equals(StaticEnums.JungleCreeps.Team.Neutral.Drake) ||
			                                                                                   s.Name.Equals(StaticEnums.JungleCreeps.Team.Neutral.Baron) ) &&
			                                                                             s.Distance < 760 && s.Health < smiteDamage && !s.IsDead);

			if(buff != null) {
				if(HasSmiteSummoner)
					SmiteSummoner.Cast(buff);
				Frame.Log("Smite casted!");
			}
		}
		
		
		
		public void MoveTo(Vector3 pos){
			
			var moveTo = (DetourMoveTo.moveTo)Marshal.GetDelegateForFunctionPointer(new IntPtr(Memory.LOLBaseAddress+Offsets.MoveTo), typeof(DetourMoveTo.moveTo));
			using (var s = new StructWrapper<Vector3>(pos)){
				moveTo(BaseAddress,2,s.Ptr,0,1,0);
			}
			
		}
		
		
		public void ListSpells() {
			
			try {

				Frame.Log("Spellname: " + SpellQ.Name);
				
				
			} catch (Exception ex) {
				
				Frame.Log(ex.ToString());
			}
			
			
		}
	}
}