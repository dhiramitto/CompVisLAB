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
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Project_Computer_Vision
{
    public partial class Form2 : Form
    {
        Image<Bgr, byte> ori, edit;
        Image<Gray, byte> gray;
        public Form2()
        {
            InitializeComponent();
            button2.Hide();
            groupBox1.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
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
                    button1.Text = "Clear";
                    groupBox1.Show();

                    CvInvoke.CvtColor(ori, gray, ColorConversion.Bgr2Gray);
                    edit = ori.Copy();
                }
            } else if (button1.Text == "Clear")
            {
                button1.Text = "Browse";
                pictureBox2.Image = new Bitmap("../../../Asset/NoImage.jpg");
                pictureBox3.Image = new Bitmap("../../../Asset/NoImage.jpg");
                groupBox1.Hide();
                button2.Hide();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

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

        private void DetectLine()
        {

            LineSegment2D[] lines = gray.HoughLines(127, 127, 5, Math.PI / 45, 10, 20, 5)[0];

            foreach (var line in lines)
            {
                edit.Draw(line, new Bgr(Color.Blue), 2);

            }
        }

        private void DetectCircle()
        {
            CircleF[] circles = gray.HoughCircles(new Gray(127), new Gray(127), 5, 400, 1, 0)[0];

            foreach (var circle in circles)
            {
                edit.Draw(circle, new Bgr(Color.Green), 2);

            }
        }

        private void DetectTriangle()
        {
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(gray, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];
                VectorOfPoint contourApprox = new VectorOfPoint();

                CvInvoke.ApproxPolyDP(contour, contourApprox, CvInvoke.ArcLength(contour, true) * 0.1, true);
                Point[] points = contourApprox.ToArray();
                if (contourApprox.Size == 3)
                {
                    LineSegment2D[] lines = PointCollection.PolyLine(points, true);

                    for (int j = 0; j < lines.Length; j++)
                    {
                        edit.Draw(lines[j], new Bgr(Color.Red), 2);

                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DetectLine();
            }
            else
            {
                edit = ori.Copy();
                if (checkBox2.Checked)
                {
                    DetectCircle();
                }
                if (checkBox3.Checked)
                {
                    DetectTriangle();
                }
            }
            pictureBox3.Image = edit.ToBitmap();
            button2.Show();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                DetectCircle();
            }
            else
            {
                edit = ori.Copy();
                if (checkBox1.Checked)
                {
                    DetectLine();
                }
                if (checkBox3.Checked)
                {
                    DetectTriangle();
                }
            }
            pictureBox3.Image = edit.ToBitmap();

            button2.Show();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                DetectTriangle();
            }
            else
            {
                edit = ori.Copy();
                if (checkBox2.Checked)
                {
                    DetectCircle();
                }
                if (checkBox1.Checked)
                {
                    DetectLine();
                }
            }
            pictureBox3.Image = edit.ToBitmap();
            button2.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
