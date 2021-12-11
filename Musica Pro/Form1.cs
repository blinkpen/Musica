//Code written by Kevin Gragg of iLL-Logic Studios 2022
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Musica_Pro
{

    public partial class Form1 : Form
    {
        private int notes = 0;
        private int newLineInc = 0;
        private int rowCount = 10;
        private int t1 = 0;
        private int ti = 0;
        private int ni = 0;
        private bool loop = false;

        public Form1()
        {
            InitializeComponent();
            toolStripComboBox1.SelectedIndex = 0;
            ti = timer1.Interval;                       
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();           
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            toolStripStatusLabel1.Text = $"Current Sound: {openFileDialog1.FileName}";
            addRemoveHandler();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            addRemoveHandler();
        }

        private void addRemoveHandler()
        {
            int tempoEntry = Convert.ToInt32(toolStripTextBox2.Text);
            int notesEntry = Convert.ToInt32(toolStripTextBox1.Text);

            if (tempoEntry != ti)//tempo
            {
                timer1.Interval = tempoEntry;
                ti = timer1.Interval;
            }

            if (notesEntry != ni)//notes
            {
                if (notesEntry > notes) //if amount of notes requested is more than already exists
                {
                    loadNotes(notesEntry - notes); //load notes
                    ni = notesEntry; //update note inquiry with requested amount of notes                            
                }
                else if (notesEntry < notes) //if amount of notes requested is less than what already exists
                {
                    removeNotes(notesEntry); //remove notes
                    ni = notesEntry; //update note inquiry with requested amount of notes
                }
            }

            foreach (MusicaBlock mb in panel1.Controls.OfType<MusicaBlock>())
            {
                mb.source = openFileDialog1.FileName;                
            }
        }

        private void removeNotes(int whenStop)
        {
            for (int i = notes; i > whenStop; i--)
            {
                panel1.Controls.Remove(panel1.Controls["Musica Block " + (i)]);
                notes -= 1;
                newLineInc -= 1;

                if (notes != 0)
                {
                    if (newLineInc == 0)
                    {
                        newLineInc = 10;
                    }
                }     
                else
                {
                    newLineInc = 0;
                }
            }
        }

        private void loadNotes(int whenStop)
        { 
            for (int i = 0; i < whenStop; i++)
            {
                if (notes == 0)
                {
                    MusicaBlock mB = new MusicaBlock();
                    mB.Location = new Point(30, 30);                    
                    mB.Name = "Musica Block " + (notes + 1);
                    mB.lbl.Text = mB.Name;
                    panel1.Controls.Add(mB);                    
                    notes += 1;
                    newLineInc += 1;
                }
                else
                {
                    MusicaBlock mB = new MusicaBlock();
                    mB.Name = "Musica Block " + (notes + 1);
                    mB.lbl.Text = mB.Name;                    
                    Control lastOne = panel1.Controls["Musica Block " + (notes)];
                    panel1.Controls.Add(mB);
                    if (newLineInc == rowCount)
                    {
                        mB.Location = new Point(30, lastOne.Bottom + 10); //load below last one starting new row
                        newLineInc = 0;
                    }
                    else
                    {
                        mB.Location = new Point(lastOne.Right + 10, lastOne.Bottom - lastOne.Height); //load beside last one
                    }
                    notes += 1;
                    newLineInc += 1;
                }                
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            reCenterForms();
        }

        private void reCenterForms()
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
        }

        private void generateMelody()
        {                        
            Random rnd = new Random();
            foreach (MusicaBlock mB in panel1.Controls)
            {
                if(!(mB.chbx.Checked)) //if block is not protected then generate note, otherwise do not
                {
                    var test = rnd.Next(0, 53);

                    if(test == 0)//if rest
                    {
                        mB.note = test;//set note to rest
                        mB.cb.SelectedIndex = mB.note;                   
                    }
                    else//if not rest
                    {
                        if(toolStripComboBox1.SelectedIndex == 0)
                        {
                            mB.note = rnd.Next(0, 53);//select from all notes
                        }
                        else if(toolStripComboBox1.SelectedIndex == 1)
                        {
                            mB.note = rnd.Next(1, 8);//select from Scale 1
                        }
                        else if (toolStripComboBox1.SelectedIndex == 2)
                        {
                            mB.note = rnd.Next(8, 15);//select from Scale 2
                        }
                        else if (toolStripComboBox1.SelectedIndex == 3)
                        {
                            mB.note = rnd.Next(15, 22);//select from Scale 3
                        }
                        else if (toolStripComboBox1.SelectedIndex == 4)
                        {
                            mB.note = rnd.Next(22, 29);//select from Scale 4
                        }
                        else if (toolStripComboBox1.SelectedIndex == 5)
                        {
                            mB.note = rnd.Next(29, 36);//select from Scale 5
                        }
                        else if (toolStripComboBox1.SelectedIndex == 6)
                        {
                            mB.note = rnd.Next(36, 43);//select from Scale 6
                        }
                        else if (toolStripComboBox1.SelectedIndex == 7)
                        {
                            mB.note = rnd.Next(43, 50);//select from Scale 7
                        }
                        else if (toolStripComboBox1.SelectedIndex == 8)
                        {
                            mB.note = rnd.Next(50, 53);//select from Scale 8
                        }
                        mB.cb.SelectedIndex = mB.note;
                    }            
                }
                
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            generateMelody();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled)
            {
                timer1.Stop();
                toolStripButton3.Text = "Play";
            }
            else
            {
                timer1.Start();
                toolStripButton3.Text = "Stop";
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t1 += 1;
            foreach (MusicaBlock mb in panel1.Controls)
            {
                if(mb.Name == "Musica Block " + t1)
                {                    
                    mb.playMe();
                }                  
            }
            
            if (t1 >= panel1.Controls.Count)
            {
                if(!loop)
                {
                    timer1.Stop();
                    toolStripButton3.Text = "Play";                    
                }
                t1 = 0;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if(toolStripButton5.Checked)
            {
                loop = true;
            }
            else
            {
                loop = false;
            }
        }
    }
}






