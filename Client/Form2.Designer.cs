namespace TCP_OMOK_Client
{
    partial class Form2
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
            this.createroom = new System.Windows.Forms.Button();
            this.btnserverjoin = new System.Windows.Forms.Button();
            this.join = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // createroom
            // 
            this.createroom.Location = new System.Drawing.Point(201, 324);
            this.createroom.Name = "createroom";
            this.createroom.Size = new System.Drawing.Size(75, 23);
            this.createroom.TabIndex = 13;
            this.createroom.Text = "방만들기";
            this.createroom.UseVisualStyleBackColor = true;
            this.createroom.Click += new System.EventHandler(this.createroom_Click);
            // 
            // btnserverjoin
            // 
            this.btnserverjoin.Location = new System.Drawing.Point(157, 44);
            this.btnserverjoin.Name = "btnserverjoin";
            this.btnserverjoin.Size = new System.Drawing.Size(75, 23);
            this.btnserverjoin.TabIndex = 12;
            this.btnserverjoin.Text = "서버연결";
            this.btnserverjoin.UseVisualStyleBackColor = true;
            this.btnserverjoin.Click += new System.EventHandler(this.btnserverjoin_Click);
            // 
            // join
            // 
            this.join.Location = new System.Drawing.Point(68, 324);
            this.join.Name = "join";
            this.join.Size = new System.Drawing.Size(75, 23);
            this.join.TabIndex = 11;
            this.join.Text = "참가";
            this.join.UseVisualStyleBackColor = true;
            this.join.Click += new System.EventHandler(this.join_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(23, 90);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(336, 208);
            this.listBox2.TabIndex = 10;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(23, 44);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(100, 21);
            this.name.TabIndex = 9;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 380);
            this.Controls.Add(this.createroom);
            this.Controls.Add(this.btnserverjoin);
            this.Controls.Add(this.join);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.name);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createroom;
        private System.Windows.Forms.Button btnserverjoin;
        private System.Windows.Forms.Button join;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox name;
    }
}