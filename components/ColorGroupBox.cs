using Paint.Properties;

namespace Paint.components
{
    internal class ColorGroupBox: CustomGroupBox
    {
        private static Color[] DEFAULT_COLORS = new Color[] {
            Color.Red, Color.Blue, Color.Black,
            Color.Yellow, Color.Green, Color.White,
            Color.Orange, Color.Salmon, Color.Purple,
            Color.Orchid, Color.AliceBlue, Color.Aquamarine,
            Color.Plum, Color.Beige, Color.Bisque,
        };

        private static readonly string TITLE = "Colors";

        private static readonly Color DEFAULT_SELECT_COLOR = Color.Black;

        private static readonly Color DEFAULT_PREVIOUS_COLOR = Color.White;

        private ColorDialog colorDialog;

        private ColorButton selectButton;

        private ColorButton previousButton;
       
        private FlowLayoutPanel layout;

        private Button buttonNewColor;

        public event EventHandler OnSelect;

        public ColorGroupBox() : base(TITLE)
        {
            selectButton = (ColorButton) initSelectButton(new ColorButton());
            previousButton = (ColorButton) initPreviousButton(new ColorButton());
            layout = initLayout(new FlowLayoutPanel());

            selectButton.Color = DEFAULT_SELECT_COLOR;
            previousButton.Color = DEFAULT_PREVIOUS_COLOR;

            buttonNewColor = new ColorButton();
            buttonNewColor.BackgroundImage = Resources.Add;
            buttonNewColor.FlatAppearance.BorderSize = 0;
            buttonNewColor.FlatStyle = FlatStyle.Flat;
            buttonNewColor.Location = new Point(3, 3);
            buttonNewColor.Name = "buttonNewColor";
            buttonNewColor.Size = new Size(24, 24);
            buttonNewColor.TabIndex = 0;
            buttonNewColor.UseVisualStyleBackColor = true;
            buttonNewColor.Click += buttonNewColor_Click;
            buttonNewColor.Click += OnSelect_Click;

            selectButton.Click += OnSelect_Click;

            previousButton.Click += ChangeColor_Click;
            previousButton.Click += OnSelect_Click;
            InitDefalutValues();
        }

        private void InitDefalutValues()
        {
            foreach (Color color in DEFAULT_COLORS)
            {
                ColorButton colorButton = new ColorButton();
                colorButton.Color = color;
                colorButton.Click += ChangeColor_Click;
                colorButton.Click += OnSelect_Click;
                layout.Controls.Add(colorButton);
            }
        }

        private void buttonNewColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ColorButton newColorButton = new ColorButton();
                newColorButton.Color = colorDialog.Color;
                newColorButton.Click += ChangeColor_Click;

                ColorButton? buttonToRemove = null;

                foreach (Button button in layout.Controls)
                {
                    if (button is ColorButton)
                    {
                        ColorButton colorButton = (ColorButton)button;

                        if (colorButton.Color == newColorButton.Color)
                        {
                            buttonToRemove = colorButton;
                            break;
                        }
                    }
                }
                if (buttonToRemove != null)
                {
                    layout.Controls.Remove(buttonToRemove);
                }
                layout.Controls.Add(newColorButton);
                layout.Controls.SetChildIndex(newColorButton, 1);
            }
        }

        private void ChangeColor_Click(object sender, EventArgs e)
        {
            ChangeColor(((ColorButton)sender).Color);
            
        }

        private void ChangeColor(Color color)
        {
            if (selectButton.Color != color)
            {
                previousButton.Color = selectButton.Color;
                selectButton.Color = color;
            }
        }

        public Color getCurrentColor()
        {
            return selectButton.Color;
        }

        public void setCurrentColor(Color color)
        {
            ChangeColor(color);
        }

        private void OnSelect_Click(object sender, EventArgs e) 
        {
            OnSelect.Invoke(selectButton.Color, e);
        }
    }
}
