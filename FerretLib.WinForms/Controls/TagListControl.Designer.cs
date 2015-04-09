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
            this.combFG = new DropDownControls.FilteredGroupedComboBox.FilteredGroupedComboBox();
            this.tagPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagPanel
            // 
            this.tagPanel.AutoScroll = true;
            this.tagPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tagPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tagPanel.Controls.Add(this.combFG);
            this.tagPanel.Location = new System.Drawing.Point(3, 3);
            this.tagPanel.Name = "tagPanel";
            this.tagPanel.Size = new System.Drawing.Size(468, 127);
            this.tagPanel.TabIndex = 1;
            // 
            // combFG
            // 
            this.combFG.BackColor = System.Drawing.Color.White;
            this.combFG.DataSource = null;
            this.combFG.DataView = null;
            this.combFG.Location = new System.Drawing.Point(3, 3);
            this.combFG.Name = "combFG";
            this.combFG.Size = new System.Drawing.Size(144, 21);
            this.combFG.TabIndex = 0;
            this.combFG.SelectionChangeCommitted += new System.EventHandler(this.combFG_SelectionChangeCommitted);
            this.combFG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.combFG_KeyDown);
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
        private FilteredGroupedComboBox combFG;
    }
}
