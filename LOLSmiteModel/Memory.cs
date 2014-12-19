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
		
		
		private const string EXENAME = "League Of Legends";
		private static Magic magic;
		
		public static Magic GetMagic {
			get {
				if (magic==null)
					magic = new Magic();
				return magic;
			}
		}

        #region win32 imports
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
            ProcessAccessFlags processAccess,
            bool bInheritHandle,
            int processId
        );

        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }
        #endregion


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

        public static T Read<T>(uint address) where T : struct
        {
            return GetMagic.Read<T>(address);
        }

        public static byte[] ReadBytes(uint address, int c) {
            return GetMagic.ReadBytes(address, c);
        }
		
		public unsafe static string ReadString(uint address, Encoding encoding, uint maxLength = 512)
		{
			uint current = address;
			uint end = address + maxLength;
			byte[] buffer = new byte[maxLength];
			
			int cnt = 0;
			while(current < end) {
				buffer[current-address] = *(byte*)current;
				current++;
				cnt++;
			}
			
			string s = encoding.GetString(buffer);
			if (s.IndexOf('\0') != -1) {
				s = s.Remove(s.IndexOf('\0'));
			}

			return s;
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
			foreach (ProcessModule processModule in Process.GetProcessesByName(EXENAME)[0].Modules)
			{
				if (processModule.ModuleName.ToLower().Equals((EXENAME + ".exe").ToLower()))
				{
					result = uint.Parse(processModule.BaseAddress.ToString());
				}
			}
			return result;
		}

        public static IntPtr GetLOLOpenProcessHandle
        {
            get
            {
                return OpenProcess(ProcessAccessFlags.All, false, Process.GetProcessesByName(EXENAME)[0].Id);
            }
        }

        public static IntPtr ScanSignature(string sig, string mask, int offset)
        {
            SigScan sigScan = new SigScan(Process.GetProcessesByName(EXENAME)[0], new IntPtr(LOLBaseAddress), Process.GetProcessesByName(EXENAME)[0].MainModule.ModuleMemorySize);
            IntPtr sigPtr = sigScan.FindPattern(sig, mask, offset);
            return sigPtr;

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
			return FindProcess(EXENAME);
		}
		
		
	}
}
