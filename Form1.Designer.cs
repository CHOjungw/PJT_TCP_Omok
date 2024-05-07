namespace TCP_OMOK
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_close = new System.Windows.Forms.Button();
            this.lb_Log = new System.Windows.Forms.ListBox();
            this.lb_Roomlist = new System.Windows.Forms.ListBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(147, 51);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 11;
            this.btn_close.Text = "서버종료";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // lb_Log
            // 
            this.lb_Log.FormattingEnabled = true;
            this.lb_Log.ItemHeight = 12;
            this.lb_Log.Location = new System.Drawing.Point(301, 120);
            this.lb_Log.Name = "lb_Log";
            this.lb_Log.Size = new System.Drawing.Size(461, 280);
            this.lb_Log.TabIndex = 10;
            // 
            // lb_Roomlist
            // 
            this.lb_Roomlist.FormattingEnabled = true;
            this.lb_Roomlist.ItemHeight = 12;
            this.lb_Roomlist.Location = new System.Drawing.Point(39, 120);
            this.lb_Roomlist.Name = "lb_Roomlist";
            this.lb_Roomlist.Size = new System.Drawing.Size(227, 280);
            this.lb_Roomlist.TabIndex = 9;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(39, 51);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 8;
            this.btn_start.Text = "서버시작";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.lb_Log);
            this.Controls.Add(this.lb_Roomlist);
            this.Controls.Add(this.btn_start);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.ListBox lb_Log;
        private System.Windows.Forms.ListBox lb_Roomlist;
        private System.Windows.Forms.Button btn_start;
    }
}

