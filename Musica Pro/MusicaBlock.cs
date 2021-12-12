using System;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Drawing;

namespace Musica_Pro
{
    public partial class MusicaBlock : UserControl
    {
        public string source;
        public int note = 0;
        public ComboBox cb;
        private WaveOutEvent device;
        private SmbPitchShiftingSampleProvider pitch;
        private int t1 = 0;
        private bool isRest = false;
        public Label lbl;
        public CheckBox chbx;
        
        public MusicaBlock()
        {
            InitializeComponent();
            cb = comboBox1;
            lbl = label1;
            chbx = checkBox1;
        }

        private void MusicaBlock_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = note;
        }

        public void playMe()
        {
            button1.Image = Properties.Resources.Note24pink;
            button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            if (comboBox1.SelectedIndex != 0)
            {
                isRest = false;                
                float changer = 0;
                var semitone = Math.Pow(changer, 1.0 / 12);
                var upOneTone = semitone * semitone;
                var downOneTone = 1.0 / upOneTone;

                using (var reader = new MediaFoundationReader(source))
                {
                    pitch = new SmbPitchShiftingSampleProvider(reader.ToSampleProvider());

                    using (device = new WaveOutEvent())
                    {                        
                        if(comboBox1.SelectedIndex >= 1 && comboBox1.SelectedIndex <= 26)
                        {
                            for (int i = comboBox1.Items.Count; i > comboBox1.SelectedIndex - 1; i--)
                            {
                                pitch.PitchFactor = (float)downOneTone;
                                changer += 1;
                                semitone = Math.Pow(changer, 1.0 / 12);
                                upOneTone = semitone * semitone;
                                downOneTone = 1.0 / upOneTone;
                            }
                        }
                        else if(comboBox1.SelectedIndex >= 27 && comboBox1.SelectedIndex <= 53)
                        {
                            for (int i = 0; i < comboBox1.SelectedIndex - 1; i++)
                            {
                                pitch.PitchFactor = (float)upOneTone;
                                changer += 1;
                                semitone = Math.Pow(changer, 1.0 / 12);
                                upOneTone = semitone * semitone;
                                downOneTone = 1.0 / upOneTone;
                            }
                        }                        
                    }
                }

                                //device.Init(pitch.Take(TimeSpan.FromSeconds(2)));
                device.Init(pitch);
                device.Play();                  
                button1.Image = Properties.Resources.Note24pink;                
            }
            else
            {
                isRest = true;
            }
  
           timer1.Start();
        }  

        private void button1_Click_1(object sender, EventArgs e)
        {
            playMe();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {             
            note = comboBox1.SelectedIndex;
            colorize(this);
            button1.Text = comboBox1.SelectedItem.ToString();        
        }

        private void colorize(MusicaBlock mB) 
        {
            if(mB.note == 0)
            {
                mB.BackColor = Color.White;
            }
            else if (mB.note >= 1 && mB.note <= 7)
            {
                mB.BackColor = Color.Red;
            }
            else if (mB.note >= 8 && mB.note <= 14)
            {
                mB.BackColor = Color.OrangeRed;
            }
            else if (mB.note >= 15 && mB.note <= 21)
            {
                mB.BackColor = Color.Orange;
            }
            else if (mB.note >= 22 && mB.note <= 28)
            {
                mB.BackColor = Color.Yellow;
            }
            else if (mB.note >= 29 && mB.note <= 35)
            {
                mB.BackColor = Color.Green;
            }
            else if (mB.note >= 36 && mB.note <= 42)
            {
                mB.BackColor = Color.Blue;
            }
            else if (mB.note >= 43 && mB.note <= 49)
            {
                mB.BackColor = Color.Purple;
            }
            else if (mB.note >= 50 && mB.note <= 54)
            {
                mB.BackColor = Color.DeepPink;
            }
            Color lightRed = ControlPaint.Light(mB.BackColor);
            mB.button1.BackColor = lightRed;
            mB.cb.BackColor = lightRed;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            t1 += 1;
            if (t1 >= 10)
            {
                button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            }

            if (isRest)
            {
                if (t1 >= 50)
                {
                    button1.Image = Properties.Resources.Note24;
                    timer1.Stop();
                    t1 = 0;
                }
            }
            else
            {
                if (device.PlaybackState == PlaybackState.Stopped)
                {
                    button1.Image = Properties.Resources.Note24;
                    timer1.Stop();
                    button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
                    t1 = 0;
                }
            }
        }
    }
}



