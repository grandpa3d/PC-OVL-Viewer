using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_OVL_Viewer
{
    class OVL
    {
        public OHeader header { get; private set; }
        public OStringTable st { get; private set; }
        public string stringTable { get; private set; }
        public string archiveString { get; private set; }
        public OZlib zlib { get; private set; }

        private BinaryReader reader;
        private ODirectory dir;
        private OFile f;
        private OArchive arch;
        private int compressedSize;
        private OPart part;
        private OUnknown unknown;
        private OArchive2 arch2;
        private byte[] compressedData;

        public OVL(string fullPath)
        {   
            try
            {
                reader = new BinaryReader(File.OpenRead(fullPath));
            }
            catch
            {
                MessageBox.Show("Unable to open or read the specified file!");
            }
            finally
            {
                header = new OHeader(reader);
                validateHeader();
                header.save(fullPath);

                int headerID = header.headerID;

                st = new OStringTable(reader, (int)(header.stringTableSize));
                st.stringTableSave(headerID, header.fileName);
                stringTable = st.str;

                for (int i = 0; i < header.dirCount; i++)
                {
                    dir = new ODirectory(reader, stringTable);
                    dir.dirSave(headerID, header.fileName);
                }

                for (int j = 0; j < header.fileCount; j++)
                {
                    f = new OFile(reader, stringTable);
                    f.fileSave(headerID, header.fileName);
                }

                char[] chars = reader.ReadChars((int)header.archStringSize);
                archiveString = "";
                for (int k = 0; k < (int)header.archStringSize; k++)
                {
                    archiveString += chars[k];
                }

                for (int l = 0; l < (int)header.archiveCount; l++)
                {
                    arch = new OArchive(reader, archiveString);
                    arch.archSave(headerID, header.fileName);
                    if (l == 0)
                    {
                        compressedSize = (int)arch.compressedSize;
                    }
                }

                for (int m = 0; m < header.partCount; m++)
                {
                    part = new OPart(reader, stringTable);
                    part.partSave(headerID, header.fileName);
                }

                for (int n = 0; n < header.unknownCount; n++)
                {
                    unknown = new OUnknown(reader);
                    unknown.unknownSave(headerID, header.fileName);
                }

                for (int p = 0; p < header.archiveCount; p++)
                {
                    arch2 = new OArchive2(reader);
                    arch2.archive2Save(headerID, header.fileName);
                }

                compressedData = reader.ReadBytes(compressedSize);
                zlib = new OZlib(fullPath, compressedData);

                reader.Dispose();
            }
        }

        private void validateHeader()
        {
            if(header.fileFormatStr != "FRES")
            {
                reader.Dispose();
                MessageBox.Show("This is not a valid OVL file!\nThe file format String does not match");
            }
        }
    }
}

