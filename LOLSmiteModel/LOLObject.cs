/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 03.12.2014
 * Time: 18:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;


namespace LOLSmiteModel
{
	/// <summary>
	/// Description of LOLObject.
	/// </summary>
	public class LOLObject
	{
		
		public uint BaseAddress { get; set; }
		
		public unsafe LOLObject(uint baseAddress, bool rebased = false)
		{
			if (!rebased)
			{
				this.BaseAddress = *(uint*)(baseAddress);
			}
			else
			{
				this.BaseAddress = baseAddress;
			}
			
			 // update d3ddrawstring with current coordinates if exists
            if(LOLClient.D3dDrawStringList.ContainsKey(this.NetworkId)){
				
                D3DDrawString d3ds;
                if(LOLClient.D3dDrawStringList.TryGetValue(this.NetworkId,out d3ds)) {
                    if(LOLClient.D3dDrawStringList.TryRemove(this.NetworkId, out d3ds)){
                        d3ds.X = (int)ViewPort.X;
                        d3ds.Y = (int)ViewPort.Z;
                        LOLClient.D3dDrawStringList.TryAdd(this.NetworkId,d3ds);
						
                    }
                }
				
                if(this.IsDead)
                    if(LOLClient.D3dDrawStringList.TryRemove(this.NetworkId, out d3ds));
				
            }
			
		}
		
		public void SetD3dDrawString(string text){
			RemoveD3dDrawString();
			LOLClient.D3dDrawStringList.TryAdd(this.NetworkId,new D3DDrawString(text,(int)this.ViewPort.X,(int)this.ViewPort.Z,Color.Red));
		}
		
		public void RemoveD3dDrawString() {
			D3DDrawString d3ds;
			LOLClient.D3dDrawStringList.TryRemove(this.NetworkId,out d3ds);
		}
		
		public unsafe int Level
		{
			get{ return *(int*)(this.BaseAddress + Offsets.Level); }
		}
		
		public unsafe float Health
		{
			get{ return *(float*)(this.BaseAddress + Offsets.Health); }
		}
		
		public unsafe int NetworkId
		{
			get { return *(int*)(this.BaseAddress + Offsets.NetworkID);}
		}
		
		public unsafe bool IsDead
		{
			get{  return *(int*)(this.BaseAddress + Offsets.IsDead) == 658;}
		}
		
		public unsafe string Name
		{
			get
			{
				return *(int*)(BaseAddress + Offsets.NameLength) > 15 ? Memory.ReadString(*(uint*)(BaseAddress + Offsets.Name), Encoding.UTF8, 30) :
					Memory.ReadString(BaseAddress + Offsets.Name, Encoding.UTF8, 15);
			}
		}
		
		
		[DllImport("LOLBootstrap.dll", EntryPoint = "InvokeFastcall", CallingConvention = CallingConvention.StdCall)]
		private unsafe static extern int ViewPort_Stub(IntPtr address, uint vectorIn, uint vectorOut);
		
		public unsafe Vector3 ViewPort
		{
			get{
				
				Vector3 ret = new Vector3(0,0,0);
				Vector3 currentPos = this.Position;
				ViewPort_Stub(new IntPtr(Memory.LOLBaseAddress+Offsets.ViewPort), (uint)&currentPos , (uint)&ret);

				return ret;
			}
		}
		
		public unsafe string ChampionName
		{
			get { return  Memory.ReadString(*(uint*)(BaseAddress + Offsets.Name), Encoding.UTF8,15); }
		}
		
		public float SqrMagnitude { get { return Mathf.SqrMagnitude(Frame.Client.Me.Position.Subtract(this.Position)); } }
		
		public Vector3 Position { get { return Memory.ReadStruct<Vector3>(BaseAddress + Offsets.Position); } }
		
		public float Distance { get { return Frame.Client.Me.Position.Distance(this.Position); } }
		
		public unsafe int Team
		{
			get{  return *(int*)(this.BaseAddress + Offsets.Team);}
		}
		
		public bool IsEnemy
		{
			get { return Team != Frame.Client.Me.Team; }
		}
		
		public unsafe ObjectType ObjectType
		{
			get
			{
                if (this.Name.Equals("SightWard") || this.Name.Equals("VisionWard"))
                {
                    return ObjectType.Ward;
                }
				return (ObjectType)Enum.ToObject(typeof(ObjectType), *(int*)(BaseAddress + Offsets.Type));
			}
		}
		
		public unsafe int ObjectTypeInt {
			get
			{
				return *(int*)(BaseAddress + Offsets.Type);
			}
		}
		
	}
}

