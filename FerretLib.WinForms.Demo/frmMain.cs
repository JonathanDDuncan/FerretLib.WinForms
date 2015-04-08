using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

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
        }
 
    }
}
