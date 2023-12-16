using Paint.components.Drawable;
using Paint.components.Tools;
using Paint.Properties;

namespace Paint.components
{
    internal class StateGroupBox: CustomGroupBox
    {

        private static readonly string TITLE = "In use";

        private static readonly Image NON_EDITABLE_COLOR = Resources.Padlock;

        private static readonly Image DISABLED_COLOR = Resources.DisableColor;

        private static readonly string TEXT_DISABLED_PEN = "No pen";

        private static readonly string TEXT_DISABLED_BRUSH = "No brush";

        private ImageButton toolButton;

        private ImageButton colorButtonPen;

        private ImageButton colorButtonBrush;

        private bool isEditablePen = true;

        private bool isEditableBrush = true;

        private bool isDisablePen = false;

        private bool isDisableBrush = false;

        private Label toolLabel;

        private Label colorLabelPen;

        private Label colorLabelBrush;

        public event EventHandler? OnPenColorChanged;

        public event EventHandler? OnBrushColorChanged;

        public StateGroupBox() : base(TITLE)
        {
            toolButton = (ImageButton)initSelectButton(new ImageButton());
            colorButtonPen = (ImageButton)initPreviousButton(new ImageButton());
            colorButtonBrush = (ImageButton)initPreviousButton(new ImageButton());
            colorButtonBrush.Location = new Point(135, 76);

            toolLabel = new Label();
            toolLabel.AutoSize = true;
            toolLabel.Location = new Point(44, 36);
            toolLabel.Name = "toolUseLabel";
            toolLabel.Size = new Size(90, 22);
            toolLabel.Text = "";

            colorLabelPen = new Label();
            colorLabelPen.AutoSize = true;
            colorLabelPen.Location = new Point(44, 84);
            colorLabelPen.Name = "colorLabelPen";
            colorLabelPen.Size = new Size(90, 22);
            colorLabelPen.Text = "";

            colorLabelBrush = new Label();
            colorLabelBrush.AutoSize = true;
            colorLabelBrush.Location = new Point(173, 84);
            colorLabelBrush.Name = "colorLabelBrush";
            colorLabelBrush.Size = new Size(90, 22);
            colorLabelBrush.Text = "";

            Controls.Add(toolLabel);
            Controls.Add(colorLabelPen);
            Controls.Add(colorLabelBrush);

            colorButtonPen.Click += OnClosePen;
            colorButtonBrush.Click += OnCloseBrush;
        }


        private void OnPenColorChanged_Event(Color color)
        {

            OnPenColorChanged?.Invoke(color, EventArgs.Empty);
        }

        private void OnBrushColorChanged_Event(Color color)
        {
            OnBrushColorChanged?.Invoke(color, EventArgs.Empty);
        }

        private void OnClosePen(object? sender, EventArgs e)
        {
            if(!isDisablePen)
            {
                if (isEditablePen)
                {
                    colorButtonPen.Image = NON_EDITABLE_COLOR;
                    isEditablePen = false;
                }
                else
                {
                    colorButtonPen.Image = null;
                    isEditablePen = true;
                }
            }
            else
            {
            }
        }

        private void OnCloseBrush(object? sender, EventArgs e)
        {
            if (!isDisableBrush)
            {
                if (isEditableBrush)
                {
                    colorButtonBrush.Image = NON_EDITABLE_COLOR;
                    isEditableBrush = false;
                }
                else
                {
                    colorButtonBrush.Image = null;
                    isEditableBrush = true;
                }
            }
        }

        public void SetFigure(Figure figure) 
        {
            toolButton.Image = figure.image;
            toolButton.Tag = figure;
            toolLabel.Text = figure.name;

            IsPenDisabled(!figure.hasPen);
            IsBrushDisabled(!figure.hasBrush);
        }

        public void SetTool(Tool tool) 
        { 
            toolButton.Image = tool.image;
            toolButton.Tag = tool;
            toolLabel.Text = tool.name;

            IsPenDisabled(!tool.hasPen);
            IsBrushDisabled(!tool.hasBrush);
        }

        public bool hasFigure()
        {
            return toolButton.Tag is Figure;
        }

        public bool hasTool() 
        {
            return toolButton.Tag is Tool;    
        }

        public void IsPenDisabled(bool isDisabled)
        {
            isDisablePen = isDisabled;
            if (isDisablePen)
            {
                colorLabelPen.Text = TEXT_DISABLED_PEN;
                colorButtonPen.BackColor = Color.Transparent;
                colorButtonPen.Image = DISABLED_COLOR;
                OnPenColorChanged_Event(Color.Empty);
                isEditablePen = true;
            }
            else
            {
                colorButtonPen.Image = null;
            }
        }

        public void IsBrushDisabled(bool isDisabled)
        {
            isDisableBrush = isDisabled;
            if (isDisableBrush)
            {
                colorLabelBrush.Text = TEXT_DISABLED_BRUSH;
                colorButtonBrush.BackColor = Color.Transparent;
                colorButtonBrush.Image = DISABLED_COLOR;
                OnBrushColorChanged_Event(Color.Empty);
                isEditableBrush = true;
            }
            else
            {
                colorButtonBrush.Image = null;
            }
        }


        public void SetPenColor(Color color)
        {
            if(!isDisablePen) 
            {
                if (isEditablePen)
                {
                    colorButtonPen.BackColor = color;
                    colorLabelPen.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                    OnPenColorChanged_Event(color);
                }
                else
                {
                    colorButtonPen.Image = NON_EDITABLE_COLOR;
                }
            }
        }

        public void isEditablePenColor(bool isEditable)
        {
            isEditablePen = isEditable;
        }

        public void SetBrushColor(Color color)
        {
            if(!isDisableBrush)
            {
                if (isEditableBrush)
                {
                    colorButtonBrush.BackColor = color;
                    colorLabelBrush.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                    OnBrushColorChanged_Event(color);
                }
                else
                {
                    colorButtonBrush.Image = NON_EDITABLE_COLOR;
                }
            }
        }

        public void isEditableBrushColor(bool isEditable)
        {
            isEditableBrush = isEditable;
        }

        public Color GetPenColor()
        {
            return colorButtonPen.BackColor;
        }

        public Color GetBrushColor()
        {
            return colorButtonBrush.BackColor;
        }
    }
}
