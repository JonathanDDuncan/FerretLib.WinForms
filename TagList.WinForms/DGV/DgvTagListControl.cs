using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TagList.Controls;

namespace TagList.DGV
{
    public class DgvTagListControl : TagListControl , IDataGridViewEditingControl
    {
        private bool _valueChanged;
        private readonly TagListControl _tagList;
        private bool _comboBoxVisible;
        private bool _tagPanelAutoScroll;

        public DgvTagListControl()
        {
            InitializeComponent();
            _tagList = new TagListControl();
        }

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue  
        // property. 
        public object EditingControlFormattedValue
        {
            get { return _tagList.TagValues; }
            set { _tagList.TagValues = (List<string>)value; }
        }

        // Implements the  
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method. 
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the  
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method. 
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            _tagList.Font = dataGridViewCellStyle.Font;
           
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex  
        // property. 
        public int EditingControlRowIndex { get; set; }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey  
        // method. 
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
           return true;
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit  
        // method. 
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        // Implements the IDataGridViewEditingControl 
        // .RepositionEditingControlOnValueChange property. 
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl 
        // .EditingControlDataGridView property. 
        public DataGridView EditingControlDataGridView { get; set; }

        // Implements the IDataGridViewEditingControl 
        // .EditingControlValueChanged property. 
        public bool EditingControlValueChanged
        {
            get
            {
                return _valueChanged;
            }
            set
            {
                _valueChanged = value;
            }
        }

        // Implements the IDataGridViewEditingControl 
        // .EditingPanelCursor property. 
        public Cursor EditingPanelCursor
        {
            get
            {
                return _tagList.Cursor;
            }
        }

        public bool ComboBoxVisible
        {
            get { return _comboBoxVisible; }
            set
            {
                _comboBoxVisible = value;
                combFG.Visible = value;
            }
        }

       

        protected new void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell 
            // have changed.
            _valueChanged = true;
            EditingControlDataGridView.NotifyCurrentCellDirty(true);
            _tagList.OnValueChanged(eventargs);
        }

        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(DgvTagListControl));
            SuspendLayout();
            // 
            // DgvTagListControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            Name = "DgvTagListControl";
            TagValues = ((List<string>)(resources.GetObject("$this.TagValues")));
             
            ResumeLayout(false);

        }

        
    }
}
