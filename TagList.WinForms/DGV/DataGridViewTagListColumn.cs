using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TagList.DGV
{
    public class DataGridViewTagListColumn : DataGridViewColumn
    {
        public DataGridViewTagListColumn()
            : base(new DataGridViewTagListCell())
        {
        }

        private DataGridViewTagListCell DataGridViewTagListTemplate
        {
            get
            {
                return (DataGridViewTagListCell)CellTemplate;
            }
        }

      [
            DefaultValue(null),
            RefreshProperties(RefreshProperties.Repaint),
            AttributeProvider(typeof(IListSource)),
        ]
        public object DataSource
        {
            get
            {
                if (DataGridViewTagListTemplate == null)
                {
                    throw new InvalidOperationException("ComboBoxCellTemplate cannot be null.");
                }
                return DataGridViewTagListTemplate.DataSource;
            }
            set
            {
                if (DataGridViewTagListTemplate == null)
                {
                    throw new InvalidOperationException("ComboBoxCellTemplate cannot be null.");
                }
                DataGridViewTagListTemplate.DataSource = value;
                if (DataGridView == null) return;
                var dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    var dataGridViewCell = dataGridViewRow.Cells[Index] as DataGridViewTagListCell;
                    if (dataGridViewCell != null)
                    {
                        dataGridViewCell.DataSource = value;
                    }
                }
                //DataGridView.OnColumnCommonChange(this.Index);
            }
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a DgvTagListControl. 
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DgvTagListControl)))
                {
                    throw new InvalidCastException("Must be a DgvTagListControl");
                }
                base.CellTemplate = value;
            }
        }
    }
}
