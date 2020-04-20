namespace Gazette
{
	partial class ChatMenu
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
            this.ChatLog = new System.Windows.Forms.ListBox();
            this.UsersLog = new System.Windows.Forms.ListBox();
            this.ChatBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChatLog
            // 
            this.ChatLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatLog.FormattingEnabled = true;
            this.ChatLog.Location = new System.Drawing.Point(12, 12);
            this.ChatLog.Name = "ChatLog";
            this.ChatLog.Size = new System.Drawing.Size(279, 225);
            this.ChatLog.TabIndex = 0;
            // 
            // UsersLog
            // 
            this.UsersLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UsersLog.FormattingEnabled = true;
            this.UsersLog.Location = new System.Drawing.Point(297, 12);
            this.UsersLog.Name = "UsersLog";
            this.UsersLog.Size = new System.Drawing.Size(97, 225);
            this.UsersLog.TabIndex = 1;
            // 
            // ChatBox
            // 
            this.ChatBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatBox.Location = new System.Drawing.Point(12, 261);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.Size = new System.Drawing.Size(279, 20);
            this.ChatBox.TabIndex = 2;
            this.ChatBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatBox_KeyDown);
            // 
            // SendButton
            // 
            this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendButton.Location = new System.Drawing.Point(297, 261);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(97, 20);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ChatMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 293);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.UsersLog);
            this.Controls.Add(this.ChatLog);
            this.Name = "ChatMenu";
            this.Text = "ChatMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox ChatLog;
		private System.Windows.Forms.ListBox UsersLog;
		private System.Windows.Forms.TextBox ChatBox;
		private System.Windows.Forms.Button SendButton;
	}
}