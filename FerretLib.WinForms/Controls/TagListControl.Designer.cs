using DropDownControls.FilteredGroupedComboBox;

namespace FerretLib.WinForms.Controls
{
    partial class TagListControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tagPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.filteredGroupableDropDown1 = new DropDownControls.FilteredGroupedComboBox.FilteredGroupedComboBox();
            this.btnEditTags = new System.Windows.Forms.Button();
            this.tagPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagPanel
            // 
            this.tagPanel.AutoScroll = true;
            this.tagPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tagPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tagPanel.Controls.Add(this.filteredGroupableDropDown1);
            this.tagPanel.Controls.Add(this.btnEditTags);
            this.tagPanel.Location = new System.Drawing.Point(3, 3);
            this.tagPanel.Name = "tagPanel";
            this.tagPanel.Size = new System.Drawing.Size(468, 127);
            this.tagPanel.TabIndex = 1;
            // 
            // filteredGroupableDropDown1
            // 
            this.filteredGroupableDropDown1.BackColor = System.Drawing.Color.White;
            this.filteredGroupableDropDown1.DataSource = null;
            this.filteredGroupableDropDown1.DataView = null;
            this.filteredGroupableDropDown1.Location = new System.Drawing.Point(3, 3);
            this.filteredGroupableDropDown1.Name = "filteredGroupableDropDown1";
            this.filteredGroupableDropDown1.Size = new System.Drawing.Size(144, 21);
            this.filteredGroupableDropDown1.TabIndex = 0;
            this.filteredGroupableDropDown1.SelectionChangeCommitted += new System.EventHandler(this.filteredGroupableDropDown1_SelectionChangeCommitted);
            this.filteredGroupableDropDown1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filteredGroupableDropDown1_KeyDown);
            // 
            // btnEditTags
            // 
            this.btnEditTags.Location = new System.Drawing.Point(153, 3);
            this.btnEditTags.Name = "btnEditTags";
            this.btnEditTags.Size = new System.Drawing.Size(60, 23);
            this.btnEditTags.TabIndex = 1;
            this.btnEditTags.Text = "Edit Tags";
            this.btnEditTags.UseVisualStyleBackColor = true;
            this.btnEditTags.Click += new System.EventHandler(this.btnEditTags_Click);
            // 
            // TagListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagPanel);
            this.Name = "TagListControl";
            this.Size = new System.Drawing.Size(474, 133);
            this.tagPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel tagPanel;
        private FilteredGroupedComboBox filteredGroupableDropDown1;
        private System.Windows.Forms.Button btnEditTags;
    }
}
