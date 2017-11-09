using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_OVL_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OVLopenBtn_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Planet Coaster OVL Files|*.ovl";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog.FileName.Contains(".ovl"))
                {
                    if (!exists(openFileDialog.FileName))
                    {
                        OVL o = new OVL(openFileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show("This file has already been read into the database!");
                    }
                }
               Form1Update();
            }
        }

        private bool exists(string fullPath)
        {
            string fileName = Path.GetFileName(fullPath);
            string filePath = Path.GetDirectoryName(fullPath);
            Boolean test = false;
            using (SqlConnection conn = new SqlConnection (ConnValue.connSt()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT id from Headers WHERE [fileName]=@fileName AND [filePath]=@filePath";
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@filePath", filePath);
                    SqlDataReader results = cmd.ExecuteReader();
                    
                    if (results.HasRows)
                    {
                        test = true;
                    }
                }
            }
            return test;
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'oVLDataSet.Headers' table. You can move, or remove it, as needed.
            this.headersTableAdapter.Fill(this.oVLDataSet.Headers);
        }

        private void Form1Update()
        {
            this.headersTableAdapter.Fill(this.oVLDataSet.Headers);
        }
    }
}
