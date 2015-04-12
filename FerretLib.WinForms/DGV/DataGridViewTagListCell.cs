using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;
using FerretLib.WinForms.Controls;

namespace FerretLib.WinForms.DGV
{
    public class DataGridViewTagListCell : DataGridViewTextBoxCell
    {
        private List<string> _displayValue;

        private static GroupedComboBoxItem[] groupedItems = new[] { 
                new GroupedComboBoxItem{ Group = "Gases", Value = "1", Display = "Helium" }, 
				new GroupedComboBoxItem{ Group = "Gases", Value = "2", Display = "Hydrogen" },
				new GroupedComboBoxItem{ Group = "Gases", Value = "3", Display = "Oxygen" },
				new GroupedComboBoxItem{ Group = "Gases", Value = "4", Display = "Argon" },
				new GroupedComboBoxItem{ Group = "Metals", Value = "5", Display = "Iron" },
				new GroupedComboBoxItem{ Group = "Metals", Value = "6", Display = "Lithium" },
				new GroupedComboBoxItem{ Group = "Metals", Value = "7", Display = "Copper" },
				new GroupedComboBoxItem{ Group = "Metals", Value = "8", Display = "Gold" },
				new GroupedComboBoxItem{ Group = "Metals", Value = "9", Display = "Silver" },
				new GroupedComboBoxItem{ Group = "Radioactive", Value = "10", Display = "Uranium" },
				new GroupedComboBoxItem{ Group = "Radioactive", Value = "11", Display = "Plutonium" },
				new GroupedComboBoxItem{ Group = "Radioactive", Value = "12", Display = "Americium" },
				new GroupedComboBoxItem{ Group = "Radioactive", Value = "13", Display = "Radon" } 
			};

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

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            var ctrl = ControlToPaint(cellBounds.Width, value);

            var img = new Bitmap(cellBounds.Width, cellBounds.Height);
            ctrl.DrawToBitmap(img, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            graphics.DrawImage(img, cellBounds.Location);
        }

        private static DgvTagListControl ControlToPaint(int cellWidth, object value)
        {
            var ctrl = new DgvTagListControl();

            ctrl.SelectionItemList(groupedItems.AsEnumerable());
            ctrl.TagValues = (List<string>) value;
            ctrl.ComboBoxVisible = false;
            ctrl.Height = GetPreferredHeight(cellWidth, ctrl);
            return ctrl;
        }

        private static int GetPreferredHeight(int cellWidth, TagListControl ctrl)
        {
            ctrl.Width = cellWidth;
            return ctrl.GetTagPanelPreferredSize(new Size(cellWidth, 0)).Height;
        }

        protected override void OnClick(DataGridViewCellEventArgs e)
        {
            var popup = new TagListPopup();
            var cell = DataGridView.CurrentCell;
            popup.TagValues = (List<string>)cell.Value;
            popup.SelectionItemList = groupedItems;
            var result = popup.ShowDialog();
            if (result != DialogResult.OK) return;

            var value = (List<string>)cell.Value;
            value.Clear();
            value.AddRange(popup.TagValues);

            popup.Close();
            cell.Selected = true;
            cell.Selected = false;
            var height = GetTagPreferedHeight(value, cell.Size.Width);
            DataGridViewCell cell1 = DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell1.OwningRow.Height = height;
            DataGridView.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }

        private static int GetTagPreferedHeight(List<string> value, int width)
        {
            var ctrl = new DgvTagListControl();

            ctrl.SelectionItemList(groupedItems.AsEnumerable());
            ctrl.TagValues = value;
            ctrl.ComboBoxVisible = false;
         
            return ctrl.GetTagPanelPreferredSize(new Size(width, 0)).Height;;
        }


    }
}
