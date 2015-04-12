using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

namespace FerretLib.WinForms.DGV
{
    //https://msdn.microsoft.com/en-us/library/system.windows.forms.idatagridvieweditingcontrol.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2
    //https://msdn.microsoft.com/en-us/library/7tas5c80.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-1
    public class DataGridViewTagListCell : DataGridViewTextBoxCell
    {
        private List<string> _displayValue;

        public DataGridViewTagListCell()
            : base()
        {
            // Use the short date format. 
            //this.Style.Format = "d";
        }
        private GroupedComboBoxItem[] groupedItems = new[] { 
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
                ctl.TagValues = new List<string>
                {
                    "5",
                    "6",
                    "8"
                };  
            }
            else
            {
                ctl.TagValues = (List<string>)this.Value;
            }
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that CalendarCell uses. 
                return typeof(DgvTagListControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains. 

                return typeof(List<string>);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value. 
                return new List<string>
                {
                    "5",
                    "6",
                    "8"
                };
            }
        }
        
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            //The first time in, make sure that we get the initial DisplayValue
            //if (_displayValue == null) SetDisplayValue((List<string>)value);
            //Override paint to pass DisplayValue instead of formattedValue
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _displayValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);

            //var ctrl = new DgvTagListControl { TagValues = (List<string>)value };
            var ctrl = new DgvTagListControl
            {
                
            };
           
            
            ctrl.SelectionItemList(groupedItems.AsEnumerable());
            ctrl.TagValues = (List<string>) value;
            ctrl.ComboBoxVisible = false;
            ctrl.Width = cellBounds.Width;
            var pSize = ctrl.TagPanel.GetPreferredSize(new Size(cellBounds.Width, 0));
            ctrl.Height = pSize.Height;
            //ctrl.Height = cellBounds.Height;


           
                //var b = DataGridView.CurrentCell;
                //if (b != null && b.OwningRow != null) b.OwningRow.Height = ctrl.TagPanelPreferredSize.Height;
           
            //ctrl.Refresh();
            //b.OwningRow.Height = ctrl.TagPanelPreferredSize.Height;

            var img = new Bitmap(cellBounds.Width, cellBounds.Height);
            ctrl.DrawToBitmap(img, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            graphics.DrawImage(img, cellBounds.Location);

            
        }

        private void SetDisplayValue(List<string> newValue)
        {
            _displayValue = newValue;
        }



        protected override void OnClick(DataGridViewCellEventArgs e)
        {

            //List<DataRow> objs = DataGridView.DataSource as List<DataRow>;
            //if (objs == null)
            //    return;
            //if (e.RowIndex < 0 || e.RowIndex >= objs.Count)
            //    return;
          
            //DgvTagListControl ctrl = objs[e.RowIndex].Ctrl;
            // //Take any action - I will just change the color for now.
            //ctrl.BackColor = Color.Red;
            //ctrl.Refresh();
            //InitializeEditingControl(e.RowIndex, new List<string>
            //{
            //    "5",
            //    "6",
            //    "8"
            //}, DataGridView.DefaultCellStyle);
       //     DataGridViewTagListCell cell = (DataGridViewTagListCell)
       //DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //DataGridViewCell cell = DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //DataGridView.CurrentCell = cell;
            //DataGridView.BeginEdit(true);

            var popup = new TagListPopup();
            var cell = DataGridView.CurrentCell;
            popup.TagValues = (List<string>)cell.Value;
            popup.SelectionItemList = groupedItems;
            var result = popup.ShowDialog();
            if (result == DialogResult.OK)
            {
               
                //cell.Value = popup.TagValues;
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
            
        }

       

        private int GetTagPreferedHeight(List<string> value, int width)
        {
            var ctrl = new DgvTagListControl
            {
            };
            var groupedItems = new[]
            {
                new GroupedComboBoxItem {Group = "Gases", Value = "1", Display = "Helium"},
                new GroupedComboBoxItem {Group = "Gases", Value = "2", Display = "Hydrogen"},
                new GroupedComboBoxItem {Group = "Gases", Value = "3", Display = "Oxygen"},
                new GroupedComboBoxItem {Group = "Gases", Value = "4", Display = "Argon"},
                new GroupedComboBoxItem {Group = "Metals", Value = "5", Display = "Iron"},
                new GroupedComboBoxItem {Group = "Metals", Value = "6", Display = "Lithium"},
                new GroupedComboBoxItem {Group = "Metals", Value = "7", Display = "Copper"},
                new GroupedComboBoxItem {Group = "Metals", Value = "8", Display = "Gold"},
                new GroupedComboBoxItem {Group = "Metals", Value = "9", Display = "Silver"},
                new GroupedComboBoxItem {Group = "Radioactive", Value = "10", Display = "Uranium"},
                new GroupedComboBoxItem {Group = "Radioactive", Value = "11", Display = "Plutonium"},
                new GroupedComboBoxItem {Group = "Radioactive", Value = "12", Display = "Americium"},
                new GroupedComboBoxItem {Group = "Radioactive", Value = "13", Display = "Radon"}
            };

            ctrl.SelectionItemList(groupedItems.AsEnumerable());
            ctrl.TagValues = value;
            ctrl.ComboBoxVisible = false;
            ctrl.Width = width;
            var pSize = ctrl.TagPanel.GetPreferredSize(new Size(width, 0));
            ctrl.Height = pSize.Height;
            //ctrl.Height = cellBounds.Height;
            return pSize.Height;
        }


    }
}
