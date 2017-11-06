using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_OVL_Viewer
{
    class Type
    {
        public string name { get; private set; }
        public uint nameOffset { get; private set; }
        public uint unknown1 { get; private set; }
        public uint hash { get; private set; }
        public uint unknown2 { get; private set; }
        public uint unknown3 { get; private set; }
        public uint count { get; private set; }

        public Type(BinaryReader reader)
        {
            nameOffset = reader.ReadUInt32();
            unknown1 = reader.ReadUInt32();
            hash = reader.ReadUInt32();
            unknown2 = reader.ReadUInt32();
            unknown3 = reader.ReadUInt32();
            count = reader.ReadUInt32();
        }
    }
}
