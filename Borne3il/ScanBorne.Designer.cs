namespace Borne3il
{
    partial class ScanBorne
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            scanbutton = new Button();
            button2 = new Button();
            label1 = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Location = new Point(12, 113);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(533, 294);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // scanbutton
            // 
            scanbutton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            scanbutton.Location = new Point(570, 113);
            scanbutton.Name = "scanbutton";
            scanbutton.Size = new Size(112, 44);
            scanbutton.TabIndex = 2;
            scanbutton.Text = "Scanner";
            scanbutton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(433, 54);
            button2.Name = "button2";
            button2.Size = new Size(112, 44);
            button2.TabIndex = 4;
            button2.Text = "Demarrer";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(41, 56);
            label1.Name = "label1";
            label1.Size = new Size(116, 38);
            label1.TabIndex = 5;
            label1.Text = "Camera";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(163, 64);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(243, 33);
            comboBox1.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(619, 192);
            label2.Name = "label2";
            label2.Size = new Size(97, 38);
            label2.TabIndex = 7;
            label2.Text = "Statut";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(605, 260);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 147);
            textBox1.TabIndex = 8;
            // 
            // ScanBorne
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(scanbutton);
            Controls.Add(pictureBox1);
            Name = "ScanBorne";
            Text = "ScanBorne";
            Load += ScanBorne_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button scanbutton;
        private Button button2;
        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private TextBox textBox1;
    }
}