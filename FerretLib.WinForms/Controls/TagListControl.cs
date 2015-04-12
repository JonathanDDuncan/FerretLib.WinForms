using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

namespace FerretLib.WinForms.Controls
{
    public partial class TagListControl : UserControl
    {
        public delegate void ValueChangedHandler(object sender, EventArgs args);

        public event ValueChangedHandler ValueChanged;

        protected virtual void OnValueChanged()
        {
            ValueChangedHandler handler = ValueChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private readonly Dictionary<string, string> _tags;
        private IEnumerable<GroupedComboBoxItem> _groupItems;
        

        public int Count
        {
            get { return _tags.Count; }
        }
        public Size TagPanelPreferredSize
        {
            get
            {
                if (tagPanel != null) return tagPanel.GetPreferredSize(new Size( tagPanel.Width,0));
                return new Size();
            }
        }

        public FlowLayoutPanel TagPanel
        {
            get { return tagPanel; }
            
        }

        public int TagPanelHeight
        {
            get { return tagPanel.Height; }
           
        }

        public List<string> TagValues
        {
            get { return _tags.Keys.Select(x => x).ToList(); }
            set { SetTagValues(value); }
        }

        private void SetTagValues(List<string> value)
        {
            Clear();

            if (value != null)
                value.ForEach(x =>
                     { var item = GetTag(x); if (item != null) _tags.Add(item.Value, item.Display); });
            RebuildTagList();
        }

        private void RebuildTagList()
        {
            combFG.Text = "";
            foreach (var tag in _tags.OrderBy(x => x.Value))
            {
                AddTagLabel(tag.Value, tag.Key);
            }
        }

        private void AddTag(string text, string value)
        {
            value = value.Trim();
            text = text.Trim();

            if (_tags.ContainsKey(value)) return;
            _tags.Add(value, text);
            AddTagLabel(text, value);
            OnValueChanged();
        }

        private void AddTagLabel(string text, string value)
        {
            var tagLabel = new TagLabelControl(text) { Name = GetTagControlName(value), TabStop = false };
            tagPanel.Controls.Add(tagLabel);
            tagLabel.DeleteClicked += TagLabel_DeleteClicked;
        }

        private void RemoveTag(string text)
        {
            var item = GetTagByText(text);
            if (item == null) return;
            _tags.Remove(item.Value);
            var tagControl = tagPanel.Controls.Find(GetTagControlName(item.Value), true)[0];
            tagPanel.Controls.Remove(tagControl);
            OnValueChanged();
        }

        private GroupedComboBoxItem GetTag(string value)
        {
            return _groupItems != null ? _groupItems.FirstOrDefault(x => x.Value == value) : null;
        }

        private GroupedComboBoxItem GetTagByText(string text)
        {
            return _groupItems.FirstOrDefault(x => x.Display == text);
        }

        private static string GetTagControlName(string value)
        {
            return "tagLabel_" + value;
        }

        public void Clear()
        {
            _tags.Clear();
            while (tagPanel.Controls.Count > 1)
                tagPanel.Controls.RemoveAt(1);
            OnValueChanged();
        }

        public TagListControl()
        {
            InitializeComponent();
            _tags = new Dictionary<string, string>();
            Clear();
        }

        private void AddTagFromComboBox()
        {
            if (combFG.SelectedIndex == -1) return;
            AddTag(combFG.Text, combFG.SelectedValue.ToString());
        }

        public void SelectionItemList(IEnumerable<GroupedComboBoxItem> groupItems)
        {
            var groupedComboBoxItems = groupItems as IList<GroupedComboBoxItem> ?? groupItems.ToList();
            _groupItems = groupedComboBoxItems;
            combFG.FilterableGroupableDataSource(groupedComboBoxItems);
        }

        private void TagLabel_DeleteClicked(object sender, string text)
        {
            RemoveTag(text);
        }

        private void combFG_SelectionChangeCommitted(object sender, EventArgs e)
        {
            AddTagFromComboBox();
        }

        private void combFG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            AddTagFromComboBox();

        }

        public virtual void OnValueChanged(EventArgs eventargs)
        {
            OnValueChanged();
        }
    }
}
