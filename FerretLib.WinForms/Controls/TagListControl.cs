using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

namespace FerretLib.WinForms.Controls
{
    public partial class TagListControl : UserControl
    {
       
        private readonly Dictionary<string, string> _tags;
        private IEnumerable<GroupedComboBoxItem> _groupItems;

        public int Count
        {
            get { return _tags.Count; }
        }

        public List<string> Tags
        {
            get { return _tags.Keys.Select(x => x).ToList(); }

            set
            {
                SetTags(value ?? new List<string>());
            }
        }

        private void SetTags(List<string> value)
        {
            Clear();

            value.ForEach(x =>
            {
                var item = GetTag(x);
                if (item != null) _tags.Add(item.Value,item.Display);
            }
                );
            RebuildTagList();
        }

        private GroupedComboBoxItem GetTag(string id)
        {
            return _groupItems.FirstOrDefault(x => x.Value == id);
        }

        private void RebuildTagList()
        {
            filteredGroupableDropDown1.Text = "";
            foreach (var tag in _tags.OrderBy(x => x.Value))
            {
                AddTagLabel(tag.Value);
            }
        }

        private void AddTag(string text, string id)
        {
            id = id.Trim();
            text = text.Trim();

            if (_tags.ContainsKey(id)) return;
            _tags.Add(id, text);
            AddTagLabel(text);
        }

        private void AddTagLabel(string tag)
        {
            var tagLabel = new TagLabelControl(tag) { Name = GetTagControlName(tag), TabStop = false };
            tagPanel.Controls.Add(tagLabel);
            tagLabel.DeleteClicked += TagLabel_DeleteClicked;
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

       
        private static string GetTagControlName(string tag)
        {
            return "tagLabel_" + tag;
        }

        public void Clear()
        {
            _tags.Clear();
            while (tagPanel.Controls.Count > 1)
                tagPanel.Controls.RemoveAt(1);
        }

        public TagListControl()
        {
            InitializeComponent();
            _tags = new Dictionary<string, string>();
            Clear();
        }

        public void SelectionItemList(IEnumerable<GroupedComboBoxItem> groupItems)
        {
            var groupedComboBoxItems = groupItems as IList<GroupedComboBoxItem> ?? groupItems.ToList();
            _groupItems = groupedComboBoxItems;
            filteredGroupableDropDown1.FilterableGroupableDataSource(groupedComboBoxItems);
        }
        
        private void filteredGroupableDropDown1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            AddTagFromComboBox();
        }

        private void filteredGroupableDropDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            AddTagFromComboBox();
        }

        private void AddTagFromComboBox()
        {
            if (filteredGroupableDropDown1.SelectedIndex == -1) return;
            var selected = filteredGroupableDropDown1.Text;
            var value = filteredGroupableDropDown1.SelectedValue;
            AddTag(selected, value.ToString());
        }

    }
}
