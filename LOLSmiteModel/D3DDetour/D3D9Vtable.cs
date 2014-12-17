/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 12.12.2014
 * Time: 18:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace D3DDetour
{
	/// <summary>
	/// Description of D3D9Vtable.
	/// </summary>
	public class D3D9Vtable
	{
		public D3D9Vtable()
		{
		}
		
		public static  int Queryinterface_Index = 0;
		public static  int AddRef_Index = 1;
		public static  int Release_Index = 2;
		public static  int TestCooperativeLevel_Index = 3;
		public static  int GetAvailableTextureMem_Index = 4;
		public static  int EvictManagedResources_Index = 5;
		public static  int GetDirect3D_Index = 6;
		public static  int GetDeviceCaps_Index = 7;
		public static  int GetDisplayMode_Index = 8;
		public static  int GetCreationParameters_Index = 9;
		public static  int SetCursorProperties_Index = 10;
		public static  int SetCursorPosition_Index = 11;
		public static  int ShowCursor_Index = 12;
		public static  int CreateAdditionalSwapChain_Index = 13;
		public static  int GetSwapChain_Index = 14;
		public static  int GetNumberOfSwapChains_Index = 15;
		public static  int Reset_Index = 16;
		public static  int Present_Index = 17;
		public static  int GetBackBuffer_Index = 18;
		public static  int GetRasterStatus_Index = 19;
		public static  int SetDialogBoxMode_Index = 20;
		public static  int SetGammaRamp_Index = 21;
		public static  int GetGammaRamp_Index = 22;
		public static  int CreateTexture_Index = 23;
		public static  int CreateVolumeTexture_Index = 24;
		public static  int CreateCubeTexture_Index = 25;
		public static  int CreateVertexBuffer_Index = 26;
		public static  int CreateIndexBuffer_Index = 27;
		public static  int CreateRenderTarget_Index = 28;
		public static  int CreateDepthStencilSurface_Index = 29;
		public static  int UpdateSurface_Index = 30;
		public static  int UpdateTexture_Index = 31;
		public static  int GetRenderTargetData_Index = 32;
		public static  int GetFrontBufferData_Index = 33;
		public static  int StretchRect_Index = 34;
		public static  int ColorFill_Index = 35;
		public static  int CreateOffscreenPlainSurface_Index = 36;
		public static  int SetRenderTarget_Index = 37;
		public static  int GetRenderTarget_Index = 38;
		public static  int SetDepthStencilSurface_Index = 39;
		public static  int GetDepthStencilSurface_Index = 40;
		public static  int BeginScene_Index = 41;
		public static  int EndScene_Index = 42;
		public static  int Clear_Index = 43;
		public static  int SetTransform_Index = 44;
		public static  int GetTransform_Index = 45;
		public static  int MultiplyTransform_Index = 46;
		public static  int SetViewport_Index = 47;
		public static  int GetViewport_Index = 48;
		public static  int SetMaterial_Index = 49;
		public static  int GetMaterial_Index = 50;
		public static  int SetLight_Index = 51;
		public static  int GetLight_Index = 52;
		public static  int LightEnable_Index = 53;
		public static  int GetLightEnable_Index = 54;
		public static  int SetClipPlane_Index = 55;
		public static  int GetClipPlane_Index = 56;
		public static  int SetRenderState_Index = 57;
		public static  int GetRenderState_Index = 58;
		public static  int CreateStateBlock_Index = 59;
		public static  int BeginStateBlock_Index = 60;
		public static  int EndStateBlock_Index = 61;
		public static  int SetClipStatus_Index = 62;
		public static  int GetClipStatus_Index = 63;
		public static  int GetTexture_Index = 64;
		public static  int SetTexture_Index = 65;
		public static  int GetTextureStageState_Index = 66;
		public static  int SetTextureStageState_Index = 67;
		public static  int GetSamplerState_Index = 68;
		public static  int SetSamplerState_Index = 69;
		public static  int ValidateDevice_Index = 70;
		public static  int SetPaletteEntries_Index = 71;
		public static  int GetPaletteEntries_Index = 72;
		public static  int SetCurrentTexturePalette_Index = 73;
		public static  int GetCurrentTexturePalette_Index = 74;
		public static  int SetScissorRect_Index = 75;
		public static  int GetScissorRect_Index = 76;
		public static  int SetSoftwareVertexProcessing_Index = 77;
		public static  int GetSoftwareVertexProcessing_Index = 78;
		public static  int SetNPatchMode_Index = 79;
		public static  int GetNPatchMode_Index = 80;
		public static  int DrawPrimitive_Index = 81;
		public static  int DrawIndexedPrimitive_Index = 82;
		public static  int DrawPrimitiveUP_Index = 83;
		public static  int DrawIndexedPrimitiveUP_Index = 84;
		public static  int ProcessVertices_Index = 85;
		public static  int CreateVertexDeclaration_Index = 86;
		public static  int SetVertexDeclaration_Index = 87;
		public static  int GetVertexDeclaration_Index = 88;
		public static  int SetFVF_Index = 89;
		public static  int GetFVF_Index = 90;
		public static  int CreateVertexShader_Index = 91;
		public static  int SetVertexShader_Index = 92;
		public static  int GetVertexShader_Index = 93;
		public static  int SetVertexShaderantF_Index = 94;
		public static  int GetVertexShaderantF_Index = 95;
		public static  int SetVertexShaderantI_Index = 96;
		public static  int GetVertexShaderantI_Index = 97;
		public static  int SetVertexShaderantB_Index = 98;
		public static  int GetVertexShaderantB_Index = 99;
		public static  int SetStreamSource_Index = 100;
		public static  int GetStreamSource_Index = 101;
		public static  int SetStreamSourceFreq_Index = 102;
		public static  int GetStreamSourceFreq_Index = 103;
		public static  int SetIndices_Index = 104;
		public static  int GetIndices_Index = 105;
		public static  int CreatePixelShader_Index = 106;
		public static  int SetPixelShader_Index = 107;
		public static  int GetPixelShader_Index = 108;
		public static  int SetPixelShaderantF_Index = 109;
		public static  int GetPixelShaderantF_Index = 110;
		public static  int SetPixelShaderantI_Index = 111;
		public static  int GetPixelShaderantI_Index = 112;
		public static  int SetPixelShaderantB_Index = 113;
		public static  int GetPixelShaderantB_Index = 114;
		public static  int DrawRectPatch_Index = 115;
		public static  int DrawTriPatch_Index = 116;
		public static  int DeletePatch_Index = 117;
		public static  int CreateQuery_Index = 118;
		public static  int NumberOfFunctions = 118;
	}
}
