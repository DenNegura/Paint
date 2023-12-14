using Paint.components.Tools;
using Paint.Properties;

namespace Paint.components
{
    internal class ToolGroupBox: CustomGroupBox
    {

        private static readonly string TITLE = "Tools";

        private static readonly Tool DEFAULT_SELECT_TOOL = Tool.PEN;

        private static readonly Tool DEFAULT_PREVIOUS_TOOL = Tool.ERASER;

        private ImageButton selectButton;

        private ImageButton previousButton;

        private FlowLayoutPanel layout;

        public event EventHandler OnSelect;

        public ToolGroupBox(): base(TITLE)
        {
            selectButton = (ImageButton) initSelectButton(new ImageButton());
            previousButton = (ImageButton) initPreviousButton(new ImageButton());
            layout = initLayout(new FlowLayoutPanel());

            selectButton.Image = DEFAULT_SELECT_TOOL.image;
            selectButton.Tag = DEFAULT_SELECT_TOOL;
            selectButton.Click += OnSelect_Click;

            previousButton.Image = DEFAULT_PREVIOUS_TOOL.image;
            previousButton.Tag = DEFAULT_PREVIOUS_TOOL;
            previousButton.Click += OnSelect_Click;
            previousButton.Click += ChangeTool_Click;

            InitDefalutValues();
        }

        private void InitDefalutValues()
        {
            foreach(Tool tool in Tool.TOOLS)
            {
                ImageButton imageButton = new ImageButton();
                imageButton.Image = tool.image;
                imageButton.Tag = tool;
                imageButton.Click += ChangeTool_Click;
                imageButton.Click += OnSelect_Click;
                layout.Controls.Add(imageButton);
            }
        }

        private void ChangeTool_Click(object sender, EventArgs e)
        {
            Tool? tool = ((ImageButton)sender).Tag as Tool;
            if (tool != null && (selectButton.Tag as Tool).name != tool.name)
            {
                previousButton.Image = selectButton.Image;
                previousButton.Tag = selectButton.Tag;
                selectButton.Image = tool.image;
                selectButton.Tag = tool;
            }
        }

        private void OnSelect_Click(object sender, EventArgs e)
        {
            OnSelect.Invoke(((ImageButton)sender).Tag as Tool, EventArgs.Empty);
        }

        public Tool? GetSelectTool()
        {
            return selectButton.Tag as Tool; 
        }
    }
}
