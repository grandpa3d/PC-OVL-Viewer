using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_OVL_Viewer
{
    class ODirectory
    {
        public string name { get; private set; }
        public uint offset { get; private set; }
        public uint unknown1 { get; private set; }
        public uint hash { get; private set; }
        public uint unknown2 { get; private set; }
        public uint prevDirFileCount { get; private set; }
        public uint fileCount { get; private set; }

        public ODirectory(BinaryReader reader, string str)
        {
            offset = reader.ReadUInt32();
            unknown1 = reader.ReadUInt32();
            hash = reader.ReadUInt32();
            unknown2 = reader.ReadUInt32();
            prevDirFileCount = reader.ReadUInt32();
            fileCount = reader.ReadUInt32();

            name =  new FindName().name(str, (int)offset);

        }

        internal void dirSave(int id, string fileName)
        {
            using (SqlConnection conn = new SqlConnection(ConnValue.connSt()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Directories ([Header_id],[fileName],[name],[offset],[u1],[hash],[u2],[prevDirFileCount],[fileCount]) values(@id,@fileName,@name,@offset," +
                        "@u1,@hash,@u2,@prevDirFileCount,@fileCount)";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@offset", (int)offset);
                    cmd.Parameters.AddWithValue("@u1", (int)unknown1);
                    cmd.Parameters.AddWithValue("@hash", (int)hash);
                    cmd.Parameters.AddWithValue("@u2", (int)unknown2);
                    cmd.Parameters.AddWithValue("@prevDirFileCount", (int)prevDirFileCount);
                    cmd.Parameters.AddWithValue("@fileCount", (int)fileCount);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
