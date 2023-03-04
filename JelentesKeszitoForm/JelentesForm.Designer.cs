namespace JelentesKeszitoForm
{
    partial class JelentesForm
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
            this.categoryLabel = new System.Windows.Forms.Label();
            this.websiteComboBox = new System.Windows.Forms.ComboBox();
            this.websiteLabel = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(55, 84);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(62, 16);
            this.categoryLabel.TabIndex = 8;
            this.categoryLabel.Text = "Category";
            // 
            // websiteComboBox
            // 
            this.websiteComboBox.FormattingEnabled = true;
            this.websiteComboBox.Location = new System.Drawing.Point(123, 41);
            this.websiteComboBox.Name = "websiteComboBox";
            this.websiteComboBox.Size = new System.Drawing.Size(262, 24);
            this.websiteComboBox.TabIndex = 7;
            this.websiteComboBox.SelectedIndexChanged += new System.EventHandler(this.websiteComboBox_SelectedIndexChanged);
            // 
            // websiteLabel
            // 
            this.websiteLabel.AutoSize = true;
            this.websiteLabel.Location = new System.Drawing.Point(60, 44);
            this.websiteLabel.Name = "websiteLabel";
            this.websiteLabel.Size = new System.Drawing.Size(57, 16);
            this.websiteLabel.TabIndex = 6;
            this.websiteLabel.Text = "Website";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(183, 207);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(111, 26);
            this.createButton.TabIndex = 5;
            this.createButton.Text = "Create report";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(123, 81);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(262, 24);
            this.categoryComboBox.TabIndex = 9;
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(83, 125);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(34, 16);
            this.pathLabel.TabIndex = 10;
            this.pathLabel.Text = "Path";
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(123, 122);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(262, 22);
            this.pathTextBox.TabIndex = 11;
            this.pathTextBox.TextChanged += new System.EventHandler(this.pathTextBox_TextChanged);
            // 
            // JelentesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 285);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.websiteComboBox);
            this.Controls.Add(this.websiteLabel);
            this.Controls.Add(this.createButton);
            this.Name = "JelentesForm";
            this.Text = "Report";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.ComboBox websiteComboBox;
        private System.Windows.Forms.Label websiteLabel;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.TextBox pathTextBox;
    }
}

