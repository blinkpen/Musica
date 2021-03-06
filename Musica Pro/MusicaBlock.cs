//Code written by Kevin Gragg (blinkpen) of iLL-Logic Studios 2022
using System;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Drawing;
using System.ComponentModel;

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

        private bool isBetween(int x, int y, int i)
        {
            return i >= x && i <= y;
        }

        public void playMe()
        {
            if(timer1.Enabled)
            { 
                timer1.Stop();
                button1.ImageAlign = ContentAlignment.TopCenter;
                button1.Image = Properties.Resources.Note24;
            }
            button1.Image = Properties.Resources.Note24pink; //set button image to played block image
            button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter; //bring button image down

            if (comboBox1.SelectedIndex != 0) //if currently selected note is NOT rest
            {
                isRest = false; //set boolean to reflect this               
                
                var semitone = Math.Pow(2, 1.0 / 12); //semitone
                var upOneTone = semitone * semitone; //go up one tone in pitch
                var downOneTone = 1.0 / upOneTone; //go down one tone in pitch

                using (var reader = new MediaFoundationReader(source))
                {
                    pitch = new SmbPitchShiftingSampleProvider(reader.ToSampleProvider());
                    
                    using (device = new WaveOutEvent())
                    {
                        if (isBetween(1, 43, comboBox1.SelectedIndex))
                        {
                            float n = (float)semitone;
                            for (int i = 43; i > comboBox1.SelectedIndex; i--)
                            {
                                n = (float)downOneTone * n;
                            }
                            pitch.PitchFactor = n;
                        }
                        else if (isBetween(45, 88, comboBox1.SelectedIndex))
                        {
                            float n = (float)semitone;
                            for (int i = 44; i < comboBox1.SelectedIndex; i++)
                            {
                                 n = (float)upOneTone * n;
                            }
                            pitch.PitchFactor = n;
                        }
                        else
                        {
                            //no pitch shift here (middle note)
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

            timer1.Start(); //start timer
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            playMe(); //play this note
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            note = comboBox1.SelectedIndex; //set note variable to whichever note is selected in combobox
            colorize(this); //colorize musica block based on combobox's current value
            button1.Text = comboBox1.SelectedItem.ToString(); //set button text to match combobox so it can display the current Note selected like a label
        }

        private void colorize(MusicaBlock mB) //change the color theme of this instance of Musica Block based on which note is selected in the combobox
        {
            Color rgb = SystemColors.Control;
            
            if(mB.note == 0)
            {
                rgb = Color.White;
            }
            else if (mB.note >= 1 && mB.note <= 12)
            {
                rgb = Color.FromArgb(255, 0, 0);//red
            }
            else if (mB.note >= 13 && mB.note <= 24)
            {
                rgb = Color.FromArgb(253, 68, 0);//redorange
            }
            else if (mB.note >= 25 && mB.note <= 36)
            {
                rgb = Color.FromArgb(248, 128, 7);//orange
            }
            else if (mB.note >= 37 && mB.note <= 48)
            {
                rgb = Color.FromArgb(255, 255, 0);//yellow
            }
            else if (mB.note >= 49 && mB.note <= 60)
            {
                rgb = Color.FromArgb(0, 255, 0);//green
            }
            else if (mB.note >= 61 && mB.note <= 72)
            {
                rgb = Color.FromArgb(0, 255, 255);//blue
            }
            else if (mB.note >= 73 && mB.note <= 84)
            {
                rgb = Color.FromArgb(255, 0, 255);//magenta
            }
            else if (mB.note >= 85 && mB.note <= 88)
            {
                rgb = Color.DeepPink;
            }
            mB.ForeColor = rgb;
            Color lightRGB = ControlPaint.Light(rgb); //based on the new backcolor, create a lighter color of it
            mB.button1.BackColor = rgb;
            mB.button1.ForeColor = Color.Black;
            mB.cb.BackColor = lightRGB; //make combobox that color as well
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            t1 += 1; //increment t1 each tick

            if (t1 >= 10) //if t1 is equal to 10 or goes past it
            {
                button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter; //bring image on button back up
            }

            if (isRest) //if note is rest
            {
                if (t1 >= 50) //if t1 is equal to 50 or goes past it
                {
                    button1.Image = Properties.Resources.Note24; //change button image back to normal
                    timer1.Stop(); //stop timer
                    t1 = 0; //reset t1
                }
            }
            else // if not rest but an actual note
            {
                if (device.PlaybackState == PlaybackState.Stopped) //if device is not currently playing
                {
                    button1.Image = Properties.Resources.Note24; //set button image back to normal
                    timer1.Stop(); // stop timer
                    button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter; // bring image on button back up
                    t1 = 0; //reset t1
                }
            }
        }
    }
}



