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
            var groupedItems = GetAvailableTagValues();
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

            Column3.DataSource = GetAvailableTagValues();

            var colbox = new DataGridViewComboBoxColumn
            {
                DataSource = GetAvailableTagValues(),
                ValueMember = "Value",
                DisplayMember = "Display"
            };
            dataGridView1.Columns.Add(colbox);

        }

        private static IEnumerable<GroupedComboBoxItem> GetAvailableTagValues()
        {
            return new[] { 
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
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1.AutoResizeRows();
        }

        private void SetPreferedHeight(DataGridViewRow row)
        {
            var height = GetTagPreferedHeight();


            row.MinimumHeight = 2;
            row.Height = height;
        }

        private int GetTagPreferedHeight()
        {
            //TODO Get height from cell, set height taking into account other columns besides TagControl Column

            var groupedItems = GetAvailableTagValues();
            var ctrl = new DgvTagListControl {ComboBoxVisible = false, Width = Column3.Width};
            
            ctrl.SelectionItemList(groupedItems.AsEnumerable());
            ctrl.TagValues = DefaultTagValues();

            return ctrl.GetTagPanelPreferredSize(new Size(Column3.Width, 0)).Height;
        }

        private static List<string> DefaultTagValues()
        {
            return new List<string>
            {
                "5",
                "6",
                "8"
            };
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetPreferedHeightAllRows(dataGridView1);
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            SetPreferedHeightAllRows(dataGridView1);
        }
         
        private void dataGridView1_Validated(object sender, System.EventArgs e)
        {
            SetPreferedHeightAllRows(dataGridView1);
        }

        private void SetPreferedHeightAllRows(DataGridView gridView1)
        {
            foreach (DataGridViewRow row in gridView1.Rows)
            {
                SetPreferedHeight(row);
            }
        }
    }
}
