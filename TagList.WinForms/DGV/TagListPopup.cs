using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

namespace TagList.DGV
{
    public partial class TagListPopup : Form
    {

        public TagListPopup()
        {
            InitializeComponent();
        }

        public List<string> TagValues { get; set; }

        public IEnumerable<GroupedColoredComboBoxItem> SelectionItemList
        {
            set { tagListControl1.SelectionItemList(value); }
        }

        public Font LabelFont { get; set; }

        private void TagListPopup_Load(object sender, EventArgs e)
        {
            tagListControl1.LabelFont = LabelFont;
            tagListControl1.TagValues = TagValues;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            TagValues = tagListControl1.TagValues;
        }
    }
}
