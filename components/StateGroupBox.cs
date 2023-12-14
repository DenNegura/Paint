using Paint.components.Drawable;
using Paint.components.Tools;

namespace Paint.components
{
    internal class StateGroupBox: CustomGroupBox
    {

        private static readonly string TITLE = "In use";

        private ImageButton toolButton;

        private ColorButton colorButton;

        private Label toolLabel;

        private Label colorLabel;

        public StateGroupBox() : base(TITLE)
        {
            toolButton = (ImageButton)initSelectButton(new ImageButton());
            colorButton = (ColorButton)initPreviousButton(new ColorButton());

            toolLabel = new Label();
            toolLabel.AutoSize = true;
            toolLabel.Location = new Point(44, 36);
            toolLabel.Name = "toolUseLabel";
            toolLabel.Size = new Size(90, 22);
            toolLabel.Text = "";

            colorLabel = new Label();
            colorLabel.AutoSize = true;
            colorLabel.Location = new Point(44, 84);
            colorLabel.Name = "colorUseLabel";
            colorLabel.Size = new Size(90, 22);
            colorLabel.Text = "";

            Controls.Add(toolLabel);
            Controls.Add(colorLabel);
        }

        public void SetFigure(Figure figure) 
        {
            toolButton.Image = figure.image;
            toolButton.Tag = figure;
            toolLabel.Text = figure.name;
        }

        public void SetTool(Tool tool) 
        { 
            toolButton.Image = tool.image;
            toolButton.Tag = tool;
            toolLabel.Text = tool.name;
        }

        public void SetColor(Color color)
        {
            colorButton.Color = color;
            colorLabel.Text = color.Name;
        }
    }
}
