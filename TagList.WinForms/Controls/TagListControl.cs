using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;
using TagList.DGV;

namespace TagList.Controls
{
    public partial class TagListControl : UserControl
    {
        public delegate void ValueChangedHandler(object sender, EventArgs args);

        public event ValueChangedHandler ValueChanged;

        protected virtual void OnValueChanged()
        {
            var handler = ValueChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private readonly Dictionary<string, Tuple<string, Color>> _tags;
        private IEnumerable<GroupedColoredComboBoxItem> _groupItems;
        private bool _visualOnly;


        public int Count
        {
            get { return _tags.Count; }
        }

        public List<string> TagValues
        {
            get { return _tags.Keys.Select(x => x).ToList(); }
            set { SetTagValues(value); }
        }

        public bool TagPanelAutoScroll
        {
            set { tagPanel.AutoScroll = value; }
        }

        public Font LabelFont { get; set; }

        private void SetTagValues(List<string> value)
        {
            Clear();

            if (value != null)
                value.ForEach(x =>
                     {
                         var item = GetTag(x); if (item != null)
                         {
                             if (!_tags.ContainsKey(item.Value))
                                 _tags.Add(item.Value, Tuple.Create(item.Display, item.Color));
                         }
                     });
            RebuildTagList();
        }

        private void RebuildTagList()
        {
            if (!_visualOnly)
                combFG.Text = "";
            foreach (var tag in _tags.OrderBy(x => x.Value))
            {
                AddTagLabel(tag.Value.Item1, tag.Key, tag.Value.Item2);
            }
        }

        private void AddTag(string text, string value, Color color)
        {
            value = value.Trim();
            text = text.Trim();

            if (_tags.ContainsKey(value)) return;
            _tags.Add(value, Tuple.Create(text, color));
            AddTagLabel(text, value, color);
            OnValueChanged();
        }

        private void AddTagLabel(string text, string value, Color color)
        {
            var tagLabel = new TagLabelControl(true) { Name = GetTagControlName(value), TabStop = false, Color = color };

            tagLabel.SetString(text, LabelFont);
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

        private GroupedColoredComboBoxItem GetTag(string value)
        {
            return _groupItems != null ? _groupItems.FirstOrDefault(x => x.Value == value) : null;
        }

        private GroupedColoredComboBoxItem GetTagByText(string text)
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

        public TagListControl(bool visualOnly = false)
        {
            if (!visualOnly)
            {
                InitializeComponent();
            }
            else
            {
                _visualOnly = true;
                InitializeComponentVisualOnly();
            }
            _tags = new Dictionary<string, Tuple<string, Color>>();
            Clear();
        }
        private void InitializeComponentVisualOnly()
        {
            this.tagPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.combFG = new DropDownControls.FilteredGroupedComboBox.FilteredGroupedComboBox();
            this.tagPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagPanel
            // 
            this.tagPanel.AutoScroll = true;
            this.tagPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tagPanel.BackColor = System.Drawing.Color.White;
            this.tagPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tagPanel.Controls.Add(this.combFG);
            this.tagPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagPanel.Location = new System.Drawing.Point(0, 0);
            this.tagPanel.Name = "tagPanel";
            this.tagPanel.Size = new System.Drawing.Size(474, 133);
            this.tagPanel.TabIndex = 1;
            // 
            // combFG
            // 
            //this.combFG.BackColor = System.Drawing.Color.White;
            //this.combFG.DataSource = null;
            //this.combFG.DataView = null;
            //this.combFG.Location = new System.Drawing.Point(3, 3);
            //this.combFG.Name = "combFG";
            //this.combFG.Size = new System.Drawing.Size(144, 21);
            //this.combFG.TabIndex = 0;
            //this.combFG.SelectionChangeCommitted += new System.EventHandler(this.combFG_SelectionChangeCommitted);
            //this.combFG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.combFG_KeyDown);
            // 
            // TagListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagPanel);
            this.Name = "TagListControl";
            this.Size = new System.Drawing.Size(474, 133);

            this.tagPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private void AddTagFromComboBox()
        {
            if (_visualOnly) return;
            if (combFG.SelectedIndex == -1) return;
            var color = (Color)((System.Data.DataRowView)(combFG.SelectedItem)).Row.ItemArray[3];
            AddTag(combFG.Text, combFG.SelectedValue.ToString(), color);
        }

        public void SelectionItemList(IEnumerable<GroupedColoredComboBoxItem> groupItems)
        {
            if (groupItems == null) return;
            var groupedColoredComboBoxItems = groupItems as IList<GroupedColoredComboBoxItem> ?? groupItems.ToList();
            _groupItems = groupedColoredComboBoxItems;

            if (_visualOnly) return;
            combFG.FilterableGroupableDataSource(groupedColoredComboBoxItems);
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

        public Size GetTagPanelPreferredSize(Size size)
        {
            return tagPanel.GetPreferredSize(size);
        }
    }
}
