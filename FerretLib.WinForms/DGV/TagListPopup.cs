﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

namespace FerretLib.WinForms.DGV
{
    public partial class TagListPopup : Form
    {

        public TagListPopup()
        {
            InitializeComponent();
        }

        public List<string> TagValues { get; set; }

        public GroupedComboBoxItem[] SelectionItemList
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