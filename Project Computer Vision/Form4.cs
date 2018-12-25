using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Computer_Vision
{
    public partial class Form4 : Form
    {
        Image<Bgr, byte> ori, edit;
        Image<Gray, byte> gray;

        bool play = false;

        CascadeClassifier haarcas;

        VideoCapture capt;

        int count = 0;

        public Form4()
        {
            InitializeComponent();
            capt = new VideoCapture();
            pictureBox3.Hide();
            pictureBox4.Hide();
            groupBox1.Hide();
            timer1.Interval = 4000;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void detection()
        {
            haarcas = new CascadeClassifier("../../haarcascade_frontalface_alt.xml");
            count = 0;
            edit = ori.Copy();
            CvInvoke.CvtColor(ori, gray, ColorConversion.Bgr2Gray);
            var objects = haarcas.DetectMultiScale(gray, 1.1, 10, Size.Empty);

            foreach (var obj in objects)
            {
                count++;
                edit.Draw(obj, new Bgr(Color.Red), 3);
            }
            pictureBox2.Image = edit.ToBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "Image files(*.jpg;*.png)|*.jpg;*.png";
            openFileDialog1.Filter = "Image files(*.jpg;*.png)|*.jpg;*.png | Video files(*.mp4; *.avi; *.flv; *.wmv; *.mov) | *.mp4; *.avi; *.flv; *.wmv; *.mov";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.Hide();
                label1.Hide();
                pictureBox3.Show();
                groupBox1.Show();

                //if (openFileDialog1.FileName.GetType() == "")
                //{
                //    pictureBox4.Image = new Bitmap("../../../Asset/pause.png");
                //    timer1.Start();
                //    play = true;
                //    capt = new VideoCapture(openFileDialog1.FileName);
                //    //time start
                //    //detect face

                //}
                if(openFileDialog1.FileName.EndsWith(".png") || openFileDialog1.FileName.EndsWith(".jpg")) { 

                    pictureBox2.Image = new Bitmap(openFileDialog1.FileName);
                    ori = new Image<Bgr, byte>(new Bitmap(openFileDialog1.FileName));
                    edit = new Image<Bgr, byte>(ori.Width, ori.Height);
                    gray = new Image<Gray, byte>(ori.Width, ori.Height);

                    detection();
                    label2.Text = count + " Face(s)";
                }
                else
                {
                    pictureBox4.Image = new Bitmap("../../../Asset/pause.png");
                    play = true;
                    capt = new VideoCapture(openFileDialog1.FileName);
                    timer1.Start();
                    pictureBox4.Show();
                    //time start
                    //detect face
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if(play)
            {
                timer1.Stop();
                play = false;
                pictureBox4.Image = new Bitmap("../../../Asset/play.png");
            }
            else
            {
                play = true;
                timer1.Start();
                pictureBox4.Image = new Bitmap("../../../Asset/pause.png");
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            ori = capt.QueryFrame().ToImage<Bgr, byte>();
            pictureBox2.Image = ori.ToBitmap();
            gray = new Image<Gray, byte>(ori.Width, ori.Height);

            detection();

            label2.Text = count + " Face(s)";
            gray.Dispose();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            label1.Show();
            button1.Show();
            pictureBox3.Hide();
            groupBox1.Hide();
            pictureBox4.Hide();
            label2.Text = "";
        }
    }
}
