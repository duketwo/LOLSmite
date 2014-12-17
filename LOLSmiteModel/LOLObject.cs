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
				return *(int*)(BaseAddress + Offsets.NameLength) > 15 ? Memory.ReadString(*(uint*)(BaseAddress + Offsets.Name), Encoding.UTF8, 20) :
					Memory.ReadString(BaseAddress + Offsets.Name, Encoding.UTF8, 15);
			}
		}
		
		
		public unsafe string ChampionName
		{
			get { return  Memory.ReadString(*(uint*)(BaseAddress + Offsets.Name), Encoding.UTF8,8); }
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
//				if(this.Name.ToLower().Equals("sightward") || this.Name.ToLower().Equals("visionward")) {
//					return ObjectType.Ward;
//				}
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

