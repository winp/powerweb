namespace powerServer
{
    partial class winMain
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
			this.StartSrv = new System.Windows.Forms.Button();
			this.TxtClient = new System.Windows.Forms.TextBox();
			this.TxtServer = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// StartSrv
			// 
			this.StartSrv.Location = new System.Drawing.Point(121, 220);
			this.StartSrv.Name = "StartSrv";
			this.StartSrv.Size = new System.Drawing.Size(123, 47);
			this.StartSrv.TabIndex = 0;
			this.StartSrv.Text = "Start Serving";
			this.StartSrv.UseVisualStyleBackColor = true;
			this.StartSrv.Click += new System.EventHandler(this.StartSrv_Click);
			// 
			// TxtClient
			// 
			this.TxtClient.Location = new System.Drawing.Point(12, 12);
			this.TxtClient.Multiline = true;
			this.TxtClient.Name = "TxtClient";
			this.TxtClient.Size = new System.Drawing.Size(162, 186);
			this.TxtClient.TabIndex = 1;
			this.TxtClient.TextChanged += new System.EventHandler(this.TxtClient_TextChanged);
			// 
			// TxtServer
			// 
			this.TxtServer.Location = new System.Drawing.Point(192, 12);
			this.TxtServer.Multiline = true;
			this.TxtServer.Name = "TxtServer";
			this.TxtServer.Size = new System.Drawing.Size(170, 186);
			this.TxtServer.TabIndex = 2;
			// 
			// winMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(374, 279);
			this.Controls.Add(this.TxtServer);
			this.Controls.Add(this.TxtClient);
			this.Controls.Add(this.StartSrv);
			this.Name = "winMain";
			this.Text = "TestNet";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button StartSrv;
		private System.Windows.Forms.TextBox TxtClient;
		private System.Windows.Forms.TextBox TxtServer;
    }
}

