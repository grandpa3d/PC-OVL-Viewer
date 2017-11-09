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
    class OFile
    {
        public string name { get; private set; }
        public uint offset { get; private set; }
        public uint hash { get; private set; }
        public UInt16 u1 { get; private set; }
        public UInt16 type { get; private set; }

        public OFile(BinaryReader reader, string str)
        {
            offset = reader.ReadUInt32();
            hash = reader.ReadUInt32();
            u1 = reader.ReadUInt16();
            type = reader.ReadUInt16();

            name = new FindName().name(str, (int)offset);
        }

        internal void fileSave(int id, string fileName)
        {
            using (SqlConnection conn = new SqlConnection(ConnValue.connSt()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Files ([Header_id],[fileName],[name],[offset],[hash],[u1],[type]) values(@id,@fileName,@name,@offset,@hash,@u1,@type)";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@offset", (int)offset);
                    cmd.Parameters.AddWithValue("@hash", (int)hash);
                    cmd.Parameters.AddWithValue("@u1", (int)u1);
                    cmd.Parameters.AddWithValue("@type", (int)type);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
