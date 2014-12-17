/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 04.12.2014
 * Time: 04:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of LOLMinion.
	/// </summary>
	public class LOLMinion : LOLObject
	{
		public LOLMinion(uint baseAddress, bool rebased = false) : base(baseAddress, rebased)
		{
		}
	}
}
