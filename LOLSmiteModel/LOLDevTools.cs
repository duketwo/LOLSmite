/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 04.12.2014
 * Time: 04:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;

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
			
			Frame.Log("(LOLSmiteModel.Memory.LOLBaseAddress+Offsets.HudManager).ToString(X): " + (*(uint*)(LOLSmiteModel.Memory.LOLBaseAddress+Offsets.HudManager)).ToString("X"));
			
			Frame.Log("(*(uint*)(*(uint*)(LOLSmiteModel.Memory.LOLBaseAddress+Offsets.HudManager))).ToString(X): " + ( ((*(uint*)(*(uint*)(LOLSmiteModel.Memory.LOLBaseAddress+Offsets.HudManager)))) ).ToString("X"));
			
			
			Frame.Log("Frame.Client.ScreenCenterPosition.X: " + Frame.Client.ScreenCenterPositionWorld.X);
			Frame.Log("Frame.Client.ScreenCenterPosition.Y: " + Frame.Client.ScreenCenterPositionWorld.Y);
			
			
			Frame.Log("Frame.Client.Me.Position.X: " + Frame.Client.Me.Position.X);
			Frame.Log("Frame.Client.Me.Position.Y: " + Frame.Client.Me.Position.Y);
			Frame.Log("Frame.Client.Me.Position.Z: " + Frame.Client.Me.Position.Z);
			
			
			Frame.Log("Frame.Client.Me.Position.Subtract(Frame.Client.ScreenCenterPosition).X: " + Frame.Client.Me.Position.Subtract(Frame.Client.ScreenCenterPositionWorld).X);
			Frame.Log("Frame.Client.Me.Position.Subtract(Frame.Client.ScreenCenterPosition).Y: " + Frame.Client.Me.Position.Subtract(Frame.Client.ScreenCenterPositionWorld).Y);
			
			
			Frame.Log("Frame.Client.Me.GetViewPort().X " + Frame.Client.Me.ViewPort.X);
			Frame.Log("Frame.Client.Me.GetViewPort().Y " + Frame.Client.Me.ViewPort.Y);
			Frame.Log("Frame.Client.Me.GetViewPort().Z " + Frame.Client.Me.ViewPort.Z);
			
			
			Frame.Log("Frame.Client.Me.SpellQ.TimeStamp.ToString(): " + Frame.Client.Me.SpellQ.TimeStamp.ToString());
			
			Frame.Log("(*(uint*)(LOLSmiteModel.Memory.LOLBaseAddress+Offsets.GameClock)).ToString(X): " + (*(uint*)(LOLSmiteModel.Memory.LOLBaseAddress+Offsets.GameClock)).ToString("X") );
			
			Frame.Log("Gameclock: " + ( *(float*) ((*(uint*)(LOLSmiteModel.Memory.LOLBaseAddress+Offsets.GameClock))+0x2C)) );
			Frame.Log("Frame.Client.Me.Summoner2.TimeStamp: " +  Frame.Client.Me.Summoner2.TimeStamp );
			Frame.Log("Frame.Client.Me.Summoner2.IsReady: " +  Frame.Client.Me.Summoner2.IsReady );
			Frame.Log("Frame.Client.Me.Summoner2.TimeUntilReady: " +  Frame.Client.Me.Summoner2.TimeUntilReady );
			
			
			foreach(LOLWard obj in Frame.Client.GetLOLObjects.Where(s => s.ObjectType == ObjectType.Ward)){
				Frame.Log("Remaining Ward Time: " + obj.RemainingTime);
			}
			
			
			foreach(LOLPlayer obj in Frame.Client.GetLOLObjects.Where( s => s.ObjectType == ObjectType.Player)) {
				Frame.Log("Player: " + obj.Name);
			}
			
			
			Frame.Log("Frame.Client.Me.SpellQ.IsReady: " + Frame.Client.Me.SpellQ.IsReady);
			Frame.Log("Frame.Client.Me.SpellW.IsReady: " + Frame.Client.Me.SpellW.IsReady);
			Frame.Log("Frame.Client.Me.SpellE.IsReady: " + Frame.Client.Me.SpellE.IsReady);
			Frame.Log("Frame.Client.Me.SpellR.IsReady: " + Frame.Client.Me.SpellR.IsReady);
			
			Frame.Log("Frame.Client.Me.Summoner1.IsReady: " + Frame.Client.Me.Summoner1.IsReady);
			Frame.Log("Frame.Client.Me.Summoner2.IsReady: " + Frame.Client.Me.Summoner2.IsReady);
			
			
			Frame.Log("Frame.Client.Me.Summoner2.IsReady: " + Frame.Client.Me.Summoner1.BaseAddress.ToString("X"));
			
			
			
		}
	}
}

