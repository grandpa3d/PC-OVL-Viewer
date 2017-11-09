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
    class OArchive
    {
        public uint offset { get; private set; }
        public uint u1 { get; private set; }
        public uint u2 { get; private set; }
        public uint u3 { get; private set; }
        public UInt16 u4a { get; private set; }
        public UInt16 u4b { get; private set; }
        public uint zero1 { get; private set; }
        public uint u5 { get; private set; }
        public uint u6 { get; private set; }
        public uint u7 { get; private set; }
        public uint zero2 { get; private set; }
        public uint u8 { get; private set; }
        public uint compressedSize { get; private set; }
        public uint uncompressedSize { get; private set; }
        public uint zero3 { get; private set; }
        public uint u9 { get; private set; }
        public uint headerSize { get; private set; }
        public uint u10 { get; private set; }
        public string name { get; private set; }

        public OArchive(BinaryReader reader, string str)
        {
            offset = reader.ReadUInt32();
            u1 = reader.ReadUInt32();
            u2 = reader.ReadUInt32();
            u3 = reader.ReadUInt32();
            u4a = reader.ReadUInt16();
            u4b = reader.ReadUInt16();
            zero1 = reader.ReadUInt32();
            u5 = reader.ReadUInt32();
            u6 = reader.ReadUInt32();
            u7 = reader.ReadUInt32();
            zero2 = reader.ReadUInt32();
            u8 = reader.ReadUInt32();
            compressedSize = reader.ReadUInt32();
            uncompressedSize = reader.ReadUInt32();
            zero3 = reader.ReadUInt32();
            u9 = reader.ReadUInt32();
            headerSize = reader.ReadUInt32();
            u10 = reader.ReadUInt32();

            name = new FindName().name(str, (int)offset);
        }

        internal void archSave(int id, string fileName)
        {
            using (SqlConnection conn = new SqlConnection(ConnValue.connSt()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Archives ([Header_id],[fileName],[name],[offset],[u1],[u2],[u3],[u4a],[u4b],[zero1],[u5],[u6],[u7],[zero2],[u8],[compressedSize]," +
                        "[uncompressedSize],[zero3],[u9],[headerSize],[u10]) values(@id,@fileName,@name,@offset,@u1,@u2,@u3,@u4a,@u4b,@zero1,@u5,@u6,@u7,@zero2,@u8,@compressedSize," +
                        "@uncompressedSize,@zero3,@u9,@headerSize,@u10)";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@offset", (int)offset);
                    cmd.Parameters.AddWithValue("@u1", (int)u1);
                    cmd.Parameters.AddWithValue("@u2", (int)u2);
                    cmd.Parameters.AddWithValue("@u3", (int)u3);
                    cmd.Parameters.AddWithValue("@u4a", (int)u4a);
                    cmd.Parameters.AddWithValue("@u4b", (int)u4b);
                    cmd.Parameters.AddWithValue("@zero1", (int)zero1);
                    cmd.Parameters.AddWithValue("@u5", (int)u5);
                    cmd.Parameters.AddWithValue("@u6", (int)u6);
                    cmd.Parameters.AddWithValue("@u7", (int)u7);
                    cmd.Parameters.AddWithValue("@zero2", (int)zero2);
                    cmd.Parameters.AddWithValue("@u8", (int)u8);
                    cmd.Parameters.AddWithValue("@compressedSize", (int)compressedSize);
                    cmd.Parameters.AddWithValue("@uncompressedSize", (int)uncompressedSize);
                    cmd.Parameters.AddWithValue("@zero3", (int)zero3);
                    cmd.Parameters.AddWithValue("@u9", (int)u9);
                    cmd.Parameters.AddWithValue("@headerSize", (int)headerSize);
                    cmd.Parameters.AddWithValue("@u10", (int)u10);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
