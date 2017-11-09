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
    class OHeader
    {
        public string fileFormatStr { get; private set; }
        public byte gameFlag { get; private set; }
        public byte version { get; private set; }
        public UInt16 unknown1 { get; private set; }
        public uint unknown2 { get; private set; }
        public uint unknown3 { get; private set; }
        public uint stringTableSize { get; private set; }
        public uint unknown4 { get; private set; }
        public uint otherCount1 { get; private set; }
        public UInt16 otherCount2 { get; private set; }
        public UInt16 dirCount { get; private set; }
        public uint fileCount { get; private set; }
        public uint fileCount2 { get; private set; }
        public uint partCount { get; private set; }
        public uint archiveCount { get; private set; }
        public uint unknown5 { get; private set; }
        public uint unknown6 { get; private set; }
        public uint unknown7 { get; private set; }
        public uint unknown8 { get; private set; }
        public uint unknownCount { get; private set; }
        public uint unknown9 { get; private set; }
        public uint unknown10 { get; private set; }
        public uint unknown11 { get; private set; }
        public uint archStringSize { get; private set; }
        public uint fileCount3 { get; private set; }
        public uint dirNamesLength { get; private set; }
        public uint unknown12 { get; private set; }
        public uint unknown13 { get; private set; }
        public uint unknown14 { get; private set; }
        public uint unknown15 { get; private set; }
        public uint unknown16 { get; private set; }
        public uint unknown17 { get; private set; }
        public uint unknown18 { get; private set; }
        public uint unknown19 { get; private set; }
        public uint unknown20 { get; private set; }
        public uint unknown21 { get; private set; }
        public uint unknown22 { get; private set; }
        public uint unknown23 { get; private set; }
        public uint unknown24 { get; private set; }
        public int headerID { get; private set; }
        public string fileName { get; private set; }

        private SqlConnection conn;
        private SqlCommand cmd;
        private string directory;

        public OHeader(BinaryReader reader)

        {
            char[] fileFormat = reader.ReadChars(4);
            fileFormatStr = "";
            for (int i = 0; i < 4; i++)
            {
                fileFormatStr += fileFormat[i];
            }
            gameFlag = reader.ReadByte();
            version = reader.ReadByte();
            unknown1 = reader.ReadUInt16();
            unknown2 = reader.ReadUInt32();
            unknown3 = reader.ReadUInt32();
            stringTableSize = reader.ReadUInt32();
            unknown4 = reader.ReadUInt32();
            otherCount1 = reader.ReadUInt32();
            otherCount2 = reader.ReadUInt16();
            dirCount = reader.ReadUInt16();
            fileCount = reader.ReadUInt32();
            fileCount2 = reader.ReadUInt32();
            partCount = reader.ReadUInt32();
            archiveCount = reader.ReadUInt32();
            unknown5 = reader.ReadUInt32();
            unknown6 = reader.ReadUInt32();
            unknown7 = reader.ReadUInt32();
            unknown8 = reader.ReadUInt32();
            unknownCount = reader.ReadUInt32();
            unknown9 = reader.ReadUInt32();
            unknown10 = reader.ReadUInt32();
            unknown11 = reader.ReadUInt32();
            archStringSize = reader.ReadUInt32();
            fileCount3 = reader.ReadUInt32();
            dirNamesLength = reader.ReadUInt32();
            unknown12 = reader.ReadUInt32();
            unknown13 = reader.ReadUInt32();
            unknown14 = reader.ReadUInt32();
            unknown15 = reader.ReadUInt32();
            unknown16 = reader.ReadUInt32();
            unknown17 = reader.ReadUInt32();
            unknown18 = reader.ReadUInt32();
            unknown19 = reader.ReadUInt32();
            unknown20 = reader.ReadUInt32();
            unknown21 = reader.ReadUInt32();
            unknown22 = reader.ReadUInt32();
            unknown23 = reader.ReadUInt32();
            unknown24 = reader.ReadUInt32();
        }

        internal void save(string fullPath)
        {
            fileName = Path.GetFileName(fullPath);
            directory = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

            using (conn = new SqlConnection (ConnValue.connSt()))
            {
                conn.Open();
                using (cmd = conn.CreateCommand())
                { 
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT into Headers ([fileName],[filePath],[fileFormat],[gameFlag],[version],[u1],[u2],[u3],[stringTableSize],[u4],[otherCount],[dirCount]," +
                        "[typeCount],[filecount],[fileCount2],[partCount],[archiveCount],[u5],[u6],[u7],[u8],[unknownCount],[u9],[u10],[u11],[archNamesLength],[fileCount3]," +
                        "[typeNamesLength],[u12],[u13],[u14],[u15],[u16],[u17],[u18],[u19],[u20],[u21],[u22],[u23],[u24]) values(@fileName, @directory, @fileFormatStr, @gameFlag," +
                        "@version, @unknown1, @unknown2, @unknown3, @stringTableSize, @unknown4, @otherCount1, @otherCount2, @dirCount, @fileCount, @fileCount2, @partCount, @archiveCount," +
                        "@unknown5, @unknown6, @unknown7, @unknown8, @unknownCount, @unknown9, @unknown10, @unknown11, @archStringSize, @fileCount3, @dirNamesLength, @unknown12," +
                        "@unknown13, @unknown14, @unknown15, @unknown16, @unknown17, @unknown18, @unknown19, @unknown20, @unknown21, @unknown22, @unknown23, @unknown24)";

                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@directory", directory);
                    cmd.Parameters.AddWithValue("@fileFormatStr", fileFormatStr);
                    cmd.Parameters.AddWithValue("@gameFlag", gameFlag);
                    cmd.Parameters.AddWithValue("@version", version);
                    cmd.Parameters.AddWithValue("@unknown1", (int)unknown1);
                    cmd.Parameters.AddWithValue("@unknown2", (int)unknown2);
                    cmd.Parameters.AddWithValue("@unknown3", (int)unknown3);
                    cmd.Parameters.AddWithValue("@stringTableSize", (int)stringTableSize);
                    cmd.Parameters.AddWithValue("@unknown4", (int)unknown4);
                    cmd.Parameters.AddWithValue("@otherCount1", (int)otherCount1);
                    cmd.Parameters.AddWithValue("@otherCount2", (int)otherCount2);
                    cmd.Parameters.AddWithValue("@dirCount", (int)dirCount);
                    cmd.Parameters.AddWithValue("@fileCount", (int)fileCount);
                    cmd.Parameters.AddWithValue("@fileCount2", (int)fileCount2);
                    cmd.Parameters.AddWithValue("@partCount", (int)partCount);
                    cmd.Parameters.AddWithValue("@archiveCount", (int)archiveCount);
                    cmd.Parameters.AddWithValue("@unknown5", (int)unknown5);
                    cmd.Parameters.AddWithValue("@unknown6", (int)unknown6);
                    cmd.Parameters.AddWithValue("@unknown7", (int)unknown7);
                    cmd.Parameters.AddWithValue("@unknown8", (int)unknown8);
                    cmd.Parameters.AddWithValue("@unknownCount", (int)unknownCount);
                    cmd.Parameters.AddWithValue("@unknown9", (int)unknown9);
                    cmd.Parameters.AddWithValue("@unknown10", (int)unknown10);
                    cmd.Parameters.AddWithValue("@unknown11", (int)unknown11);
                    cmd.Parameters.AddWithValue("@archStringSize", (int)archStringSize);
                    cmd.Parameters.AddWithValue("@fileCount3", (int)fileCount3);
                    cmd.Parameters.AddWithValue("@dirNamesLength", (int)dirNamesLength);
                    cmd.Parameters.AddWithValue("@unknown12", (int)unknown12);
                    cmd.Parameters.AddWithValue("@unknown13", (int)unknown13);
                    cmd.Parameters.AddWithValue("@unknown14", (int)unknown14);
                    cmd.Parameters.AddWithValue("@unknown15", (int)unknown15);
                    cmd.Parameters.AddWithValue("@unknown16", (int)unknown16);
                    cmd.Parameters.AddWithValue("@unknown17", (int)unknown17);
                    cmd.Parameters.AddWithValue("@unknown18", (int)unknown18);
                    cmd.Parameters.AddWithValue("@unknown19", (int)unknown19);
                    cmd.Parameters.AddWithValue("@unknown20", (int)unknown20);
                    cmd.Parameters.AddWithValue("@unknown21", (int)unknown21);
                    cmd.Parameters.AddWithValue("@unknown22", (int)unknown22);
                    cmd.Parameters.AddWithValue("@unknown23", (int)unknown23);
                    cmd.Parameters.AddWithValue("@unknown24", (int)unknown24);
                    cmd.ExecuteNonQuery();
                }

                setHeaderID();
            }
        }

        internal void setHeaderID()
        {
            using (cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT id from Headers WHERE [fileName]=@fileName AND [filePath]=@directory";
                cmd.Parameters.AddWithValue("@fileName", fileName);
                cmd.Parameters.AddWithValue("@directory", directory);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        headerID = reader.GetInt32(0);
                    }
                }
            }
        }
    }
}
