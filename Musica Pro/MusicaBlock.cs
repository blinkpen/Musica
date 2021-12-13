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
            button1.Image = Properties.Resources.Note24pink; //set button image to played block image
            button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter; //bring button image down

            if (comboBox1.SelectedIndex != 0) //if currently selected note is NOT rest
            {
                isRest = false; //set boolean to reflect this               
                float changer = 2; //variable i can use to dynamically adjust the equation based on which note is selected
                var semitone = Math.Pow(changer, 1.0 / 12); //semitone
                var upOneTone = semitone * semitone; //go up one tone in pitch
                var downOneTone = 1.0 / upOneTone; //go down one tone in pitch

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
            //change backcolor
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
            Color lightRed = ControlPaint.Light(mB.BackColor); //based on the new backcolor, create a lighter color of it
            mB.button1.BackColor = lightRed; //make button this lighter color
            mB.cb.BackColor = lightRed; //make combobox that color as well
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



