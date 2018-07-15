namespace KangarooPunchWithInterface
{
    partial class KangarooPunchInteface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KangarooPunchInteface));
            this.StatusText = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            this.LeftDropdown = new System.Windows.Forms.ComboBox();
            this.RightDropdown = new System.Windows.Forms.ComboBox();
            this.LeftTextBox = new System.Windows.Forms.TextBox();
            this.RightTextBox = new System.Windows.Forms.TextBox();
            this.LeftPicture = new System.Windows.Forms.PictureBox();
            this.RightPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusText
            // 
            this.StatusText.AutoSize = true;
            this.StatusText.Location = new System.Drawing.Point(12, 427);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(66, 25);
            this.StatusText.TabIndex = 0;
            this.StatusText.Text = "Status";
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.Red;
            this.ExitButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ExitButton.Location = new System.Drawing.Point(674, 12);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(100, 40);
            this.ExitButton.TabIndex = 5;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // LeftDropdown
            // 
            this.LeftDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LeftDropdown.FormattingEnabled = true;
            this.LeftDropdown.Location = new System.Drawing.Point(17, 281);
            this.LeftDropdown.Name = "LeftDropdown";
            this.LeftDropdown.Size = new System.Drawing.Size(313, 33);
            this.LeftDropdown.TabIndex = 6;
            this.LeftDropdown.SelectedIndexChanged += new System.EventHandler(this.LeftDropdown_SelectedIndexChanged);
            // 
            // RightDropdown
            // 
            this.RightDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RightDropdown.FormattingEnabled = true;
            this.RightDropdown.Location = new System.Drawing.Point(438, 281);
            this.RightDropdown.Name = "RightDropdown";
            this.RightDropdown.Size = new System.Drawing.Size(336, 33);
            this.RightDropdown.TabIndex = 7;
            this.RightDropdown.SelectedIndexChanged += new System.EventHandler(this.RightDropdown_SelectedIndexChanged);
            // 
            // LeftTextBox
            // 
            this.LeftTextBox.Location = new System.Drawing.Point(17, 320);
            this.LeftTextBox.Name = "LeftTextBox";
            this.LeftTextBox.Size = new System.Drawing.Size(313, 33);
            this.LeftTextBox.TabIndex = 8;
            this.LeftTextBox.TextChanged += new System.EventHandler(this.LeftTextBox_TextChanged);
            // 
            // RightTextBox
            // 
            this.RightTextBox.Location = new System.Drawing.Point(438, 320);
            this.RightTextBox.Name = "RightTextBox";
            this.RightTextBox.Size = new System.Drawing.Size(336, 33);
            this.RightTextBox.TabIndex = 9;
            this.RightTextBox.TextChanged += new System.EventHandler(this.RightTextBox_TextChanged);
            // 
            // LeftPicture
            // 
            this.LeftPicture.InitialImage = null;
            this.LeftPicture.Location = new System.Drawing.Point(59, 75);
            this.LeftPicture.Name = "LeftPicture";
            this.LeftPicture.Size = new System.Drawing.Size(200, 200);
            this.LeftPicture.TabIndex = 10;
            this.LeftPicture.TabStop = false;
            // 
            // RightPicture
            // 
            this.RightPicture.InitialImage = null;
            this.RightPicture.Location = new System.Drawing.Point(517, 75);
            this.RightPicture.Name = "RightPicture";
            this.RightPicture.Size = new System.Drawing.Size(200, 200);
            this.RightPicture.TabIndex = 11;
            this.RightPicture.TabStop = false;
            // 
            // KangarooPunchInteface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.ControlBox = false;
            this.Controls.Add(this.RightPicture);
            this.Controls.Add(this.LeftPicture);
            this.Controls.Add(this.RightTextBox);
            this.Controls.Add(this.LeftTextBox);
            this.Controls.Add(this.RightDropdown);
            this.Controls.Add(this.LeftDropdown);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.StatusText);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "KangarooPunchInteface";
            this.Text = "Kangaroo Punch Interface";
            ((System.ComponentModel.ISupportInitialize)(this.LeftPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StatusText;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ComboBox LeftDropdown;
        private System.Windows.Forms.ComboBox RightDropdown;
        private System.Windows.Forms.TextBox LeftTextBox;
        private System.Windows.Forms.TextBox RightTextBox;
        private System.Windows.Forms.PictureBox LeftPicture;
        private System.Windows.Forms.PictureBox RightPicture;
    }
}

