using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;
using TagList.Controls;

namespace TagList.DGV
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
            var ctrl = GetControltoDraw(value as List<string>);
            ctrl.Height = GetPreferredHeight(cellBounds.Width, ctrl);

            var blankimg = new Bitmap(cellBounds.Width, cellBounds.Height);
            var gblank = Graphics.FromImage(blankimg);
            gblank.Clear(Color.White);
            var img = new Bitmap(cellBounds.Width, cellBounds.Height);
            ctrl.DrawToBitmap(img, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            graphics.DrawImage(blankimg, cellBounds.Location);
            graphics.DrawImage(img, cellBounds.Location);
        }

        private IEnumerable<GroupedComboBoxItem> GetDataSource()
        {
            var oc = (DataGridViewTagListColumn)OwningColumn;
            var ds = oc.DataSource as IEnumerable<GroupedComboBoxItem>;
            return ds;
        }

        private static int GetPreferredHeight(int cellWidth, TagListControl ctrl)
        {
            ctrl.Width = cellWidth;
            return ctrl.GetTagPanelPreferredSize(new Size(cellWidth, 0)).Height;
        }

        //So that AutoResize know how tall it is
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            try
            {
                var ctrl = GetControltoDraw(Value as List<string>);
                var cellWidth = OwningColumn.Width;
                var preferedHeight = GetPreferredHeight(cellWidth, ctrl);

                return new Size(cellWidth, preferedHeight);
            }
            catch (Exception ex)
            {  //Sometimes throws error when trying to get Value because objects are not fully initialized yet
                Console.WriteLine(ex.ToString());
            }

            return constraintSize;
        }

        protected override void OnClick(DataGridViewCellEventArgs e)
        {

            var cell = DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var cellValue = cell.Value as List<string>;

            var popup = new TagListPopup { TagValues = cellValue, SelectionItemList = GetDataSource() };
            var result = popup.ShowDialog();

            var popupTagValues = popup.TagValues;
            popup.Close();
            if (result != DialogResult.OK) return;

            AddValuesIntoCellTagValues(cellValue, popupTagValues, cell);

            DataGridView.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }

        private static void AddValuesIntoCellTagValues(List<string> cellValue, IEnumerable<string> popupTagValues, DataGridViewCell cell)
        {
            if (cellValue == null) return;
            cellValue.Clear();
            cellValue.AddRange(popupTagValues);
        }

        private DgvTagListControl GetControltoDraw(List<string> value)
        {
            var ctrl = new DgvTagListControl();

            ctrl.SelectionItemList(GetDataSource());
            ctrl.TagValues = value;
            ctrl.ComboBoxVisible = false;
            ctrl.TagPanelAutoScroll = false;
            return ctrl;
        }
    }
}
