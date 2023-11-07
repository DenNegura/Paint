using Paint.components;

namespace Paint
{
    public partial class Form1 : Form
    {

        Color[] defaultColors = new Color[] {
            Color.Red, Color.Blue, Color.Black,
            Color.Yellow, Color.Green, Color.White,
            Color.Orange, Color.Salmon, Color.Purple
        };

        public Form1()
        {
            InitializeComponent();
            IntializeColorsBox();
        }

        private void IntializeColorsBox()
        {
            foreach (Color color in defaultColors)
            {
                ColorButton colorButton = new ColorButton(color);
                colorsBox.Controls.Add(colorButton);
            }
        }

        private void buttonNewColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ColorButton newColorButton = new ColorButton(colorDialog.Color);

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
    }
}