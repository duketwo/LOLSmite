using System;
namespace D3DDetour
{
	public static class Pulse
	{
		public static D3DHook Hook = null;
		public static void Initialize(D3DVersion ver)
		{
			switch (ver)
			{
				case D3DVersion.Direct3D9:
					Hook = new D3D9();
					break;					
			}
			Hook.Initialize();
		}
		public static void Shutdown()
		{
			Hook.Remove();
		}
	}
}