/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 03.12.2014
 * Time: 17:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;


namespace LOLSmiteModel
{
	/// <summary>
	/// Description of LOLClient.
	/// </summary>
	/// 
	

	public class LOLClient : IDisposable
	{
		
		private ConcurrentBag<LOLObject> LOLObjectBag = new ConcurrentBag<LOLObject>();
		public LOLPlayer Me { get; set; }
		public LOLDevTools DevTools { get; set; }
		private static bool startUp;
		private static ConcurrentDictionary<int,D3DDrawString> _D3dDrawStringList = new ConcurrentDictionary<int,D3DDrawString>();
		
		public LOLClient()
		{
			DevTools = new LOLDevTools();
			Me = new LOLPlayer(Memory.LOLBaseAddress+Offsets.LocalPlayer);
			PopulateLOLObjects();
			StartUp();
			
		}
		
		private void StartUp()
		{
			if(!startUp) {
				startUp = true;
				PrintToChat("<font color='#19D1CE'>LOLSmite loaded. by duketwo ( seviers@gmail.com )</font>");
			}
			
			
		}

		public static ConcurrentDictionary<int, D3DDrawString> D3dDrawStringList {
			get {
				return _D3dDrawStringList;
			}
		}
		
		public ConcurrentBag<LOLObject> GetLOLObjects
		{
			get { return LOLObjectBag; }
		}
		
		public IEnumerable<LOLObject> GetNearestLOLObjects(int n, ObjectType t = ObjectType.All)
		{
			if(t == ObjectType.All)
				return GetLOLObjects.Where( s => s.BaseAddress != Frame.Client.Me.BaseAddress).OrderBy(s => s.SqrMagnitude).Take(n);
			return GetLOLObjects.Where( s => s.BaseAddress != Frame.Client.Me.BaseAddress && s.ObjectType == t ).OrderBy(s => s.SqrMagnitude).Take(n);
		}
		
		public unsafe Vector3 ScreenCenterPositionWorld {
			get {
				return Memory.ReadStruct<Vector3>(((*(uint*)(*(uint*)(LOLSmiteModel.Memory.LOLBaseAddress+Offsets.HudManager))) + 0x104));

			}
		}
		
		private unsafe void PopulateLOLObjects()
		{
			try
			{
				uint firstObj = *(uint*)(Memory.LOLBaseAddress + Offsets.ObjectManager);
				uint lastObj =  *(uint*)(Memory.LOLBaseAddress + Offsets.ObjectManager+0x4);
				
				while(firstObj < lastObj) {
					
					var obj =  new LOLObject(firstObj);
					
					switch(obj.ObjectType) {
						case ObjectType.Player:
							obj = new LOLPlayer(firstObj);
							break;
						case ObjectType.Minion:
							obj = new LOLMinion(firstObj);
							break;
						case ObjectType.Ward:
							obj = new LOLWard(firstObj);
							break;
					}
					
					if( obj.BaseAddress != Me.BaseAddress )
						LOLObjectBag.Add(obj);
					
					firstObj += 0x4;
				}
			}

			
			catch (Exception ex)
			{
				Frame.Log(ex.StackTrace);
			}
			
		}
		
		
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void PrintChat(uint objPointer, uint stringPointer, uint type);
		
		public unsafe void PrintToChat(string text, uint type = 4)
		{
			try
			{
				using (var s = new StructWrapper<string>(text)){
					var chat = (PrintChat)Marshal.GetDelegateForFunctionPointer(new IntPtr(Memory.LOLBaseAddress+Offsets.PrintChat), typeof(PrintChat));
					uint printArg = *(uint*)(Memory.LOLBaseAddress+0x300267C);
					chat(printArg,s.Ptr,type);
				}
				
			} catch (Exception ex)
			{
				Frame.Log(ex.StackTrace);
				
			}
		}
		
		public unsafe float GameClockTime
		{
			get { return ( *(float*) ((*(uint*)(LOLSmiteModel.Memory.LOLBaseAddress+Offsets.GameClock))+0x2C)); }
		}
	
		
		
		public int GetLOLObjectAmount()
		{
			return LOLObjectBag.Count;
		}
		
		public void Dispose()
		{
			
		}
	}
}
