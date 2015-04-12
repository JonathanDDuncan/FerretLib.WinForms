using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;
using FerretLib.WinForms.DGV;

namespace FerretLib.WinForms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            var groupedItems = new[] { 
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
            tagListControl1.SelectionItemList(groupedItems.AsEnumerable());
            tagListControl1.TagValues = new List<string>
            {
                "3",
                "7",
                "12"
            };

            var rows = new[]
            {
                new {Value1 = "1", Value2 = "1", Value3 = new List<string>
                {
                    "5",
                    "6",
                    "8"
                }
                },
                new {Value1 = "2", Value2 = "2", Value3 = new List<string>
                {
                    "12",
                    "13",
                    "7"
                }}
            };
            
            dataGridView1.DataSource = rows;


        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1.AutoResizeRows();
        }

        private void dataGridView1_RowHeightInfoNeeded(object sender, DataGridViewRowHeightInfoNeededEventArgs e)
        {
            //var ctrl = new DgvTagListControl
            //{

            //};
            //var groupedItems = new[] { 
            //    new GroupedComboBoxItem{ Group = "Gases", Value = "1", Display = "Helium" }, 
            //    new GroupedComboBoxItem{ Group = "Gases", Value = "2", Display = "Hydrogen" },
            //    new GroupedComboBoxItem{ Group = "Gases", Value = "3", Display = "Oxygen" },
            //    new GroupedComboBoxItem{ Group = "Gases", Value = "4", Display = "Argon" },
            //    new GroupedComboBoxItem{ Group = "Metals", Value = "5", Display = "Iron" },
            //    new GroupedComboBoxItem{ Group = "Metals", Value = "6", Display = "Lithium" },
            //    new GroupedComboBoxItem{ Group = "Metals", Value = "7", Display = "Copper" },
            //    new GroupedComboBoxItem{ Group = "Metals", Value = "8", Display = "Gold" },
            //    new GroupedComboBoxItem{ Group = "Metals", Value = "9", Display = "Silver" },
            //    new GroupedComboBoxItem{ Group = "Radioactive", Value = "10", Display = "Uranium" },
            //    new GroupedComboBoxItem{ Group = "Radioactive", Value = "11", Display = "Plutonium" },
            //    new GroupedComboBoxItem{ Group = "Radioactive", Value = "12", Display = "Americium" },
            //    new GroupedComboBoxItem{ Group = "Radioactive", Value = "13", Display = "Radon" } 
			 
            //};

            //ctrl.SelectionItemList(groupedItems.AsEnumerable());
            //ctrl.TagValues = new List<string>
            //{
            //    "5",
            //    "6",
            //    "8"
            //};
            //ctrl.ComboBoxVisible = false;
            //ctrl.Width = Column3.Width;
            ////ctrl.Height = cellBounds.Height;



          
           
            //e.MinimumHeight = ctrl.TagPanelPreferredSize.Height;
            //e.Height = e.MinimumHeight;
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            
        }

        private void SetPreferedMinimumHeight(DataGridViewRow row)
        {
            var height = GetTagPreferedHeight();


            if (dataGridView1.CurrentRow != null)
                row.MinimumHeight = height;
        }

        private void SetPreferedHeight(DataGridViewRow row)
        {
            var height = GetTagPreferedHeight();


            row.MinimumHeight = 2;
                row.Height = height;
        }
        private int GetTagPreferedHeight()
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
            ctrl.TagValues = new List<string>
            {
                "5",
                "6",
                "8"
            };
            ctrl.ComboBoxVisible = false;
            ctrl.Width = Column3.Width;
            var pSize = ctrl.TagPanel.GetPreferredSize(new Size(Column3.Width, 0));
            ctrl.Height = pSize.Height;
            //ctrl.Height = cellBounds.Height;
            return pSize.Height;
        }

        private void dataGridView1_Layout(object sender, LayoutEventArgs e)
        {
            
             
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                SetPreferedHeight(row);
            }
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                SetPreferedHeight(row);
            }
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_Validated(object sender, System.EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                SetPreferedHeight(row);
            }
        }

        private void FrmMain_Load(object sender, System.EventArgs e)
        {
            
        }

    }
}
