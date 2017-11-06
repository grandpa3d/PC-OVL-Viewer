using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace PC_OVL_Viewer
{
    internal class StringTable
    {
        private char[] str;
        
        public StringTable(BinaryReader reader, int size)
        {
            str = reader.ReadChars(size);
        }

        internal void stringTableSave(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnValue.connSt()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT into StringTables values('"+id+"','"+str+"')";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}