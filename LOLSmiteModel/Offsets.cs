/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 03.12.2014
 * Time: 15:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of Offsets.
	/// </summary>
	public class Offsets
	{
		// LOL

        public static uint ObjectManager { get { return 0x2FBDE34; } } // 4.22
        public static uint LocalPlayer { get { return 0x14C6290; } } // 4.22
        public static uint HudManager { get { return 0x14C6060; } } // 4.22
        public static uint GameClock { get { return 0x2FDB158; } } // 4.22


        //public static uint ObjectManager { get { return 0x2FF9D84; } } // 4.21
        //public static uint LocalPlayer { get { return 0x1500D88; } } // 4.21
        //public static uint HudManager { get { return 0x1501294; } } // 4.21
        //public static uint GameClock { get { return 0x30170A4; } } // 4.21
		
		// LOLObj
		
		public static uint Name { get { return 0x24; }}
		public static uint NameLength { get { return 0x34; }}
		public static uint ObjectName { get { return 0xB4; }}
		public static uint Health { get { return 0x140 ; } }
		public static uint MaxHealth { get { return 0x150 ; } }
		public static uint Mana { get { return 0x1AC ; } } 	//also Timelife for Wards
		public static uint MaxMana { get { return 0x1BC ; } }
		public static uint MovementSpeed { get { return 0x76C ; } }
		public static uint MaxMoveMentSpeed { get { return 0x770 ; } }
		public static uint BaseAttackDamage { get { return 0x734 ; } }
		public static uint BonusAttackDamge { get { return 0xA28 ; } }
		public static uint BonusAbilityPower { get { return 0xA48 ; } }
		public static uint ArmorResistance { get { return 0x750 ; } }
		public static uint MagicResistance { get { return 0x754 ; } }
		public static uint Gold { get { return 0xCE0 ; } }
		public static uint Team { get { return 0x18 ; } } // 100 = Team 1, 200 = Team2
		public static uint Position { get { return 0x60 ; } }
		public static uint Level { get { return 0x22CC; } }
		public static uint Type { get { return 0x1c; } }
		public static uint IsDead { get { return 0xB0; } }  //658 = dead, 643 = alive
		public static uint NetworkID { get { return 0xFC; } }
		public static uint Champion_SCI { get { return 0xD78; } } // Pointer to Spell -> Q,W,E,R -> SpellInfo
		
		
		// Functions

        public static uint FloatingText { get { return 0x883F00; } } // 4.22?
        public static uint PrintChat { get { return 0x287800; } }    // 4.22
        public static uint CastSpell { get { return 0x7DBDB0; } }    // 4.22
        public static uint MoveTo { get { return 0x49F300; } }    // 4.22
        public static uint ViewPort { get { return 0x4A64B0; } }    // 4.22


        //public static uint FloatingText { get { return 0x883F00; } } // 4.21
        //public static uint PrintChat { get { return 0xA8E320; } }    // 4.21 
        //public static uint CastSpell { get { return 0x6EEA50; } }    // 4.21
        //public static uint MoveTo { get { return 0x4A60A0; } }    // 4.21
        //public static uint ViewPort { get { return 0x65F060; } }    // 4.21
		
		
		// SpellInfo
		
		public static uint SpellLevel { get { return 0x10 ; } }		// int
		public static uint TimeStamp  { get { return 0x14 ; } } 	// float
		public static uint SpellName { get { return 0xD4 ; } }
		public static uint SpellSlot { get { return 0xC ; } }
		
		
		// Spell
		
		public static uint SpellQ  { get { return 0x4E8 ; } } 
		public static uint SpellW  { get { return 0x4EC ; } }
		public static uint SpellE { get { return 0x4F0 ; } } 
		public static uint SpellR  { get { return 0x4F4 ; } }
		public static uint Summoner1  { get { return 0x4F8 ; } }
		public static uint Summoner2  { get { return 0x4FC ; } }
		

		
	}
	
	
}
