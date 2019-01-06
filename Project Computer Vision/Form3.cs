using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Project_Computer_Vision
{
    public partial class Form3 : Form
    {
        Image<Bgr, byte> edit, ori;
        Image<Gray, byte> gray, canny;
        Image<Gray, float> grayconv, laplace, sobel;
        public Form3()
        {
            InitializeComponent();
            button2.Hide();
            groupBox1.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Aqua;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;

            pictureBox3.Image = canny.ToBitmap();
            button2.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            label2.ForeColor = Color.Aqua;
            label1.ForeColor = Color.White;
            label3.ForeColor = Color.White;

            pictureBox3.Image = laplace.ToBitmap();
            button2.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            label3.ForeColor = Color.Aqua;
            label2.ForeColor = Color.White;
            label1.ForeColor = Color.White;

            pictureBox3.Image = sobel.ToBitmap();
            button2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Image files(*.jpg;*.png)|*.jpg;*.png";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                pictureBox2.Image.Save(saveFileDialog1.FileName);

            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "Browse")
            {
                openFileDialog1.Filter = "Image files(*.jpg;*.png)|*.jpg;*.png";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image = new Bitmap(openFileDialog1.FileName);
                    ori = new Image<Bgr, byte>(new Bitmap(openFileDialog1.FileName));
                    edit = new Image<Bgr, byte>(ori.Width, ori.Height);
                    gray = new Image<Gray, byte>(ori.Width, ori.Height);
                    canny = new Image<Gray, byte>(ori.Width, ori.Height);
                    button1.Text = "Clear";
                    groupBox1.Show();

                    CvInvoke.CvtColor(ori, gray, ColorConversion.Bgr2Gray);
                    
                    edit = ori.Copy();
                    grayconv = gray.Convert<Gray, float>();


                    CvInvoke.Canny(gray, canny, 100, 10, 3);
                    sobel = grayconv.Sobel(1,0, (Int32)3);
                    laplace = grayconv.Laplace(3);

                    pictureBox4.Image = canny.ToBitmap();
                    pictureBox5.Image = laplace.ToBitmap();
                    pictureBox6.Image = sobel.ToBitmap();
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.White;
                    label3.ForeColor = Color.White;

                }
            }
            else if (button1.Text == "Clear")
            {
                button1.Text = "Browse";
                pictureBox2.Image = new Bitmap("../../../Asset/NoImage.jpg");
                pictureBox3.Image = new Bitmap("../../../Asset/NoImage.jpg");
                groupBox1.Hide();
                button2.Hide();
            }
        }
    }
}
