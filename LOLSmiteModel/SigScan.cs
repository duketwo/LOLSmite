using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

//
// sigScan C# Implementation - Written by atom0s [aka Wiccaan]
// Class Version: 2.0.0
//
// [ CHANGE LOG ] -------------------------------------------------------------------------
//
//      2.0.0
//          - Updated to no longer require unsafe or fixed code.
//          - Removed unneeded methods and code.
//
//      1.0.0
//          - First version written and release.
//
// [ CREDITS ] ----------------------------------------------------------------------------
//
// sigScan is based on the FindPattern code written by
// dom1n1k and Patrick at GameDeception.net
//
// Full credit to them for the purpose of this code. I, atom0s, simply
// take credit for converting it to C#.
//
// [ USAGE ] ------------------------------------------------------------------------------
//
// Examples:
//
//      SigScan _sigScan = new SigScan();
//      _sigScan.Process = someProc;
//      _sigScan.Address = new IntPtr(0x123456);
//      _sigScan.Size = 0x1000;
//      IntPtr pAddr = _sigScan.FindPattern(new byte[]{ 0xFF, 0xFF, 0xFF, 0xFF, 0x51, 0x55, 0xFC, 0x11 }, "xxxx?xx?", 12);
//
//      SigScan _sigScan = new SigScan(someProc, new IntPtr(0x123456), 0x1000);
//      IntPtr pAddr = _sigScan.FindPattern(new byte[]{ 0xFF, 0xFF, 0xFF, 0xFF, 0x51, 0x55, 0xFC, 0x11 }, "xxxx?xx?", 12);
//
// ----------------------------------------------------------------------------------------
namespace LOLSmiteModel
{
    public class SigScan
    {
        /// <summary>
        /// ReadProcessMemory
        /// 
        ///     API import definition for ReadProcessMemory.
        /// </summary>
        /// <param name="hProcess">Handle to the process we want to read from.</param>
        /// <param name="lpBaseAddress">The base address to start reading from.</param>
        /// <param name="lpBuffer">The return buffer to write the read data to.</param>
        /// <param name="dwSize">The size of data we wish to read.</param>
        /// <param name="lpNumberOfBytesRead">The number of bytes successfully read.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out()] byte[] lpBuffer,
            int dwSize,
            out int lpNumberOfBytesRead
            );

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


        /// <summary>
        /// m_vDumpedRegion
        /// 
        ///     The memory dumped from the external process.
        /// </summary>
        private static byte[] m_vDumpedRegion;

        /// <summary>
        /// m_vProcess
        /// 
        ///     The process we want to read the memory of.
        /// </summary>
        private static Process m_vProcess;

        /// <summary>
        /// m_vAddress
        /// 
        ///     The starting address we want to begin reading at.
        /// </summary>
        private static IntPtr m_vAddress;

        /// <summary>
        /// m_vSize
        /// 
        ///     The number of bytes we wish to read from the process.
        /// </summary>
        private static long m_vSize;


        #region "sigScan Class Construction"
        /// <summary>
        /// SigScan
        /// 
        ///     Main class constructor that uses no params. 
        ///     Simply initializes the class properties and 
        ///     expects the user to set them later.
        /// </summary>
        public SigScan()
        {
            m_vProcess = null;
            m_vAddress = IntPtr.Zero;
            m_vSize = 0;
            m_vDumpedRegion = null;
        }
        /// <summary>
        /// SigScan
        /// 
        ///     Overloaded class constructor that sets the class
        ///     properties during construction.
        /// </summary>
        /// <param name="proc">The process to dump the memory from.</param>
        /// <param name="addr">The started address to begin the dump.</param>
        /// <param name="size">The size of the dump.</param>
        public SigScan(Process proc, IntPtr addr, long size)
        {
            m_vProcess = proc;
            m_vAddress = addr;
            m_vSize = size;
        }
        #endregion

