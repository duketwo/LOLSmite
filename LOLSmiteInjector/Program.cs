/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 01.12.2014
 * Time: 16:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace LOLSmiteInterface {
	public class LOLSmiteInterface: MarshalByRefObject
	{
		public void Ping()
		{
		}
	}
}

namespace LOLSmiteInjector
{
	class Program
	{
		#region win32 imports / structs
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool OpenProcessToken(IntPtr ProcessHandle,
		                                    UInt32 DesiredAccess, out IntPtr TokenHandle);

		private static uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
		private static uint STANDARD_RIGHTS_READ = 0x00020000;
		private static uint TOKEN_ASSIGN_PRIMARY = 0x0001;
		private static uint TOKEN_DUPLICATE = 0x0002;
		private static uint TOKEN_IMPERSONATE = 0x0004;
		private static uint TOKEN_QUERY = 0x0008;
		private static uint TOKEN_QUERY_SOURCE = 0x0010;
		private static uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
		private static uint TOKEN_ADJUST_GROUPS = 0x0040;
		private static uint TOKEN_ADJUST_DEFAULT = 0x0080;
		private static uint TOKEN_ADJUST_SESSIONID = 0x0100;
		private static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
		private static uint TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
		                                        TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
		                                        TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
		                                        TOKEN_ADJUST_SESSIONID);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr GetCurrentProcess();

		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
		                                        out LUID lpLuid);
		
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);
		
		[DllImport("kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hModule);
		
		public const string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";
		public const string SE_AUDIT_NAME = "SeAuditPrivilege";
		public const string SE_BACKUP_NAME = "SeBackupPrivilege";
		public const string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";
		public const string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";
		public const string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";
		public const string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";
		public const string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";
		public const string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";
		public const string SE_DEBUG_NAME = "SeDebugPrivilege";
		public const string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";
		public const string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";
		public const string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";
		public const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
		public const string SE_INC_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";
		public const string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";
		public const string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";
		public const string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";
		public const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";
		public const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
		public const string SE_RELABEL_NAME = "SeRelabelPrivilege";
		public const string SE_REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";
		public const string SE_RESTORE_NAME = "SeRestorePrivilege";
		public const string SE_SECURITY_NAME = "SeSecurityPrivilege";
		public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
		public const string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";
		public const string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";
		public const string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";
		public const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";
		public const string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";
		public const string SE_TCB_NAME = "SeTcbPrivilege";
		public const string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";
		public const string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";
		public const string SE_UNDOCK_NAME = "SeUndockPrivilege";
		public const string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";

		[StructLayout(LayoutKind.Sequential)]
		public struct LUID
		{
			public UInt32 LowPart;
			public Int32 HighPart;
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool CloseHandle(IntPtr hHandle);

		public const UInt32 SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
		public const UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
		public const UInt32 SE_PRIVILEGE_REMOVED = 0x00000004;
		public const UInt32 SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;

		[StructLayout(LayoutKind.Sequential)]
		public struct TOKEN_PRIVILEGES
		{
			public UInt32 PrivilegeCount;
			public LUID Luid;
			public UInt32 Attributes;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct LUID_AND_ATTRIBUTES
		{
			public LUID Luid;
			public UInt32 Attributes;
		}

		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
		                                         [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
		                                         ref TOKEN_PRIVILEGES NewState,
		                                         UInt32 Zero,
		                                         IntPtr Null1,
		                                         IntPtr Null2);

		
		private const uint TH32CS_SNAPMODULE = 0x00000008;
		


		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("kernel32.dll", SetLastError=true, ExactSpelling=true)]
		static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
		                                    uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
		
		[DllImport("kernel32.dll", SetLastError=true)]
		static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
		
		
		[Flags]
		public enum AllocationType
		{
			Commit = 0x1000,
			Reserve = 0x2000,
			Decommit = 0x4000,
			Release = 0x8000,
			Reset = 0x80000,
			Physical = 0x400000,
			TopDown = 0x100000,
			WriteWatch = 0x200000,
			LargePages = 0x20000000
		}

		[Flags]
		public enum MemoryProtection
		{
			Execute = 0x10,
			ExecuteRead = 0x20,
			ExecuteReadWrite = 0x40,
			ExecuteWriteCopy = 0x80,
			NoAccess = 0x01,
			ReadOnly = 0x02,
			ReadWrite = 0x04,
			WriteCopy = 0x08,
			GuardModifierflag = 0x100,
			NoCacheModifierflag = 0x200,
			WriteCombineModifierflag = 0x400
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool WriteProcessMemory(
			IntPtr hProcess,
			IntPtr lpBaseAddress,
			IntPtr lpBuffer,
			int nSize,
			out IntPtr lpNumberOfBytesWritten);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress,
		                                        IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
		[DllImport("KERNEL32.DLL ")]
		public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
		[DllImport("KERNEL32.DLL ")]
		public static extern int Process32First(IntPtr handle, ref   ProcessEntry32 pe);
		[DllImport("KERNEL32.DLL ")]
		public static extern int Process32Next(IntPtr handle, ref   ProcessEntry32 pe);
		
		[DllImport("kernel32.dll")]
		static extern bool GetExitCodeThread(IntPtr hThread, out uint lpExitCode);
		
		[DllImport("kernel32.dll")]
		static extern bool Module32Next(IntPtr hSnapshot, ref ModuleEntry32 lpme);
		[DllImport("kernel32.dll")]
		static extern bool Module32First(IntPtr hSnapshot, ref ModuleEntry32 lpme);
		
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(
			ProcessAccessFlags processAccess,
			bool bInheritHandle,
			int processId
		);
		
		
		[StructLayout(LayoutKind.Sequential)]
		public struct ProcessEntry32
		{
			public uint dwSize;
			public uint cntUsage;
			public uint th32ProcessID;
			public IntPtr th32DefaultHeapID;
			public uint th32ModuleID;
			public uint cntThreads;
			public uint th32ParentProcessID;
			public int pcPriClassBase;
			public uint dwFlags;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szExeFile;
		}
		
		
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
		
		
		public struct ModuleEntry32
		{
			private const int MAX_PATH = 255;
			internal uint dwSize;
			internal uint th32ModuleID;
			internal uint th32ProcessID;
			internal uint GlblcntUsage;
			internal uint ProccntUsage;
			internal IntPtr modBaseAddr;
			internal uint modBaseSize;
			internal IntPtr hModule;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH + 1)]
			internal string szModule;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH+5)]
			internal string szExePath;
		}
		#endregion

		private const string PROCESS_NAME_INJECT = "League of Legends.exe";
		//private const string PROCESS_NAME_INJECT = "League of Legends.exe";
		private const string LOLBOOTSTRAP_DLLNAME = "LOLBootstrap.dll";
		private const string MANAGEDASSEMBLY_NAME = "AppDomainHandler.dll";
		private const string MANAGEDASSEMBLY_TYPE_NAME = "AppDomainHandler.Program";
		private const string MANAGEDASSEMBLY_METHOD_NAME = "EntryPoint";
		
		public static void Main(string[] args)
		{
			int pid = GetProcessIdByName(PROCESS_NAME_INJECT);
			if(pid>0){
				Console.WriteLine("Injected into: " + PROCESS_NAME_INJECT + " [" + pid + "]");
				InjectNativeNetBootstrap(pid);
			}
			Console.ReadKey(true);
		}

		
		public static void InjectNativeNetBootstrap(int pid) {
			
			EnableDebugPriviliges();
			
			// get the pid of the process
			
			
			if(pid == 0) return;
			
			// open the process
			IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, pid);
			
			// get current path
			string currentPath = Directory.GetCurrentDirectory();
			
			// get the LOLBOOTSTRAPDLLNAME absolute path
			string lolBootstrapDllPath = currentPath + Path.DirectorySeparatorChar + LOLBOOTSTRAP_DLLNAME;
			
			// get the pointer of the LoadLibraryW function
			IntPtr fnLoadLib = GetProcAddress(GetModuleHandle("Kernel32"),"LoadLibraryW");
			
			uint InjectBootstrap = InjectModule(hProc,fnLoadLib,lolBootstrapDllPath);
			
			// add the function offset to the base of the module in the remote process
			uint BootstrapBase = GetRemoteModuleHandle(pid, LOLBOOTSTRAP_DLLNAME);
			uint offset = GetFunctionOffset(LOLBOOTSTRAP_DLLNAME,"ImplantDotNetAssembly");
			uint fnImplantDotNetAssembly = BootstrapBase + offset;
			
			// build arguments for the .Net Programm, tokenized by tabulators
			string args = currentPath + Path.DirectorySeparatorChar + MANAGEDASSEMBLY_NAME + "\t" + MANAGEDASSEMBLY_TYPE_NAME + "\t" + MANAGEDASSEMBLY_METHOD_NAME + "\t" + ""; // last string = arg
			
			// inject the managed assembly in the remote process
			uint InjectManagedAssembly = InjectModule(hProc,(IntPtr)fnImplantDotNetAssembly,args);
			
			// get the pointer of the FreeLibrary function
			IntPtr fnFreeLib = GetProcAddress(GetModuleHandle("Kernel32"),"FreeLibrary");
			
			//  unload bootstrap out of the remote process
			IntPtr hThread = CreateRemoteThread(hProc,IntPtr.Zero,IntPtr.Zero,fnFreeLib,(IntPtr)BootstrapBase,0,IntPtr.Zero);
			
			// close the handle
			CloseHandle(hProc);
			
		}
		
		public unsafe static uint InjectModule(IntPtr hProc, IntPtr function, string arg){
			
			
			int argByteSize = arg.Length * sizeof(Char);
			
			// allocate memory in size of the argument
			IntPtr baseAddress = VirtualAllocEx(hProc,IntPtr.Zero,(uint)argByteSize,AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ReadWrite);
			
			// write the string into local memory
			IntPtr pArgStr = Marshal.StringToHGlobalUni(arg);
			
			// write the string into the remote memory
			IntPtr bytesWritten = IntPtr.Zero;
			Boolean isSucceeded = WriteProcessMemory(hProc, baseAddress, pArgStr, argByteSize, out bytesWritten);
			
			// make the remote process invoke the function
			IntPtr hThread = CreateRemoteThread(hProc,IntPtr.Zero,IntPtr.Zero,function,baseAddress,0,IntPtr.Zero);
			WaitForSingleObject(hThread,uint.MaxValue);
			
			// retrieve thread exit code
			uint Exitcode = default(uint);
			GetExitCodeThread(hThread, out Exitcode);
			
			// close the handle
			CloseHandle(hThread);
			
			// free the memory local and remote
			Marshal.FreeHGlobal(pArgStr);
			VirtualAllocEx(hProc, baseAddress, 0, AllocationType.Release, MemoryProtection.ExecuteReadWrite);
			
			return Exitcode;
			
		}
		
		
		public static int GetProcessIdByName(string processName)
		{

			IntPtr handle = CreateToolhelp32Snapshot(0x2, 0);
			if ((int)handle > 0)
			{
				List<ProcessEntry32> list = new List<ProcessEntry32>();
				ProcessEntry32 pe32 = new ProcessEntry32();
				pe32.dwSize = (uint)Marshal.SizeOf(pe32);
				int bMore = Process32First(handle, ref pe32);
				while (bMore == 1)
				{
					IntPtr temp = Marshal.AllocHGlobal((int)pe32.dwSize);
					Marshal.StructureToPtr(pe32, temp, true);
					ProcessEntry32 pe = (ProcessEntry32)Marshal.PtrToStructure(temp, typeof(ProcessEntry32));
					Marshal.FreeHGlobal(temp);
					list.Add(pe);
					bMore = Process32Next(handle, ref pe32);
				}
				CloseHandle(handle);
				foreach (ProcessEntry32 p in list)
				{
					if(p.szExeFile.Equals(processName))
						return (int)p.th32ProcessID;
				}
				
			}
			return 0;
		}
		
		
		
		public static void EnableDebugPriviliges()
		{
			IntPtr hToken;
			LUID luidSEDebugNameValue;
			TOKEN_PRIVILEGES tkpPrivileges;

			if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken))
			{
				return;
			}


			if (!LookupPrivilegeValue(null, SE_DEBUG_NAME, out luidSEDebugNameValue))
			{
				CloseHandle(hToken);
				return;
			}
			
			tkpPrivileges.PrivilegeCount = 1;
			tkpPrivileges.Luid = luidSEDebugNameValue;
			tkpPrivileges.Attributes = SE_PRIVILEGE_ENABLED;

			if (!AdjustTokenPrivileges(hToken,false,ref tkpPrivileges, 0,IntPtr.Zero,IntPtr.Zero))
			{
				throw new Exception();
			}
			CloseHandle(hToken);
		}
		
		
		public static uint GetFunctionOffset(string library, string procName)
		{
			IntPtr hModule = LoadLibrary(library);
			IntPtr hProc = GetProcAddress(hModule,procName);
			uint offset = (uint)hProc-(uint)hModule;
			FreeLibrary(hModule);
			return offset;
		}
		
		
		public static uint GetRemoteModuleHandle(int processId, string moduleName)
		{
			
			IntPtr handle = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE, (uint)processId);
			if ((int)handle > 0)
			{
				var list = new List<ModuleEntry32>();
				var pe32 = new ModuleEntry32();
				pe32.dwSize = (uint)Marshal.SizeOf(pe32);
				bool bMore = Module32First(handle, ref pe32);
				while (bMore)
				{
					IntPtr temp = Marshal.AllocHGlobal((int)pe32.dwSize);
					Marshal.StructureToPtr(pe32, temp, true);
					var pe = (ModuleEntry32)Marshal.PtrToStructure(temp, typeof(ModuleEntry32));
					Marshal.FreeHGlobal(temp);
					list.Add(pe);
					bMore = Module32Next(handle, ref pe32);
				}
				CloseHandle(handle);
				foreach (ModuleEntry32 p in list)
				{
					if(p.szModule.Equals(moduleName))
						return (uint)p.modBaseAddr;
				}
			}
			return 0x0;
		}
		
		
		
		
	}
	
	
	
}