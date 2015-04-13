using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

namespace TagList.WinForms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            var groupedItems = GetAvailableTagValues();
            tagListControl1.SelectionItemList(groupedItems.AsEnumerable());

            var bmp1 = new Bitmap(5,20);
            var bmp2 = new Bitmap(5,100);
            var g1 = Graphics.FromImage(bmp1);
            var g2 = Graphics.FromImage(bmp2);
            g1.Clear(Color.Blue);
            g2.Clear(Color.BlueViolet);
            
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
                },Value4 = bmp1
                },
                new {Value1 = "2", Value2 = "2", Value3 = new List<string>
                {
                    "12",
                    "13",
                    "7"
                },Value4 =  bmp2
                }
            };
            
            dataGridView1.DataSource = rows;

            Column3.DataSource = GetAvailableTagValues();
             
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

    }
}
