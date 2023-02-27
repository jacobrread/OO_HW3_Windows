using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using AppLayer.Command;
using AppLayer.DrawingComponents;

namespace Forests
{
    public partial class MainForm : Form
    {
        private readonly Invoker _invoker = new Invoker();
        private readonly CommandFactory _commandFactory = new CommandFactory();
        private readonly Drawing _drawing = new Drawing();
        private bool _forceRedraw;
        private string _currentCorvetteResource;
        private float _currentScale = 1;

        private enum PossibleModes
        {
            None,
            CorvetteDrawing,
            Selection
        };

        private PossibleModes _mode = PossibleModes.None;

        private Bitmap _imageBuffer;
        private Graphics _imageBufferGraphics;
        private Graphics _panelGraphics;
       
        public MainForm()
        {
            InitializeComponent();
            var corvetteFactory = new CorvetteFactory()
            {
                ResourceNamePattern = @"Corvettes.Graphics.{0}",
                ReferenceType = typeof(Program)
            };
            corvetteFactory.Initialize();

            _drawing.CorvetteFactory = corvetteFactory;
            _commandFactory.TargetDrawing = _drawing;
            _commandFactory.CorvetteFactory = corvetteFactory;
            _commandFactory.Invoker = _invoker;
            _invoker.Start();

            AddCorvetteButtonsToToolStrip();
        }

        private void AddCorvetteButtonsToToolStrip()
        {
            foreach (var corvette in _drawing.CorvetteFactory.Corvettes)
            {
                var corvetteButton = new ToolStripButton
                {
                    AutoSize = false,
                    CheckOnClick = true,
                    DisplayStyle = ToolStripItemDisplayStyle.Image,
                    Image = corvette.ToolImage,
                    Name = $"{corvette.CorvetteName.Replace(" ","")}Button",
                    Size = new Size(61, 61),
                    Text = corvette.CorvetteName
                };
                corvetteButton.Click += CorvetteButton_Click;
                drawingToolStrip.Items.Add(corvetteButton);
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ComputeDrawingPanelSize();
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            DisplayDrawing();
        }

        private void DisplayDrawing()
        {
            if (_imageBuffer == null)
            {
                _imageBuffer = new Bitmap(drawingPanel.Width, drawingPanel.Height);
                _imageBufferGraphics = Graphics.FromImage(_imageBuffer);
                _panelGraphics = drawingPanel.CreateGraphics();
            }

            if (_drawing.Draw(_imageBufferGraphics, _forceRedraw))
                _panelGraphics.DrawImageUnscaled(_imageBuffer, 0, 0);

            _forceRedraw = false;
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            _commandFactory.CreateAndDo("new");
        }

        private void ClearOtherSelectedTools(ToolStripButton ignoreItem)
        {
            foreach (ToolStripItem item in drawingToolStrip.Items)
            {
                var toolButton = item as ToolStripButton;
                if (toolButton != null && item!=ignoreItem && toolButton.Checked )
                    toolButton.Checked = false;
            }
        }

        private void PointerButton_Click(object sender, EventArgs e)
        {
            var button = sender as ToolStripButton;
            ClearOtherSelectedTools(button);

            if (button!=null && button.Checked)
            {
                _mode = PossibleModes.Selection;
                _currentCorvetteResource = string.Empty;
            }
            else
            {
                _commandFactory.CreateAndDo("deselect");
                _mode = PossibleModes.None;
            }
        }

        private void CorvetteButton_Click(object sender, EventArgs e)
        {
            var button = sender as ToolStripButton;
            ClearOtherSelectedTools(button);

            if (button != null && button.Checked)
                _currentCorvetteResource = button.Text;
            else
                _currentCorvetteResource = string.Empty;

            _commandFactory.CreateAndDo("deselect");
            _mode = (_currentCorvetteResource != string.Empty) ? PossibleModes.CorvetteDrawing : PossibleModes.None;
        }

        private void DrawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (_mode == PossibleModes.CorvetteDrawing)
            {
                if (!string.IsNullOrWhiteSpace(_currentCorvetteResource))
                    _commandFactory.CreateAndDo("add", _currentCorvetteResource, e.Location, _currentScale);
            }
            else if (_mode == PossibleModes.Selection)
                _commandFactory.CreateAndDo("select", e.Location);
        }

        private void Scale_Leave(object sender, EventArgs e)
        {
            _currentScale = ConvertToFloat(scale.Text, 0.01F, 99.0F, 1);
            scale.Text = _currentScale.ToString(CultureInfo.InvariantCulture);
        }

        private float ConvertToFloat(string text, float min, float max, float defaultValue)
        {
            var result = defaultValue;
            if (!string.IsNullOrWhiteSpace(text))
            {
                result = !float.TryParse(text, out result) ? defaultValue : Math.Max(min, Math.Min(max, result));
            }
            return result;
        }

        private void Scale_TextChanged(object sender, EventArgs e)
        {
            _currentScale = ConvertToFloat(scale.Text, 0.01F, 99.0F, 1);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                DefaultExt = "json",
                Multiselect = false,
                RestoreDirectory = true,
                Filter = @"JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _commandFactory.CreateAndDo("load", dialog.FileName);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = "json",
                RestoreDirectory = true,
                Filter = @"JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _commandFactory.CreateAndDo("save", dialog.FileName);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ComputeDrawingPanelSize();
        }

        private void ComputeDrawingPanelSize()
        {
            var width = Width - drawingToolStrip.Width;
            var height = Height - fileToolStrip.Height;

            drawingPanel.Size = new Size(width, height);
            drawingPanel.Location = new Point(drawingToolStrip.Width, fileToolStrip.Height);

            _imageBuffer = null;
            _forceRedraw = true;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            _commandFactory.CreateAndDo("remove");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _invoker?.Stop();
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            _invoker.Undo();
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            _invoker.Redo();
        }
    }
}