        #region "sigScan Class Private Methods"
        /// <summary>
        /// DumpMemory
        /// 
        ///     Internal memory dump function that uses the set class
        ///     properties to dump a memory region.
        /// </summary>
        /// <returns>Boolean based on RPM results and valid properties.</returns>
        private bool DumpMemory()
        {
            try
            {
                // Checks to ensure we have valid data.
                if (m_vProcess == null)
                    return false;
                if (m_vProcess.HasExited == true)
                    return false;
                if (m_vAddress == IntPtr.Zero)
                    return false;
                if (m_vSize == 0)
                    return false;

                // Create the region space to dump into.
                m_vDumpedRegion = new byte[m_vSize];

                //Frame.Log("Size: " + m_vSize);

                bool bReturn = false;
                int nBytesRead = 0;

                IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, m_vProcess.Id);

                // Dump the memory.
                bReturn = ReadProcessMemory(
                    hProc, m_vAddress, m_vDumpedRegion, (int)m_vSize, out nBytesRead
                    );

                //Frame.Log("nBytesRead: " + nBytesRead);

                // Validation checks.
                if (bReturn == false || nBytesRead != m_vSize)
                    return false;

                //Frame.Log("Validated.");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// MaskCheck
        /// 
        ///     Compares the current pattern byte to the current memory dump
        ///     byte to check for a match. Uses wildcards to skip bytes that
        ///     are deemed unneeded in the compares.
        /// </summary>
        /// <param name="nOffset">Offset in the dump to start at.</param>
        /// <param name="btPattern">Pattern to scan for.</param>
        /// <param name="strMask">Mask to compare against.</param>
        /// <returns>Boolean depending on if the pattern was found.</returns>
        private bool MaskCheck(long nOffset, byte[] btPattern, string strMask)
        {
            // Loop the pattern and compare to the mask and dump.

            try
            {

                for (int x = 0; x < btPattern.Length; x++)
                {
                    // If the mask char is a wildcard, just continue.
                    if (strMask[x] == '?')
                        continue;


                    if (m_vDumpedRegion.Length <= nOffset - x)
                        return false;
                    // If the mask char is not a wildcard, ensure a match is made in the pattern.
                    if ((strMask[x] == 'x') && (btPattern[x] != m_vDumpedRegion[nOffset + x]))
                        return false;
                }

                // The loop was successful so we found the pattern.
                return true;


            }
            catch (Exception)
            {

                Frame.Log("MaskCheck exception.");
            }

            return false;
        }
        #endregion



        private byte[] ParseSig(string s)
        {
            try
            {
                s = s.Substring(1);
                s = s.Replace("x", "0x");
                byte[] data = s.Split('\\').Select(b => Convert.ToByte(b, 16)).ToArray();
                return data;

            }
            catch (Exception e)
            {
                
                Frame.Log("ParseSig exception.");
            }
            return new byte[] { };
        }

        #region "sigScan Class Public Methods"
        /// <summary>
        /// FindPattern
        /// 
        ///     Attempts to locate the given pattern inside the dumped memory region
        ///     compared against the given mask. If the pattern is found, the offset
        ///     is added to the located address and returned to the user.
        /// </summary>
        /// <param name="btPattern">Byte pattern to look for in the dumped region.</param>
        /// <param name="strMask">The mask string to compare against.</param>
        /// <param name="nOffset">The offset added to the result address.</param>
        /// <returns>IntPtr - zero if not found, address if found.</returns>
        public IntPtr FindPattern(string sig, string strMask, int nOffset)
        {
            try
            {

               // Frame.Log("Sig: " + sig);

                // parse the sig to byte array
                byte[] btPattern = ParseSig(sig);

                // Dump the memory region if we have not dumped it yet.
                if (m_vDumpedRegion == null || m_vDumpedRegion.Length == 0)
                {
                    if (!DumpMemory()) {
                        Frame.Log("Memory dump failed.");
                        return IntPtr.Zero;
                }
                }

                 // Ensure the mask and pattern lengths match.
                 if (strMask.Length != btPattern.Length)
                     return IntPtr.Zero;

                 // Loop the region and look for the pattern. 
                 for (long x = 0; x < m_vDumpedRegion.Length; x++)
                 {
                     if (MaskCheck(x, btPattern, strMask))
                     {
                         // The pattern was found, return it.
                         return new IntPtr((int)m_vAddress + (x + nOffset));
                     }
                 }

                 // Pattern was not found. 
                 return IntPtr.Zero;
            }
            catch (Exception ex)
            {
                Frame.Log("FindPattern exception.");
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// ResetRegion
        /// 
        ///     Resets the memory dump array to nothing to allow
        ///     the class to redump the memory.
        /// </summary>
        public void ResetRegion()
        {
            m_vDumpedRegion = null;
        }
        #endregion

        #region "sigScan Class Properties"
        public Process Process
        {
            get { return m_vProcess; }
            set { m_vProcess = value; }
        }
        public IntPtr Address
        {
            get { return m_vAddress; }
            set { m_vAddress = value; }
        }
        public long Size
        {
            get { return m_vSize; }
            set { m_vSize = value; }
        }
        #endregion

    }
}