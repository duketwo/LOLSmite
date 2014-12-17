/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 15.12.2014
 * Time: 08:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;

namespace LOLSmiteModel
{
	/// <summary>
	/// Description of Mathf.
	/// </summary>
	public class Mathf
	{
		public Mathf()
		{
		}
		public static float Sqrt(float z)
		{
			if (z == 0) return 0;
			FloatIntUnion u;
			u.tmp = 0;
			u.f = z;
			u.tmp -= 1 << 23; /* Subtract 2^m. */
			u.tmp >>= 1; /* Divide by 2. */
			u.tmp += 1 << 29; /* Add ((b + 1) / 2) * 2^m. */
			return u.f;
		}
		
		
		public static float SqrtInverse(float z)
		{
			if (z == 0) return 0;
			FloatIntUnion u;
			u.tmp = 0;
			float xhalf = 0.5f * z;
			u.f = z;
			u.tmp = 0x5f375a86 - (u.tmp >> 1);
			u.f = u.f * (1.5f - xhalf * u.f * u.f);
			return u.f * z;
		}
		
		[StructLayout(LayoutKind.Explicit)]
		private struct FloatIntUnion
		{
			[FieldOffset(0)]
			public float f;

			[FieldOffset(0)]
			public int tmp;
		}
		
		
		public static float SqrMagnitude(Vector3 a)
		{
			return a.X * a.X + a.Y * a.Y + a.Z * a.Z;
		}

	}
	
	[StructLayout(LayoutKind.Explicit)]
	public struct Vector3
	{
		[FieldOffset(0x0)]
		public float X;
		[FieldOffset(0x4)]
		public float Y;
		[FieldOffset(0x8)]
		public float Z;
		
		public Vector3(float x, float y, float z){
			this.X = x;
			this.Y = y;
			this.Z = z;
		}
		
		public Vector3 Subtract(Vector3 v){
			this.X -= v.X;
			this.Y -= v.Y;
			this.Z -= v.Z;
			return this;
		}
		
		public float Distance(Vector3 v)
		{
			double x = (double)(this.X - v.X);
			double y = (double)(this.Y - v.Y);
			double z = (double)(this.Z - v.Z);
			double xSquare = x * x;
			double ySquare = y * y;
			double zSquare = z * z;
			return (float)Math.Sqrt(ySquare + xSquare + zSquare);
		}
	}
}
