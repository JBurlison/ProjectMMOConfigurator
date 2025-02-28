namespace ProjectMMOConfigurator.Forms
{
    partial class BatchEditForm
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
            this.modelTypeComboBox = new System.Windows.Forms.ComboBox();
            
            this.label2 = new System.Windows.Forms.Label();
            this.propertyComboBox = new System.Windows.Forms.ComboBox();
            
            this.label3 = new System.Windows.Forms.Label();
            this.skillNameTextBox = new System.Windows.Forms.TextBox();
            
            this.label4 = new System.Windows.Forms.Label();
            this.skillLevelTextBox = new System.Windows.Forms.TextBox();
            
            this.applyButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            
            this.resultsGroupBox = new System.Windows.Forms.GroupBox();
            this.resultsTextBox = new System.Windows.Forms.TextBox();
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model Type:";
            
            // modelTypeComboBox
            this.modelTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modelTypeComboBox.FormattingEnabled = true;
            this.modelTypeComboBox.Location = new System.Drawing.Point(106, 12);
            this.modelTypeComboBox.Name = "modelTypeComboBox";
            this.modelTypeComboBox.Size = new System.Drawing.Size(200, 21);
            this.modelTypeComboBox.TabIndex = 1;
            
            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Property Path:";
            
            // propertyComboBox
            this.propertyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.propertyComboBox.FormattingEnabled = true;
            this.propertyComboBox.Location = new System.Drawing.Point(106, 42);
            this.propertyComboBox.Name = "propertyComboBox";
            this.propertyComboBox.Size = new System.Drawing.Size(200, 21);
            this.propertyComboBox.TabIndex = 3;
            
            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Skill Name:";
            
            // skillNameTextBox
            this.skillNameTextBox.Location = new System.Drawing.Point(106, 72);
            this.skillNameTextBox.Name = "skillNameTextBox";
            this.skillNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.skillNameTextBox.TabIndex = 5;
            
            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Skill Level:";
            
            // skillLevelTextBox
            this.skillLevelTextBox.Location = new System.Drawing.Point(106, 102);
            this.skillLevelTextBox.Name = "skillLevelTextBox";
            this.skillLevelTextBox.Size = new System.Drawing.Size(100, 20);
            this.skillLevelTextBox.TabIndex = 7;
            this.skillLevelTextBox.Text = "1";
            
            // applyButton
            this.applyButton.Location = new System.Drawing.Point(106, 137);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 8;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            
            // closeButton
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(187, 137);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 9;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            
            // statusStrip1
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(484, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            
            // progressBar
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.Visible = false;
            
            // resultsGroupBox
            this.resultsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsGroupBox.Controls.Add(this.resultsTextBox);
            this.resultsGroupBox.Location = new System.Drawing.Point(12, 175);
            this.resultsGroupBox.Name = "resultsGroupBox";
            this.resultsGroupBox.Size = new System.Drawing.Size(460, 250);
            this.resultsGroupBox.TabIndex = 11;
            this.resultsGroupBox.TabStop = false;
            this.resultsGroupBox.Text = "Results";
            
            // resultsTextBox
            this.resultsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTextBox.Location = new System.Drawing.Point(3, 16);
            this.resultsTextBox.Multiline = true;
            this.resultsTextBox.Name = "resultsTextBox";
            this.resultsTextBox.ReadOnly = true;
            this.resultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resultsTextBox.Size = new System.Drawing.Size(454, 231);
            this.resultsTextBox.TabIndex = 0;
            
            // BatchEditForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(484, 450);
            this.Controls.Add(this.resultsGroupBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.skillLevelTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.skillNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.propertyComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.modelTypeComboBox);
            this.Controls.Add(this.label1);
            this.Name = "BatchEditForm";
            this.Text = "Batch Edit Skills";
            
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            
            this.resultsGroupBox.ResumeLayout(false);
            this.resultsGroupBox.PerformLayout();
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox modelTypeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox propertyComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox skillNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox skillLevelTextBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.GroupBox resultsGroupBox;
        private System.Windows.Forms.TextBox resultsTextBox;
    }
}
