/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 14.12.2014
 * Time: 06:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of LOLHookmanager.
	/// </summary>
	public class LOLHookManager : IDisposable
	{
		
		private List<IHook> ControllerList;
		
		public LOLHookManager()
		{
			ControllerList = new List<IHook>();
			
		}
		
		public void AddController(IHook controller)
		{
			if (!ControllerList.Contains(controller))
				ControllerList.Add(controller);
		}
		public void RemoveController(IHook controller)
		{
			ControllerList.Remove(controller);
		}
		
		public void DiposeControllers()
		{
			ControllerList.RemoveAll(s => true);	
		}
		
		
		public void Dispose(){
			DiposeControllers();
		}
	}
}
