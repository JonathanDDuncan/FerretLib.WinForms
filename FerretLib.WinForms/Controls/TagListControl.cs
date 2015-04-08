using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

namespace FerretLib.WinForms.Controls
{
    public partial class TagListControl : UserControl
    {
        public event EventHandler EditTagsEvent;

        protected virtual void OnEditTagsEvent()
        {
            var handler = EditTagsEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        private readonly HashSet<string> _tags;

        public int Count
        {
            get { return _tags.Count; }
        }

        public List<string> Tags
        {
            get
            {
                return _tags.ToList();
            }

            set
            {
                value = value ?? new List<string>();
                Clear();

                value.ForEach(x => _tags.Add(x));
                RebuildTagList();
            }
        }

        private void RebuildTagList()
        {
            filteredGroupableDropDown1.Text = "";
            foreach (var tag in _tags.OrderBy(x => x))
            {
                AddTagLabel(tag);
            }
        }

        private void AddTag(string tag)
        {
            if (_tags.Add(tag.Trim()))
                AddTagLabel(tag);
        }

        private void AddTagLabel(string tag)
        {
            var tagLabel = new TagLabelControl(tag) { Name = GetTagControlName(tag), TabStop = false };
            tagPanel.Controls.Add(tagLabel);
            tagLabel.DeleteClicked += TagLabel_DeleteClicked;
            tagLabel.DoubleClicked += TagLabel_DoubleClicked;
        }

        private void RemoveTag(string tag)
        {
            _tags.Remove(tag);
            var tagControl = tagPanel.Controls.Find(GetTagControlName(tag), true)[0];
            tagPanel.Controls.Remove(tagControl);
        }

        private void TagLabel_DeleteClicked(object sender, string tag)
        {
            RemoveTag(tag);
        }

        private void TagLabel_DoubleClicked(object sender, string tag)
        {
            RemoveTag(tag);
        }

        private static string GetTagControlName(string tag)
        {
            return "tagLabel_" + tag;
        }

        public void Clear()
        {
            _tags.Clear();
            while (tagPanel.Controls.Count > 2)
                tagPanel.Controls.RemoveAt(1);
        }

        public TagListControl()
        {
            InitializeComponent();
            _tags = new HashSet<string>();
            Clear();
        }

       public void SelectionItemList(IEnumerable<GroupedComboBoxItem> groupItems)
        {
            filteredGroupableDropDown1.FilterableGroupableDataSource(groupItems);
        }


        private void filteredGroupableDropDown1_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (filteredGroupableDropDown1.SelectedIndex == -1) return;
            var selected = filteredGroupableDropDown1.Text;
            AddTag(selected);
        }

        private void filteredGroupableDropDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (filteredGroupableDropDown1.SelectedIndex == -1) return;
            var selected = filteredGroupableDropDown1.Text;
            AddTag(selected);
        }

        private void btnEditTags_Click(object sender, System.EventArgs e)
        {
            OnEditTagsEvent();
        }
    }
}
