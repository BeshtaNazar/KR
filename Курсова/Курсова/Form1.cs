using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Курсова
{
    public partial class Form1 : Form
    {
        string s = null;
        Bitmap[,] bmps;
        PictureBox[,] arr;
        public Form1()
        {
            InitializeComponent();
            Draw();
        }
        private void Draw(string path = "123.jpg")
        {
            Image img = Image.FromFile(path);
            int widthThird = (int)((double)img.Width / 3.0);
            int heightThird = (int)((double)img.Height / 3.0);
            bmps = new Bitmap[3, 3];            
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    bmps[i, j] = new Bitmap(widthThird, heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird,
                        i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    g.Dispose();
                }                
            p0p0.Image = bmps[0, 0];
            p0p1.Image = bmps[0, 1];
            p0p2.Image = bmps[0, 2];
            p1p0.Image = bmps[1, 0];
            p1p1.Image = bmps[1, 1];
            p1p2.Image = bmps[1, 2];
            p2p0.Image = bmps[2, 0];
            p2p1.Image = bmps[2, 1];
            p2p2.Image = bmps[2, 2];
        }
        private void Random(PictureBox[,] arr)
        {
            Random r = new Random();
            int x = 0, y = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    x = r.Next(0, 2);
                    y = r.Next(0, 2);
                    Image img = arr[i, j].Image;
                    arr[i, j].Image = arr[x, y].Image;
                    arr[x, y].Image = img;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            arr = new PictureBox[,] { { p0p0, p0p1, p0p2 }, { p1p0, p1p1, p1p2 }, { p2p0, p2p1, p2p2 } };
            Random(arr);
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (s == null)
            {
                s = pb.Name;
                pb.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                Swap(s, pb.Name);
                PictureBox pic2 = (PictureBox)this.Controls.Find(s, false).First();
                pic2.BorderStyle = BorderStyle.FixedSingle;
                s = null;
            }
        }
        private void Swap(string from, string to)
        {
            PictureBox pb1 = (PictureBox)this.Controls.Find(from, false).First();
            PictureBox pb2 = (PictureBox)this.Controls.Find(to, false).First();
            Image img = pb1.Image;
            pb1.Image = pb2.Image;
            pb2.Image = img;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (bmps[i, j] == arr[i, j].Image)
                    {
                        count++;
                    }
                }
            }
            if (count == 9)
            {
                Form2 form2 = new Form2();
                form2.Show();
                timer1.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random(arr);
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Draw(openFileDialog.FileName); Random(arr);
                }
            }
        }
    }
}
