using Paint.components.Drawable;
using Paint.components.Figurs;
using Paint.components.Tools;
using Paint.Properties;

namespace Paint.components
{
    internal class ToolGroupBox: CustomGroupBox
    {
        private class Tool
        {
            public EnumTool tool;

            public Image image;

            public Tool(Image image, EnumTool tool)
            {
                this.tool = tool;
                this.image = image;
            }
        }

        private static Tool[] DEFAULT_TOOLS = new Tool[]
        {
            new Tool(Resources.Pen, EnumTool.PEN),
            new Tool(Resources.Eraser, EnumTool.ERASER),
            new Tool(Resources.Fill, EnumTool.FILL),
            new Tool(Resources.ColourPicker, EnumTool.COLOUR_PICKER)
        };

        private static readonly string TITLE = "Tools";

        private static readonly Tool DEFAULT_SELECT_TOOL = new Tool(Resources.Pen, EnumTool.PEN);

        private static readonly Tool DEFAULT_PREVIOUS_TOOL = new Tool(Resources.Eraser, EnumTool.ERASER);

        private ImageButton selectButton;

        private ImageButton previousButton;

        private FlowLayoutPanel layuot;

        public event EventHandler OnSelect;

        public ToolGroupBox(): base(TITLE)
        {
            selectButton = (ImageButton) initSelectButton(new ImageButton());
            previousButton = (ImageButton) initPreviousButton(new ImageButton());
            layuot = initLayout(new FlowLayoutPanel());

            selectButton.Image = DEFAULT_SELECT_TOOL.image;
            selectButton.Tag = DEFAULT_SELECT_TOOL;
            selectButton.Click += OnSelect_Click;

            previousButton.Image = DEFAULT_PREVIOUS_TOOL.image;
            previousButton.Tag = DEFAULT_PREVIOUS_TOOL;
            previousButton.Click += ChangeTool_Click;
            previousButton.Click += OnSelect_Click;

            InitDefalutValues();
        }

        private void InitDefalutValues()
        {
            foreach(Tool tool in DEFAULT_TOOLS)
            {
                ImageButton imageButton = new ImageButton();
                imageButton.Image = tool.image;
                imageButton.Tag = tool;
                imageButton.Click += ChangeTool_Click;
                imageButton.Click += OnSelect_Click;
                layuot.Controls.Add(imageButton);
            }
        }

        private void ChangeTool_Click(object sender, EventArgs e)
        {
            Tool? tool = ((ImageButton)sender).Tag as Tool;
            if ( tool != null && (selectButton.Tag as Tool).tool != tool.tool)
            {
                previousButton.Image = selectButton.Image;
                previousButton.Tag = selectButton.Tag;
                selectButton.Image = tool.image;
                selectButton.Tag = tool;
            }
        }

        private void OnSelect_Click(object sender, EventArgs e)
        {
            Tool? tool = ((ImageButton)sender).Tag as Tool;
            if (tool != null)
            {
                OnSelect.Invoke(tool.tool, e);
            }
        }
    }
}
