using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;


namespace AppDomainHandler
{
	
	public class Program
	{
		public static int EntryPoint(string args){
			
			AppDomain currentDomain = AppDomain.CurrentDomain;
			AppDomain lolSmiteDomain = AppDomain.CreateDomain("LOLSmiteDomain");
			string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			lolSmiteDomain.ExecuteAssembly(assemblyFolder + "\\LOLSmite.exe");
			
			// unload after LOLSmite.exe has been closed
			AppDomain.Unload(lolSmiteDomain);
			
			return 0;
		}
	}
}
