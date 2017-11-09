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
    class OUnknown
    {
        public uint u1 { get; private set; }
        public uint u2 { get; private set; }
        public uint u3 { get; private set; }

        public OUnknown(BinaryReader reader)
        {
            u1 = reader.ReadUInt32();
            u2 = reader.ReadUInt32();
            u3 = reader.ReadUInt32();
        }

        internal void unknownSave(int id, string fileName)
        {
            using (SqlConnection conn = new SqlConnection(ConnValue.connSt()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Unknowns ([Header_id],[fileName],[u1],[u2],[u3]) values(@id,@fileName,@u1,@u2,@u3)";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@u1", (int)u1);
                    cmd.Parameters.AddWithValue("@u2", (int)u2);
                    cmd.Parameters.AddWithValue("@u3", (int)u3);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
