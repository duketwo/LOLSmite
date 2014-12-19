using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
using SlimDX;
using SlimDX.Direct3D9;
using System.Diagnostics;
using LOLSmiteModel;

namespace D3DDetour
{
	public class D3D9 : D3DHook
	{
		[DllImport("d3d9.dll")]
		static extern IntPtr Direct3DCreate9(uint SDKVersion);
		private delegate int D3DEndscene(IntPtr intptr_0);
		private delegate void D3DRelease(IntPtr instance);
		private delegate int D3DCreateDevice(IntPtr instance, uint adapter, uint deviceType, IntPtr focusWindow, uint behaviorFlags, [In] ref D3D9.D3DPRESENT_PARAMETERS presentationParameters, out IntPtr returnedDeviceInterface);

		private D3D9.D3DEndscene endSceneDelegate;
		
		public IntPtr EndScenePointer = IntPtr.Zero;
		public IntPtr ResetPointer = IntPtr.Zero;
		public IntPtr ResetExPointer = IntPtr.Zero;
		public override void Initialize()
		{
			
			Form form = new Form();
			IntPtr iDirect3D9 = Direct3DCreate9(32u);
			if (iDirect3D9 == IntPtr.Zero)
			{
				throw new Exception("Failed to create D3D.");
			}
			D3D9.D3DPRESENT_PARAMETERS d3dPresentParams = new D3D9.D3DPRESENT_PARAMETERS
			{
				Windowed = true,
				SwapEffect = 1u,
				BackBufferFormat = 0u
			};
			
			var createDevice = (D3D9.D3DCreateDevice) Marshal.GetDelegateForFunctionPointer ( Marshal.ReadIntPtr(Marshal.ReadIntPtr(iDirect3D9), D3D9Vtable.Reset_Index*4), typeof(D3D9.D3DCreateDevice) );
			
			IntPtr device;
			if (createDevice(iDirect3D9, 0u, 1u, form.Handle, 32u, ref d3dPresentParams, out device) < 0)
			{
				throw new Exception("Failed to create device.");
			}
			
			this.EndScenePointer = Marshal.ReadIntPtr(Marshal.ReadIntPtr(device), D3D9Vtable.EndScene_Index*4);
			
			var deviceRelease = (D3D9.D3DRelease)Marshal.GetDelegateForFunctionPointer(Marshal.ReadIntPtr(Marshal.ReadIntPtr(device), D3D9Vtable.Release_Index*4), typeof(D3D9.D3DRelease));
			var d3dRelease = (D3D9.D3DRelease)Marshal.GetDelegateForFunctionPointer(Marshal.ReadIntPtr(Marshal.ReadIntPtr(iDirect3D9), D3D9Vtable.Release_Index*4), typeof(D3D9.D3DRelease));
			
			deviceRelease(device);
			d3dRelease(iDirect3D9);
			form.Dispose();
			
			this.endSceneDelegate = (D3D9.D3DEndscene)Marshal.GetDelegateForFunctionPointer(this.EndScenePointer, typeof(D3D9.D3DEndscene));
			
			LOLSmiteModel.Memory.GetMagic.Detours.CreateAndApply(endSceneDelegate,new D3D9.D3DEndscene(this.EndsceneDetour),"endscene");
		}
		
		private struct D3DPRESENT_PARAMETERS
		{
			#pragma warning disable
			public readonly uint BackBufferWidth;
			public readonly uint BackBufferHeight;
			public uint BackBufferFormat;
			public readonly uint BackBufferCount;
			public readonly uint MultiSampleType;
			public readonly uint MultiSampleQuality;
			public uint SwapEffect;
			public readonly IntPtr hDeviceWindow;
			[MarshalAs(UnmanagedType.Bool)]
			public bool Windowed;
			[MarshalAs(UnmanagedType.Bool)]
			public readonly bool EnableAutoDepthStencil;
			public readonly uint AutoDepthStencilFormat;
			public readonly uint Flags;
			public readonly uint FullScreen_RefreshRateInHz;
			public readonly uint PresentationInterval;
			#pragma warning restore
			

		}
		
		private static System.Drawing.Font drawFont = new System.Drawing.Font("Verdena", 20.0f, System.Drawing.FontStyle.Bold);
		private static Device device = null;
		public static Font font = null;
		
		private int EndsceneDetour(IntPtr intptr_0)
		{
			if(device == null)
				device = Device.FromPointer(intptr_0);
			if(font == null)
				font = new Font(device, drawFont);
			
		
				foreach(KeyValuePair<int,LOLSmiteModel.D3DDrawString> kv in LOLSmiteModel.LOLClient.D3dDrawStringList){

					font.DrawString(null, kv.Value.Text, kv.Value.X, kv.Value.Y, kv.Value.Color);

				}
			

			base.RaiseEvent();
			return (int)LOLSmiteModel.Memory.GetMagic.Detours["endscene"].CallOriginal(intptr_0);
		}
		public override void Remove()
		{
			LOLSmiteModel.Memory.GetMagic.Detours["endscene"].Dispose();
		}
	}
}