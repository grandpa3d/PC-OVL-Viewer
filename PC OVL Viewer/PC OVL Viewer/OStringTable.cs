using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace PC_OVL_Viewer
{
    internal class OStringTable
    {
        public string str { get; private set; }

        public OStringTable(BinaryReader reader, int size)
        {
            str = "";
            char[] chars = reader.ReadChars(size);

            for(int i = 0; i < size; i++)
            {
                str += chars[i];
            }
        }

        internal void stringTableSave(int id, string name)
        {
            using (SqlConnection conn = new SqlConnection(ConnValue.connSt()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into StringTables ([header_id],[fileName],[string]) values(@headerID,@fileName,@str)";
                    cmd.Parameters.AddWithValue("@headerID", id);
                    cmd.Parameters.AddWithValue("@fileName", name);
                    cmd.Parameters.AddWithValue("@str", str);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}