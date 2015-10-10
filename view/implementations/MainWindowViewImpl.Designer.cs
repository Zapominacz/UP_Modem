namespace ModemConnect
{
    partial class MainWindowViewImpl
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
            this.portListBox = new System.Windows.Forms.ListBox();
            this.connectPortButton = new System.Windows.Forms.Button();
            this.commandHistoryTextBox = new System.Windows.Forms.TextBox();
            this.commandLineTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.serverButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // portListBox
            // 
            this.portListBox.FormattingEnabled = true;
            this.portListBox.Location = new System.Drawing.Point(501, 12);
            this.portListBox.Name = "portListBox";
            this.portListBox.Size = new System.Drawing.Size(117, 95);
            this.portListBox.TabIndex = 0;
            // 
            // connectPortButton
            // 
            this.connectPortButton.Location = new System.Drawing.Point(501, 113);
            this.connectPortButton.Name = "connectPortButton";
            this.connectPortButton.Size = new System.Drawing.Size(117, 23);
            this.connectPortButton.TabIndex = 1;
            this.connectPortButton.Text = "Połącz";
            this.connectPortButton.UseVisualStyleBackColor = true;
            this.connectPortButton.Click += new System.EventHandler(this.onPortConnectPressed);
            // 
            // commandHistoryTextBox
            // 
            this.commandHistoryTextBox.Location = new System.Drawing.Point(13, 13);
            this.commandHistoryTextBox.Multiline = true;
            this.commandHistoryTextBox.Name = "commandHistoryTextBox";
            this.commandHistoryTextBox.Size = new System.Drawing.Size(482, 251);
            this.commandHistoryTextBox.TabIndex = 2;
            // 
            // commandLineTextBox
            // 
            this.commandLineTextBox.Location = new System.Drawing.Point(13, 270);
            this.commandLineTextBox.Name = "commandLineTextBox";
            this.commandLineTextBox.Size = new System.Drawing.Size(401, 20);
            this.commandLineTextBox.TabIndex = 3;
            this.commandLineTextBox.TextChanged += new System.EventHandler(this.commandLineTextBox_TextChanged);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(420, 267);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "Wyśij";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.onSendCommandButtonPressed);
            // 
            // serverButton
            // 
            this.serverButton.Location = new System.Drawing.Point(501, 143);
            this.serverButton.Name = "serverButton";
            this.serverButton.Size = new System.Drawing.Size(116, 24);
            this.serverButton.TabIndex = 5;
            this.serverButton.Text = "Włącz server";
            this.serverButton.UseVisualStyleBackColor = true;
            this.serverButton.Click += new System.EventHandler(this.serverButtonClicked);
            // 
            // MainWindowViewImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 302);
            this.Controls.Add(this.serverButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.commandLineTextBox);
            this.Controls.Add(this.commandHistoryTextBox);
            this.Controls.Add(this.connectPortButton);
            this.Controls.Add(this.portListBox);
            this.Name = "MainWindowViewImpl";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox portListBox;
        private System.Windows.Forms.Button connectPortButton;
        private System.Windows.Forms.TextBox commandHistoryTextBox;
        private System.Windows.Forms.TextBox commandLineTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button serverButton;
    }
}

