using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOLSmiteModel
{
    public class Signatures
    {
        public static List<Signature> SignatureList;

        static Signatures()
        {
            SignatureList = new List<Signature>();
            var castSpellSig = new Signature("CASTSPELL_FUNC", 0, @"\x8B\xFF\x55\x8B\xEC\x83\xE4\xF8\x83\xEC\x28\x53", "xxxxxxxxxxxx", 0);
            var printChatSig = new Signature("PRINTCHAT_FUNC", 0, @"\x8B\xFF\x55\x8B\xEC\x83\xE4\xF8\x6A\xFF\x68\x00\x00\x00\x00\x64\xA1\x00\x00\x00\x00\x50\x83\xEC\x44\xA1\x00\x00\x00\x00\x33\xC4\x89\x44\x24\x3C\x56\x57\xA1\x00\x00\x00\x00\x33\xC4\x50\x8D\x44\x24\x50\x64\xA3\x00\x00\x00\x00\x8B\xF1\x8B\x06", "xxxxxxxxxxx????xx????xxxxx????xxxxxxxxx????xxxxxxxxx????xxxx", 0);
            var moveToSig = new Signature("MOVETO_FUNC", 0, @"\x8B\x83\x00\x00\x00\x00\x8D\x8B\x00\x00\x00\x00\xC7\x44\x24\x00\x00\x00\x00\x00\x8B\x40\x04\xFF\xD0\x84\xC0\x0F\x85\x00\x00\x00\x00\x8A\x45\x18\x84\xC0\x74\x0E\x80\x7D\x14\x00\x74\x08\x32\xC0", "xx????xx????xxx?????xxxxxxxxx????xxxxxxxxxxxxxxx", -0x36);
            var viewPortSig = new Signature("VIEWPORT_FUNC", 0, @"\x56\x8B\xF1\x57\x8B\x48\x14\x8B\xFA\x8D\x55\xE4\x52\x8B\x01\xFF\x90\xE0\x00\x00\x00", "xxxxxxxxxxxxxxxxxx???", -0x1A);
            var localPlayerSig = new Signature("LOCALPLAYER", 1, @"\xFF\x35\x00\x00\x00\x00\x56\xE8\x00\x00\x00\x00\x0F\xB6\x47\x68\x83\xC4\x10\x8B\xCB\x50\x8D\x47\x20\x50\x8D\x47\x08\x50\xFF\x75\xFC\xFF\x75\xF8\xE8\x00\x00\x00\x00", "xx????xx????xxxxxxxxxxxxxxxxxxxxxxxxx????", 0);
            var objectManagerSig = new Signature("OBJECTMANAGER", 1, @"\x8B\x35\x00\x00\x00\x00\x2B\xCE\x83\xC1\x03\x89\x75\xD8\xC1\xE9\x02\x3B\x35\x00\x00\x00\x00\x57\x0F\x47\xCA\xC7\x45\x00\x00\x00\x00\x00\x89\x4D\xC8\x85\xC9\x0F\x84\x00\x00\x00\x00\x8D\x64\x24\x00\x8B\x1E\x8B\x83\x00\x00\x00\x00\x8D\xBB\x00\x00\x00\x00\x8B\xCF\x89\x5D\xCC\xFF\x50\x00\x3D\x00\x00\x00\x00\x0F\x84\x00\x00\x00\x00\x8B\x07", "xx????xxxxxxxxxxxxx????xxxxxx?????xxxxxxx????xxxxxxxx????xx????xxxxxxx?x????xx????xx", 0);
            var hudManagerSig = new Signature("HUDMANAGER", 1, @"\xC7\x05\x00\x00\x00\x00\x00\x00\x00\x00\x85\xC0\x75\x10\xFF\x35\x00\x00\x00\x00\xE8\x00\x00\x00\x00\x83\xC4\x04\xEB\x11\xFF\x35\x00\x00\x00\x00\xFF\x35\x00\x00\x00\x00\xFF\xD0\x83\xC4\x08\x8B\x0D\x00\x00\x00\x00\xC7\x05\x00\x00\x00\x00\x00\x00\x00\x00\xC7\x05\x00\x00\x00\x00\x00\x00\x00\x00\x85\xC9\x74\x06", "xx????????xxxxxx????x????xxxxxxx????xx????xxxxxxx????xx????????xx????????xxxx", 0);
            var gameClockSig = new Signature("GAMECLOCK", 1, @"\xFF\x35\x00\x00\x00\x00\xE8\x00\x00\x00\x00\x8B\x0D\x00\x00\x00\x00\x83\xC4\x04\x8B\x01\x68\x00\x00\x00\x00\x68\x00\x00\x00\x00\xFF\x50\x00\x8B\x4D\xF4\x64\x89\x0D\x00\x00\x00\x00\x59\x8B\xE5\x5D\xC3", "xx????x????xx????xxxxxx????x????xx?xxxxxx????xxxxx", 0);

            SignatureList.Add(castSpellSig);
            SignatureList.Add(printChatSig);
            SignatureList.Add(moveToSig);
            SignatureList.Add(viewPortSig);
            SignatureList.Add(localPlayerSig);
            SignatureList.Add(objectManagerSig);
            SignatureList.Add(hudManagerSig);
            SignatureList.Add(gameClockSig);

        }

        public static void PrintSignatures()
        {
            foreach (Signature s in Signatures.SignatureList)
            {
                Frame.Log("Name: " + s.Name + " Offset: " + s.GetOffset.ToString("X"));
            }

        }

    }



    public class Signature
    {
        public string Name { get; set; }
        private int Type { get; set; }
        public string Sig { get; set; }
        public string Mask { get; set; }
        public int SigOffset { get; set; }

        private uint Offset { get; set; }


        public Signature(string name, int type, string sig, string mask, int sigOffset)
        {
            this.Name = name;
            this.Type = type;
            this.Sig = sig;
            this.Mask = mask;
            this.SigOffset = sigOffset;

        }


        public uint GetOffset
        {
            get
            {

                if (this.Offset == default(uint))
                {
                    uint ptr = (uint)LOLSmiteModel.Memory.ScanSignature(this.Sig, this.Mask, this.SigOffset);
                    if (ptr != default(uint))

                        if (this.Type == 0)
                        {
                            this.Offset = ptr - Memory.LOLBaseAddress;
                        }
                        else
                        {
                            // read the 4 byte pointer, ignore the first two op code bytes
                            byte[] b = Memory.ReadBytes((uint)ptr + 0x2, 4);

                            // convert the result in reverse order to uint
                            this.Offset = (BitConverter.ToUInt32(b, 0) - Memory.LOLBaseAddress);
                        }
                }
                return this.Offset;
            }
        }
    }



}
