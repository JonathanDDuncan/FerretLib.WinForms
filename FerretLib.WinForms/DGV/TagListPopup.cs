using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

namespace TagList.WinForms.DGV
{
    public partial class TagListPopup : Form
    {

        public TagListPopup()
        {
            InitializeComponent();
        }

        public List<string> TagValues { get; set; }

        public IEnumerable<GroupedComboBoxItem> SelectionItemList
        {
            set { tagListControl1.SelectionItemList(value); }
        }

        private void TagListPopup_Load(object sender, EventArgs e)
        {
            tagListControl1.TagValues = TagValues;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            TagValues = tagListControl1.TagValues;
        }
    }
}
