using System;
using System.Windows.Forms;

namespace FerretLib.WinForms.DGV
{
    public class DataGridViewTagListColumn : DataGridViewColumn
    {
        public DataGridViewTagListColumn()
            : base(new DgvTagListControl())
        {
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
