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
using System.Windows.Forms;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of LOLClient.
	/// </summary>
	public class LOLClient : IDisposable
	{
		
		private ConcurrentBag<LOLObject> LOLObjectBag = new ConcurrentBag<LOLObject>();
		public LOLPlayer Me { get; set; }
		public LOLDevTools DevTools { get; set; }
		private static bool startUp;
		
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
				Frame.Log(ex.ToString());
				
			}
		}
		
		public unsafe void DrawFloatingText(LOLObject obj, string text, uint type = 0)
		{
			
			try
			{
				using (var s = new StructWrapper<string>(text)){
					
					//var draw = (DetourFloatingText.FloatingText)Marshal.GetDelegateForFunctionPointer(new IntPtr(Memory.LOLBaseAddress+Offsets.FloatingText), typeof(DetourFloatingText.FloatingText));
					
//					uint printArg = *(uint*)(0x21D4D2C);
//					uint printArg = *(uint*)(0x2404D2C);
//					uint printArg = *(uint*)(0x2674D2C); // was fine
					uint printArg = *(uint*)(Memory.LOLBaseAddress+0x300267C);
					
					// draw(FONT,Frame.Client.Me.BaseAddress,10(type?) ,0, ( VALUE ) ,0);
					// 1 = + YELLOW TEXT
					// 2 = + GREEN TEXT ( prob health )
					// 3 = blue text
					// 4 = blue text
					// 5 = 
					// 10 =  + GOLD
					// 
					
					// draw(printArg,Frame.Client.Me.BaseAddress,type,0,0,0);
				}
				
			} catch (Exception ex)
			{
				Frame.Log(ex.ToString());
				
			}
			
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
