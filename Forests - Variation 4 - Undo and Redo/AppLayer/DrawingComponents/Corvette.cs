using System;
using System.Drawing;
using System.Reflection;

namespace AppLayer.DrawingComponents
{
    public abstract class Corvette
    {
        public static Color SelectionBackgroundColor { get; set; } = Color.DarkKhaki;

        public static Pen SelectedPen { get; set; } = new Pen(Color.DarkGray);
        public static Brush HandlesBrush { get; set; } = new SolidBrush(Color.Black);
        public static int HandleHalfSize { get; set; } = 3;
        public static Size ToolSize { get; set; } = new Size() { Width = 61, Height = 61};

        public Bitmap Image { get; private set; }
        public Bitmap ToolImage { get; private set; }
        public Bitmap ToolImageSelected { get; private set; }

        public abstract string CorvetteName { get; }
        public abstract string ResourceName { get; }

        public void LoadFromResource(string resourcePath, Type referenceTypeForAssembly)
        {
            var assembly = Assembly.GetAssembly(referenceTypeForAssembly);

            if (assembly == null) return;

            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null) return;
                Image = new Bitmap(stream);
                ToolImage = new Bitmap(Image, ToolSize);
                ToolImageSelected = new Bitmap(ToolSize.Width, ToolSize.Height);

                var g = Graphics.FromImage(ToolImageSelected);
                g.Clear(SelectionBackgroundColor);
                g.DrawImage(ToolImage, new Point() { X = 0, Y = 0 });
            }
        }

        public void Render(Graphics graphics, CorvetteExtrinsicState extrinsicState)
        {
            if (graphics == null || Image == null) return;

            DrawBasicCorvette(graphics, extrinsicState);

            DrawAdornments(graphics, extrinsicState);

            if (extrinsicState.IsSelected)
                DrawSelectionHandles(graphics, extrinsicState);
        }

        protected virtual void DrawBasicCorvette(Graphics graphics, CorvetteExtrinsicState extrinsicState)
        {
            graphics.DrawImage(Image,
                        new Rectangle(extrinsicState.Location.X, extrinsicState.Location.Y, extrinsicState.Size.Width, extrinsicState.Size.Height),
                        0, 0, Image.Width, Image.Height,
                        GraphicsUnit.Pixel);
        }

        protected virtual void DrawAdornments(Graphics graphics, CorvetteExtrinsicState extrinsicState) { }

        protected void DrawSelectionHandles(Graphics graphics, CorvetteExtrinsicState extrinsicState)
        {
            graphics.DrawRectangle(
                SelectedPen,
                extrinsicState.Location.X,
                extrinsicState.Location.Y,
                extrinsicState.Size.Width,
                extrinsicState.Size.Height);

            DrawActionHandle(graphics, extrinsicState.Location.X, extrinsicState.Location.Y);
            DrawActionHandle(graphics, extrinsicState.Location.X + extrinsicState.Size.Width, extrinsicState.Location.Y);
            DrawActionHandle(graphics, extrinsicState.Location.X, extrinsicState.Location.Y + extrinsicState.Size.Height);
            DrawActionHandle(graphics, extrinsicState.Location.X + extrinsicState.Size.Width, extrinsicState.Location.Y + extrinsicState.Size.Height);
        }

        private static void DrawActionHandle(Graphics graphics, int x, int y)
        {
            graphics.FillRectangle(HandlesBrush, x - HandleHalfSize, y - HandleHalfSize, HandleHalfSize * 2, HandleHalfSize * 2);
        }
    }
}
