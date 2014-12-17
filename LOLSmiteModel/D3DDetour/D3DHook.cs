using System;
using System.Runtime.InteropServices;

namespace D3DDetour
{
	public abstract class D3DHook
	{
		protected static readonly object _frameLock = new object();
		public static event EventHandler OnFrame;

		public delegate void OnFrameDelegate();
		public static event OnFrameDelegate OnFrameOnce;

		public abstract void Initialize();
		public abstract void Remove();

		protected void RaiseEvent()
		{
			lock (_frameLock)
			{
				if (OnFrame != null)
					OnFrame(null, new EventArgs());

				if (OnFrameOnce != null)
				{
					OnFrameOnce();
					OnFrameOnce = null;
				}
			}
		}
	}

	public enum D3DVersion
	{
		Direct3D9,
		Direct3D10,
		Direct3D10_1,
		Direct3D11,
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct DXGI_MODE_DESC
	{
		public int Width;
		public int Height;
		public Rational RefreshRate;
		public Format Format;
		public DisplayModeScanlineOrdering ScanlineOrdering;
		public DisplayModeScaling Scaling;
	}
	
	
	public struct Rational
	{
		public int Numerator;
		public int Denominator;
		public Rational(int numerator, int denominator)
		{
			this.Numerator = numerator;
			this.Denominator = denominator;
		}
	}
	
	
	public enum Format
	{
		BC7_UNorm_SRGB = 99,
		BC7_UNorm = 98,
		BC7_Typeless = 97,
		BC6_SFloat16 = 96,
		BC6_UFloat16 = 95,
		BC6_Typeless = 94,
		B8G8R8X8_UNorm_SRGB = 93,
		B8G8R8X8_Typeless = 92,
		B8G8R8A8_UNorm_SRGB = 91,
		B8G8R8A8_Typeless = 90,
		R10G10B10_XR_Bias_A2_UNorm = 89,
		B8G8R8X8_UNorm = 88,
		B8G8R8A8_UNorm = 87,
		B5G5R5A1_UNorm = 86,
		B5G6R5_UNorm = 85,
		BC5_SNorm = 84,
		BC5_UNorm = 83,
		BC5_Typeless = 82,
		BC4_SNorm = 81,
		BC4_UNorm = 80,
		BC4_Typeless = 79,
		BC3_UNorm_SRGB = 78,
		BC3_UNorm = 77,
		BC3_Typeless = 76,
		BC2_UNorm_SRGB = 75,
		BC2_UNorm = 74,
		BC2_Typeless = 73,
		BC1_UNorm_SRGB = 72,
		BC1_UNorm = 71,
		BC1_Typeless = 70,
		G8R8_G8B8_UNorm = 69,
		R8G8_B8G8_UNorm = 68,
		R9G9B9E5_SharedExp = 67,
		R1_UNorm = 66,
		A8_UNorm = 65,
		R8_SInt = 64,
		R8_SNorm = 63,
		R8_UInt = 62,
		R8_UNorm = 61,
		R8_Typeless = 60,
		R16_SInt = 59,
		R16_SNorm = 58,
		R16_UInt = 57,
		R16_UNorm = 56,
		D16_UNorm = 55,
		R16_Float = 54,
		R16_Typeless = 53,
		R8G8_SInt = 52,
		R8G8_SNorm = 51,
		R8G8_UInt = 50,
		R8G8_UNorm = 49,
		R8G8_Typeless = 48,
		X24_Typeless_G8_UInt = 47,
		R24_UNorm_X8_Typeless = 46,
		D24_UNorm_S8_UInt = 45,
		R24G8_Typeless = 44,
		R32_SInt = 43,
		R32_UInt = 42,
		R32_Float = 41,
		D32_Float = 40,
		R32_Typeless = 39,
		R16G16_SInt = 38,
		R16G16_SNorm = 37,
		R16G16_UInt = 36,
		R16G16_UNorm = 35,
		R16G16_Float = 34,
		R16G16_Typeless = 33,
		R8G8B8A8_SInt = 32,
		R8G8B8A8_SNorm = 31,
		R8G8B8A8_UInt = 30,
		R8G8B8A8_UNorm_SRGB = 29,
		R8G8B8A8_UNorm = 28,
		R8G8B8A8_Typeless = 27,
		R11G11B10_Float = 26,
		R10G10B10A2_UInt = 25,
		R10G10B10A2_UNorm = 24,
		R10G10B10A2_Typeless = 23,
		X32_Typeless_G8X24_UInt = 22,
		R32_Float_X8X24_Typeless = 21,
		D32_Float_S8X24_UInt = 20,
		R32G8X24_Typeless = 19,
		R32G32_SInt = 18,
		R32G32_UInt = 17,
		R32G32_Float = 16,
		R32G32_Typeless = 15,
		R16G16B16A16_SInt = 14,
		R16G16B16A16_SNorm = 13,
		R16G16B16A16_UInt = 12,
		R16G16B16A16_UNorm = 11,
		R16G16B16A16_Float = 10,
		R16G16B16A16_Typeless = 9,
		R32G32B32_SInt = 8,
		R32G32B32_UInt = 7,
		R32G32B32_Float = 6,
		R32G32B32_Typeless = 5,
		R32G32B32A32_SInt = 4,
		R32G32B32A32_UInt = 3,
		R32G32B32A32_Float = 2,
		R32G32B32A32_Typeless = 1,
		Unknown = 0
	}
	
	
	public enum DisplayModeScanlineOrdering
	{
		LowerFieldFirst = 3,
		UpperFieldFirst = 2,
		Progressive = 1,
		Unspecified = 0
	}
	
	public enum DisplayModeScaling
	{
		Stretched = 2,
		Centered = 1,
		Unspecified = 0
	}
}
