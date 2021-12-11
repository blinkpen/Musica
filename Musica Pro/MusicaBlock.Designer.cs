
namespace Musica_Pro
{
    partial class MusicaBlock
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Image = global::Musica_Pro.Properties.Resources.Note24;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(5, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 100);
            this.button1.TabIndex = 0;
            this.button1.Text = "Rest";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Rest",
            "A",
            "A#",
            "B",
            "B#",
            "C",
            "C#",
            "D",
            "D#",
            "E",
            "E#",
            "F",
            "F#",
            "G",
            "G#",
            "A",
            "A#",
            "B",
            "B#",
            "C",
            "C#",
            "D",
            "D#",
            "E",
            "E#",
            "F",
            "F#",
            "G",
            "G#",
            "A",
            "A#",
            "B",
            "B#",
            "C",
            "C#",
            "D",
            "D#",
            "E",
            "E#",
            "F",
            "F#",
            "G",
            "G#",
            "A",
            "A#",
            "B",
            "B#",
            "C",
            "C#",
            "D",
            "D#",
            "E",
            "E#"});
            this.comboBox1.Location = new System.Drawing.Point(5, 133);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 22);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Musica Block";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(5, 161);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(81, 18);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Protected";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // MusicaBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "MusicaBlock";
            this.Size = new System.Drawing.Size(111, 185);
            this.Load += new System.EventHandler(this.MusicaBlock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
