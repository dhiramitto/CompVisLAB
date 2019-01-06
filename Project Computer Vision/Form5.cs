using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
namespace Project_Computer_Vision
{
    public partial class Form5 : Form
    {
        Image<Bgr, byte> ori, edit;
        Image<Gray, byte> gray, thresholding;
        public Form5()
        {
            InitializeComponent();
            groupBox1.Hide();
            button2.Hide();
            label4.Hide();
            label5.Hide();
            numericUpDown1.Hide();
            numericUpDown2.Hide();

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
                    thresholding = new Image<Gray, byte>(ori.Width, ori.Height);
                    gray = new Image<Gray, byte>(ori.Width, ori.Height);

                    button1.Text = "Clear";
                    groupBox1.Show();

                    CvInvoke.CvtColor(ori, gray, ColorConversion.Bgr2Gray);
                    pictureBox4.Image = gray.ToBitmap();

                    CvInvoke.Blur(ori, edit, new Size(1, 1), new Point(-1, -1));
                    pictureBox5.Image = edit.ToBitmap();

                    CvInvoke.Threshold(gray, thresholding, 60, 255, ThresholdType.Binary);
                    pictureBox6.Image = thresholding.ToBitmap();

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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.Aqua;
            label3.ForeColor = Color.White;

            pictureBox3.Image = pictureBox5.Image;

            label4.Text = "Smooth Level";
            numericUpDown1.Value = 1;
            numericUpDown1.Increment = 2;

            label4.Show();
            numericUpDown1.Show();
            button2.Show();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(label4.Text == "Smooth Level")
            {
                int level = Convert.ToInt32(numericUpDown1.Value);
                if (level % 2 == 0)
                {
                    MessageBox.Show("Smooth level must be an odd number");
                }
                CvInvoke.Blur(ori, edit, new Size(level, level), new Point(-1, -1));
                pictureBox5.Image = edit.ToBitmap();
                pictureBox3.Image = pictureBox5.Image;
            }else if (label4.Text == "Low Threshold")
            {
                int low  = Convert.ToInt32(numericUpDown1.Value);
                int high = Convert.ToInt32(numericUpDown2.Value);
                CvInvoke.Threshold(gray, thresholding, low, high, ThresholdType.Binary);
                pictureBox6.Image = thresholding.ToBitmap();
                pictureBox3.Image = pictureBox6.Image;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            label1.ForeColor = Color.Aqua;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;

            pictureBox3.Image = pictureBox4.Image;

            button2.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.Aqua;
            
            pictureBox3.Image = pictureBox6.Image;

            label4.Text = "Low Threshold";
            numericUpDown1.Value = 60;
            numericUpDown1.Increment = 1;
            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 255;

            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 255;
            numericUpDown2.Value = 255;
            numericUpDown2.Increment = 1;
            
            label4.Show();
            numericUpDown1.Show();
            label5.Show();
            numericUpDown2.Show();
            button2.Show();

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

            int low = Convert.ToInt32(numericUpDown1.Value);
            int high = Convert.ToInt32(numericUpDown2.Value);
            CvInvoke.Threshold(gray, thresholding, low, high, ThresholdType.Binary);
            pictureBox6.Image = thresholding.ToBitmap();
            pictureBox3.Image = pictureBox6.Image;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
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
    }
}
