using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace picturethis
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("data source=localhost;uid=sa;password=;database=picturethis");
        SqlCommand com;
        SqlDataAdapter ad;
        FileStream fs;
        byte[] rawdata;
        DataSet ds = new DataSet();
        int i = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInsertImage_Click(object sender, EventArgs e)
        {
            //To show the File Dialog to get the path of the image

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.ShowDialog();

            fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);

            rawdata = new byte[Convert.ToInt32(fs.Length)];

            fs.Read(rawdata, 0, Convert.ToInt32(fs.Length));

            con.Open();

            com = new SqlCommand("INSERT INTO picturethis(Picture) VALUES(@k)", con);

            com.Parameters.AddWithValue("@k", rawdata);

            com.ExecuteNonQuery();

            con.Close();

            pictureBox1.Image = Image.FromStream(new MemoryStream((byte[])rawdata));
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            con.Open();

            ad = new SqlDataAdapter("SELECT Picture FROM Picturethis", con);

            ad.Fill(ds);

            con.Close();

            int k = ds.Tables[0].Rows.Count;

            i++;

            if (i < k)

                pictureBox1.Image = Image.FromStream(new MemoryStream((byte[])ds.Tables[0].Rows[i].ItemArray[0]));

            else
            {

                i = 0;

                pictureBox1.Image = Image.FromStream(new MemoryStream((byte[])ds.Tables[0].Rows[i].ItemArray[0]));

            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            con.Open();

            ad = new SqlDataAdapter("SELECT Picture FROM Picturethis", con);

            ad.Fill(ds);

            con.Close();

            int k = ds.Tables[0].Rows.Count;

            i--;

            if (i >= 0)

                pictureBox1.Image = Image.FromStream(new MemoryStream((byte[])ds.Tables[0].Rows[i].ItemArray[0]));

            else
            {

                i = k;

                
                
                
                
                pictureBox1.Image = Image.FromStream(new MemoryStream((byte[])ds.Tables[0].Rows[i].ItemArray[0]));

            }
        }
    }
}
