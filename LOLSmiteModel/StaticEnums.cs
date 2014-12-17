/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 02.12.2014
 * Time: 15:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of StaticEnmus.
	/// </summary>
	/// 
	
	
	public enum ObjectType
	{
		All = 99999,
		Player = 5121,
		Minion = 3073,
		Ward = 1337,
		Nexus = 5,
		Turret1 = 131077,
		Turret2 = 9217,
		Spawn_Barracks = 5,
		Inhibitor = 1000,
		Unknown = 0
	}
	
	
	public class StaticEnums
	{
		
		public class JungleCreeps
		{
			public class Team
			{
				public class Blue
				{
					
					public static string BlueBuff { get { return "SRU_Blue1.1.1"; } }
					public static string RedBuff { get { return "SRU_Red4.1.1"; } }
					
				}

				public class Red
				{
					public static string BlueBuff { get { return "SRU_Blue7.1.1"; } }

					public static string RedBuff { get { return "SRU_Red10.1.1"; } }
					

				}

				public class Neutral
				{
					public static string Drake { get { return "SRU_Dragon6.1.1"; } }
					public static string Baron { get { return "SRU_Worm12.1.1"; } }
				}
				
				
			}
		}
	}
}
