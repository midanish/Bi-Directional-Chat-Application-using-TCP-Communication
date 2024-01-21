namespace SE_Project
{
    partial class Form_Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Client));
            this.button_Upload = new System.Windows.Forms.Button();
            this.richTextBox_Display = new System.Windows.Forms.RichTextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_Connect = new System.Windows.Forms.Button();
            this.pictureBox_Image = new System.Windows.Forms.PictureBox();
            this.textBox_Text = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Upload
            // 
            this.button_Upload.Location = new System.Drawing.Point(76, 588);
            this.button_Upload.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Upload.Name = "button_Upload";
            this.button_Upload.Size = new System.Drawing.Size(112, 35);
            this.button_Upload.TabIndex = 0;
            this.button_Upload.Text = "Upload";
            this.button_Upload.UseVisualStyleBackColor = true;
            this.button_Upload.Click += new System.EventHandler(this.button_Upload_Click);
            // 
            // richTextBox_Display
            // 
            this.richTextBox_Display.Location = new System.Drawing.Point(76, 61);
            this.richTextBox_Display.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox_Display.Name = "richTextBox_Display";
            this.richTextBox_Display.Size = new System.Drawing.Size(494, 519);
            this.richTextBox_Display.TabIndex = 1;
            this.richTextBox_Display.Text = "";
            this.richTextBox_Display.TextChanged += new System.EventHandler(this.richTextBox_Display_TextChanged);
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(410, 588);
            this.button_Send.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(112, 35);
            this.button_Send.TabIndex = 2;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // button_Connect
            // 
            this.button_Connect.BackColor = System.Drawing.Color.Red;
            this.button_Connect.Location = new System.Drawing.Point(76, 16);
            this.button_Connect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(112, 35);
            this.button_Connect.TabIndex = 5;
            this.button_Connect.Text = "Connect";
            this.button_Connect.UseVisualStyleBackColor = false;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // pictureBox_Image
            // 
            this.pictureBox_Image.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Image.Image")));
            this.pictureBox_Image.Location = new System.Drawing.Point(531, 588);
            this.pictureBox_Image.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox_Image.Name = "pictureBox_Image";
            this.pictureBox_Image.Size = new System.Drawing.Size(40, 31);
            this.pictureBox_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Image.TabIndex = 6;
            this.pictureBox_Image.TabStop = false;
            this.pictureBox_Image.Click += new System.EventHandler(this.pictureBox_Image_Click);
            // 
            // textBox_Text
            // 
            this.textBox_Text.Location = new System.Drawing.Point(198, 591);
            this.textBox_Text.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_Text.Name = "textBox_Text";
            this.textBox_Text.Size = new System.Drawing.Size(202, 26);
            this.textBox_Text.TabIndex = 7;
            this.textBox_Text.TextChanged += new System.EventHandler(this.textBox_Text_TextChanged);
            this.textBox_Text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Text_KeyPress);
            // 
            // Form_Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(664, 692);
            this.Controls.Add(this.textBox_Text);
            this.Controls.Add(this.pictureBox_Image);
            this.Controls.Add(this.button_Connect);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.richTextBox_Display);
            this.Controls.Add(this.button_Upload);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form_Client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Form_Client_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Upload;
        private System.Windows.Forms.RichTextBox richTextBox_Display;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.PictureBox pictureBox_Image;
        private System.Windows.Forms.TextBox textBox_Text;
    }
}

