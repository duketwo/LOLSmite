/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 02.12.2014
 * Time: 15:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using WhiteMagic;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of Memory.
	/// </summary>
	public static class Memory
	{
		
		
		private const string ExeName = "League Of Legends";
		private static Magic magic;
		
		public static Magic GetMagic {
			get {
				if (magic==null)
					magic = new Magic();
				return magic;
			}
		}
		
		public static T ReadStruct<T>(uint Address) where T: struct
		{
			try
			{
				return (T)Marshal.PtrToStructure(new IntPtr(Address), typeof(T));
			}
			catch (Exception e)
			{
				Frame.Log(e.ToString());
				return default(T);
			}
		}
		
		public unsafe static string ReadString(uint address, Encoding encoding, uint maxLength = 512)
		{
			uint current = address;
			uint end = address + maxLength;
			byte[] buffer = new byte[maxLength];
			
			while(current < end) {
				buffer[current-address] = *(byte*)current;
				current++;
			}
			
			string s = encoding.GetString(buffer);
			if (s.IndexOf('\0') != -1) {
				s = s.Remove(s.IndexOf('\0'));
			}

			return s;
		}
		
		public unsafe static string ReadString2(uint address, int len = 512){
			return new string((char*)((void*)address), 0, len);
		}
		
		/// <summary>
		/// Locates a process.
		/// </summary>
		/// <param name="ProcessName"></param>
		/// <returns></returns>
		private static Process FindProcess(String ProcessName)
		{
			Process proc = (from Process p in Process.GetProcesses()
			                where p.ProcessName.ToLower() == ProcessName.ToLower()
			                select p).FirstOrDefault();
			return proc;
		}

		/// <summary>
		/// Locates a module.
		/// </summary>
		/// <param name="proc"></param>
		/// <param name="ModuleName"></param>
		/// <returns></returns>
		private static ProcessModule FindModule(Process proc, String ModuleName)
		{
			if (proc == null)
				return null;

			ProcessModule mod = (from ProcessModule m in proc.Modules
			                     where m.ModuleName.ToLower() == ModuleName.ToLower()
			                     select m).FirstOrDefault();
			return mod;
		}
		/// <summary>
		/// Retrieves the LOL Base Address.
		/// </summary>
		/// <param name="proc"></param>
		/// <param name="ModuleName"></param>
		/// <returns></returns>
		
		private static uint GetLOLBaseAddress()
		{
			uint result = 0u;
			foreach (ProcessModule processModule in Process.GetProcessesByName(ExeName)[0].Modules)
			{
				if (processModule.ModuleName.ToLower().Equals((ExeName + ".exe").ToLower()))
				{
					result = uint.Parse(processModule.BaseAddress.ToString());
				}
			}
			return result;
		}
		
		private static uint _LOLBaseAddress;
		public static uint LOLBaseAddress{
			get {
				if(_LOLBaseAddress == default(uint))
					_LOLBaseAddress = GetLOLBaseAddress();
				return _LOLBaseAddress;
			}
		}

		
		public static Process GetLOLProc() {
			return FindProcess(ExeName);
		}
		
		
	}
}
