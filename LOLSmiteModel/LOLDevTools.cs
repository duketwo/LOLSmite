/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 04.12.2014
 * Time: 04:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of LOLDevTools.
	/// </summary>
	public class LOLDevTools
	{
		public LOLDevTools()
		{
		}
		
		public void LogObjects()
		{
			Frame.Log(" .... ");
			foreach(LOLObject obj in Frame.Client.GetLOLObjects)
			{
				PrintObjDetails(obj);
			}
		}
		
		public void LogNearestObjects(int n, ObjectType t = ObjectType.All)
		{
			Frame.Log(" .... ");
			foreach(LOLObject obj in Frame.Client.GetNearestLOLObjects(n,t)){
				PrintObjDetails(obj);
			}
		}
		
		public void PrintObjDetails(LOLObject obj)
		{
			Frame.Log("Name: " + obj.Name +" Address: " + obj.BaseAddress.ToString("X") + "  Type: " + obj.ObjectType.ToString() + " Dist: " + obj.Distance
			          + " ObjectTypeInt: " + obj.ObjectTypeInt + " IsDead: " + obj.IsDead + " IsEnemy: " + obj.IsEnemy );
		}
		
		public unsafe void LogInfo(){
			
			Frame.Log("LOLSmiteModel.Memory.LOLBaseAddress: " + LOLSmiteModel.Memory.LOLBaseAddress.ToString("X"));
			
			Frame.Log("Frame.Client.Me.BaseAddress: " + Frame.Client.Me.BaseAddress.ToString("X"));

			Frame.Log("Frame.Client.Me.SpellW.BaseAddress: " + Frame.Client.Me.SpellW.BaseAddress.ToString("X"));

			Frame.Log("Frame.Client.Me.Champion_SCIPointer + Offsets.SpellW: " + (Frame.Client.Me.Champion_SCIPointer + Offsets.SpellW).ToString("X"));
			
			Frame.Log("(*(uint*)(Frame.Client.Me.Champion_SCIPointer + Offsets.SpellW)).ToString(X): " + (*(uint*)(Frame.Client.Me.Champion_SCIPointer + Offsets.SpellW)).ToString("X"));

			Frame.Log("Frame.Client.Me.Champion_SCIPointer: " + Frame.Client.Me.Champion_SCIPointer.ToString("X"));

			Frame.Log("(*(uint*)(Frame.Client.Me.Champion_SCIPointer)).ToString(X): " + (*(uint*)(Frame.Client.Me.Champion_SCIPointer)).ToString("X") );

			Frame.Log("Frame.Client.Me.SpellR.Name: " + Frame.Client.Me.SpellR.Name);
			
			Frame.Log("Frame.Client.Me.Name: " + Frame.Client.Me.Name);

			Frame.Log("Frame.Client.Me.Summoner1.Name: " + Frame.Client.Me.Summoner1.Name);
			
			Frame.Log("Frame.Client.Me.Summoner2.Name: " + Frame.Client.Me.Summoner2.Name);

			Frame.Log("Frame.Client.Me.Summoner2.BaseAddress.ToString(X): " + Frame.Client.Me.Summoner2.BaseAddress.ToString("X"));

			Frame.Log("Frame.Client.Me.NetworkId: " + Frame.Client.Me.NetworkId.ToString("X"));

			Frame.Log("Frame.Client.Me.Level: " + Frame.Client.Me.Level);

			Frame.Log("Frame.Client.Me.SmiteDamage: " + Frame.Client.Me.SmiteDamage);

			Frame.Log("Frame.Client.Me.HasSmiteSummoner: " + Frame.Client.Me.HasSmiteSummoner);
			
			Frame.Log("Frame.Client.Me.SmiteSummoner.BaseAddress.ToString(X): " + Frame.Client.Me.SmiteSummoner.BaseAddress.ToString("X"));
			
			Frame.Log("Frame.Client.Me.SmiteSummoner.TimeStamp.ToString(): " + Frame.Client.Me.SmiteSummoner.TimeStamp.ToString());
		}
	}
}
