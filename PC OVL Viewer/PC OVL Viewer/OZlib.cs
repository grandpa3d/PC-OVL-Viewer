using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zlib;

namespace PC_OVL_Viewer
{
    class OZlib
    {
        public OZlib(string fileName, byte[] data) 
        {
            //File.WriteAllBytes(path + ".out", ZlibStream.UncompressBuffer(o.GetCompressedData));
            File.WriteAllBytes(fileName + ".ove", ZlibStream.UncompressBuffer(data));
        }
    }
}
