using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Diagnostics;


namespace PC_OVL_Viewer
{
    class OVL
    {
        public Header header { get; }

        private BinaryReader reader;

        private int headerID;
        
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
                header = new Header(reader);
                validateHeader();
                header.save(fullPath);
                header.setHeaderID();
                headerID = header.headerID;
                StringTable st = new StringTable(reader, (int)(header.stringTableSize));
                st.stringTableSave(headerID);
            }
        }

        private void validateHeader()
        {
            if(header.fileFormatStr != "FRES")
            {
                reader.Close();
                MessageBox.Show("This is not a valid OVL file!\nThe file format String does not match");
            }
        }
    }
}

