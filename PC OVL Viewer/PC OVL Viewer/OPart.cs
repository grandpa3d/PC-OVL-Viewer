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
    class OPart
    {
        public uint hash { get; private set; }
        public uint offset { get; private set; }
        public uint u1 { get; private set; }
        public uint u2 { get; private set; }
        public uint u3 { get; private set; }
        public string name { get; private set; }
        
        public OPart(BinaryReader reader, string str)
        {
            hash = reader.ReadUInt32();
            offset = reader.ReadUInt32();
            u1 = reader.ReadUInt32();
            u2 = reader.ReadUInt32();
            u3 = reader.ReadUInt32();

            name = new FindName().name(str, (int)offset);
        }

        internal void partSave(int id, string fileName)
        {
            using (SqlConnection conn = new SqlConnection(ConnValue.connSt()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Parts ([Header_id],[fileName],[name],[hash],[offset],[u1],[u2],[u3]) values(@id,@fileName,@name,@hash,@offset,@u1,@u2,@u3)";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@hash", (int)hash);
                    cmd.Parameters.AddWithValue("@offset", (int)offset);
                    cmd.Parameters.AddWithValue("@u1", (int)u1);
                    cmd.Parameters.AddWithValue("@u2", (int)u2);
                    cmd.Parameters.AddWithValue("@u3", (int)u3);
                    cmd.ExecuteNonQuery();                }
            }
        }
    }
}
