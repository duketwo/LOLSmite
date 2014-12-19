/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 13.12.2014
 * Time: 15:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;
using System.Text;


namespace LOLSmiteModel
{
	/// <summary>
	/// Description of StructWrapper.
	/// </summary>
	class StructWrapper<T> : IDisposable
	{
		public uint Ptr { get; private set; }

		public unsafe StructWrapper(T obj)
		{
			
			switch(Type.GetTypeCode(obj.GetType())) 
			{
				case TypeCode.Int32:
					Ptr = (uint)Marshal.AllocHGlobal(4);
					Marshal.WriteInt32(Ptr,0,Convert.ToInt32(obj));
					break;
				case TypeCode.String:
					Ptr = (uint)(char*)Marshal.StringToHGlobalAnsi(Convert.ToString(obj)).ToPointer();
					break;
				default:
					Ptr = (uint)Marshal.AllocHGlobal(Marshal.SizeOf(obj));
					Marshal.StructureToPtr(obj, new IntPtr(Ptr), false);
					break;
			}
		}
		
		~StructWrapper() {
			if (Ptr != default(uint)) {
				Marshal.FreeHGlobal(new IntPtr(Ptr));
				Ptr = default(uint);
			}
		}

		public void Dispose() {
			Marshal.FreeHGlobal(new IntPtr(Ptr));
			Ptr = default(uint);
			GC.SuppressFinalize(this);
		}

		public static implicit operator uint(StructWrapper<T> w) {
			return w.Ptr;
		}
	}
}
