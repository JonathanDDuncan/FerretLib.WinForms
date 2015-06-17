using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TagList.Controls
{
    internal partial class TagLabelControl : UserControl
    {
        public delegate void TagEvent(object sender, string tag);
        public event TagEvent DeleteClicked;
        public event TagEvent DoubleClicked;

        private string _value;
        private const int HEIGHT = 19;
        private const int LEFT_WIDTH = 9;
        private const int MARGIN = 1;
        private const int RIGHT_WIDTH = 15;

        #region ctor
        private bool _isDisposing;


        public TagLabelControl()
        {
            InitializeComponent();

        }

        public void SetString(string value, Font labelFont)
        {
            LabelFont = labelFont;
            Value = value;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Opaque | ControlStyles.FixedHeight, false);
            BackColor = Color.Transparent;

            RecreateBuffer();
            Redraw();
        }

        protected override void Dispose(bool disposing)
        {
            _isDisposing = true;
            if (disposing)
            {
                if (_backbuffer != null)
                    _backbuffer.Dispose();
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Properties
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                ResizeControl();
            }
        }

        public Font LabelFont { get; set; }
        public Color Color { get; set; }

        #endregion

        #region Input events

        private void Control_Click(object sender, EventArgs e)
        {
            if (!IsCursorOverDeleteButton()) return;
            if (DeleteClicked != null) DeleteClicked(this, Value);
        }

        private void Control_DoubleClick(object sender, EventArgs e)
        {
            if (IsCursorOverDeleteButton())
            {
                if (DeleteClicked != null) DeleteClicked(this, Value);
                return;
            }

            if (DoubleClicked != null) DoubleClicked(this, Value);
        }
        #endregion

        #region Rendering

        private Bitmap _backbuffer;
        private bool _drawDeleteButton;

        private void ResizeControl()
        {
            using (var g = Graphics.FromImage(_backbuffer))
            {
                //The extra width of an i margin
                var width = MeasureDisplayStringWidth(g, Value + "i", GetFont()) + LEFT_WIDTH + RIGHT_WIDTH;
                MaximumSize = new Size(width, HEIGHT);
                MinimumSize = MaximumSize;
                Width = width;
            }

            _deleteButtonRegion = new Rectangle(Width - 18, 1, 16, 16);
        }

        private Font GetFont()
        {
            return LabelFont ?? new Font("Buxton Sketch", 11);
        }

        private void RecreateBuffer()
        {
            if (_isDisposing)
                return;

            var newBuffer = new Bitmap(Math.Max(Width, 1), Math.Max(Height, 1), PixelFormat.Format32bppPArgb);

            if (_backbuffer != null)
            {
                using (var g = Graphics.FromImage(newBuffer))
                {
                    g.DrawImage(_backbuffer, Point.Empty);
                }
                _backbuffer.Dispose();
            }

            _backbuffer = newBuffer;

            DoInvalidate();
        }

        private void DoInvalidate()
        {
            if (Parent != null)
            {
                var rc = new Rectangle(Location, Size);
                Parent.Invalidate(rc, true);
            }
            Invalidate();
        }

        static public int MeasureDisplayStringWidth(Graphics graphics, string text, Font font)
        {
            var format = new StringFormat();
            var rect = new RectangleF(0, 0, 1000, 1000);
            CharacterRange[] ranges = { new CharacterRange(0, text.Length) };

            format.SetMeasurableCharacterRanges(ranges);

            var regions = graphics.MeasureCharacterRanges(text, font, rect, format);
            rect = regions[0].GetBounds(graphics);

            return (int)(rect.Right + 1.0f);
        }

        private void Redraw()
        {
            if (_backbuffer == null)
                return;
            try
            {
                using (var canvas = Graphics.FromImage(_backbuffer))
                {
                    canvas.Clear(Color.Transparent);
                    canvas.PixelOffsetMode = PixelOffsetMode.Half;
                    canvas.InterpolationMode = InterpolationMode.NearestNeighbor;
                    canvas.DrawImage(RemapColor(Properties.Resources.tagLabel_background, Color), new Rectangle(LEFT_WIDTH, 0, Width - LEFT_WIDTH - RIGHT_WIDTH, Height));
                    canvas.DrawImage(RemapColor(Properties.Resources.tagLabel_background_left, Color), 0, 0);
                    canvas.DrawImage(RemapColor(Properties.Resources.tagLabel_background_right, Color), Width - Properties.Resources.tagLabel_background_right.Width, 0);
                    if (_drawDeleteButton)
                        canvas.DrawImage(RemapColor(Properties.Resources.icon_round_delete, Color), _deleteButtonRegion);

                    canvas.DrawString(Value, GetFont(), Brushes.Black, LEFT_WIDTH, 1);
                }
            }
            catch { }
            Refresh();

        }

        private Bitmap RemapColor(Bitmap image, Color color)
        {

            // Set the image attribute's color mappings
            var colorMap = new ColorMap[8];
            colorMap[0] = new ColorMap { OldColor = GetColor("#FFF8FF3A"), NewColor = color };
            colorMap[1] = new ColorMap { OldColor = GetColor("#FFCFAA18"), NewColor = Brightness(color, 0.9) };
            colorMap[2] = new ColorMap { OldColor = GetColor("#FFB36E00"), NewColor = Brightness(color, 1.5) };
            colorMap[3] = new ColorMap { OldColor = GetColor("#FFDFCB25"), NewColor = Brightness(color, 0.9) };
            colorMap[4] = new ColorMap { OldColor = GetColor("#FFF8FE3A"), NewColor = color };
            colorMap[5] = new ColorMap { OldColor = GetColor("#FFC28D0D"), NewColor = Brightness(color, 1.5) };
            colorMap[6] = new ColorMap { OldColor = GetColor("#A5B36E00"), NewColor = Brightness(color, 1.5) };
            colorMap[7] = new ColorMap { OldColor = GetColor("#37B36E00"), NewColor = Brightness(color, 1.5) };
        
            var attr = new ImageAttributes();
            attr.SetRemapTable(colorMap);
            var g = Graphics.FromImage(image);
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attr);
            return image;
        }

        private static Color Brightness(Color c1, double factor)
        {
            return Color.FromArgb(c1.A, Math.Min((int)(c1.R * factor), 255), Math.Min((int)(c1.G * factor), 255), Math.Min((int)(c1.B * factor), 255));
        }

        private static Color GetColor(string colorString)
        {
            var cc = new ColorConverter();
            var convertFromString = cc.ConvertFromString(colorString);
            if (convertFromString != null)
                return (Color)convertFromString;

            return Color.Empty;
        }

        #endregion
        #region Event overrides
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = Cursors.Default;

            if (!_drawDeleteButton) return;
            _drawDeleteButton = false;
            Redraw();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = IsCursorOverDeleteButton() ? Cursors.Hand : Cursors.Default;

            if (_drawDeleteButton) return;
            _drawDeleteButton = true;
            Redraw();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecreateBuffer();
            Redraw();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!_isDisposing && _backbuffer != null)
                e.Graphics.DrawImage(_backbuffer, Point.Empty);
        }
        #endregion

        #region Misc logic
        private Rectangle _deleteButtonRegion;
        private bool IsCursorOverDeleteButton()
        {
            return _deleteButtonRegion.Contains(PointToClient(Cursor.Position));
        }
        #endregion
    }
}
