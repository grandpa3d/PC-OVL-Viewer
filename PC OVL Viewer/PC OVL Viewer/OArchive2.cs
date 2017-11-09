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
    class OArchive2
    {
        public uint u1 { get; private set; }
        public uint dataSize { get; private set; }

        public OArchive2(BinaryReader reader)
        {
            u1 = reader.ReadUInt32();
            dataSize = reader.ReadUInt32();
        }


        internal void archive2Save(int id, string fileName)
        {
            using (SqlConnection conn = new SqlConnection(ConnValue.connSt()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Archives2 ([Header_id],[fileName],[u1],[dataSize]) values(@id,@fileName,@u1,@dataSize)";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@u1", (int)u1);
                    cmd.Parameters.AddWithValue("@dataSize", (int)dataSize);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
