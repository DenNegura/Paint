using Paint.Properties;

namespace Paint.components
{
    internal class ColorGroupBox: GroupBox
    {
        private static Color[] DEFAULT_COLORS = new Color[] {
            Color.Red, Color.Blue, Color.Black,
            Color.Yellow, Color.Green, Color.White,
            Color.Orange, Color.Salmon, Color.Purple,
            Color.Orchid, Color.AliceBlue, Color.Aquamarine,
            Color.Plum, Color.Beige, Color.Bisque,
        };

        private ColorDialog colorDialog;

        private ColorButton buttonColorSelect;

        private ColorButton buttonColorPrevious;
       
        private FlowLayoutPanel colorsBox;

        private Button buttonNewColor;

        public ColorGroupBox() 
        {
            InitializeComponents();
            IntializeColorsBox();
        }
        private void InitializeComponents()
        {
            colorDialog = new ColorDialog();
            buttonColorSelect = new ColorButton(Color.Black);
            buttonColorPrevious = new ColorButton(Color.White);
            colorsBox = new FlowLayoutPanel();
            buttonNewColor = new Button();

            colorsBox.SuspendLayout();
            SuspendLayout();

            // 
            // buttonColorPrevious
            // 
            buttonColorPrevious.Location = new Point(6, 76);
            buttonColorPrevious.Name = "buttonColorPrevious";
            buttonColorPrevious.Size = new Size(30, 30);
            buttonColorPrevious.TabIndex = 5;
            buttonColorPrevious.Click += ButtonColor_Click;

            // 
            // buttonColorSelect
            // 
            buttonColorSelect.Location = new Point(6, 30);
            buttonColorSelect.Name = "buttonColorSelect";
            buttonColorSelect.Size = new Size(30, 30);

            // 
            // buttonNewColor
            // 
            buttonNewColor.BackgroundImage = Resources.Add;
            buttonNewColor.FlatAppearance.BorderSize = 0;
            buttonNewColor.FlatStyle = FlatStyle.Flat;
            buttonNewColor.Location = new Point(3, 3);
            buttonNewColor.Name = "buttonNewColor";
            buttonNewColor.Size = new Size(24, 24);
            buttonNewColor.TabIndex = 0;
            buttonNewColor.UseVisualStyleBackColor = true;
            buttonNewColor.Click += buttonNewColor_Click;

            // 
            // colorsBox
            // 
            colorsBox.AutoScroll = true;
            colorsBox.Controls.Add(buttonNewColor);
            colorsBox.Location = new Point(48, 27);
            colorsBox.Name = "colorsBox";
            colorsBox.Size = new Size(175, 92);
            colorsBox.TabIndex = 2;

            // 
            // groupBoxColors
            // 
            Controls.Add(buttonColorPrevious);
            Controls.Add(colorsBox);
            Controls.Add(buttonColorSelect);
            Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            TabStop = false;
            Text = "Colors";

            colorsBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void IntializeColorsBox()
        {
            foreach (Color color in DEFAULT_COLORS)
            {
                ColorButton colorButton = new ColorButton(color);
                colorButton.Click += ButtonColor_Click;
                colorsBox.Controls.Add(colorButton);
            }
        }

        private void buttonNewColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ColorButton newColorButton = new ColorButton(colorDialog.Color);
                newColorButton.Click += ButtonColor_Click;

                ColorButton? buttonToRemove = null;

                foreach (Button button in colorsBox.Controls)
                {
                    if (button is ColorButton)
                    {
                        ColorButton colorButton = (ColorButton)button;

                        if (colorButton.GetColor() == newColorButton.GetColor())
                        {
                            buttonToRemove = colorButton;
                            break;
                        }
                    }
                }
                if (buttonToRemove != null)
                {
                    colorsBox.Controls.Remove(buttonToRemove);
                }
                colorsBox.Controls.Add(newColorButton);
                colorsBox.Controls.SetChildIndex(newColorButton, 1);
            }
        }

        private void ButtonColor_Click(object sender, EventArgs e)
        {
            Color selectColor = ((ColorButton)sender).GetColor();
            buttonColorPrevious.SetColor(buttonColorSelect.BackColor);
            buttonColorSelect.SetColor(selectColor);
        }
    }
}
