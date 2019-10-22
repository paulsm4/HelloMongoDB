namespace HelloMongoDBCS
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.edtHost = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.edtStatus = new System.Windows.Forms.TextBox();
            this.edtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnListRecords = new System.Windows.Forms.Button();
            this.btnAddRecords = new System.Windows.Forms.Button();
            this.btnPurgeRecords = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host:";
            // 
            // edtHost
            // 
            this.edtHost.Location = new System.Drawing.Point(51, 9);
            this.edtHost.Name = "edtHost";
            this.edtHost.Size = new System.Drawing.Size(100, 20);
            this.edtHost.TabIndex = 1;
            this.edtHost.Text = "ubuntu18";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(15, 81);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 41);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Status:";
            // 
            // edtStatus
            // 
            this.edtStatus.Location = new System.Drawing.Point(15, 146);
            this.edtStatus.Multiline = true;
            this.edtStatus.Name = "edtStatus";
            this.edtStatus.ReadOnly = true;
            this.edtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.edtStatus.Size = new System.Drawing.Size(702, 301);
            this.edtStatus.TabIndex = 0;
            // 
            // edtPort
            // 
            this.edtPort.Location = new System.Drawing.Point(51, 35);
            this.edtPort.Name = "edtPort";
            this.edtPort.Size = new System.Drawing.Size(100, 20);
            this.edtPort.TabIndex = 3;
            this.edtPort.Text = "27017";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port:";
            // 
            // btnListRecords
            // 
            this.btnListRecords.Enabled = false;
            this.btnListRecords.Location = new System.Drawing.Point(129, 81);
            this.btnListRecords.Name = "btnListRecords";
            this.btnListRecords.Size = new System.Drawing.Size(95, 41);
            this.btnListRecords.TabIndex = 5;
            this.btnListRecords.Text = "&List Records";
            this.btnListRecords.UseVisualStyleBackColor = true;
            this.btnListRecords.Click += new System.EventHandler(this.btnListRecords_Click);
            // 
            // btnAddRecords
            // 
            this.btnAddRecords.Enabled = false;
            this.btnAddRecords.Location = new System.Drawing.Point(263, 81);
            this.btnAddRecords.Name = "btnAddRecords";
            this.btnAddRecords.Size = new System.Drawing.Size(91, 41);
            this.btnAddRecords.TabIndex = 6;
            this.btnAddRecords.Text = "&Add Records";
            this.btnAddRecords.UseVisualStyleBackColor = true;
            this.btnAddRecords.Click += new System.EventHandler(this.btnAddRecords_Click);
            // 
            // btnPurgeRecords
            // 
            this.btnPurgeRecords.Enabled = false;
            this.btnPurgeRecords.Location = new System.Drawing.Point(393, 81);
            this.btnPurgeRecords.Name = "btnPurgeRecords";
            this.btnPurgeRecords.Size = new System.Drawing.Size(98, 41);
            this.btnPurgeRecords.TabIndex = 7;
            this.btnPurgeRecords.Text = "&Purge Records";
            this.btnPurgeRecords.UseVisualStyleBackColor = true;
            this.btnPurgeRecords.Click += new System.EventHandler(this.btnPurgeRecords_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 452);
            this.Controls.Add(this.btnPurgeRecords);
            this.Controls.Add(this.btnAddRecords);
            this.Controls.Add(this.btnListRecords);
            this.Controls.Add(this.edtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.edtStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.edtHost);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "HelloMongoDB C#";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtHost;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edtStatus;
        private System.Windows.Forms.TextBox edtPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnListRecords;
        private System.Windows.Forms.Button btnAddRecords;
        private System.Windows.Forms.Button btnPurgeRecords;
    }
}

