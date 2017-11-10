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
            File.WriteAllBytes(fileName + ".ove", ZlibStream.UncompressBuffer(data));
        }

        public OZlib(string fileName)
        {
            File.WriteAllBytes(fileName + ".ove", ZlibStream.UncompressBuffer(File.ReadAllBytes(fileName)));
        }
    }
}
