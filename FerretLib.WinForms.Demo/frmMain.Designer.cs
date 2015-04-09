namespace FerretLib.WinForms
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tagListControl1 = new FerretLib.WinForms.Controls.TagListControl();
            this.SuspendLayout();
            // 
            // tagListControl1
            // 
            this.tagListControl1.Location = new System.Drawing.Point(54, 28);
            this.tagListControl1.Name = "tagListControl1";
            this.tagListControl1.Size = new System.Drawing.Size(474, 133);
            this.tagListControl1.TabIndex = 0;
            this.tagListControl1.TagValues = ((System.Collections.Generic.List<string>)(resources.GetObject("tagListControl1.Tags")));
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 264);
            this.Controls.Add(this.tagListControl1);
            this.Name = "FrmMain";
            this.Text = "frmMain";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TagListControl tagListControl1;


    }
}