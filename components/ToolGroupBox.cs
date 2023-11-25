using Paint.Properties;

namespace Paint.components
{
    internal class ToolGroupBox: GroupBox
    {
        private static Image[] DEFOULT_TOOLS = new Image[] {
            Resources.Pen, Resources.Eraser, Resources.Fill, 
            Resources.Line, Resources.Rectangle, Resources.Circle,
    };

        private ImageButton buttonToolSelect;

        private ImageButton buttonToolPrevious;

        private FlowLayoutPanel toolsBox;

        public delegate void ClickHeandler(object sender, EventArgs e);

        public event ClickHeandler Click;

        public ToolGroupBox()
        {
            InitializeComponents();
            IntializeToolsBox();
        }

        private void InitializeComponents()
        {
            buttonToolSelect = new ImageButton(DEFOULT_TOOLS[0]);
            buttonToolPrevious = new ImageButton(DEFOULT_TOOLS[1]);
            toolsBox = new FlowLayoutPanel();

            toolsBox.SuspendLayout();
            SuspendLayout();

            // 
            // buttonToolSelect
            // 
            buttonToolSelect.Location = new Point(6, 30);
            buttonToolSelect.Name = "buttonToolSelect";
            buttonToolSelect.FlatStyle = FlatStyle.Standard;
            buttonToolSelect.Size = new Size(32, 32);
            buttonToolSelect.Click += Send_Event;

            // 
            // buttonToolPrevious
            // 
            buttonToolPrevious.Location = new Point(6, 76);
            buttonToolPrevious.Name = "buttonToolPrevious";
            buttonToolPrevious.Size = new Size(32, 32);
            buttonToolPrevious.FlatStyle = FlatStyle.Standard;
            buttonToolPrevious.Click += SelectTool_Click;
            buttonToolPrevious.Click += Send_Event;

            // 
            // toolsBox
            // 
            toolsBox.AutoScroll = true;
            toolsBox.Location = new Point(48, 27);
            toolsBox.Name = "toolsBox";
            toolsBox.Size = new Size(175, 92);
            toolsBox.TabIndex = 2;

            // 
            // groupBoxTools
            // 
            Controls.Add(buttonToolSelect);
            Controls.Add(buttonToolPrevious);
            Controls.Add(toolsBox);
            Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            TabStop = false;
            Text = "Colors";

            toolsBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void IntializeToolsBox()
        {
            foreach(Image tool in DEFOULT_TOOLS)
            {
                ImageButton imageButton = new ImageButton(tool);
                imageButton.Click += SelectTool_Click;
                imageButton.Click += Send_Event;
                toolsBox.Controls.Add(imageButton);
            }
        }

        private void SelectTool_Click(object sender, EventArgs e)
        {
            Image selectTool = ((ImageButton)sender).Image;
            buttonToolPrevious.Image = buttonToolSelect.Image;
            buttonToolSelect.Image = selectTool;
        }

        private void Send_Event(object sender, EventArgs e)
        {
            Click.Invoke(this, e);
        }
    }
}
