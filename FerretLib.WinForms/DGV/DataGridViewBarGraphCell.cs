﻿using System.Drawing;
using System.Windows.Forms;

namespace FerretLib.WinForms.DGV
{
   public class DataGridViewBarGraphCell : DataGridViewTextBoxCell
    {
       protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates cellState, object value, object formattedValue, string errorText, 
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, 
           DataGridViewPaintParts paintParts)
       {
           base.Paint(graphics, clipBounds,
             cellBounds, rowIndex, cellState,
             value, formattedValue, errorText,
             cellStyle, advancedBorderStyle,
             paintParts);
       }

   
    }


}