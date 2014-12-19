/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 03.12.2014
 * Time: 18:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of LOLWard.
	/// </summary>
	public class LOLWard : LOLObject
	{
		public LOLWard(uint baseAddress, bool rebased = false) : base(baseAddress, rebased)
		{
			
			float remainingTime = this.RemainingTime;
			if(remainingTime > 0){
				this.SetD3dDrawString("T: " + RemainingTime);
			} else {
				this.RemoveD3dDrawString();
			}
			
			if(this.IsDead || remainingTime <= 0)
				this.RemoveD3dDrawString();
		}
		
		
		
		public unsafe float RemainingTime
		{
			get{  return *(float*)(this.BaseAddress + Offsets.Mana);}
		}
	}
}
