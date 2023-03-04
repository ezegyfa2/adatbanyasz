namespace AdatBanyaszForm
{
    partial class BanyaszForm
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
            this.collectButton = new System.Windows.Forms.Button();
            this.websiteLabel = new System.Windows.Forms.Label();
            this.websiteComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pageCountTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // collectButton
            // 
            this.collectButton.Location = new System.Drawing.Point(118, 129);
            this.collectButton.Name = "collectButton";
            this.collectButton.Size = new System.Drawing.Size(82, 26);
            this.collectButton.TabIndex = 0;
            this.collectButton.Text = "Collect data";
            this.collectButton.UseVisualStyleBackColor = true;
            this.collectButton.Click += new System.EventHandler(this.collectButton_Click);
            // 
            // websiteLabel
            // 
            this.websiteLabel.AutoSize = true;
            this.websiteLabel.Location = new System.Drawing.Point(45, 34);
            this.websiteLabel.Name = "websiteLabel";
            this.websiteLabel.Size = new System.Drawing.Size(57, 16);
            this.websiteLabel.TabIndex = 1;
            this.websiteLabel.Text = "Website";
            // 
            // websiteComboBox
            // 
            this.websiteComboBox.FormattingEnabled = true;
            this.websiteComboBox.Location = new System.Drawing.Point(108, 31);
            this.websiteComboBox.Name = "websiteComboBox";
            this.websiteComboBox.Size = new System.Drawing.Size(174, 24);
            this.websiteComboBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Page count";
            // 
            // pageCountTextBox
            // 
            this.pageCountTextBox.Location = new System.Drawing.Point(108, 71);
            this.pageCountTextBox.Name = "pageCountTextBox";
            this.pageCountTextBox.Size = new System.Drawing.Size(174, 22);
            this.pageCountTextBox.TabIndex = 4;
            this.pageCountTextBox.Text = "1";
            this.pageCountTextBox.TextChanged += new System.EventHandler(this.pageCountTextBox_TextChanged);
            // 
            // BanyaszForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 199);
            this.Controls.Add(this.pageCountTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.websiteComboBox);
            this.Controls.Add(this.websiteLabel);
            this.Controls.Add(this.collectButton);
            this.Name = "BanyaszForm";
            this.Text = "Data collector";
            this.Load += new System.EventHandler(this.BanyaszForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button collectButton;
        private System.Windows.Forms.Label websiteLabel;
        private System.Windows.Forms.ComboBox websiteComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pageCountTextBox;
    }
}

