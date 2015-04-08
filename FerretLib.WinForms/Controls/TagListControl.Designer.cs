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
            this.txtTag = new System.Windows.Forms.TextBox();
            this.filteredGroupableDropDown1 = new DropDownControls.FilteredGroupedComboBox.FilteredGroupedComboBox();
            this.tagPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagPanel
            // 
            this.tagPanel.AutoScroll = true;
            this.tagPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tagPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tagPanel.Controls.Add(this.txtTag);
            this.tagPanel.Controls.Add(this.filteredGroupableDropDown1);
            this.tagPanel.Location = new System.Drawing.Point(3, 3);
            this.tagPanel.Name = "tagPanel";
            this.tagPanel.Size = new System.Drawing.Size(468, 127);
            this.tagPanel.TabIndex = 1;
            // 
            // txtTag
            // 
            this.txtTag.Location = new System.Drawing.Point(3, 3);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(144, 20);
            this.txtTag.TabIndex = 0;
            this.txtTag.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTag_KeyUp);
            // 
            // filteredGroupableDropDown1
            // 
            this.filteredGroupableDropDown1.BackColor = System.Drawing.Color.White;
            this.filteredGroupableDropDown1.DataSource = null;
            this.filteredGroupableDropDown1.DataView = null;
            this.filteredGroupableDropDown1.Location = new System.Drawing.Point(153, 3);
            this.filteredGroupableDropDown1.Name = "filteredGroupableDropDown1";
            this.filteredGroupableDropDown1.Size = new System.Drawing.Size(144, 21);
            this.filteredGroupableDropDown1.TabIndex = 0;
            this.filteredGroupableDropDown1.SelectionChangeCommitted += new System.EventHandler(this.filteredGroupableDropDown1_SelectionChangeCommitted);
            this.filteredGroupableDropDown1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filteredGroupableDropDown1_KeyDown);
            // 
            // TagListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagPanel);
            this.Name = "TagListControl";
            this.Size = new System.Drawing.Size(474, 133);
            this.tagPanel.ResumeLayout(false);
            this.tagPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel tagPanel;
        private System.Windows.Forms.TextBox txtTag;
        private FilteredGroupedComboBox filteredGroupableDropDown1;
    }
}
