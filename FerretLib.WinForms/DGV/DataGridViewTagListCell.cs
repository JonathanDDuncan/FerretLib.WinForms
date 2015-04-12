using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;
using FerretLib.WinForms.Controls;

namespace FerretLib.WinForms.DGV
{
    public class DataGridViewTagListCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            //Set the value of the editing control to the current cell value. 
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            var ctl = DataGridView.EditingControl as DgvTagListControl;
            // Use the default row value when Value property is null. 
            if (Value == null)
            {
                if (ctl != null)
                    ctl.TagValues = new List<string>();
            }
            else
            {
                if (ctl != null) ctl.TagValues = (List<string>)Value;
            }
        }

        public override Type EditType
        {
            get { return typeof(DgvTagListControl); }
        }

        public override Type ValueType
        {
            get { return typeof(List<string>); }
        }

        public override object DefaultNewRowValue
        {
            get { return new List<string>(); }
        }

        public object DataSource { get; set; }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            var ctrl = ControlToPaint(cellBounds.Width, value);

            var img = new Bitmap(cellBounds.Width, cellBounds.Height);
            ctrl.DrawToBitmap(img, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            graphics.DrawImage(img, cellBounds.Location);
        }

        private   DgvTagListControl ControlToPaint(int cellWidth, object value)
        {
            var ctrl = new DgvTagListControl {ComboBoxVisible = false};
           
            ctrl.SelectionItemList(GetDataSource());
            ctrl.TagValues = (List<string>) value;
            ctrl.Height = GetPreferredHeight(cellWidth, ctrl);
            return ctrl;
        }

        private IEnumerable<GroupedComboBoxItem> GetDataSource()
        {
            var oc = (DataGridViewTagListColumn) OwningColumn;
            var ds = oc.DataSource as IEnumerable<GroupedComboBoxItem>;
            return ds;
        }

        private static int GetPreferredHeight(int cellWidth, TagListControl ctrl)
        {
            ctrl.Width = cellWidth;
            return ctrl.GetTagPanelPreferredSize(new Size(cellWidth, 0)).Height;
        }

        protected override void OnClick(DataGridViewCellEventArgs e)
        {
           
            var cell = DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var cellValue =  cell.Value as List<string>;

            var popup = new TagListPopup {TagValues = cellValue, SelectionItemList = GetDataSource()};
            var result = popup.ShowDialog();

            var popupTagValues = popup.TagValues;
            popup.Close();
            if (result != DialogResult.OK) return;

            AddValuesIntoCellTagValues(cellValue, popupTagValues, cell);

            DataGridView.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }

        private void AddValuesIntoCellTagValues(List<string> cellValue, IEnumerable<string> popupTagValues, DataGridViewCell cell)
        {
            if (cellValue == null) return;
            cellValue.Clear();
            cellValue.AddRange(popupTagValues);

            if (cell != null) cell.OwningRow.Height = GetTagPreferedHeight(cellValue, cell.Size.Width);
        }

        private int GetTagPreferedHeight(List<string> value, int width)
        {
            var ctrl = new DgvTagListControl();

            ctrl.SelectionItemList(GetDataSource());
            ctrl.TagValues = value;
            ctrl.ComboBoxVisible = false;
         
            return ctrl.GetTagPanelPreferredSize(new Size(width, 0)).Height;;
        }


    }
}
