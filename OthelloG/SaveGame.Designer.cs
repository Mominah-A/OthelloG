namespace OthelloG
{
	partial class SaveGame
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
			this.cbSavedGames = new System.Windows.Forms.ComboBox();
			this.btnOverwrite = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cbSavedGames
			// 
			this.cbSavedGames.FormattingEnabled = true;
			this.cbSavedGames.Location = new System.Drawing.Point(17, 48);
			this.cbSavedGames.Name = "cbSavedGames";
			this.cbSavedGames.Size = new System.Drawing.Size(225, 21);
			this.cbSavedGames.TabIndex = 0;
			// 
			// btnOverwrite
			// 
			this.btnOverwrite.Location = new System.Drawing.Point(248, 47);
			this.btnOverwrite.Name = "btnOverwrite";
			this.btnOverwrite.Size = new System.Drawing.Size(75, 23);
			this.btnOverwrite.TabIndex = 1;
			this.btnOverwrite.Text = "Overwrite";
			this.btnOverwrite.UseVisualStyleBackColor = true;
			this.btnOverwrite.Click += new System.EventHandler(this.btnOverwrite_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(309, 35);
			this.label1.TabIndex = 2;
			this.label1.Text = "The maximum number of game states has been reached. Select the game you want to o" +
	"verwrite?";
			// 
			// SaveGame
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(337, 81);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOverwrite);
			this.Controls.Add(this.cbSavedGames);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveGame";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Overwrite Game State";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cbSavedGames;
		private System.Windows.Forms.Button btnOverwrite;
		private System.Windows.Forms.Label label1;
	}
}