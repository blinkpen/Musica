//Code written by Kevin Gragg (blinkpen) of iLL-Logic Studios 2022
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
                mb.source = openFileDialog1.FileName; //load sound file for each musica block                
            }
        }

        private void removeNotes(int whenStop)
        {
            for (int i = notes; i > whenStop; i--) //remove last musica block until new note amount remains
            {
                panel1.Controls.Remove(panel1.Controls["Musica Block " + (i)]); //remove last musica block
                notes -= 1; //update note count
                newLineInc -= 1; //update newline incrementer

                if (notes != 0) //if note count is not zero
                {
                    if (newLineInc == 0) //if newline incrementer is zero
                    {
                        newLineInc = rowCount; //set incrementer to 10 to establish newline
                    }
                }     
                else //if note count is zero
                {
                    newLineInc = 0; // set newline incrementer to zero
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
            reCenterForms(); //keep panel centered at all times
        }

        private void reCenterForms()
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2; //center panel1 in form
        }

        private void generateMelody()
        {                        
            Random rnd = new Random(); //declare new random
            foreach (MusicaBlock mB in panel1.Controls) //for every musica block
            {
                if(!(mB.chbx.Checked)) //if block is not protected then generate note, otherwise do not
                {
                    var test = rnd.Next(0, 89); //deciding variable

                    if(test == 0) //if rest
                    {                        
                        mB.cb.SelectedIndex = 0; //set note to rest                                          
                    }
                    else//if not rest
                    {
                        if(toolStripComboBox1.SelectedIndex == 0)
                        {
                            mB.note = rnd.Next(1, 89);//select from all notes
                        }
                        else if(toolStripComboBox1.SelectedIndex == 1)
                        {
                            mB.note = rnd.Next(1, 13);//select from Scale 1
                        }
                        else if (toolStripComboBox1.SelectedIndex == 2)
                        {
                            mB.note = rnd.Next(13, 25);//select from Scale 2
                        }
                        else if (toolStripComboBox1.SelectedIndex == 3)
                        {
                            mB.note = rnd.Next(25, 37);//select from Scale 3
                        }
                        else if (toolStripComboBox1.SelectedIndex == 4)
                        {
                            mB.note = rnd.Next(37, 49);//select from Scale 4
                        }
                        else if (toolStripComboBox1.SelectedIndex == 5)
                        {
                            mB.note = rnd.Next(49, 61);//select from Scale 5
                        }
                        else if (toolStripComboBox1.SelectedIndex == 6)
                        {
                            mB.note = rnd.Next(61, 73);//select from Scale 6
                        }
                        else if (toolStripComboBox1.SelectedIndex == 7)
                        {
                            mB.note = rnd.Next(73, 85);//select from Scale 7
                        }
                        else if (toolStripComboBox1.SelectedIndex == 8)
                        {
                            mB.note = rnd.Next(85, 89);//select from Scale 8
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

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))//textbox can only accept numeric characters and nothing else
            {
                e.Handled = true;
            }
        }

        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))//textbox can only accept numeric characters and nothing else
            {
                e.Handled = true;
            }
        }

        private void toolStripTextBox1_Leave(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                toolStripTextBox1.Text = "10";
            }
        }

        private void toolStripTextBox2_Leave(object sender, EventArgs e)
        {
            if (toolStripTextBox2.Text == "0" || toolStripTextBox2.Text == "")
            {
                toolStripTextBox2.Text = "300";
            }
        }
    }
}






